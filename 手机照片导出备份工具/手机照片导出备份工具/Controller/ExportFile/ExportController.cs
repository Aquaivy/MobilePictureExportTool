using Aquaivy.Core.Utilities;
using GoogleARCoreInternal;
using System;
using System.Collections.Generic;
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

        private string[] cloud_files;
        private string[] device_files;

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

            GetCloudFiles();
            GetDeviceFiles(map);
            CompareDeviceAndCloud();
            ExportDeviceFileToPC();
        }

        private void GetCloudFiles()
        {
            var cloud_path = SettingController.Setting.CloudPath;
            cloud_files = FileUtility.GetFiles(cloud_path);
        }

        private void GetDeviceFiles(PathMap map)
        {
            var path = map.RemotePath.Replace(" ", @"\ ");
            var cmd = "shell ls " + path;

            var adbPath = ShellHelper.GetAdbPath(SettingController.Setting.AndroidSdkRootPath);
            ShellHelper.RunCommand(adbPath, cmd, out string output, out string errors);

            if (!string.IsNullOrEmpty(errors))
            {
                throw new Exception($"手机 GetDeviceFiles导出出错\r\n\r\n{errors}");
            }

            device_files = output.Split('\r');
        }

        private void CompareDeviceAndCloud()
        {

        }

        private void ExportDeviceFileToPC()
        {

        }
    }
}
