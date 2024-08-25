namespace UI;
using static Console;

/// <summary>
/// Menuitem class for representing a menuitem
/// </summary>
public class MenuItem : IActionable
{
    // Readonly properties 
    public string Key{ get; }
    public string Text { get; }
    public MenuItem(string key, string text)
    {
        Key = key;
        Text = text;
    }
    public virtual void DoAction()
    {
        WriteLine($"{Text} - DoAction has been invoked" );
    }
    public override string ToString()
    {
        return $"{Key}         : {Text}";
    }
}
