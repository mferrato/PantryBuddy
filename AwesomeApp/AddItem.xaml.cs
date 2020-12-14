using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using dotMorten.Xamarin.Forms;

namespace AwesomeApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItem : ContentPage
    {
        public String FoodName;

        public AddItem()
        {
            InitializeComponent();
        }

        public void FoodSearch_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if(args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                // TODO
                //sender.ItemsSource = new String[] { "Hello", "Goodbye" };
                sender.ItemsSource = ItemList.GetAutoComplete(sender.Text);
            }
        }

        public void FoodSearch_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            // TODO
        }

        public void FoodSearch_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            // TODO
            ExpirationDate.Date = ItemList.GetExpirationDate(NameBox.Text);
            FoodName = sender.Text;
        }

        public void AddItemButton(object sender, System.EventArgs args)
        {
            Inventory.AddItem(NameBox.Text, ExpirationDate.Date, Color.Teal);
            Acr.UserDialogs.UserDialogs.Instance.Toast("Item Added!", new TimeSpan(3));
            Navigation.PopAsync();
        }
    }
}