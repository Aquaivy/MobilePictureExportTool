using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureExportTools.Controller;

namespace PictureExportTools.Model
{

    public class Settings
    {
        public string AndroidSdkRootPath;
        public string CloudPath;
        public string LocalBackupPath;
        public string RecycleBinPath;
        public string DeviceConfigPath;

        public bool IncludeLocalBackupPathWhenSearchCloudFiles;

        [NonSerialized]
        public DeviceConfig Device;
    }

    public class DeviceConfig
    {
        public RemoteType Type;

        public PathMap[] PathMap;
    }


    public class PathMap
    {
        public string Name;
        public string RemotePath;
    }
}
