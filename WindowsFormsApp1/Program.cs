﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using ESC_POS_USB_NET.Printer;
using System.Drawing;
using ESC_POS_USB_NET.Enums;
using Application = System.Windows.Forms.Application;
using System.Security.Policy;
using System.Reflection.Emit;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        public static Albet form1 = new Albet();
        public static string protocol = "albet://";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine(args);
            if (args.Length > 0) {

                Form2 form2 = new Form2();
                string arg_str = "";
                foreach (var arg in args)
                {
                    arg_str = arg_str + arg;
                    Console.WriteLine($"Argument received: {arg}");
                    // Perform actions based on the arguments received
                    // Add your custom protocol handling logic here
                }
                string params_url = arg_str.Replace(protocol, "");
                string[] params_ = params_url.Split(Char.Parse("?"));
                string[] result = params_[0].Split(Char.Parse("&"));
                string[] selectednumbers = params_[1].Split(Char.Parse("&"));

                form2.ReceiveValue("GameNumber", arg_str);
                Application.Run(form1);

                if (result.Length > 1)
                {
                    var arguments = new
                    {
                        GameNumber = result[0].Replace("game=", ""),
                        TicketNumber = result[1].Replace("ticket=", ""),
                        Stack = result[2].Replace("stack=", ""),
                        SelectedNumbers = String.Join(" ", selectednumbers),
                        MinPayout = result[3].Replace("minpay=", ""),
                        MaxPayout = result[4].Replace("maxpay=", ""),
                        Date = result[5].Replace("date=", "").Replace("%", "-"),
                        Casher = result[6].Replace("casher=", ""),
                        Branch = result[7].Replace("branch=", ""),
                        Odd = result[8].Replace("odd=", "").Replace("/", ""),
                    };
                    string printer_name = AlbetSettings.Default.printer_name.ToString();
                    // Printer printer = new Printer("POS Printer 80250 Series", "UTF-8");
                    Printer printer = new Printer(printer_name, "UTF-8");

                    printer.AlignLeft();
                    printer.DoubleWidth3();
                    printer.BoldMode("ALbet");
                    printer.NormalWidth();
                    //printer.Image(image);
                    printer.AlignRight();
                    printer.Append("Branch : " + arguments.Branch);
                    printer.Append("Casher : " + arguments.Casher);
                    printer.Append(arguments.Date);
                    printer.AlignLeft();
                    printer.Separator();
                    printer.Append("Game : " + arguments.GameNumber);
                    printer.Append("Ticket : " + arguments.TicketNumber);
                    printer.Append(arguments.SelectedNumbers + "  x" + arguments.Odd);
                    printer.AlignCenter();
                    printer.Separator();
                    printer.Append("STACK --- " + arguments.GameNumber + " ETB");
                    printer.Append("MIN PAYOUT  ".PadRight(17) + (arguments.MinPayout + " ETB").PadLeft(17));
                    printer.Append("MAX PAYOUT  ".PadRight(17) + (arguments.MaxPayout + " ETB").PadLeft(17));
                    printer.Separator();
                    printer.Code128(arguments.TicketNumber);
                    printer.Append("");
                    printer.Separator();
                    printer.Append("Under 21s are strictly forbidden!");
                    printer.Append("Terms and Conditions Apply");

                    printer.FullPaperCut();
                    printer.PrintDocument();
                    printer.FullPaperCut();
                    printer.PartialPaperCut();
                }
                if (result.Length > 1)
                {
                    var arguments = new
                    {
                        GameNumber = result[0].Replace("game=", ""),
                        TicketNumber = result[1].Replace("ticket=", ""),
                        Stack = result[2].Replace("stack=", ""),
                        SelectedNumbers = String.Join(" ", selectednumbers),
                        MinPayout = result[3].Replace("minpay=", ""),
                        MaxPayout = result[4].Replace("maxpay=", ""),
                        Date = result[5].Replace("date=", "").Replace("%", "-"),
                        Casher = result[6].Replace("casher=", ""),
                        Branch = result[7].Replace("branch=", ""),
                        Odd = result[8].Replace("odd=", "").Replace("/", ""),
                    };
                    form2.ReceiveValue("GameNumber", arguments.GameNumber.ToString());
                    form2.ReceiveValue("TicketNumber", arguments.TicketNumber.ToString());
                    form2.ReceiveValue("Stack", arguments.Stack.ToString());
                    form2.ReceiveValue("SelectedNumbers", arguments.SelectedNumbers.ToString());
                    form2.ReceiveValue("MinPayout", arguments.MinPayout.ToString());
                    form2.ReceiveValue("MaxPayout", arguments.MaxPayout.ToString());
                    form2.ReceiveValue("Date", arguments.Date.ToString());
                    form2.ReceiveValue("Casher", arguments.Casher.ToString());
                    form2.ReceiveValue("Branch", arguments.Branch.ToString());
                    form2.ReceiveValue("Odd", arguments.Odd.ToString());
                }
                Application.Run(form2);
            }
            else if (args.Length < 1)
            { 
                Application.Run(form1);
            }
            else
            {
                //string url = "albetprtcl:game=123&ticket=12347686397486389&stack=10&minpay=123&maxpay=567&date=Jan 12 2022&casher=user1&branch=Jemo1&odd=3.5" +
                //"?12&13&14&15&17";
                string url = args[0];
                string params_url = url.Replace(protocol, "");
                string[] params_ = params_url.Split(Char.Parse("?"));
                string[] result = params_[0].Split(Char.Parse("&"));
                string[] selectednumbers = params_[1].Split(Char.Parse("&"));

                if (result.Length > 1)
                {
                    var arguments = new
                    {
                        GameNumber = result[0].Replace("game=", ""),
                        TicketNumber = result[1].Replace("ticket=", ""),
                        Stack = result[2].Replace("stack=", ""),
                        SelectedNumbers = String.Join(" ", selectednumbers),
                        MinPayout = result[3].Replace("minpay=", ""),
                        MaxPayout = result[4].Replace("maxpay=", ""),
                        Date = result[5].Replace("date=", "").Replace("%", "-"),
                        Casher = result[6].Replace("casher=", ""),
                        Branch = result[7].Replace("branch=", ""),
                        Odd = result[8].Replace("odd=", "").Replace("/", ""),
                    };
                    Printer printer = new Printer("POS Printer 80250 Series", "UTF-8");
                    System.Drawing.Image img = System.Drawing.Image.FromFile("C:\\Users\\PROBOOK\\Documents\\Visual Studio 2022\\Projects\\ProtocolHandler\\WindowsFormsApp1\\albets.png");
                    Bitmap image = new Bitmap(Bitmap.FromFile("C:\\Users\\PROBOOK\\Documents\\Visual Studio 2022\\Projects\\ProtocolHandler\\WindowsFormsApp1\\Icon.bmp"));
                    printer.AlignLeft();
                    printer.DoubleWidth3();
                    printer.BoldMode("ALbet");
                    printer.NormalWidth();
                    //printer.Image(image);
                    printer.AlignRight();
                    printer.Append("Branch : " + arguments.Branch);
                    printer.Append("Casher : " + arguments.Casher);
                    printer.Append(arguments.Date);
                    printer.AlignLeft();
                    printer.Separator();
                    printer.Append("Game : " + arguments.GameNumber);
                    printer.Append("Ticket : " + arguments.TicketNumber);
                    printer.Append(arguments.SelectedNumbers + "  x" + arguments.Odd);
                    printer.AlignCenter();
                    printer.Separator();
                    printer.Append("STACK --- " + arguments.GameNumber + " ETB");
                    printer.Append("MIN PAYOUT  ".PadRight(17) + (arguments.MinPayout + " ETB").PadLeft(17));
                    printer.Append("MAX PAYOUT  ".PadRight(17) + (arguments.MaxPayout + " ETB").PadLeft(17));
                    printer.Separator();
                    printer.Code128(arguments.TicketNumber);
                    printer.Append("");
                    printer.Separator();
                    printer.Append("Under 21s are strictly forbidden!");
                    printer.Append("Terms and Conditions Apply");

                    printer.FullPaperCut();
                    printer.PrintDocument();
                    printer.FullPaperCut();
                    printer.PartialPaperCut();
                }
            }
        }
        static void RegisterMyProtocol(string myAppPath)  //myAppPath = full path to your application
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey("myApp");  //open myApp protocol's subkey

            if (key == null)  //if the protocol is not registered yet...we register it
            {
                key = Registry.ClassesRoot.CreateSubKey("myApp");
                key.SetValue(string.Empty, "URL: myApp Protocol");
                key.SetValue("URL Protocol", string.Empty);

                key = key.CreateSubKey(@"shell\open\command");
                key.SetValue(string.Empty, myAppPath + " " + "%1");
                //%1 represents the argument - this tells windows to open this program with an argument / parameter
            }

            key.Close();
        }
    }
}




// related links
// https://codingvision.net/c-register-a-url-protocol
// customurl://?param1=xy&param2=xy
// https://stackoverflow.com/questions/80650/how-do-i-register-a-custom-url-protocol-in-windows
// https://www.youtube.com/watch?v=bJw0eGfM1ZU
// https://github.com/r-aghaei/UrlSchemeSample
// https://github.com/madskristensen/ProtocolHandlerSample