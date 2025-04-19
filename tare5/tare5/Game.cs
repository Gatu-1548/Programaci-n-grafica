using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using tare5;

namespace tare5
{
    internal class Game : GameWindow
    {
        
        private Escenario escenario;
        private int objetoActivoIndex = 0;

        private float angle = 0f;
        public Game()
            : base(GameWindowSettings.Default, new NativeWindowSettings()
            {
                ClientSize = new Vector2i(800, 600),
                Title = "Tarea 4.1 - Letra U 3D",
                APIVersion = new Version(3, 3),
                Profile = ContextProfile.Compatability,
                API = ContextAPI.OpenGL
            })
        {
            CenterWindow();
            VSync = VSyncMode.On;
        }

        protected override void OnLoad()
        {
            
            base.OnLoad();

            GL.ClearColor(new Color4(0.2f, 0.2f, 0.4f, 1.0f));
            GL.Enable(EnableCap.DepthTest);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Size.X / (float)Size.Y, 0.1f, 100f);
            GL.LoadMatrix(ref perspective);
            //GL.Ortho(-4, 4, -3, 3, -10, 10); 
            //GL.Ortho(-2, 2, -2, 2, -10, 10);
            
            escenario = new Escenario(new Vector3(0f, 0f, 0f));

           
            Parte parteU = new Parte("vertices_convertidos.json"); 

            
            Objeto objetoU = new Objeto(new Vector3(0f, 0f, 0f));
            objetoU.AddParte(parteU);

           
            escenario.AddObjeto(objetoU);

            Objeto u2 = new Objeto(new Vector3(-2f, 0f, 0f));
            u2.AddParte(parteU);
            escenario.AddObjeto(u2);

            //Objeto u3 = new Objeto(new Vector3(2f, 0f, 0f));
            //u3.AddParte(parteU);
            //escenario.AddObjeto(u3);
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);

            float aspect = Size.X / (float)Size.Y;
            float zoom = 2.0f; 

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-zoom * aspect, zoom * aspect, -zoom, zoom, -8, 8);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
          
           // GL.Translate(0f, 0f, -4f);

            // Rotar la figura en Y
            //GL.Rotate(angle, 0f, 1f, 0f);

            escenario.Dibujar();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            //angle += 0.5f;
            //if (angle > 360f) angle -= 360f;
            var input = KeyboardState;
            var objetos = escenario.GetObjetos();
            bool objetoSeleccionado = false;

          

            // Seguridad: límite del índice
            if (objetoActivoIndex >= objetos.Count)
                objetoActivoIndex = objetos.Count - 1;

            // Modo: un objeto o todos
            List<Objeto> objetosControlados = objetoSeleccionado
                ? new List<Objeto> { objetos[objetoActivoIndex] }
                : objetos;

            // === TRANSFORMACIONES ===
            foreach (var objeto in objetosControlados)
            {
                // TRASLACIÓN
                Vector3 pos = objeto.Posicion;
                if (input.IsKeyDown(Keys.Right)) pos.X += 0.05f;
                if (input.IsKeyDown(Keys.Left)) pos.X -= 0.05f;
                if (input.IsKeyDown(Keys.Up)) pos.Y += 0.05f;
                if (input.IsKeyDown(Keys.Down)) pos.Y -= 0.05f;
                if (input.IsKeyDown(Keys.R)) pos.Z -= 0.05f;
                if (input.IsKeyDown(Keys.F)) pos.Z += 0.05f;
                objeto.Posicion = pos;

                // ROTACIÓN
                Vector3 rot = objeto.Rotacion;
                if (input.IsKeyDown(Keys.I)) rot.X += 2f;
                if (input.IsKeyDown(Keys.K)) rot.X -= 2f;
                if (input.IsKeyDown(Keys.J)) rot.Y += 2f;
                if (input.IsKeyDown(Keys.L)) rot.Y -= 2f;
                if (input.IsKeyDown(Keys.U)) rot.Z += 2f;
                if (input.IsKeyDown(Keys.O)) rot.Z -= 2f;
                objeto.Rotacion = rot;

                // ESCALA
                if (input.IsKeyDown(Keys.W)) objeto.Escala += 0.01f;
                if (input.IsKeyDown(Keys.S)) objeto.Escala -= 0.01f;
                if (objeto.Escala < 0.1f) objeto.Escala = 0.1f;
            }

        }

        protected override void OnUnload()
        {
            base.OnUnload();
            
        }
    }
}
