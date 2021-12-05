using System;
using System.Collections.Generic;
using System.Text;

namespace BarSimulator
{
    class Drink
    {
        public string name { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }

        public Drink(string name, double price, int quantity)
        {
            this.name = name;
            this.price = price;
            this.quantity = quantity;
        }

        public void Give(int count )
        {
            if (this.quantity - count < 0)
            {
                throw new Exception("Drink is out of stock");
            }

            this.quantity -= count;

        }
    }
}
