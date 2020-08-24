using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Net;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GTFCommActiveX
{
    /// <summary>
    /// See Internet SDK, IObjectSafety.
    /// </summary>
    [Flags]
    public enum ObjectSafetyFlags : int
    {
        /// <summary>
        /// Caller of interface may be untrusted
        /// </summary>
        INTERFACESAFE_FOR_UNTRUSTED_CALLER = 1,

        /// <summary>
        /// Data passed into interface may be untrusted
        /// </summary>
        INTERFACESAFE_FOR_UNTRUSTED_DATA = 2,

        /// <summary>
        /// Object knows to use IDispatchEx.
        /// </summary>
        INTERFACE_USES_DISPEX = 4,

        /// <summary>
        /// Objects knows to use IInternetHostSecurityManager.
        /// </summary>
        INTERFACE_USES_SECURITY_MANAGER = 8,

        /// <summary>
        /// Flags combination.
        /// </summary>
        SafeForScripting = INTERFACESAFE_FOR_UNTRUSTED_CALLER |
        INTERFACESAFE_FOR_UNTRUSTED_DATA
    }

    /// <summary>
    /// See Internet SDK, IObjectSafety.
    /// </summary>
    [ComVisible(true)]
    [ComImport]
    [Guid("EC35D551-0E50-4F16-B80E-C6636D7D183F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IObjectSafety
    {
        void GetInterfaceSafetyOptions(ref Guid riid, out int supportedOptions, out int enabledOptions);
        void SetInterfaceSafetyOptions(ref Guid riid, int optionSetMask, int enabledOptions);
    }

    [ComVisible(false)]
    public delegate void EventHandlerMyEvent(string sValue);
    [Guid("1506AE41-4617-417D-B69A-8E8D10A2B1F9")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IActiveXControl_Method
    {
        [DispId(1)]
        int PassScan();
        [DispId(2)]
        int PassSetInfo();
        [DispId(3)]
        int JPNPrintTicket(string docid, string retailer, string goods, string tourist, string adsinfo);
        [DispId(4)]
        int time_out
        {
            get;
            set;
        }
        [DispId(5)]
        int print_count
        {
            get;
            set;
        }
        [DispId(6)]
        string buyer_name
        {
            get;
            set;
        }
        [DispId(7)]
        string nationality_code
        {
            get;
            set;
        }
        [DispId(8)]
        string gender_code
        {
            get;
            set;
        }
        [DispId(9)]
        string passport_serial_no
        {
            get;
            set;
        }
        [DispId(10)]
        string buyer_birth
        {
            get;
            set;
        }
        [DispId(11)]
        string pass_expirydt
        {
            get;
            set;
        }
        [DispId(12)]
        string entry_port
        {
            get;
            set;
        }
        [DispId(13)]
        string residence_name
        {
            get;
            set;
        }
        [DispId(14)]
        string entry_date
        {
            get;
            set;
        }
        [DispId(15)]
        string buyer_no
        {
            get;
            set;
        }
        [DispId(16)]
        string passport_type
        {
            get;
            set;
        }
    }

    [Guid("F5EC9188-71E6-4F39-9D84-6DC4195EF85A")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IActiveXControl_Event
    {
        [DispId(1)]
        void getPassportInfo(string param, int ret);
    }


    [Guid("3E837B10-CCB9-4611-8D8C-A6C2DF3CC584")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(IActiveXControl_Event))]
    public partial class GTF_COS : UserControl, IActiveXControl_Method, IObjectSafety
    {
        [ComRegisterFunction()]
        public static void RegisterClass(string key)
        {
            // Strip off HKEY_CLASSES_ROOT\ from the passed key as I don't need it 
            StringBuilder sb = new StringBuilder(key);
            sb.Replace(@"HKEY_CLASSES_ROOT\", "");

            // Open the CLSID\{guid} key for write access 
            RegistryKey k = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

            // And create    the    'Control' key -    this allows    it to show up in 
            // the ActiveX control container 
            RegistryKey ctrl = k.CreateSubKey("Control");
            ctrl.Close();

            // Next create the CodeBase entry    - needed if    not    string named and GACced. 
            RegistryKey inprocServer32 = k.OpenSubKey("InprocServer32", true);
            inprocServer32.SetValue("CodeBase", Assembly.GetExecutingAssembly().CodeBase);
            inprocServer32.Close();

            // Finally close the main    key 
            k.Close();
            MessageBox.Show(key + " 's registered");
        }

        ///    <summary> 
        ///    Called to unregister the control 
        ///    </summary> 
        ///    <param name="key">Tke registry key</param> 
        [ComUnregisterFunction()]
        public static void UnregisterClass(string key)
        {
            StringBuilder sb = new StringBuilder(key);
            sb.Replace(@"HKEY_CLASSES_ROOT\", "");

            // Open    HKCR\CLSID\{guid} for write    access 
            RegistryKey k = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

            // Delete the 'Control'    key, but don't throw an    exception if it    does not exist 
            k.DeleteSubKey("Control", false);

            // Next    open up    InprocServer32 
            RegistryKey inprocServer32 = k.OpenSubKey("InprocServer32", true);

            // And delete the CodeBase key,    again not throwing if missing 
            k.DeleteSubKey("CodeBase", false);

            // Finally close the main key 
            k.Close();
        }

        public void GetInterfaceSafetyOptions(ref Guid riid, out int supportedOptions, out int enabledOptions)
        {
            supportedOptions = enabledOptions = (int)ObjectSafetyFlags.SafeForScripting;
        }

        public void SetInterfaceSafetyOptions(ref Guid riid, int optionSetMask, int enabledOptions)
        {

        }
    }
}
