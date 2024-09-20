using CommunityToolkit.Maui.Views;
using Zaklad.Interfaces;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;
using Zaklad.ViewModel;

namespace Zaklad.Views;

public partial class MakroQuestionnaire : Popup
{
	public MakroQuestionnaire()
	{
		InitializeComponent();
		BindingContext = ServiceHelper.Current.GetService<IMakroQuestionnaireViewModel>();
    }

    private void Button_Pressed(object sender, EventArgs e)
    {
        if (!ValidateNumericInput())
        {
            ServiceHelper.Current.GetService<IAlertService>().ShowAlert("Błąd", "Podane wartości nie są prawidłowe");
            return;
        }
        ((IMakroQuestionnaireViewModel)BindingContext).CalculateMakro.Execute(this);
        Close();
    }
    private bool ValidateNumericInput()
    { 
        if (!float.TryParse(entryAge.Text, out float resultAge))
            return false;
        if (!float.TryParse(entryHeight.Text, out float resultHeight))
            return false;
        if (!float.TryParse(entryWeight.Text, out float resultWeight))
            return false;
        return true;
    }
}