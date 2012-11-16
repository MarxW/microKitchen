using System;
using Microsoft.SPOT;
using System.Collections;
using GHIElectronics.NETMF.Glide.UI;
using Microsoft.SPOT.Hardware;

namespace microKitchen.Shopping
{

    enum ShoppingItemTypes
    {
        Meat_SeaFood,
        Dairy,
        Frozen,
        UnCategorised
    }

    class ShoppingList
    {
        public ArrayList Items { get; set; }

        public ShoppingList()
        {
            this.Items = new ArrayList();
        }

        public ShoppingListItem GetItem(string name)
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (name.Equals(((ShoppingListItem)this.Items[i]).Name))
                {
                    return (ShoppingListItem)this.Items[i];
                }
            }
            return null;
        }

        public ShoppingListItem GetItem(int position)
        {
            return (ShoppingListItem)Items[position];
        }

        public int Length()
        {
            return this.Items.Count;
        }

        public void Add(ShoppingListItem item)
        {
            this.Items.Add(item);
        }

        public void Add(string name, ShoppingItemTypes type, int numberOfItem)
        {
            this.Items.Add(new ShoppingListItem(name, type, numberOfItem));
        }

        public void Remove(int position)
        {
            this.Items.Remove(this.Items[position]);
        }

        public void Remove(ShoppingListItem item)
        {
            this.Items.Remove(item);
        }
    
    }

    class ShoppingListItem
    {
        public string Name { get; set; }
        public ShoppingItemTypes Type { get; set; }
        public int NumberOfItems { get; set; }

        public ShoppingListItem(string name, ShoppingItemTypes type, int number)
        {
            this.Name = name;
            this.Type = type;
            this.NumberOfItems = number;
        }
    }
}
