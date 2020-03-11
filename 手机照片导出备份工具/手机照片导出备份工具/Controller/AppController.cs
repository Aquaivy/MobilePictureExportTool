using Aquaivy.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 手机照片导出备份工具.Model;

namespace 手机照片导出备份工具.Controller
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
    }
}
