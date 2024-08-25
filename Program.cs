using UI;
using AuctionHouse;
using idk;
internal class Program
{   
    private static void Main(string[] args)
    {
        Console.WriteLine(idk.af.Wow.Fucu.Eka.Value);
        AuctionSystem auctionSystem = new();
        //Instantiates a new Menu class that will represent the main menu.
        Menu mainMenu = new Menu("", "", "+------------------------------+\n| Welcome to the Auction House |\n+------------------------------+",
            new RegistrationMenuItem("1", "Register", auctionSystem.Customers),
            new SignInMenuItem("2", "Sign in", "+-------------+\n| Client Menu |\n+-------------+", auctionSystem.Products, auctionSystem.Customers),
            new ExitMenuItem("3", "Exit"));
        mainMenu.DoAction();
    }
}