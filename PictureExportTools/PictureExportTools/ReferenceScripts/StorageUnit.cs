using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aquaivy.Core.Utilities
{
    /// <summary>
    /// 存储单元，
    /// 8Bit = 1Byte、KB、MB、GB、TB、PB、EB、ZB、YB、DB、NB
    /// 1KB=1024Byte字节
    /// </summary>
    public struct StorageUnit
    {
        /// <summary>
        /// 1KB的byte数量
        /// </summary>
        public const long KBSize = 1024;

        /// <summary>
        /// 1MB的byte数量
        /// </summary>
        public const long MBSize = 1024 * 1024;

        /// <summary>
        /// 1GB的byte数量
        /// </summary>
        public const long GBSize = 1024 * 1024 * 1024;

        /// <summary>
        /// 1TB的byte数量
        /// </summary>
        public const long TBSize = (long)(1024f * 1024 * 1024 * 1024);        //编译器默认按照int计算，所以在编译阶段超范围了


        private long bytes;

        /// <summary>
        /// 获取转换为Bit后的值
        /// </summary>
        public long Bit { get { return bytes * 8; } }

        /// <summary>
        /// 获取Byte数量值
        /// </summary>
        public long Byte { get { return bytes; } }

        /// <summary>
        /// 获取转换为KB后的值
        /// </summary>
        public float TotalKBs { get { return bytes * 1f / KBSize; } }

        /// <summary>
        /// 获取转换为MB后的值
        /// </summary>
        public float TotalMBs { get { return bytes * 1f / MBSize; } }

        /// <summary>
        /// 获取转换为GB后的值
        /// </summary>
        public float TotalGBs { get { return bytes * 1f / GBSize; } }

        /// <summary>
        /// 获取转换为TB后的值
        /// </summary>
        public float TotalTBs { get { return bytes * 1f / TBSize; } }

        /// <summary>
        /// 存储单元
        /// </summary>
        /// <param name="bytes"></param>
        public StorageUnit(long bytes)
        {
            this.bytes = bytes;
        }

        public static StorageUnit GetKB(float kb) => new StorageUnit((long)(kb * KBSize));
        public static StorageUnit GetMB(float mb) => new StorageUnit((long)(mb * MBSize));
        public static StorageUnit GetGB(float gb) => new StorageUnit((long)(gb * GBSize));
        public static StorageUnit GetTB(float tb) => new StorageUnit((long)(tb * TBSize));

        /// <summary>
        /// ToString
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ToString(string format)
        {
            return base.ToString();
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storage1"></param>
        /// <param name="storage2"></param>
        /// <returns></returns>
        public static StorageUnit operator +(StorageUnit storage1, StorageUnit storage2) => new StorageUnit(storage1.bytes + storage2.bytes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storage1"></param>
        /// <param name="storage2"></param>
        /// <returns></returns>
        public static StorageUnit operator -(StorageUnit storage1, StorageUnit storage2) => new StorageUnit(storage1.bytes - storage2.bytes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storage1"></param>
        /// <param name="storage2"></param>
        /// <returns></returns>
        public static bool operator ==(StorageUnit storage1, StorageUnit storage2) => storage1.bytes == storage2.bytes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storage1"></param>
        /// <param name="storage2"></param>
        /// <returns></returns>
        public static bool operator !=(StorageUnit storage1, StorageUnit storage2) => storage1.bytes != storage2.bytes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storage1"></param>
        /// <param name="storage2"></param>
        /// <returns></returns>
        public static bool operator <(StorageUnit storage1, StorageUnit storage2) => storage1.bytes < storage2.bytes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storage1"></param>
        /// <param name="storage2"></param>
        /// <returns></returns>
        public static bool operator >(StorageUnit storage1, StorageUnit storage2) => storage1.bytes > storage2.bytes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storage1"></param>
        /// <param name="storage2"></param>
        /// <returns></returns>
        public static bool operator <=(StorageUnit storage1, StorageUnit storage2) => storage1.bytes <= storage2.bytes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storage1"></param>
        /// <param name="storage2"></param>
        /// <returns></returns>
        public static bool operator >=(StorageUnit storage1, StorageUnit storage2) => storage1.bytes >= storage2.bytes;
    }
}
