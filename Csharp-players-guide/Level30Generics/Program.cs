using Level30Generics.Challenges;

// Shorter definition:
// ColoredItem<Sword> blueSword = new(ConsoleColor.Blue, new Sword());
// ColoredItem<Bow> redBow = new(ConsoleColor.Red, new Bow());
// ColoredItem<Axe> greenAxe = new(ConsoleColor.Green, new Axe());

var blueSword = new ColoredItem<Sword>(ConsoleColor.Blue, new Sword());
var redBow = new ColoredItem<Bow>(ConsoleColor.Red, new Bow());
var greenAxe = new ColoredItem<Axe>(ConsoleColor.Green, new Axe());

blueSword.Display();
redBow.Display();
greenAxe.Display();
