using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 手机照片导出备份工具.Controller
{
    public static class ImageTools
    {
        public static Image LoadPictureToImage(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            int byteLength = (int)fileStream.Length;
            byte[] fileBytes = new byte[byteLength];
            fileStream.Read(fileBytes, 0, byteLength);
            //文件流关闭,文件解除锁定
            fileStream.Close();
            var image = Image.FromStream(new MemoryStream(fileBytes));
            fileStream.Dispose();

            return image;
        }
    }
}
