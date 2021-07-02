using Microsoft.AspNetCore.Authorization;
using MudBlazor;
using PermissionAuthDemo.Shared.Constants;
using PermissionAuthDemo.Shared.Responses.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PermissionAuthDemo.Client.Pages.Identity
{
    public partial class Users
    {
        private List<UserResponse> _userList = new();
        private UserResponse _user = new();
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateUsers;
        private bool _canSearchUsers;
        private bool _canExportUsers;
        private bool _canViewRoles;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateUsers = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Users.Create)).Succeeded;
            _canSearchUsers = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Users.Search)).Succeeded;
            _canExportUsers = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Users.Export)).Succeeded;
            _canViewRoles = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Roles.View)).Succeeded;

            await GetUsersAsync();
            _loaded = true;
        }

        private async Task GetUsersAsync()
        {
            var response = await _userManager.GetAllAsync();
            if (response.Succeeded)
            {
                _userList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private bool Search(UserResponse user)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (user.FirstName?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (user.LastName?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (user.Email?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (user.PhoneNumber?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (user.UserName?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }

        private async Task InvokeModal()
        {
            var parameters = new DialogParameters();
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<RegisterUserModal>("Register New User", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetUsersAsync();
            }
        }

        private void ViewProfile(string userId)
        {
            _navigationManager.NavigateTo($"/user-profile/{userId}");
        }

        private void ManageRoles(string userId, string email)
        {
            if (email == "mohamed.salman@itelasoft.com") _snackBar.Add("Not Allowed.", Severity.Error);
            else _navigationManager.NavigateTo($"/identity/user-roles/{userId}");
        }
    }
}