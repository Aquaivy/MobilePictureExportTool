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

        [NonSerialized]
        public DeviceConfig Device;

        //[NonSerialized]
        //public SyncRemoveConfig SyncRemove;
    }

    public class DeviceConfig
    {
        public RemoteType Type;

        public PathMap[] PathMap;
    }

    //public class SyncRemoveConfig
    //{
    //    public PathMap[] SDCardPathMap;
    //    public PathMap[] MobilePathMap;
    //}


    public class PathMap
    {
        public string Name;
        public string RemotePath;
    }
}
