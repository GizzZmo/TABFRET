using TABFRET.Models;
using System.Threading.Tasks;

public interface ISettingsService
{
    Task<UserSettings> LoadAsync();
    Task SaveAsync(UserSettings settings);
}
