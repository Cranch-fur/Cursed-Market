using Microsoft.Win32;
using System;

namespace Cursed_Market
{
    public static class WinReg
    {
        // BINARY = byte[]
        // DWORD = int (Int32)
        // QWORD = long (Int64)
        // SZ = string

        // "HKEY_CURRENT_USER" --> Hive
        // "HKEY_CURRENT_USER\SOFTWARE" --> Key
        // "HKEY_CURRENT_USER\SOFTWARE\Microsoft" --> Sub-Key
        // "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer" --> Sub-Key
        // "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer\Start Page (REG_SZ)" --> Value
        public const string subKeyPath = @"HKEY_CURRENT_USER\SOFTWARE\Cursed Market";
        private static readonly string shortSubKeyPath = subKeyPath.Substring(subKeyPath.IndexOf(@"\") + 1); // Remove Hive from the registry path, start it with the Key instead.




        public class SE_CommonEntries
        {
            public static readonly string firstLaunch      = "First Launch Complete";
            public static readonly string applicationTheme = "Application Theme";
            public static readonly string queueNotifySound = "Queue Notify Sound";
            public static readonly string crosshair        = "Crosshair";
            public static readonly string crosshairOpacity = "Crosshair Opacity";
            public static readonly string gameProfile      = "Game Profile";
        }




        public static bool GetSubKeyExist()
        {
            try
            {
                using (RegistryKey subKey = Registry.CurrentUser.OpenSubKey(shortSubKeyPath, true))
                {
                    if (subKey != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
                using (RegistryKey subKey = Registry.CurrentUser.OpenSubKey(shortSubKeyPath, true))
                {
                    if (subKey != null)
                    {
                        Registry.CurrentUser.DeleteSubKeyTree(shortSubKeyPath);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }




        public static byte[] GetData_BINARY(string entryName, byte[] defaultReturn = null)
        {
            object data = Registry.GetValue(subKeyPath, entryName, defaultReturn);
            if (data != null && data is byte[])
            {
                return (byte[])data;
            }
            else
                return defaultReturn;
        }
        public static int GetData_DWORD(string entryName, int defaultReturn = -1)
        {
            object data = Registry.GetValue(subKeyPath, entryName, defaultReturn);
            if (data != null && data is int)
            {
                return Convert.ToInt32(data);
            }
            else
                return defaultReturn;
        }
        public static long GetData_QWORD(string entryName, long defaultReturn = -1)
        {
            object data = Registry.GetValue(subKeyPath, entryName, defaultReturn);
            if (data != null && data is long)
            {
                return Convert.ToInt32(data);
            }
            else
                return defaultReturn;
        }
        public static string GetData_SZ(string entryName, string defaultReturn = null)
        {
            object data = Registry.GetValue(subKeyPath, entryName, defaultReturn);
            if (data != null && data is string)
            {
                return Convert.ToString(data);
            }
            else
                return defaultReturn;
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




        public static bool DisableProxy()
        {
            try
            {
                string internetSettingsKey = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings";
                Registry.SetValue(internetSettingsKey, "ProxyEnable", 0, RegistryValueKind.DWord);
                Registry.SetValue(internetSettingsKey, "ProxyServer", "", RegistryValueKind.String);
                Registry.SetValue(internetSettingsKey, "ProxyOverride", "", RegistryValueKind.String);


                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
