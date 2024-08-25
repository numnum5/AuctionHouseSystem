namespace UI;
using AuctionHouse;
using static Console;
/// <summary>
/// Menu item for advertising product.
/// </summary>
public class AdvertiseProductMenuItem : MenuItem
{
    private Customer currentCustomer;
    private ProductCollection products;
    /// <summary>
    /// Initializes a new instance of the AdvertiseProductMenuItem class.
    /// </summary>
    /// <param name="key">A unique key associated with the menu item.</param>
    /// <param name="text">The display text or label for the menu item.</param>
    /// <param name="currentCustomer">The current customer.</param>
    /// <param name="products">A collection of products registred on the auction house.</param>
    public AdvertiseProductMenuItem(string key, string text, Customer currentCustomer, ProductCollection products) : base(key, text)
    {
        this.currentCustomer = currentCustomer;
        this.products = products;
    }
    public override void DoAction()
    {
        WriteLine();
        WriteLine("+-------------------+");
        WriteLine("| Advertise Product |");
        WriteLine("+-------------------+");
        WriteLine();

        //Prompts for product name.
        string productName = Helpers.ReadNonBlankString("Product name:", "Name cannot be empty");
        
        string productDescription;
        while (true)
        {
            //Prompts for product description.
            productDescription = Helpers.ReadNonBlankString("Product description:", "Description cannot be empty");
            //Checks if description is different to name.
            if (string.Compare(productDescription, productName) != 0)
            {
                break;
            }
            WriteLine("Product description cannot be the same as the name");
        }
        
        // Prompts for price.
        Currency price = Helpers.Read<Currency>("Sale price:", "Must start with '$' is followed by a whole dollar amount, with no intervening spaces followed by '.' and exactly two decimal digits", Currency.TryParse);
        Product newProduct = new(productName, productDescription, price, currentCustomer.EmailAddress);

        // Registers the products on the auction house as well as on the customer's advertised products.
        products.Add(newProduct);
        currentCustomer.AdvertisedProducts.Add(newProduct);

        WriteLine();
        WriteLine("Successfully created advertisement:");
        WriteLine($"\t{productName} - {productDescription} {price}");
        WriteLine();
        WriteLine("Returning to the Client Menu.");
        WriteLine();
    }
}
