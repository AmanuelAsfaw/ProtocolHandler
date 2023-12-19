using ESC_POS_USB_NET.Printer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    public partial class Albet : Form
    {
        private string[] argsList;
        private string str_text = "";
        public bool isShow = false;
        public Albet(string[] args = null)
        {
            if(args != null)
            {
                argsList = args;
            }
            else
            {
                argsList= new string[0];
            }
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public  void changeText(string text)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AlbetSettings.Default.printer_name = textBox1.Text.ToString();
            AlbetSettings.Default.Save();
        }

        private void getButton_Click(object sender, EventArgs e)
        {
            label1.Text = AlbetSettings.Default.printer_name.ToString();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            string protocol = "albet://";
            string arg_str = "albet:type=1&game=123&ticket=12347686397486389&stack=10&minpay=123&maxpay=567&date=1700036999832&casher=user1&branch=Jemo1&odd=3.5?12&13&14&15&17";

            string params_url = arg_str.Replace(protocol, "");

            if (arg_str.Contains("type=1"))
                params_url = params_url.Replace("type=1&", "");


            MessageBox.Show(params_url);

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
                    Date = changeUnixTimeToDateTime(result[5].Replace("date=", "")).ToString(),
                    Casher = result[6].Replace("casher=", ""),
                    Branch = result[7].Replace("branch=", ""),
                    Odd = result[8].Replace("odd=", "").Replace("/", ""),
                };

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

        private void buttonRedeemTest_Click(object sender, EventArgs e)
        {
            string protocol = "albet://";
            string arg_str = "albet://type=2&casher=user1&branch=Jemo1&date=1700036999832&game=547&ticket=1234509876&stake=234&max=6000&min=10&win=300?12&13&14&15&17";

            string params_url = arg_str.Replace(protocol, "");

            if (arg_str.Contains("type=2"))
                params_url = params_url.Replace("type=2&", "");


            MessageBox.Show(params_url);

            string[] params_ = params_url.Split(Char.Parse("?"));
            string[] result = params_[0].Split(Char.Parse("&"));
            string[] selectednumbers = params_[1].Split(Char.Parse("&"));

            if (result.Length > 1)
            {
                var arguments = new
                {
                    Casher = result[0].Replace("casher=", ""),
                    Branch = result[1].Replace("branch=", ""),
                    Date = changeUnixTimeToDateTime(result[2].Replace("date=", "")).ToString(),
                    GameNumber = result[3].Replace("game=", ""),                    
                    TicketNumber = result[4].Replace("ticket=", ""),

                    Stake = result[5].Replace("stake=", ""),

                    SelectedNumbers = String.Join(" ", selectednumbers),
                    MaxPayout = result[6].Replace("maxpay=", ""),
                    MinPayout = result[7].Replace("minpay=", ""),

                    Win = result[8].Replace("win=", ""),
                };

                MessageBox.Show(arguments.Date);
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
                printer.Append("Win  :  " + arguments.Win + "     "+ "   Game  :  " + arguments.GameNumber);
                
                printer.Separator();
                printer.Append("Ticket : " + arguments.TicketNumber);
                printer.Append("STACK --- " + arguments.Stake + " ETB");

                printer.Append(arguments.SelectedNumbers);
                printer.AlignCenter();
                printer.Separator();
                printer.Append("MIN PAYOUT  ".PadRight(17) + (arguments.MinPayout + " ETB").PadLeft(17));
                printer.Append("MAX PAYOUT  ".PadRight(17) + (arguments.MaxPayout + " ETB").PadLeft(17));
                printer.Separator();
                printer.Append("Code128");
                printer.Code128(arguments.TicketNumber.ToString());
                printer.Append("Code39");
                printer.Code39(arguments.TicketNumber.ToString());
                printer.Append("Ean13");
                printer.Ean13(arguments.TicketNumber.ToString());
                printer.Append("Ean13");
                printer.Ean13(arguments.TicketNumber.ToString());
                // printer.Separator();
                // printer.Append("Under 21s are strictly forbidden!");
                // printer.Append("Terms and Conditions Apply");

                printer.FullPaperCut();
                printer.PrintDocument();
            }
        }

        private void buttonSummaryTest_Click(object sender, EventArgs e)
        {
            string protocol = "albet://";
            string arg_str = "albet://type=3&casher=user1&branch=Jemo1&date=1700036999832&startdate=1700036999832&enddate=1700036999832&bets=8760&tickets=407&redeemed=6960&balance=1800";

            string params_url = arg_str.Replace(protocol, "");

            if (arg_str.Contains("type=3"))
                params_url = params_url.Replace("type=3&", "");


            MessageBox.Show(params_url);

            string[] params_ = params_url.Split(Char.Parse("?"));
            string[] result = params_[0].Split(Char.Parse("&"));

            if (result.Length > 1)
            {
                var arguments = new
                {
                    Casher = result[0].Replace("casher=", ""),
                    Branch = result[1].Replace("branch=", ""),
                    Date = changeUnixTimeToDateTime(result[2].Replace("date=", "")).ToString(),
                    StartDate = changeUnixTimeToDateTime(result[3].Replace("date=", "")).ToString(),
                    EndDate = changeUnixTimeToDateTime(result[4].Replace("date=", "")).ToString(),
                    Bets = result[5].Replace("bets=", ""),

                    Tickets = result[6].Replace("tickets=", ""),

                    Redeemed = result[7].Replace("redeemed=", ""),
                    Balance = result[8].Replace("balance=", ""),
                };

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
                printer.Append("Tickets  ".PadRight(17) + (arguments.Tickets + " ETB").PadLeft(17));
                printer.Append("Redeemed  ".PadRight(17) + (arguments.Redeemed + " ETB").PadLeft(17));
                printer.Append("End Balance  ".PadRight(17) + (arguments.Balance + " ETB").PadLeft(17));
                
                // printer.Separator();
                // printer.Append("Under 21s are strictly forbidden!");
                // printer.Append("Terms and Conditions Apply");

                printer.FullPaperCut();
                printer.PrintDocument();
            }
        }

        public DateTime changeUnixTimeToDateTime(string time_str) {
            long unixTime = long.Parse(time_str);
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(unixTime);
            DateTime dateTime = dateTimeOffset.LocalDateTime;
            Console.WriteLine(dateTime);
            return dateTime;
        }

        private void buttonMultiRedeemTest_Click(object sender, EventArgs e)
        {
            string protocol = "albet://";
            string arg_str = "albet://type=4&casher=user1&branch=Jemo1&date=1700036999832&game=547&ticket=1234509876&stake=234&max=6000&min=10&win=300?len=3&stake=20&win=3000&24&55&67?len=4&stake=80&win=1800&4&5&7";

            string params_url = arg_str.Replace(protocol, "");

            if (arg_str.Contains("type=4"))
                params_url = params_url.Replace("type=4&", "");


            MessageBox.Show(params_url);

            string[] params_ = params_url.Split(Char.Parse("?"));
            string[] result = params_[0].Split(Char.Parse("&"));
            string[] selectednumbers = params_[1].Split(Char.Parse("&"));

            if (result.Length > 1)
            {
                var arguments = new
                {
                    Casher = result[0].Replace("casher=", ""),
                    Branch = result[1].Replace("branch=", ""),
                    Date = changeUnixTimeToDateTime(result[2].Replace("date=", "")).ToString(),
                    GameNumber = result[3].Replace("game=", ""),
                    TicketNumber = result[4].Replace("ticket=", ""),

                    Stake = result[5].Replace("stake=", ""),

                    SelectedNumbers = String.Join(" ", selectednumbers),
                    MaxPayout = result[6].Replace("maxpay=", ""),
                    MinPayout = result[7].Replace("minpay=", ""),

                    Win = result[8].Replace("win=", ""),
                };

                MessageBox.Show(arguments.Date);
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
                printer.Append("Total Win  :  " + arguments.Win + "     " + "   Game  :  " + arguments.GameNumber);

                printer.Append("Ticket : " + arguments.TicketNumber);
                for(int i = 1; i < params_.Length; i++)
                {
                    string[] selectednumbers_ = params_[i].Split(Char.Parse("&"));
                    string length_num = selectednumbers_[0].Replace("len=", "");
                    string stake_num = selectednumbers_[1].Replace("stake=", "");
                    string win_num = selectednumbers_[2].Replace("win=", "");
                    MessageBox.Show(selectednumbers_.ToString(), "selectednumbers_");
                    MessageBox.Show(length_num.ToString(), "length_num");
                    MessageBox.Show(stake_num.ToString(), "stake_num");
                    MessageBox.Show(win_num.ToString(), "win_num");
                    printer.Separator();
                    printer.Append("STACK --- " + stake_num + " ETB");
                    printer.Append("Win --- " + win_num + " ETB");
                    string selectednumbers_list = "";
                    for (int j = 3; j < selectednumbers_.Length; j++)
                    {
                        selectednumbers_list+= " "+ selectednumbers_[j].ToString();
                    }
                    printer.Append(selectednumbers_list);
                    MessageBox.Show(selectednumbers_list.ToString(), "selectednumbers_list");

                }

                printer.AlignCenter();
                printer.Separator();
                printer.Append("MIN PAYOUT  ".PadRight(17) + (arguments.MinPayout + " ETB").PadLeft(17));
                printer.Append("MAX PAYOUT  ".PadRight(17) + (arguments.MaxPayout + " ETB").PadLeft(17));
                printer.Separator();
                printer.Append("Code128");
                printer.Code128(arguments.TicketNumber.ToString());
                printer.Append("Code39");
                printer.Code39(arguments.TicketNumber.ToString());
                printer.Append("Ean13");
                printer.Ean13(arguments.TicketNumber.ToString());
                printer.Append("Ean13");
                printer.Ean13(arguments.TicketNumber.ToString());
                // printer.Separator();
                // printer.Append("Under 21s are strictly forbidden!");
                // printer.Append("Terms and Conditions Apply");

                printer.FullPaperCut();
                printer.PrintDocument();
            }
        }

        private void buttonMultiBill_Click(object sender, EventArgs e)
        {
            string protocol = "albet://";
            string arg_str = "albet:type=1&game=123&ticket=12347686397486389&stack=10&minpay=123&maxpay=567&date=1700036999832&casher=user1&branch=Jemo1&odd=3.5?len=3&stake=20&24&55&67?len=4&stake=80&4&5&7";

            string params_url = arg_str.Replace(protocol, "");

            if (arg_str.Contains("type=1"))
                params_url = params_url.Replace("type=1&", "");


            MessageBox.Show(params_url);

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
                    Date = changeUnixTimeToDateTime(result[5].Replace("date=", "")).ToString(),
                    Casher = result[6].Replace("casher=", ""),
                    Branch = result[7].Replace("branch=", ""),
                    Odd = result[8].Replace("odd=", "").Replace("/", ""),
                };

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
                printer.Append("Total STAKE --- " + arguments.Stack + " ETB");
                printer.AlignCenter();
                printer.Separator();
                for (int i = 1; i < params_.Length; i++)
                {
                    string[] selectednumbers_ = params_[i].Split(Char.Parse("&"));
                    string length_num = selectednumbers_[0].Replace("len=", "");
                    string stake_num = selectednumbers_[1].Replace("stake=", "");
                    MessageBox.Show(selectednumbers_.ToString(), "selectednumbers_");
                    MessageBox.Show(length_num.ToString(), "length_num");
                    MessageBox.Show(stake_num.ToString(), "stake_num");
                    printer.Separator();
                    printer.Append("STACK --- " + stake_num + " ETB");
                    string selectednumbers_list = "";
                    for (int j = 2; j < selectednumbers_.Length; j++)
                    {
                        selectednumbers_list += " " + selectednumbers_[j].ToString();
                    }
                    printer.Append(selectednumbers_list);
                    MessageBox.Show(selectednumbers_list.ToString(), "selectednumbers_list");

                }
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

        private void buttonEscPosPrinter_Click(object sender, EventArgs e)
        {
            // var printer = new SerialPrinter(portName: "COM5", baudRate: 115200);
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = "Your USB Printer Name Here";

        }
    }
    
}
