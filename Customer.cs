namespace AuctionHouse;
/// <summary>
/// Customer class to represent a customer in the auction house
/// </summary>
public class Customer
{
    /// <summary>
    /// Get property for customer name.
    /// </summary>
    public Name Name { get; }
    /// <summary>
    /// Get property for customer emailaddress.
    /// </summary>
    public EmailAddress EmailAddress { get; }
    /// <summary>
    /// Get property for customer's password.
    /// </summary>
    public Password Password { get; }
    private Address ? homeAddress;

    /// <summary>
    /// Get and set the home address.
    /// </summary>
    public Address ? HomeAddress
    {
        get { return homeAddress; }
        set
        {
            if (value == null) throw new ArgumentException();
            homeAddress = value;
        }
    }
    /// <summary>
    /// A get property for customer's advertised products.
    /// </summary>
    public ProductCollection AdvertisedProducts { get; } = new();
    /// <summary>
    /// A get property for customer's purchased products.
    /// </summary>
    public ProductCollection PurchasedProducts { get; } = new();
    /// <summary>
    /// Initializes a new instance of the Customer class.
    /// </summary>
    /// <param name="name">The customer's name.</param>
    /// <param name="emailAddress">The customer's email address.</param>
    /// <param name="password">The customer's password.</param>
    public Customer(Name name, EmailAddress emailAddress, Password password)
    {
        Name = name;
        EmailAddress = emailAddress;
        Password = password;
    }
    /// <summary>
    /// Converts the Customer instance to its equivalent string representation
    /// </summary>
    /// <returns>The string representation of the Customer instance in a formatted form.</returns>
    public override string ToString() => $"{Name} ({EmailAddress})";
}