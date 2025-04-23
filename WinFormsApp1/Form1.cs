using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private const string VERSIONS_URL = "https://piston-meta.mojang.com/mc/game/version_manifest.json";
        private readonly string LAUNCHER_DIR;
        private readonly HttpClient httpClient = new HttpClient();
        private bool isDownloading = false;

        // Przechowywanie konfiguracji jako pole klasy
        private LauncherConfig config;

        public Form1()
        {
            try
            {
                InitializeComponent();

                // Inicjalizuj œcie¿kê launchera
                LAUNCHER_DIR = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MCLauncher");

                SetupDirectories();

                // Przygotuj podstawow¹ konfiguracjê formularza
                SetupInitialFormState();

                // Wczytaj konfiguracjê
                config = LauncherConfig.LoadConfig();
                Console.WriteLine("Konfiguracja wczytana w konstruktorze");

                // Zastosuj konfiguracjê do kontrolek
                ApplyConfigToControls();

                // £aduj dostêpne wersje
                LoadVersionsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"B³¹d podczas inicjalizacji launchera: {ex.Message}\n\n{ex.StackTrace}",
                    "B³¹d inicjalizacji", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupInitialFormState()
        {
            // Skonfiguruj suwak pamiêci (przed wczytaniem konfiguracji)
            memorySlider.Minimum = 1;
            memorySlider.Maximum = 8;
            memorySlider.Value = 2;
            memorySlider.TickFrequency = 1;
            memoryLabel.Text = $"{memorySlider.Value} GB";

            // Dodaj opcje jêzyka
            languageComboBox.Items.Add("English");
            languageComboBox.Items.Add("Polski");
            languageComboBox.SelectedIndex = 0;

            // Pod³¹cz zdarzenia (po wczytaniu konfiguracji zostan¹ wywo³ane)
            usernameTextBox.TextChanged += usernameTextBox_TextChanged;
            memorySlider.ValueChanged += memorySlider_ValueChanged;
            customJavaPathCheckBox.CheckedChanged += customJavaPathCheckBox_CheckedChanged;
        }

        private void ApplyConfigToControls()
        {
            try
            {
                Console.WriteLine("Rozpoczêcie aplikowania konfiguracji do kontrolek");

                // Ustaw nazwê u¿ytkownika
                if (!string.IsNullOrEmpty(config.Username))
                {
                    // Od³¹cz zdarzenia tymczasowo
                    usernameTextBox.TextChanged -= usernameTextBox_TextChanged;
                    usernameTextBox.Text = config.Username;
                    usernameTextBox.TextChanged += usernameTextBox_TextChanged;
                    Console.WriteLine($"Ustawiono nazwê u¿ytkownika: {config.Username}");
                }

                // Ustaw jêzyk
                if (config.SelectedLanguage >= 0 && config.SelectedLanguage < languageComboBox.Items.Count)
                {
                    languageComboBox.SelectedIndex = config.SelectedLanguage;
                    Console.WriteLine($"Ustawiono jêzyk: {config.SelectedLanguage}");
                }

                // Ustaw przydzia³ pamiêci
                if (config.MemoryAllocation >= memorySlider.Minimum && config.MemoryAllocation <= memorySlider.Maximum)
                {
                    // Od³¹cz zdarzenie tymczasowo
                    memorySlider.ValueChanged -= memorySlider_ValueChanged;
                    memorySlider.Value = config.MemoryAllocation;
                    memoryLabel.Text = $"{memorySlider.Value} GB";
                    memorySlider.ValueChanged += memorySlider_ValueChanged;
                    Console.WriteLine($"Ustawiono przydzia³ pamiêci: {config.MemoryAllocation} GB");
                }

                // Ustaw œcie¿kê Java
                if (!string.IsNullOrEmpty(config.JavaPath))
                {
                    customJavaPathCheckBox.CheckedChanged -= customJavaPathCheckBox_CheckedChanged;
                    customJavaPathCheckBox.Checked = config.UseCustomJavaPath;
                    javaPathTextBox.Text = config.JavaPath;
                    javaPathTextBox.Enabled = config.UseCustomJavaPath;
                    customJavaPathCheckBox.CheckedChanged += customJavaPathCheckBox_CheckedChanged;
                    Console.WriteLine($"Ustawiono œcie¿kê Java: {config.JavaPath} (w³asna: {config.UseCustomJavaPath})");
                }
                else
                {
                    // Jeœli brak œcie¿ki w konfiguracji, spróbuj j¹ znaleŸæ
                    string foundPath = FindJavaPath();
                    if (!string.IsNullOrEmpty(foundPath))
                    {
                        javaPathTextBox.Text = foundPath;
                        Console.WriteLine($"Znaleziono œcie¿kê Java: {foundPath}");
                    }
                    else
                    {
                        javaPathTextBox.Text = "java";
                        Console.WriteLine("Nie znaleziono œcie¿ki Java, ustawiono domyœln¹ 'java'");
                    }
                }

                // Aktualizuj UI zgodnie z wybranym jêzykiem
                UpdateUILanguage();

                Console.WriteLine("Zakoñczono aplikowanie konfiguracji do kontrolek");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"B³¹d podczas aplikowania konfiguracji: {ex.Message}");
            }
        }

        private void UpdateUILanguage()
        {
            switch (languageComboBox.SelectedIndex)
            {
                case 0: // English
                    this.Text = "Minecraft Non-Premium Launcher";
                    languageLabel.Text = "Select language:";
                    usernameLabel.Text = "Username:";
                    versionLabel.Text = "Select Minecraft version:";
                    customJavaPathCheckBox.Text = "Set custom Java path";
                    javaPathLabel.Text = "Java path:";
                    memoryGroupBox.Text = "Memory allocation:";
                    statusLabel.Text = "Ready.";
                    break;

                case 1: // Polish
                    this.Text = "Minecraft Launcher - Nieoficjalny";
                    languageLabel.Text = "Wybierz jêzyk:";
                    usernameLabel.Text = "Nazwa u¿ytkownika:";
                    versionLabel.Text = "Wybierz wersjê Minecrafta:";
                    customJavaPathCheckBox.Text = "U¿yj w³asnej œcie¿ki do Java";
                    javaPathLabel.Text = "Œcie¿ka do Java:";
                    memoryGroupBox.Text = "Przydzia³ pamiêci RAM:";
                    statusLabel.Text = "Gotowy.";
                    break;
            }
        }

        // Metoda zapisuj¹ca konfiguracjê ze wszystkimi aktualnymi ustawieniami
        private void SaveCurrentConfig()
        {
            try
            {
                // SprawdŸ czy config jest null i zainicjalizuj go jeœli potrzeba
                if (config == null)
                {
                    config = new LauncherConfig();
                    Console.WriteLine("Utworzono nowy obiekt konfiguracji, poniewa¿ poprzedni by³ null");
                }

                // Zapisz wszystkie wartoœci z kontrolek
                config.Username = usernameTextBox.Text;
                config.JavaPath = javaPathTextBox.Text;
                config.UseCustomJavaPath = customJavaPathCheckBox.Checked;
                config.MemoryAllocation = memorySlider.Value;
                config.SelectedLanguage = languageComboBox.SelectedIndex;
                config.LastSelectedVersion = versionListBox.SelectedItem?.ToString() ?? "";

                // Reszta metody pozostaje bez zmian...
            }
            catch (Exception ex)
            {
                Console.WriteLine($"B³¹d podczas zapisywania konfiguracji: {ex.Message}");
            }
        }

        private void SetupDirectories()
        {
            Directory.CreateDirectory(Path.Combine(LAUNCHER_DIR, "saves"));
            Directory.CreateDirectory(Path.Combine(LAUNCHER_DIR, "versions"));
            Directory.CreateDirectory(Path.Combine(LAUNCHER_DIR, "libraries"));
            Directory.CreateDirectory(Path.Combine(LAUNCHER_DIR, "assets"));
            Directory.CreateDirectory(Path.Combine(LAUNCHER_DIR, "assets", "indexes"));
            Directory.CreateDirectory(Path.Combine(LAUNCHER_DIR, "assets", "objects"));
            Directory.CreateDirectory(Path.Combine(LAUNCHER_DIR, "assets", "virtual"));
        }

        private async void LoadVersionsAsync()
        {
            try
            {
                progressBar.Value = 10;
                statusLabel.Text = "Loading versions...";

                string jsonResponse = await httpClient.GetStringAsync(VERSIONS_URL);
                var versionData = JsonConvert.DeserializeObject<JObject>(jsonResponse);
                var versions = versionData["versions"] as JArray;

                if (versions == null)
                {
                    throw new Exception("Invalid version manifest format");
                }

                versionListBox.Items.Clear();
                foreach (var version in versions)
                {
                    if (version["type"].ToString() == "release")
                    {
                        versionListBox.Items.Add(version["id"].ToString());
                    }
                }

                // Wybierz zapisan¹ wersjê po za³adowaniu listy
                if (!string.IsNullOrEmpty(config.LastSelectedVersion))
                {
                    for (int i = 0; i < versionListBox.Items.Count; i++)
                    {
                        if (versionListBox.Items[i].ToString() == config.LastSelectedVersion)
                        {
                            versionListBox.SelectedIndex = i;
                            break;
                        }
                    }
                }

                statusLabel.Text = "Versions loaded successfully";
                progressBar.Value = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading versions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error loading versions";
                progressBar.Value = 0;
            }
        }

        private void versionListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateActionButton();
            SaveCurrentConfig();
        }

        private void UpdateActionButton()
        {
            if (versionListBox.SelectedIndex == -1)
            {
                actionButton.Enabled = false;
                return;
            }

            string selectedVersion = versionListBox.SelectedItem.ToString();
            string versionPath = Path.Combine(LAUNCHER_DIR, "versions", selectedVersion);

            if (Directory.Exists(versionPath) && File.Exists(Path.Combine(versionPath, $"{selectedVersion}.jar")))
            {
                actionButton.Text = languageComboBox.SelectedIndex == 0 ? "Run Game" : "Uruchom grê";
            }
            else
            {
                actionButton.Text = languageComboBox.SelectedIndex == 0 ? "Download Version" : "Pobierz wersjê";
            }

            actionButton.Enabled = true;
        }

        private async void actionButton_Click(object sender, EventArgs e)
        {
            if (isDownloading) return;

            string selectedVersion = versionListBox.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedVersion)) return;

            string versionPath = Path.Combine(LAUNCHER_DIR, "versions", selectedVersion);

            if (actionButton.Text.Contains("Download") || actionButton.Text.Contains("Pobierz"))
            {
                await DownloadVersionAsync(selectedVersion);
            }
            else
            {
                LaunchGame(selectedVersion);
            }
        }

        private async Task DownloadVersionAsync(string version)
        {
            isDownloading = true;
            actionButton.Enabled = false;
            progressBar.Value = 0;
            statusLabel.Text = languageComboBox.SelectedIndex == 0 ? "Downloading version..." : "Pobieranie wersji...";

            try
            {
                // Get version manifest
                string jsonResponse = await httpClient.GetStringAsync(VERSIONS_URL);
                var versionData = JsonConvert.DeserializeObject<JObject>(jsonResponse);
                var versions = versionData["versions"] as JArray;

                if (versions == null)
                {
                    throw new Exception("Invalid version manifest format");
                }

                JObject selectedVersion = null;
                foreach (var v in versions)
                {
                    if (v["id"].ToString() == version)
                    {
                        selectedVersion = v as JObject;
                        break;
                    }
                }

                if (selectedVersion == null)
                {
                    throw new Exception($"Version {version} not found in manifest");
                }

                // Get version info
                string versionInfoJson = await httpClient.GetStringAsync(selectedVersion["url"].ToString());
                var versionInfo = JsonConvert.DeserializeObject<JObject>(versionInfoJson);

                if (versionInfo == null)
                {
                    throw new Exception("Invalid version info format");
                }

                // Create version directory
                string versionDir = Path.Combine(LAUNCHER_DIR, "versions", version);
                Directory.CreateDirectory(versionDir);

                // Save version JSON
                File.WriteAllText(Path.Combine(versionDir, $"{version}.json"), versionInfoJson);

                // Download client jar
                progressBar.Value = 20;
                statusLabel.Text = "Downloading client...";
                string clientJarUrl = versionInfo["downloads"]["client"]["url"].ToString();
                string clientJarPath = Path.Combine(versionDir, $"{version}.jar");
                await DownloadFileAsync(clientJarUrl, clientJarPath);

                // Download libraries
                progressBar.Value = 40;
                statusLabel.Text = "Downloading libraries...";
                var libraries = versionInfo["libraries"] as JArray;
                if (libraries != null)
                {
                    await DownloadLibrariesAsync(libraries);
                }

                // Download assets
                progressBar.Value = 60;
                statusLabel.Text = "Downloading assets...";
                var assetIndex = versionInfo["assetIndex"] as JObject;
                if (assetIndex != null)
                {
                    await DownloadAssetsAsync(assetIndex);
                }

                progressBar.Value = 100;
                statusLabel.Text = "Download complete";
                MessageBox.Show(
                    languageComboBox.SelectedIndex == 0 ? "Download complete!" : "Pobieranie zakoñczone!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading version: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error downloading version";
                progressBar.Value = 0;
            }
            finally
            {
                isDownloading = false;
                UpdateActionButton();
            }
        }

        private async Task DownloadFileAsync(string url, string destinationPath)
        {
            try
            {
                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));

                // Download the file if it doesn't exist
                if (!File.Exists(destinationPath))
                {
                    byte[] fileBytes = await httpClient.GetByteArrayAsync(url);
                    File.WriteAllBytes(destinationPath, fileBytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading file {url}: {ex.Message}");
                throw;
            }
        }

        private async Task DownloadLibrariesAsync(JArray libraries)
        {
            int count = 0;
            int total = libraries.Count;

            foreach (var lib in libraries)
            {
                try
                {
                    if (lib["downloads"] != null && lib["downloads"]["artifact"] != null)
                    {
                        string path = lib["downloads"]["artifact"]["path"].ToString();
                        string url = lib["downloads"]["artifact"]["url"].ToString();
                        string libPath = Path.Combine(LAUNCHER_DIR, "libraries", path);

                        Directory.CreateDirectory(Path.GetDirectoryName(libPath));

                        if (!File.Exists(libPath))
                        {
                            await DownloadFileAsync(url, libPath);
                        }
                    }

                    // Handle natives if present
                    if (lib["downloads"] != null && lib["downloads"]["classifiers"] != null)
                    {
                        string osName = GetOSName();
                        if (lib["downloads"]["classifiers"][osName] != null)
                        {
                            string nativePath = lib["downloads"]["classifiers"][osName]["path"].ToString();
                            string nativeUrl = lib["downloads"]["classifiers"][osName]["url"].ToString();
                            string nativeFilePath = Path.Combine(LAUNCHER_DIR, "libraries", nativePath);

                            Directory.CreateDirectory(Path.GetDirectoryName(nativeFilePath));

                            if (!File.Exists(nativeFilePath))
                            {
                                await DownloadFileAsync(nativeUrl, nativeFilePath);
                            }
                        }
                    }

                    count++;
                    int percentage = 40 + (count * 20 / total);
                    progressBar.Value = Math.Min(percentage, 60);
                }
                catch (Exception ex)
                {
                    // Log and continue
                    Console.WriteLine($"Error downloading library: {ex.Message}");
                }
            }
        }

        private string GetOSName()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                return Environment.Is64BitOperatingSystem ? "natives-windows-64" : "natives-windows-32";
            }
            else if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                return "natives-linux";
            }
            else if (Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                return "natives-macos";
            }

            return "natives-windows"; // Default fallback
        }

        private async Task DownloadAssetsAsync(JObject assetIndex)
        {
            // Download asset index
            string id = assetIndex["id"].ToString();
            string url = assetIndex["url"].ToString();
            string indexPath = Path.Combine(LAUNCHER_DIR, "assets", "indexes", $"{id}.json");

            Directory.CreateDirectory(Path.GetDirectoryName(indexPath));

            if (!File.Exists(indexPath))
            {
                await DownloadFileAsync(url, indexPath);
            }

            // Parse asset index
            string assetIndexJson = File.ReadAllText(indexPath);
            var assetsObj = JsonConvert.DeserializeObject<JObject>(assetIndexJson);
            var objectsObj = assetsObj["objects"] as JObject;

            if (objectsObj == null)
            {
                throw new Exception("Invalid asset index format");
            }

            int count = 0;
            int totalAssets = objectsObj.Count;
            HashSet<string> specialPaths = new HashSet<string>() { "minecraft/sounds", "minecraft/lang" };

            foreach (var prop in objectsObj.Properties())
            {
                try
                {
                    var assetObj = prop.Value as JObject;
                    string hash = assetObj["hash"].ToString();
                    string hashPrefix = hash.Substring(0, 2);
                    string assetUrl = $"https://resources.download.minecraft.net/{hashPrefix}/{hash}";

                    // Hash-based path (objects directory)
                    string objectPath = Path.Combine(LAUNCHER_DIR, "assets", "objects", hashPrefix, hash);
                    Directory.CreateDirectory(Path.GetDirectoryName(objectPath));

                    // Download the asset to the objects directory
                    if (!File.Exists(objectPath))
                    {
                        await DownloadFileAsync(assetUrl, objectPath);
                    }

                    // Create virtual directory structure for certain paths
                    string virtualPath = prop.Name;
                    bool createVirtualFile = false;

                    foreach (var specialPath in specialPaths)
                    {
                        if (virtualPath.StartsWith(specialPath))
                        {
                            createVirtualFile = true;
                            break;
                        }
                    }

                    if (createVirtualFile)
                    {
                        string fullVirtualPath = Path.Combine(LAUNCHER_DIR, "assets", "virtual", "legacy", virtualPath);
                        Directory.CreateDirectory(Path.GetDirectoryName(fullVirtualPath));

                        // If the virtual file doesn't exist, copy from the objects directory
                        if (!File.Exists(fullVirtualPath))
                        {
                            // Copy the file to maintain the original structure
                            File.Copy(objectPath, fullVirtualPath, true);
                        }
                    }

                    count++;
                    int percentage = 60 + (count * 40 / totalAssets);
                    progressBar.Value = Math.Min(percentage, 100);

                    // Update status every 100 assets
                    if (count % 100 == 0)
                    {
                        statusLabel.Text = $"Downloading assets... ({count}/{totalAssets})";
                        Application.DoEvents(); // Allow UI to update
                    }
                }
                catch (Exception ex)
                {
                    // Log and continue
                    Console.WriteLine($"Error downloading asset: {ex.Message}");
                }
            }
        }

        private void LaunchGame(string version)
        {
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text))
            {
                MessageBox.Show(
                    languageComboBox.SelectedIndex == 0 ? "Please enter a username!" : "Proszê podaæ nazwê u¿ytkownika!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Validate memory allocation
            long systemMemoryMB = GetSystemMemoryInMB();
            if (memorySlider.Value > (systemMemoryMB * 0.8 / 1024))
            {
                DialogResult result = MessageBox.Show(
                    languageComboBox.SelectedIndex == 0
                        ? "You are allocating a large amount of memory. This might cause system instability. Continue anyway?"
                        : "Przydzielasz du¿o pamiêci RAM. Mo¿e to spowodowaæ problemy z systemem. Kontynuowaæ?",
                    languageComboBox.SelectedIndex == 0 ? "Memory Warning" : "Ostrze¿enie o pamiêci",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;
                }
            }

            try
            {
                string username = usernameTextBox.Text;
                string javaPath = customJavaPathCheckBox.Checked
                    ? javaPathTextBox.Text
                    : FindJavaPath();

                if (string.IsNullOrEmpty(javaPath) || !File.Exists(javaPath))
                {
                    javaPath = SelectJavaPath();
                    if (string.IsNullOrEmpty(javaPath))
                    {
                        MessageBox.Show(
                            languageComboBox.SelectedIndex == 0 ? "Cannot find a valid Java path" : "Nie mo¿na znaleŸæ prawid³owej œcie¿ki Java",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }

                // Get version JSON to read mainClass and other data
                string versionJsonPath = Path.Combine(LAUNCHER_DIR, "versions", version, $"{version}.json");
                if (!File.Exists(versionJsonPath))
                {
                    MessageBox.Show(
                        languageComboBox.SelectedIndex == 0 ? "Version JSON file not found" : "Nie znaleziono pliku JSON wersji",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                JObject versionJson = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(versionJsonPath));
                string mainClass = versionJson["mainClass"]?.ToString() ?? "net.minecraft.client.main.Main";

                // Get asset index ID
                string assetIndexId = versionJson["assetIndex"]?["id"]?.ToString() ?? "legacy";

                // Build classpath
                StringBuilder classPath = new StringBuilder();
                BuildClassPath(Path.Combine(LAUNCHER_DIR, "libraries"), classPath);
                classPath.Append(Path.PathSeparator);
                classPath.Append(Path.Combine(LAUNCHER_DIR, "versions", version, $"{version}.jar"));

                // Build Java arguments
                int memoryMB = memorySlider.Value * 1024;
                List<string> args = new List<string>
                {
                    $"-Xmx{memoryMB}M",
                    $"-Xms{memoryMB / 2}M",
                    "-XX:+UseG1GC",
                    "-XX:+ParallelRefProcEnabled",
                    "-XX:MaxGCPauseMillis=200",
                    "-XX:+UnlockExperimentalVMOptions",
                    "-XX:+DisableExplicitGC",
                    "-XX:+AlwaysPreTouch",
                    "-XX:G1NewSizePercent=30",
                    "-XX:G1MaxNewSizePercent=40",
                    "-XX:G1HeapRegionSize=8M",
                    "-XX:G1ReservePercent=20",
                    "-XX:G1HeapWastePercent=5",
                    "-XX:G1MixedGCCountTarget=4",
                    "-XX:InitiatingHeapOccupancyPercent=15",
                    "-XX:G1MixedGCLiveThresholdPercent=90",
                    "-XX:G1RSetUpdatingPauseTimePercent=5",
                    "-XX:SurvivorRatio=32",
                    "-XX:+PerfDisableSharedMem",
                    "-XX:MaxTenuringThreshold=1",
                    "-Dminecraft.launcher.brand=MCLauncher",
                    "-Dminecraft.launcher.version=1.0",
                    "-cp",
                    classPath.ToString(),
                    mainClass,
                    "--username", username,
                    "--version", version,
                    "--gameDir", Path.Combine(LAUNCHER_DIR, "gamedata"),
                    "--assetsDir", Path.Combine(LAUNCHER_DIR, "assets"),
                    "--assetIndex", assetIndexId,
                    "--uuid", Guid.NewGuid().ToString().Replace("-", ""),
                    "--accessToken", "dummy_access_token",
                    "--userType", "legacy",
                    "--versionType", "release",
                    "--savesDir", Path.Combine(LAUNCHER_DIR, "saves"),
                };

                // Start the process
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = javaPath,
                    Arguments = string.Join(" ", args.Select(arg => arg.Contains(" ") ? $"\"{arg}\"" : arg)),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = false
                };

                Process process = new Process { StartInfo = startInfo };

                // Create console window for output
                ConsoleForm consoleForm = new ConsoleForm("Minecraft Console");
                consoleForm.AppendText("NOTE: If you experience lag or crashes, try increasing memory allocation in the launcher.\n\n");
                consoleForm.Show();

                process.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        consoleForm.AppendText($"{e.Data}\n");

                        // Check for memory issues
                        if (e.Data.Contains("OutOfMemoryError") || e.Data.Contains("Can't keep up! Is the server overloaded?"))
                        {
                            consoleForm.AppendText("\nWARNING: Memory issues detected. Try increasing memory allocation in launcher settings.\n");
                        }

                        // Check for view distance warnings
                        if (e.Data.Contains("Changing view distance"))
                        {
                            try
                            {
                                int distance = ExtractViewDistance(e.Data);
                                if (distance > 16 && memorySlider.Value < 4)
                                {
                                    consoleForm.Invoke(new Action(() =>
                                    {
                                        MessageBox.Show(
                                            languageComboBox.SelectedIndex == 0
                                                ? "High view distance (>16) requires significant memory. Recommended minimum is 4GB RAM."
                                                : "Du¿a odleg³oœæ renderowania (>16) wymaga znacznej iloœci pamiêci. Zalecane minimum 4GB RAM.",
                                            languageComboBox.SelectedIndex == 0 ? "View Distance Warning" : "Ostrze¿enie o odleg³oœci renderowania",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                                    }));
                                }
                            }
                            catch { /* Ignore parsing errors */ }
                        }
                    }
                };

                process.ErrorDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        consoleForm.AppendText($"ERROR: {e.Data}\n");
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                // Set up process closed event
                process.EnableRaisingEvents = true;
                process.Exited += (sender, e) =>
                {
                    consoleForm.Invoke(new Action(() =>
                    {
                        consoleForm.AppendText("\nGame process has exited.\n");
                    }));
                };

                // Zapisz konfiguracjê po pomyœlnym uruchomieniu gry
                SaveCurrentConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error launching game: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private long GetSystemMemoryInMB()
        {
            try
            {
                // Simple approach - just use a hardcoded value based on system page size
                // This is not fully accurate but gives a rough estimate that works for the memory warning
                return Environment.SystemPageSize * 256L * 1024L / 1024L; // Approximation based on page size
            }
            catch
            {
                // Fallback to a safe default
                return 4096; // Assume 4GB as a safe default
            }
        }

        private int ExtractViewDistance(string logLine)
        {
            string[] parts = logLine.Split(new string[] { "to " }, StringSplitOptions.None);
            if (parts.Length >= 2)
            {
                string[] parts2 = parts[1].Split(new string[] { "," }, StringSplitOptions.None);
                string distancePart = parts2[0].Trim();
                return int.Parse(distancePart);
            }
            return 0;
        }

        private void BuildClassPath(string directory, StringBuilder classPath)
        {
            foreach (string file in Directory.GetFiles(directory, "*.jar"))
            {
                classPath.Append(file);
                classPath.Append(Path.PathSeparator);
            }

            foreach (string subDirectory in Directory.GetDirectories(directory))
            {
                BuildClassPath(subDirectory, classPath);
            }
        }

        private string FindJavaPath()
        {
            // SprawdŸ najpierw zapisan¹ œcie¿kê Java z konfiguracji
            if (!string.IsNullOrEmpty(config?.JavaPath) && File.Exists(config.JavaPath))
            {
                Console.WriteLine($"U¿ywam zapisanej œcie¿ki Java: {config.JavaPath}");
                return config.JavaPath;
            }

            // Check common Java paths
            string[] commonPaths = {
                "java",
                @"C:\Program Files\Java\jdk-24\bin\java.exe",
                @"C:\Program Files\Java\jdk-17\bin\java.exe",
                @"C:\Program Files\Java\jre\bin\java.exe",
                @"C:\Program Files (x86)\Java\jre\bin\java.exe",
                @"C:\Program Files\Common Files\Oracle\Java\javapath\java.exe"
            };

            foreach (string path in commonPaths)
            {
                try
                {
                    if (path == "java") continue; // Pomijamy domyœln¹ œcie¿kê, chcemy pe³n¹ œcie¿kê

                    if (File.Exists(path))
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = path,
                            Arguments = "-version",
                            UseShellExecute = false,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        };

                        using (Process process = Process.Start(startInfo))
                        {
                            string output = process.StandardError.ReadToEnd();
                            process.WaitForExit();
                            if (output.Contains("version"))
                            {
                                Console.WriteLine($"Znaleziono œcie¿kê Java: {path}");

                                // Automatycznie zapisz znalezion¹ œcie¿kê w konfiguracji
                                if (config != null)
                                {
                                    config.JavaPath = path;
                                    LauncherConfig.SaveConfig(config);
                                }

                                return path;
                            }
                        }
                    }
                }
                catch
                {
                    // Try next path
                }
            }

            return null;
        }

        private string SelectJavaPath()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Java Executable|java.exe";
                openFileDialog.Title = "Select Java Executable";
                openFileDialog.InitialDirectory = @"C:\Program Files\Java";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Zapisz wybran¹ œcie¿kê Java w kontrolce
                        javaPathTextBox.Text = openFileDialog.FileName;
                        customJavaPathCheckBox.Checked = true;
                        javaPathTextBox.Enabled = true;

                        // Zapisz konfiguracjê natychmiast - WA¯NE
                        config.JavaPath = openFileDialog.FileName;
                        config.UseCustomJavaPath = true;

                        bool success = LauncherConfig.SaveConfig(config);
                        Console.WriteLine($"Wybrano now¹ œcie¿kê Java: {openFileDialog.FileName}, zapisano: {success}");

                        return openFileDialog.FileName;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"B³¹d podczas zapisu œcie¿ki Java: {ex.Message}");
                        MessageBox.Show($"Nie uda³o siê zapisaæ œcie¿ki Java: {ex.Message}", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return null;
        }

        private void customJavaPathCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            javaPathTextBox.Enabled = customJavaPathCheckBox.Checked;
            if (!customJavaPathCheckBox.Checked)
            {
                javaPathTextBox.Text = FindJavaPath() ?? "java";
            }

            // Zapisz konfiguracjê
            SaveCurrentConfig();
        }

        private void languageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update UI language
            UpdateUILanguage();
            UpdateActionButton();

            // Zapisz konfiguracjê
            SaveCurrentConfig();
        }

        private void memorySlider_ValueChanged(object sender, EventArgs e)
        {
            memoryLabel.Text = $"{memorySlider.Value} GB";
            SaveCurrentConfig();
        }

        private void usernameTextBox_TextChanged(object sender, EventArgs e)
        {
            SaveCurrentConfig();
        }

        private void memoryGroupBox_Enter(object sender, EventArgs e)
        {
            // Pusta metoda, ale potrzebna z uwagi na powi¹zanie w designerze
        }
    }

    // Console form for game output
    public class ConsoleForm : Form
    {
        private RichTextBox textBox;

        public ConsoleForm(string title)
        {
            this.Text = title;
            this.Size = new System.Drawing.Size(800, 600);

            textBox = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackColor = System.Drawing.Color.Black,
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Consolas", 10F),
                MaxLength = 100000 // Limit text length to prevent memory issues
            };

            this.Controls.Add(textBox);

            // Add form closing handler
            this.FormClosing += (sender, e) => {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    // Just hide the console instead of closing it
                    e.Cancel = true;
                    this.Hide();
                }
            };
        }

        public void AppendText(string text)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new Action<string>(AppendText), text);
            }
            else
            {
                textBox.AppendText(text);

                // Auto-trim if text gets too long
                if (textBox.TextLength > 90000) // 90% of max length
                {
                    textBox.Select(0, 10000);
                    textBox.SelectedText = "";
                }

                textBox.ScrollToCaret();
            }
        }
    }
}