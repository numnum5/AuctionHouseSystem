namespace AuctionHouse;
/// <summary>
/// AuctionSystem class stores collection of all products and all customers registered on the auction house.
/// </summary>
public class AuctionSystem
{
    // CustomerCollection of all the registered customers on the Auction House.
    public CustomerCollection Customers {get;} = new();

    // ProductCollection of all the registered products on the Auction House.
    public ProductCollection Products {get;} = new();
}