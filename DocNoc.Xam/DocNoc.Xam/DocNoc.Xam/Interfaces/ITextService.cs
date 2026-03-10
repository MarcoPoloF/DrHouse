using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocNoc.Xam.Interfaces
{
    public interface ITextService
    {
        T Get<T>(string page, IPreferenceService preference);
    }
}
