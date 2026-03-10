using DocNoc.Xam.Interfaces;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace DocNoc.Xam.Services
{
    public class DialogService : IDialogService
    {
        public async Task Show(string title, string msg, string closeText)
        {
            await Application.Current!.MainPage!.DisplayAlert(title, msg, closeText);
        }
    }
}