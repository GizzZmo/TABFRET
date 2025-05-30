using System.Threading.Tasks;
using TABFRET.Models;

namespace TABFRET.Services
{
    public interface ISettingsService
    {
        Task<UserSettings> LoadAsync();
        Task SaveAsync(UserSettings settings);
    }
}
