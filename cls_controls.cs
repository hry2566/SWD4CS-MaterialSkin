using System.Reflection;
using MaterialSkin.Controls;

namespace SWD4CS
{
    internal class cls_controls
    {
        private cls_userform? form;
        internal Control? ctrl;
        internal string? className;
        //internal Object? obj;
        private cls_selectbox? selectBox;
        private bool selectFlag = false;
        private bool changeFlag;
        private Point memPos;
        private int grid = 4;
        internal List<string> decHandler = new();
        internal List<string> decFunc = new();

        public cls_controls(cls_userform form, string className, Control parent, int X, int Y)
        {
            this.form = form;

            if (Init(className))
            {
                this.className = className;
                ctrl!.Location = new System.Drawing.Point(X, Y);
                this.form.CtrlItems!.Add(this);

                parent.Controls.Add(this.ctrl);

                if (ctrl is TabControl || ctrl is MaterialTabControl)
                {
                    _ = new cls_controls(form, "TabPage", this.ctrl, X, Y);
                    _ = new cls_controls(form, "TabPage", this.ctrl, X, Y);
                }

                if (className == "MaterialTabControl" && parent.GetType() == typeof(cls_userform))
                {
                    MaterialForm? userform = parent as MaterialForm;
                    userform!.DrawerTabControl = this.ctrl as MaterialTabControl;
                }

                if (this.ctrl is TabPage)
                {
                    selectBox = new cls_selectbox(this, this.ctrl);
                    Selected = false;
                }
                else
                {
                    selectBox = new cls_selectbox(this, parent);
                    Selected = true;
                }


                ctrl!.Click += new System.EventHandler(Ctrl_Click);
                ctrl.MouseMove += new System.Windows.Forms.MouseEventHandler(ControlMouseMove);
                ctrl.MouseDown += new System.Windows.Forms.MouseEventHandler(ControlMouseDown);

            }



        }
        internal bool Selected
        {
            set
            {
                if (selectBox != null)
                {
                    selectFlag = value;
                    selectBox!.SetSelectBoxPos(value);
                }
                ShowProperty(value);
                // form!.mainForm!.eventView!.ShowEventList(value, this);
            }
            get
            {
                return selectFlag;
            }
        }
        private void ShowProperty(bool flag)
        {
            if (flag)
            {
                form!.mainForm!.propertyGrid!.SelectedObject = this.ctrl;
                form.mainForm.propertyCtrlName!.Text = this.ctrl!.Name;
            }
            else
            {
                form!.mainForm!.propertyGrid!.SelectedObject = null;
                form.mainForm!.propertyCtrlName!.Text = "";
            }
        }
        private void SetSelected(MouseEventArgs me)
        {
            if (me.Button == MouseButtons.Left)
            {
                if (Selected && changeFlag)
                {
                    Selected = false;
                }
                else
                {
                    if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
                    {
                        form!.SelectAllClear();
                    }
                    Selected = true;
                }
            }
        }
        private void Ctrl_Click(object? sender, EventArgs e)
        {
            if (e.ToString() != "System.EventArgs")
            {
                MouseEventArgs me = (MouseEventArgs)e;

                if (form!.mainForm!.toolLstBox!.Text == "")
                {
                    SetSelected(me);
                    foreach (TreeNode n in form.mainForm.ctrlTree!.Nodes)
                    {
                        // TreeNode ret = FindNode(n, this.ctrl!.Name);
                        // if (ret != null)
                        // {
                        //     form!.mainForm.ctrlTree.SelectedNode = ret;
                        //     break;
                        // }
                    }
                }
                else
                {
                    AddControls(me);
                }
            }
        }
        private void AddControls(MouseEventArgs me, SplitterPanel? splitpanel = null)
        {
            int X = (int)(me.X / grid) * grid;
            int Y = (int)(me.Y / grid) * grid;

            form!.SelectAllClear();

            if ((this.ctrl is TabControl && form!.mainForm!.toolLstBox!.Text == "TabPage") || (this.ctrl is TabControl == false && form!.mainForm!.toolLstBox!.Text != "TabPage"))
            {
                if (splitpanel == null)
                {
                    _ = new cls_controls(form, form!.mainForm!.toolLstBox!.Text, this.ctrl!, X, Y);
                }
                else
                {
                    _ = new cls_controls(form, form!.mainForm!.toolLstBox!.Text, splitpanel!, X, Y);
                }
            }
            form!.mainForm!.toolLstBox!.SelectedIndex = 0;
        }

        private void CreateTrancePanel(Control ctrl)
        {
            cls_transparent_panel trancepanel = new();
            trancepanel.Dock = DockStyle.Fill;
            trancepanel.BackColor = Color.FromArgb(0, 0, 0, 0);

            trancepanel.Click += new System.EventHandler(Ctrl_Click);
            trancepanel.MouseMove += new System.Windows.Forms.MouseEventHandler(ControlMouseMove);
            trancepanel.MouseDown += new System.Windows.Forms.MouseEventHandler(ControlMouseDown);
            ctrl.Controls.Add(trancepanel);

            trancepanel.BringToFront();
            trancepanel.Invalidate();
        }
        private void ControlMouseMove(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Selected)
            {
                Point pos = new((int)(e.X / grid) * grid, (int)(e.Y / grid) * grid);
                Point newPos = new(pos.X - memPos.X + ctrl!.Location.X, pos.Y - memPos.Y + ctrl.Location.Y);

                ctrl.Location = newPos;
                Selected = true;
                changeFlag = false;
            }
            else
            {
                changeFlag = true;
            }
        }
        private void ControlMouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Selected)
            {
                memPos.X = (int)(e.X / grid) * grid;
                memPos.Y = (int)(e.Y / grid) * grid;
            }
        }
        internal Control? GetBaseCtrl()
        {
            Type type = this.ctrl!.GetType();
            Control? baseCtrl = (Control)Activator.CreateInstance(type)!;
            return baseCtrl;
        }
        internal static string Property2String(Control ctrl, PropertyInfo item)
        {
            string strProperty = "";
            Type type = item.GetValue(ctrl)!.GetType();
            string str2 = item.GetValue(ctrl)!.ToString()!;

            // Console.WriteLine(item.Name);
            // Console.WriteLine(type);

            switch (type)
            {
                case Type t when t == typeof(System.Drawing.Point):
                    Point point = (Point)item.GetValue(ctrl)!;
                    strProperty = " = new " + type.ToString() + "(" + point.X + "," + point.Y + ");";
                    break;
                case Type t when t == typeof(System.Drawing.Size):
                    Size size = (Size)item.GetValue(ctrl)!;
                    strProperty = " = new " + type.ToString() + "(" + size.Width + "," + size.Height + ");";
                    break;
                case Type t when t == typeof(System.String):
                    strProperty = " =  " + "\"" + str2 + "\";";
                    break;
                case Type t when t == typeof(System.Boolean):
                    strProperty = " =  " + str2.ToLower() + ";";
                    break;
                case Type t when t == typeof(System.Windows.Forms.AnchorStyles):
                    strProperty = AnchorStyles2String(item.GetValue(ctrl));
                    break;
                case Type t when t == typeof(System.Int32):
                    strProperty = " = " + int.Parse(str2) + ";";
                    break;
                case Type t when t == typeof(System.Windows.Forms.DockStyle) ||
                                 t == typeof(System.Drawing.ContentAlignment) ||
                                 t == typeof(System.Windows.Forms.ScrollBars) ||
                                 t == typeof(System.Windows.Forms.HorizontalAlignment) ||
                                 t == typeof(System.Windows.Forms.FormWindowState) ||
                                 t == typeof(System.Windows.Forms.FixedPanel) ||
                                 t == typeof(System.Windows.Forms.PictureBoxSizeMode) ||
                                 t == typeof(System.Windows.Forms.View) ||
                                 t == typeof(System.Windows.Forms.Orientation) ||
                                 t == typeof(System.Windows.Forms.FormBorderStyle) ||
                                 t == typeof(System.Windows.Forms.AutoScaleMode) ||
                                 t == typeof(System.Windows.Forms.TableLayoutPanelCellBorderStyle) ||
                                 t == typeof(System.Windows.Forms.FormStartPosition):

                    strProperty = " = " + type.ToString() + "." + str2 + ";";
                    break;
                case Type t when t == typeof(System.Drawing.Color):
                    strProperty = " = " + Property2Color(str2) + ";";
                    break;
                case Type t when t == typeof(System.Drawing.Font):
                    strProperty = " = " + Property2Font(ctrl.Font) + ";";
                    break;
            }

            return strProperty;
        }

        private static string Property2Font(Font font)
        {
            string[] split = font.ToString().Split(",");
            string strSize = split[1].Replace("Size=", "").Trim() + "F, ";

            string strStyle = "";
            split = font.Style.ToString().Split(",");

            if (split.Length == 1)
            {
                strStyle = string.Format("System.Drawing.FontStyle.{0}, System.Drawing.GraphicsUnit.Point)", split[0]);
            }
            else if (split.Length == 2)
            {
                strStyle = string.Format("((System.Drawing.FontStyle)((System.Drawing.FontStyle.{0} | System.Drawing.FontStyle.{1}))), System.Drawing.GraphicsUnit.Point)", split[0].Trim(), split[1].Trim());
            }
            else if (split.Length == 3)
            {
                strStyle = string.Format("((System.Drawing.FontStyle)(((System.Drawing.FontStyle.{0} | System.Drawing.FontStyle.{1}) | System.Drawing.FontStyle.{2}))), System.Drawing.GraphicsUnit.Point)", split[0].Trim(), split[1].Trim(), split[2].Trim());
            }
            else if (split.Length == 4)
            {
                strStyle = string.Format("((System.Drawing.FontStyle)((((System.Drawing.FontStyle.{0} | System.Drawing.FontStyle.{1})  | System.Drawing.FontStyle.{2}) | System.Drawing.FontStyle.{3}))), System.Drawing.GraphicsUnit.Point)", split[0].Trim(), split[1].Trim(), split[2].Trim(), split[3].Trim());
            }

            string strProperty = "new System.Drawing.Font(\"" + font.Name + "\", " + strSize + strStyle;
            return strProperty;
        }

        private static string AnchorStyles2String(object? propertyinfo)
        {
            string strProperty;
            string[] split = propertyinfo!.ToString()!.Split(',');
            Type type = propertyinfo.GetType();
            string str2 = propertyinfo.ToString()!;

            if (split.Length == 1)
            {
                strProperty = " = " + type.ToString() + "." + str2 + ";";
            }
            else
            {
                string ancho = "";

                for (int j = 0; j < split.Length; j++)
                {
                    if (j == 0)
                    {
                        ancho = "(" + type.ToString() + "." + split[j].Trim();
                    }
                    else
                    {
                        ancho = "(" + ancho + " | " + type.ToString() + "." + split[j].Trim() + ")";
                    }
                }
                ancho = "((" + type.ToString() + ")" + ancho + "));";
                strProperty = " = " + ancho;
            }
            return strProperty;
        }
        private static string Property2Color(string color)
        {
            List<string> systemColorName = new()
            {
                "ActiveBorder",
                "ActiveCaption",
                "ActiveCaptionText",
                "AppWorkspace",
                "ButtonFace",
                "ButtonHighlight",
                "ButtonShadow",
                "Control",
                "ControlDark",
                "ControlDarkDark",
                "ControlLight",
                "ControlLightLight",
                "ControlText",
                "Desktop",
                "GradientActiveCaption",
                "GradientInactiveCaption",
                "GrayText",
                "Highlight",
                "HighlightText",
                "HotTrack",
                "InactiveBorder",
                "InactiveCaption",
                "InactiveCaptionText",
                "Info",
                "InfoText",
                "Menu",
                "MenuBar",
                "MenuHighlight",
                "MenuText",
                "ScrollBar",
                "Window",
                "WindowFrame",
                "WindowText",
            };

            List<string> colorName = new()
            {
                "Black",
                "DimGray",
                "Gray",
                "DarkGray",
                "Silver",
                "LightGray",
                "Gainsboro",
                "WhiteSmoke",
                "White",
                "RosyBrown",
                "IndianRed",
                "Brown",
                "Firebrick",
                "LightCoral",
                "Maroon",
                "DarkRed",
                "Red",
                "Snow",
                "MistyRose",
                "Salmon",
                "Tomato",
                "DarkSalmon",
                "Coral",
                "OrangeRed",
                "LightSalmon",
                "Sienna",
                "SeaShell",
                "Chocolate",
                "SaddleBrown",
                "SandyBrown",
                "PeachPuff",
                "Peru",
                "Linen",
                "Bisque",
                "DarkOrange",
                "BurlyWood",
                "Tan",
                "AntiqueWhite",
                "NavajoWhite",
                "BlanchedAlmond",
                "PapayaWhip",
                "Moccasin",
                "Orange",
                "Wheat",
                "OldLace",
                "FloralWhite",
                "DarkGoldenrod",
                "Goldenrod",
                "Cornsilk",
                "Gold",
                "Khaki",
                "LemonChiffon",
                "PaleGoldenrod",
                "DarkKhaki",
                "Beige",
                "LightGoldenrodYellow",
                "Olive",
                "Yellow",
                "LightYellow",
                "Ivory",
                "OliveDrab",
                "YellowGreen",
                "DarkOliveGreen",
                "GreenYellow",
                "Chartreuse",
                "LawnGreen",
                "DarkSeaGreen",
                "ForestGreen",
                "LimeGreen",
                "LightGreen",
                "PaleGreen",
                "DarkGreen",
                "Green",
                "Lime",
                "Honeydew",
                "SeaGreen",
                "MediumSeaGreen",
                "SpringGreen",
                "MintCream",
                "MediumSpringGreen",
                "MediumAquamarine",
                "Aquamarine",
                "Turquoise",
                "LightSeaGreen",
                "MediumTurquoise",
                "DarkSlateGray",
                "PaleTurquoise",
                "Teal",
                "DarkCyan",
                "Cyan",
                "Aqua",
                "LightCyan",
                "Azure",
                "DarkTurquoise",
                "CadetBlue",
                "PowderBlue",
                "LightBlue",
                "DeepSkyBlue",
                "SkyBlue",
                "LightSkyBlue",
                "SteelBlue",
                "AliceBlue",
                "DodgerBlue",
                "SlateGray",
                "LightSlateGray",
                "LightSteelBlue",
                "CornflowerBlue",
                "RoyalBlue",
                "MidnightBlue",
                "Lavender",
                "Navy",
                "DarkBlue",
                "MediumBlue",
                "Blue",
                "GhostWhite",
                "SlateBlue",
                "DarkSlateBlue",
                "MediumSlateBlue",
                "MediumPurple",
                "BlueViolet",
                "Indigo",
                "DarkOrchid",
                "DarkViolet",
                "MediumOrchid",
                "Thistle",
                "Plum",
                "Violet",
                "Purple",
                "DarkMagenta",
                "Fuchsia",
                "Magenta",
                "Orchid",
                "MediumVioletRed",
                "DeepPink",
                "HotPink",
                "LavenderBlush",
                "PaleVioletRed",
                "Crimson",
            };

            color = color.Replace("Color [", "").Replace("]", "");
            string? strSystemColor = "System.Drawing.SystemColors.";
            string? strColor = "System.Drawing.Color.";
            string? strRGB = "System.Drawing.Color.FromArgb(";

            if (color == "Transparent")
            {
                return "Color.Transparent";
            }

            for (int i = 0; i < systemColorName.Count; i++)
            {
                if (systemColorName[i] == color)
                {
                    return strSystemColor + color;
                }
            }

            for (int i = 0; i < colorName.Count; i++)
            {
                if (colorName[i] == color)
                {
                    return strColor + color;
                }
            }

            color = color.Replace("A", "").Replace("R", "").Replace("G", "").Replace("B", "").Replace("=", "");
            string[] split = color.Split(",");
            strRGB += split[1] + "," + split[2] + "," + split[3] + ")";
            return strRGB;
        }

        internal static bool HideProperty(string itemName)
        {
            List<string> propertyName = new()
            {
                "AccessibilityObject",
                "BindingContext",
                "Parent",
                "TopLevelControl",
                "DataSource",
                "FirstDisplayedCell",
                "Item",
                "TopItem",
                "Rtf",
                "ParentForm",
                "SelectedTab",
                "Top",
                "Left",
                "Right",
                "Bottom",
                "Width",
                "Height",
                "CanSelect",
                "Created",
                "IsHandleCreated",
                "PreferredSize",
                "Visible",
                "Enable",
                "ClientSize",
                "UseVisualStyleBackColor",
                "PreferredHeight",
                "ColumnCount",
                "FirstDisplayedScrollingColumnIndex",
                "FirstDisplayedScrollingRowIndex",
                "NewRowIndex",
                "RowCount",
                "HasChildren",
                "PreferredWidth",
                "SingleMonthSize",
                "TextLength",
                "SelectedIndex",
                "TabCount",
                "VisibleCount",
                "DesktopLocation",
                "AutoScale",
                "CanFocus",
                "IsMirrored",
                "SelectionStart",
                "ContextMenuDefaultLocation",
                "CanUndo",
                "CompanyName",
                "ProductName",
                "ProductVersion",
                "TopLevel",
                "",
                "",
            };

            for (int i = 0; i < propertyName.Count; i++)
            {
                if (propertyName[i] == itemName)
                {
                    return false;
                }
            }
            return true;
        }

        // ****************************************************************************************
        // コントロール追加時に下記を編集すること
        // ****************************************************************************************
        private bool Init(string className)
        {
            switch (className)
            {
                // case "Button":
                //     this.ctrl = new Button();
                //     this.ctrl.Size = new System.Drawing.Size(96, 32);
                //     this.ctrl!.Name = className + form!.cnt_Button;
                //     form.cnt_Button++;
                //     break;
                // case "Label":
                //     this.ctrl = new Label();
                //     this.ctrl!.Name = className + form!.cnt_Label;
                //     this.ctrl!.AutoSize = true;
                //     form.cnt_Label++;
                //     break;
                // case "GroupBox":
                //     this.ctrl = new GroupBox();
                //     this.ctrl.Size = new System.Drawing.Size(250, 125);
                //     this.ctrl!.Name = className + form!.cnt_GroupBox;
                //     form.cnt_GroupBox++;
                //     break;
                // case "TextBox":
                //     this.ctrl = new TextBox();
                //     this.ctrl!.Name = className + form!.cnt_TextBox;
                //     form.cnt_TextBox++;
                //     break;
                // case "ListBox":
                //     this.ctrl = new ListBox();
                //     this.ctrl.Size = new System.Drawing.Size(120, 104);
                //     this.ctrl!.Name = className + form!.cnt_ListBox;
                //     ListBox? listbox = this.ctrl as ListBox;
                //     listbox!.Items.Add("ListBox");
                //     form.cnt_ListBox++;
                //     break;
                // case "TabControl":
                //     this.ctrl = new TabControl();
                //     this.ctrl.Size = new System.Drawing.Size(250, 125);
                //     this.ctrl!.Name = className + form!.cnt_TabControl;
                //     form.cnt_TabControl++;
                //     break;
                case "TabPage":
                    this.ctrl = new TabPage();
                    this.ctrl.Size = new System.Drawing.Size(250, 125);
                    break;
                // case "CheckBox":
                //     this.ctrl = new CheckBox();
                //     this.ctrl!.Name = className + form!.cnt_CheckBox;
                //     this.ctrl!.AutoSize = true;
                //     form.cnt_CheckBox++;
                //     break;
                // case "ComboBox":
                //     this.ctrl = new ComboBox();
                //     this.ctrl!.Name = className + form!.cnt_ComboBox;
                //     form.cnt_ComboBox++;
                //     break;
                // case "SplitContainer":
                //     this.ctrl = new SplitContainer();
                //     this.ctrl.Size = new System.Drawing.Size(120, 32);
                //     this.ctrl!.Name = className + form!.cnt_SplitContainer;
                //     this.ctrl.Size = new System.Drawing.Size(250, 125);
                //     SplitContainer? splitcontainer = this.ctrl as SplitContainer;
                //     splitcontainer!.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                //     splitcontainer.Panel1.Name = this.ctrl.Name + ".Panel1";
                //     splitcontainer.Panel1.Click += new System.EventHandler(this.SplitContainerPanelClick);
                //     splitcontainer.Panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(ControlMouseMove);
                //     splitcontainer.Panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(ControlMouseDown);
                //     splitcontainer.Panel2.Name = this.ctrl.Name + ".Panel2";
                //     splitcontainer.Panel2.Click += new System.EventHandler(this.SplitContainerPanelClick);
                //     splitcontainer.Panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(ControlMouseMove);
                //     splitcontainer.Panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(ControlMouseDown);
                //     form.cnt_SplitContainer++;
                //     break;
                // case "DataGridView":
                //     this.ctrl = new DataGridView();
                //     this.ctrl.Size = new System.Drawing.Size(304, 192);
                //     this.ctrl!.Name = className + form!.cnt_DataGridView;
                //     form.cnt_DataGridView++;
                //     break;
                // case "Panel":
                //     this.ctrl = new Panel();
                //     this.ctrl.Size = new System.Drawing.Size(304, 192);
                //     this.ctrl!.Name = className + form!.cnt_Panel;
                //     Panel? panel = this.ctrl as Panel;
                //     panel!.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                //     form.cnt_Panel++;
                //     break;
                // case "CheckedListBox":
                //     this.ctrl = new CheckedListBox();
                //     this.ctrl.Size = new System.Drawing.Size(152, 112);
                //     this.ctrl!.Name = className + form!.cnt_CheckedListBox;
                //     CheckedListBox? checkedlistbox = this.ctrl as CheckedListBox;
                //     checkedlistbox!.Items.Add("CheckedListBox");
                //     form.cnt_CheckedListBox++;
                //     break;
                // case "LinkLabel":
                //     this.ctrl = new LinkLabel();
                //     this.ctrl.Size = new System.Drawing.Size(120, 32);
                //     this.ctrl!.Name = className + form!.cnt_LinkLabel;
                //     this.ctrl!.AutoSize = true;
                //     form.cnt_LinkLabel++;
                //     break;
                // case "PictureBox":
                //     this.ctrl = new PictureBox();
                //     this.ctrl.Size = new System.Drawing.Size(125, 62);
                //     this.ctrl!.Name = className + form!.cnt_PictureBox;
                //     PictureBox? picbox = this.ctrl as PictureBox;
                //     picbox!.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                //     form.cnt_PictureBox++;
                //     break;
                // case "ProgressBar":
                //     this.ctrl = new ProgressBar();
                //     this.ctrl.Size = new System.Drawing.Size(125, 29);
                //     this.ctrl!.Name = className + form!.cnt_ProgressBar;
                //     ProgressBar? prgressbar = this.ctrl as ProgressBar;
                //     prgressbar!.Value = 50;
                //     form.cnt_ProgressBar++;
                //     break;
                // case "RadioButton":
                //     this.ctrl = new RadioButton();
                //     this.ctrl.Size = new System.Drawing.Size(125, 29);
                //     this.ctrl!.Name = className + form!.cnt_RadioButton;
                //     this.ctrl!.AutoSize = true;
                //     form.cnt_RadioButton++;
                //     break;
                // case "RichTextBox":
                //     this.ctrl = new RichTextBox();
                //     this.ctrl.Size = new System.Drawing.Size(125, 120);
                //     this.ctrl!.Name = className + form!.cnt_RichTextBox;
                //     form.cnt_RichTextBox++;
                //     break;
                // case "StatusStrip":
                //     this.ctrl = new StatusStrip();
                //     this.ctrl.Size = new System.Drawing.Size(125, 120);
                //     this.ctrl!.Name = className + form!.cnt_StatusStrip;
                //     form.cnt_StatusStrip++;
                //     break;
                // case "HScrollBar":
                //     this.ctrl = new HScrollBar();
                //     this.ctrl.Size = new System.Drawing.Size(120, 32);
                //     this.ctrl!.Name = className + form!.cnt_HScrollBar;
                //     CreateTrancePanel(this.ctrl);
                //     form.cnt_HScrollBar++;
                //     break;
                // case "VScrollBar":
                //     this.ctrl = new VScrollBar();
                //     this.ctrl.Size = new System.Drawing.Size(32, 120);
                //     this.ctrl!.Name = className + form!.cnt_VScrollBar;
                //     CreateTrancePanel(this.ctrl);
                //     form.cnt_VScrollBar++;
                //     break;
                // case "MonthCalendar":
                //     this.ctrl = new MonthCalendar();
                //     this.ctrl.Size = new System.Drawing.Size(151, 121);
                //     this.ctrl!.Name = className + form!.cnt_MonthCalendar;
                //     CreateTrancePanel(this.ctrl);
                //     form.cnt_MonthCalendar++;
                //     break;
                // case "ListView":
                //     this.ctrl = new ListView();
                //     this.ctrl.Size = new System.Drawing.Size(151, 121);
                //     this.ctrl!.Name = className + form!.cnt_ListView;
                //     CreateTrancePanel(this.ctrl);
                //     form.cnt_ListView++;
                //     break;
                // case "TreeView":
                //     this.ctrl = new TreeView();
                //     this.ctrl.Size = new System.Drawing.Size(151, 121);
                //     this.ctrl!.Name = className + form!.cnt_TreeView;
                //     CreateTrancePanel(this.ctrl);
                //     form.cnt_TreeView++;
                //     break;
                // case "MaskedTextBox":
                //     this.ctrl = new MaskedTextBox();
                //     this.ctrl.Size = new System.Drawing.Size(151, 121);
                //     this.ctrl!.Name = className + form!.cnt_MaskedTextBox;
                //     form.cnt_MaskedTextBox++;
                //     break;
                // case "PropertyGrid":
                //     this.ctrl = new PropertyGrid();
                //     this.ctrl.Size = new System.Drawing.Size(151, 121);
                //     this.ctrl!.Name = className + form!.cnt_PropertyGrid;
                //     CreateTrancePanel(this.ctrl);
                //     form.cnt_PropertyGrid++;
                //     break;
                // case "DateTimePicker":
                //     this.ctrl = new DateTimePicker();
                //     this.ctrl.Size = new System.Drawing.Size(151, 121);
                //     this.ctrl!.Name = className + form!.cnt_DateTimePicker;
                //     CreatePickBox(this.ctrl);
                //     form.cnt_DateTimePicker++;
                //     break;
                // case "DomainUpDown":
                //     this.ctrl = new DomainUpDown();
                //     this.ctrl.Size = new System.Drawing.Size(151, 121);
                //     this.ctrl!.Name = className + form!.cnt_DomainUpDown;
                //     form.cnt_DomainUpDown++;
                //     break;
                // case "FlowLayoutPanel":
                //     this.ctrl = new FlowLayoutPanel();
                //     this.ctrl.Size = new System.Drawing.Size(151, 121);
                //     this.ctrl!.Name = className + form!.cnt_FlowLayoutPanel;
                //     FlowLayoutPanel? flwlayoutpnl = this.ctrl as FlowLayoutPanel;
                //     flwlayoutpnl!.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                //     form.cnt_FlowLayoutPanel++;
                //     break;
                // case "Splitter":
                //     this.ctrl = new Splitter();
                //     this.ctrl.Size = new System.Drawing.Size(151, 121);
                //     this.ctrl!.Name = className + form!.cnt_Splitter;
                //     Splitter? splitter = this.ctrl as Splitter;
                //     splitter!.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                //     form.cnt_Splitter++;
                //     break;
                // case "TableLayoutPanel":
                //     this.ctrl = new TableLayoutPanel();
                //     this.ctrl.Size = new System.Drawing.Size(151, 121);
                //     this.ctrl!.Name = className + form!.cnt_TblLayPnl;
                //     TableLayoutPanel? tbllaypnl = this.ctrl as TableLayoutPanel;
                //     tbllaypnl!.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
                //     form.cnt_TblLayPnl++;
                //     break;
                // case "TrackBar":
                //     this.ctrl = new TrackBar();
                //     this.ctrl.Size = new System.Drawing.Size(151, 121);
                //     this.ctrl!.Name = className + form!.cnt_TblLayPnl;
                //     form.cnt_TrackBar++;
                //     break;
                case "MaterialButton":
                    this.ctrl = new MaterialButton();
                    break;
                case "MaterialCard":
                    this.ctrl = new MaterialCard();
                    break;
                case "MaterialCheckbox":
                    this.ctrl = new MaterialCheckbox();
                    break;
                case "MaterialCheckedListBox":
                    this.ctrl = new MaterialCheckedListBox();
                    break;
                case "MaterialComboBox":
                    this.ctrl = new MaterialComboBox();
                    break;
                case "MaterialDivider":
                    this.ctrl = new MaterialDivider();
                    break;
                case "MaterialDrawer":
                    this.ctrl = new MaterialDrawer();
                    CreateTrancePanel(this.ctrl);
                    break;
                case "MaterialExpansionPanel":
                    this.ctrl = new MaterialExpansionPanel();
                    break;
                case "MaterialFloatingActionButton":
                    this.ctrl = new MaterialFloatingActionButton();
                    break;
                case "MaterialLabel":
                    this.ctrl = new MaterialLabel();
                    break;
                case "MaterialListBox":
                    this.ctrl = new MaterialListBox();
                    break;
                case "MaterialListView":
                    this.ctrl = new MaterialListView();
                    CreateTrancePanel(this.ctrl);
                    break;
                case "MaterialMaskedTextBox":
                    this.ctrl = new MaterialMaskedTextBox();
                    break;
                case "MaterialMultiLineTextBox":
                    this.ctrl = new MaterialMultiLineTextBox();
                    break;
                case "MaterialMultiLineTextBox2":
                    this.ctrl = new MaterialMultiLineTextBox2();
                    CreateTrancePanel(this.ctrl);
                    break;
                case "MaterialProgressBar":
                    this.ctrl = new MaterialProgressBar();
                    break;
                case "MaterialRadioButton":
                    this.ctrl = new MaterialRadioButton();
                    break;
                case "MaterialScrollBar":
                    this.ctrl = new MaterialScrollBar();
                    break;
                case "MaterialSlider":
                    this.ctrl = new MaterialSlider();
                    break;
                case "MaterialSwitch":
                    this.ctrl = new MaterialSwitch();
                    break;
                case "MaterialTabControl":
                    // this.ctrl = new MaterialTabControl();
                    this.ctrl = new TabControl();
                    this.ctrl.Size = new System.Drawing.Size(151, 121);
                    break;
                case "MaterialTabSelector":
                    this.ctrl = new MaterialTabSelector();
                    break;
                case "MaterialTextBox":
                    this.ctrl = new MaterialTextBox();
                    break;
                case "MaterialTextBox2":
                    this.ctrl = new MaterialTextBox2();
                    break;
                // case "ImageList":
                //     this.ctrl = new ImageList();
                //     break;
                default:
                    return false;
            }

            form!.cnt_Control++;
            this.ctrl!.Name = className + form!.cnt_Control;
            this.ctrl!.TabIndex = form.cnt_Control;

            if (className != "DateTimePicker" && className != "WebBrowser")
            {
                this.ctrl!.Text = this.ctrl!.Name;
            }
            return true;
        }
        internal static void AddToolList(ListBox ctrlLstBox)
        {
            ctrlLstBox.Items.Add("");
            // ctrlLstBox.Items.Add("Button");
            // ctrlLstBox.Items.Add("Label");
            // ctrlLstBox.Items.Add("GroupBox");
            // ctrlLstBox.Items.Add("TextBox");
            // ctrlLstBox.Items.Add("ListBox");
            // ctrlLstBox.Items.Add("TabControl");
            // ctrlLstBox.Items.Add("TabPage");
            // ctrlLstBox.Items.Add("CheckBox");
            // ctrlLstBox.Items.Add("ComboBox");
            // ctrlLstBox.Items.Add("SplitContainer");
            // ctrlLstBox.Items.Add("DataGridView");
            // ctrlLstBox.Items.Add("Panel");
            // ctrlLstBox.Items.Add("CheckedListBox");
            // ctrlLstBox.Items.Add("LinkLabel");
            // ctrlLstBox.Items.Add("PictureBox");
            // ctrlLstBox.Items.Add("ProgressBar");
            // ctrlLstBox.Items.Add("RadioButton");
            // ctrlLstBox.Items.Add("RichTextBox");
            // ctrlLstBox.Items.Add("StatusStrip");
            // ctrlLstBox.Items.Add("HScrollBar");
            // ctrlLstBox.Items.Add("VScrollBar");
            // ctrlLstBox.Items.Add("MonthCalendar");
            // ctrlLstBox.Items.Add("ListView");
            // ctrlLstBox.Items.Add("TreeView");
            // ctrlLstBox.Items.Add("MaskedTextBox");
            // ctrlLstBox.Items.Add("PropertyGrid");
            // ctrlLstBox.Items.Add("DateTimePicker");
            // ctrlLstBox.Items.Add("DomainUpDown");
            // ctrlLstBox.Items.Add("FlowLayoutPanel");
            // ctrlLstBox.Items.Add("Splitter");
            // ctrlLstBox.Items.Add("TableLayoutPanel");
            // ctrlLstBox.Items.Add("TrackBar");
            ctrlLstBox.Items.Add("MaterialButton");
            ctrlLstBox.Items.Add("MaterialCard");
            ctrlLstBox.Items.Add("MaterialCheckbox");
            ctrlLstBox.Items.Add("MaterialCheckedListBox");
            ctrlLstBox.Items.Add("MaterialComboBox");
            ctrlLstBox.Items.Add("MaterialDivider");
            ctrlLstBox.Items.Add("MaterialDrawer");
            ctrlLstBox.Items.Add("MaterialExpansionPanel");
            ctrlLstBox.Items.Add("MaterialFloatingActionButton");
            ctrlLstBox.Items.Add("MaterialLabel");
            ctrlLstBox.Items.Add("MaterialListBox");
            ctrlLstBox.Items.Add("MaterialListView");
            ctrlLstBox.Items.Add("MaterialMaskedTextBox");
            ctrlLstBox.Items.Add("MaterialMultiLineTextBox");
            ctrlLstBox.Items.Add("MaterialMultiLineTextBox2");
            ctrlLstBox.Items.Add("MaterialProgressBar");
            ctrlLstBox.Items.Add("MaterialRadioButton");
            ctrlLstBox.Items.Add("MaterialScrollBar");
            ctrlLstBox.Items.Add("MaterialSlider");
            ctrlLstBox.Items.Add("MaterialSwitch");
            ctrlLstBox.Items.Add("MaterialTabControl");
            ctrlLstBox.Items.Add("MaterialTabSelector");
            ctrlLstBox.Items.Add("MaterialTextBox");
            ctrlLstBox.Items.Add("MaterialTextBox2");
            // ctrlLstBox.Items.Add("ImageList");

            // MaterialButton;
            // MaterialCard;
            // MaterialCheckbox;
            // MaterialCheckedListBox;
            // MaterialComboBox;
            // MaterialContextMenuStrip;
            // MaterialDialog;
            // MaterialDivider;
            // MaterialDrawer;
            // MaterialExpansionPanel;
            // MaterialFloatingActionButton;
            // MaterialForm;
            // MaterialItemCollectionEditor;
            // MaterialLabel;
            // MaterialListBox;
            // MaterialMaskedTextBox;
            // MaterialMessageBox;
            // MaterialMultiLineTextBox;
            // MaterialMultiLineTextBox2;
            // MaterialProgressBar;
            // MaterialRadioButton;
            // MaterialScrollBar;
            // MaterialScrollOrientation;
            // MaterialSkin;
            // MaterialSlider;
            // MaterialSnackBar;
            // MaterialSwitch;
            // MaterialTabControl;
            // MaterialTabSelector;
            // MaterialTextBox;
            // MaterialTextBox2;
            // MaterialToolStripMenuItem;

        }
    }
}