// •Define a Door class that can keep track of whether it is locked, open, or closed.
// •Make it so you can perform the four transitions defined above with methods.
// •Build a constructor that requires the starting numeric passcode.
// •Build a method that will allow you to change the passcode for an existing door by supplying the current passcode
//      and new passcode. Only change the passcode if the current passcode is correct.
// •Make your main method ask the user for a starting passcode, then create a new Door instance. Allow the user to
//      attempt the four transitions described above (open, close, lock, unlock) and change the code by typing in text
//      commands.


namespace Level24TheCatacombsOfTheClass.Challenges;

using Level24TheCatacombsOfTheClass.Enums;

public class Door
{
    public DoorState DoorState { get; private set; }
    private int _passcode;

    public Door(int passcode)
    {
        _passcode = passcode;
        DoorState = DoorState.Locked;
    }
    
    public void Open()
    {
        if (DoorState == DoorState.Closed) DoorState = DoorState.Opened;
    }
    
    public void Close()
    {
        if (DoorState == DoorState.Opened) DoorState = DoorState.Closed;
    }
    
    public void Lock()
    {
        if (DoorState == DoorState.Closed) DoorState = DoorState.Locked;
    }

    public void Unlock(int passCode)
    {
        if (DoorState == DoorState.Locked && passCode == _passcode) DoorState = DoorState.Closed;
    }

    public void ChangePasscode(int currentPassCode, int newPassCode)
    {
        if (currentPassCode == _passcode) _passcode = newPassCode;
    }
}