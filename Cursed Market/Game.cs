using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO;






namespace Cursed_Market
{
    public static class Game
    {
        private static Process GetProcess()
        {
            Process matchedProcess = null;
            Process[] processArray = Process.GetProcesses();

            foreach (Process process in processArray)
            {
                try
                {
                    if (matchedProcess == null && process.ProcessName.Contains("DeadByDaylight-"))
                    {
                        matchedProcess = process;
                        continue;
                    }
                }
                catch // Ignore processes that exited or denied access during enumeration.
                { }

                process.Dispose();
            }

            return matchedProcess;
        }

        public static bool IsRunning()
        {
            using (Process gameProcess = GetProcess())
            {
                if (gameProcess == null)
                    return false;

                try
                {
                    return !gameProcess.HasExited && gameProcess.Responding;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool Exit()
        {
            using (Process gameProcess = GetProcess())
            {
                if (gameProcess == null)
                    return true;

                try
                {
                    gameProcess.Kill();
                    gameProcess.WaitForExit(5000);
                    return gameProcess.HasExited;
                }
                catch
                {
                    return false;
                }
            }
        }




        public static bool ClearContentCache(string specificFile = null)
        {
            if (Directory.Exists(ProgramPaths.contentCacheDirectoryPath) == false)
                return true;

            if (specificFile != null)
            {
                try
                {
                    string cachedFilePath = Path.Combine(ProgramPaths.contentCacheDirectoryPath, specificFile);
                    if (File.Exists(cachedFilePath) == false)
                        return true;

                    File.Delete(cachedFilePath);
                    return File.Exists(cachedFilePath) == false;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    Directory.Delete(ProgramPaths.contentCacheDirectoryPath, true);
                    return Directory.Exists(ProgramPaths.contentCacheDirectoryPath) == false;
                }
                catch
                {
                    return false;
                }
            }
        }




        public static void ResolveUserID(string gameAuthResponse)
        {
            if (!string.IsNullOrWhiteSpace(gameAuthResponse) && gameAuthResponse.IsJson())
            {
                try
                {
                    JToken token = JToken.Parse(gameAuthResponse);
                    if (token is JObject json && json.ContainsKey("userId"))
                    {
                        ProgramSession.Game.userId = (string)json["userId"];
                    }
                }
                catch // Ignore malformed auth payloads.
                { }
            }
        }
    }
}