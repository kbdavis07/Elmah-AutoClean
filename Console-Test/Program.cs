using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Console_Test.AutoClean;
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

            
            Console.WriteLine((Files.DirectorySize(dInfo)));

        }

        
        //End of Program
    }
}