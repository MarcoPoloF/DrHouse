using DocNoc.Xam.Interfaces;
using System.Runtime.Serialization.Json;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Networking;
using Microsoft.Maui.Storage;

namespace DocNoc.Xam.Services
{
    public class TextService : ITextService
    {
        public T Get<T>(string page, IPreferenceService preference)
        {
            var lang = preference.Get("lang");

            if (string.IsNullOrEmpty(lang))
            {
                lang = "MX";
                preference.Set("lang", "MX");
            }

            var file = $"DocNoc.Xam.Content.Text.{lang}.{page}.json";

            var assembly = this.GetType().Assembly;

            T obj;

            using (var stream = assembly.GetManifestResourceStream(file))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                obj = (T)serializer.ReadObject(stream);
            }

            return obj;
        }
    }
}