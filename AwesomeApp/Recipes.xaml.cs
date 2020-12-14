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
    public partial class Recipes : ContentPage
    {
        public String RecipeName;

        public static String[] chickenNoodleItems = { "Chicken Breast", "Noodle", "Celery", "Chicken Broth", "Carrot", "Salt", "Pepper" };
        public static String[] chickenFriedRiceItems = { "Chicken Breast", "Soy Sauce", "Celery", "Sesame Oil", "Carrot", "Salt", "Pepper", "Vegetable Oil" };
        public static String[] baconCheeseburgerItems = { "Bacon", "Ground Beef", "Hamburger Bread", "American Cheese" };
        public static float[] chickenNoodleItemQ = { 1, 1, 1, 1, 1, 1, 1 };
        public static float[] chickenFriedRiceItemQ = { 1, 1, 1, 1, 1, 1, 1, 1 };
        public static float[] baconCheeseburgerItemQ = { 2, 30, 1, 1 };

        public static List<Recipe> masterList = new List<Recipe>
            {
                new Recipe("Chicken Fried Rice", 20, chickenFriedRiceItems, chickenFriedRiceItemQ),
                new Recipe("Bacon Cheeseburger", 15, baconCheeseburgerItems, baconCheeseburgerItemQ),
                new Recipe("Chicken Noodle Soup", 30, chickenNoodleItems, chickenNoodleItemQ),
            };

        public static List<Recipe> GetAutocomplete(String word)
        {
            List<Recipe> l = new List<Recipe>();
            foreach (Recipe r in masterList)
            {
                //if (word.Length <= p.Name.Length)
               // {
                //    if (word.ToLower().Equals(p.Name.Substring(0, word.Length).ToLower()))
                //    {
                //        l.Add(p);
               //     }
               // }

                if (r.HasItem(word.ToLower()))
                {
                    l.Add(r);
                    System.Diagnostics.Debug.WriteLine("Inside");
                }
                System.Diagnostics.Debug.WriteLine("SomeText");
            }
            System.Diagnostics.Debug.WriteLine("SomeText");
            return l;
        }

        public ListView listView;

        public Recipes()
        {
            InitializeComponent();
            // Create the ListView.
            listView = new ListView
            {
                // Source of data items.
                ItemsSource = sort_recipes(masterList),

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "name");

                    //Label cookingTimeLabel = new Label();
                    //cookingTimeLabel.SetBinding(Label.TextProperty,
                    //    new Binding("Cooking Time:", BindingMode.OneWay,
                     //       null, " {d}"));

                    BoxView boxView = new BoxView();
                    //boxView.SetBinding(BoxView.ColorProperty, "FavoriteColor");

                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                //boxView,
                                new StackLayout
                                {
                                    VerticalOptions = LayoutOptions.Center,
                                    Spacing = 0,
                                    Children =
                                    {
                                        nameLabel
                                      //  cookingTimeLabel
                                    }
                                }
                            }
                        }
                    };
                })

            };
            InventoryGrid.Children.Add(listView, 0, 1);
            Grid.SetColumnSpan(listView, 4);
        }

        public void RecipeSearch_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                // TODO
                //sender.ItemsSource = new String[] { "Hello", "Goodbye" };
                sender.ItemsSource = ItemList.GetAutoComplete(sender.Text);
            }
        }

        public void RecipeSearch_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            // TODO
        }

        public void RecipeSearch_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            Console.WriteLine("Hello");
            // TODO
            if (sender.Text.Length <= 0)
            {
                listView.ItemsSource = sort_recipes(masterList);
            }
            else
            {
                listView.ItemsSource = sort_recipes(GetAutocomplete(sender.Text));
            }
        }

        public static void InitializeRecipes()
        {

        }

        public static int score(Recipe r)
        {
            int s = 0;
            foreach(KeyValuePair<String, float> v in r.items)
            {
                String ingredient = v.Key;
                if (Inventory.contains.Contains(ingredient)) s--;
            }
            return s;
        }

        public static List<Recipe> sort_recipes(List<Recipe> o)
        {
            Recipe[] oldRecipes = o.ToArray();
            int[] scores = new int[o.Count];
            for(int i = 0; i < oldRecipes.Length; i++)
            {
                scores[i] = score(oldRecipes[i]);
            }
            Array.Sort(scores, oldRecipes);
            return new List<Recipe>(oldRecipes);
        }
    }
}

