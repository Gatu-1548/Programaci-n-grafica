using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

public class Program
{
    public static void Main()
    {
        var settings = new NativeWindowSettings()
        {
            Size = new Vector2i(800, 600),
            Title = "Multiple U Shapes"
        };

        using var window = new UShapeWindow(GameWindowSettings.Default, settings);
        window.Run();
    }
}
