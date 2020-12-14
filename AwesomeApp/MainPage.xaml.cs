using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AwesomeApp
{

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //_InitializeNotifications();
            ItemList.InitializeItemList();
            Inventory.InitializeInventory();
            //RecipeList.InitializeRecipeList();
            //Recipes.InitializeRecipes();
        }

        private void Navigate_To_Inventory(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Inventory());
        }

        private void Navigate_To_AddItem(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AddItem());
        }

        private void Navigate_To_Recipes(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Recipes());
        }

    //    private void Notification_Scroll_Handler(object sender, ScrolledEventArgs e)
    //    {
     //       double spaceAvailableForScrolling = Notifications.ContentSize.Height - Notifications.Height;
     //       double buffer = 32;
     //       NotifcationScrollIndicator.IsVisible = spaceAvailableForScrolling > e.ScrollY + buffer;
     //   }

    //    private List<String> _GetRecentNotifications()
     //   {
            // TODO
     //       List<String> notifications = new List<String>();
     //       notifications.Add("Hello");
      //      return notifications;
      //  }
      //
      //  private void _InitializeNotifications()
      //  {
      //      List<String> notifications = _GetRecentNotifications();

        //}
    }
}

