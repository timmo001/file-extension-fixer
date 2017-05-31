using System;
using System.IO;

namespace FileExtensionFixer
{
    public class Main
    {

        /// <summary>
        /// Fix file extension
        /// </summary>
        /// <param name="filePath">Full Path to file</param>
        /// <param name="extension">extension with dot (.msg)</param>
        /// <returns></returns>
        public static string FixFileExtension(string filePath, string extension)
        {
            try
            {
                if (!File.Exists(filePath)) return "";

                if (filePath.Contains(extension.Substring(1)))
                {
                    string newFilePath = filePath.Replace(extension.Substring(1), extension);
                    return DoMove(filePath, newFilePath, extension);
                }
                else
                {
                    string newFilePath = filePath + extension;
                    return DoMove(filePath, newFilePath, extension);
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Calls MoveFile
        /// </summary>
        /// <param name="filePath">Full path to file</param>
        /// <param name="newFilePath">New full path</param>
        /// <param name="extension">extension with dot (.msg)</param>
        private static string DoMove(string filePath, string newFilePath, string extension)
        {
            bool fileFixed = false;
            int counter = 0;
            while (!fileFixed)
            {
                fileFixed = MoveFile(filePath, newFilePath);

                if (!fileFixed)
                {
                    counter++;
                    newFilePath = newFilePath.Substring(0, newFilePath.LastIndexOf("."))
                        + "_"
                        + counter.ToString()
                        + extension;
                }
                else { return newFilePath; }
            }
            return "";
        }

        /// <summary>
        /// Attempts to move. Returns false if failed
        /// </summary>
        /// <param name="fromFilePath">Full path to file</param>
        /// <param name="toFilePath">New full path</param>
        /// <returns></returns>
        private static bool MoveFile(string fromFilePath, string toFilePath)
        {
            try
            {
                File.Move(fromFilePath, toFilePath);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
