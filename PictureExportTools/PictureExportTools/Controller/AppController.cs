using Aquaivy.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PictureExportTools.Model;

namespace PictureExportTools.Controller
{
    public class AppController : Singleton<AppController>
    {
        //public const string PicturesSearchPartten = "(*.jpg|*.png|*.jpeg|*.ico)";
        public static string[] PicturesSearchParttern = new string[] {
            ".jpg",
            ".jpeg",
            ".png",
            ".ico",
        };


        private AppController()
        {

        }

        public void LoadReferenceAssembly()
        {
            //Assembly.LoadFile(@"\\bin\\Newtonsoft.Json.dll");
        }
    }
}
