#region License, Terms and Author(s)
//
// Auto Clean for ASP.NET
// Copyright (c) 2016 Brian Keith Davis. All rights reserved.
//
//  Author(s):
//
//  Brian Keith Davis, http://Kbdavis07.com
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion



using System;
using System.IO;
using System.Linq;


namespace AutoClean
{

    /// <summary>
    /// Tools for operating with XML files for AutoClean Operations.
    /// </summary>
    class Files
    {

        #region Settings

            //For DeleteFileIfOlderThan() Method
            private const int RetriesOnError = 3;
            private const int DelayOnRetry = 1000;

        
        #endregion

        #region Delete 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="maximumAgeInDays"></param>
        /// <remarks>cref ="http://stackoverflow.com/questions/10295561/delete-files-older-than-a-date" </remarks>
        /// <call> Helpers.DeleteOldFiles(@"c:\mypath\", logAge); </call>
        public static void DeleteOldFiles(string folderPath, int maximumAgeInDays)
        {
            
            DateTime minimumDate = DateTime.Now.AddDays(-maximumAgeInDays);
            foreach (var path in Directory.EnumerateFiles(folderPath))
            {
                DeleteFileIfOlderThan(path, minimumDate);
            }

        }

       
        private static bool DeleteFileIfOlderThan(string path, DateTime date)
        {
            
            for (int i = 0; i < RetriesOnError; ++i)
            {
                try
                {
                    FileInfo file = new FileInfo(path);
                   
                    if ( (file.LastWriteTime < date) || (file.CreationTime < date) )
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
        /// <param name="logPath"></param>
        /// <returns>Total Size of Directory as a Decimal in MegaBytes (MB)</returns>
        public static decimal directorySize(DirectoryInfo logPath)
        {
            // Enumerate all the files
            long totalSize = logPath.EnumerateFiles().Sum(file => file.Length);
            double doubletotalSizeMB = ((double)totalSize) / (1024 * 1024);
            decimal totalSizeMB = ((decimal)doubletotalSizeMB);
            return (Math.Round(totalSizeMB,2));
        }

        /// <summary>
        /// Returns the Number of Files in a Directory
        /// </summary>
        /// <param name="logPath"></param>
        /// <returns></returns>
        public static int numFilesinDirectory(string logPath)
        {
            DirectoryInfo myDir = new DirectoryInfo (logPath);
            int count = myDir.GetFiles().Length;
            return count;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public static int GetOldestFile(string directory)
        {
            if (!Directory.Exists(directory))
            {
                throw new ArgumentException(directory + "Directory does not exist");
            }

            DirectoryInfo parent = new DirectoryInfo(directory);
            FileInfo[] children = parent.GetFiles();
            
            if (children.Length == 0) return 000000;

            int OldestDateinDays = ( (DateTime.Now - children[0].LastWriteTime).Days);
            
            return OldestDateinDays;
        }

        
        #endregion




    }
}
