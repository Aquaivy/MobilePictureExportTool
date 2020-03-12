using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureExportTools.Model;

namespace PictureExportTools.Controller
{
    class SDCardRemoveSyncController : BaseRemoveSyncController
    {
        public SDCardRemoveSyncController(Settings settings)
            : base(settings)
        {

        }

        public override void RemoveRemoteFile(FileData file)
        {
            var remote_path = Path.Combine(GetRemotePathWithParentName(file.ParentName), file.Name);
            if (File.Exists(remote_path))
            {
                File.Delete(remote_path);
            }
        }

        
    }
}
