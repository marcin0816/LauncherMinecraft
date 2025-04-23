using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WindowsFormsApp1
{
    public class LauncherConfig
    {
        // Właściwości konfiguracji
        public string Username { get; set; } = "";
        public string JavaPath { get; set; } = "";
        public bool UseCustomJavaPath { get; set; } = false;
        public int MemoryAllocation { get; set; } = 2;
        public int SelectedLanguage { get; set; } = 0;
        public string LastSelectedVersion { get; set; } = "";

        // Ścieżka do folderu i pliku konfiguracyjnego
        private static string LauncherDir => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "MCLauncher");

        private static string ConfigFile => Path.Combine(LauncherDir, "launcher_config.json");

        // Zapisz konfigurację do pliku
        public static bool SaveConfig(LauncherConfig config)
        {
            try
            {
                // Upewnij się, że katalog istnieje
                if (!Directory.Exists(LauncherDir))
                {
                    Directory.CreateDirectory(LauncherDir);
                }

                // Serializuj obiekt konfiguracji do JSON z formatowaniem
                string json = JsonConvert.SerializeObject(config, Formatting.Indented);

                // Zapisz plik
                File.WriteAllText(ConfigFile, json);

                // Zapisz informacje do dziennika i pokaż MessageBox o powodzeniu
                Console.WriteLine($"Konfiguracja zapisana pomyślnie w {ConfigFile}");
                return true;
            }
            catch (Exception ex)
            {
                // Zapisz informacje o błędzie do dziennika i pokaż MessageBox o błędzie
                Console.WriteLine($"Błąd podczas zapisywania konfiguracji: {ex.Message}");
                // MessageBox.Show($"Nie można zapisać konfiguracji: {ex.Message}", "Błąd konfiguracji", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Załaduj konfigurację z pliku
        public static LauncherConfig LoadConfig()
        {
            try
            {
                // Sprawdź czy plik istnieje
                if (File.Exists(ConfigFile))
                {
                    // Odczytaj zawartość pliku
                    string json = File.ReadAllText(ConfigFile);
                    Console.WriteLine($"Odczytana zawartość konfiguracji: {json}");

                    // Deserializuj JSON do obiektu
                    var config = JsonConvert.DeserializeObject<LauncherConfig>(json);

                    if (config != null)
                    {
                        Console.WriteLine($"Wczytano konfigurację: Username={config.Username}, JavaPath={config.JavaPath}, Memory={config.MemoryAllocation}");
                        return config;
                    }
                }
                else
                {
                    Console.WriteLine($"Plik konfiguracyjny nie istnieje: {ConfigFile}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas wczytywania konfiguracji: {ex.Message}");
                // MessageBox.Show($"Nie można wczytać konfiguracji: {ex.Message}", "Błąd konfiguracji", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Jeśli nie udało się wczytać, utwórz nową konfigurację
            Console.WriteLine("Tworzenie nowej konfiguracji");
            return new LauncherConfig();
        }
    }
}