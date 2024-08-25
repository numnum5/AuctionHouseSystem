namespace AuctionHouse;

using System.Text;
using static System.Console;

/// <summary>
/// ProductCollection class that has a list of products as well as related functions.
/// </summary>
public class ProductCollection
{
    /// <summary>
    /// A field for storing a list of products.
    /// </summary>S
    private List<Product> products = new();
    /// <summary>
    /// Get property for products returns IEnumerable of product type.
    /// </summary>
    public IEnumerable<Product> Products => products;
    /// <summary>
    /// Get property which returns the count of products.
    /// </summary>
    public int Count => products.Count;
    /// <summary>
    /// Sorts the elements in the entire ProductCollection using the default comparer.
    /// </summary>
    public void Sort()
    {
        products.Sort();
    }
    /// <summary>
    /// Gets the element at the specified index.
    /// </summary>
    /// <param name="index">Index number</param>
    /// <returns>Element at the index in the ProductCollection.</returns>
    public Product GetElement(int index)
    {
        return products[index];
    }
    /// <summary>
    /// Adds an object to the end of the ProductCollection.
    /// </summary>
    /// <param name="product">A Product object</param>
    public void Add(Product product)
    {
        products.Add(product);
    }
    /// <summary>
    /// Removes the first occurrence of a specific object from the List.
    /// </summary>
    /// <param name="product">A product object</param>
    public void Remove(Product product)
    {
        products.Remove(product);
    }
    /// <summary>
    /// Removes overlapping products between two ProductCollection objects.
    /// </summary>
    /// <param name="other"></param>
    public void RemoveOverlappingProducts(ProductCollection other)
    {
        // Removes products by comparing the products.
        foreach (Product product in other.Products)
        {
            products.RemoveAll(item => item.Equals(product));
        }
    }
    /// <summary>
    /// Searches for products based on the phrase provided.
    /// </summary>
    /// <param name="phrase">Search phrase.</param>
    /// <returns>A ProductCollection of search results.</returns>
    public ProductCollection SearchProduct(string phrase)
    {
        ProductCollection searchResults = new();
        //Checks if the phrase is contained in the description or the name of the product.
        foreach(Product product in Products)
        {
            if(product.Name.ToLower().Contains(phrase.ToLower()) || product.Description.ToLower().Contains(phrase.ToLower()))
            {
                searchResults.Add(product);
            }
        }
        return searchResults;
    }
    /// <summary>
    /// Adds range to ProductCollection
    /// </summary>
    /// <param name="other">A ProductCollection object</param>
    public void AddRange(ProductCollection other)
    {
        products.AddRange(other.Products);
    }
    /// <summary>
    /// Lists the products in a tabulriased format.
    /// </summary>
    public void ListProducts()
    {
        int count = 1;
        WriteLine("Number\tName\tDescription\tPrice\tBidder name\tBidder email\tBid price");
        foreach(Product product in Products)
        {   
            // Formats the bid message to "-" if its null;
            string bid = product.Bid?.ToString() ?? $"-\t-\t-";
            WriteLine($"{count}\t{product}\t{bid}");
            count++;
        }
    }
    /// <summary>
    /// Lists the products in a tabularised format for purchased products.
    /// </summary>
    public void ListPurchasedProducts()
    {
        int count = 1;
        WriteLine("Number\tSeller Email\tName\tDescription\tList Price\tAmount Paid\tDelivery Option");
        foreach(Product product in Products)
        {
            if (product.Bid == null || product.DeliveryMethod == null) throw new ArgumentException();
            WriteLine($"{count}\t{product.SellerEmailAddress}\t{product}\t{product.Bid.CurrentBidAmount}\t{product.DeliveryMethod.GetDeliveryOptionMessage()}");
            count++;
        }
    }
    /// <summary>
    /// A method which returns current customer's products with existing bids.
    /// </summary>
    /// <returns></returns>
    public ProductCollection GetBidProducts()
    {
        ProductCollection bidProducts = new();

        //Checks if the product has a bid and adds to bidProducts
        foreach (Product product in Products)
        {
            if (product.Bid != null) bidProducts.Add(product);
        }
        return bidProducts;
    }
    /// <summary>
    /// Creates a new instance of ProductCollection containing products available to a specific user.
    /// </summary>
    /// <param name="advertisedProducts">Customer's advertised products.</param>
    /// <returns>Avaiable products to the Customer which excludes their advertised products.</returns>
    public ProductCollection GetCurrentUserAvailableProducts(ProductCollection advertisedProducts)
    {
        ProductCollection availableProducts = new();
        // Adds all the products registered to the auction house to the availble products.
        availableProducts.AddRange(this);
        // Removes current customer's advertised products from products listed on the auction house
        availableProducts.RemoveOverlappingProducts(advertisedProducts);
        return availableProducts;
    }
    /// <summary>
    /// Converts a ProductCollection object into a string representation for debugging purposes.
    /// </summary>
    /// <returns>A string consisting of all the Products.</returns>
    public override string ToString()
    {
        StringBuilder productsString = new();

        foreach (Product product in products)
        {
            productsString.AppendLine(product.ToString());
        }
        return productsString.ToString();
    }
}