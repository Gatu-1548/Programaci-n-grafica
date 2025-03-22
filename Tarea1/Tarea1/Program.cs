using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

public class UShapeWindow : GameWindow
{
    private int _vao, _vbo, _ebo, _shaderProgram;
    private Matrix4 _view, _projection;
    /*
        private readonly float[] _vertices =
        {
            // Parte frontal de la U
            -0.5f,  1.0f,  0.5f,  // 0
            -0.5f, -0.5f,  0.5f,  // 1
             0.5f,  1.0f,  0.5f,  // 2
             0.5f, -0.5f,  0.5f,  // 3
             0.3f, -0.7f,  0.5f,  // 4
            -0.3f, -0.7f,  0.5f,  // 5

            // Parte trasera de la U
            -0.5f,  1.0f, -0.5f,  // 6
            -0.5f, -0.5f, -0.5f,  // 7
             0.5f,  1.0f, -0.5f,  // 8
             0.5f, -0.5f, -0.5f,  // 9
             0.3f, -0.7f, -0.5f,  // 10
            -0.3f, -0.7f, -0.5f,  // 11
        };
    */
    private readonly float[] _vertices =
    {

    // Posiciones (x, y, z)
    // Primer cubo (lado izquierdo)
    -0.5f,  0.5f, 0.0f,    // Vértice 0 
    -0.4f,  0.5f, 0.0f,    // Vértice 1 
    -0.4f, -0.5f, 0.0f,    // Vértice 2 
    -0.5f, -0.5f, 0.0f,    // Vértice 3 

    -0.4f,  0.6f, 0.0f,    // Vértice 4 
    -0.3f,  0.6f, 0.0f,    // Vértice 5 
    -0.3f, -0.4f, 0.0f,    // Vértice 6 
    -0.4f, -0.4f, 0.0f,    // Vértice 7 

    // Segundo cubo (lado derecho)
     0.3f,  0.5f, 0.0f,    // Vértice 8 
     0.4f,  0.5f, 0.0f,    // Vértice 9 
     0.4f, -0.5f, 0.0f,    // Vértice 10 
     0.3f, -0.5f, 0.0f,    // Vértice 11 

     0.4f,  0.6f, 0.0f,    // Vértice 12 
     0.5f,  0.6f, 0.0f,    // Vértice 13 
     0.5f, -0.4f, 0.0f,    // Vértice 14 
     0.4f, -0.4f, 0.0f,    // Vértice 15 

     // Tercer cubo (parte inferior)
    -0.5f, -0.5f, 0.0f,    // Vértice 16 
     0.4f, -0.5f, 0.0f,    // Vértice 17
     0.4f, -0.6f, 0.0f,    // Vértice 18 
    -0.5f, -0.6f, 0.0f,    // Vértice 19 

    -0.4f, -0.4f, 0.0f,    // Vértice 20 
     0.5f, -0.4f, 0.0f,    // Vértice 21 
     0.5f, -0.5f, 0.0f,    // Vértice 22 
    -0.4f, -0.5f, 0.0f,    // Vértice 23


    };

    private readonly uint[] _indices =
{
        /*
    // Conectar la parte frontal
    0, 1,  // Lado izquierdo
    1, 5,  // Bajando al extremo inferior izquierdo
    5, 4,  // Conectando base curva
    4, 3,  // Subiendo lado derecho
    3, 2,  // Lado derecho

    // Conectar la parte trasera
    6, 7,  // Lado izquierdo trasero
    7, 11, // Bajando al extremo inferior izquierdo trasero
    11, 10, // Conectando base curva trasera
    10, 9, // Subiendo lado derecho trasero
    9, 8,  // Lado derecho trasero

    // Conectar frente con atrás
    0, 6,  // Conectar parte superior izquierda
    1, 7,  // Conectar lado izquierdo
    2, 8,  // Conectar parte superior derecha
    3, 9,  // Conectar lado derecho
    4, 10, // Conectar base curva derecha
    5, 11  // Conectar base curva izquierda
        */
    0,1,
    1,2,
    2,3,
    3,0,

     4,5,
     5,6,
     6,7,
     7,4,

     0,4,
     1,5,
     3,7,
     2,6,

     8,9,
     9,10,
     10,11,
     11,8,

     12,13,
     13,14,
     14,15,
     15,12,

     8,12,
     9,13,
     11,15,
     10,14,

     16,17,
     17,18,
     18,19,
     19,16,

     20,21,
     21,22,
     22,23,
     23,20,

     16,20,
     19,23,
     17,21,
     18,22,
};

    public UShapeWindow() : base(GameWindowSettings.Default, new NativeWindowSettings
    {
        Size = new Vector2i(800, 600),
        Title = "3D U Shape",
        Flags = ContextFlags.ForwardCompatible
    })
    { }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        GL.Enable(EnableCap.DepthTest);

        _vao = GL.GenVertexArray();
        _vbo = GL.GenBuffer();
        _ebo = GL.GenBuffer();

        GL.BindVertexArray(_vao);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        _shaderProgram = CreateShader();
        GL.UseProgram(_shaderProgram);

        // Matriz de vista (cámara)
        _view = Matrix4.LookAt(new Vector3(0f, 0f, 3f), Vector3.Zero, Vector3.UnitY);

        // Matriz de proyección (perspectiva)
        _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), 800f / 600f, 0.1f, 100f);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        GL.UseProgram(_shaderProgram);

        // Enviar matrices al shader
        GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "view"), false, ref _view);
        GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "projection"), false, ref _projection);

        GL.BindVertexArray(_vao);
        GL.DrawElements(PrimitiveType.Lines, _indices.Length, DrawElementsType.UnsignedInt, 0);

        SwapBuffers();
    }

    private int CreateShader()
    {
        string vertexShaderSource = @"
            #version 330 core
            layout (location = 0) in vec3 aPosition;
            uniform mat4 view;
            uniform mat4 projection;
            void main()
            {
                gl_Position = projection * view * vec4(aPosition, 1.0);
            }
        ";
        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);
        GL.CompileShader(vertexShader);

        string fragmentShaderSource = @"
            #version 330 core
            out vec4 FragColor;
            void main()
            {
                FragColor = vec4(1.0, 1.0, 1.0, 1.0); // Blanco
            }
        ";
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

    public static void Main()
    {
        using (var window = new UShapeWindow())
        {
            window.Run();
        }
    }
}