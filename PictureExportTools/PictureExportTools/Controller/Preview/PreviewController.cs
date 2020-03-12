using Aquaivy.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureExportTools.Model;

namespace PictureExportTools.Controller
{
    public abstract class PreviewController
    {
        protected Settings settings;
        private List<FileData> m_files = new List<FileData>();

        public PreviewController(Settings settings)
        {
            this.settings = settings;
        }

        public static PreviewController Create(Settings settings)
        {
            if (settings.Device.Type == RemoteType.Mobile)
            {
                return new MobileRemoveSyncController(settings);
            }
            else if (settings.Device.Type == RemoteType.SDCard)
            {
                return new SDCardRemoveSyncController(settings);
            }

            return null;
        }

        public List<FileData> Files { get { return m_files; } }

        public void ReadFiles(string path)
        {
            Clear();

            var files = FileUtility.GetFiles(path, "*.*", SearchOption.AllDirectories).ToList();
            for (int i = 0; i < files.Count; i++)
            {
                bool isPic = false;
                var pic = files[i];

                for (int ext = 0; ext < AppController.PicturesSearchParttern.Length; ext++)
                {
                    if (pic.ToLower().EndsWith(AppController.PicturesSearchParttern[ext]))
                    {
                        isPic = true;
                        break;
                    }
                }

                if (!isPic)
                {
                    files.RemoveAt(i);
                    i--;
                }
            }

            foreach (var f in files)
            {
                var data = new FileData(f);
                m_files.Add(data);
            }
        }

        public void Clear()
        {
            m_files.Clear();
        }

        public void RemoveFile(FileData file)
        {
            int idx = m_files.IndexOf(file);
            if (idx >= 0)
            {
                RemoveLocalFile(file);
                RemoveRemoteFile(file);
                m_files.RemoveAt(idx);
            }
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


        public abstract void RemoveRemoteFile(FileData file);

        public void RenameFile(FileData file)
        {

        }
    }
}
