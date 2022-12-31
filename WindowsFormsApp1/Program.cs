using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            //RegisterMyProtocol("");
            Application.SetCompatibleTextRenderingDefault(false);

            string[] args = Environment.GetCommandLineArgs();
            Application.Run(new Form1(args));
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