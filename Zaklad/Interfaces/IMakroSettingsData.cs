using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Zaklad.Models;

namespace Zaklad.Interfaces
{
    //this attribute is made for deserializing interface to typed object
    [JsonDerivedType(typeof(MakroSettingsData), typeDiscriminator: nameof(MakroSettingsData))]
    public interface IMakroSettingsData
    {
        int Kcal { get; set; }
        int Proteins { get; set; }
        int Fat { get; set; }
        int Carbohydrates { get; set; }
    }
}
