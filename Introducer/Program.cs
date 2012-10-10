using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Introducer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Something bad happened, and this is the final try { } catch { } block to catch it: \n\n" + ex.ToString(),
                    "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
