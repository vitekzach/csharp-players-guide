namespace Level52TheFinalBattle.Helpers;

public static class StringHelpers
{
    public static string CenterString(string stringToCenter, char paddingChar, int paddedLength)
    {
        if (stringToCenter.Length >= paddedLength)
            return stringToCenter;

        int paddingBothSides = paddedLength - stringToCenter.Length;
        int paddingOneSideLen = paddingBothSides / 2;

        string paddingOneSide = new String(paddingChar, paddingOneSideLen);

        string result = $"{paddingOneSide}{stringToCenter}{paddingOneSide}";

        if (paddingBothSides % 2 == 1)
            result += paddingChar;

        return result;
    }

    public static string LeftAndRightJustify(
        string leftString,
        string rightString,
        char paddingChar,
        int desiredLength
    )
    {
        int leftAndRightLength = leftString.Length + rightString.Length;

        if (leftAndRightLength >= desiredLength)
            return $"{leftString}{rightString}";

        string padding = new String(paddingChar, desiredLength - leftAndRightLength);
        return $"{leftString}{padding}{rightString}";
    }

    public static string RightJustify(string stringToJustify, char paddingChar, int desiredLength)
    {
        int stringLength = stringToJustify.Length;

        if (stringLength >= desiredLength)
            return stringToJustify;

        string padding = new String(paddingChar, desiredLength - stringLength);
        return $"{padding}{stringToJustify}";
    }

    public static string LeftJustify(string stringToJustify, char paddingChar, int desiredLength)
    {
        int stringLength = stringToJustify.Length;

        if (stringLength >= desiredLength)
            return stringToJustify;

        string padding = new String(paddingChar, desiredLength - stringLength);
        return $"{stringToJustify}{padding}";
    }
}
