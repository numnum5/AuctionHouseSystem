namespace AuctionHouse;

/// <summary>
/// Represents a collection of customers and provides methods for managing them.
/// </summary>
public class CustomerCollection
{
    /// <summary>
    /// A field to store a list of customers.
    /// </summary>
    private List<Customer> customers = new();

    /// <summary>
    /// Gets an enumerable collection of customers.
    /// </summary>
    public IEnumerable<Customer> Customers => customers;

    /// <summary>
    /// Adds a new customer to the collection.
    /// </summary>
    /// <param name="customer">The customer to add.</param>
    /// <exception cref="ArgumentException">Thrown if a customer with the same email already exists.</exception>
    public void Add(Customer customer)
    {
        if (FindCustomer(customer.EmailAddress) != null)
        {
            throw new ArgumentException();
        }
        customers.Add(customer);
    }
    /// <summary>
    /// Finds a customer in the collection on the provided email address.
    /// </summary>
    /// <param name="emailAddress">The email address to search for.</param>
    /// <returns>The found customer or null if not found.</returns>
    public Customer ? FindCustomer(EmailAddress emailAddress)
    {
        foreach (Customer customer in Customers)
        {
            if (customer.EmailAddress.Equals(emailAddress))
            {
                return customer;
            }
        }
        return null;
    }
}
