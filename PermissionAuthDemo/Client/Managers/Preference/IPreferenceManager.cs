using PermissionAuthDemo.Client.Settings;
using System.Threading.Tasks;

namespace PermissionAuthDemo.Client.Managers.Preference
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();
    }
}
