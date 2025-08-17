using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using TABFRET.Models;

namespace TABFRET.Services
{
    public class SettingsService : ISettingsService
    {
        private const string FileName = "settings.json";
        private readonly string _settingsPath = Path.Combine(
            System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData),
            "TABFRET", FileName);

        public async Task<UserSettings> LoadAsync()
            {
                try
                {
                    if (!File.Exists(_settingsPath))
                        return new UserSettings();

                    string json = await File.ReadAllTextAsync(_settingsPath);
                    return JsonSerializer.Deserialize<UserSettings>(json) ?? new UserSettings();
                }
                catch (Exception)
                {
                    return new UserSettings();
                }
            }

        public async Task SaveAsync(UserSettings settings)
            {
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(_settingsPath)!);
                    string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                    await File.WriteAllTextAsync(_settingsPath, json);
                }
                catch (Exception)
                {
                }
            }
    }
}
