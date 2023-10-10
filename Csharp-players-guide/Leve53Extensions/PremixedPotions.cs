internal enum Ingredient
{
  Water,
  Stardust,
  SnakeVenom,
  DragonBreath,
  ShadowGlass,
  EyeshineGem
}

internal static class PotionRecognizer
{
  internal static void GetPotion(List<Ingredient> potionIngredients)
  {
    string potionName = potionIngredients switch
    {
      [Ingredient.Water, Ingredient.Stardust] => "Elixir",
      [Ingredient.Water, Ingredient.Stardust, Ingredient.SnakeVenom] => "Poison potion",
      [Ingredient.Water, Ingredient.Stardust, Ingredient.DragonBreath] => "Flying potion",
      [Ingredient.Water, Ingredient.Stardust, Ingredient.ShadowGlass] => "Invisibility potion",
      [Ingredient.Water, Ingredient.Stardust, Ingredient.EyeshineGem] => "Night sight potion",
      [Ingredient.Water, Ingredient.Stardust, Ingredient.EyeshineGem, Ingredient.ShadowGlass] => "Cloudy brew",
      [Ingredient.Water, Ingredient.Stardust, Ingredient.ShadowGlass, Ingredient.EyeshineGem] => "Cloudy brew",
      [Ingredient.Water, Ingredient.Stardust, Ingredient.EyeshineGem, Ingredient.ShadowGlass, Ingredient.EyeshineGem] => "Wraith potion",
      [Ingredient.Water, Ingredient.Stardust, Ingredient.ShadowGlass, Ingredient.EyeshineGem, Ingredient.EyeshineGem] => "Wraith potion",
      _ => "Ruined potion"
    };

    Console.WriteLine($"It's a {potionName}.");
  }
}
