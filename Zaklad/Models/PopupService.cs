using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaklad.Models
{
    public class PopupService : IPopupService
    {
        public void ShowPopup(Popup popup)
        {
            Page page = Application.Current?.MainPage ?? throw new NullReferenceException();
            MainThread.BeginInvokeOnMainThread(new Action(() =>
            {
                page.ShowPopup(popup);
            }));
        }
        public async Task<object?> ShowPopupAsync(Popup popup)
        {
            Page page = Application.Current?.MainPage ?? throw new NullReferenceException();
            object result = null;
            await MainThread.InvokeOnMainThreadAsync( async() =>
            {
                 result = await page.ShowPopupAsync(popup, CancellationToken.None);
            });
            return result;
        }
    }
}
