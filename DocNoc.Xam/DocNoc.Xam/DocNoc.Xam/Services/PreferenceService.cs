using DocNoc.Xam.Interfaces;
using Microsoft.Maui.Storage;

namespace DocNoc.Xam.Services
{
    public class PreferenceService : IPreferenceService
    {
        public string Get(string key)
        {
            return Preferences.Get(key, "");
        }

        public void Set(string key, string value)
        {
            Preferences.Set(key, value);
        }
    }
}