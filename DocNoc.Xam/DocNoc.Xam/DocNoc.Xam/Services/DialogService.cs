using DocNoc.Xam.Interfaces;
using System.Threading.Tasks;

namespace DocNoc.Xam.Services
{
    public class DialogService : IDialogService
    {
        public async Task Show(string title, string msg, string closeText)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(title, msg, closeText);
        }
    }
}