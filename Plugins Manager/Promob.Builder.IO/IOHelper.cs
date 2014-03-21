using System.IO;
using System.Threading;

namespace Promob.Builder.IO
{
    public static class IOHelper
    {
        #region Static Methods

        private static void MoveFiles(DirectoryInfo source, bool overrideFiles, string destDirectory)
        {
            foreach (FileInfo file in source.GetFiles())
            {
                string destFile = Path.Combine(destDirectory, file.Name);

                if (File.Exists(destFile) && overrideFiles)
                    File.Delete(destFile);
                else if (File.Exists(destFile))
                    continue;
                try
                {
                    File.Move(file.FullName, destFile);
                }
                catch (IOException ioex)
                {
                    throw ioex;
                }
            }
        }

        public static void MoveDirectoryContent(string source, string dest)
        {
            if (!Directory.Exists(dest))
                Directory.CreateDirectory(dest);

            DirectoryInfo sourceDirInfo = new DirectoryInfo(source);

            MoveFiles(sourceDirInfo, true, dest);

            foreach (DirectoryInfo dirInfo in sourceDirInfo.GetDirectories())
                MoveDirectory(dirInfo, new DirectoryInfo(dest), true);
        }

        public static void MoveDirectory(string source, string dest)
        {
            if (!Directory.Exists(dest))
                Directory.CreateDirectory(dest);

            MoveDirectory(new DirectoryInfo(source), new DirectoryInfo(dest), true);
        }

        private static void MoveDirectory(DirectoryInfo source, DirectoryInfo dest, bool overrideFiles)
        {
            string destDirectory = Path.Combine(dest.FullName, source.Name);

            if (!Directory.Exists(destDirectory))
                Directory.CreateDirectory(destDirectory);

            MoveFiles(source, overrideFiles, destDirectory);

            foreach (DirectoryInfo dir in source.GetDirectories())
                MoveDirectory(dir, new DirectoryInfo(destDirectory), overrideFiles);

            try
            {
                source.Delete();
            }
            catch (IOException ioex)
            {
                throw ioex;
            }
        }

        public static void DeleteDirectory(string directory)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(directory);

            DeleteDirectoryFiles(dirInfo);
        }

        private static void DeleteDirectoryFiles(DirectoryInfo dirInfo)
        {
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                DeleteDirectoryFiles(dir);

            foreach (FileInfo fileInfo in dirInfo.GetFiles())
            {
                fileInfo.IsReadOnly = false;

                try
                {
                    fileInfo.Delete();
                }
                catch (IOException ioex)
                {
                    throw ioex;
                }
            }

            if (dirInfo.Exists)
            {
                try
                {
                    dirInfo.Delete();
                }
                catch (IOException ioex)
                {
                    // as vezes o windows demora pra liberar o handle do arquivo , o que ocasiona essa exception.
                    // entao esperamos um pouco e tentamos excluir novamente.
                    Thread.Sleep(10);
                    dirInfo.Delete();
                }
            }
        }

        #endregion
    }
}
