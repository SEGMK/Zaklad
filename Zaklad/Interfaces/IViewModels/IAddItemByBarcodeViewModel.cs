using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaklad.Interfaces.IViewModels
{
    internal interface IAddItemByBarcodeViewModel
    {
        public Task<bool> AddItemByBarcode(string barcode);
        public IPopupService PopupService { get; set; }
    }
}
