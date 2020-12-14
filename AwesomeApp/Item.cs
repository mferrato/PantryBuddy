using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeApp
{
    public class Item
    {
        public List<String> names;
        public int expiration;
        public int quantity;
        public int unit;
        public int storage;

        public Item(String n)
        {
            this.names = new List<String>();
            this.names.Add(n);
            this.expiration = -1;
        }

        public Item(String n, int e)
        {
            this.names = new List<String>();
            this.names.Add(n);
            this.expiration = e;
        }

        public Item(String[] ns)
        {
            this.names = new List<String>();
            foreach (String n in ns)
            {
                this.names.Add(n);
            }
            this.expiration = -1;
        }

        public Item(String[] ns, int e)
        {
            this.names = new List<String>();
            foreach(String n in ns)
            {
                this.names.Add(n);
            }
            this.expiration = e;
        }
    }
}
