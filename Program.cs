using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Management;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Collections;
using System.Net;
using System.Runtime.Remoting;
using System.Text.RegularExpressions;
using System.CodeDom.Compiler;

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
        const int PRODUCT_CORE_SINGLELANGUAGE = 0x00000064;
        const int PRODUCT_CORE_COUNTRYSPECIFIC = 0x00000063;

        public static string licence_code;
        
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
                            licence_code = "TX9XD-98N7V-6WMQ6-BX7FG-H8Q99";
                            return "Home";
                        case PRODUCT_HOME_N:
                            licence_code = "3KHY7-WNT83-DGQKR-F7HPR-844BM";
                            return "Home N";
                        case PRODUCT_PROFESSIONAL:
                            licence_code = "W269N-WFGWX-YVC9B-4J6C9-T83GX";
                            return "Professional";
                        case PRODUCT_PROFESSIONAL_N:
                            licence_code = "MH37W-N47XK-V7XM9-C7227-GCQG9";
                            return "Proffesional N";
                        case PRODUCT_EDUCATION:
                            licence_code = "NW6C2-QMPVW-D7KKK-3GKT6-VCFB2";
                            return "Education";
                        case PRODUCT_EDUCATION_N:
                            licence_code = "2WH4N-8QGBV-H22JP-CT43Q-MDWWJ";
                            return "Education N";
                        case PRODUCT_ENTERPRISE:
                            licence_code = "NPPR9-FWDCX-D2C8J-H872K-2YT43";
                            return "Enterprise";
                        case PRODUCT_ENTERPRISE_N:
                            licence_code = "DPH2V-TTNVB-4X9Q3-TJR4H-KHJW4";
                            return "Enterprise N";
                        case PRODUCT_CORE_SINGLELANGUAGE:
                            licence_code = "7HNRX-D7KGG-3K4RQ-4WPJ4-YTDFH";
                            return "Home Single Language";
                        case PRODUCT_CORE_COUNTRYSPECIFIC:
                            licence_code = "PVMJN-6DFY6-9CCP6-7BKTT-D3WVR";
                            return "Home China";
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
        
        // https://learn.microsoft.com/en-us/windows/win32/api/sysinfoapi/nf-sysinfoapi-getproductinfo
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
            Console.Title = "Windows 10 & 11 Activator";
            
            string edition = GetWindowsEdition();
            string Yes_No;

            if (edition != null)
            {
                Console.WriteLine("Your Operating System: Windows " + edition);

                do
                {
                    Console.Write("Do you want to Activate your Windows?[Y,N]: ");
                    Yes_No = Console.ReadLine().ToLower();
                } while (!(Yes_No == "yes" || Yes_No == "ye" || Yes_No == "y" || Yes_No == "no" || Yes_No == "n"));

                if(Yes_No == "no" || Yes_No == "n")
                {
                    Console.Clear();
                    Console.WriteLine("Thanks for the usage!");
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                }

                Console.Clear();
                Thread.Sleep(1000);
            }
            else {
                Console.WriteLine("Your Operating System cannot be identified.");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.Verb = "runas";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = false;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.StandardOutputEncoding = Encoding.UTF8;

            Console.WriteLine("Outputs:");
            cmd.Start();
            cmd.StandardInput.WriteLine($"slmgr /ipk {licence_code}");
            Console.WriteLine($"slmgr /ipk {licence_code}");
            cmd.StandardInput.Flush();
            Thread.Sleep(5000);
            cmd.StandardInput.WriteLine("slmgr /skms kms8.msguides.com");
            Console.WriteLine("slmgr /skms kms8.msguides.com");
            Thread.Sleep(5000);
            cmd.StandardInput.WriteLine("slmgr /ato");
            Console.WriteLine("slmgr /ato");
            Thread.Sleep(3000);
            cmd.StandardInput.Close();
            cmd.WaitForExit();

            // Kiíratja a commandokat
            //Console.WriteLine(cmd.StandardOutput.ReadToEnd());

            Console.WriteLine("Your Windows 10 Succesfully Activated!");

            Console.ReadKey();
        }
    }
}