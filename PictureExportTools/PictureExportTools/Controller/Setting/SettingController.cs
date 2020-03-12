using Aquaivy.Core.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureExportTools.Model;

namespace PictureExportTools.Controller
{
    public class SettingController : Singleton<SettingController>
    {
        private static string setting_file_name = "Settings.json";
        public static string RootPath = Environment.CurrentDirectory;
        private static string setting_full_path = Path.Combine(RootPath, setting_file_name);

        private SettingController()
        {
        }

        private static Settings m_setting;
        public static Settings Setting { get { return m_setting; } }

        public void Load()
        {
            var json = File.ReadAllText(setting_full_path, Encoding.UTF8);
            m_setting = JsonConvert.DeserializeObject<Settings>(json);

            var dp = RootPath + m_setting.DeviceConfigPath;
            json = File.ReadAllText(dp, Encoding.UTF8);
            var device = JsonConvert.DeserializeObject<DeviceConfig>(json);

            m_setting.Device = device;
        }


        public void CreateDefaultSettings()
        {
            // Settings
            Settings settings = new Settings
            {
                AndroidSdkRootPath = @"D:\Softwares\Android\android-sdk\",
                CloudPath= @"E:\cloud_path\",
                LocalBackupPath = @"E:\backup_path\",
                RecycleBinPath = @"E:\backup_path\_recycle_bin",
                DeviceConfigPath = @"\Configs\Mobile.config",
            };

            // SyncRemoveConfig
            //SyncRemoveConfig removeConfig = new SyncRemoveConfig();
            //removeConfig.SDCardPathMap = new PathMap[] {
            //    new PathMap { Name = @"Saved Pictures", RemotePath = @"E:\sdcard_path\Saved Pictures" },
            //    new PathMap { Name = @"WeiXin", RemotePath = @"E:\sdcard_path\WeiXin" },
            //    new PathMap { Name = @"Camera", RemotePath = @"E:\sdcard_path\Camera" },
            //};
            //removeConfig.MobilePathMap = new PathMap[] {
            //    new PathMap { Name = @"Saved Pictures", RemotePath = @"sdcard/DCIM/Saved Pictures" },
            //    new PathMap { Name = @"WeiXin", RemotePath = @"sdcard/WeiXin" },
            //    new PathMap { Name = @"Camera", RemotePath = @"sdcard/DCIM/Camera" },
            //};


            // 赋值
            //settings.SyncRemove = removeConfig;

            // save
            var json = JsonConvert.SerializeObject(settings);
            File.WriteAllText(setting_full_path, json, Encoding.UTF8);
        }

        public PathMap GetPathMapWithName(string name)
        {
            if (m_setting.Device == null)
                return null;

            var maps = m_setting.Device.PathMap;
            foreach (var m in maps)
            {
                if (m.Name == name)
                {
                    return m;
                }
            }

            return null;
        }
    }
}
