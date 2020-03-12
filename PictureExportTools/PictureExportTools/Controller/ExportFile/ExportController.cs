using Aquaivy.Core.Utilities;
using GoogleARCoreInternal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureExportTools.Model;

namespace PictureExportTools.Controller
{
    public abstract class ExportController : IExport
    {
        protected Settings settings;

        protected List<FileData> cloud_files = new List<FileData>(4096);
        protected List<FileData> device_files = new List<FileData>(1024);
        protected List<FileData> need_backup_files = new List<FileData>(256);

        public event EventHandler<ExportedOneFileEventArgs> ExportedOneFile;

        public ExportController(Settings settings)
        {
            this.settings = settings;
        }

        public static ExportController Create(Settings settings)
        {
            if (settings.Device.Type == RemoteType.Mobile)
            {
                return new MobileExportController(settings);
            }
            else if (settings.Device.Type == RemoteType.SDCard)
            {
                return new SDCardExportController(settings);
            }

            return null;
        }

        public void Export(PathMap map)
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
            ExportDeviceFileToLocal();
        }

        private void GetCloudFiles()
        {
            // 获取远端文件
            var cloud_path = settings.CloudPath;
            var cloud_files_path = FileUtility.GetFiles(cloud_path);
            for (int i = 0; i < cloud_files_path.Length; i++)
            {
                var fd = new FileData(cloud_files_path[i]);
                cloud_files.Add(fd);
            }

            // 获取本地LocalBackup文件
            if (settings.IncludeLocalBackupPathWhenSearchCloudFiles)
            {
                var local_path = settings.LocalBackupPath;
                var local_path_files = FileUtility.GetFiles(local_path);
                for (int i = 0; i < local_path_files.Length; i++)
                {
                    var fd = new FileData(local_path_files[i]);
                    cloud_files.Add(fd);
                }
            }
        }

        public abstract void GetDeviceFiles(PathMap map);


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

        protected abstract void ExportDeviceFileToLocal();

        protected void RaiseExportedOneFileEvent(ExportedOneFileEventArgs exportedOneFile)
        {
            ExportedOneFile?.Invoke(this, exportedOneFile);
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
