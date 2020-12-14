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
    public partial class Inventory : ContentPage
    {

        public class InvItem
        {
            public InvItem(string name, DateTime birthday, Color favoriteColor)
            {
                this.Name = name;
                this.Expiration = birthday;
                this.FavoriteColor = favoriteColor;
            }

            public string Name { private set; get; }

            public DateTime Expiration { private set; get; }

            public Color FavoriteColor { private set; get; }
        };

        public static void InitializeInventory()
        {
        }

        public static List<InvItem> masterList = new List<InvItem>
            {
                new InvItem("Chicken Breast", DateTime.Today.AddDays(3), Color.Green),
                new InvItem("Egg", DateTime.Today.AddMonths(-5), Color.Yellow)
            };

        public static HashSet<String> contains = new HashSet<String>
        {
            "chicken breast, egg"
        };

        public static void AddItem(string n, DateTime d, Color c)
        {
            masterList.Add(new InvItem(n, d, c));
            contains.Add(n.ToLower());
        }

        public static void RemoveItem(String s)
        {
            for(int i = 0; i < masterList.Count; i++)
            {
                if(masterList.ElementAt(i).Name.Equals(s))
                {
                    masterList.RemoveAt(i);
                }
            }
        }

        public void FoodSearch_TextChanged(object sender, System.EventArgs args)
        {
            if (((Entry)sender).Text.Length <= 0)
            {
                listView.ItemsSource = masterList;
            } else {
                listView.ItemsSource = GetAutocomplete(((Entry)sender).Text);
            }
        }

        public static List<InvItem> GetAutocomplete(String word)
        {
            List<InvItem> l = new List<InvItem>();
            foreach(InvItem p in masterList)
            {
                if(word.Length <= p.Name.Length)
                {
                    if(word.ToLower().Equals(p.Name.Substring(0, word.Length).ToLower()))
                    {
                        l.Add(p);
                    }
                }
            }
            return l;
        }

        // Define some data.

        public ListView listView;

        public Inventory()
        {
            InitializeComponent();
            // Create the ListView.
            listView = new ListView
            {
                // Source of data items.
                ItemsSource = masterList,

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "Name");

                    Label birthdayLabel = new Label();
                    birthdayLabel.SetBinding(Label.TextProperty,
                        new Binding("Expiration", BindingMode.OneWay,
                            null, null, "Expires {0:d}"));

                    BoxView boxView = new BoxView();
                    boxView.SetBinding(BoxView.ColorProperty, "FavoriteColor");

                    Grid grid = new Grid
                    {
                        ColumnDefinitions = { new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength(9, GridUnitType.Star) } }
                    };
                    grid.Children.Add(boxView, 0, 0);
                    Grid.SetRowSpan(boxView, 2);
                    grid.Children.Add(nameLabel, 1, 0);
                    grid.Children.Add(birthdayLabel, 1, 1);

                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = grid
                        //View = new StackLayout
                        //{
                          //  Padding = new Thickness(0, 5),
                          //  Orientation = StackOrientation.Horizontal,
                          //  Children =
                          //  {
                          //      boxView,
                          //      new StackLayout
                          //      {
                          //          VerticalOptions = LayoutOptions.Center,
                          //          Spacing = 0,
                          //          Children =
                          //          {
                          //              nameLabel,
                          //              birthdayLabel
                          //          }
                          //      }
                           // }
                        //}
                    };
                })

            };
            InventoryGrid.Children.Add(listView, 0, 1);
            Grid.SetColumnSpan(listView, 4);
        }
    }
}