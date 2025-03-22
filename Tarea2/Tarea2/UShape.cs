using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

public class UShape
{
    public Vector3 Position { get; set; }
    public Vector3 Rotation { get; set; } = Vector3.Zero;

    private float[] _vertices;
    private uint[] _indices;
    private int _vao, _vbo, _ebo;
    private int _shaderProgram;


    public UShape(Vector3 position, float width, float height, int shaderProgram)
    {
        Position = position;
        _shaderProgram = shaderProgram;
        GenerateGeometry(width, height);
        SetupBuffers();
    }
    /*
    public UShape(Vector3 position, int shaderProgram)
    {
        Position = position;
        _shaderProgram = shaderProgram;
        GenerateGeometry();
        SetupBuffers();
    }
    
   
    private void GenerateGeometry()
    {
        _vertices = new float[]
        {
            -0.5f,  0.5f, 0.0f, -0.4f,  0.5f, 0.0f, -0.4f, -0.5f, 0.0f, -0.5f, -0.5f, 0.0f,
            -0.4f,  0.6f, 0.0f, -0.3f,  0.6f, 0.0f, -0.3f, -0.4f, 0.0f, -0.4f, -0.4f, 0.0f,
             0.3f,  0.5f, 0.0f,  0.4f,  0.5f, 0.0f,  0.4f, -0.5f, 0.0f,  0.3f, -0.5f, 0.0f,
             0.4f,  0.6f, 0.0f,  0.5f,  0.6f, 0.0f,  0.5f, -0.4f, 0.0f,  0.4f, -0.4f, 0.0f,
            -0.5f, -0.5f, 0.0f,  0.4f, -0.5f, 0.0f,  0.4f, -0.6f, 0.0f, -0.5f, -0.6f, 0.0f,
            -0.4f, -0.4f, 0.0f,  0.5f, -0.4f, 0.0f,  0.5f, -0.5f, 0.0f, -0.4f, -0.5f, 0.0f
        };

        _indices = new uint[]
        {
            0,1, 1,2, 2,3, 3,0, 4,5, 5,6, 6,7, 7,4,
            0,4, 1,5, 3,7, 2,6, 8,9, 9,10, 10,11, 11,8,
            12,13, 13,14, 14,15, 15,12, 8,12, 9,13, 11,15, 10,14,
            16,17, 17,18, 18,19, 19,16, 20,21, 21,22, 22,23, 23,20,
            16,20, 19,23, 17,21, 18,22
        };
    }este es el actual que funciona
    -------------------------------------

    */
    private void GenerateGeometry(float width, float height)
    {
        float w = width / 2f;   // mitad del ancho
        float h = height / 2f;  // mitad del alto

        _vertices = new float[]
        {
        // Cubo izquierdo
        -w,  h, 0.0f,     -w + 0.1f,  h, 0.0f,   -w + 0.1f, -h, 0.0f,   -w, -h, 0.0f,
        -w + 0.1f, h + 0.1f, 0.0f,   -w + 0.2f, h + 0.1f, 0.0f,   -w + 0.2f, -h + 0.1f, 0.0f,   -w + 0.1f, -h + 0.1f, 0.0f,

        // Cubo derecho
        w - 0.2f,  h, 0.0f,   w - 0.1f,  h, 0.0f,   w - 0.1f, -h, 0.0f,   w - 0.2f, -h, 0.0f,
        w - 0.1f,  h + 0.1f, 0.0f,   w, h + 0.1f, 0.0f,       w, -h + 0.1f, 0.0f,   w - 0.1f, -h + 0.1f, 0.0f,

        // Cubo inferior
        -w, -h, 0.0f,   w - 0.1f, -h, 0.0f,   w - 0.1f, -h - 0.1f, 0.0f,   -w, -h - 0.1f, 0.0f,
        -w + 0.1f, -h + 0.1f, 0.0f,   w, -h + 0.1f, 0.0f,   w, -h, 0.0f,   -w + 0.1f, -h, 0.0f
        };


        _indices = new uint[]
        {
        0,1, 1,2, 2,3, 3,0, 4,5, 5,6, 6,7, 7,4,
        0,4, 1,5, 3,7, 2,6, 8,9, 9,10, 10,11, 11,8,
        12,13, 13,14, 14,15, 15,12, 8,12, 9,13, 11,15, 10,14,
        16,17, 17,18, 18,19, 19,16, 20,21, 21,22, 22,23, 23,20,
        16,20, 19,23, 17,21, 18,22
        };
    }
    private void SetupBuffers()
    {
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
    }

    public void Draw(Matrix4 view, Matrix4 projection)
    {
        Matrix4 model =
            Matrix4.CreateRotationX(Rotation.X) *
            Matrix4.CreateRotationY(Rotation.Y) *
            Matrix4.CreateRotationZ(Rotation.Z) *
            Matrix4.CreateTranslation(Position);

        GL.UseProgram(_shaderProgram);
        GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "model"), false, ref model);
        GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "view"), false, ref view);
        GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "projection"), false, ref projection);

        GL.BindVertexArray(_vao);
        GL.DrawElements(PrimitiveType.Lines, _indices.Length, DrawElementsType.UnsignedInt, 0);
    }
}