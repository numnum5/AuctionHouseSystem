namespace AuctionHouse;
/// <summary>
/// Represents a product in the auction house.
/// </summary>
public class Product : IComparable
{
    /// <summary>
    /// Get property for Name.
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// Get property for Description.
    /// </summary>
    public string Description { get; }
    /// <summary>
    /// Get property for Price.
    /// </summary>
    public Currency Price { get; }
    /// <summary>
    /// Get property for SellerEmailAddress.
    /// </summary>
    public EmailAddress SellerEmailAddress { get; }
    private Bid ? bid;
    private IDeliveryMethod ? deliveryMethod;
    /// <summary>
    /// Gets or sets the bid associated with the product.
    /// </summary>
    public Bid ? Bid
    {
        get { return bid; }
        set
        {
            if (value == null) throw new ArgumentException();
            bid = value;
        }
    }
    /// <summary>
    /// Gets or sets the delivery method associated with the product.
    /// </summary>
    public IDeliveryMethod? DeliveryMethod
    {
        get { return deliveryMethod; }
        set
        {
            if (value == null) throw new ArgumentException();
            deliveryMethod = value;
        }
    }
    /// <summary>
    /// Initializes a new instance of the Product class.
    /// </summary>
    /// <param name="name">The name of the product.</param>
    /// <param name="description">The description of the product.</param>
    /// <param name="price">The price of the product.</param>
    /// <param name="emailAddress">The seller's email address associated with the product.</param>
    public Product(string name, string description, Currency price, EmailAddress emailAddress)
    {
        Name = name;
        Description = description;
        Price = price;
        SellerEmailAddress = emailAddress;
    }
    /// <summary>
    /// Returns a string representation of the product in a formatted form.
    /// </summary>
    public override string ToString() => $"{Name}\t{Description}\t{Price}";

    /// <summary>
    /// Compares two products by their unique identifier (Seller email address).
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns>True if the seller email addresses match, false otherwise.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is Product other)
        {
            return SellerEmailAddress.Equals(other.SellerEmailAddress);
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Compares this product to another for sorting purposes.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns>0 if equal, a positive value if greater, and a negative value if smaller.</returns>
    public int CompareTo(object? obj)
    {
        if (obj is Product other)
        {
            int result = string.Compare(Name, other.Name);
            if (result == 0) result = string.Compare(Description, other.Description);
            if (result == 0) result = Price.CompareTo(other.Price);
            return result;
        }
        else
        {
            return 1;
        }
    }
    /// <summary>
    /// Had to be implemented to remove warning.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}

