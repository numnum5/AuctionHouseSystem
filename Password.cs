namespace AuctionHouse;
/// <summary>
/// Password class for validating a password input from user
/// </summary>
public class Password
{
    private string rawPassword;
    /// <summary>
    /// Initializes a new instance of the Password class.
    /// </summary>
    /// <param name="password">The raw password string to be validated and set.</param>
    /// <exception cref="ArgumentException">Throws when the password is invalid.</exception>
    public Password(string password)
    {
        if (!IsValid(password))
        {
            throw new ArgumentException();
        }
        rawPassword = password;
    }
    /// <summary>
    /// Checks if input from user is a valid password.
    /// </summary>
    /// <param name="password">The raw password string to be validated.</param>
    /// <returns></returns>
    public static bool IsValid(string password)
    {   

        if (string.IsNullOrEmpty(password)) return false;
        password = password.Trim();
        //Checks if the password is less than 8 characters long.
        if (password.Length < 8) return false;
        //Checks if the password is ASCII values.
        if (!password.All(char.IsAscii)) return false;
        if (!IsValidPassword(password)) return false;
        return true;
    }
    /// <summary>
    /// Checks for different cases for valid password.
    /// </summary>
    /// <param name="password">The raw password string to be validated.</param>
    /// <returns></returns>
    public static bool IsValidPassword(string password)
    {
        //Checks for at least 1 upper case letter.
        if (!password.Any(char.IsUpper)) return false;
        //Checks for at least 1 digit.
        if (!password.Any(char.IsDigit)) return false;
        //Checks for at least 1 lower case letter.
        if (!password.Any(char.IsLower)) return false;
        //Checks for at least 1 non-alphanumeric value
        if (!password.Any(c => !char.IsLetterOrDigit(c))) return false;
        return true;
    }
    /// <summary>
    /// Tries to parse a raw password string and create a new instance of the Password class.
    /// </summary>
    /// <param name="password">The raw password string to be parsed and validated.</param>
    /// <param name="result"> When this method returns, contains the parsed and validated <see cref="Password"/> instance if parsing is successful;
    /// otherwise, contains a default Password instance</param>
    /// <returns>Returns true if sucessful otherwise false.</returns>
    public static bool TryParse(string ? password, out Password result)
    {
        result = new("Default1#");
        if (password == null) return false;
        if (!IsValid(password)) return false;
        result = new Password(password);
        return true;
    }
    /// <summary>
    /// Determines whether the current Password object is equal to another Password object.
    /// </summary>
    /// <param name="obj">The object to compare with the current Password object.</param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if ( obj is Password other) 
        {
            //Compares the string representation of the password.
            return string.Compare(rawPassword, other.rawPassword) == 0;
        }
        else return false;
    }
    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}