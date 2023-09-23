using Level52TheFinalBattle.Characters;

namespace Level52TheFinalBattle.Interfaces;

public interface IUsable
{
    public void Use(Character target, Party targetParty);
}
