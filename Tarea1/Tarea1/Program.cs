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
        //cubo izquierdo
    -0.5f,  0.5f, 0.0f,    
    -0.4f,  0.5f, 0.0f,   
    -0.4f, -0.5f, 0.0f,     
    -0.5f, -0.5f, 0.0f,    

    -0.4f,  0.6f, 0.0f,    
    -0.3f,  0.6f, 0.0f,    
    -0.3f, -0.4f, 0.0f,     
    -0.4f, -0.4f, 0.0f,    

    //cubo derecha
     0.3f,  0.5f, 0.0f,    
     0.4f,  0.5f, 0.0f,     
     0.4f, -0.5f, 0.0f,    
     0.3f, -0.5f, 0.0f,     

     0.4f,  0.6f, 0.0f,   
     0.5f,  0.6f, 0.0f,    
     0.5f, -0.4f, 0.0f,     
     0.4f, -0.4f, 0.0f,   

     //cubo abajo
    -0.5f, -0.5f, 0.0f,     
     0.4f, -0.5f, 0.0f,    
     0.4f, -0.6f, 0.0f,    
    -0.5f, -0.6f, 0.0f,    

    -0.4f, -0.4f, 0.0f,    
     0.5f, -0.4f, 0.0f,    
     0.5f, -0.5f, 0.0f,    
    -0.4f, -0.5f, 0.0f,    


    };

    private readonly uint[] _indices =
{
     
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
        Title = "3D U",
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

        
        _view = Matrix4.LookAt(new Vector3(0f, 0f, 3f), Vector3.Zero, Vector3.UnitY);

        
        _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), 800f / 600f, 0.1f, 100f);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        GL.UseProgram(_shaderProgram);

        
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