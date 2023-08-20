// •Define enumerations for card colors and card ranks.
// •Define a Card class to represent a card with a color and a rank, as described above.
// •Add properties or methods that tell you if a card is a number or symbol card (the equivalent of a face card).
// •Create a main method that will create a card instance for the whole deck (every color with every rank) and
//      display each (for example, “The Red Ampersand” and “The Blue Seven”).
// •Answer this question: Why do you think we used a color enumeration here but made a color class in the previous
//      challenge?

using Level24TheCatacombsOfTheClass.Enums;

namespace Level24TheCatacombsOfTheClass.Challenges;

public class Card
{
    public CardColor Color { get; }
    public CardRank Rank { get; }

    public Card(CardColor color, CardRank cardRank)
    {
        Color = color;
        Rank = cardRank;
    }

    public bool IsSymbol => Rank == CardRank.And || Rank == CardRank.Hat || Rank == CardRank.Percent || Rank == CardRank.Dollar;
    public bool IsNumber => !IsSymbol;
}
