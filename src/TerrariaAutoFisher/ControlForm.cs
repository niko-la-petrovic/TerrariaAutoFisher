using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Threading;
using System.Reflection;
using Np.Windows.DllImports;
using Np.Windows.Hooks;

namespace TerrariaAutoFisher
{
    public partial class ControlForm : Form
    {
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        bool active = false;
        bool fishing = false;
        bool shooting = false;
        // TODO add checkbox
        bool showPreview = false;

        Keys lastKey = new Keys();
        int[] currentCoords = new int[2];
        int[] initialCoords = new int[2];
        int[] finalCoords = new int[2];

        public int StartingRed { get; private set; } = 0;

        int shootingDelay = 20;
        int held = 10;
        int red = 2000;
        int refishingDelay = 1000;

        IntPtr terrHandle;
        OverlayForm overlay;
        HookManager hookManager;

        public ControlForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hookManager = new HookManager(Assembly.GetExecutingAssembly().GetModules()[0]);
            SetHooks();

            overlay = new OverlayForm(this);
            overlay.Show();

            ObtainTerrariaHandle();
        }

        protected bool SetHooks()
        {
            if (hookManager == null)
                return false;

            bool hookedMouse = hookManager.SetMouseHook();
            bool hookedKeyboard = hookManager.SetKeyboardHook();

            hookManager.MouseAction += new MouseEventHandler(MouseMoved);
            hookManager.KeyDown += new KeyEventHandler(KeyDown);

            return hookedMouse && hookedKeyboard;
        }

        public new void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.NumPad4)
            {
                if (active)
                {
                    active = false; overlay.FastTimerDisable();
                    overlay.SlowTimerDisable();
                    overlay.SetStateText("Deactivated - Num4");
                    overlay.SetStateColor("DarkRed");
                }
                else
                {
                    active = true;
                    overlay.SlowTimerEnable();
                    overlay.FastTimerEnable();
                    overlay.SetStateText("Activated - Num4");
                    overlay.SetStateColor("Lime");
                }
            }
            if (active == true)
            {
                if (e.KeyData == Keys.NumPad5)
                {
                    if (lastKey != Keys.NumPad5)
                    {
                        initialCoords[0] = currentCoords[0];
                        initialCoords[1] = currentCoords[1];
                        lastKey = Keys.NumPad5;

                        OverlaySendCoords(initialCoords, currentCoords);
                    }
                    else
                    {
                        finalCoords[0] = currentCoords[0];
                        finalCoords[1] = currentCoords[1];
                        OverlaySendCoords(initialCoords, finalCoords);
                        lastKey = new Keys();
                    }
                }
                if (e.KeyData == Keys.NumPad2)
                {
                    if (lastKey == Keys.NumPad2)
                    {
                        finalCoords[0] = currentCoords[0];
                        finalCoords[1] = currentCoords[1];
                        lastKey = new Keys();
                    }
                    else
                    {
                        initialCoords[0] = currentCoords[0];
                        initialCoords[1] = currentCoords[1];
                    }
                    lastKey = e.KeyData;
                }
                if (e.KeyData == Keys.F)
                {
                    // TODO use mutex over fishing
                    if (fishing == false)
                    {
                        if (lastKey != Keys.F)
                        {
                            StartingRed = CalculateRed();
                            fishing = true;

                            overlay.SetFishingText("Fishing - F");
                            overlay.SetFishingColor("Lime");

                            new Thread(() =>
                            {
                                while (fishing == true)
                                {
                                    if (CalculateRedDifference() >= red)
                                    {
                                        //Console.Beep();
                                        Thread.Sleep(700);
                                        //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)currentCoords[0], (uint)currentCoords[1], 0, 0);
                                        //ClickOnPoint(terrHandle, new Point(Cursor.Position.X, Cursor.Position.Y));
                                        //ClickOnPoint(terrHandle, new Point(Cursor.Position.X, Cursor.Position.Y));
                                        //ClickOnPoint(terrHandle, new Point(currentCoords[0], currentCoords[1]));
                                        //ClickOnPoint(terrHandle, new Point(currentCoords[0], currentCoords[1]));
                                        MouseEvent.mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
                                        Thread.Sleep(50);
                                        MouseEvent.mouse_event(MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
                                        //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
                                        Console.Beep();
                                        //How long the pause is between two clicks -- should be the length of the pole recall animation
                                        Thread.Sleep(refishingDelay);
                                        //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)currentCoords[0], (uint)currentCoords[1], 0, 0);
                                        //ClickOnPoint(terrHandle, new Point(currentCoords[0], currentCoords[1]));
                                        //ClickOnPoint(terrHandle, new Point(currentCoords[0], currentCoords[1]));
                                        //ClickOnPoint(terrHandle, new Point(Cursor.Position.X, Cursor.Position.Y));
                                        //ClickOnPoint(terrHandle, new Point(Cursor.Position.X, Cursor.Position.Y));
                                        MouseEvent.mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
                                        Thread.Sleep(50);
                                        MouseEvent.mouse_event(MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
                                        //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);

                                        //Thread.Sleep(500); too short?
                                        Thread.Sleep(1500);

                                        StartingRed = CalculateRed();
                                    }
                                    Thread.Sleep(1 / 60 * 1000);
                                }
                            }).Start();
                        }
                    }
                    else
                    {
                        fishing = false;

                        overlay.SetFishingText("Not Fishing - F");
                        overlay.SetFishingColor("DarkRed");

                        //lastKey = Keys.F;
                    }
                }
                if (e.KeyCode == Keys.NumPad0)
                {
                    // TODO use mutex over shooting
                    if (shooting == false)
                    {
                        shooting = true;
                        new Thread(() =>
                        {
                            while (shooting == true)
                            {
                                Thread.Sleep(shootingDelay);
                                MouseEvent.mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
                                Thread.Sleep(held);
                                MouseEvent.mouse_event(MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
                            }
                        }).Start();
                    }
                    else
                        shooting = false;
                }
                if (e.KeyCode == Keys.NumPad1)
                {
                    try
                    {
                        red = CalculateRedDifference();
                        numericRed.Value = red;
                    }
                    catch { }
                }
                if (e.KeyCode == Keys.NumPad3)
                    ObtainTerrariaHandle();
            }
        }

        public int CalculateRedDifference()
        {
            return Math.Abs(StartingRed - CalculateRed());
        }

        public void MouseMoved(object sender, MouseEventArgs e)
        {
            currentCoords[0] = e.X;
            currentCoords[1] = e.Y;
            if (lastKey == Keys.NumPad5)
                OverlaySendCoords(initialCoords, currentCoords);
        }

        public int CalculateRed()
        {
            try
            {
                ScreenCapture sc = new ScreenCapture();
                // TODO nullcheck
                using (Image img = sc.CaptureScreen())
                {
                    // if(showPreview)
                    //ChangePic(img);
                    PixelFormat format = img.PixelFormat;
                    Rectangle rect = new Rectangle(
                        initialCoords[0],
                        initialCoords[1],
                        finalCoords[0] - initialCoords[0],
                        finalCoords[1] - initialCoords[1]);
                    // TODO perf. improvements - could access the existing img instance, but 
                    // use logic while accessing
                    using (Bitmap test = new Bitmap(img).Clone(rect, format))
                    {
                        //if(showPreview)...
                        //ChangePic(test);
                        Rectangle full = new Rectangle(
                            0,
                            0,
                            finalCoords[0] - initialCoords[0] - 1,
                            finalCoords[1] - initialCoords[1] - 1);
                        BitmapData small = test.LockBits(full, ImageLockMode.ReadWrite, format);
                        int depth = Image.GetPixelFormatSize(small.PixelFormat);
                        int step = depth / 8;
                        int pixelCount = small.Width * small.Height;

                        byte[] pixels = new byte[pixelCount * step];
                        IntPtr ptr = small.Scan0;
                        Marshal.Copy(ptr, pixels, 0, pixels.Length);

                        int sum = 0;
                        for (int i = 0; i < pixels.Length / 3; i += 3)
                        {
                            sum += pixels[i];
                        }

                        return sum;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return 0;
        }

        public void ChangePic(Image img)
        {
            picturePreview.Image = img;
            picturePreview.Width = img.Width;
            picturePreview.Height = img.Height;
            picturePreview.Refresh();
        }

        public void OverlaySendCoords(int[] initial, int[] final)
        {
            overlay.UpdateCoords(initial, final);
        }

        public void ObtainTerrariaHandle()
        {
            bool success = false;
            try
            {
                Process[] processes = Process.GetProcessesByName("Terraria");
                if (processes.Length == 0)
                    return;

                terrHandle = processes[0].MainWindowHandle;
                success = true;
            }
            catch (Exception)
            {
                //overlay.SetErrorText(ex.Message);
            }
            finally
            {
                if (!success)
                    overlay.SetErrorText("Terraria not running?\nPress Num3 to re-lock");
            }
        }

        #region UI

        private void NumericRefishDelay_ValueChanged(object sender, EventArgs e)
        {
            try { refishingDelay = (int)numericRefishDelay.Value; }
            catch
            {
                refishingDelay = 1000;
            }
        }
        private void NumericShootingDelay_ValueChanged(object sender, EventArgs e)
        {
            try { shootingDelay = (int)numericDelay.Value; }
            catch
            {
                shootingDelay = 20;
            }
        }
        private void NumericShootingDuration_ValueChanged(object sender, EventArgs e)
        {
            try { held = (int)numericDuration.Value; }
            catch
            {
                held = 10;
            }
        }
        private void Red_ValueChanged(object sender, EventArgs e)
        {
            try { red = (int)numericRed.Value; }
            catch
            {
                red = 10;
            }
        }

        #endregion

    }
}
