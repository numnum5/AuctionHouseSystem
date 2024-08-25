namespace AuctionHouse;
/// <summary>
/// A class for CollectionTime for storing the starttime and endTime for collection time
/// </summary>
public class CollectionTime : IDeliveryMethod
{
    /// <summary>
    /// A field for storing start time of collect delivery.
    /// </summary>
    private DateTime startTime;
    /// <summary>
    /// A field for storing end time of collect delivery.
    /// </summary>
    private DateTime endTime;
    /// <summary>
    /// Initializes a new instance of the CollectionTime class, representing a time range for collection pick up.
    /// </summary>
    /// <param name="startTime">The start time of the collection period.</param>
    /// <param name="endTime">The end time of the collection period.</param>
    public CollectionTime(DateTime startTime, DateTime endTime)
    {
        this.startTime = startTime;
        this.endTime = endTime;
    }
    /// <summary>
    /// A method which returns deliver message.
    /// </summary>
    /// <returns>Returns formatted string.</returns>
    public string GetDeliveryOptionMessage()
    {
        return $"Pick up between {this}";
    }
    /// <summary>
    /// Converts the CollectionTime object to its equivalent string representation.
    /// </summary>
    /// <returns>Returns a formatted string representation of CollectionTime.</returns>
    public override string ToString()
    {
        return $"{startTime} and {endTime}";
    }
}