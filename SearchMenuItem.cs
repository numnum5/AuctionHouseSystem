using UI;
using static System.Console;
namespace AuctionHouse;
/// <summary>
/// The search menu item searches for a product and displays subsequent dialogues.
/// </summary>
public class SearchMenuItem : MenuItem
{
    private Customer currentCustomer;
    private ProductCollection availableProducts;
    /// <summary>
    /// Initializes a new instance of the SearchMenuItem class.
    /// </summary>
    /// <param name="key">The unique key associated with the menu item.</param>
    /// <param name="text">The display text of the menu item.</param>
    /// <param name="currentCustomer">The current customer.</param>
    /// <param name="availableProducts">The available products to the current customer.</param>
    public SearchMenuItem(string key, string text, Customer currentCustomer, ProductCollection availableProducts) : base(key, text)
    {
        this.currentCustomer = currentCustomer;
        this.availableProducts = availableProducts;
    }
    public override void DoAction()
    {
        WriteLine();
        WriteLine("+----------------+");
        WriteLine("| Product Search |");
        WriteLine("+----------------+");
        WriteLine();
        

        string phrase = Helpers.ReadNonBlankString("Search phrase (ALL to match all products):", "Invalid input cannot be blank");

        if(phrase.ToLower() != "all") 
        {
            //Searches for products based on the search phrase
            availableProducts = availableProducts.SearchProduct(phrase);
        }

        if (availableProducts.Count == 0) 
        {
            WriteLine();
            WriteLine("No products match the search phrase.");
            WriteLine();
        }
        else
        {
            //Sorts the products and displays it a tabularised form.
            availableProducts.Sort();
            availableProducts.ListProducts();
            WriteLine();
            WriteLine("Product search complete.");
            WriteLine();
            DisplayBidDialogue();
        }
        WriteLine();
        WriteLine("Returning to Client Menu.");
        WriteLine();
    }
    /// <summary>
    /// Displays dialogue for placing bid.
    /// </summary>
    private void DisplayBidDialogue()
    {
        string input = Helpers.ReadYesOrNo("Would you like to place a bid (yes or no)?", "Please enter \"yes\" or \"no\"");
        
        if (input == "no")
        { 
            WriteLine();
            WriteLine("Bid postponed."); 
            WriteLine();
        }
        else 
        {
            SelectProductDialogue();
        }
    }
    /// <summary>
    /// Displays dialogue for selecting the product.
    /// </summary>
    private void SelectProductDialogue()
    {
        int rowCount = Helpers.ReadProductSelection($"Which product would you like to bid for (1..{availableProducts.Count})?", availableProducts.Count);
        Product selectedProduct = availableProducts.GetElement(rowCount-1);
        MakeBid(selectedProduct);
    }   
    /// <summary>
    /// Dialogue for making bid on the product.
    /// </summary>
    /// <param name="currentProduct">The selected product.</param>
    private void MakeBid(Product currentProduct)
    {
        Currency bidAmount;
        //Constantly prompts the user for the right bid amount.
        while (true)
        {
            bidAmount = Helpers.Read<Currency>("Please enter the bid amount:", "Must start with '$' is followed by a whole dollar amount, with no intervening spaces followed by '.' and exactly two decimal digits", Currency.TryParse);
            if (currentProduct.Bid == null) 
            {
                break;
            }
            //If the new bid amount is greater than the current one breaks the loop.
            else
            {
                if (currentProduct.Bid.CurrentBidAmount < bidAmount) break;
            }
            WriteLine("Bid amount must be greater than the existing bid");
        }
        //Creates a new bid and sets it to the currentProduct.
        Bid newBid = new(currentCustomer.Name, currentCustomer.EmailAddress, bidAmount);
        currentProduct.Bid = newBid;
        ChooseDeliverMethod(currentProduct);
    }
    /// <summary>
    /// Dialogue for choosing delivery method.
    /// </summary>
    /// <param name="selectedProduct">The selected product.</param>
    /// <exception cref="ArgumentException">Throws error if DeliverMethod or Bid on the selected product is null.</exception>
    private void ChooseDeliverMethod(Product selectedProduct)
    {
        string input = Helpers.ReadBinaryChoice("How would you like to receive the item (collect or deliver)?", "collect", "deliver", "Please input \"collect\" or \"deliver\" only");
        if (input == "collect") 
        {
            CollectionTimeDialogue(selectedProduct);
        }
        else 
        {
            DeliveryAddressDialogue(selectedProduct);
        }

        //Checks if delivermethod and bid in the product are null.
        if (selectedProduct.DeliveryMethod == null || selectedProduct.Bid == null) throw new ArgumentException();

        WriteLine();
        WriteLine("Bid successfully placed:"); 
        WriteLine($"\t{selectedProduct.Bid.CurrentBidAmount}\t{selectedProduct.Bid.BidderName}\t{selectedProduct.Bid.BidderEmail}\t{selectedProduct.DeliveryMethod.GetDeliveryOptionMessage()}");
    }
    /// <summary>
    /// Displays the dialogue choosing address delivery method.
    /// </summary>
    /// <param name="selectedProduct">The selected product.</param>
    /// <exception cref="ArgumentException">Throws error if current customer's home address is null.</exception>
    private void DeliveryAddressDialogue(Product selectedProduct)
    {
        string input = Helpers.ReadYesOrNo("Deliver to your home address (yes or no)?", "Please enter \"yes\" or \"no\"");

        IDeliveryMethod deliveryAddress;

        if (input == "yes") 
        {
            //Sets deliverAddress to Customer's home address.
            if (currentCustomer.HomeAddress == null) throw new ArgumentException();
            deliveryAddress = currentCustomer.HomeAddress;
        }
        else
        {
            WriteLine();
            WriteLine("+----------+");
            WriteLine("| delivery |");
            WriteLine("+----------+");
            WriteLine();
            deliveryAddress = Helpers.ReadAddress();
        }
        selectedProduct.DeliveryMethod = deliveryAddress;   
    }
    /// <summary>
    /// Displays the dialogue for choosing collection time.
    /// </summary>
    /// <param name="selectedProduct">The selected product.</param>
    private void CollectionTimeDialogue(Product selectedProduct)
    {
        IDeliveryMethod collectionTime = Helpers.ReadCollectionTime();
        selectedProduct.DeliveryMethod = collectionTime;
    }
}