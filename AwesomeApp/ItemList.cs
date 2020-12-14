using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace AwesomeApp
{
    class ItemList
    {
        public static Dictionary<String, Item> items;

        public static Dictionary<String, int> expirations;

        public static void AddItem(Item i)
        {
            foreach(String name in i.names)
            {
                items.Add(name.ToLower(), i);
            }
        }

        public static bool HasItem(String n)
        {
            return items.ContainsKey(n);
        }

        public static Item GetItem(String n)
        {
            Item i;
            items.TryGetValue(n, out i);
            return i;
        }

        public static void InitializeItemList()
        {
            items = new Dictionary<string, Item>();
            AddItem(new Item("Apple", 5));

            expirations = new Dictionary<string, int>();
            expirations.Add("Chicken Breast", 4);
            expirations.Add("Ground Beef", 4);
            expirations.Add("Rice", 365);
            expirations.Add("Lettuce", 7);
            expirations.Add("Egg", 30);
        }

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

        public static List<String> GetAutoComplete(String word)
        {
            List<String> list = new List<String>();

            if (word.Length == 0) return list;

            // Find the best matches.
            string[] words;
            int[] values;
            FindBestMatches(word, 10, out words, out values);

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
            if(start_index < 0)
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

        public static int GetScore(String word1, String word2)
        {
            int score = 0;
            for(int i = 0; i < word2.Length; i++)
            {
                if(word1[i] == word2[i])
                {
                    score--;
                }
            }
            return score;
        }

        public static DateTime GetExpirationDate(String food)
        {
            if (expirations.ContainsKey(food))
            {
                int exp = 0;
                expirations.TryGetValue(food, out exp);
                return DateTime.Now.AddDays(exp);
            } else
            {
                return DateTime.Now;
            }
        }
    }
}
