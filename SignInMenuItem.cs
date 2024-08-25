namespace UI;
using AuctionHouse;
using static System.Console;
/// <summary>
/// A menuitem that signs the user and creates the client menu for the user.
/// </summary>
public class SignInMenuItem : Menu
{
    private CustomerCollection customers;
    private ProductCollection products;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key">The unique key associated with the menu item.</param>
    /// <param name="text">The display text of the menu item./param>
    /// <param name="title">The title text of the menu.</param>
    /// <param name="products">The regisered products on the auction house.</param>
    /// <param name="customers">The registered customers on the auction house.</param>
    /// <param name="items">The list of menu items in the menu.</param>
    public SignInMenuItem(string key, string text, string title, ProductCollection products, CustomerCollection customers, params MenuItem[] items) : 
        base(key, text, title, items)
    {   
        this.customers = customers;
        this.products = products;
    }
    public override void DoAction()
    {
        WriteLine();
        WriteLine("+------------------+");
        WriteLine("| Customer Sign In |");
        WriteLine("+------------------+");
        WriteLine();
        
        //Signs the currentCustomer
        Customer ? currentCustomer = SignInUser();
        
        //If currentCustomer is null displays subsequent message and returns to Main Menu
        if (currentCustomer == null)
        {
            WriteLine("Wrong email or password.");
            WriteLine();
            WriteLine("Returning to Main Menu.");
            return;
        }
        
        //Adds the 6 menu items to the SignInMenuItem with necessary arguments passed.
        items.Add(new AdvertiseProductMenuItem("1", "Advertise product", currentCustomer, products));
        items.Add(new ListAdvertisedProductsMenuItem("2", "List my advertised products", currentCustomer.AdvertisedProducts));
        items.Add(new SearchMenuItem("3", "Search for products to buy", currentCustomer, products.GetCurrentUserAvailableProducts(currentCustomer.AdvertisedProducts)));
        items.Add(new ListBidProductsMenuItem("4", "Display bids for my products", customers, products, currentCustomer.AdvertisedProducts));
        items.Add(new ListPurchasedProductsMenuItem("5", "Show my purchases", currentCustomer.PurchasedProducts));
        items.Add(new LogOutMenuItem("6", "Log out"));

        WriteLine();
        WriteLine($"Successfully signed in as {currentCustomer}");
        WriteLine();

        //Checks if currentCustomer's HomeAddress is empty/null
        if (currentCustomer.HomeAddress == null)
        {
            WriteLine();
            WriteLine("+-----------------------+");
            WriteLine("| home delivery address |");
            WriteLine("+-----------------------+");
            WriteLine();

            Address newAddress = Helpers.ReadAddress();
            currentCustomer.HomeAddress = newAddress;
            WriteLine();
            WriteLine($"Address successfully recorded as:");
            WriteLine($"\t{newAddress}");
            WriteLine();
            WriteLine("Continuing to Client Menu.");
            WriteLine();
        }

        //Starts the client menu.
        base.DoAction();

        // After the user logs out, clears the menuitems so that the next user can use their-specific menu items.
        items.Clear();
    }
    /// <summary>
    /// Signs the customer by finding and returning the customer
    /// </summary>
    /// <returns>Current Customer if found or returns null if not</returns>
    private Customer ? SignInUser()
    {
        //Prompts for email and password
        EmailAddress emailAddress = Helpers.Read<EmailAddress>("Email address:", "Valid email addresses consist of two parts separated by '@', according to normal conventions; the domain part must contain at least 1 '.'", EmailAddress.TryParse);
        Password password = Helpers.Read<Password>("Password:", "Password must contain at least 1 upper, 1 lower, 1 digit, and 1 non-alphanumeric characters, and must be at least 8 characters long", Password.TryParse);
        
        //Finds the customer based on their emailaddress
        Customer ? currentCustomer = customers.FindCustomer(emailAddress);

        //Checks if currentCustomer exists and the given password matches
        if (currentCustomer != null && currentCustomer.Password.Equals(password))
        {
            return currentCustomer;
        }
        return null;
    }
}