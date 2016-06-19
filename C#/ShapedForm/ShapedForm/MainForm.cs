namespace ShapedForm
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// Main form
    /// </summary>
    public partial class MainForm : Form
    {

        /// <summary>
        /// WM_NCLBUTTONDOWN
        /// </summary>
        public const int WM_NCLBUTTONDOWN = 0xA1;

        /// <summary>
        /// HT_CAPTION
        /// </summary>
        public const int HT_CAPTION = 0x2;

        /// <summary>
        /// API call for sending message
        /// </summary>
        /// <param name="hWnd">window handle</param>
        /// <param name="Msg">the message</param>
        /// <param name="wParam">wparam</param>
        /// <param name="lParam">lparam</param>
        /// <returns></returns>
        [DllImportAttribute("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        /// <summary>
        /// Release mouse capture
        /// </summary>
        /// <returns></returns>
        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when the form loads
        /// </summary>
        /// <param name="sender">what triggered the event</param>
        /// <param name="e">event args</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // make cool shape
            Region r = new Region();
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(new Rectangle(0, 0, this.Width, this.Height));
            r.MakeEmpty();
            r.Union(gp);
            gp = new GraphicsPath();
            gp.AddRectangle(new Rectangle(this.Width / 2, 0, this.Width / 2, this.Height / 2));
            r.Union(gp);
            this.Region = r;
        }

        /// <summary>
        /// When a user clicks the form
        /// </summary>
        /// <param name="sender">what triggered the event</param>
        /// <param name="e">event args</param>
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
