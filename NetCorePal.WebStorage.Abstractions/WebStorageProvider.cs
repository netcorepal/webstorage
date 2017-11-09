using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace NetCorePal.WebStorage
{
    /// <summary>
    /// 文件存储提供程序
    /// </summary>
    public abstract class WebStorageProvider
    {
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="filePath">文件路径,例如/abc/efg/myfile.txt</param>
        /// <param name="stream">文件流</param>
        /// <returns>返回保存后的文件路径</returns>
        public abstract string Save(string filePath, Stream stream);
        /// <summary>
        /// 根据文件路径获取文件流，如果文件不存在则抛出异常
        /// </summary>
        /// <param name="filePath">文件路径,例如/abc/efg/myfile.txt</param>
        /// <returns>返回文件流</returns>
        public abstract Stream GetFile(string filePath);
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件路径,例如/abc/efg/myfile.txt</param>
        /// <returns>返回是否删除成功，如果文件不存在，则返回false</returns>
        public abstract bool DeleteFile(string filePath);
    }
}
