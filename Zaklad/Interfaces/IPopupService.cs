using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaklad.Interfaces
{
    public interface IPopupService
    {
        void ShowPopup(Popup popup);
        Task<object?> ShowPopupAsync(Popup popup);
    }
}
