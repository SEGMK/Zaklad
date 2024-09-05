using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zaklad.Interfaces;

namespace Zaklad.Models
{
    public static class UserCustomProductTemplates
    {
        public static void SaveCustomTemplate(IProductDataTemplate productDataTemplate)
        { 
        
        }
        public static List<IProductDataTemplate> GetCustomTemplates(string name = "")
        {
            return new List<IProductDataTemplate>();
        }
        public static void UpdateCustomTemplate(Guid Id)
        {
            
        }
        public static void DeleteCustomTemplate(Guid Id)
        {

        }
    }
}
