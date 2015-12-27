using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using static ManagedNativeWifi.Win32.NativeMethod;

namespace ManagedNativeWifi.Win32
{
	internal static class NativeMethodExtra
	{
		[DllImport("Wlanapi.dll")]
		public static extern uint WFDOpenHandle(
			uint dwClientVersion,
			out uint pdwNegotiatedVersion,
			out IntPtr phClientHandle);

		[DllImport("Wlanapi.dll")]
		public static extern uint WFDCloseHandle(
			IntPtr hClientHandle);

		[DllImport("Wlanapi.dll")]
		public static extern uint WFDStartOpenSession(
			IntPtr hClientHandle,
			DOT11_MAC_ADDRESS pDeviceAddress,
			IntPtr pvContext,
			WFD_OPEN_SESSION_COMPLETE_CALLBACK pfnCallback,
			out IntPtr phSessionHandle);

		public delegate void WFD_OPEN_SESSION_COMPLETE_CALLBACK(
			IntPtr hSessionHandle,
			IntPtr pvContext,
			[MarshalAs(UnmanagedType.LPStruct)] Guid guidSessionInterface,
			uint dwError,
			uint dwReasonCode);

		[DllImport("Wlanapi.dll")]
		public static extern uint WFDCancelOpenSession(
			IntPtr hSessionHandle);

		[DllImport("Wlanapi.dll")]
		public static extern uint WFDCloseSession(
			IntPtr hSessionHandle);
	}
}