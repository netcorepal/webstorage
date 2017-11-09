using System;
using System.IO;
using System.Web;

namespace NetCorePal.WebStorage
{
    /// <summary>
    /// website local storage provider
    /// </summary>
    public class LocalWebStorageProvider : WebStorageProvider
    {
#if NETSTANDARD2_0
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public LocalWebStorageProvider(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.Env = env;
        }
        Microsoft.AspNetCore.Hosting.IHostingEnvironment Env;
        private string PathToFileName(string filePath)
        {
            return Path.Combine(Env.WebRootPath, filePath.TrimStart('/').Replace('/', '\\'));
        }
#else
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="server"></param>
        public LocalWebStorageProvider(HttpServerUtilityBase server)
        {
            this.Server = server;
        }
        HttpServerUtilityBase Server;
        private string PathToFileName(string filePath)
        {
            return Server.MapPath(filePath);
        }


#endif

        /// <summary>
        /// 根据文件路径获取文件流，如果文件不存在则抛出异常
        /// </summary>
        /// <param name="filePath">文件路径,例如/abc/efg/myfile.txt</param>
        /// <returns>返回文件流</returns>
        public override Stream GetFile(string filePath)
        {
            this.CheckFilePath(filePath);
            var fileName = this.PathToFileName(filePath);
            return new FileStream(fileName, FileMode.Open);
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="filePath">文件路径,例如/abc/efg/myfile.txt</param>
        /// <param name="stream">文件流</param>
        /// <returns>返回保存后的文件路径</returns>
        public override string Save(string filePath, Stream stream)
        {
            this.CheckFilePath(filePath);
            var fileName = this.PathToFileName(filePath);
            this.CheckFileName(fileName);
            byte[] buffer = new byte[stream.Length];
            if (stream.Length > Int32.MaxValue)
            {
                throw new ArgumentOutOfRangeException("strean", "暂不支持长度超过Int32.MaxValue的文件流"); //不支持超大文件
            }
            stream.Read(buffer, 0, (int)stream.Length);

            var dir = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.WriteAllBytes(fileName, buffer);
            return filePath;
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件路径,例如/abc/efg/myfile.txt</param>
        /// <returns>返回是否删除成功，如果文件不存在，则返回false</returns>
        public override bool DeleteFile(string filePath)
        {
            this.CheckFilePath(filePath);
            var fileName = this.PathToFileName(filePath);
            this.CheckFileName(fileName);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                return true;
            }
            return false;
        }


        private void CheckFilePath(string filePath)
        {
            if (!filePath.StartsWith(@"/"))
            {
                throw new ArgumentOutOfRangeException("filePath", "filePath 必须以“/”开头");
            }
        }

        private void CheckFileName(string fileName)
        {
            if (!Path.HasExtension(fileName))
            {
                throw new ArgumentOutOfRangeException("filePath", "filePath 不是有效的文件名");
            }
            if (string.IsNullOrEmpty(Path.GetFileNameWithoutExtension(fileName)))
            {
                throw new ArgumentOutOfRangeException("filePath", "filePath 不是有效的文件名");
            }
        }
    }
}
