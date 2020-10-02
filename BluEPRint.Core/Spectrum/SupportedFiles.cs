using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BluEPRint.Core.Spectrum
{
    public static class SupportedFiles
    {
        /// <summary>
        /// Returns additional needed Files
        /// </summary>
        private static IEnumerable<string[]> SupportedFileTypes
        {
            get
            {
                //Bruker EPR
                yield return new[] {".par", ".spc"};

                //Bruker NMR
                yield return new[] {"acqus", "fid"};

                yield return new[] {".DSW"};
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

            var needed =SupportedFileTypes.FirstOrDefault(s => s.Contains(ext));
            if (needed == null) yield break;
            foreach (var ex in needed)
                yield return Path.GetDirectoryName(file) + @"\" + (ext.Contains(".") ? Path.GetFileNameWithoutExtension(file) : "") + ex;
        }

        public static string SpectrumType(string file)
        {
            if (new[] { "fid", "acqus" }.Contains(Path.GetFileName(file))) return "NMR";
            if (new[] { ".par", ".spc" }.Contains(Path.GetExtension(file))) return "EPR";
            if (new[] { ".DSW" }.Contains(Path.GetExtension(file))) return "UVVis";
            throw new NotSupportedException("File Type is not supported");
        }
    }
}
