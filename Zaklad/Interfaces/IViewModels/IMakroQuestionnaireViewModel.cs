using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Zaklad.Interfaces.IViewModels
{
    interface IMakroQuestionnaireViewModel : INotifyPropertyChanged
    {
        int ExerciseIntensity { get; set; }
        string ExerciseIntensityDescritpion { get; }
        int Age { get; set; }
        float Height { get; set; }
        float Weight { get; set; }
        bool IsMen { get; set; }
        ICommand CalculateMakro { get;  }
    }
}
