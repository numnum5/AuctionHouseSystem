namespace AuctionHouse;

/// <summary>
/// A Name class which validates user input.
/// </summary>
public class Name: IComparable
{
    private string rawName;
    /// <summary>
    /// Initializes a new instance of the Name class.
    /// </summary>
    /// <param name="name">String user input of raw name.</param>
    /// <exception cref="ArgumentException">Throws error if not valid otherwise sets name.</exception>
    public Name(string name)
    {
        if (!IsValid(name)) throw new ArgumentException();
        rawName = name;
    }
    /// <summary>
    /// A method for validating name input.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool IsValid(string name)
    {
        if (name == null) return false;
        name = name.Trim();
        if (name.Length == 0) return false;
        //Checks if the first and last character are letters
        if (!Char.IsLetter(name[0])) return false;
        if (!Char.IsLetter(name[name.Length-1])) return false;
        if (name.Length > 0 && !IsValidNames(name)) return false;
        return true;
    }
    /// <summary>
    /// Check for valid Name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private static bool IsValidNames(string name)
    {
        // Splits the name by the specified delimeters.
        var parts = name.Split(['\'', '-', ' ']);
        foreach (var part in parts)
        {
            if (!IsLetters(part)) return false;
        }
        return true;
    }
    /// <summary>
    /// Checks if a string is letters only.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>True if the string is all letters</returns>
    private static bool IsLetters(string name)
    {
        if (name.Length == 0) return false;
        foreach (Char character in name)
        {
            if (!Char.IsLetter(character)) return false;
        }
        return true;
    }
    public static bool TryParse(string? userInput, out Name result)
    {
        result = new Name("Default");
        if (userInput == null) return false;
        if (!IsValid(userInput)) return false;
        result = new Name(userInput.Trim());
        return true;
    }
    /// <summary>
    /// Converts the Name object into string representation.
    /// </summary>
    /// <returns>String of Name object.</returns>
    public override string ToString() => rawName;    
    /// <summary>
    /// Compares two Name objects.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>Result for comparison.</returns>
    public int CompareTo(object ? obj)
    {
        if (obj is Name other ) {
            int result = rawName.CompareTo(other.rawName);
            return result;
        }
        else {
            return 1;
        }
    }
}

