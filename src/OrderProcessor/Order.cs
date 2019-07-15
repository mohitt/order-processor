using System;

namespace OrderProcessor
{
    public class Order
    {
        public Order()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string CCNumber { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
    }
}