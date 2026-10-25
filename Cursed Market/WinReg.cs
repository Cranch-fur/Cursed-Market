using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;






namespace Cursed_Market
{
    public static class WinReg
    {
        /*
            BINARY = byte[]
            DWORD = int (Int32)
            QWORD = long (Int64)
            SZ = string
            
            "HKEY_CURRENT_USER" --> Hive
            "HKEY_CURRENT_USER\SOFTWARE" --> Key
            "HKEY_CURRENT_USER\SOFTWARE\Microsoft" --> Sub-Key
            "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer" --> Sub-Key
            "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer\Start Page (REG_SZ)" --> Value
        */
        public const string subKeyPath = @"HKEY_CURRENT_USER\SOFTWARE\Cursed Market";
        private static readonly string shortSubKeyPath = subKeyPath.Substring(subKeyPath.IndexOf(@"\") + 1); // Remove Hive from the registry path, start it with the Key instead.

        private const string internetSettingsKey = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings";

        private const int INTERNET_OPTION_REFRESH = 37;
        private const int INTERNET_OPTION_SETTINGS_CHANGED = 39;

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

        private const int INTERNET_OPTION_PER_CONNECTION_OPTION = 75;

        private const int INTERNET_PER_CONN_FLAGS = 1;
        private const int INTERNET_PER_CONN_PROXY_SERVER = 2;
        private const int INTERNET_PER_CONN_AUTOCONFIG_URL = 4;

        public const int PROXY_TYPE_PROXY = 0x02;
        public const int PROXY_TYPE_AUTO_PROXY_URL = 0x04;

        [StructLayout(LayoutKind.Sequential)]
        private struct INTERNET_PER_CONN_OPTION_LIST
        {
            public int dwSize;
            public IntPtr pszConnection; // IntPtr.Zero is LAN connection.
            public int dwOptionCount;
            public int dwOptionError;
            public IntPtr pOptions;
        }

        [StructLayout(LayoutKind.Explicit, Size = 8)] // Union of DWORD, LPWSTR & FILETIME (8 bytes on both x86 and x64).
        private struct INTERNET_PER_CONN_OPTION_VALUE
        {
            [FieldOffset(0)] public int dwValue;
            [FieldOffset(0)] public IntPtr pszValue;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct INTERNET_PER_CONN_OPTION
        {
            public int dwOption;
            public INTERNET_PER_CONN_OPTION_VALUE Value;
        }

        [DllImport("wininet.dll", EntryPoint = "InternetQueryOptionW", SetLastError = true)]
        private static extern bool InternetQueryOption(IntPtr hInternet, int dwOption, ref INTERNET_PER_CONN_OPTION_LIST lpBuffer, ref int lpdwBufferLength);

        public static class Common
        {
            public static readonly string firstLaunch = "First Launch Complete";
            public static readonly string applicationTheme = "Application Theme";
            public static readonly string queueNotifySound = "Queue Notify Sound";
            public static readonly string crosshair = "Crosshair";
            public static readonly string crosshairOpacity = "Crosshair Opacity";
            public static readonly string crosshairCode = "Crosshair ShareCode";
            public static readonly string gameProfile = "Game Profile";
        }

        public static class API
        {
            public static readonly string host = "API_Host";
            public static readonly string pathStatus = "API_Path_Status";
            public static readonly string pathUpdate = "API_Path_Update";
            public static readonly string pathMarketFull = "API_Path_MarketFull";
            public static readonly string pathMarketDLC = "API_Path_MarketDLC";
            public static readonly string pathCharacterData = "API_Path_CharacterData";
            public static readonly string pathBloodwebData = "API_Path_BloodwebData";
            public static readonly string pathCharactersList = "API_Path_CharactersList";
            public static readonly string pathItemsList = "API_Path_ItemsList";
            public static readonly string pathCatalog = "API_Path_Catalog";
            public static readonly string pathAntiKillSwitch = "API_Path_AntiKillSwitch";
            public static readonly string pathFont = "API_Path_Font";
            public static readonly string pathCharactersPreset = "API_Path_CharactersPreset";
        }

        public static bool GetSubKeyExist()
        {
            try
            {
                /* Request read-only access (writable: false) when only checking for existence. */
                using (RegistryKey subKey = Registry.CurrentUser.OpenSubKey(shortSubKeyPath, false))
                {
                    return subKey != null;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool DestroySubKey()
        {
            try
            {
                using (RegistryKey subKey = Registry.CurrentUser.OpenSubKey(shortSubKeyPath, false))
                {
                    if (subKey != null)
                    {
                        Registry.CurrentUser.DeleteSubKeyTree(shortSubKeyPath);
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static byte[] GetData_BINARY(string entryName, byte[] defaultReturn = null)
        {
            try
            {
                object data = Registry.GetValue(subKeyPath, entryName, defaultReturn);
                return data is byte[] bytes ? bytes : defaultReturn;
            }
            catch
            {
                return defaultReturn;
            }
        }

        public static int GetData_DWORD(string entryName, int defaultReturn = -1)
        {
            try
            {
                object data = Registry.GetValue(subKeyPath, entryName, defaultReturn);
                return data is int intVal ? intVal : defaultReturn;
            }
            catch
            {
                return defaultReturn;
            }
        }

        public static long GetData_QWORD(string entryName, long defaultReturn = -1)
        {
            try
            {
                object data = Registry.GetValue(subKeyPath, entryName, defaultReturn);
                // Fixed: Previously used Convert.ToInt32(data) which caused OverflowException on 64-bit integers.
                return data is long longVal ? longVal : defaultReturn;
            }
            catch
            {
                return defaultReturn;
            }
        }

        public static string GetData_SZ(string entryName, string defaultReturn = null)
        {
            try
            {
                object data = Registry.GetValue(subKeyPath, entryName, defaultReturn);
                return data is string strVal ? strVal : defaultReturn;
            }
            catch
            {
                return defaultReturn;
            }
        }

        public static bool SetData_BINARY(string entryName, byte[] data)
        {
            try
            {
                Registry.SetValue(subKeyPath, entryName, data, RegistryValueKind.Binary);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SetData_DWORD(string entryName, int data)
        {
            try
            {
                Registry.SetValue(subKeyPath, entryName, data, RegistryValueKind.DWord);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SetData_QWORD(string entryName, long data)
        {
            try
            {
                Registry.SetValue(subKeyPath, entryName, data, RegistryValueKind.QWord);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SetData_SZ(string entryName, string data)
        {
            try
            {
                Registry.SetValue(subKeyPath, entryName, data, RegistryValueKind.String);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /* True if system proxy is enabled and points to our local FiddlerCore port (e.g. left over after a crash). */
        public static bool GetIsOwnProxyEnabled()
        {
            try
            {
                if (!(Registry.GetValue(internetSettingsKey, "ProxyEnable", 0) is int proxyEnable) || proxyEnable == 0)
                    return false;

                return GetIsOwnProxyServer(Registry.GetValue(internetSettingsKey, "ProxyServer", string.Empty) as string);
            }
            catch
            {
                return false;
            }
        }

        public static bool GetIsOwnProxyServer(string proxyServer)
        {
            if (string.IsNullOrEmpty(proxyServer))
                return false;

            /* ProxyServer is either "host:port" or "http=host:port;https=host:port". */
            return Regex.IsMatch(proxyServer, $@"(^|[=;\s])(127\.0\.0\.1|localhost|\[::1\]):{ProgramNetworking.proxyPort}($|[;\s])", RegexOptions.IgnoreCase);
        }

        /* Proxy settings WinINet actually uses right now (registry values alone may be out of sync with them). */
        public static bool GetActiveProxySettings(out int flags, out string proxyServer, out string autoConfigUrl)
        {
            flags = 0;
            proxyServer = null;
            autoConfigUrl = null;

            int[] optionIds = { INTERNET_PER_CONN_FLAGS, INTERNET_PER_CONN_PROXY_SERVER, INTERNET_PER_CONN_AUTOCONFIG_URL };
            int optionSize = Marshal.SizeOf(typeof(INTERNET_PER_CONN_OPTION));
            IntPtr optionsPointer = Marshal.AllocHGlobal(optionSize * optionIds.Length);

            try
            {
                for (int i = 0; i < optionIds.Length; i++)
                    Marshal.StructureToPtr(new INTERNET_PER_CONN_OPTION { dwOption = optionIds[i] }, optionsPointer + i * optionSize, false);

                INTERNET_PER_CONN_OPTION_LIST optionList = new INTERNET_PER_CONN_OPTION_LIST
                {
                    dwSize = Marshal.SizeOf(typeof(INTERNET_PER_CONN_OPTION_LIST)),
                    pszConnection = IntPtr.Zero,
                    dwOptionCount = optionIds.Length,
                    pOptions = optionsPointer
                };
                int optionListSize = optionList.dwSize;

                if (!InternetQueryOption(IntPtr.Zero, INTERNET_OPTION_PER_CONNECTION_OPTION, ref optionList, ref optionListSize))
                    return false;

                INTERNET_PER_CONN_OPTION[] options = new INTERNET_PER_CONN_OPTION[optionIds.Length];
                for (int i = 0; i < optionIds.Length; i++)
                    options[i] = (INTERNET_PER_CONN_OPTION)Marshal.PtrToStructure(optionsPointer + i * optionSize, typeof(INTERNET_PER_CONN_OPTION));

                flags = options[0].Value.dwValue;
                proxyServer = TakeWinINetString(options[1].Value.pszValue);
                autoConfigUrl = TakeWinINetString(options[2].Value.pszValue);

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                Marshal.FreeHGlobal(optionsPointer);
            }
        }

        /* Strings returned by WinINet are allocated with GlobalAlloc and have to be freed by caller. */
        private static string TakeWinINetString(IntPtr pointer)
        {
            if (pointer == IntPtr.Zero)
                return null;

            string value = Marshal.PtrToStringUni(pointer);
            Marshal.FreeHGlobal(pointer); // LocalFree, same heap as GlobalAlloc.
            return value;
        }

        public static bool DisableProxy()
        {
            try
            {
                Registry.SetValue(internetSettingsKey, "ProxyEnable", 0, RegistryValueKind.DWord);
                Registry.SetValue(internetSettingsKey, "ProxyServer", string.Empty, RegistryValueKind.String);
                Registry.SetValue(internetSettingsKey, "ProxyOverride", string.Empty, RegistryValueKind.String);

                /* Without this already running apps keep using dead proxy until they're restarted. */
                InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
                InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}