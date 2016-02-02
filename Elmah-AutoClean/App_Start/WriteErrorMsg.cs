using Elmah;
using Elmah.Bootstrapper;
using System;
using System.Web;

namespace Elmah_AutoClean
{
    public static class ErrorLog
    {

        public static void Init()
        { 
            //App.OnModuleEvent(
            //    (m, h) => m.Logged += h,
            //    (m, h) => m.Logged -= h,
            //    h => new ErrorLoggedEventHandler((sender, args) => h(sender, args)),
            //    (Elmah.ErrorLogModule sender, ErrorLoggedEventArgs args) =>
            //    {
            //        Exception ex = new Exception("This is from ErrorLog");
                    
            //        ErrorSignal.FromCurrentContext().Raise(ex);
            //    });
        }








        /// <summary>
        /// Log error to Elmah
        /// </summary>
        public static void LogError(Exception ex, string contextualMessage = null)
        {
            try
            {
                // log error to Elmah
                if (contextualMessage != null)
                {
                    // log exception with contextual information that's visible when 
                    // clicking on the error in the Elmah log
                    var annotatedException = new Exception(contextualMessage, ex);
                    ErrorSignal.FromCurrentContext().Raise(annotatedException, HttpContext.Current);
                }
                else
                {
                    ErrorSignal.FromCurrentContext().Raise(ex, HttpContext.Current);
                }

                // send errors to ErrorWS (my own legacy service)
                // using (ErrorWSSoapClient client = new ErrorWSSoapClient())
                // {
                //    client.LogErrors(...);
                // }
            }
            catch (Exception)
            {
                // uh oh! just keep going
            }
        }
    }
}
