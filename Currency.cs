
namespace AuctionHouse;
/// <summary>
/// A class representing and validating Currency objects.
/// </summary>
public class Currency : IComparable
{
    /// <summary>
    /// A field for storing whole dollar amount.
    /// </summary>
    private string wholeAmount;
    /// <summary>
    /// A field for storing cent dollar amount.
    /// </summary>
    private string centAmount;
    /// <summary>
    /// Initializes a new instance of the Currency class.
    /// </summary>
    /// <param name="amount">A string representing the currency amount.</param>
    /// <exception cref="ArgumentException">Thrown if the provided amount is not valid.</exception>
    public Currency(string amount) 
    {   
        string[] parts = amount.Split('.');
        if (!IsValid(parts[0], parts[1])) throw new ArgumentException();
        wholeAmount = parts[0];
        centAmount = parts[1];
    }
    /// <summary>
    /// Validates the whole and cent amount parts of the currency object.
    /// </summary>
    /// <param name="wholeAmount">The whole dollar amount part of the currency.</param>
    /// <param name="centAmount">The cent dollar amount part of the currency.</param>
    /// <returns>True if the currency is valid; otherwise, false.</returns>
    public static bool IsValid(string wholeAmount, string centAmount)
    {
        if (wholeAmount == null) return false;
        if (centAmount == null) return false;
        wholeAmount = wholeAmount.Trim();
        centAmount = centAmount.Trim();
        if (wholeAmount.Length == 0 && centAmount.Length == 0) return false;
        if (!IsValidWholeAmount(wholeAmount)) return false;
        if ( !IsValidCentAmount(centAmount)) return false;
        return true;
    }
    /// <summary>
    /// Checks if the whole amount is valid.
    /// </summary>
    /// <param name="wholeAmount">The whole dollar amount part of the currency.</param>
    /// <returns>True if the whole amount is valid; otherwise, false.</returns>
    public static bool IsValidWholeAmount(string wholeAmount)
    {
        if (!wholeAmount.StartsWith("$")) return false;
        if (wholeAmount.Contains(" ")) return false;
        for (int i = 1; i < wholeAmount.Length; i++)
        {
            if(!Char.IsDigit(wholeAmount[i])) return false;
        }
        return true;
    }
    /// <summary>
    /// Checks if the cent amount is valid.
    /// </summary>
    /// <param name="centAmount">The cent dollar amount part of the currency.</param>
    /// <returns>True if the cent amount is valid; otherwise, false.</returns>
    public static bool IsValidCentAmount(string centAmount)
    {
        if (!(centAmount.Length == 2)) return false;

        //Checks if each character in centAmount is digit
        foreach (Char c in centAmount)
        {
            if (!Char.IsDigit(c))
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Tries to parse a string into a Currency object.
    /// </summary>
    /// <param name="userInput">The user input string to parse.</param>
    /// <param name="result">The resulting Currency object.</param>
    /// <returns>True if parsing is successful; otherwise, false.</returns>
    public static bool TryParse(string ? userInput, out Currency result)
    {
        result = new Currency("$0.00");
        if (userInput == null) return false;
        var parts = userInput.Split('.');
        if (parts.Length != 2) return false;
        if (!IsValid(parts[0], parts[1])) return false;
        result = new Currency(userInput);
        return true;
    }
    /// <summary>
    /// Converts the Currency object to its equivalent string representation.
    /// </summary>
    /// <returns>Returns a formatted string representation of Currency object.</returns>
    public override string ToString() => $"{wholeAmount}.{centAmount}";
    
    /// <summary>
    /// Compares two Currency objects.
    /// </summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns>A value indicating the relative order of the two Currency objects.</returns>
    public int CompareTo(object? obj)
    {
        // Check if the provided object is not null and is of type Currency
        if (obj is Currency other) 
        {
            // Compare sthe whole amount of the current object with that of the other object
            int result = Double.Parse(wholeAmount.Replace("$", "")).CompareTo(Double.Parse(other.wholeAmount.Replace("$", "")));

            // If the whole amounts are equal, compare the cent amounts
            if (result == 0) 
            {
                result = Double.Parse(centAmount).CompareTo(Double.Parse(other.centAmount));
            }

            // Return the result of the comparison
            return result;
        }
        else 
        {
            // Returns 1 if the current Currency object is greater than other
            return 1;
        }
    }
    /// <summary>
    /// Operator overload for "<=" to compare Currency objects. 
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>True if the left Currency object is less than or equal to the right; otherwise, false.</returns>
    public static bool operator <=(Currency left, Currency right)
    {
        return left.CompareTo(right) <= 0;
    }
    /// <summary>
    /// Operator overload for ">=" to compare Currency objects.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>True if the left Currency object is greater or equal than the right; otherwise, false.</returns>
    public static bool operator >=(Currency left, Currency right)
    {
        return left.CompareTo(right) >= 0;
    }
    /// <summary>
    /// Operator overload for "<" to compare Currency objects. 
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>True if the left Currency object is less than the right; otherwise, false.</returns>
    public static bool operator <(Currency left, Currency right)
    {
        return left.CompareTo(right) < 0;
    }
    /// <summary>
    /// Operator overload for ">" to compare Currency objects.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>True if the left Currency object is greater than the right; otherwise, false.</returns>
    public static bool operator >(Currency left, Currency right)
    {
        return left.CompareTo(right) > 0;
    }

}