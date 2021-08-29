using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace TerrariaAutoFisher
{
    public partial class OverlayForm : Form
    {

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        #region Dll Imports

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        #endregion

        ControlForm controlForm;

        int[] currentCoords = new int[2];
        int[] initialCoords = new int[2];
        int[] finalCoords = new int[2];

        int[] reds = new int[10];
        int indexer = 0;

        public OverlayForm(ControlForm form)
        {
            controlForm = form;
            InitializeComponent();
        }

        private void OverlayForm_Load(object sender, EventArgs e)
        {
            TopMost = true;

            SetWindowPos(Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);

            WindowState = FormWindowState.Maximized;

            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.Pink;
            TransparencyKey = Color.Pink;

            Opacity = 0.7;

            //

            ShowInTaskbar = false;

            //

            //fastTimer.Enabled = false;
            //fastTimer.Interval = 100;
            //fastTimer.Tick += new EventHandler(FastTimer);

            //for clickthrough
            int initialStyle = GetWindowLong(Handle, -20);
            SetWindowLong(new HandleRef(this, Handle), -20, initialStyle | 0x80000 | 0x20);
        }

        public void UpdateCoords(int[] initial, int[] final)
        {
            initialCoords = initial;
            finalCoords = final;
            //this.Invalidate();
        }

        public Rectangle CoordsRect()
        {
            return new Rectangle(initialCoords[0], initialCoords[1], finalCoords[0] - initialCoords[0], finalCoords[1] - initialCoords[1]);
        }

        #region Timers
        public void FastTimerToggle()
        {
            if (fastTimer.Enabled == true)
            {
                fastTimer.Enabled = false;
            }
            else
            {
                fastTimer.Enabled = true;
            }
        }

        public void FastTimerDisable()
        {
            fastTimer.Enabled = false;

        }

        public void FastTimerEnable()
        {
            fastTimer.Enabled = true;
        }

        public void SlowTimerToggle()
        {
            if (slowTimer.Enabled == true)
            {
                slowTimer.Enabled = false;
            }
            else
            {
                slowTimer.Enabled = true;
            }
        }

        public void SlowTimerDisable()
        {
            slowTimer.Enabled = false;
        }

        public void SlowTimerEnable()
        {
            slowTimer.Enabled = true;
        }

        private void FastTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
        
        private void SlowTimer_Tick(object sender, EventArgs e)
        {
            int calculatedRed = controlForm.CalculateRed();
            int startingRed = controlForm.StartingRed;

            AddRed(calculatedRed);

            labelStartRed.Text = "Start red: " + startingRed;
            labelCurrentRed.Text = "Current red: " + calculatedRed;
            labelAverageRed.Text = "Average red: " + MeanReds();
            labelRedDiff.Text = " Red Diff: " + (startingRed - calculatedRed);
        }
        #endregion

        private void TestDraw(Graphics graphics)
        {
            SolidBrush brush = new SolidBrush(Color.Red);
            Pen pen = new Pen(brush);
            Graphics formGraphics = CreateGraphics();
            formGraphics.DrawRectangle(pen, new Rectangle(0, 0, 200, 300));
            brush.Dispose();
            pen.Dispose();
        }

        private void DrawSelectRect(Graphics graphics)
        {
            if (initialCoords[0]!=0 && initialCoords[1] != 0 && finalCoords[0]!=0 && finalCoords[1]!=0)
            {
                SolidBrush brush = new SolidBrush(Color.Red);
                Pen pen = new Pen(brush, 2);
                Graphics formGraphics = CreateGraphics();

                Rectangle rect = CoordsRect();

                formGraphics.DrawRectangle(pen, rect);
                brush.Dispose();
                pen.Dispose();
                //Debug.WriteLine("Initial: x{0},y{1} | Final: x{2},y{3}", initialCoords[0], initialCoords[1],finalCoords[0],finalCoords[1]);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //TestDraw(e.Graphics);
            DrawSelectRect(e.Graphics);
        }

        public void AddRed(int red)
        {
            if (indexer == 9)
            {
                indexer = 0;
            }
            reds[indexer] = red;
            indexer++;
        }

        public double MeanReds()
        {
            int mean = 0;
            for(int i = 0; i < reds.Length; i++)
            {
                mean += reds[i];
            }
            return Convert.ToDouble(mean) / 10;
        }


        #region Overlay label changes

        public void SetErrorText(string errorText)
        {
            labelError.Text = errorText;
        }

        public void SetStateText(string text)
        {
            labelState.Text = text;

        }

        public void SetStateColor(string text)
        {
            labelState.ForeColor = Color.FromName(text);
        }

        public void SetFishingText(string text)
        {
            labelFishing.Text = text;
        }

        public void SetFishingColor(string text)
        {
            labelFishing.ForeColor = Color.FromName(text);
        }
        #endregion
    }
}
