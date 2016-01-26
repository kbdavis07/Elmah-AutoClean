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
            DirectoryInfo dInfo = new DirectoryInfo(@"P:/Projects/Elmah/Elmah-AutoClean/Elmah-AutoClean/App_Data/errors/xmlstore/");
            string LogPath = (@"P:/Projects/Elmah/Elmah-AutoClean/Elmah-AutoClean/App_Data/errors/xmlstore/");
            
            Console.WriteLine("Max Day Age: " + GetOldestFile(LogPath) );

            Console.WriteLine("There are:" + numFilesinDirectory(dInfo) + "Files in the XML Store");


        }


        //End of Program
    }
}