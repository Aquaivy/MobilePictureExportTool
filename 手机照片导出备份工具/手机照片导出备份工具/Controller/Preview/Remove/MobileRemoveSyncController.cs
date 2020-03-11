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
    class MobileRemoveSyncController : RemoveSyncController
    {
        public MobileRemoveSyncController(Settings settings)
            : base(settings)
        {

        }

        public override void RemoveRemoteFile(FileData file)
        {
            var remote_path = GetRemotePathWithParentName(file.ParentName) + "/" + file.Name;

            var adbPath = ShellHelper.GetAdbPath(SettingController.Setting.AndroidSdkRootPath);
            ShellHelper.RunCommand(adbPath,
                "shell rm " + remote_path,
                out string output, out string errors);

            if (!string.IsNullOrEmpty(errors))
            {
                throw new Exception($"手机文件 {remote_path} 删除出错\r\n\r\n{errors}");
            }
        }

        //private string GetRemotePathWithParentName(string parent)
        //{
        //    foreach (var map in settings.Device.PathMap)
        //    {
        //        if (map.Name == parent)
        //        {
        //            return map.RemotePath;
        //        }
        //    }

        //    return string.Empty;
        //}
    }
}
