using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaklad.Interfaces.IViewModels
{
    public interface IProductEditorViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Button> DecisionButtonsCollection { get; }
        public IUserProduct UserProduct { get; }
        public int Gramature { get; set; }
        public int EditableKcal { get; set; }
        public int EditableProteins { get; set; }
        public int EditableFat { get; set; }
        public int EditableCarbohydrates { get; set; }
        public string ProductName { get; set; }
        public ImageSource ProductImage { get; }
    }
}
