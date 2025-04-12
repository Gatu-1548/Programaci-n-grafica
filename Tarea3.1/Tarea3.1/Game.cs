using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;

namespace Tarea3._1
{
    internal class Game : GameWindow
    {
        private List<Objeto> objetos;
        private List<Vector3> vertices;

        public Game()
            : base(GameWindowSettings.Default, new NativeWindowSettings()
            {
                ClientSize = new Vector2i(800, 600),
                Title = "Trabajo 3",
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
            //GL.Ortho(-1, 1, -1, 1, -1, 1); // Proyección ortográfica normalizada
            // GL.Ortho(-2, 2, -2, 2, -1, 1);
            GL.Ortho(-4, 4, -3, 3, -1, 1);
            vertices = new List<Vector3>
            {
                // Columna izquierda
                new Vector3(-0.6f,  0.5f, 0f),
                new Vector3(-0.4f,  0.5f, 0f),
                new Vector3(-0.4f, -0.5f, 0f),
                new Vector3(-0.6f, -0.5f, 0f),

                // Columna derecha
                new Vector3(0.4f,  0.5f, 0f),
                new Vector3(0.6f,  0.5f, 0f),
                new Vector3(0.6f, -0.5f, 0f),
                new Vector3(0.4f, -0.5f, 0f),

                // Base inferior
                new Vector3(-0.4f, -0.3f, 0f),
                new Vector3( 0.4f, -0.3f, 0f),
                new Vector3( 0.4f, -0.5f, 0f),
                new Vector3(-0.4f, -0.5f, 0f),
            };

            objetos = new List<Objeto>
            {
                // new Objeto(new Vector3(-1.4f, 1.0f, 1.0f), vertices),
                new Objeto(new Vector3(0.0f, 0.0f, 0f), vertices),
                //new Objeto(new Vector3(1.3f, 0.0f, 0f), vertices)
            };
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            //GL.Translate(0f, 0f, -0.5f);

            foreach (var objeto in objetos)
            {
                objeto.Dibujar();
            }

            SwapBuffers();
        }
    }
}