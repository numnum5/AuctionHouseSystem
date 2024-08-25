using UI; 
using static System.Console;
namespace AuctionHouse;

/// <summary>
/// Represents a menu item for listing a Customer's advertised products.
/// </summary>
public class ListAdvertisedProductsMenuItem : MenuItem
{
    private ProductCollection advertisedProducts;
    /// <summary>
    /// Initializes a new instance of the ListAdvertisedProductsMenuItem class.
    /// </summary>
    /// <param name="key">A unique key associated with the menu item.</param>
    /// <param name="text">The display text or label for the menu item.</param>
    /// <param name="advertisedProducts">Advertised products by the current customer.</param>
    public ListAdvertisedProductsMenuItem(string key, string text, ProductCollection advertisedProducts) : base(key, text)
    {
        this.advertisedProducts = advertisedProducts;
    }
    public override void DoAction()
    {
        WriteLine();
        WriteLine("+--------------+");
        WriteLine("| Product List |");
        WriteLine("+--------------+");
        WriteLine();

        if (advertisedProducts.Count == 0)
        {
            WriteLine();
            WriteLine("You have no advertised products at this time.");
            WriteLine();
        }
        else
        {
            //Sorts and displays the products in tabularised form.
            advertisedProducts.Sort();
            advertisedProducts.ListProducts();
            WriteLine();
            WriteLine("Product list complete.");
            WriteLine();
        }

        WriteLine("Returning to Client Menu.");
        WriteLine();
    }
}

