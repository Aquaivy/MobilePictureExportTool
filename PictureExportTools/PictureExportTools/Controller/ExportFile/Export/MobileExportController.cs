using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleARCoreInternal;
using PictureExportTools.Model;

namespace PictureExportTools.Controller
{
    public class MobileExportController : ExportController
    {
        public MobileExportController(Settings settings)
            : base(settings)
        {

        }

        public override void GetDeviceFiles(PathMap map)
        {
            var cmd = "shell ls " + map.RemotePath.Replace(" ", @"\ ");

            var adbPath = ShellHelper.GetAdbPath(SettingController.Setting.AndroidSdkRootPath);
            ShellHelper.RunCommand(adbPath, cmd, out string output, out string errors);

            if (!string.IsNullOrEmpty(errors))
            {
                throw new Exception($"手机 GetDeviceFiles导出出错\r\n\r\n{errors}");
            }

            var device_files_path = output.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            List<FileData> device_files = new List<FileData>();

            for (int i = 0; i < device_files_path.Length; i++)
            {
                var fd = new FileData(map.RemotePath + "/" + device_files_path[i]);
                device_files.Add(fd);
            }
        }

        protected override void ExportDeviceFileToLocal()
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
                    RaiseExportedOneFileEvent(new ExportedOneFileEventArgs { FileData = file, DistPath = dist_dir, Index = idx, TotalCount = total });
                }

                idx++;
            }
        }
    }
}
