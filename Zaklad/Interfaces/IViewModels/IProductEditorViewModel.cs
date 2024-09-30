using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zaklad.CustomControls;

namespace Zaklad.Interfaces.IViewModels
{
    public interface IProductEditorViewModel : INotifyPropertyChanged
    {
        public ReadOnlyObservableCollection<ButtonWithDecision> DecisionButtonsCollection { get; }
        public bool IsGramatureReadOnly { get; }
        public int Gramature { get; }
        public int EditableKcal { get; set; }
        public int EditableProteins { get; set; }
        public int EditableFat { get; set; }
        public int EditableCarbohydrates { get; set; }
        public string ProductName { get; set; }
        public ImageSource ProductImage { get; set; }
        public ICommand TakeNewPhotoCommand { get; }
        public ICommand ClearSavedDataUponDismissedByTappingOutsideOfPopup { get; }
    }
}
