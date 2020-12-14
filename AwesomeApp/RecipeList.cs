using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace AwesomeApp
{
    public class RecipeList
    {
        // List of recipes represented as dictionary
        public static Dictionary<String, Recipe> recipes;

        // Adds a new recipe to the dictionary
        public static void AddRecipe(Recipe r)
        {
            recipes.Add(r.name.ToLower(), r);
        }

        // Checks if a recipe exists already
        public static bool HasRecipe(String n)
        {
            return recipes.ContainsKey(n);
        }

        // Get recipe instance from a name
        public static Recipe GetRecipe(String n)
        {
            Recipe r;
            recipes.TryGetValue(n, out r);
            return r;
        }

        // List of food items stored

        public static String[] Words = { 
            "A",
            "B", "Bacon",
            "C", "Carrot", "Celery", "Chicken Breast", "Chicken Broth",
            "D",
            "E", "Egg",
            "F",
            "G","Garlic", "Green Onion", "Ground Beef",
            "H", "Hamburger Bread",
            "I",
            "J",
            "K", "Ketchup",
            "L", "Lettuce",
            "M", "Mayonnaise", "Mustard",
            "N", "Noodles",
            "O", "Olive Oil",
            "P", "Peas", "Pepper", "Pickles",
            "Q",
            "R", "Rice",
            "S", "Salt", "Sesame Oil", "Soy Sauce",
            "T", "Thyme", "Tomato",
            "U",
            "V", "Vegetable Oil",
            "W", "White Onion",
            "X",
            "Y",
            "Z"
        };

        // Names of recipes stored
        public static String[] recipeNames = {"Bacon Cheeseburger", "Chicken Fried Rice", "Chicken Noodle Soup"
        };

        public static void InitializeRecipeList()
        {
            // Creates a new instance of a recipe list, then creates three recipes Chicken Noodle, Chicken Fried Rice, Burger, then adds them to the recipe list
            recipes = new Dictionary<string, Recipe>();
            String[] chickenNoodleItems = { "Chicken Breast", "Noodle", "Celery", "Chicken Broth", "Carrot", "Salt", "Pepper" };
            String[] chickenFriedRiceItems = { "Chicken Breast", "Soy Sauce", "Celery", "Sesame Oil", "Carrot", "Salt", "Pepper", "Vegetable Oil" };
            String[] baconCheeseburgerItems = { "Bacon", "Ground Beef", "Hamburger Bread", "American Cheese" };
            float[] chickenNoodleItemQ = { 1, 1, 1, 1, 1, 1, 1 };
            float[] chickenFriedRiceItemQ = { 1, 1, 1, 1, 1, 1, 1, 1 };
            float[] baconCheeseburgerItemQ = { 2, 30, 1, 1 };
            AddRecipe(new Recipe("Chicken Fried Rice", 20, chickenFriedRiceItems, chickenFriedRiceItemQ));
            AddRecipe(new Recipe("Bacon Cheeseburger", 15, baconCheeseburgerItems, baconCheeseburgerItemQ));
            AddRecipe(new Recipe("Chicken Noodle Soup", 30, chickenNoodleItems, chickenNoodleItemQ));
        }

        public static List<String> GetAutoComplete(String word)
        {
            List<String> list = new List<String>();

            if (word.Length == 0) return list;

            // Find the best matches.
            string[] words;
            int[] values;
            FindBestMatches(word, 10, out words, out values);

            // Display the best matches.
            //for (int i = 0; i < Math.Min(words.Length, 6); i++)
            //{
            //    foreach (String recipe in recipeNames) {
            //        Recipe r = GetRecipe(recipe);
            //        if (r.HasItem(words[i]))
            //        {
            //            list.Add(recipe);
            //        }
            //    }
            //}

            // Display the best matches.
            for (int i = 0; i < Math.Min(words.Length, 6); i++)
            {
                list.Add(words[i]);
            }

            return list;
        }

        public static void FindBestMatches(string word, int num_matches, out string[] words, out int[] values)
        {
            // Find words that start with the same letter.
            string start_char = word.Substring(0, 1).ToUpper();
            int start_index = Array.BinarySearch(Words, start_char);
            if (start_index < 0)
            {
                words = new string[0];
                values = new int[0];
                return;
            }
            Console.WriteLine(start_index);
            List<string> match_words = new List<string>();
            List<int> match_values = new List<int>();
            for (int i = start_index + 1; i < Words.Length; i++)
            //for(int i = 0; i < Words.Length; i++)
            {
                // Get the next word and make sure it starts
                // with the same letter.
                string test_word = Words[i];
                if (test_word.Substring(0, 1).ToUpper() != start_char)
                    break;

                // Consider the next word up to the length
                // of the typed word.
                int max_length = Math.Min(test_word.Length, word.Length);
                string short_word = test_word.Substring(0, max_length);

                // Build the edit graph.
                int score = GetScore(word.ToUpper(), short_word.ToUpper());
                match_words.Add(test_word);
                match_values.Add(score);
            }

            // Sort the matches by distance, smallest distance first.
            string[] match_words_array = match_words.ToArray();
            int[] match_values_array = match_values.ToArray();
            Array.Sort(match_values_array, match_words_array);

            // Return the desired number of matches.
            int max = Math.Min(num_matches, match_values_array.Length);
            words = new string[max];
            Array.Copy(match_words_array, words, max);
            values = new int[max];
            Array.Copy(match_values_array, values, max);
        }
    }
}
