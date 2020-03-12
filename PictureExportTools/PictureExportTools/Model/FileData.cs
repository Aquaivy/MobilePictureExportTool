using Aquaivy.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureExportTools.Model
{
    public class FileData
    {
        public FileData(string path)
        {
            Path = path;
            Name = System.IO.Path.GetFileName(path);
            ParentName = PathEx.GetParentDirectoryName(path);
            Extension = System.IO.Path.GetExtension(path);
        }

        public string Name;
        public string ParentName;
        public string Path;
        public string Extension;
    }
}
