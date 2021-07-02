using MudBlazor;
using System.Threading.Tasks;

namespace PermissionAuthDemo.Client.Managers.Preference
{
    public interface IClientPreferenceManager : IPreferenceManager
    {
        Task<MudTheme> GetCurrentThemeAsync();

        Task<bool> ToggleDarkModeAsync();
    }
}