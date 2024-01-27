using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Infinity.Bnois.Api.Core
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path)
        { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            int count = 1;
            string fileName = headers.ContentDisposition.FileName.TrimEnd('"').TrimStart('"');
            string fileNameOnly = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);

            string fullPath = base.RootPath + "\\" + fileName;
            string newFullPath = fullPath;

            string localFileName = fileName;
            while (File.Exists(newFullPath))
            {
                localFileName = string.Format("{0}({1}){2}", fileNameOnly, count++, extension);
                newFullPath = Path.Combine(base.RootPath, localFileName);
            }

            return localFileName;
        }
    }
}