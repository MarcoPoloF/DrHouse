using System;
namespace DocNoc.Xam.Interfaces
{
    public interface IPreferenceService
    {
        void Set(string key, string value);

        string Get(string key);
    }
}