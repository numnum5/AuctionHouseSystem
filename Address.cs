namespace AuctionHouse;
/// <summary>
/// A class for Address
/// </summary>
public class Address : IDeliveryMethod
{
    /// <summary>
    /// The unit associated with the address.
    /// </summary>
    private Unit unit;
    /// <summary>
    /// Field for storing street number.
    /// </summary>
    private uint streetNumber;
    /// <summary>
    /// Field for storing street name.
    /// </summary>
    private string streetName;
    /// <summary>
    /// Field for storing street type.
    /// </summary>
    private StreetType streetType;
    /// <summary>
    /// Field for storing city.
    /// </summary>
    private string city;
    /// <summary>
    /// Field for storing post code.
    /// </summary>
    private uint postCode;
    private State state;
    /// <summary>
    /// Initializes a new instance of the Address class with the specified parameters.
    /// </summary>
    /// <param name="unit">The unit of the address.</param>
    /// <param name="streetNumber">The numeric value indicating the street number.</param>
    /// <param name="streetName">The name of the street.</param>
    /// <param name="streetType">The type of the street.</param>
    /// <param name="city">The city where the address is located.</param>
    /// <param name="postCode">The postal code of the address.</param>
    /// <param name="state">The state where the address is situated.</param>
    public Address(Unit unit, uint streetNumber, string streetName, StreetType streetType, string city, uint postCode, State state)
    {   
        this.unit = unit;
        this.streetNumber = streetNumber;
        this.streetName = streetName;
        this.streetType = streetType;
        this.city = city;
        this.postCode = postCode;
        this.state = state;
    }
    /// <summary>
    /// Converts the Address object to its equivalent string representation.
    /// </summary>
    /// <returns>A formatted string representing the Address, including unit, street number, street name, street type, city, state, and postal code.</returns>
    public override string ToString()
    {
        return $"{unit}{streetNumber} {streetName} {streetType}, {city} {state} {postCode}";
    }
    /// <summary>
    /// Gets the delivery option message for the current address.
    /// </summary>
    /// <returns>Formatted string for delivery message.</returns>
    public string GetDeliveryOptionMessage()
    {
        return $"Deliver to {this}";
    }
}

