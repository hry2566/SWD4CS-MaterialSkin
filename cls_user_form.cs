using System;
using System.Reflection;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;


namespace SWD4CS
{
    internal class cls_userform : MaterialForm
    {
        internal MainForm? mainForm;
        internal TabPage? backPanel;
        private cls_selectbox? selectBox;
        private bool selectFlag = false;
        private int grid = 4;
        internal int cnt_Control = -1;
        internal List<cls_controls> CtrlItems = new();
        internal List<string> decHandler = new();
        internal List<string> decFunc = new();

        // internal ImageList imgLst;
        // private System.ComponentModel.IContainer components = null;

        public cls_userform()
        {
            this.FormClosing += new FormClosingEventHandler(userForm_FormClosing);
            this.Resize += new EventHandler(userForm_Resize);
            this.Click += new EventHandler(Form_Click);

            this.TopLevel = false;
            this.Text = "Form1";
            this.Show();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            // 
            // imageList1
            // 
            // System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            // this.imgLst.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            // this.imgLst.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLst.ImageStream")));
            // this.imgLst.TransparentColor = System.Drawing.Color.Transparent;
            // this.imgLst.Images.SetKeyName(0, "close_FILL1_wght400_GRAD0_opsz48.png");
            // this.imgLst.Images.SetKeyName(1, "home_FILL1_wght400_GRAD0_opsz48.png");
            // this.imgLst.Images.SetKeyName(2, "menu_FILL1_wght400_GRAD0_opsz48.png");
            // this.imgLst.Images.SetKeyName(3, "settings_FILL1_wght400_GRAD0_opsz48.png");
            // this.imgLst = new System.Windows.Forms.ImageList(this.components);
        }

        // ********************************************************************************************
        // Event Function 
        // ********************************************************************************************
        private void Form_Click(object? sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (mainForm!.toolLstBox!.Text == "" || mainForm!.toolLstBox!.SelectedIndex == -1)
            {
                if (me.Button == MouseButtons.Left)
                {
                    if (selectFlag)
                    {
                        SetSelect(false);
                    }
                    else
                    {
                        SelectAllClear();
                        SetSelect(true);
                    }
                }
            }
            else
            {
                SelectAllClear();

                int X = (int)(me.X / grid) * grid;
                int Y = (int)(me.Y / grid) * grid;
                _ = new cls_controls(this, mainForm!.toolLstBox!.Text, this, X, Y);
                mainForm!.toolLstBox!.SelectedIndex = 0;
            }
        }

        private void userForm_Resize(object? sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized ||
                this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void userForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const long SC_SIZE = 0xF000L;
            const long SC_MOVE = 0xF010L;

            if (m.Msg == WM_SYSCOMMAND && ((m.WParam.ToInt64() & 0xFFF0L) == SC_MOVE ||
                                           (m.WParam.ToInt64() & 0xFFF0L) == SC_SIZE))
            {
                m.Result = IntPtr.Zero;
                return;
            }

            base.WndProc(ref m);
        }
        private void BackPanel_Click(object? sender, EventArgs e)
        {
            SelectAllClear();
        }

        // ********************************************************************************************
        // Public Function
        // ********************************************************************************************
        internal void Init(MainForm mainForm, TabPage backPanel)
        {
            this.mainForm = mainForm;
            this.backPanel = backPanel;
            this.backPanel.Click += new EventHandler(BackPanel_Click);
            selectBox = new cls_selectbox(this, backPanel);
            SetSelect(true);
        }
        internal void SetSelect(bool flag)
        {
            selectFlag = flag;
            selectBox!.SetSelectBoxPos(selectFlag);
            ShowProperty(flag);
            if (flag)
            {
                mainForm!.ctrlTree!.SelectedNode = mainForm.ctrlTree.TopNode;
            }
            // mainForm!.eventView!.ShowEventList(flag, this);
        }
        internal void SelectAllClear()
        {
            SetSelect(false);

            for (int i = 0; i < CtrlItems!.Count; i++)
            {
                CtrlItems[i].Selected = false;
            }
        }


        // ********************************************************************************************
        // Private Function
        // ********************************************************************************************
        private void ShowProperty(bool flag)
        {
            if (flag)
            {
                mainForm!.propertyGrid!.SelectedObject = this;
                if (this.GetType() == typeof(cls_userform))
                {
                    mainForm.propertyCtrlName!.Text = "";
                }
                else
                {
                    mainForm.propertyCtrlName!.Text = this.Name;
                }
            }
            else
            {
                mainForm!.propertyGrid!.SelectedObject = null;
                mainForm.propertyCtrlName!.Text = "";
            }
        }
    }
}
