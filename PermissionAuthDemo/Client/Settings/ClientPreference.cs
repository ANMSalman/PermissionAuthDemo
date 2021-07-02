namespace PermissionAuthDemo.Client.Settings
{
    public record ClientPreference : IPreference
    {
        public bool IsDarkMode { get; set; }
        public bool IsDrawerOpen { get; set; }
        public string PrimaryColor { get; set; }
    }
}