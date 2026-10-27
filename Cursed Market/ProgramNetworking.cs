using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;






namespace Cursed_Market
{
    public static class ProgramNetworking
    {
        public static readonly List<string> defaultHeaders = new List<string>() // Default set of headers to use in networking requests.
        {
            $"User-Agent: {ProgramPaths.executableFriendlyName}",
            $"Application-Version: {ProgramGlobals.rawFileVersion}", // Dotted format, e.g. "9.1.0.0".
            $"Startup-Time: {ProgramGlobals.startupTimeStamp}"
        };




        /* Local port FiddlerCore listens on and registers as system proxy. */
        public const int proxyPort = 38517;

        public static bool CheckIfProxyHasIssues(out string issueReasoning)
        {
            /* FiddlerCore isn't running or there's no Proxy instance running on PC. */
            if (FiddlerCore.IsRunning() && WinReg.GetActiveProxySettings(out int flags, out string proxyServer, out string autoConfigUrl))
            {
                if ((flags & WinReg.PROXY_TYPE_AUTO_PROXY_URL) != 0 && !string.IsNullOrWhiteSpace(autoConfigUrl))
                {
                    issueReasoning = $"Cursed Market proxy is running, but Windows uses a proxy setup script instead of it:\n\"{autoConfigUrl}\"\n\nWhile the script is on, game traffic doesn't go through Cursed Market. Turn off \"Use setup script\" in Windows proxy settings (VPN and antivirus apps often turn it on).";
                    return true;
                }

                if ((flags & WinReg.PROXY_TYPE_PROXY) == 0)
                {
                    issueReasoning = "Cursed Market proxy is running, but Windows proxy has been turned off.\n\nSome other program (VPN, antivirus or another proxy tool) has changed proxy settings, so game traffic doesn't go through Cursed Market.";
                    return true;
                }

                if (!WinReg.GetIsCursedProxyServer(proxyServer))
                {
                    issueReasoning = $"Cursed Market proxy is running, but Windows proxy is set to \"{proxyServer}\" instead of 127.0.0.1:{ProgramNetworking.proxyPort}.\n\nSome other program (VPN, antivirus or another proxy tool) has changed proxy settings, so game traffic doesn't go through Cursed Market.";
                    return true;
                }
            }

            issueReasoning = string.Empty;
            return false;
        }




        public const string proxyDefaultCertPassword = "ybxmUwpz1UjPdDXJqRvOWdlktlVUztq48JNE4MxXQkQsmQdqC7lXjlGJJbCBsWJQ";
        public static string GetProxyPassword()
        {
            string certPassword = WinReg.GetData_SZ(WinReg.Common.certificatePassword);
            if (certPassword == null)
            {
                string randomPassword = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N"); // "N" stands for stripping "-" out of GUID's.

                if (WinReg.SetData_SZ(WinReg.Common.certificatePassword, randomPassword))
                    return randomPassword;
                else
                    return proxyDefaultCertPassword;
            }

            return certPassword;
        }
        public static bool WriteProxyPassword(bool openFileAfterCreation)
        {
            if (Directory.Exists(ProgramPaths.GetDataFolderPath()))
            {
                try
                {
                    File.WriteAllText(ProgramPaths.networkingPasswordFilePath, GetProxyPassword());
                }
                catch { return false; }

                bool fileExists = File.Exists(ProgramPaths.networkingPasswordFilePath);
                if (openFileAfterCreation && fileExists)
                    Process.Start(ProgramPaths.networkingPasswordFilePath);

                return fileExists;
            }

            return false;
        }
    }
}
