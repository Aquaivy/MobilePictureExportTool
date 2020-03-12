using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aquaivy.Core.Utilities
{
    /// <summary>
    /// Path类扩展
    /// </summary>
    public class PathEx
    {
        /// <summary>
        /// 返回路径的目录名，
        /// 举例：
        ///     D:\Parent\file.txt      D:\Parent
        ///     \Parent\file.txt        \Parent
        ///     Parent\file.txt         Parent
        ///     \file.txt               \
        ///     file.txt                空字符串
        ///     D:\Parent\file\         D:\Parent\file
        ///     D:\Parent\file          D:\Parent
        ///     \Parent\file            \Parent
        ///     \file                   \
        ///     \                       空字符串
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetDirectoryName(string path)
        {
            string dir = Path.GetDirectoryName(path);
            return dir ?? string.Empty;
        }

        /// <summary>
        /// 返回路径的前一层目录名，
        /// 举例：
        ///     D:\Parent\file.txt      Parent
        ///     \Parent\file.txt        Parent
        ///     Parent\file.txt         Parent
        ///     \file.txt               空字符串
        ///     file.txt                空字符串
        ///     D:\Parent\file\         file
        ///     D:\Parent\file          Parent
        ///     \Parent\file            Parent
        ///     \file                   空字符串
        ///     \                       空字符串
        /// </summary>
        /// <param name="path"></param>
        public static string GetParentDirectoryName(string path)
        {
            return Path.GetFileName(GetDirectoryName(path));
        }

        /// <summary>
        /// 返回去掉扩展名后的路径(给Unity使用)，
        /// 举例：
        ///     D:\Parent\file.txt      D:\Parent\file
        ///     \Parent\file.txt        \Parent\file
        ///     Parent\file.txt         Parent\file
        ///     \file.txt               \file
        ///     file.txt                file
        ///     D:\Parent\file\         D:\Parent\file
        ///     D:\Parent\file          D:\Parent\file
        ///     \Parent\file            \Parent\file
        ///     \file                   \file
        ///     \                       空字符串
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetPathWithoutExtension(string path)
        {
            var dir = GetDirectoryName(path);
            var name = Path.GetFileNameWithoutExtension(path);
            return Path.Combine(dir, name);
        }

        /// <summary>
        /// 返回修改了文件名之后的路径，
        /// 只改文件名，不改父级路径，不改扩展名
        /// </summary>
        /// <param name="path">文件完整路径</param>
        /// <param name="newName">文件新名字，不包含扩展名</param>
        public static string GetNewFileName(string path, string newName)
        {
            var dir = Path.GetDirectoryName(path);
            var ext = Path.GetExtension(path);
            return Path.Combine(dir, newName + ext);
        }

        /// <summary>
        /// 修改目录中的分隔符为【正分隔符】，形如 "/"
        /// </summary>
        /// <param name="path"></param>
        public static string ChangeSeparatorToPositive(string path)
        {
            return path.Replace('\\', '/');
        }

        /// <summary>
        /// 修改目录中的分隔符为【反分隔符】，形如 "\"
        /// </summary>
        /// <param name="path"></param>
        public static string ChangeSeparatorToPassive(string path)
        {
            return path.Replace('/', '\\');
        }

        /// <summary>
        /// 重置为Windows系统默认【反分隔符】，形如 "\"
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ResetToWindowsSeparator(string path) => ChangeSeparatorToPassive(path);

        /// <summary>
        /// 将目录中的分隔符由"双斜杠"改为"单斜杠"
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ChangeSeparatorToSingle(string path)
        {
            return path.Replace(@"//", @"/").Replace(@"\\", @"\");
        }

        /// <summary>
        /// 将目录中的分隔符由"单斜杠"改为"双斜杠"
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ChangeSeparatorToDouble(string path)
        {
            return ChangeSeparatorToSingle(path).Replace(@"/", @"//").Replace(@"\", @"\\");
        }

        /// <summary>
        /// 将目录中的【反分隔符\和"】转为【转义字符\\和\"】
        /// （暂有问题）
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ChangePathToEscape(string path)
        {
            //path = ResetToWindowsSeparator(path);
            //path = ChangeSeparatorToSingle(path);
            return path.Replace(@"\", @"\\").Replace("\"", "\\\"");
        }

        /// <summary>
        /// 将目录中的【转义字符\\和\"】转为【反分隔符\和"】
        /// （暂有问题）
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ChangeEscapeToPath(string path)
        {
            return path.Replace(@"\\", @"\").Replace("\\\"", "\"");
        }
    }
}
