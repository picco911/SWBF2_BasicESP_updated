using System;
using System.Diagnostics;
using System.Threading;
using Yato.DirectXOverlay;
using SharpDX;
using PimpMan;

namespace BasicESP
{
    public class Overlay
    {
        private IntPtr handle;
        private Process process = null;

        private Thread updateThread = null;
        private Thread gameCheckThread = null;
        private Thread ssCheckThread = null;
        private GameManager gameManager;

        private OverlayWindow overlay;
        private Direct2DRenderer d2d;
        private Direct2DBrush clearBrush;

        private bool IsGameRunning = true;

        private bool OPTIONS_AA = false;
        private bool OPTIONS_VSync = false;
        private bool OPTIONS_ShowFPS = true;
       

        public Overlay(Process gameProcess)
        {
            process = gameProcess;

            // check the game window exists then create the overlay
            while (true)
            {
                handle = NativeMethods.FindWindow(null, "STAR WARS Battlefront II");

                if (handle != IntPtr.Zero)
                {
                    break;
                }
            }

            // check if game running. timed at 2-5ms per call so runs in own thread
            gameCheckThread = new Thread(new ParameterizedThreadStart(GameCheck));
            gameCheckThread.Start();

            ssCheckThread = new Thread(new ParameterizedThreadStart(SSCheck));
            ssCheckThread.Start();

            // Starting the ESP before the game leaves invalid process info so we'll wait a second to let the game check thread fix that
            if (process.MainWindowHandle == IntPtr.Zero)
            {
                Thread.Sleep(1000);
            }

            // set up the remote process memory class
            RPM.OpenProcess(process.Id);

            // setup the overlay
            var rendererOptions = new Direct2DRendererOptions()
            {
                AntiAliasing = OPTIONS_AA,
                Hwnd = IntPtr.Zero,
                MeasureFps = OPTIONS_ShowFPS,
                VSync = OPTIONS_VSync
            };

            OverlayManager manager = new OverlayManager(handle, rendererOptions);

            overlay = manager.Window;
            d2d = manager.Graphics;
            clearBrush = d2d.CreateBrush(0xF5, 0xF5, 0xF5, 0);  // our transparent colour

            // start the update thread
            updateThread = new Thread(new ParameterizedThreadStart(Update));
            updateThread.Start();
        }

        private void SSCheck(object sender)
        {
            Pimp pimp = new Pimp(Pimp.BlockMethod.Zero, true, false, false);
            if (pimp.Inject("starwarsbattlefrontii"))
            {
                Console.WriteLine("bitblt Injection Complete");
                while (true)
                {
                    bool check = pimp.IsScreenShot();
                    if (check)
                    {
                        Console.WriteLine("Screenshot Taken by FairFight!");
                    }
                    System.Threading.Thread.Sleep(50);
                }
            }
        }
        private void GameCheck(object sender)
        {
            while (IsGameRunning)
            {
                Process[] pList = Process.GetProcessesByName("starwarsbattlefrontii");
                process = pList.Length > 0 ? pList[0] : null;
                if (process == null)
                {
                    IsGameRunning = false;
                }

                Thread.Sleep(100);
            }
        }

        private void Update(object sender)
        {
            // set up our colours for drawing
            var blackBrush = d2d.CreateBrush(0, 0, 0, 255);
            var redBrush = d2d.CreateBrush(255, 0, 0, 255);
            var yellowBrush = d2d.CreateBrush(255, 255, 0, 255);
            var orangeBrush = d2d.CreateBrush(255, 200, 0, 255);
            var darkOrangeBrush = d2d.CreateBrush(255, 100, 0, 255);
            var greenBrush = d2d.CreateBrush(0, 255, 0, 255);
            var blueBrush = d2d.CreateBrush(0, 0, 255, 255);
            var whiteBrush = d2d.CreateBrush(240, 240, 240, 255);
            var vehicleBrush = d2d.CreateBrush(200, 200, 240, 255);
            var heroVisBrush = d2d.CreateBrush(240, 0, 255, 255);
            var heroBrush = d2d.CreateBrush(120, 0, 120, 255);

            // and our font
            var font = d2d.CreateFont("Consolas", 11);

            Direct2DBrush brush;

            Console.WriteLine("Initialising...");

            // initialise the GameManager class that handles the player data
            gameManager = new GameManager(process, new Rectangle(0, 0, overlay.Width, overlay.Height));

            Console.WriteLine("Ready.");

            // main loop
            while (IsGameRunning)
            {
                if (gameManager.UpdateFrame(new Rectangle(0, 0, overlay.Width, overlay.Height)))
                {
                    d2d.BeginScene();
                    d2d.ClearScene(clearBrush);

                    if (OPTIONS_ShowFPS)
                    {
                        d2d.DrawTextWithBackground($"FPS: {d2d.FPS}", 20, 20, font, greenBrush, blackBrush);
                    }

                    foreach (Player player in gameManager.AllPlayers)
                    {
                        if (player.Distance < 800)
                        {
                            if (!player.IsDead)
                            {
                                if (player.IsVisible)
                                {
                                    if (player.MaxHealth < 400) brush = yellowBrush;
                                    else brush = heroVisBrush;
                                }
                                else
                                {
                                    if (player.MaxHealth < 400) brush = redBrush;
                                    else brush = heroBrush;
                                }

                                if (!player.InVehicle)
                                {
                                    //DrawAABB(player.TransformAABB, brush);
                                    //Vector3 head = player.Position;
                                    //head.Y += 20 + (float)0.25;
                                    //Vector3 foot = player.Position;
                                    //var heightoffset = Distance3D(foot, head);
                                    //float factor = ((float)(heightoffset / 5));

                                    //Vector3 m2 = new Vector3(head.X - factor, head.Y, 0);
                                    //Vector3 m1 = new Vector3(head.X + factor, head.Y, 0);
                                    //Vector3 m3 = new Vector3(foot.X - factor, foot.Y, 0);
                                    //Vector3 m4 = new Vector3(foot.X + factor, foot.Y, 0);

                                    //d2d.DrawLine(m1.X, m1.Y, m2.X, m2.Y, 1, brush);
                                    //d2d.DrawLine(m2.X, m2.Y, m3.X, m3.Y, 1, brush);
                                    //d2d.DrawLine(m3.X, m3.Y, m4.X, m4.Y, 1, brush);
                                    //d2d.DrawLine(m4.X, m4.Y, m1.X, m1.Y, 1, brush);
                                }
                                else
                                {
                                    brush = vehicleBrush;

                                    DrawAABB(player.TransformAABB, brush);
                                }

                                var name = string.Format("{0}[{1}]", player.Name, player.Health);
                                var dist = $"{(int)player.Distance}m";


                                Vector3 textPos = new Vector3(player.Position.X, player.Position.Y, player.Position.Z);
                                if (gameManager.WorldToScreen(textPos, out textPos))
                                {
                                    var textPosX = textPos.X - ((name.Length * font.FontSize) / 4);
                                    d2d.DrawText(name, textPosX - 1, textPos.Y - 1, font, blackBrush);
                                    d2d.DrawText(name, textPosX, textPos.Y, font, whiteBrush);

                                    textPosX = textPos.X - ((dist.Length * font.FontSize) / 4);
                                    var textPosY = textPos.Y + font.FontSize;

                                    d2d.DrawText(dist, textPosX - 1, textPosY - 1, font, blackBrush);
                                    d2d.DrawText(dist, textPosX, textPosY, font, whiteBrush);
                                }
                            }
                        }
                    }

                    d2d.EndScene();
                }
            }

            // clean up if the game has closed
            RPM.CloseProcess();
            Environment.Exit(0);
        }

        double Distance3D(Vector3 v1, Vector3 v2)
        {
            float x_d = (v2.X - v1.X);
            float y_d = (v2.Y - v1.Y);
            float z_d = (v2.Z - v1.Z);
            return Math.Sqrt((x_d * x_d) + (y_d * y_d) + (z_d * z_d));
        }
        private void DrawAABB(Frostbite.TransformAABBStruct TransformAABB, Direct2DBrush brush)
        {
            Vector3 pos = TransformAABB.Matrix.TranslationVector;

            Vector3 min = new Vector3(TransformAABB.AABB.Min.X, TransformAABB.AABB.Min.Y, TransformAABB.AABB.Min.Z);
            Vector3 max = new Vector3(TransformAABB.AABB.Max.X, TransformAABB.AABB.Max.Y, TransformAABB.AABB.Max.Z);

            Vector3 crnr2 = pos + gameManager.MultiplyMat(new Vector3(max.X, min.Y, min.Z), TransformAABB.Matrix);
            Vector3 crnr3 = pos + gameManager.MultiplyMat(new Vector3(max.X, min.Y, max.Z), TransformAABB.Matrix);
            Vector3 crnr4 = pos + gameManager.MultiplyMat(new Vector3(min.X, min.Y, max.Z), TransformAABB.Matrix);
            Vector3 crnr5 = pos + gameManager.MultiplyMat(new Vector3(min.X, max.Y, max.Z), TransformAABB.Matrix);
            Vector3 crnr6 = pos + gameManager.MultiplyMat(new Vector3(min.X, max.Y, min.Z), TransformAABB.Matrix);
            Vector3 crnr7 = pos + gameManager.MultiplyMat(new Vector3(max.X, max.Y, min.Z), TransformAABB.Matrix);

            min = pos + gameManager.MultiplyMat(min, TransformAABB.Matrix);
            max = pos + gameManager.MultiplyMat(max, TransformAABB.Matrix);

            if (!gameManager.WorldToScreen(min, out min) || !gameManager.WorldToScreen(max, out max)
                || !gameManager.WorldToScreen(crnr2, out crnr2) || !gameManager.WorldToScreen(crnr3, out crnr3)
                || !gameManager.WorldToScreen(crnr4, out crnr4) || !gameManager.WorldToScreen(crnr5, out crnr5)
                || !gameManager.WorldToScreen(crnr6, out crnr6) || !gameManager.WorldToScreen(crnr7, out crnr7))
                return;

            d2d.DrawLine(min.X, min.Y, crnr2.X, crnr2.Y, 1, brush);
            d2d.DrawLine(min.X, min.Y, crnr4.X, crnr4.Y, 1, brush);
            d2d.DrawLine(min.X, min.Y, crnr6.X, crnr6.Y, 1, brush);

            d2d.DrawLine(max.X, max.Y, crnr5.X, crnr5.Y, 1, brush);
            d2d.DrawLine(max.X, max.Y, crnr7.X, crnr7.Y, 1, brush);
            d2d.DrawLine(max.X, max.Y, crnr3.X, crnr3.Y, 1, brush);

            d2d.DrawLine(crnr2.X, crnr2.Y, crnr7.X, crnr7.Y, 1, brush);
            d2d.DrawLine(crnr2.X, crnr2.Y, crnr3.X, crnr3.Y, 1, brush);

            d2d.DrawLine(crnr4.X, crnr4.Y, crnr5.X, crnr5.Y, 1, brush);
            d2d.DrawLine(crnr4.X, crnr4.Y, crnr3.X, crnr3.Y, 1, brush);

            d2d.DrawLine(crnr6.X, crnr6.Y, crnr5.X, crnr5.Y, 1, brush);
            d2d.DrawLine(crnr6.X, crnr6.Y, crnr7.X, crnr7.Y, 1, brush);
        }
    }
}
