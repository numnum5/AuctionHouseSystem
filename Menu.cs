namespace UI;
using static System.Console;

/// <summary>
/// Menu that processes user input for menu items.
/// </summary>
public class Menu : MenuItem
{
    private readonly string title;
    protected List<MenuItem> items = new();
    public Menu(string key, string text, string title, params MenuItem[] items) : base(key, text)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Menu title must not be blank");
        this.title = title;
        this.items.AddRange(items);
    }
    public override void DoAction()
    {
        while (true)
        {
            WriteLine($"{title}");
            WriteLine();
            WriteLine("Please select an option from the following list:");
            WriteLine();

            foreach (var item in items)
            {
                WriteLine(item);
            }
            WriteLine();
            Write("? ");

            string? userInput = ReadLine();

            if (userInput is null) throw new EndOfStreamException();

            userInput = userInput.Trim();

            MenuItem ? selectedItem = null;

            foreach (var item in items)
            {
                if (string.Compare(userInput, item.Key, ignoreCase: true) == 0)
                {
                    selectedItem = item;
                    break;
                }
            }

            if (selectedItem is not null)
            {
                try
                {
                    selectedItem.DoAction();
                }
                catch (ExitMenuException)
                {
                    break;
                }
            }
            else
            {
                WriteLine("Please choose a valid option.");
            }
        }
    }
}
