using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aquaivy.Core.Utilities
{
    /// <summary>
    /// 行尾
    /// </summary>
    public enum LineEnding
    {
        /// <summary>
        /// 回车换行，\r\n
        /// </summary>
        CRLF,

        /// <summary>
        /// 回车，\r
        /// </summary>
        CR,

        /// <summary>
        /// 换行，\n
        /// </summary>
        LF,

        /// <summary>
        /// 其他的
        /// </summary>
        Others
    }
}
