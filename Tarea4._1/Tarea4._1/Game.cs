using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using Tarea4._1;

namespace Tarea4._1
{
    internal class Game : GameWindow
    {
        
        private Escenario escenario;
        
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

           
            Parte parteU = new Parte("partes.txt"); 

            
            Objeto objetoU = new Objeto(new Vector3(0f, 0f, 0f));
            objetoU.AddParte(parteU);

           
            escenario.AddObjeto(objetoU);

            Objeto u2 = new Objeto(new Vector3(-2f, 0f, 0f));
            u2.AddParte(parteU);
            escenario.AddObjeto(u2);
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
          
            GL.Translate(0f, 0f, -4f);

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
            
            for (int i = 0; i < escenario.GetObjetos().Count; i++)
            {
                escenario.GetObjetos()[i].RotacionY += (i + 1) * 0.5f; 
            }
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            
        }
    }
}
