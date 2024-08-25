namespace AuctionHouse;
using UI;
/// <summary>
/// A menuitem for logging out the user
/// </summary>
public class LogOutMenuItem : MenuItem
{
    public LogOutMenuItem(string key, string text) : base(key, text)
    {
    }
    public override void DoAction()
    {
        //Throws the ExitMenuException to exit out of the current menu.
        throw new ExitMenuException();
    }

}