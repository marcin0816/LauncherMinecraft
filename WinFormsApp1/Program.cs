using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// G³ówny punkt wejœcia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // Przekierowanie Console.WriteLine do pliku dziennika
                SetupLogging();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Console.WriteLine("Uruchamianie MCLauncher...");

                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Krytyczny b³¹d: {ex.Message}\n\n{ex.StackTrace}",
                    "B³¹d aplikacji", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void SetupLogging()
        {
            try
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string logDir = Path.Combine(appDataPath, "MCLauncher", "logs");

                if (!Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }

                string logFile = Path.Combine(logDir, $"launcher-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.log");
                TextWriter writer = new StreamWriter(logFile, true);
                Console.SetOut(writer);
                Console.SetError(writer);

                Console.WriteLine($"--- MCLauncher Log {DateTime.Now} ---");
            }
            catch
            {
                // Jeœli nie uda siê skonfigurowaæ dziennika, po prostu kontynuuj
            }
        }
    }
}