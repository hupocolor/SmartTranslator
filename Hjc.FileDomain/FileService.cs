using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjc.FileDomain
{
    internal class FileService : IFileService
    {
        public async Task<string> GetByFileStringAsync(Stream fileStream)
        {
            using StreamReader sr = new StreamReader(fileStream);
            string res = await sr.ReadToEndAsync();
            
            return res;
        }
    }
}
