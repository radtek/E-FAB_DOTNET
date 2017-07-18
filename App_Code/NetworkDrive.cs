using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace oscar.Portal.Common
{
    /// <summary> 
    /// Network Drive class / wrapper 
    /// </summary> 
    public class NetworkDrive
    {
        #region ��������

        private bool lf_SaveCredentials = false;
        /// <summary> 
        /// �Ƿ��������ϱ������ 
        /// </summary> 
        public bool SaveCredentials
        {
            get { return (lf_SaveCredentials); }
            set { lf_SaveCredentials = value; }
        }


        private bool lf_Persistent = false;
        /// <summary> 
        /// ������ʱ���Ƿ��������� 
        /// </summary> 
        public bool Persistent
        {
            get { return (lf_Persistent); }
            set { lf_Persistent = value; }
        }


        private bool lf_Force = false;
        /// <summary> 
        /// ������ڵ�ǰӳ�䣬�Ƿ�ǿ�ƽ�ԭ��ӳ��ı������Ҫ��ӳ�� 
        /// or force disconnection if network path is not responding... 
        /// </summary> 
        public bool Force
        {
            get { return (lf_Force); }
            set { lf_Force = value; }
        }


        private bool ls_PromptForCredentials = false;
        /// <summary> 
        /// ӳ��ʱ�Ƿ�����ʹ�������֤ 
        /// </summary> 
        public bool PromptForCredentials
        {
            get { return (ls_PromptForCredentials); }
            set { ls_PromptForCredentials = value; }
        }


        private bool lf_FindNextFreeDrive = false;
        /// <summary> 
        /// �Ƿ��Զ�ȥ�ұ��صĿ����������� 
        /// </summary> 
        public bool FindNextFreeDrive
        {
            get { return (lf_FindNextFreeDrive); }
            set
            {
                lf_FindNextFreeDrive = value;
            }
        }


        private string ls_LocalDrive = null;
        /// <summary> 
        /// ����ӳ��ı��������� 
        /// </summary> 
        public string LocalDrive
        {
            get { return (ls_LocalDrive); }
            set
            {
                if (value == null || value.Length == 0)
                {
                    ls_LocalDrive = null;
                }
                else
                {

                    ls_LocalDrive = value.Substring(0, 1) + ":";
                }
            }
        }


        private string ls_ShareName = "";
        /// <summary> 
        /// Զ�̼�����Ĺ����ļ��У�����\\Computer\C$' 
        /// </summary> 
        public string ShareName
        {
            get { return (ls_ShareName); }
            set { ls_ShareName = value; }
        }


        /// <summary> 
        /// ��ǰ�Ѿ�ӳ����������� 
        /// </summary> 
        public string[] MappedDrives
        {
            get
            {
                System.Collections.ArrayList oDrives = new System.Collections.ArrayList();
                foreach (string sDrive in System.IO.Directory.GetLogicalDrives())
                {
                    if (PathIsNetworkPath(sDrive))
                    {
                        oDrives.Add(sDrive);
                    }
                }
                return ((string[])oDrives.ToArray(typeof(string)));
            }
        }


        #endregion

        #region ��������

        /// <summary> 
        /// ӳ������������ 
        /// </summary> 
        public void MapDrive()
        {
            z_MapDrive(null, null);
        }


        /// <summary> 
        /// ӳ������������ (�����֤ʹ���û������뷽ʽ) 
        /// </summary> 
        /// <param name="Username">Զ�̼�������û���</param> 
        /// <param name="Password">Զ�̼����������</param> 
        public void MapDrive(string Username, string Password)
        {
            z_MapDrive(Username, Password);
        }


        /// <summary> 
        /// ʹ�ù���������ӳ������������ 
        /// </summary> 
        /// <param name="LocalDrive">������������</param> 
        /// <param name="ShareName">Զ�̵Ĺ����ļ���(����. '\\Computer\Share')</param> 
        /// <param name="Force">�Ƿ�ǿ��ӳ��</param> 
        public void MapDrive(string LocalDrive, string ShareName, bool Force)
        {
            ls_LocalDrive = LocalDrive;
            ls_ShareName = ShareName;
            lf_Force = Force;
            z_MapDrive(null, null);
        }


        /// <summary> 
        /// ʹ�ù���������ӳ������������ 
        /// </summary> 
        /// <param name="LocalDrive">������������</param> 
        /// <param name="Force">�Ƿ�ǿ��ӳ��</param> 
        public void MapDrive(string LocalDrive, bool Force)
        {
            ls_LocalDrive = LocalDrive;
            ls_ShareName = ShareName;
            lf_Force = Force;
            z_MapDrive(null, null);
        }


        /// <summary> 
        /// �Ͽ�ӳ�� 
        /// </summary> 
        public void UnMapDrive()
        {
            z_UnMapDrive();
        }


        /// <summary> 
        /// �Ͽ�ӳ��(�����ض���ӳ��) 
        /// </summary> 
        public void UnMapDrive(string LocalDrive)
        {
            ls_LocalDrive = LocalDrive;
            z_UnMapDrive();
        }


        /// <summary> 
        /// �Ͽ�ӳ��(�����ض���ӳ��) 
        /// </summary> 
        public void UnMapDrive(string LocalDrive, bool Force)
        {
            ls_LocalDrive = LocalDrive;
            lf_Force = Force;
            z_UnMapDrive();
        }


        /// <summary> 
        /// �ָ�ӳ�� 
        /// </summary> 
        public void RestoreDrives()
        {
            //request all drives be restored 
            z_RestoreDrive(null);
        }


        /// <summary> 
        /// �ָ�ӳ�� 
        /// </summary> 
        public void RestoreDrive(string LocalDrive)
        {
            //request drive be reinstalled 
            z_RestoreDrive(LocalDrive);
        }


        /// <summary> 
        /// ӳ��������������ʱ����ʾ���� 
        /// </summary>       
        public void ShowConnectDialog()
        {
            z_DisplayDialog(IntPtr.Zero, 1);
        }


        /// <summary> 
        /// ӳ��������������ʱ����ʾ���� 
        /// </summary> 
        /// <param name="ParentForm">Ҫ���ĸ�ĸ��������ʾ</param> 
        public void ShowConnectDialog(IntPtr ParentFormHandle)
        {
            z_DisplayDialog(ParentFormHandle, 1);
        }


        /// <summary> 
        /// �Ͽ�������������ʱ����ʾ���� 
        /// </summary>       
        public void ShowDisconnectDialog()
        {
            z_DisplayDialog(IntPtr.Zero, 2);
        }


        /// <summary> 
        /// �Ͽ�������������ʱ����ʾ���� 
        /// </summary> 
        /// <param name="ParentForm">Ҫ���ĸ�ĸ��������ʾ</param> 
        public void ShowDisconnectDialog(IntPtr ParentFormHandle)
        {
            z_DisplayDialog(ParentFormHandle, 2);
        }


        /// <summary> 
        /// �õ������������Ĺ����ļ��У�����. \\computer\share 
        /// </summary> 
        /// <param name="DriveName">���������� (����. 'X:')</param> 
        /// <returns></returns> 
        public string GetMappedShareName(string LocalDrive)
        {

            //collect and clean the passed LocalDrive param 
            if (LocalDrive == null || LocalDrive.Length == 0) throw new Exception("Invalid 'LocalDrive' passed, 'LocalDrive' parameter cannot be 'null'");
            LocalDrive = LocalDrive.Substring(0, 1);

            //call api to collect LocalDrive's share name  
            int i = 255; byte[] bSharename = new byte[i];
            int iCallStatus = WNetGetConnection(LocalDrive + ":", bSharename, ref i);
            switch (iCallStatus)
            {
                case 85: throw new Exception("Mapping failed, the 'DriveName' is exists!");
                case 1200: throw new Exception("Invalid / Malfored 'Drive Name' passed to 'GetShareName' function (API: ERROR_BAD_DEVICE)");
                case 1201: throw new Exception("Cannot collect 'ShareName', Passed 'DriveName' is valid but currently not connected (API: ERROR_CONNECTION_UNAVAIL)");
                case 1202: throw new Exception("Mapping failed, don't find the mapping path.");
                case 1203: throw new Exception("Mapping failed, don't find the mapping path.");
                case 1208: throw new Exception("API function 'WNetGetConnection' failed (API: ERROR_EXTENDED_ERROR:" + iCallStatus.ToString() + ")");
                case 1222: throw new Exception("Cannot collect 'ShareName', No network connection found (API: ERROR_NO_NETWORK / ERROR_NO_NET_OR_BAD_PATH)");
                case 1326: throw new Exception("Mapping failed, password is wrong.");
                case 2250: throw new Exception("Invalid 'DriveName' passed, Drive is not a network drive (API: ERROR_NOT_CONNECTED)");
                case 234: throw new Exception("Invalid 'Buffer' length, buffer is too small (API: ERROR_MORE_DATA)");
            }

            //return collected share name 
            return (System.Text.Encoding.GetEncoding(1252).GetString(bSharename, 0, i).TrimEnd((char)0));

        }


        /// <summary> 
        /// �Ƿ������������� 
        /// </summary> 
        /// <param name="DriveName">�������� (����. 'X:')</param> 
        /// <returns></returns> 
        public bool IsNetworkDrive(string LocalDrive)
        {

            //collect and clean the passed LocalDrive param 
            if (LocalDrive == null || LocalDrive.Trim().Length == 0) throw new Exception("Invalid 'LocalDrive' passed, 'DriveName' cannot be 'empty'");
            LocalDrive = LocalDrive.Substring(0, 1);

            //return status of drive type 
            return (IsNetworkDrive(LocalDrive + ":"));
        }


        #endregion

        #region API

        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2A(ref structNetResource NetResStruct, string Password, string Username, int Flags);
        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2A(string Name, int Flags, int Force);
        [DllImport("mpr.dll")]
        private static extern int WNetConnectionDialog(int hWnd, int Type);
        [DllImport("mpr.dll")]
        private static extern int WNetDisconnectDialog(int hWnd, int Type);
        [DllImport("mpr.dll", CharSet = CharSet.Unicode)]
        private static extern int WNetRestoreConnectionW(int hWnd, string LocalDrive);
        [DllImport("mpr.dll")]
        private static extern int WNetGetConnection(string LocalDrive, byte[] RemoteName, ref int BufferLength);
        [DllImport("shlwapi.dll")]
        private static extern bool PathIsNetworkPath(string LocalDrive);
        [DllImport("kernel32.dll")]
        private static extern int GetDriveType(string LocalDrive);

        [StructLayout(LayoutKind.Sequential)]
        private struct structNetResource
        {
            public int Scope;
            public int Type;
            public int DisplayType;
            public int Usage;
            public string LocalDrive;
            public string RemoteName;
            public string Comment;
            public string Provider;
        }


        //Standard 
        private const int RESOURCETYPE_DISK = 0x1;
        private const int CONNECT_INTERACTIVE = 0x00000008;
        private const int CONNECT_PROMPT = 0x00000010;
        private const int CONNECT_UPDATE_PROFILE = 0x00000001;
        //IE4+ 
        private const int CONNECT_REDIRECT = 0x00000080;
        //NT5 only 
        private const int CONNECT_COMMANDLINE = 0x00000800;
        private const int CONNECT_CMD_SAVECRED = 0x00001000;

        #endregion

        #region ˽�з���

        // ӳ������������ 
        private void z_MapDrive(string Username, string Password)
        {

            //if drive property is set to auto select, collect next free drive           
            if (lf_FindNextFreeDrive)
            {
                ls_LocalDrive = z_NextFreeDrive();
                if (ls_LocalDrive == null || ls_LocalDrive.Length == 0) { throw new Exception("Could not find valid free drive name"); }
            }

            //create struct data to pass to the api function 
            structNetResource stNetRes = new structNetResource();
            stNetRes.Scope = 2;
            stNetRes.Type = RESOURCETYPE_DISK;
            stNetRes.DisplayType = 3;
            stNetRes.Usage = 1;
            stNetRes.RemoteName = ls_ShareName;
            stNetRes.LocalDrive = ls_LocalDrive;

            //prepare params 
            int iFlags = 0;
            if (lf_SaveCredentials) { iFlags += CONNECT_CMD_SAVECRED; }
            if (lf_Persistent) { iFlags += CONNECT_UPDATE_PROFILE; }
            if (ls_PromptForCredentials) { iFlags += CONNECT_INTERACTIVE + CONNECT_PROMPT; }
            if (Username != null && Username.Length == 0) { Username = null; }
            if (Password != null && Password.Length == 0) { Password = null; }

            //if force, unmap ready for new connection 
            if (lf_Force) { try { z_UnMapDrive(); } catch { } }

            //call and return 
            int i = WNetAddConnection2A(ref stNetRes, Password, Username, iFlags);
            if (i > 0) { throw new System.ComponentModel.Win32Exception(i); }

        }


        // �Ͽ�����������   
        private void z_UnMapDrive()
        {

            //prep vars and call unmap 
            int iFlags = 0; int iRet = 0;
            if (lf_Persistent) { iFlags += CONNECT_UPDATE_PROFILE; }
            if (ls_LocalDrive == null)
            {
                //unmap use connection, passing the share name, as local drive 
                iRet = WNetCancelConnection2A(ls_ShareName, iFlags, Convert.ToInt32(lf_Force));
            }
            else
            {
                //unmap drive 
                iRet = WNetCancelConnection2A(ls_LocalDrive, iFlags, Convert.ToInt32(lf_Force));
            }

            //if errors, throw exception 
            if (iRet > 0) { throw new System.ComponentModel.Win32Exception(iRet); }

        }


        // �ָ����������� 
        private void z_RestoreDrive(string DriveName)
        {
            //call restore and return 
            int i = WNetRestoreConnectionW(0, DriveName);
            if (i > 0) { throw new System.ComponentModel.Win32Exception(i); }
        }


        // ��ʾ���� 
        private void z_DisplayDialog(IntPtr WndHandle, int DialogToShow)
        {
            int i = -1;
            int iHandle = 0;

            //get parent handle 
            if (WndHandle != IntPtr.Zero)
            {
                iHandle = WndHandle.ToInt32();
            }

            //chose dialog to show bassed on  
            if (DialogToShow == 1)
            {
                i = WNetConnectionDialog(iHandle, RESOURCETYPE_DISK);
            }
            else if (DialogToShow == 2)
            {
                i = WNetDisconnectDialog(iHandle, RESOURCETYPE_DISK);
            }
            if (i > 0) { throw new System.ComponentModel.Win32Exception(i); }

        }


        // ��һ�����õ������������� 
        private string z_NextFreeDrive()
        {

            //loop from c to z and check that drive is free 
            string sRet = null;
            for (int i = 67; i <= 90; i++)
            {
                if (GetDriveType(((char)i).ToString() + ":") == 1)
                {
                    sRet = ((char)i).ToString() + ":";
                    break;
                }
            }

            //return selected drive 
            return (sRet);

        }


        #endregion 
    }
}
