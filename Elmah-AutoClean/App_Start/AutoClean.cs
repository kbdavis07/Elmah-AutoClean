using System;
using AutoClean;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Elmah_AutoClean.App_Start.AutoClean), "Start")]

namespace Elmah_AutoClean.App_Start 
{
    public static class AutoClean 
	{
        public static void Start() 
		{
             CleanUp.AutoClean();
        }
    }
}