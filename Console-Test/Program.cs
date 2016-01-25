using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Console_Test
{
    class Program
    {

        const string _FileToLoad = ("P:/Projects/Elmah/Elmah-AutoClean/Elmah-AutoClean/App_Data/errors/xmlstore/error-2016-01-24131405Z-64f5fa41-20a5-4d28-b5e8-1b61fae3982a.xml");

       
        static void Main(string[] args)
        {

            Console.WriteLine("*********************************************************");

            xmlExample2();

            Console.WriteLine("*********************************************************");

            
        }

        



        static void QueryDuplicates()
        {
            // Change the root drive or folder if necessary
            string startFolder = @"P:\Projects\Elmah\Elmah-AutoClean\Elmah-AutoClean\App_Data\errors\xmlstore\";

            // Take a snapshot of the file system.
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(startFolder);

            // This method assumes that the application has discovery permissions
            // for all folders under the specified path.
            IEnumerable<System.IO.FileInfo> fileList = dir.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            
        }


     

        static void xmlExample2()
        {

            string file = _FileToLoad;



            ErrorMessage m = new ErrorMessage();

            // Create an XML reader for this file.
            using (XmlReader reader = XmlReader.Create(file))
            {
                while (reader.Read())
                {
                    m.Host = reader["host"];
                    m.Type = reader["type"];
                    m.StatusCode = reader["statusCode"];
                    m.Detail = reader["detail"];

                        // Only detect start elements.
                        if (reader.IsStartElement())
                        {
                            // Get element name and switch on it.
                            switch (reader.Name)
                            {
                                case "item":
                                    // Detect this element.
                                    Console.WriteLine("Start <item> element.");
                                
                                break;

                                 case "article":
                                    // Detect this article element.
                                    Console.WriteLine("Start <article> element.");


                                    // Search for the attribute name on this current node.
                                    string attribute = reader["name"];

                                     

                                if (attribute != null)
                                    {
                                        Console.WriteLine("  Has attribute name: " + attribute);
                                    m.USER_AGENT = reader["HTTP_USER_AGENT"];

                                }
                                    // Next read will contain text.
                                    if (reader.Read())
                                    {
                                        Console.WriteLine("  Text node: " + reader.Value.Trim());
                                    }
                                    break;
                            }
                        }
                   
                }




                
                    m.Cookie = reader["HTTP_COOKIE"];


                    Console.WriteLine(m.USER_AGENT);   
                     
                  }
                }
              }
           
        






        //End of Program
    }
}