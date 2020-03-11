using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 手机照片导出备份工具.Model;

namespace 手机照片导出备份工具.Controller
{
    public interface ISyncRemove : ISyncOperation
    {
        void RemoveLocalFile(FileData file);

        void RemoveRemoteFile(FileData file);
    }
}
