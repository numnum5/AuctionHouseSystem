namespace AuctionHouse;
/// <summary>
/// A class for Bid
/// </summary>
public class Bid
{
    /// <summary>
    /// Get property for bidder name.
    /// </summary>
    public Name BidderName { get; }
    /// <summary>
    /// Get property for bidder email.
    /// </summary>
    public EmailAddress BidderEmail { get; }
    /// <summary>
    /// Get property for current bid amount.
    /// </summary>
    public Currency CurrentBidAmount {get;}
    /// <summary>
    /// Initializes a new instance of the Bid class
    /// </summary>
    /// <param name="bidderName">Name of bidder.</param>
    /// <param name="bidderEmail">Email address of bidder.</param>
    /// <param name="currentBidAmount">Bid amount currently set.</param>
    public Bid(Name bidderName, EmailAddress bidderEmail, Currency currentBidAmount)
    {
        BidderName = bidderName;
        BidderEmail = bidderEmail;
        CurrentBidAmount = currentBidAmount;
    }
    /// <summary>
    /// Converts the Bid object to its equivalent string representation.
    /// </summary>
    /// <returns>Formatted string representing bid.</returns>
    public override string ToString()
    {
        return $"{BidderName}\t{BidderEmail}\t{CurrentBidAmount}";
    }

}