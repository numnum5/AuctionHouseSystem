namespace AuctionHouse;
/// <summary>
/// Interface for DeliveryMethod which are inherited by Address and CollectionTime classes.
/// </summary>
public interface IDeliveryMethod
{
    string GetDeliveryOptionMessage();
}