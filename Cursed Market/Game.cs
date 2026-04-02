using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursed_Market
{
    public static class Game
    {
        private static Process GetProcess()
        {
            Process[] processArray = Process.GetProcesses();
            if (processArray.Length > 0)
            {
                foreach (Process process in processArray)
                {
                    if (process.ProcessName.Contains("DeadByDaylight-"))
                        return process;
                }
            }

            return null;
        }

        public static bool IsRunning()
        {
            Process gameProcess = GetProcess();
            return gameProcess.Responding;
        }

        public static bool Exit()
        {
            Process gameProcess = GetProcess();
            gameProcess.Kill();

            return gameProcess.HasExited;
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
    }
}
