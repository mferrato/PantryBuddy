using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeApp
{
    public class Recipe
    {

        public String name { private set; get; }

        // List of ingredients
        public Dictionary<String, float> items;
        // cooking duration
        public int cookingtime;
        // recipe instructions
        public String[] instructions;

        // constructor for recipe, takes in a name, a cooking duration time, and a list of ingredients

        public Recipe(String n, int c, String[] itemList, float[] quantity)
        {
            items = new Dictionary<String, float>();
            this.name = n;
            this.cookingtime = c;
            items = new Dictionary<string, float>();
            // makes sure that the items added are not in capital letters
            for (int i = 0; i < itemList.Length; i++)
            {
                items.Add(itemList[i].ToLower(), quantity[i]);
            }

        }

        public Recipe(String n, int c, String[] instructions)
        {
            this.name = n;
            this.cookingtime = c;
            this.instructions = instructions;
        }

        // checks if the item is part of the ingredient lists
        public bool HasItem(String n)
        {
            return items.ContainsKey(n);
        }


        // Adds list of ingredients to recipe instance
        public void AddItemList(String[] newItems, float[] quantity)
        {
            for(int i = 0; i < newItems.Length; i++)
            {
                items.Add(newItems[i].ToLower(), quantity[i]);
            }
        }

        // Get quantitiy of an item in the ingredient list
        public float GetQuantity(String n)
        {
            float q;
            items.TryGetValue(n, out q);
            return q;
        }

    //    public int ItemCheck(Item i)
     //   {
    //       foreach (String name in i.names)
     //       {
      //          if (HasItem(name))
      //          {
      //              float quantityNeeded = GetQuantity(name);
      //              float quantityAvail = i.quantity;
      //              if (quantityAvail >= quantityNeeded)
      //              {
      //                  return 1;
      //              }
      //              else {
      //                  return 2;
       //             }
      //          }
      //      }
     //       return 0;
    //    }
    }
}