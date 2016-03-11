using System;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RansomwareDetection
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new RansomwareDetectionSystemTray());
            bool createdNew = false;
            Mutex mutex = null;
            try
            {
                mutex = new Mutex(true, "RansomwareDetectionSystemTray", out createdNew);
            }
            catch
            {
            }
            if (mutex == null || !createdNew)
            {
                MessageBox.Show("Another instance of RansomwareDetectionSystemTray is already running.", "Cannot start RansomwareDetectionSystemTray", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Application.Run(new RansomwareDetectionSystemTray());
            }
            finally
            {
                mutex.Close();
            }
        }
    }
}
