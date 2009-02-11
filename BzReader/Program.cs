using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace BzReader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CultureInfo formattingCulture = Thread.CurrentThread.CurrentCulture;
            CultureInfo uiCulture = Thread.CurrentThread.CurrentUICulture;

            // A lot of people use English Windows even though their native language is different

            if (formattingCulture != uiCulture)
            {
                Thread.CurrentThread.CurrentUICulture = formattingCulture;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BrowseForm());
        }
    }
}