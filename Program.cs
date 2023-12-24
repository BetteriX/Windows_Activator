using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Collections;

namespace Windows_Activator
{
    internal class Program
    {
        const int PRODUCT_UNDEFINED = 0x00000000;
        const int PRODUCT_HOME = 0x00000065;
        const int PRODUCT_HOME_N = 0x00000062;
        const int PRODUCT_EDUCATION = 0x00000079;
        const int PRODUCT_EDUCATION_N = 0x0000007A;
        const int PRODUCT_ENTERPRISE = 0x00000004;
        const int PRODUCT_ENTERPRISE_N = 0x0000001B;
        const int PRODUCT_PROFESSIONAL = 0x00000030;
        const int PRODUCT_PROFESSIONAL_N = 0x00000031;

        static string GetWindowsEdition()
        {
            try
            {
                int editionId;
                bool result = GetProductInfo(10, 0, 0, 0, out editionId);


                if (result)
                {
                    switch (editionId)
                    {
                        case PRODUCT_HOME:
                            return "Home";
                        case PRODUCT_HOME_N:
                            return "Home N";
                        case PRODUCT_PROFESSIONAL:
                            return "Professional";
                        case PRODUCT_PROFESSIONAL_N:
                            return "Proffesional N";
                        case PRODUCT_EDUCATION:
                            return "Education";
                        case PRODUCT_EDUCATION_N:
                            return "Education N";
                        case PRODUCT_ENTERPRISE:
                            return "Enterprise";
                        case PRODUCT_ENTERPRISE_N:
                            return "Enterprise N";
                        default:
                            return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a Windows Verziója nem megfelelő: {ex.Message}");
            }

            return null;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetProductInfo(
            int dwOSMajorVersion,
            int dwOSMinorVersion,
            int dwSpMajorVersion,
            int dwSpMinorVersion,
            out int pdwReturnedProductType);
    
    static void Main(string[] args)
        {
            Console.Title = "Windows 10 Aktivátor";

            string edition = GetWindowsEdition();

            if (edition != null) {
                Console.WriteLine("Operációs Rendszered: Windows " + edition);
                Thread.Sleep(1000);
            }
            else {
                Console.WriteLine("Nem érzékel Operációs rendszert");
                Thread.Sleep(1000);
            }


            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.Verb = "runas";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = false;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.StandardOutputEncoding = Encoding.UTF8;

            cmd.Start();

            
            if (edition == "Home") { cmd.StandardInput.WriteLine("slmgr /ipk TX9XD-98N7V-6WMQ6-BX7FG-H8Q99"); }
            else if (edition == "Home N ") { cmd.StandardInput.WriteLine("slmgr /ipk 3KHY7-WNT83-DGQKR-F7HPR-844BM"); }
            else if (edition == "Professional") { cmd.StandardInput.WriteLine("slmgr /ipk W269N-WFGWX-YVC9B-4J6C9-T83GX"); }
            else if (edition == "Professional N") { cmd.StandardInput.WriteLine("slmgr /ipk MH37W-N47XK-V7XM9-C7227-GCQG9"); }
            else if (edition == "Education") { cmd.StandardInput.WriteLine("slmgr /ipk NW6C2-QMPVW-D7KKK-3GKT6-VCFB2"); }
            else if (edition == "Education N") { cmd.StandardInput.WriteLine("slmgr /ipk 2WH4N-8QGBV-H22JP-CT43Q-MDWWJ"); }
            else if (edition == "Enterprise") { cmd.StandardInput.WriteLine("slmgr / ipk NPPR9-FWDCX-D2C8J-H872K-2YT43"); }
            else if (edition == "Enterprise N") { cmd.StandardInput.WriteLine("slmgr /ipk DPH2V-TTNVB-4X9Q3-TJR4H-KHJW4"); }
            else { Environment.Exit(0); }
            cmd.StandardInput.Flush();
            Thread.Sleep(5000);
            cmd.StandardInput.WriteLine("slmgr /skms kms8.msguides.com");
            Thread.Sleep(5000);
            cmd.StandardInput.WriteLine("slmgr /ato");
            cmd.StandardInput.Close();
            cmd.WaitForExit();

            // Kiíratja a commandokat
            //Console.WriteLine(cmd.StandardOutput.ReadToEnd());

            Console.WriteLine("Sikeresen aktiválva van a Windows 10!");

            Console.ReadKey();
        }
    }
}