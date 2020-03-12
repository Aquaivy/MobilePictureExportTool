using Aquaivy.Core.Utilities;
using GoogleARCoreInternal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 手机照片导出备份工具.Model;

namespace 手机照片导出备份工具.Controller
{
    public class ExportController
    {
        private List<FileData> m_files = new List<FileData>();

        private BaseExportController expertController;

        private List<FileData> cloud_files = new List<FileData>();
        private List<FileData> device_files = new List<FileData>();
        private List<FileData> need_backup_files = new List<FileData>();

        public event EventHandler<ExportedOneFileEventArgs> ExportedOneFile;

        public ExportController(Settings settings)
        {
            if (settings.Device.Type == RemoteType.Mobile)
            {
                expertController = new MobileExportController(settings);
            }
            else if (settings.Device.Type == RemoteType.SDCard)
            {
                expertController = new SDCardExportController(settings);
            }
        }

        public List<FileData> Files { get { return m_files; } }

        public void Export(FileData file)
        {
            expertController.Export(file);
        }

        public void Clear()
        {
            m_files.Clear();
        }

        public void Compare(PathMap map)
        {
            //1.遍历Cloud Path，获取所有已经备份文件
            //2.遍历Remote Path，获取所有设备中的文件
            //3.对比，得出需要备份的文件
            //4.将需要备份的文件备份出来

            cloud_files?.Clear();
            device_files?.Clear();
            need_backup_files?.Clear();

            GetCloudFiles();
            GetDeviceFiles(map);
            CompareDeviceAndCloud();
            ExportDeviceFileToPC();
        }

        private void GetCloudFiles()
        {
            var cloud_path = SettingController.Setting.CloudPath;
            var cloud_files_path = FileUtility.GetFiles(cloud_path);
            cloud_files = new List<FileData>(cloud_files_path.Length);
            for (int i = 0; i < cloud_files_path.Length; i++)
            {
                var fd = new FileData(cloud_files_path[i]);
                cloud_files.Add(fd);
            }
        }

        private void GetDeviceFiles(PathMap map)
        {
            //var path = map.RemotePath.Replace(" ", @"\ ");
            var cmd = "shell ls " + map.RemotePath.Replace(" ", @"\ ");

            var adbPath = ShellHelper.GetAdbPath(SettingController.Setting.AndroidSdkRootPath);
            ShellHelper.RunCommand(adbPath, cmd, out string output, out string errors);

            if (!string.IsNullOrEmpty(errors))
            {
                throw new Exception($"手机 GetDeviceFiles导出出错\r\n\r\n{errors}");
            }

            var device_files_path = output.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < device_files_path.Length; i++)
            {
                var fd = new FileData(map.RemotePath + "/" + device_files_path[i]);
                device_files.Add(fd);
            }
        }

        private void CompareDeviceAndCloud()
        {
            for (int i = 0; i < device_files.Count; i++)
            {
                var df = device_files[i];
                if (cloud_files.FirstOrDefault(o => o.Name == df.Name) == null)
                {
                    //新增的图片
                    need_backup_files.Add(df);
                }
            }
        }

        private void ExportDeviceFileToPC()
        {
            int idx = 0;
            int total = need_backup_files.Count;
            foreach (var file in need_backup_files)
            {
                var dist_dir = Path.Combine(SettingController.Setting.LocalBackupPath, file.ParentName);
                if (!Directory.Exists(dist_dir))
                    Directory.CreateDirectory(dist_dir);

                var device_path = file.Path.Replace(" ", @"\ ");

                var cmd = $"pull {device_path} {dist_dir}";

                var adbPath = ShellHelper.GetAdbPath(SettingController.Setting.AndroidSdkRootPath);
                ShellHelper.RunCommand(adbPath, cmd, out string output, out string errors);

                if (!string.IsNullOrEmpty(errors))
                {
                    throw new Exception($"手机 GetDeviceFiles导出出错\r\n\r\n{errors}");
                }
                else
                {
                    ExportedOneFile?.Invoke(this, new ExportedOneFileEventArgs { FileData = file, DistPath = dist_dir, Index = idx, TotalCount = total });
                }

                idx++;
                //adb pull sdcard/DCIM/Camera/IMG_20191008_194251.jpg E:\
            }
        }
    }

    public class ExportedOneFileEventArgs : EventArgs
    {
        public FileData FileData { get; set; }
        public string DistPath { get; set; }
        public int Index { get; set; }
        public int TotalCount { get; set; }
    }
}
