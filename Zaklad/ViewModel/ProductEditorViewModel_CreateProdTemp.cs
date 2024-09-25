using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zaklad.CustomControls;
using Zaklad.Interfaces;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;
using Zaklad.Views;

namespace Zaklad.ViewModel
{
    internal class ProductEditorViewModel_CreateProdTemp : ProductEditorViewModelBase
    {
        public ProductEditorViewModel_CreateProdTemp(IProductDataTemplate productDataTemplate, bool editExisting = false) : base(productDataTemplate)
        {
            if (editExisting)
            {
                InternalDecisionButtonsCollection.Add(new ButtonWithDecision()
                {
                    Text = "Edytuj",
                    BackgroundColor = Color.Parse("Yellow"),
                    Command = new Command(() =>
                    {
                        ManageImageChanges();
                        UserCustomProductTemplates.UpdateCustomTemplate(ProductDataTemplate);
                    })
                });
                InternalDecisionButtonsCollection.Add(new ButtonWithDecision()
                {
                    Text = "Usuń",
                    BackgroundColor = Color.Parse("Red"),
                    Command = new Command(() =>
                    {
                        ManageImageChanges();
                        FileImageSource fileImageSource = ProductImage as FileImageSource;
                        if (fileImageSource != null && fileImageSource.File != "no_product")
                            CustomProductImagesCRUD.DeleteImage(fileImageSource);
                        UserCustomProductTemplates.DeleteCustomTemplate(productDataTemplate.Id);
                    })
                });
            }
            else
            {
                InternalDecisionButtonsCollection.Add(new ButtonWithDecision()
                {
                    RedirectToEarlierPage = true,
                    Text = "Dodaj",
                    BackgroundColor = Color.Parse("Green"),
                    Command = new Command(() =>
                    {
                        ManageImageChanges();
                        UserCustomProductTemplates.SaveCustomTemplate(ProductDataTemplate);
                    })
                });
            }
        }
        public override int Gramature
        {
            get => 100;
            set
            {

            }
        }
        public override bool IsGramatureReadOnly => true;
    }
}
