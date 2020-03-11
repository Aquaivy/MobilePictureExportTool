using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 手机照片导出备份工具.Model;

namespace 手机照片导出备份工具.Controller
{
    public class PreviewController
    {
        private List<FileData> m_files = new List<FileData>();

        private RemoveSyncController removeController;


        public PreviewController(Settings settings)
        {
            if (settings.Device.Type == RemoteType.Mobile)
            {
                removeController = new MobileRemoveSyncController(settings);
            }
            else if (settings.Device.Type == RemoteType.SDCard)
            {
                removeController = new SDCardRemoveSyncController(settings);
            }

        }

        public List<FileData> Files { get { return m_files; } }

        public void ReadFiles(string path)
        {
            Clear();

            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).ToList();
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
                removeController.RemoveLocalFile(file);
                removeController.RemoveRemoteFile(file);
                m_files.RemoveAt(idx);
            }
        }

        public void RenameFile(FileData file)
        {

        }
    }
}
