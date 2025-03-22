using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System.Collections.Generic;

public class UShapeWindow : GameWindow
{
    private int _shaderProgram;
    private Matrix4 _view, _projection;
    private List<UShape> _shapes = new();

    public UShapeWindow(GameWindowSettings gameSettings, NativeWindowSettings nativeSettings)
        : base(gameSettings, nativeSettings) { }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        GL.Enable(EnableCap.DepthTest);

        _shaderProgram = CreateShader();

        _view = Matrix4.LookAt(new Vector3(0, 0, 5), Vector3.Zero, Vector3.UnitY);
        _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Size.X / (float)Size.Y, 0.1f, 100f);
        /*
        _shapes.Add(new UShape(new Vector3(0f, 0f, 0f), _shaderProgram));
        _shapes.Add(new UShape(new Vector3(1.5f, 0.5f, 0.2f), _shaderProgram));
        _shapes.Add(new UShape(new Vector3(-1.5f, 0.5f, 0f), _shaderProgram));
        */

        _shapes.Add(new UShape(new Vector3(0f, 0f, 0f), 1f, 1f, _shaderProgram));         // U normal
        _shapes.Add(new UShape(new Vector3(2f, 0f, 0f), 0.5f, 0.7f, _shaderProgram));       // Estrecha y alta
        _shapes.Add(new UShape(new Vector3(-2f, 0f, 0f), 0.5f, 0.5f, _shaderProgram));      // Ancha y bajita
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        foreach (var shape in _shapes)
        {
            shape.Draw(_view, _projection);
        }

        SwapBuffers();
    }

    private int CreateShader()
    {
        string vertexShaderSource = @"
            #version 330 core
            layout (location = 0) in vec3 aPosition;
            uniform mat4 model;
            uniform mat4 view;
            uniform mat4 projection;
            void main()
            {
                gl_Position = projection * view * model * vec4(aPosition, 1.0);
            }
        ";

        string fragmentShaderSource = @"
            #version 330 core
            out vec4 FragColor;
            void main()
            {
                FragColor = vec4(1.0, 1.0, 1.0, 1.0);
            }
        ";

        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);
        GL.CompileShader(vertexShader);

        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);
        GL.CompileShader(fragmentShader);

        int shaderProgram = GL.CreateProgram();
        GL.AttachShader(shaderProgram, vertexShader);
        GL.AttachShader(shaderProgram, fragmentShader);
        GL.LinkProgram(shaderProgram);

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);

        return shaderProgram;
    }
}
