using ManagedNativeWifi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace radiomanager
{
    class Program
    {
        static void Main(string[] args)
        {

            ConsoleTraceListener consoleTracer = new ConsoleTraceListener();
            Trace.Listeners.Add(consoleTracer);

            if (args.Length != 1)
            {
                ShowRadioInformation();
                return;
            }

            string arg = args[0];
            if (arg == "on")
            {
                TurnOn();
            }
            else if (arg == "off")
            {
                TurnOff();
            }
            else
                ShowRadioInformation();
        }

        private static void TurnOff()
        {
            foreach (var interfaceInfo in NativeWifi.EnumerateInterfaces())
            {
                Trace.WriteLine($"Interface: {interfaceInfo.Description} ({interfaceInfo.Id})");

                try
                {
                    Trace.WriteLine($"Turn off: {NativeWifi.TurnOffInterfaceRadio(interfaceInfo.Id)}");
                }
                catch (UnauthorizedAccessException)
                {
                    Trace.WriteLine("Turn off: Unauthorized");
                }
            }
        }

        private static void TurnOn()
        {
            foreach (var interfaceInfo in NativeWifi.EnumerateInterfaces())
            {
                Trace.WriteLine($"Interface: {interfaceInfo.Description} ({interfaceInfo.Id})");

                try
                {
                    Trace.WriteLine($"Turn on: {NativeWifi.TurnOnInterfaceRadio(interfaceInfo.Id)}");
                }
                catch (UnauthorizedAccessException)
                {
                    Trace.WriteLine("Turn on: Unauthorized");
                }
            }
        }

        private static void ShowRadioInformation()
        {
            foreach (var interfaceInfo in NativeWifi.EnumerateInterfaces())
            {
                Trace.WriteLine($"Interface: {interfaceInfo.Description} ({interfaceInfo.Id})");

                var interfaceRadio = NativeWifi.GetInterfaceRadio(interfaceInfo.Id);
                if (interfaceRadio == null)
                    continue;

                foreach (var radioSet in interfaceRadio.RadioSets)
                {
                    Trace.WriteLine($"Type: {radioSet.Type}");
                    Trace.WriteLine($"HardwareOn: {radioSet.HardwareOn}, SoftwareOn: {radioSet.SoftwareOn}, On: {radioSet.On}");
                }
            }
        }
    }
}
