namespace UI;
using AuctionHouse;
using static Console;

/// <summary>
/// Represents a menu item responsible for registering new users to the auction house.
/// </summary>
public class RegistrationMenuItem : MenuItem
{
    private CustomerCollection customers;
    /// <summary>
    /// Initializes a new instance of the RegistrationMenuItem class.
    /// </summary>
    /// <param name="key">The unique key associated with the menu item.</param>
    /// <param name="text">The display text of the menu item.</param>
    /// <param name="customers">The customers registered on the aunction house.</param>
    public RegistrationMenuItem(string key, string text, CustomerCollection customers) : base(key, text)
    {
        this.customers = customers;
    }
    public override void DoAction()
    {
        WriteLine();
        WriteLine("+-------------------+");
        WriteLine("| Register Customer |");
        WriteLine("+-------------------+");
        WriteLine();

        // Prompts for name, emailAddress, and password
        Name name = Helpers.Read<Name>("Name:", "Valid names consist of letters, spaces, apostrophes, and dashes, and start and finish with a letter.", Name.TryParse);
        EmailAddress emailAddress = Helpers.Read<EmailAddress>("Email address:", "Valid email addresses consist of two parts separated by '@', according to normal conventions; the domain part must contain at least 1 '.'", EmailAddress.TryParse);
        Password password = Helpers.Read<Password>("Password:", "Password must contain at least 1 upper, lower, digit, and non-alphanumeric characters and must be at least 8 characters long", Password.TryParse);
        
        // Check if the customer with the provided email address already exists
        Customer ? currentCustomer = customers.FindCustomer(emailAddress);

        if (currentCustomer != null)
        {
            // Inform the user if the email is already in use
            WriteLine("Email already in use. Operation cancelled.");
            WriteLine();
            WriteLine("Returning to MainMenu.");
        }
        else
        {
            // Register a new customer if the email is not in use
            Customer newCustomer = new(name, emailAddress, password);
            customers.Add(newCustomer);
            WriteLine();
            WriteLine("Successfully registered as:");
            WriteLine($"\t{newCustomer}.");
            WriteLine();
            WriteLine("Returning to the main menu to sign in.");
            WriteLine();
        }
    }
}
