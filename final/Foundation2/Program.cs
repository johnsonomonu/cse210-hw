using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Create two orders with products and customers
        Order order1 = new Order(new List<Product>
        {
            new Product("Product1", "P001", 10.0m, 2),
            new Product("Product2", "P002", 15.0m, 3),
            new Product("Product3", "P003", 20.0m, 1)
        }, new Customer("John Doe", new Address("123 Main St", "Cityville", "CA", "USA")));

        Order order2 = new Order(new List<Product>
        {
            new Product("Product4", "P004", 25.0m, 1),
            new Product("Product5", "P005", 30.0m, 2)
        }, new Customer("Jane Doe", new Address("456 Oak St", "Townsville", "NY", "Canada")));

        // Call methods to get packing label, shipping label, and total price for each order
        DisplayOrderDetails(order1);
        DisplayOrderDetails(order2);
    }

    static void DisplayOrderDetails(Order order)
    {
        // Display packing label
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order.GeneratePackingLabel());

        // Display shipping label
        Console.WriteLine("\nShipping Label:");
        Console.WriteLine(order.GenerateShippingLabel());

        // Display total price
        Console.WriteLine($"\nTotal Price: ${order.CalculateTotalCost()}\n");
    }
}

class Order
{
    private List<Product> _products;
    private Customer _customer;

    public Order(List<Product> products, Customer customer)
    {
        _products = products;
        _customer = customer;
    }

    public decimal CalculateTotalCost()
    {
        decimal totalCost = _products.Sum(product => product.Price * product.Quantity);
        totalCost += _customer.IsInUSA() ? 5.0m : 35.0m; // Shipping cost
        return totalCost;
    }

    public string GeneratePackingLabel()
    {
        return string.Join("\n", _products.Select(product => $"{product.Name} - {product.ProductId}"));
    }

    public string GenerateShippingLabel()
    {
        return $"Name: {_customer.Name}\nAddress: {_customer.GetFullAddress()}";
    }
}

class Product
{
    private string _name;
    private string _productId;
    private decimal _price;
    private int _quantity;

    public Product(string name, string productId, decimal price, int quantity)
    {
        _name = name;
        _productId = productId;
        _price = price;
        _quantity = quantity;
    }

    public string Name => _name;
    public string ProductId => _productId;
    public decimal Price => _price;
    public int Quantity => _quantity;
}

class Customer
{
    private string _name;
    private Address _address;

    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    public string Name => _name;

    public bool IsInUSA()
    {
        return _address.IsInUSA();
    }

    public string GetFullAddress()
    {
        return _address.GetFullAddress();
    }
}

class Address
{
    private string _streetAddress;
    private string _city;
    private string _stateProvince;
    private string _country;

    public Address(string streetAddress, string city, string stateProvince, string country)
    {
        _streetAddress = streetAddress;
        _city = city;
        _stateProvince = stateProvince;
        _country = country;
    }

    public bool IsInUSA()
    {
        return _country.Equals("USA", StringComparison.OrdinalIgnoreCase);
    }

    public string GetFullAddress()
    {
        return $"{_streetAddress}\n{_city}, {_stateProvince}\n{_country}";
    }
}
