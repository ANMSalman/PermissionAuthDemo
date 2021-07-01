using System.Collections.Generic;

namespace PermissionAuthDemo.Server.Services.CurrentUser
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        List<KeyValuePair<string, string>> Claims { get; set; }
    }
}