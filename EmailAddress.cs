namespace AuctionHouse
{
    /// <summary>
    /// Class for representing and validating user input for emailaddresses.
    /// </summary>
    public class EmailAddress 
    {
        /// <summary>
        /// Field for storing user name.
        /// </summary>
        private string userName;
        /// <summary>
        /// Field for storing domain.
        /// </summary>
        private string domain;
        /// <summary>
        /// Initializes a new instance of the EmailAddress class.
        /// </summary>
        /// <param name="address">The string email address to validate and store.</param>
        /// <exception cref="ArgumentException">Thrown if the provided email address is not valid.</exception>
        public EmailAddress (string address) 
        {
            // Splits the address into parts and validates.
            string[] parts = address.Trim().Split('@');
            if (!IsValid(parts[0], parts[1])) throw new ArgumentException();

            // Set the username and domain.
            userName = parts[0];
            domain = parts[1];
        }

        /// <summary>
        /// Checks if the username and domain are valid.
        /// </summary>
        /// <param name="userName">The username part of the email address.</param>
        /// <param name="domain">The domain part of the email address.</param>
        /// <returns>True if valid, false otherwise.</returns>
        public static bool IsValid(string userName, string domain)
        {
            // Null check
            if (userName == null || domain == null) return false;

            // Trim whitespace
            userName = userName.Trim();
            domain = domain.Trim();

            // Length check
            if (userName.Length == 0 || domain.Length == 0) return false;

            // Check individual components
            if (!IsValidUserName(userName) || !IsValidDomain(domain)) return false;

            return true;
        }

        /// <summary>
        /// Checks if the userName prefix is valid.
        /// </summary>
        /// <param name="userName">The username part of the email address.</param>
        /// <returns>True if valid, false otherwise.</returns>
        private static bool IsValidUserName(string userName)
        {
            // Checks if the last character is a letter or digit, if not returns false
            if (!Char.IsLetterOrDigit(userName[userName.Length - 1])) return false;

            // Checks if the username contains valid characters
            if (!IsValidUserNameCharacters(userName)) return false;

            return true;
        }

        /// <summary>
        /// Checks if the domain suffix is valid.
        /// </summary>
        /// <param name="domain">The domain part of the email address.</param>
        /// <returns>True if valid, false otherwise.</returns>
        private static bool IsValidDomain(string domain)
        {
            // Checks if the last element of domain is either dot or dash
            if (domain[domain.Length - 1] == '.' || domain[domain.Length - 1] == '-') return false;

            // Checks if the first element of domain is either dot or dash
            if (domain[0] == '.' || domain[0] == '-') return false;

            // Checks if the domain contains at least one dot
            if (!domain.Contains('.')) return false;

            // Check for valid characters in the domain
            if (!IsValidDomainCharacters(domain)) return false;

            // Check if the last part after dot contains only letters
            var parts = domain.Split(['.']);
            if (!IsLetters(parts.Last())) return false;

            return true;
        }

        /// <summary>
        /// Checks for valid domain characters.
        /// </summary>
        /// <param name="domain">The domain part of the email address.</param>
        /// <returns>True if valid, false otherwise.</returns>
        private static bool IsValidDomainCharacters(string domain)
        {
            foreach (var ch in domain)
            {
                // Checks if the character is a letter, digit, dot, or dash
                if (!Char.IsLetterOrDigit(ch) && ch != '.' && ch != '-') return false;
            }
            return true;
        }

        /// <summary>
        /// Checks for valid username characters.
        /// </summary>
        /// <param name="s">The username part of the email address.</param>
        /// <returns>True if valid, false otherwise.</returns>
        private static bool IsValidUserNameCharacters(string s)
        {
            foreach (var ch in s)
            {
                // Checks if the character is a letter, digit, dot, dash, or underscore
                if (!Char.IsLetterOrDigit(ch) && ch != '.' && ch != '-' && ch != '_') return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if a string consists only of letters.
        /// </summary>
        /// <param name="s">The string to check.</param>
        /// <returns>True if all characters are letters, false otherwise.</returns>
        private static bool IsLetters(string s)
        {
            if (s.Length == 0) return false;
            foreach (var ch in s)
            {
                // Checks if the character is a letter
                if (!Char.IsLetter(ch)) return false;
            }
            return true;
        }

        /// <summary>
        /// Attempts to parse a string into an EmailAddress instance.
        /// </summary>
        /// <param name="emailAddress">The email address string to parse.</param>
        /// <param name="result">The resulting EmailAddress instance.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public static bool TryParse(string? emailAddress, out EmailAddress result)
        {
            // Set a default result
            result = new EmailAddress("Default@gmail.com");

            // Check for null or whitespace
            if (string.IsNullOrWhiteSpace(emailAddress)) return false;

            // Split the address into parts and validate
            var parts = emailAddress.Split('@');
            if (parts.Length != 2 || !IsValid(parts[0], parts[1])) return false;

            // Create and set the result
            result = new EmailAddress(emailAddress);
            return true;
        }

        /// <summary>
        /// Returns a string representation of the email address.
        /// </summary>
        /// <returns>The formatted email address.</returns>
        public override string ToString() => $"{userName}@{domain}";


        /// <summary>
        /// Determines whether the current EmailAddress instance is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if equal, false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is EmailAddress other) 
            {
                // Case-insensitive comparison
                return string.Compare(userName, other.userName, ignoreCase: true ) == 0
                    && string.Compare(domain, other.domain, ignoreCase: true ) == 0;
            }
            else 
            {
                return false;
            }
        }
        /// <summary>
        /// Implemented to avoid warning.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override int GetHashCode()
        {
            // Not implemented
            throw new NotImplementedException();
        }
    }
}
