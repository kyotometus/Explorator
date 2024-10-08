using Gtk;
using Explorator.Controllers;

class Program
{
    public static void Main(string[] args)
    {
        Application.Init();
        var controller = new MainController();
        controller.ShowMainWindow();
        Application.Run();
    }
}
