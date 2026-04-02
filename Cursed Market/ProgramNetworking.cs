using Fiddler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursed_Market
{
    public static class ProgramNetworking
    {
        public static readonly List<string> defaultHeaders = new List<string>() // Default set of headers to use in networking requests.
        {
            $"User-Agent: {ProgramPaths.executableFriendlyName}",
            $"Application-Version: {ProgramGlobals.version}",
            $"Startup-Time: {ProgramGlobals.startupTimeStamp}"
        };




        public const string password = "QLa7X9G6mvNbHuhRjtAnSZ8f3y52DzpwCPeKFJBcVgxMq4dY";
        public static bool WritePasswordFile(bool openOnWritten)
        {
            if (Directory.Exists(ProgramPaths.GetDataFolderPath()))
            {
                try
                {
                    File.WriteAllText(ProgramPaths.networkingPasswordFilePath, ProgramNetworking.password);
                }
                catch
                {
                    return false;
                }

                bool fileExists = File.Exists(ProgramPaths.networkingPasswordFilePath);
                if (openOnWritten && fileExists)
                    Process.Start(ProgramPaths.networkingPasswordFilePath);

                return fileExists;
            }

            return false;
        }
    }
}
