using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BluEPRint.Core
{
    public static class SupportedFiles
    {
        /// <summary>
        /// Returns additional needed Files
        /// </summary>
        public static IEnumerable<string[]> NeededFiles
        {
            get
            {
                //Bruker EPR
                yield return new[] {".par", ".spc"};

                //Bruker NMR
                yield return new[] {"acqus", "fid"};
            }
        }

        /// <summary>
        /// Get Files Array
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static IEnumerable<string> Files(string file)
        {
            var ext = Path.GetExtension(file);
            if (string.IsNullOrEmpty(ext)) ext = Path.GetFileName(file);

            var needed = NeededFiles.FirstOrDefault(s => s.Contains(ext));
            if (needed != null)
                foreach (var ex in needed)
                    yield return Path.GetDirectoryName(file) + @"\" + (ext.Contains(".") ? Path.GetFileNameWithoutExtension(file) : "") + ex;
            else yield return file;
        }
    }
}
