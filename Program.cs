using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace FileSize0001 
{
    class Program
    {
        // | STAThread is necessary for System.Windows.Clipboard services to work.
        [STAThread]
        static void Main(string[] args)
        {
            new Program().Run();
        }

        // | used for state logging, body commented out on release.
        private void log(in string s) {
            // Console.WriteLine(s);
        }

        // | All the work delegated by main happens here, we log our state while developing.
        // | The logs do nothing on release, which is inefficient, but serve dual purpose as comments
        // |    with some runtime overhead.
        // | Our program consists of three states:
        // |     0. entry 
        // |     1. after obtaining command line arguments
        // |     2. end state(after setting the clipboard to the file size)
        // | If there is a failure at any time, main will return an error code for the state(25 + state number). 
        // | Argument is provided as a test feature, we do not use it as it forces Main to do argument checking, 
        // |     which is undesireable.
        public void Run(string strTestPath=null) 
        {
            string strCurrentFilePath = strTestPath;
            log("state 0: entry state");
            
            // | if no path is given, get it from the environment.
            if (strCurrentFilePath == null) {
                try {
                    strCurrentFilePath = Environment.GetCommandLineArgs()[1];
                } catch (Exception) {
                    Environment.Exit(25);
                }
            }

            log("state 1: got path(" + strCurrentFilePath + ")");
                        
            try {
                System.Windows.Clipboard.SetText(strCurrentFilePath);
            } catch (Exception) {
                Environment.Exit(26);
            }
            log("state 2: end state, attempting to set clipboard text to " + strCurrentFilePath + " did not end our process.");
        }
    }
}
