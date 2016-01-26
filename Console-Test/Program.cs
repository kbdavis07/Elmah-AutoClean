using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Console_Test.AutoClean;
using static Console_Test.AutoClean.Files;
using System.IO;

namespace Console_Test
{
    class Program
    {
        
        static void Main(string[] args)
        {

            //CleanUp.DeleteByDate(1);

            //System.Threading.Thread.Sleep(300);

            //Console.WriteLine("Sleeping");

            DirectoryInfo dInfo = new DirectoryInfo(@"P:/Projects/Elmah/Elmah-AutoClean/Elmah-AutoClean/App_Data/errors/xmlstore/");

            string LogPath = (@"P:/Projects/Elmah/Elmah-AutoClean/Elmah-AutoClean/App_Data/errors/xmlstore/");

            //int BeforeFiles = Files.numFilesinDirectory(dInfo);
            //decimal BeforeSize = Files.directorySize(dInfo);

            //Console.WriteLine("*******************Before Delete******************************");
            //Console.WriteLine("There are: " + BeforeFiles + " files in the XML Store");

            //Console.WriteLine("The size of the XML Store is: " + BeforeSize + " MB" );
            //Console.WriteLine("");
            //Console.WriteLine("*************************************************");




            //Console.WriteLine("*************************************************");
            //Console.WriteLine("");

            //Console.WriteLine("After Clean up there are: " + CleanUp.DeleteToSaveSpace() + " files in the XML Store");

            //Console.WriteLine("The size of the XML Store is now: " + Files.directorySize(dInfo) + " MB");

            //Console.WriteLine("There was: " + (BeforeFiles - Files.numFilesinDirectory(dInfo) + "Files Deleted") );

            //Console.WriteLine("*************************************************");

            Console.WriteLine("The oldest File is: " + GetOldestFile(LogPath) );


        }


        //End of Program
    }
}