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
using Np.Windows.Imaging;

namespace TerrariaAutoFisher
{
    public partial class ControlForm : Form
    {
        public int StartingRed { get; private set; } = 0;
        public int CurrentRed { get; private set; } = 0;
        public int CurrentRedDifference { get; private set; } = 0;

        bool active = false;
        bool fishing = false;
        bool shooting = false;
        bool showPreview = false;

        Keys lastKey = new Keys();

        int[] currentCoords = new int[2];
        int[] initialCoords = new int[2];
        int[] finalCoords = new int[2];

        int shootingDelay = 20;
        int held = 10;
        int redThreshold = 2000;
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
            if (!SetHooks())
            {
                DialogResult dialogResult = MessageBox.Show("Failed to set one or both hooks Try again.", "Error",
                       MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (dialogResult == DialogResult.Cancel)
                    Application.Exit();
            }

            overlay = new OverlayForm(this);
            overlay.Show();

            colorUpdateTimer.Interval = 100;

            ObtainTerrariaHandle();
        }

        protected bool SetHooks()
        {
            if (hookManager == null)
                return false;

            bool hookedMouse = hookManager.SetMouseHook();
            bool hookedKeyboard = hookManager.SetKeyboardHook();

            hookManager.MouseAction += new MouseEventHandler(MouseMoved);

            hookManager.KeyDown += new KeyEventHandler(KeyDown_NumPad0);
            hookManager.KeyDown += new KeyEventHandler(KeyDown_NumPad1);
            hookManager.KeyDown += new KeyEventHandler(KeyDown_NumPad2);
            hookManager.KeyDown += new KeyEventHandler(KeyDown_NumPad3);
            hookManager.KeyDown += new KeyEventHandler(KeyDown_NumPad4);
            hookManager.KeyDown += new KeyEventHandler(KeyDown_NumPad5);
            hookManager.KeyDown += new KeyEventHandler(KeyDown_F);

            bool allSuccessful = hookedMouse && hookedKeyboard;

            if (!allSuccessful)
            {
                if (!hookedMouse)
                    Console.Error.WriteLine("Failed to hook the mouse.");
                else
                    Console.Error.WriteLine("Failed to hook the keyboard.");
            }

            return allSuccessful;
        }

        #region Event Handlers

        /// <summary>
        /// Treat other keys as active.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KeyDown_NumPad4(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.NumPad4)
            {
                if (active)
                {
                    active = false;
                    colorUpdateTimer.Enabled = false;
                    overlay.FastTimerDisable();
                    overlay.SlowTimerDisable();
                    overlay.SetStateText("Deactivated [Num4]");
                    overlay.SetStateColor("DarkRed");
                }
                else
                {
                    active = true;
                    colorUpdateTimer.Enabled = true;
                    overlay.SlowTimerEnable();
                    overlay.FastTimerEnable();
                    overlay.SetStateText("Activated [Num4]");
                    overlay.SetStateColor("Lime");
                }
            }
        }

        /// <summary>
        /// Set overlay.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KeyDown_NumPad5(object sender, KeyEventArgs e)
        {
            if (active && e.KeyData == Keys.NumPad5)
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
        }

        /// <summary>
        /// Adjust initial and final coords.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KeyDown_NumPad2(object sender, KeyEventArgs e)
        {
            if (active && e.KeyData == Keys.NumPad2)
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
        }

        /// <summary>
        /// Start fishing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KeyDown_F(object sender, KeyEventArgs e)
        {
            if (active && e.KeyData == Keys.F)
            {
                // TODO use mutex over fishing
                if (!fishing)
                {
                    if (lastKey != Keys.F)
                    {
                        StartingRed = CurrentRed;
                        fishing = true;

                        overlay.SetFishingText("Fishing [F]");
                        overlay.SetFishingColor("Lime");

                        new Thread(() =>
                        {
                            while (fishing)
                            {
                                if (CalculateRedDifference() >= redThreshold)
                                {
                                    Thread.Sleep(700);

                                    MouseEvent.mouse_event((uint)MouseEvent.MouseEventFlags.MOUSEEVENTF_LEFTDOWN,
                                        (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
                                    Thread.Sleep(50);

                                    MouseEvent.mouse_event((uint)MouseEvent.MouseEventFlags.MOUSEEVENTF_LEFTUP,
                                        (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
                                    Console.Beep();
                                    Thread.Sleep(refishingDelay);

                                    MouseEvent.mouse_event((uint)MouseEvent.MouseEventFlags.MOUSEEVENTF_LEFTDOWN,
                                        (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
                                    Thread.Sleep(50);

                                    MouseEvent.mouse_event((uint)MouseEvent.MouseEventFlags.MOUSEEVENTF_LEFTUP,
                                        (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);

                                    Thread.Sleep(1500);

                                    StartingRed = CurrentRed;
                                }
                                Thread.Sleep(1 / 60 * 1000);
                            }
                        }).Start();
                    }
                }
                else
                {
                    fishing = false;

                    overlay.SetFishingText("Not Fishing [F]");
                    overlay.SetFishingColor("DarkRed");
                }
            }
        }

        /// <summary>
        /// Toggle auto-shoot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KeyDown_NumPad0(object sender, KeyEventArgs e)
        {
            if (active && e.KeyCode == Keys.NumPad0)
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
                            MouseEvent.mouse_event((uint)MouseEvent.MouseEventFlags.MOUSEEVENTF_LEFTDOWN,
                                (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
                            Thread.Sleep(held);
                            MouseEvent.mouse_event((uint)MouseEvent.MouseEventFlags.MOUSEEVENTF_LEFTUP,
                                (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
                        }
                    }).Start();
                }
                else
                    shooting = false;
            }
        }

        /// <summary>
        /// Adjust red threshold to current difference.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void KeyDown_NumPad1(object sender, KeyEventArgs e)
        {
            if (active && e.KeyCode == Keys.NumPad1)
            {
                redThreshold = CalculateRedDifference();
                numericRed.Value = redThreshold;
            }
        }

        /// <summary>
        /// Obtain Terraria handle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void KeyDown_NumPad3(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad3)
                ObtainTerrariaHandle();
        }

        #endregion

        protected int CalculateRedDifference()
        {
            return Math.Abs(StartingRed - CurrentRed);
        }

        protected void MouseMoved(object sender, MouseEventArgs e)
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
                using Image img = WindowCapture.CaptureEntireDesktop();

                PixelFormat format = img.PixelFormat;
                Rectangle rect = new Rectangle(
                    initialCoords[0],
                    initialCoords[1],
                    finalCoords[0] - initialCoords[0],
                    finalCoords[1] - initialCoords[1]);

                if (rect.IsEmpty)
                    return 0;

                using Bitmap croppedBitmap = new Bitmap(img).Clone(rect, format);
                if (showPreview)
                    UpdatePreview(croppedBitmap);

                Rectangle full = new Rectangle(
                    0,
                    0,
                    finalCoords[0] - initialCoords[0] - 1,
                    finalCoords[1] - initialCoords[1] - 1);
                BitmapData small = croppedBitmap.LockBits(full, ImageLockMode.ReadWrite, format);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return 0;
            }
        }

        protected void UpdatePreview(Image img)
        {
            picturePreview.Image = img;
            picturePreview.Width = img.Width;
            picturePreview.Height = img.Height;
            picturePreview.Invalidate();
            picturePreview.Update();
        }

        protected void OverlaySendCoords(int[] initial, int[] final)
        {
            overlay.UpdateCoords(initial, final);
        }

        protected void ObtainTerrariaHandle()
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
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (!success)
                    overlay.SetErrorText("Terraria not running?\nPress Num3 to retry.");
            }
        }

        #region UI

        private void NumericRefishDelay_ValueChanged(object sender, EventArgs e)
        {
            refishingDelay = (int)numericRefishDelay.Value;
        }
        private void NumericShootingDelay_ValueChanged(object sender, EventArgs e)
        {
            shootingDelay = (int)numericDelay.Value;
        }
        private void NumericShootingDuration_ValueChanged(object sender, EventArgs e)
        {
            held = (int)numericDuration.Value;
        }
        private void Red_ValueChanged(object sender, EventArgs e)
        {
            redThreshold = (int)numericRed.Value;
        }

        private void showPreviewCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            showPreview = showPreviewCheckBox.Checked;
        }

        #endregion

        private void colorUpdateTimer_Tick(object sender, EventArgs e)
        {
            CurrentRed = CalculateRed();
            CurrentRedDifference = CalculateRedDifference();
        }
    }
}
