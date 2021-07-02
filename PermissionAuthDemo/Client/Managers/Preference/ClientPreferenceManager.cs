using Blazored.LocalStorage;
using MudBlazor;
using PermissionAuthDemo.Client.Settings;
using PermissionAuthDemo.Shared.Constants;
using System.Threading.Tasks;

namespace PermissionAuthDemo.Client.Managers.Preference
{
    public class ClientPreferenceManager : IClientPreferenceManager
    {
        private readonly ILocalStorageService _localStorageService;

        public ClientPreferenceManager(
            ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<bool> ToggleDarkModeAsync()
        {
            var preference = await GetPreference() as ClientPreference;
            if (preference != null)
            {
                preference.IsDarkMode = !preference.IsDarkMode;
                await SetPreference(preference);
                return !preference.IsDarkMode;
            }

            return false;
        }

        public async Task<MudTheme> GetCurrentThemeAsync()
        {
            var preference = await GetPreference() as ClientPreference;
            if (preference != null)
            {
                if (preference.IsDarkMode == true) return AppTheme.DarkTheme;
            }
            return AppTheme.DefaultTheme;
        }

        public async Task<IPreference> GetPreference()
        {
            return await _localStorageService.GetItemAsync<ClientPreference>(StorageConstants.Local.Preference) ?? new ClientPreference();
        }

        public async Task SetPreference(IPreference preference)
        {
            await _localStorageService.SetItemAsync(StorageConstants.Local.Preference, preference as ClientPreference);
        }
    }
}