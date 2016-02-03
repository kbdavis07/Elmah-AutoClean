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

using System.Web;
using AutoClean;

[assembly: PreApplicationStartMethod(typeof(Start.Ignition), "Start")]
namespace AutoClean
{
    /// <summary>
    /// Tasks that needs to start on Application Start goes here.
    /// </summary>
    class Start
    {

        public static class Ignition
        {
            static readonly object Lock = new object();

            static bool _registered;

            public static void Start()
            {
                lock (Lock)
                {
                    if (_registered)
                        return;
                    StartImpl();
                    _registered = true;
                }
            }

            static void StartImpl()
            {
                CleanUp.AutoClean();
            }

        }
    }
}
