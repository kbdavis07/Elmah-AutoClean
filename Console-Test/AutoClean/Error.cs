using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Test.AutoClean
{
    /// <summary>
    /// Model for Error
    /// </summary>
    public class Error
    {
        public string ID { get; set; }
        public string FileName { get; set; }
        public Boolean Delete { get; set; }

        public string Host { get; set; }
        public string Type { get; set; }
        public string StatusCode { get; set; }
        public string Detail { get; set; }
        public string USER_AGENT { get; set; }
        public string Cookie { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>

        public Error() { }

        

        


        public class ErrorMessage
        {

            

        }





    }
}
