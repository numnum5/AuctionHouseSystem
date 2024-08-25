using UI;
using static System.Console;
namespace AuctionHouse;
/// <summary>
/// A class that deals with displaying and selling bid products.
/// </summary>
public class ListBidProductsMenuItem : MenuItem
{
    private ProductCollection products;
    private ProductCollection advertisedProducts;
    private CustomerCollection customers;
    /// <summary>
    /// Initializes a new instance of the ListBidProductsMenuItem class.
    /// </summary>
    /// <param name="key">A unique key associated with the menu item.</param>
    /// <param name="text">The display text or label for the menu item.</param>
    /// <param name="customers">The registered customers on the aunction house.</param>
    /// <param name="products">The registered products on the auction house.</param>
    /// <param name="bidProducts">The advertised products by the current customer</param>
    public ListBidProductsMenuItem(string key, string text, CustomerCollection customers, ProductCollection products, ProductCollection advertisedProducts) : base(key, text)
    {
        this.customers = customers;
        this.products = products;
        this.advertisedProducts = advertisedProducts;
    }
    public override void DoAction()
    {
        WriteLine("+--------------+");
        WriteLine("| Product Bids |");
        WriteLine("+--------------+");

        // Gets the producst with bid from the customer.
        ProductCollection bidProducts = advertisedProducts.GetBidProducts();

        if (bidProducts.Count == 0)
        {
            WriteLine();
            WriteLine("You have no products with bids at this time.");
            WriteLine();
        }
        else
        {
            // Sorts and displays the products in a tabularised form.
            bidProducts.Sort();
            bidProducts.ListProducts();
            WriteLine("Bid list complete.");
            WriteLine();
            DisplaySellDialogue(bidProducts);
        }
        WriteLine("Returning to Client Menu.");
    }
    /// <summary>
    /// A method which displays the dialogue for selling choice
    /// </summary>
    private void DisplaySellDialogue(ProductCollection bidProducts)
    {
        string input = Helpers.ReadYesOrNo("Would you like to sell a product to the highest bidder?", "Please enter \"yes\" or \"no\"");
        //If user input is "yes" display subsequent message.
        if (input == "yes")
        {
            //Gets the row number of chosen product.
            int rowCount = Helpers.ReadProductSelection($"Item number  (1..{bidProducts.Count})", bidProducts.Count);
            
            //Gets the selected product.
            Product selectedProduct = bidProducts.GetElement(rowCount - 1);

            //Displays subseqent dialogue.
            SellProductDialogue(selectedProduct);
        }
        else
        {
            WriteLine("Sale postponed");
        }
    }
    /// <summary>
    /// A method which sells the product, removing the product from the listing and adding it to buyer's purchasedproducts.
    /// </summary>
    /// <param name="selectedProduct">THe selected product.</param>
    /// <exception cref="ArgumentException">Throws error if buyer is null.</exception>
    private void SellProductDialogue(Product selectedProduct)
    {
        // Checks if the Bid is null.
        if (selectedProduct.Bid == null) throw new ArgumentException();
        Customer ? buyer = customers.FindCustomer(selectedProduct.Bid.BidderEmail);

        // Checks if buyer is null throws Error.
        if (buyer == null) throw new ArgumentException();

        // Removes and adds the product where necessary.
        buyer.PurchasedProducts.Add(selectedProduct);
        advertisedProducts.Remove(selectedProduct);
        products.Remove(selectedProduct);

        // Prompts sold message.
        WriteLine($"Product {selectedProduct.Name} sold to {buyer.Name} for {selectedProduct.Bid.CurrentBidAmount}");

        if (selectedProduct.DeliveryMethod == null) throw new ArgumentException();

        // Prompts arrangement message.
        WriteLine($"\tCollection arrangement: {selectedProduct.DeliveryMethod.GetDeliveryOptionMessage()}");
        WriteLine();
    }
}