using System;

class Program
{
    static void Main(string[] args)
    {
        Address address1 = new Address("123 Main Street", "Rexburg", "ID", "USA");
        Customer customer1 = new Customer("John Smith", address1);

        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Laptop Bag", "LB-101", 25.50, 2));
        order1.AddProduct(new Product("Wireless Mouse", "WM-202", 15.00, 1));
        order1.AddProduct(new Product("USB Cable", "UC-303", 4.25, 3));

        Address address2 = new Address("Rue des Jardins, Cocody", "Abidjan", "Abidjan", "Cote d'Ivoire");
        Customer customer2 = new Customer("Aya Kone", address2);

        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Phone Case", "PC-404", 8.99, 4));
        order2.AddProduct(new Product("Screen Protector", "SP-505", 3.50, 5));

        Order[] orders = { order1, order2 };

        foreach (Order order in orders)
        {
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine($"Total Price: ${order.GetTotalPrice():F2}");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();
        }
    }
}