using Gtk;
using System;

class SimpleWindow : Window
{
    public SimpleWindow() : base("Simple GtkSharp Window")
    {
        SetDefaultSize(400, 200);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { Application.Quit(); };

        Label label = new Label("Hello, GtkSharp!");
        Add(label);

        ShowAll();
    }

    static void Main()
    {
        Application.Init();
        new SimpleWindow();
        Application.Run();
    }
}
