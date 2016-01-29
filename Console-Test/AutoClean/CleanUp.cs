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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Console_Test.AutoClean.Files;

namespace Console_Test.AutoClean
{
    /// <summary>
    /// This Class Cleans Up the Elmah Error XML files stored in the XML Store Directory /App_Data/errors/xmlstore
    /// Certain Situations triggers a clean up.
    /// </summary>
    /// <remarks>
    /// 1. XML Error File is a Duplicate by Error or User.
    /// 2. Delete Files by Date the Error File was made.
    /// 3. The Store Directory is getting full and needs to be clear to free up resources.
    ///
    /// Approach made here is to "Clean as you go" so that a massive batch of deleting files is not needed.
    /// This is made through the DeleteDuplicates and DeleteToSaveSpace functions.
    /// </remarks>
    class CleanUp
    {

        /// <summary>
        /// Path to the XML Store
        /// </summary>
        const string LogPath = (@"P:/Projects/Elmah/Elmah-AutoClean/Elmah-AutoClean/App_Data/errors/xmlstore/");
        static DirectoryInfo dInfo = new DirectoryInfo(LogPath);
        

        /// <summary>
        /// Used for Testing and Debuging instead of putting it in the Main Program 
        /// </summary>
        public static void Test()
        {
            DeleteByDate(GetOldestFile(LogPath));
        }




        /// <summary>
        /// 
        /// </summary>
        public static int DeleteToSaveSpace()
        {
            DirectoryInfo dInfo = new DirectoryInfo(LogPath);

               int numFiles = Files.numFilesinDirectory(dInfo);
            decimal dirSize = Files.directorySize(dInfo);


            //Target--> 10MB  200 Files        Dir Size 5 through 15MB ||  numFiles 195 through 205         
            if ( (dirSize > 4) & (dirSize <= 15) || (numFiles >= 195) & (numFiles <= 205) )
            {
                DeleteByDate(700);
                Console.WriteLine("Deleting 700 Days");
                return Files.numFilesinDirectory(dInfo);
            }

           
            else

            {
                //HighVolume(dirSize, numFiles);
                //ToDo: Need to check how many files are remaining.

                return Files.numFilesinDirectory(dInfo);
            }

            

        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="days"></param>
        public static void DeleteByDate(int days)
        {
            Files.DeleteOldFiles(LogPath, days);
        }

        

    }
}
