namespace AuctionHouse;
using static Console;

public delegate bool Parser<T>( string ? userInput, out T result);

/// <summary>
/// Helpers class for storing reused functions throughout the program.
/// </summary>
public static class Helpers {
    public static T Read<T>(string prompt, string formatError, Parser<T> tryParse) {
        T result;
        while (true) {
            WriteLine(prompt);

            string ? userInput = ReadLine();

            if (userInput is null) throw new EndOfStreamException();

            if (tryParse(userInput, out result)) 
            {
                break;
            }
            WriteLine(formatError);
        }
        return result;
    }
    /// <summary>
    /// Method for reading non blank and yes or no option
    /// </summary>
    /// <param name="prompt"></param>
    /// <param name="formatError"></param>
    /// <returns>Validated user's preffered option</returns>
    /// <exception cref="EndOfStreamException"></exception>
    public static string ReadYesOrNo(string prompt, string formatError)
    {
        string result;
        while (true)
        {
            WriteLine(prompt);

            string ? userInput = ReadLine();

            if ( userInput is null ) throw new EndOfStreamException();

            if (!(userInput.Trim().Length == 0))
            {
                if (userInput.Trim().ToLower() == "yes" || userInput.Trim().ToLower() == "no")
                {
                    result = userInput;
                    break;
                }
            }
            WriteLine(formatError);
        }
        return result;
    }
    /// <summary>
    /// A method for reading users' binary choice input
    /// </summary>
    /// <param name="prompt"></param>
    /// <param name="firstChoice"></param>
    /// <param name="secondChoice"></param>
    /// <param name="formatError"></param>
    /// <returns>Returns validated user input in string</returns>
    /// <exception cref="EndOfStreamException"></exception>
    public static string ReadBinaryChoice(string prompt, string firstChoice, string secondChoice, string formatError)
    {
        string result;
        while (true)
        {
            WriteLine(prompt);

            string ? userInput = ReadLine();

            if ( userInput is null ) throw new EndOfStreamException();

            if (!(userInput.Trim().Length == 0))
            {
                if (userInput.Trim().ToLower() == firstChoice || userInput.Trim().ToLower() == secondChoice)
                {
                    result = userInput;
                    break;
                }
            }
            WriteLine(formatError);
        }
        return  result;
    }
    /// <summary>
    /// Reads non blank string.
    /// </summary>
    /// <param name="prompt"></param>
    /// <param name="formatError"></param>
    /// <returns>Validated non-blank string</returns>
    /// <exception cref="EndOfStreamException"></exception>
    public static string ReadNonBlankString(string prompt, string formatError)
    {
        string result;
        while (true)
        {
            WriteLine(prompt);

            string ? userInput = ReadLine();

            if (userInput is null) throw new EndOfStreamException();

            if (!(userInput.Trim().Length == 0))
            {
                result = userInput;
                break;
            }
            WriteLine(formatError);
        }
        return  result.Trim();
    }
    /// <summary>
    /// A method parsing Enum class data types.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="userInput"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool TryParseEnums<T>(string  ? userInput, out T result) where T : struct
    {
        // Default value in case of failure.
        result = default(T); 
        //Checks if the input is null or empty and if it is all digits
        if (!string.IsNullOrWhiteSpace(userInput) && !userInput.All(char.IsDigit))
        {
            return Enum.TryParse<T>(userInput, ignoreCase: true, out result);
        }
        return false;
    }
    /// <summary>
    /// Reads street number.
    /// </summary>
    /// <returns>Validated street number</returns>
    public static uint ReadStreetNumber()
    {
        uint streetNumber;
        while (true)
        {
             // Checks if street number is in valid range.
            streetNumber = Read<uint>("Street number:", "Please input a non-zero postive interger", uint.TryParse);
            if (streetNumber != 0) break;
            WriteLine("Must be non-zero positive interger");
        }
        return streetNumber;
    }
    /// <summary>
    /// Reads post code.
    /// </summary>
    /// <returns>Validated post code</returns>
    public static uint ReadPostCode()
    {
        uint postCode;
        while (true)
        {
            postCode = Read<uint>("Postcode (1000..9999):", "Invalid input", uint.TryParse);
            if (postCode >= 1000 && postCode <= 9999) break;
            WriteLine("Poscode must be be between 1000 and 9999 inclusive");
        }
        return postCode;
    }
    /// <summary>
    /// Reads and creates a new instance of address.
    /// </summary>
    /// <returns>A new address</returns>
    public static Address ReadAddress()
    {
        Unit unit = Read<Unit>("Unit number (leave blank if none):", "Unit must be non-zeron interger try again", Unit.TryParse);
        uint streetNumber = ReadStreetNumber();
        string streetName = ReadNonBlankString("Street name:", "Cannot be blank");
        StreetType streetType = Read<StreetType>("Street type (St, Rd, Ave, Blvd, Dr, Ln, Ct, Pl, Ter, Way):", "Must be one of the mentioned street type", Helpers.TryParseEnums<StreetType>);
        string city = ReadNonBlankString("City:", "Cannot be blank");
        uint postCode = ReadPostCode();
        State state = Read<State>("State (QLD, NSW, VIC, TAS, SA, WA, NT, ACT):", "Must be one of the mentioned states", Helpers.TryParseEnums<State>);
        Address newAddress = new(unit, streetNumber, streetName, streetType, city, postCode, state);
        return newAddress;
    }
    /// <summary>
    /// Reads collection time.
    /// </summary>
    /// <returns>Validated collectiontime object</returns>
    public static IDeliveryMethod ReadCollectionTime()
    {
        DateTime startTime;
        DateTime endTime;
        while (true)
        {
            // Checks if start time is in valid range.
            startTime = Read<DateTime>($"Delivery window start:", "Invalid input", DateTime.TryParse);
            if (startTime >= DateTime.Now.AddHours(1)) break;
            WriteLine($"Start time must be 1 hour later than {DateTime.Now}");
        }
        while (true)
        {
            // Checks if end time is in valid range.
            endTime = Read<DateTime>($"Delivery window end:", "Invalid input", DateTime.TryParse);
            if (endTime >= startTime.AddHours(1)) break;
            WriteLine($"Start time must be 1 hour later than {startTime}");
        }
        return new CollectionTime(startTime, endTime);
    }
    /// <summary>
    /// A method reading product selection.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="count"></param>
    /// <returns>Selected product row number.</returns>
    public static int ReadProductSelection(string message, int count)
    {
        int rowCount;
        while (true)
        {
            // Checks if row count is in valid range.
            rowCount = Read<int>(message, "Invalid input", int.TryParse);
            if (rowCount <= count && rowCount > 0) 
            {
                break;
            }
            WriteLine($"Please input a value between 1 to {count}");
            WriteLine();
        }
        return rowCount;
    }
    
}
