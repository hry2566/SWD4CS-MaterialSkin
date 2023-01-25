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
        // internal Function
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
            // if (flag)
            // {
            //     mainForm!.ctrlTree!.SelectedNode = mainForm.ctrlTree.TopNode;
            // }
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
        internal void CtrlAllClear()
        {
            for (int i = 0; i < CtrlItems!.Count; i++)
            {
                CtrlItems[i].Selected = true;
            }
            RemoveSelectedItem();
        }
        internal void RemoveSelectedItem()
        {
            for (int i = 0; i < CtrlItems!.Count; i++)
            {
                if (CtrlItems[i].Selected)
                {
                    if (CtrlItems[i].ctrl is TabPage)
                    {
                        TabControl? tabctrl = CtrlItems[i].ctrl!.Parent as TabControl;

                        if (tabctrl!.TabPages.Count > 1)
                        {
                            Delete(CtrlItems[i]);
                            i--;
                        }
                    }
                    else
                    {
                        Delete(CtrlItems[i]);
                        i--;
                    }
                }
            }
        }
        private void Delete(cls_controls ctrl)
        {
            for (int i = 0; i < CtrlItems.Count; i++)
            {
                if (ctrl.ctrl == CtrlItems[i].ctrl!.Parent)
                {
                    Delete(CtrlItems[i]);
                    i--;
                }

                if (ctrl.ctrl is SplitContainer)
                {
                    SplitContainer? splcontainer = ctrl.ctrl as SplitContainer;

                    for (int j = 0; j < CtrlItems.Count; j++)
                    {
                        if (splcontainer!.Panel1 == CtrlItems[j].ctrl!.Parent || splcontainer!.Panel2 == CtrlItems[j].ctrl!.Parent)
                        {
                            Delete(CtrlItems[j]);
                            i--;
                        }
                    }
                }
            }
            ctrl.Delete();
            CtrlItems.Remove(ctrl);
        }
        internal void Add_Controls(List<CONTROL_INFO> ctrlInfo)
        {
            // コントロール全削除
            CtrlAllClear();

            // Control作成
            for (int i = 0; i < ctrlInfo.Count; i++)
            {
                _ = new cls_controls(this, this, ctrlInfo[i]);
            }
            this.Location = new Point(16, 16);

            // その他設定
            for (int i = 0; i < ctrlInfo.Count; i++)
            {
                for (int j = 0; j < CtrlItems.Count; j++)
                {
                    if (CtrlItems[j].ctrl != null)
                    {
                        if (CtrlItems[j].ctrl!.Name == ctrlInfo[i].ctrlName || ctrlInfo[i].ctrlName == "this")
                        {
                            CtrlItems[j].SetControls(ctrlInfo[i]);
                        }
                    }
                }
            }
            // selectbox設定
            for (int j = 0; j < CtrlItems.Count; j++)
            {
                CtrlItems[j].InitSelectBox();
            }
            SelectAllClear();
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
