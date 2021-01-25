using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job4
{
    class DirectoryMethod
    {
        public void DeleteAndCreate(string path)
        {
            if (Directory.Exists(path)) //創立-刪除 Feature-name-sample資料夾
                Directory.Delete(path, true);
            Directory.CreateDirectory(path);
        }
    }
}
