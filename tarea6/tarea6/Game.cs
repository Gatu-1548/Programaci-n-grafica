using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

using tarea6;

namespace tarea6
{
    internal class Game : GameWindow
    {

        private Escenario escenario;
        private int objetoActivoIndex = 0;
        private bool modoSeleccionIndividual = false;
        private int parteActivaIndex = 0;
        private bool modoParte = false;
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


            Parte parte1 = new Parte("parte_1.json");
            Parte parte2 = new Parte("parte_2.json");
            Parte parte3 = new Parte("parte_3.json");

            Parte parte4 = new Parte("parte_1.json");
            Parte parte5 = new Parte("parte_2.json");
            Parte parte6 = new Parte("parte_3.json");

            Objeto objetoU = new Objeto(new Vector3(0f, 0f, 0f));
            parte1.Posicion = new Vector3(0f, 0f, 0f);
            parte2.Posicion = new Vector3(1.5f, 0f, 0f);
            parte3.Posicion = new Vector3(0f, 0f, 0f);
            objetoU.AddParte(parte1);
            objetoU.AddParte(parte2);
            objetoU.AddParte(parte3);


            escenario.AddObjeto(objetoU);

            Objeto u2 = new Objeto(new Vector3(-2f, 0f, 0f));
            parte4.Posicion = new Vector3(0f, 0f, 0f);
            parte5.Posicion = new Vector3(1.5f, 0f, 0f);
            parte6.Posicion = new Vector3(0f, 0f, 0f);
            u2.AddParte(parte4);
            u2.AddParte(parte5);
            u2.AddParte(parte6);
            escenario.AddObjeto(u2);

            //Objeto u3 = new Objeto(new Vector3(2f, 0f, 0f));
            //u3.AddParte(parteU);
            //escenario.AddObjeto(u3);
            // Alejar la vista

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

            // GL.Translate(0f, 0f, -5f);
            // Rotar la figura en Y
            //GL.Rotate(angle, 0f, 1f, 0f);
          
            escenario.Dibujar();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            var objetos = escenario.GetObjetos();

            // Cambiar modo con tecla M
            if (KeyboardState.IsKeyPressed(Keys.M))
            {
                modoSeleccionIndividual = !modoSeleccionIndividual;
                Console.WriteLine(modoSeleccionIndividual
                    ? "🟢 Modo Selección Individual Activado"
                    : "🔵 Modo Mover Todos Activado");
            }

            if (modoSeleccionIndividual)
            {
                CambiarObjetoActivo(objetos);
            }



            if (KeyboardState.IsKeyPressed(Keys.Z))  // Alternar entre modo objeto/parte
            {
                modoParte = !modoParte;
                Console.WriteLine(modoParte ? "🎯 Modo Parte Activo" : "🟢 Modo Objeto Activo");
            }

            if (modoParte)
            {
                ControlarParte(escenario.GetObjetos()[objetoActivoIndex]);
            }
            // AplicarTransformaciones(objetos);
            else
            {
                 AplicarTransformaciones(objetos);
            }


        }
        private void CambiarObjetoActivo(List<Objeto> objetos)
        {
            if (KeyboardState.IsKeyPressed(Keys.Tab))
            {
                objetoActivoIndex = (objetoActivoIndex + 1) % objetos.Count;
                Console.WriteLine($"🎯 Objeto seleccionado: {objetoActivoIndex + 1} de {objetos.Count}");
            }
        }
        private void AplicarTransformaciones(List<Objeto> objetos)
        {
            List<Objeto> objetosControlados = modoSeleccionIndividual
                ? new List<Objeto> { objetos[objetoActivoIndex] }
                : objetos;

            foreach (var objeto in objetosControlados)
            {
                
                Vector3 pos = objeto.GetCentro();
                if (KeyboardState.IsKeyDown(Keys.Right)) pos.X += 0.05f;
                if (KeyboardState.IsKeyDown(Keys.Left)) pos.X -= 0.05f;
                if (KeyboardState.IsKeyDown(Keys.Up)) pos.Y += 0.05f;
                if (KeyboardState.IsKeyDown(Keys.Down)) pos.Y -= 0.05f;
                if (KeyboardState.IsKeyDown(Keys.R)) pos.Z -= 0.05f;
                if (KeyboardState.IsKeyDown(Keys.F)) pos.Z += 0.05f;
                objeto.Posicion = pos;

              
                Vector3 rot = objeto.GetRot();
                if (KeyboardState.IsKeyDown(Keys.I)) rot.X += 2f;
                if (KeyboardState.IsKeyDown(Keys.K)) rot.X -= 2f;
                if (KeyboardState.IsKeyDown(Keys.J)) rot.Y += 2f;
                if (KeyboardState.IsKeyDown(Keys.L)) rot.Y -= 2f;
                if (KeyboardState.IsKeyDown(Keys.U)) rot.Z += 2f;
                if (KeyboardState.IsKeyDown(Keys.O)) rot.Z -= 2f;
                objeto.Rotacion = rot;

              
                if (KeyboardState.IsKeyDown(Keys.W)) objeto.Escala += 0.01f;
                if (KeyboardState.IsKeyDown(Keys.S)) objeto.Escala -= 0.01f;
                if (objeto.Escala < 0.1f) objeto.Escala = 0.1f;
            }
        }
        private void ControlarParte(Objeto objeto)
        {
            var partes = objeto.GetPartes();

            if (partes.Count == 0) return;

            if (parteActivaIndex >= partes.Count)
                parteActivaIndex = partes.Count - 1;

            var parteSeleccionada = partes[parteActivaIndex];

            
            if (KeyboardState.IsKeyPressed(Keys.B))
            {
                parteActivaIndex = (parteActivaIndex + 1) % partes.Count;
                Console.WriteLine($"🔸 Parte activa: {parteActivaIndex + 1}/{partes.Count}");
            }

           
            Vector3 pos = parteSeleccionada.Posicion;
            if (KeyboardState.IsKeyDown(Keys.Right)) pos.X += 0.05f;
            if (KeyboardState.IsKeyDown(Keys.Left)) pos.X -= 0.05f;
            if (KeyboardState.IsKeyDown(Keys.Up)) pos.Y += 0.05f;
            if (KeyboardState.IsKeyDown(Keys.Down)) pos.Y -= 0.05f;
            parteSeleccionada.Posicion = pos;

            Vector3 rot = parteSeleccionada.Rotacion;
            if (KeyboardState.IsKeyDown(Keys.I)) rot.X += 2f;
            if (KeyboardState.IsKeyDown(Keys.K)) rot.X -= 2f;
            if (KeyboardState.IsKeyDown(Keys.J)) rot.Y += 2f;
            if (KeyboardState.IsKeyDown(Keys.L)) rot.Y -= 2f;
            if (KeyboardState.IsKeyDown(Keys.U)) rot.Z += 2f;
            if (KeyboardState.IsKeyDown(Keys.O)) rot.Z -= 2f;
            parteSeleccionada.Rotacion = rot;

            if (KeyboardState.IsKeyDown(Keys.W)) parteSeleccionada.Escala += 0.01f;
            if (KeyboardState.IsKeyDown(Keys.S)) parteSeleccionada.Escala -= 0.01f;
            if (parteSeleccionada.Escala < 0.1f) parteSeleccionada.Escala = 0.1f;

            // (Puedes agregar más transformaciones como rotación y escala)
        }
        private void ControlarObjetos(List<Objeto> objetos)
        {
            foreach (var objeto in objetos)
            {
                Vector3 pos = objeto.Posicion;
                if (KeyboardState.IsKeyDown(Keys.Right)) pos.X += 0.05f;
                if (KeyboardState.IsKeyDown(Keys.Left)) pos.X -= 0.05f;
                if (KeyboardState.IsKeyDown(Keys.Up)) pos.Y += 0.05f;
                if (KeyboardState.IsKeyDown(Keys.Down)) pos.Y -= 0.05f;
                if (KeyboardState.IsKeyDown(Keys.R)) pos.Z -= 0.05f;
                if (KeyboardState.IsKeyDown(Keys.F)) pos.Z += 0.05f;
                objeto.Posicion = pos;

                // Escala
                if (KeyboardState.IsKeyDown(Keys.W)) objeto.Escala += 0.01f;
                if (KeyboardState.IsKeyDown(Keys.S)) objeto.Escala -= 0.01f;
                if (objeto.Escala < 0.1f) objeto.Escala = 0.1f;
            }
        }

        protected override void OnUnload()
        {
            base.OnUnload();

        }
    }
}