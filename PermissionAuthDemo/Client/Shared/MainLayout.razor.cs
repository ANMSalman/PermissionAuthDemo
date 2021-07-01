using MudBlazor;
using PermissionAuthDemo.Client.Settings;
using System;
using System.Threading.Tasks;

namespace PermissionAuthDemo.Client.Shared
{
    public partial class MainLayout : IDisposable
    {
        private MudTheme _currentTheme = AppTheme.DefaultTheme;
        protected override async Task OnInitializedAsync()
        {
            _interceptor.RegisterEvent();
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _interceptor.DisposeEvent();
        }
    }
}
