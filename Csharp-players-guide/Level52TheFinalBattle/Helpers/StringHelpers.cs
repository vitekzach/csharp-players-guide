using Level52TheFinalBattle.Enums;

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

    public static string GetTableHeader(string headerText, int desiredWidth)
    {
        return $"╔{CenterString(headerText, '═', desiredWidth - 2)}╗";
    }

    public static string GetTableDivider(string headerText, int desiredWidth)
    {
        return $"╟{CenterString(headerText, '━', desiredWidth - 2)}╢";
    }

    public static string GetTableBottom(int desiredWidth)
    {
        return $"╚{CenterString("", '═', desiredWidth - 2)}╝";
    }

    public static string GetTableRow(
        string leftText,
        string rightText,
        TableStringHalfAlignment alignment,
        int desiredWidth,
        bool tableRowHeader = false
    )
    {
        string tableRow = "";
        char paddingChar = tableRowHeader ? '─' : ' ';

        switch (alignment)
        {
            case TableStringHalfAlignment.Left:
                tableRow = LeftAndRightJustify(
                    leftText,
                    rightText,
                    paddingChar,
                    (desiredWidth - 2) / 2
                );
                tableRow = LeftJustify(tableRow, ' ', desiredWidth - 2);
                break;
            case TableStringHalfAlignment.Right:
                tableRow = LeftAndRightJustify(
                    leftText,
                    rightText,
                    paddingChar,
                    (desiredWidth - 2) / 2
                );
                tableRow = RightJustify(tableRow, ' ', desiredWidth - 2);
                break;
            case TableStringHalfAlignment.None:
                tableRow = LeftAndRightJustify(leftText, rightText, paddingChar, desiredWidth - 2);
                break;
        }
        return $"║{tableRow}║";
    }
}
