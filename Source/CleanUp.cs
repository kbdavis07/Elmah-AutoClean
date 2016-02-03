#region License, Terms and Author(s)
//
// ELMAH - Error Logging Modules and Handlers Auto Clean for ASP.NET
// Copyright (c) 2016 Brian Keith Davis. All rights reserved.
//
//  Author(s):
//
//      Brian Keith Davis, http://Kbdavis07.com
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
using static AutoClean.Files;
using System.Web;
using Elmah;

namespace AutoClean
{
    /// <summary>
    /// Cleans Up files stored in a Directory like /App_Data/errors/xmlstore
    /// </summary>
    /// <remarks>
    /// Approach made here is to "Clean as you go" so that a massive batch of deleting files is not needed for a directory at one time.
    /// 
    /// </remarks>
    public class CleanUp
    {


        //ToDo: Need to have LogPath Method to handle if Directory is not found.

        //ToDo: Create extension method for settings? 

        /// <summary>
        /// Starts the AutoClean function with the Default Path of /App_Data/errors/xmlstore/ .
        /// If directory is not found it is created at: /App_Data/errors/xmlstore/
        /// If you need to have the directory in a different location just provide the path in the method call.
        /// AutoClean("/Path/To/YourDirectory/ThatNeedsTobeCleaned/");
        /// </summary>
        public static void AutoClean()
        {

            System.Threading.Thread.Sleep(5);

            try
            {
                string _LogPath = HttpContext.Current.Server.MapPath("~/App_Data/errors/xmlstore/");

                if (!Directory.Exists(_LogPath))
                {
                    Directory.CreateDirectory(_LogPath);
                }

                DeleteToSaveSpace(_LogPath);
                
            }
            catch (Exception)
            {
                //Todo: Do something if error happens.
                throw;
            }

        }

        /// <summary>
        /// Need to provide a path to a directory that needs to be cleaned.
        /// Like: <c> AutoClean("/Path/To/YourDirectory/ThatNeedsTobeCleaned/");</c>
        /// </summary>
        /// <param name="_LogPath"> <c>"/App_Data/Errors"</c></param>
        public static void AutoClean(string _LogPath)
        {
            System.Threading.Thread.Sleep(5);

            try
            {
                if (_LogPath != null)
                {
                    string LogPath = HttpContext.Current.Server.MapPath("~" + _LogPath);

                    if (!Directory.Exists(LogPath))
                    {
                        Directory.CreateDirectory(LogPath);
                    }

                    DeleteToSaveSpace(LogPath);
                }

            }
            catch (Exception)
            {
                //Todo: Do something if error happens.
                throw;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="LogPath"></param>
        public static void DeleteToSaveSpace(string LogPath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(LogPath);

            int OldestDate = GetOldestFile(LogPath);
              int numFiles = Files.numFilesinDirectory(LogPath);
           decimal dirSize = Files.directorySize(dInfo);


            // Older than 31 days, More than 1,000 files, or has 10MB in folder 
            if ( (OldestDate > 30) || (numFiles > 999) || dirSize > 10 )
            {
                while ( (numFiles > 999 || OldestDate > 30) || dirSize > 10)
                {
                    DeleteByDate(LogPath,OldestDate);

                    numFiles = Files.numFilesinDirectory(LogPath);
                    OldestDate = GetOldestFile(LogPath);
                    dirSize = Files.directorySize(dInfo);

                    //Console.WriteLine(String.Format("Round 1: | NumFiles:  {0,-10} | OldestDate:  {1,5} | DirSize {2,10} |", numFiles, OldestDate, dirSize));
                   
                    System.Threading.Thread.Sleep(5);
                }
             
            }

            // Older than 11 through 30 days, have 200 through 999 files
            if (Enumerable.Range(11, 30).Contains(OldestDate) || (Enumerable.Range(200, 999).Contains(numFiles)))
            {
                while (Enumerable.Range(11, 30).Contains(OldestDate) || (Enumerable.Range(200, 999).Contains(numFiles)))
                {
                    DeleteByDate(LogPath,OldestDate);

                    numFiles = Files.numFilesinDirectory(LogPath);
                    OldestDate = GetOldestFile(LogPath);
                    dirSize = Files.directorySize(dInfo);

                    System.Threading.Thread.Sleep(5);
                }

            }

            // Keep files betwen 5 to 10 days under 100 files.
            if (Enumerable.Range(5, 10).Contains(OldestDate) & numFiles > 100)
            {
                while (Enumerable.Range(5, 10).Contains(OldestDate) & numFiles > 100)
                {
                    DeleteByDate(LogPath,OldestDate);

                    numFiles = Files.numFilesinDirectory(LogPath);
                    OldestDate = GetOldestFile(LogPath);
                    dirSize = Files.directorySize(dInfo);

                    System.Threading.Thread.Sleep(5);

                }

            }


            else

            {
               //ToDo: Need to do anything?
            }

            
        }
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="days"></param>
        public static void DeleteByDate(string LogPath,int days)
        {
            Files.DeleteOldFiles(LogPath, days);
        }

    }
}
