﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 手机照片导出备份工具.Model;

namespace 手机照片导出备份工具.Controller
{
    abstract class RemoveSyncController : ISyncRemove
    {
        protected Settings settings;

        public RemoveSyncController(Settings settings)
        {
            this.settings = settings;
        }

        public void RemoveLocalFile(FileData file)
        {
            var src = file.Path;
            var dest = Path.Combine(settings.RecycleBinPath, file.ParentName, file.Name);
            var dir = Path.Combine(settings.RecycleBinPath, file.ParentName);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (File.Exists(dest))
                throw new IOException($"文件 {file.Name} 已存在，无法移动到 {dest}");

            File.Move(src, dest);
        }

        public string GetRemotePathWithParentName(string parent)
        {
            foreach (var map in settings.Device.PathMap)
            {
                if (map.Name == parent)
                {
                    return map.RemotePath;
                }
            }

            return string.Empty;
        }

        public abstract void RemoveRemoteFile(FileData file);

    }
}
