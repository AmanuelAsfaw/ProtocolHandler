using Microsoft.Win32;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

                //Form2 form2 = new Form2();
                string arg_str = "";
                foreach (var arg in args)
                {
                    arg_str = arg_str + arg;
                    Console.WriteLine($"Argument received: {arg}");
                    // Perform actions based on the arguments received
                    // Add your custom protocol handling logic here
                }
                // arg_str = "albetprtcl:game=123&ticket=12347686397486389&stack=10&minpay=123&maxpay=567&date=Jan 12 2022&casher=user1&branch=Jemo1&odd=3.5?12&13&14&15&17";
                
                // for bill print
                if (arg_str.Contains("type=1")) {

                    string params_url = arg_str.Replace(protocol, "");

                    if (arg_str.Contains("type=1"))
                        params_url = params_url.Replace("type=1&", "");


                    string[] params_ = params_url.Split(Char.Parse("?"));
                    string[] result = params_[0].Split(Char.Parse("&"));
                    string[] selectednumbers = params_[1].Split(Char.Parse("&"));

                    // form2.ReceiveValue("GameNumber", arg_str);
                    // Application.Run(form2);

                    if (result.Length > 1)
                    {
                        var arguments = new
                        {
                            GameNumber = result[0].Replace("game=", "").Replace("%20", " "),
                            TicketNumber = result[1].Replace("ticket=", "").Replace("%20", " "),
                            Stack = result[2].Replace("stack=", ""),
                            SelectedNumbers = String.Join(" ", selectednumbers),
                            MinPayout = result[3].Replace("minpay=", ""),
                            MaxPayout = result[4].Replace("maxpay=", ""),
                            Date = changeUnixTimeToDateTime(result[5].Replace("date=", "")).ToString(),
                            Casher = result[6].Replace("casher=", "").Replace("%20", " "),
                            Branch = result[7].Replace("branch=", "").Replace("%20", " "),
                            Odd = result[8].Replace("odd=", "").Replace("/", ""),
                        };

                        /*
                        MessageBox.Show("game = " + arguments.GameNumber);
                        MessageBox.Show("ticket = " + arguments.TicketNumber);
                        MessageBox.Show("stack = " + arguments.Stack);
                        MessageBox.Show("minpay = " + arguments.MinPayout);
                        MessageBox.Show("maxpay = " + arguments.MaxPayout);
                        MessageBox.Show("date = " + arguments.Date); ;
                        MessageBox.Show("casher = " + arguments.Casher);
                        MessageBox.Show("branch = " + arguments.Branch);
                        MessageBox.Show("odd = " + arguments.Odd);
                        MessageBox.Show("selectednumbers = " + arguments.SelectedNumbers);
                        */

                        string printer_name = AlbetSettings.Default.printer_name.ToString();
                        // printer_name = "POS-80"; // XP-80C
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
                        printer.Append("STAKE --- " + arguments.Stack + " ETB");
                        printer.Append("MIN PAYOUT  ".PadRight(17) + (arguments.MinPayout + " ETB").PadLeft(17));
                        printer.Append("MAX PAYOUT  ".PadRight(17) + (arguments.MaxPayout + " ETB").PadLeft(17));
                        printer.Separator();
                        printer.Code128(arguments.TicketNumber.ToString());
                        MessageBox.Show("ticket = " + arguments.TicketNumber.ToString());
                        printer.Code39(arguments.TicketNumber.ToString());
                        printer.Ean13(arguments.TicketNumber.ToString());
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

                // for redeem print
                if (arg_str.Contains("type=2")) {

                    string params_url = arg_str.Replace(protocol, "");

                    if (arg_str.Contains("type=2"))
                        params_url = params_url.Replace("type=2&", "");

                    string[] params_ = params_url.Split(Char.Parse("?"));
                    string[] result = params_[0].Split(Char.Parse("&"));
                    string[] selectednumbers = params_[1].Split(Char.Parse("&"));

                    if (result.Length > 1)
                    {
                        var arguments = new
                        {
                            Casher = result[0].Replace("casher=", "").Replace("%20", " "),
                            Branch = result[1].Replace("branch=", "").Replace("%20", " "),
                            Date = changeUnixTimeToDateTime(result[2].Replace("date=", "")).ToString(),
                            GameNumber = result[3].Replace("game=", ""),
                            TicketNumber = result[4].Replace("ticket=", ""),

                            Stake = result[5].Replace("stake=", ""),

                            SelectedNumbers = String.Join(" ", selectednumbers),
                            MaxPayout = result[6].Replace("max=", ""),
                            MinPayout = result[7].Replace("min=", ""),

                            Win = result[8].Replace("win=", ""),
                        };

                        /*
                        MessageBox.Show("casher = " + arguments.Casher);
                        MessageBox.Show("branch = " + arguments.Branch);
                        MessageBox.Show("date = " + arguments.Date);
                        MessageBox.Show("game = " + arguments.GameNumber);
                        MessageBox.Show("ticket = " + arguments.TicketNumber);
                        MessageBox.Show("stake = " + arguments.Stake); ;
                        MessageBox.Show("maxpay = " + arguments.MaxPayout);
                        MessageBox.Show("minpay = " + arguments.MinPayout);
                        MessageBox.Show("win = " + arguments.Win);
                        MessageBox.Show("selectednumbers = " + arguments.SelectedNumbers);
                        */

                        string printer_name = AlbetSettings.Default.printer_name.ToString();
                        // printer_name = "POS-80"; // XP-80C
                                                 // Printer printer = new Printer("POS Printer 80250 Series", "UTF-8");
                        Printer printer = new Printer(printer_name, "UTF-8");

                        printer.AlignLeft();
                        // printer.DoubleWidth3();
                        // printer.BoldMode("ALbet");
                        printer.NormalWidth();
                        //printer.Image(image);
                        printer.AlignRight();
                        printer.Append("Branch : " + arguments.Branch);
                        printer.Append("Casher : " + arguments.Casher);
                        printer.Append(arguments.Date);
                        printer.AlignLeft();
                        printer.Append("Win  :  " + arguments.Win + "ETB     " + "   Game  :  " + arguments.GameNumber);

                        printer.Separator();
                        printer.Append("Ticket : " + arguments.TicketNumber);
                        printer.Append("STAKE --- " + arguments.Stake + " ETB");

                        printer.Append(arguments.SelectedNumbers);
                        printer.AlignCenter();
                        printer.Separator();
                        printer.Append("MIN PAYOUT  ".PadRight(17) + (arguments.MinPayout + " ETB").PadLeft(17));
                        printer.Append("MAX PAYOUT  ".PadRight(17) + (arguments.MaxPayout + " ETB").PadLeft(17));
                        printer.Separator();
                        printer.Code128(arguments.TicketNumber.ToString());
                        printer.Code39(arguments.TicketNumber.ToString());
                        printer.Ean13(arguments.TicketNumber.ToString());
                        // printer.Separator();
                        // printer.Append("Under 21s are strictly forbidden!");
                        // printer.Append("Terms and Conditions Apply");

                        printer.FullPaperCut();
                        printer.PrintDocument();
                    }
                }

                // for summary print
                if (arg_str.Contains("type=3")) {

                    string params_url = arg_str.Replace(protocol, "");

                    if (arg_str.Contains("type=3"))
                        params_url = params_url.Replace("type=3&", "");

                    string[] params_ = params_url.Split(Char.Parse("?"));
                    string[] result = params_[0].Split(Char.Parse("&"));

                    if (result.Length > 1)
                    {
                        var arguments = new
                        {
                            Casher = result[0].Replace("casher=", "").Replace("%20", " "),
                            Branch = result[1].Replace("branch=", "").Replace("%20"," "),
                            Date = changeUnixTimeToDateTime(result[2].Replace("date=", "")).ToString(),
                            StartDate = changeUnixTimeToDateTime(result[3].Replace("startdate=", "")).ToString(),
                            EndDate = changeUnixTimeToDateTime(result[4].Replace("enddate=", "")).ToString(),
                            Bets = result[5].Replace("bets=", ""),

                            Tickets = result[6].Replace("tickets=", ""),

                            Redeemed = result[7].Replace("redeemed=", ""),
                            Balance = result[8].Replace("balance=", "").Replace("/",""),
                        };

                        /*
                        MessageBox.Show("casher = " + arguments.Casher);
                        MessageBox.Show("branch = " + arguments.Branch);
                        MessageBox.Show("date = " + arguments.Date);
                        MessageBox.Show("startdate = " + arguments.StartDate);
                        MessageBox.Show("enddate = " + arguments.EndDate);
                        MessageBox.Show("bets = " + arguments.Bets); ;
                        MessageBox.Show("tickets = " + arguments.Tickets);
                        MessageBox.Show("redeemed = " + arguments.Redeemed);
                        MessageBox.Show("balance = " + arguments.Balance);
                        */

                        string printer_name = AlbetSettings.Default.printer_name.ToString();
                        // printer_name = "POS-80"; // XP-80C
                        // Printer printer = new Printer("POS Printer 80250 Series", "UTF-8");
                        Printer printer = new Printer(printer_name, "UTF-8");

                        //printer.AlignLeft();
                        // printer.DoubleWidth3();
                        // printer.BoldMode("ALbet");
                        //printer.NormalWidth();
                        //printer.Image(image);
                        printer.AlignRight();
                        printer.Append("Branch : " + arguments.Branch);
                        printer.Append("Casher : " + arguments.Casher);
                        printer.Append(arguments.Date);
                        printer.AlignCenter();
                        printer.Append("Summary");
                        printer.Append("     " + arguments.StartDate + "   -   " + arguments.EndDate);

                        printer.AlignCenter();
                        printer.Append("Bets  ".PadRight(17) + (arguments.Bets + " ETB").PadLeft(17));
                        printer.Append("Tickets  ".PadRight(17) + arguments.Tickets.ToString().PadLeft(17));
                        printer.Append("Redeemed  ".PadRight(17) + (arguments.Redeemed + " ETB").PadLeft(17));
                        printer.Append("End Balance  ".PadRight(17) + (arguments.Balance + " ETB").PadLeft(17));

                        // printer.Separator();
                        // printer.Append("Under 21s are strictly forbidden!");
                        // printer.Append("Terms and Conditions Apply");

                        printer.FullPaperCut();
                        printer.PrintDocument();
                    }
                }

                // for redeem print for multiple bill
                if (arg_str.Contains("type=4"))
                {

                    string params_url = arg_str.Replace(protocol, "");

                    if (arg_str.Contains("type=4"))
                        params_url = params_url.Replace("type=4&", "");

                    string[] params_ = params_url.Split(Char.Parse("?"));
                    string[] result = params_[0].Split(Char.Parse("&"));
                    string[] selectednumbers = params_[1].Split(Char.Parse("&"));

                    if (result.Length > 1)
                    {
                        var arguments = new
                        {
                            Casher = result[0].Replace("casher=", "").Replace("%20", " "),
                            Branch = result[1].Replace("branch=", "").Replace("%20", " "),
                            Date = changeUnixTimeToDateTime(result[2].Replace("date=", "")).ToString(),
                            GameNumber = result[3].Replace("game=", ""),
                            TicketNumber = result[4].Replace("ticket=", ""),

                            Stake = result[5].Replace("stake=", ""),

                            SelectedNumbers = String.Join(" ", selectednumbers),
                            MaxPayout = result[6].Replace("max=", ""),
                            MinPayout = result[7].Replace("min=", ""),

                            Win = result[8].Replace("win=", ""),
                        };

                        /*
                        MessageBox.Show("casher = " + arguments.Casher);
                        MessageBox.Show("branch = " + arguments.Branch);
                        MessageBox.Show("date = " + arguments.Date);
                        MessageBox.Show("game = " + arguments.GameNumber);
                        MessageBox.Show("ticket = " + arguments.TicketNumber);
                        MessageBox.Show("stake = " + arguments.Stake); ;
                        MessageBox.Show("maxpay = " + arguments.MaxPayout);
                        MessageBox.Show("minpay = " + arguments.MinPayout);
                        MessageBox.Show("win = " + arguments.Win);
                        MessageBox.Show("selectednumbers = " + arguments.SelectedNumbers);
                        */

                        string printer_name = AlbetSettings.Default.printer_name.ToString();
                        // printer_name = "POS-80"; // XP-80C
                        // Printer printer = new Printer("POS Printer 80250 Series", "UTF-8");
                        Printer printer = new Printer(printer_name, "UTF-8");

                        printer.AlignLeft();
                        // printer.DoubleWidth3();
                        // printer.BoldMode("ALbet");
                        printer.NormalWidth();
                        //printer.Image(image);
                        printer.AlignRight();
                        printer.Append("Branch : " + arguments.Branch);
                        printer.Append("Casher : " + arguments.Casher);
                        printer.Append(arguments.Date);
                        printer.AlignLeft();
                        printer.Append("Total Win  :  " + arguments.Win + "ETB     " + "   Game  :  " + arguments.GameNumber);

                        printer.Append("Ticket : " + arguments.TicketNumber);
                        printer.Append("Total STAKE --- " + arguments.Stake + " ETB");
                        for (int i = 1; i < params_.Length; i++)
                        {
                            string[] selectednumbers_ = params_[i].Split(Char.Parse("&"));
                            string length_num = selectednumbers_[0].Replace("len=", "");
                            string stake_num = selectednumbers_[1].Replace("stake=", "");
                            string win_num = selectednumbers_[2].Replace("win=", "");
                            /*
                            MessageBox.Show(selectednumbers_.ToString(), "selectednumbers_");
                            MessageBox.Show(length_num.ToString(), "length_num");
                            MessageBox.Show(stake_num.ToString(), "stake_num");
                            MessageBox.Show(win_num.ToString(), "win_num");
                            */
                            printer.Separator();
                            printer.Append("STACK --- " + stake_num + " ETB");
                            printer.Append("Win --- " + win_num + " ETB");
                            string selectednumbers_list = "";
                            for (int j = 3; j < selectednumbers_.Length; j++)
                            {
                                selectednumbers_list += " " + selectednumbers_[j].ToString();
                            }
                            printer.Append(selectednumbers_list);
                            //MessageBox.Show(selectednumbers_list.ToString(), "selectednumbers_list");

                        }

                        printer.AlignCenter();
                        printer.Separator();
                        printer.Append("MIN PAYOUT  ".PadRight(17) + (arguments.MinPayout + " ETB").PadLeft(17));
                        printer.Append("MAX PAYOUT  ".PadRight(17) + (arguments.MaxPayout + " ETB").PadLeft(17));
                        printer.Separator();
                        printer.Code128(arguments.TicketNumber.ToString());
                        // MessageBox.Show("ticket = " + arguments.TicketNumber);
                        printer.Code39(arguments.TicketNumber.ToString());
                        printer.Ean13(arguments.TicketNumber.ToString());
                        // printer.Separator();
                        // printer.Append("Under 21s are strictly forbidden!");
                        // printer.Append("Terms and Conditions Apply");

                        printer.FullPaperCut();
                        printer.PrintDocument();
                    }
                }

                // for multiple bill print
                if (arg_str.Contains("type=5"))
                {

                    string params_url = arg_str.Replace(protocol, "");

                    if (arg_str.Contains("type=5"))
                        params_url = params_url.Replace("type=5&", "");


                    string[] params_ = params_url.Split(Char.Parse("?"));
                    string[] result = params_[0].Split(Char.Parse("&"));
                    string[] selectednumbers = params_[1].Split(Char.Parse("&"));

                    // form2.ReceiveValue("GameNumber", arg_str);
                    // Application.Run(form2);

                    if (result.Length > 1)
                    {
                        var arguments = new
                        {
                            GameNumber = result[0].Replace("game=", "").Replace("%20", " "),
                            TicketNumber = result[1].Replace("ticket=", "").Replace("%20", " "),
                            Stack = result[2].Replace("stack=", ""),
                            SelectedNumbers = String.Join(" ", selectednumbers),
                            MinPayout = result[3].Replace("minpay=", ""),
                            MaxPayout = result[4].Replace("maxpay=", ""),
                            Date = changeUnixTimeToDateTime(result[5].Replace("date=", "")).ToString(),
                            Casher = result[6].Replace("casher=", "").Replace("%20", " "),
                            Branch = result[7].Replace("branch=", "").Replace("%20", " "),
                            Odd = result[8].Replace("odd=", "").Replace("/", ""),
                        };

                        /*
                        MessageBox.Show("game = " + arguments.GameNumber);
                        MessageBox.Show("ticket = " + arguments.TicketNumber);
                        MessageBox.Show("stack = " + arguments.Stack);
                        MessageBox.Show("minpay = " + arguments.MinPayout);
                        MessageBox.Show("maxpay = " + arguments.MaxPayout);
                        MessageBox.Show("date = " + arguments.Date); ;
                        MessageBox.Show("casher = " + arguments.Casher);
                        MessageBox.Show("branch = " + arguments.Branch);
                        MessageBox.Show("odd = " + arguments.Odd);
                        MessageBox.Show("selectednumbers = " + arguments.SelectedNumbers);
                        */

                        string printer_name = AlbetSettings.Default.printer_name.ToString();
                        // printer_name = "POS-80"; // XP-80C
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
                        printer.Append("STAKE --- " + arguments.Stack + " ETB");
                        printer.AlignCenter();
                        printer.Separator();
                        for (int i = 1; i < params_.Length; i++)
                        {
                            string[] selectednumbers_ = params_[i].Split(Char.Parse("&"));
                            string length_num = selectednumbers_[0].Replace("len=", "");
                            string stake_num = selectednumbers_[1].Replace("stake=", "");
                            /*
                            MessageBox.Show(selectednumbers_.ToString(), "selectednumbers_");
                            MessageBox.Show(length_num.ToString(), "length_num");
                            MessageBox.Show(stake_num.ToString(), "stake_num");
                            */
                            printer.Separator();
                            printer.Append("STAKE --- ".PadRight(17) + (stake_num + " ETB").PadLeft(17));
                            string selectednumbers_list = "";
                            for (int j = 2; j < selectednumbers_.Length; j++)
                            {
                                selectednumbers_list += " " + selectednumbers_[j].ToString();
                            }
                            printer.Append(selectednumbers_list);
                            //MessageBox.Show(selectednumbers_list.ToString(), "selectednumbers_list");

                        }
                        printer.Append("MIN PAYOUT  ".PadRight(17) + (arguments.MinPayout + " ETB").PadLeft(17));
                        printer.Append("MAX PAYOUT  ".PadRight(17) + (arguments.MaxPayout + " ETB").PadLeft(17));
                        printer.Separator();
                        printer.Code128(arguments.TicketNumber.ToString());
                        // MessageBox.Show("ticket = " + arguments.TicketNumber);
                        printer.Code39(arguments.TicketNumber.ToString());
                        printer.Ean13(arguments.TicketNumber.ToString());
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

                // for Game Draws print
                if (arg_str.Contains("type=6"))
                {

                    string params_url = arg_str.Replace(protocol, "");

                    if (arg_str.Contains("type=6"))
                        params_url = params_url.Replace("type=6&", "");

                    string[] params_ = params_url.Split(Char.Parse("?"));
                    string[] result = params_[0].Split(Char.Parse("&"));
                    string[] draws = params_[1].Split(Char.Parse("&"));
                    /*
                    MessageBox.Show("result = " + result);
                    MessageBox.Show("draws = " + draws);
                    MessageBox.Show("Date = " + result[1].Replace("date=", ""));
                    MessageBox.Show("Time = " + result[4].Replace("time=", ""));
                    */
                    if (result.Length > 1)
                    {
                        var arguments = new
                        {
                            Game = result[0].Replace("game=", "").Replace("%20", " "),
                            Date = changeUnixTimeToDateTime(result[1].Replace("date=", "").Replace("/", "")).ToString(),
                            Casher = result[2].Replace("casher=", "").Replace("%20", " "),
                            Branch = result[3].Replace("branch=", "").Replace("%20", " "),
                            Time = changeUnixTimeToDateTime(result[4].Replace("time=", "").Replace("/", "")).ToString(),
                            
                        };

                        /*
                        MessageBox.Show("casher = " + arguments.Casher);
                        MessageBox.Show("branch = " + arguments.Branch);
                        MessageBox.Show("date = " + arguments.Date);
                        MessageBox.Show("time = " + arguments.Time);
                        MessageBox.Show("draws = " + draws.ToString());
                        MessageBox.Show("bets = " + arguments.Bets); ;
                        MessageBox.Show("tickets = " + arguments.Tickets);
                        MessageBox.Show("redeemed = " + arguments.Redeemed);
                        MessageBox.Show("balance = " + arguments.Balance);
                        */

                        string printer_name = AlbetSettings.Default.printer_name.ToString();
                        // printer_name = "POS-80"; // XP-80C
                        // Printer printer = new Printer("POS Printer 80250 Series", "UTF-8");
                        Printer printer = new Printer(printer_name, "UTF-8");

                        //printer.AlignLeft();
                        // printer.DoubleWidth3();
                        // printer.BoldMode("ALbet");
                        //printer.NormalWidth();
                        //printer.Image(image);
                        printer.AlignRight();
                        printer.Append("Branch : " + arguments.Branch);
                        printer.Append("Casher : " + arguments.Casher);
                        printer.Append(arguments.Date);
                        printer.AlignCenter();
                        printer.Append("Event Result");

                        printer.AlignCenter();
                        printer.Append("Game Id  ".PadRight(17) + (arguments.Game).PadLeft(17));
                        printer.Append("Time  ".PadRight(17) + (arguments.Time).PadLeft(17));
                        printer.AlignRight();
                        printer.Append(draws.ToString());
                        string print_str = "";
                        for (int j = 0; j < draws.Length; j++)
                        {
                            print_str += " " + draws[j].ToString();
                        }
                        printer.AlignLeft();
                        printer.Append(print_str);
                        // printer.Separator();
                        // printer.Append("Under 21s are strictly forbidden!");
                        // printer.Append("Terms and Conditions Apply");

                        printer.FullPaperCut();
                        printer.PrintDocument();
                    }
                }

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

        static DateTime changeUnixTimeToDateTime(string time_str)
        {
            long unixTime = long.Parse(time_str);
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(unixTime);
            DateTime dateTime = dateTimeOffset.LocalDateTime;
            Console.WriteLine(dateTime);
            return dateTime;
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