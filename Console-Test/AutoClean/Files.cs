using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Console_Test.AutoClean
{
    class Files
    {
        /// <summary>
        /// Path to the XML Store
        /// </summary>
        const string LogPath = ("P:/Projects/Elmah/Elmah-AutoClean/Elmah-AutoClean/App_Data/errors/xmlstore/");
        DirectoryInfo dInfo = new DirectoryInfo(LogPath);



        public String[] GetErrorFiles()
        {
           
            /* Get all files in directory */
            string logPath = LogPath;
            DirectoryInfo dir = new DirectoryInfo(logPath);

            if (!dir.Exists)
            {
                //Todo: Create Directory Path? or Throw an Error?
            }
                
            /* Gets only Elmah Error Files in XML Store */
            FileSystemInfo[] infos = dir.GetFiles("error-*.xml");


            if (infos.Length < 1)
            {
                //XML Store does not have any Errors
                Console.WriteLine("XML Store does not have any Errors");

                return null;
                
            }


            string[] files = new string[infos.Length];
            int count = 0;

            Console.WriteLine("There are" + infos.Length + "Files");


            /* Get files that are not marked with system and hidden attributes */
            foreach (FileSystemInfo info in infos)
            {
                if (IsUserFile(info.Attributes))

                    files[count++] = Path.Combine(logPath, info.Name);
            }

            return files;
            
        }



    #region Delete 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="maximumAgeInDays"></param>
        /// <param name="filesToExclude"></param>
        /// <remarks>cref ="http://stackoverflow.com/questions/10295561/delete-files-older-than-a-date" </remarks>
        /// <call> Helpers.DeleteOldFiles(@"c:\mypath\", logAge, currentLog); </call>
       public static void DeleteOldFiles(string folderPath, uint maximumAgeInDays)
        {
            DateTime minimumDate = DateTime.Now.AddDays(-maximumAgeInDays);
            foreach (var path in Directory.EnumerateFiles(folderPath))
            {
                DeleteFileIfOlderThan(path, minimumDate);
            }

        }

        private const int RetriesOnError = 3;
        private const int DelayOnRetry = 1000;

       

        private static bool DeleteFileIfOlderThan(string path, DateTime date)
        {
            for (int i = 0; i < RetriesOnError; ++i)
            {
                try
                {
                    FileInfo file = new FileInfo(path);
                   
                    if ( (file.CreationTime < date) | (file.LastWriteTime < date) )
                    {
                        file.Delete();
                    }

                    return true;
                }
                catch (IOException)
                {
                    System.Threading.Thread.Sleep(DelayOnRetry);
                }
                catch (UnauthorizedAccessException)
                {
                    System.Threading.Thread.Sleep(DelayOnRetry);
                }
            }

            return false;
        }

        #endregion


        #region File Tools

        /// <summary>
        /// Gets the Total Size in MB of a Directory.
        /// </summary>
        /// <param name="LogPath"></param>
        /// <returns>Total Size of Directory as a Decimal in MegaBytes (MB)</returns>
        public static decimal directorySize(DirectoryInfo LogPath)
        {
            // Enumerate all the files
            long totalSize = LogPath.EnumerateFiles().Sum(file => file.Length);
            double doubletotalSizeMB = ((double)totalSize) / (1024 * 1024);
            decimal totalSizeMB = ((decimal)doubletotalSizeMB);
            return (Math.Round(totalSizeMB,2));
        }

        /// <summary>
        /// Returns the Number of Files in a Directory
        /// </summary>
        /// <param name="dInfo"></param>
        /// <returns></returns>
        public static int numFilesinDirectory(DirectoryInfo dInfo)
        {
            DirectoryInfo myDir = dInfo;
            int count = myDir.GetFiles().Length;
            return count;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public static DateTime GetOldestFile(string directory)
        {
            if (!Directory.Exists(directory))
                throw new ArgumentException();

            DirectoryInfo parent = new DirectoryInfo(directory);
            FileInfo[] children = parent.GetFiles();

            if (children.Length == 0) return new DateTime(0,0,0);

            FileInfo oldest = children[0];
            foreach (var child in children.Skip(1))
            {
                if ( (child.CreationTime < oldest.CreationTime) | (child.LastWriteTime < oldest.LastWriteTime) )
                    oldest = child;
            }


            if (oldest.LastWriteTime > oldest.CreationTime)
            {
                return oldest.LastWriteTime;
            }

            else
            {
                return oldest.CreationTime;
            }
            
        }



        /// <summary>
        /// Checks if file is a User File.
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        private static bool IsUserFile(FileAttributes attributes)
        {
            return 0 == (attributes & (FileAttributes.Directory | FileAttributes.Hidden | FileAttributes.System));
        }

        #endregion


    }
}
