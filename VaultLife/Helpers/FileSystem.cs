using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;

namespace Vaultlife.Helpers
{ /// <summary>
    /// Filesystem
    /// </summary>
    public class FileSystem
    {
        // Copy directory structure recursively
        public static void copyDirectory(string Src, string Dst)
        {
            String[] Files;

            if (Dst[Dst.Length - 1] != Path.DirectorySeparatorChar)
                Dst += Path.DirectorySeparatorChar;
            if (!Directory.Exists(Dst)) Directory.CreateDirectory(Dst);
            Files = Directory.GetFileSystemEntries(Src);
            foreach (string Element in Files)
            {
                // Sub directories
                if (Directory.Exists(Element))
                    copyDirectory(Element, Dst + Path.GetFileName(Element));
                // Files in directory
                else
                    File.Copy(Element, Dst + Path.GetFileName(Element), true);
            }
        }

        public static void deleteDirectory(string Src)
        {
            String[] Files;

            Files = Directory.GetFileSystemEntries(Src);
            foreach (string Element in Files)
            {
                // Sub directories
                if (Directory.Exists(Element))
                {
                    deleteDirectory(Element);
                    Directory.Delete(Element);
                }
                // Files in directory
                else
                {
                    File.Delete(Element);
                }
            }
        }

        public static void deleteFiles(string Src, string FilePattern)
        {
            String[] Files;

            Files = Directory.GetFileSystemEntries(Src);
            foreach (string Element in Files)
            {
                // Sub directories
                if (FilePattern.Contains(Element))
                {
                    //if (File.Exists(Element))
                    {
                        File.Delete(Element);
                    }
                }
            }
        }

    }
}