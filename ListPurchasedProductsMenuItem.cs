using UI;
using static System.Console;
namespace AuctionHouse;

/// <summary>
/// Represents a menu item that lists the purchased items.
/// </summary>
public class ListPurchasedProductsMenuItem : MenuItem
{
    private ProductCollection purchasedProducts;
    /// <summary>
    /// Initializes a new instance of the ListPurchasedProductsMenuItem class.
    /// </summary>
    /// <param name="key">The unique key associated with the menu item.</param>
    /// <param name="text">The display text of the menu item.</param>
    /// <param name="purchasedProducts">The current customer's purchased products.</param>
    public ListPurchasedProductsMenuItem(string key, string text, ProductCollection purchasedProducts) : base(key, text)
    {
        this.purchasedProducts = purchasedProducts;
    }
    public override void DoAction()
    {
        WriteLine("+--------------------+");
        WriteLine("| Purchased Products |");
        WriteLine("+--------------------+");
        WriteLine();

        if (purchasedProducts.Count == 0)
        {
            // Inform the user if there are no purchased items
            WriteLine();
            WriteLine("You have not purchased any items so far.");
            WriteLine();
        }
        else
        {
            // Sort and display the list of purchased products
            purchasedProducts.Sort();
            purchasedProducts.ListPurchasedProducts();
            WriteLine("Purchase list complete.");
            WriteLine();
        }

        WriteLine("Returning to Client Menu.");
        WriteLine();
    }
}

