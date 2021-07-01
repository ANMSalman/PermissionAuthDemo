using Microsoft.AspNetCore.Components;
using MudBlazor;
using PermissionAuthDemo.Client.Extensions;
using PermissionAuthDemo.Client.Shared.Dialogs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PermissionAuthDemo.Client.Shared
{
    public partial class MainBody
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        private string CurrentUserId { get; set; }
        private string FirstName { get; set; }
        private string SecondName { get; set; }
        private string Email { get; set; }
        private string UniversityName { get; set; }
        private char FirstLetterOfName { get; set; }


        private List<BreadcrumbItem> breadcrumb = new List<BreadcrumbItem>();

        public bool _drawerOpen = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            if (user == null) return;
            if (user.Identity?.IsAuthenticated == true)
            {
                CurrentUserId = user.GetUserId();
                this.FirstName = user.GetFirstName();
                if (this.FirstName.Length > 0)
                {
                    FirstLetterOfName = FirstName[0];
                }
                this.SecondName = user.GetLastName();
                this.Email = user.GetEmail();
            }

            _snackBar.Add($"Welcome { user.GetFirstName()}", Severity.Success);

        }
        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        private void Logout()
        {
            var parameters = new DialogParameters
            {
                {nameof(Dialogs.Logout.ContentText), $"Logout Confirmation"},
                {nameof(Dialogs.Logout.ButtonText), $"Logout"},
                {nameof(Dialogs.Logout.Color), Color.Error},
                {nameof(Dialogs.Logout.CurrentUserId), CurrentUserId},
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            _dialogService.Show<Logout>("Logout", parameters, options);
        }
    }
}
