using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

class Program
{
    static void Main()
    {
        using var window = new GameWindow(GameWindowSettings.Default, NativeWindowSettings.Default);
        window.Run();
    }
}