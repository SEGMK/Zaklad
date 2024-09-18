using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zaklad.Interfaces;

namespace Zaklad.Models
{
    internal class MakroSettingsData : IMakroSettingsData
    {
        private int _kcal;
        public int Kcal { get => _kcal; set => _kcal = value; }
        private int _proteins;
        public int Proteins { get => _proteins; set => _proteins = value; }
        private int _fat;
        public int Fat { get => _fat; set => _fat = value; }
        private int _carbohydrates;
        public int Carbohydrates { get => _carbohydrates; set => _carbohydrates = value; }
    }
}
