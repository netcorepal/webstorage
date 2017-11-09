using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aliyun.OSS.Common;
namespace NetCorePal.WebStorage
{
    /// <summary>
    /// 阿里云OSS WebStorage Provider 
    /// </summary>
    public class AliyunOSSWebStorageProvider : WebStorageProvider
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="endpoint">阿里云OSS终结点</param>
        /// <param name="bucketName">bucketName</param>
        /// <param name="accessKeyId">accessKeyId</param>
        /// <param name="accessKeySecret">accessKeySecret</param>
        public AliyunOSSWebStorageProvider(string endpoint, string bucketName, string accessKeyId, string accessKeySecret)
        {
            this.Endpoint = endpoint;
            this.BucketName = bucketName;
            this.AccessKeyId = accessKeyId;
            this.AccessKeySecret = accessKeySecret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint">阿里云OSS终结点</param>
        /// <param name="bucketName">bucketName</param>
        /// <param name="accessKeyId">accessKeyId</param>
        /// <param name="accessKeySecret">accessKeySecret</param>
        /// <param name="connectionTimeout">超时时间，毫秒</param>
        public AliyunOSSWebStorageProvider(string endpoint, string bucketName, string accessKeyId, string accessKeySecret, int connectionTimeout)
        {
            this.clientConfig = new ClientConfiguration()
            {
                ConnectionTimeout = connectionTimeout
            };
        }

        private ClientConfiguration clientConfig = null; //

        /// <summary>
        /// 阿里云OSS终结点
        /// </summary>
        public string Endpoint { get; private set; }
        /// <summary>
        /// AccessKeyId
        /// </summary>
        public string AccessKeyId { get; private set; }
        /// <summary>
        /// AccessKeySecret
        /// </summary>
        public string AccessKeySecret { get; private set; }
        /// <summary>
        /// BucketName
        /// </summary>
        public string BucketName { get; private set; }
        /// <summary>
        /// 根据文件路径获取文件流，如果文件不存在则抛出异常
        /// </summary>
        /// <param name="filePath">文件路径,例如/abc/efg/myfile.txt</param>
        /// <returns>返回文件流</returns>
        public override Stream GetFile(string filePath)
        {
            var client = GetOssClient();
            var key = this.FomatFilePath(filePath);

            if (client.DoesObjectExist(this.BucketName, key))
            {
                var obj = client.GetObject(this.BucketName, key);
                return obj.ResponseStream;
            }
            throw new Exception("文件不存在");
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="filePath">文件路径,例如/abc/efg/myfile.txt</param>
        /// <param name="stream">文件流</param>
        /// <returns>返回保存后的文件路径</returns>
        public override string Save(string filePath, Stream stream)
        {
            var key = this.FomatFilePath(filePath);
            var client = this.GetOssClient();
            var r = client.PutObject(this.BucketName, key, stream);
            return filePath;
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件路径,例如/abc/efg/myfile.txt</param>
        /// <returns>返回是否删除成功，如果文件不存在，则返回false</returns>
        public override bool DeleteFile(string filePath)
        {
            var key = this.FomatFilePath(filePath);
            var client = this.GetOssClient();
            if (client.DoesObjectExist(this.BucketName, key))
            {
                client.DeleteObject(this.BucketName, key);
                return true;
            }
            return false;
        }


        private string FomatFilePath(string filePath)
        {
            return filePath.TrimStart('/');
        }


        private OssClient GetOssClient()
        {
            var client = this.clientConfig == null ? new OssClient(this.Endpoint, this.AccessKeyId, this.AccessKeySecret) : new OssClient(this.Endpoint, this.AccessKeyId, this.AccessKeySecret, this.clientConfig);
            return client;
        }
    }
}
