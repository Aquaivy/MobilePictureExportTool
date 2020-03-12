using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureExportTools.Model;

namespace PictureExportTools.Controller
{
    public class SDCardExportController : ExportController
    {
        public SDCardExportController(Settings settings)
            : base(settings)
        {
        }

        public override void GetDeviceFiles(PathMap map)
        {
            throw new NotImplementedException();
        }
    }
}
