// Challenge name: Simula’s Test
// Objectives: 
//      •Define an enumeration for the state of the chest.
//      •Make a variable whose type is this new enumeration.
//      •Write code to allow you to manipulate the chest with the lock, unlock, open, and close commands, but ensure
//          that you don’t transition between states that don’t support it.
//      •Loop forever, asking for the next command.

using _16_enumerations.Enums;

namespace _16_enumerations.Challenges
{
    public class Challenge1
    {
        public static ChestState ChangeChestState(ChestState chestState, string chestAction)
        {
            switch (chestAction)
            {
                case "unlock":
                    return ChestUnlock(chestState);
                case "open":
                    return ChestOpen(chestState);
                case "close":
                    return ChestClose(chestState);
                case "lock":
                    return ChestLock(chestState);
                default:
                    return chestState;
            }
        }

        public static ChestState ChestUnlock(ChestState chestState)
        {
            if (chestState == ChestState.Locked) return ChestState.Closed;
            return chestState;
        }
        
        public static ChestState ChestLock(ChestState chestState)
        {
            if (chestState == ChestState.Closed) return ChestState.Locked;
            return chestState;
        }
        
        public static ChestState ChestOpen(ChestState chestState)
        {
            if (chestState == ChestState.Closed) return ChestState.Open;
            return chestState;
        }
        
        public static ChestState ChestClose(ChestState chestState)
        {
            if (chestState == ChestState.Open) return ChestState.Closed;
            return chestState;
        }
        
        public static void Run()
        {
            ChestState chestState = ChestState.Locked;
            while (true)
            {
                Console.Write($"The chest is {chestState}. What do you want to do? ");
                string chestAction = Console.ReadLine();
                chestState = ChangeChestState(chestState, chestAction);

            }
        }
    }
}