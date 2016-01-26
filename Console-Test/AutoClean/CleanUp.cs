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


        /// <summary>
        /// 
        /// </summary>
        public void DeleteDuplicates()
        {
            deleteDuplicateError();
            deleteDuplicateUserError();

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

            // Target--> 20MB   400 Files     Dir Size 5.01 through 14.99MB ||  numFiles 196 through 204 
            else if ((dirSize > 15) & (dirSize < 25) || (numFiles > 395) & (numFiles < 405))
            {
                DeleteByDate(40);
                return Files.numFilesinDirectory(dInfo);
            }

            // Target--> 30MB   600 Files        Dir Size 5.01 through 14.99MB ||  numFiles 196 through 204 
            else if ((dirSize > 25) & (dirSize < 35) || (numFiles > 500) & (numFiles < 605))
            {
                DeleteByDate(30);
                Console.WriteLine("Deleting 30 Days");
                return Files.numFilesinDirectory(dInfo);
            }

            // Target--> 40MB  800 Files        Dir Size 5.01 through 14.99MB ||  numFiles 196 through 204 
            else if ((dirSize > 35) & (dirSize < 45) || (numFiles > 605) & (numFiles < 805))
            {
                DeleteByDate(20);
                return Files.numFilesinDirectory(dInfo);
            }

            // Target--> 50MB  1000 Files    Dir Size 5.01 through 14.99MB ||  numFiles 196 through 204 
            else if ((dirSize > 45) & (dirSize < 55) || (numFiles > 805) & (numFiles < 1005))
            {
                DeleteByDate(10);
                return Files.numFilesinDirectory(dInfo);
            }



            //Higher than Normal Error Files, Should not go here often.

            else

            {
                //HighVolume(dirSize, numFiles);
                //ToDo: Need to check how many files are remaining.

                return Files.numFilesinDirectory(dInfo);
            }

            

        }


        public static void HighVolume(decimal dirSize, int numFiles)
        {

            //Target--> 10MB  200 Files        Dir Size 5 through 15MB ||  numFiles 195 through 205         
            if ((dirSize >= 5) & (dirSize <= 15) || (numFiles >= 195) & (numFiles <= 205))
            {
                DeleteByDate(8);
            }

            // Target--> 20MB   400 Files     Dir Size 5.01 through 14.99MB ||  numFiles 196 through 204 
            else if ((dirSize > 15) & (dirSize < 25) || (numFiles > 395) & (numFiles < 405))
            {
                DeleteByDate(6);
            }

            // Target--> 30MB   600 Files        Dir Size 5.01 through 14.99MB ||  numFiles 196 through 204 
            else if ((dirSize > 25) & (dirSize < 35) || (numFiles > 595) & (numFiles < 605))
            {
                DeleteByDate(4);
            }

            // Target--> 40MB  800 Files        Dir Size 5.01 through 14.99MB ||  numFiles 196 through 204 
            else if ((dirSize > 35) & (dirSize < 45) || (numFiles > 795) & (numFiles < 805))
            {
                DeleteByDate(3);
            }

            // Target--> 50MB  1000 Files    Dir Size 5.01 through 14.99MB ||  numFiles 196 through 204 
            else if ((dirSize > 45) & (dirSize < 55) || (numFiles > 995) & (numFiles < 1005))
            {
                DeleteByDate(2);
            }

            else
            {
                //ToDo: Do anything? Just Delete Files without a date or break down by hour, min? 
            }

        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="days"></param>
        public static void DeleteByDate(uint days)
        {
            Files.DeleteOldFiles(LogPath, days);
        }


       


        /// <summary>
        /// Errors that occur that are duplicates within a very close timeframe.
        /// </summary>
        public void DeleteRepeatedError()
        {
            archiveError();
        }





        /// <summary>
        /// Errors that are the same made from different User/User-Agent 
        /// </summary>
        /// <example>
        /// User A, User B, and GoogleBot visits SomePage.cshtml and it generates the exact same error for all Visits that were made.
        /// </example>
        public void deleteDuplicateError()
        {
            archiveError();
        }


        /// <summary>
        /// Errors that are the same and made by the same User/User-Agent
        /// </summary>
        /// <example>
        /// A user keeps visiting the same page or hitting reload generating the same error for the same user repeately.
        /// This can be in multiple session or different visits. 
        /// </example>
        public void deleteDuplicateUserError()
        {
            archiveUser();
        }



        //ToDo: Define "Exact" same Error

        /// <summary>
        /// Takes a duplicate error and record the differences between the error and gets rid of the duplicate information. 
        /// </summary>
        /// <example>
        /// Multiple Users visit Page-2.cshtml it generates the "exact" same error.
        /// Unique information that is recorded would be the Number of times the error happen, and the date time of those errors.
        /// </example>
        /// <returns>
        /// 1. New xml file with the File name of the GuidID ("errorId") of the Error to be Archived saved in the /Archive folder.
        /// 2. Updates an already existing Error with updated information on the new instances of the same error 
        ///    (Update Error Count, adds another Date-Time to the file <datetime>DateTime new error</datetime>).
        /// </returns>
        public void archiveError()
        {

        }

        /// <summary>
        /// Takes duplicate information on a User away and only record unique information about a user. 
        /// </summary>
        /// <example>
        /// User A Vists Page-1.cshtml 5 times and each time generates the "exact" same error.  
        /// Unique information on user would be User-Agent,Cookie, Number of times same error was generated by user, date time of errors, 
        /// and the ErrorID of the (first | latest)? error. 
        /// </example>
        public void archiveUser()
        {

        }



    }
}
