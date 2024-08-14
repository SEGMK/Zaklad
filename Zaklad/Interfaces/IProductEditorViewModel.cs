using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaklad.Interfaces
{
    public interface IProductEditorViewModel : INotifyPropertyChanged
    {
        public IUserProduct UserProduct { get; }
        public int Gramature { get; set; }
        public decimal EditableKcal { get; set; }
        public decimal EditableProteins { get; set; }
        public decimal EditableFat { get; set; }
        public decimal EditableCarbohydrates { get; set; }
        public string ProductName { get; set; }
        public ImageSource ProductImage { get; }
    }
}
