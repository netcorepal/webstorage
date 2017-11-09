using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace NetCorePal.WebStorage
{
    /// <summary>
    /// 提供程序扩展
    /// </summary>
    public static class WebStorageProviderExtentions
    {
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="provider">提供程序实例</param>
        /// <param name="filePath">文件路径,例如/abc/efg/myfile.txt</param>
        /// <param name="bytes">文件字节</param>
        /// <returns>返回保存后的文件路径</returns>
        public static string Save(this WebStorageProvider provider, string filePath, byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                return provider.Save(filePath, stream);
            }
        }
        /// <summary>
        /// 根据文件路径获取文件，如果文件不存在则抛出异常
        /// </summary>
        /// <param name="provider">提供程序实例</param>
        /// <param name="filePath">文件路径,例如/abc/efg/myfile.txt</param>
        /// <returns></returns>
        public static byte[] GetFileAllBytes(this WebStorageProvider provider, string filePath)
        {
            using (var stream = provider.GetFile(filePath))
            {
                List<byte> data = new List<byte>();
                var buffer = new byte[3];
                int readCount = 0;

                do
                {
                    readCount = stream.Read(buffer, 0, buffer.Length);
                    if (readCount > 0)
                    {
                        if (readCount < buffer.Length)
                        {
                            data.AddRange(buffer.Take(readCount));
                            readCount = 0;
                        }
                        else
                        {
                            data.AddRange(buffer);
                        }
                    }
                } while (readCount > 0);



                return data.ToArray();
            }
        }
    }
}
