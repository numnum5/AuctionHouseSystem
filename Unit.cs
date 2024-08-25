namespace AuctionHouse;
/// <summary>
/// A class validating Unit objects from user input.
/// </summary>
public class Unit
{
    private uint ? unitNumber;
    /// <summary>
    /// Initializes a new instance of the Unit class.
    /// </summary>
    /// <param name="unitNumber">The unit number as a string which could be null.</param>
    /// <exception cref="ArgumentException">Thrown if the provided unit number is not valid.</exception>
    public Unit(string unitNumber)
    {
        if (!IsValid(unitNumber)) throw new ArgumentException();

        if (unitNumber.Trim() == "")
        {
            this.unitNumber = null;
        }
        else
        {
            this.unitNumber = uint.Parse(unitNumber);
        }
    }
    /// <summary>
    /// Checks if user input for unit number is valid.
    /// </summary>
    /// <param name="unitNumber">The unit number as a string.</param>
    /// <returns>True if the unit number is valid; otherwise, false.</returns>
    public static bool IsValid(string unitNumber)
    {
        if (unitNumber == null) return false;
        unitNumber = unitNumber.Trim();
        if (unitNumber == "") return true;
        if (!IsValidUnitNumber(unitNumber)) return false;
        return true;
    }
    /// <summary>
    /// Checks if unit number is only positive and non-zero.
    /// </summary>
    /// <param name="unitNumber">The unit number as a string.</param>
    /// <returns>True if the unit number is valid; otherwise, false.</returns>
    public static bool IsValidUnitNumber(string unitNumber)
    {
        uint value;
        // Tries parsing user input and if the value not 0 returns true.
        if (!uint.TryParse(unitNumber, out value) || value == 0) return false;
        return true;
    }
    /// <summary>
    /// Attempts to parse a string into a Unit instance.
    /// </summary>
    /// <param name="userInput">The user input string to parse.</param>
    /// <param name="result">The resulting Unit instance.</param>
    /// <returns>True if parsing is successful; otherwise, false.</returns>
    public static bool TryParse(string ? userInput, out Unit result)
    {
        result = new("");
        if (userInput == null) return false;
        if (!IsValid(userInput)) return false;
        result = new Unit(userInput.Trim());
        return true;
    }
    /// <summary>
    /// Converts an instance of Unit into string representation.
    /// </summary>
    /// <returns>Empty string if null or formated string if not.</returns>
    public override string ToString()
    {
        if (unitNumber == null) return "";
        return $"U{unitNumber} ";
    }
}


