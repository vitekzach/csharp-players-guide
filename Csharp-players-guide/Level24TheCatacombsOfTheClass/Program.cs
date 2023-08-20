using Level24TheCatacombsOfTheClass.Challenges;
using Level24TheCatacombsOfTheClass.Enums;
//
// // Challenge 1
// Point point1 = new Point(2,3);
// Point point2 = new Point(-4,0);
// Console.WriteLine($"Point 1: ({point1.X}, {point1.Y})");
// Console.WriteLine($"Point 2: ({point2.X}, {point2.Y})");
//
//
// // Challenge 2
// Color color1 = new Color(1,2,3);
// Color color2 = Color.Purple;
// Console.WriteLine($"Color 1: ({color1.R}, {color1.G}, {color1.B})");
// Console.WriteLine($"Color 2: ({color2.R}, {color2.G}, {color2.B})");
//
//
// // Challenge 3
// CardRank[] cardRanks = { CardRank.One, CardRank.Two, CardRank.Three, CardRank.Four, CardRank.Five, CardRank.Six, CardRank.Seven, CardRank.Eight, CardRank.Nine, CardRank.Ten, CardRank.Dollar, CardRank.Percent, CardRank.Hat, CardRank.And};
// CardColor[] cardColors = { CardColor.Blue, CardColor.Green, CardColor.Red, CardColor.Yellow };
//
// foreach (CardColor cardColor in cardColors)
// {
//     foreach (CardRank cardRank in cardRanks)
//     {
//         Card card = new Card(cardColor, cardRank);
//         Console.WriteLine($"The {card.Color} {card.Rank}");
//     }
// }
//
// // Challenge 4
// Console.Write("Choose door pass code: ");
// int passCode = Convert.ToInt32(Console.ReadLine());
// var door = new Door(passCode);
//
// while (true)
// {
//     Console.Write($"The door is {door.DoorState}. What's your action? (open, close, lock, unlock, change): ");
//     string choice = Console.ReadLine();
//     switch (choice)
//     {
//         case "open":
//             door.Open();
//             break;
//         case "close":
//             door.Close();
//             break;
//         case "lock":
//             door.Lock();
//             break;
//         case "unlock":
//             Console.Write("What's the passcode? ");
//             int passCodeUnlock = Convert.ToInt32(Console.ReadLine());
//             door.Unlock(passCodeUnlock);
//             break;
//         case "change":
//             Console.Write("What's the old passcode? ");
//             int passCodeCurrent = Convert.ToInt32(Console.ReadLine());
//             Console.Write("What's the new passcode? ");
//             int passCodeNew = Convert.ToInt32(Console.ReadLine());
//             door.ChangePasscode(passCodeCurrent, passCodeNew);
//             break;
//     }
// }


// // Challenge 5
// while (true)
// {
//     Console.Write("Choose a password for validation: ");
//     string chosenPassword = Console.ReadLine() ?? "";
//     bool chosenPasswordValid = PasswordValidator.ValidatePassword(chosenPassword);
//     Console.WriteLine($"Password validity: {chosenPasswordValid}");
// }

TicTacToeGame game = new TicTacToeGame(3);
game.RunGame();

