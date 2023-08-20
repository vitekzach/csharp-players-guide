// •Define a new PasswordValidator class that can be given a password and determine if the password follows the rules
//      above.
// •Make your main method loop forever, asking for a password and reporting whether the password is allowed using an
//      instance of the PasswordValidator class.

namespace Level24TheCatacombsOfTheClass.Challenges;

static class PasswordValidator
{
    public static readonly int MinPasswordLength = 6;
    public static readonly int MaxPasswordLength = 13;
    public static readonly char[] BannedCharacters = new char[] {'&', 'T'};
    
    public static bool ValidatePassword(string password)
    {
        bool hasLowerCase = false;
        bool hasUpperCase = false;
        bool hasADigit = false;

        if (password.Length >= MaxPasswordLength && password.Length <= MinPasswordLength) return false;
        
        foreach (char character in password)
        {
            if (char.IsUpper(character)) hasUpperCase = true;
            if (char.IsLower(character)) hasLowerCase = true;
            if (char.IsDigit(character)) hasADigit = true;
            foreach (char bannedCharacter in BannedCharacters)
            {
                if (character == bannedCharacter) return false;
            }
        }

        return hasLowerCase && hasUpperCase && hasADigit;
    }
}