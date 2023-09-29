using Level52TheFinalBattle.Enums;

namespace Level52TheFinalBattle.ActionChoosers;

internal static class ActionChooserCreator
{
    internal static IChooseActionInterface CreateActionChooser(ActionChooserEnum actionChooserType)
    {
        switch (actionChooserType)
        {
            case ActionChooserEnum.Human:
                return new ConsoleAction();
            case ActionChooserEnum.AI:
                return new AIAction();
        }
        throw new NotImplementedException("Unkonwn action chooser encoutered.");
    }
}
