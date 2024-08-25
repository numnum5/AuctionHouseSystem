namespace UI;
/// <summary>
/// A custom exception for exiting out of menu.
/// </summary>
public class ExitMenuException : Exception {}
/// <summary>
/// Menu item for exiting out of current menu.
/// </summary>
public class ExitMenuItem : MenuItem
{
    public ExitMenuItem(string key, string text) : base(key, text)
    {
        
    }
    public override void DoAction()
    {
        throw new ExitMenuException();
    }
}

