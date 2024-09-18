using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Zaklad.Interfaces;

namespace Zaklad.Models
{
    public static class UserSettingsCRUD
    {
        private static string AppPath = FileSystem.Current.AppDataDirectory;
        private const string UserProductsFile = "UsersSettingsData";
        private static readonly string FilePath = Path.Combine(AppPath + UserProductsFile);
        public static void SaveUserSettingsData(IMakroSettingsData makroSettingsData)
        {
            File.WriteAllText(FilePath, JsonSerializer.Serialize(makroSettingsData));
        }
        public static IMakroSettingsData GetUserSettingsData()
        {
            try
            {
                return JsonSerializer.Deserialize<IMakroSettingsData>(File.ReadAllText(FilePath));
            }
            catch (Exception ex)
            { 
                return new MakroSettingsData() 
                {
                    Kcal = 2000,
                    Proteins = 100,
                    Fat = 100,
                    Sugar = 100
                };
            }
        }
    }
}
