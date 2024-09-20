using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zaklad.Interfaces;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;

namespace Zaklad.ViewModel
{
    class MakroQuestionnaireViewModel : IMakroQuestionnaireViewModel
    {
        public MakroQuestionnaireViewModel()
        {
            ExerciseIntensityDescritpion = ExerciseIntensityDescriptionsCollection[0].Item1;
        }
        private Dictionary<int, (string, float)> ExerciseIntensityDescriptionsCollection = new Dictionary<int, (string, float)>()
        {
            { 0, ("Brak wysiłku fizycznego", 0f)},
            { 1, ("Mały wysiłek fizyczny/ćwiczenia 1-3 w tygodni", 1.375f)},
            { 2, ("średni wysiłek fizyczny/ćwiczenia 3-5 w tygodniu", 1.55f)},
            { 3, ("Duży wysiłek fizyczny/ćwiczenia 5-6 w togdniu", 1.725f)}
        };
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        private int _exerciseIntensity;
        public int ExerciseIntensity
        {
            get => _exerciseIntensity;
            set
            {
                _exerciseIntensity = value;
                ExerciseIntensityDescritpion = ExerciseIntensityDescriptionsCollection[value].Item1;
                OnPropertyChange(nameof(ExerciseIntensity));
            }
        }
        private int _age;
        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChange(nameof(Age));
            }
        }
        private float _height;
        public float Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChange(nameof(Height));
            }
        }
        private float _weight;
        public float Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChange(nameof(Weight));
            }
        }
        private string _exerciseIntensityDescritpion;
        public string ExerciseIntensityDescritpion
        {
            get => _exerciseIntensityDescritpion;
            private set
            {
                _exerciseIntensityDescritpion = value;
                OnPropertyChange(nameof(ExerciseIntensityDescritpion));
            }
        }
        private bool _isMen;
        public bool IsMen
        {
            get => _isMen;
            set 
            {
                _isMen = value;
                OnPropertyChange(nameof(IsMen));
            }
        }
        public ICommand CalculateMakro => new Command(SaveNewMakroSettings);
        private void SaveNewMakroSettings()
        {
            //for women: BMR = 655 + (9.6 × body weight in kg) +(1.8 × body height in cm) -(4.7 × age in years);
            //for men: BMR = 66 + (13.7 × weight in kg) +(5 × height in cm) -(6.8 × age in years).
            const float carbohydratesCaloriesPerUnit = 3.984375f;
            const float proteinColoriesPerUnit = 4;
            const float fatColoriesPerUnit = 8.918f;
            float bodyWeightMultipliciant;
            float heightMultipliciant;
            float ageMultipliciant;
            int kcalConstant;
            if (IsMen)
            {
                bodyWeightMultipliciant = 13.7f;
                heightMultipliciant = 5f;
                ageMultipliciant = 6.8f;
                kcalConstant = 66;
            }
            else
            {
                bodyWeightMultipliciant = 9.6f;
                heightMultipliciant = 1.8f;
                ageMultipliciant = 4.7f;
                kcalConstant = 655;
            }
            float kcalIntake = (kcalConstant + (bodyWeightMultipliciant * Weight) + (heightMultipliciant * Height) - (ageMultipliciant * Age)) * ExerciseIntensityDescriptionsCollection[ExerciseIntensity].Item2;
            float proteinsIntake = (kcalIntake * 0.35f) / proteinColoriesPerUnit;
            float fatIntake = (kcalIntake * 0.35f) / fatColoriesPerUnit;
            float carbohydratesIntake = (kcalIntake * 0.30f) / carbohydratesCaloriesPerUnit;
            IMakroSettingsData makroData = ServiceHelper.Current.GetService<IMakroSettingsData>();
            makroData.Kcal = (int)kcalIntake;
            makroData.Proteins = (int)proteinsIntake;
            makroData.Fat = (int)fatIntake;
            makroData.Carbohydrates = (int)carbohydratesIntake;
            UserSettingsCRUD.SaveUserSettingsData(makroData);
        }
    }
}
