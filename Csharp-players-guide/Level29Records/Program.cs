using Level29Records.Records;
using Level29Records.Enums;

Sword swordBasic = new Sword(SwordMaterial.Iron, SwordGemstone.None, 50, 15);
Sword swordBasicWithGem = swordBasic with {Gemstone = SwordGemstone.Bitstone};
Sword swordFancy = swordBasic with {Material = SwordMaterial.Binarium, Length = 100};

Console.WriteLine(swordBasic);
Console.WriteLine(swordBasicWithGem);
Console.WriteLine(swordFancy);