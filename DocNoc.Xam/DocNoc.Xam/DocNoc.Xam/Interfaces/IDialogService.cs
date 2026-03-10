using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocNoc.Xam.Interfaces
{
    public interface IDialogService
    {
        Task Show(string title, string msg, string closeText);
    }
}
