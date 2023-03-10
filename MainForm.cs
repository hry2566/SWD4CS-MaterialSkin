using System.Reflection;
using MaterialSkin.Controls;

namespace SWD4CS;

public partial class MainForm : Form
{
    cls_userform? userForm;
    private FILE_INFO fileInfo;
    internal PropertyGrid? propertyGrid;
    internal TextBox? propertyCtrlName;
    internal ListBox? toolLstBox;
    // internal TreeView? ctrlTree;

    private MenuStrip? menuStrip1;
    private ToolStripMenuItem? fileToolStripMenuItem;
    private ToolStripMenuItem? openToolStripMenuItem;
    private ToolStripMenuItem? saveToolStripMenuItem;
    private ToolStripMenuItem? closeToolStripMenuItem;
    private ToolStripMenuItem? editToolStripMenuItem;
    private ToolStripMenuItem? deleteToolStripMenuItem;
    // internal cls_user_datagridview? eventView;
    private string sourceFileName = "";

    public MainForm()
    {
        InitializeComponent();
        Private2Internal_Controls();
        Init_OtherControls();
        cls_controls.AddToolList(ctrlLstBox);
        Run_CommandLine();
    }

    // ********************************************************************************************
    // private Function 
    // ********************************************************************************************
    private void Private2Internal_Controls()
    {
        propertyGrid = propertyBox;
        propertyCtrlName = nameTxtBox;
        toolLstBox = ctrlLstBox;
        // ctrlTree = ctrlTreeView;
        // eventView = evtGridView;
    }
    private void Init_OtherControls()
    {
        this.userForm = new SWD4CS.cls_userform();
        this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        // 
        // userForm
        // 
        this.userForm.Location = new System.Drawing.Point(16, 16);
        this.userForm.Size = new System.Drawing.Size(480, 400);
        this.userForm.TabIndex = 0;
        this.userForm!.Init(this, designePage);
        // 
        // menuStrip1
        // 
        this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
        this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
        this.menuStrip1.Location = new System.Drawing.Point(0, 0);
        this.menuStrip1.Name = "menuStrip1";
        this.menuStrip1.Size = new System.Drawing.Size(1003, 28);
        this.menuStrip1.TabIndex = 2;
        this.menuStrip1.Text = "menuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.closeToolStripMenuItem});
        this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
        this.fileToolStripMenuItem.Text = "File";
        // 
        // openToolStripMenuItem
        // 
        this.openToolStripMenuItem.Name = "openToolStripMenuItem";
        this.openToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
        this.openToolStripMenuItem.Text = "Open";
        this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
        // 
        // saveToolStripMenuItem
        // 
        this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        this.saveToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
        this.saveToolStripMenuItem.Text = "Save";
        this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
        // 
        // closeToolStripMenuItem
        // 
        this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
        this.closeToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
        this.closeToolStripMenuItem.Text = "Close";
        this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
        // 
        // editToolStripMenuItem
        // 
        this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
        this.editToolStripMenuItem.Name = "editToolStripMenuItem";
        this.editToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
        this.editToolStripMenuItem.Text = "Edit";
        // 
        // deleteToolStripMenuItem
        // 
        this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
        this.deleteToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
        this.deleteToolStripMenuItem.Text = "Delete (Alt + Del)";
        this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);

        this.designePage.Controls.Add(this.userForm);
        this.Controls.Add(this.menuStrip1);
    }
    private void Run_CommandLine()
    {
        string[] cmds = System.Environment.GetCommandLineArgs();
        if (cmds.Length < 2) { return; }
        fileInfo = cls_file.CommandLine(cmds[1]);
        sourceFileName = fileInfo.source_FileName;
        logTxtBox.Text = "";
        userForm!.Add_Controls(fileInfo.ctrlInfo);
    }
    private string Create_EventCode()
    {
        List<string> decHandler = new();
        List<string> decFunc = new();
        Add_Declaration(ref decHandler, ref decFunc);

        if (decHandler.Count == 0) { return ""; }

        if (fileInfo.source_base == null)
        {
            fileInfo.source_base = cls_file.NewFile();
        }

        string[] split = Create_SourceCode().Split(Environment.NewLine);
        string eventSource = Create_EventsSource(split, decHandler, decFunc);
        return eventSource;
    }
    private void Add_Declaration(ref List<string> decHandler, ref List<string> decFunc)
    {
        for (int i = 0; i < userForm!.decHandler.Count; i++)
        {
            decHandler.Add(userForm.decHandler[i]);
            decFunc.Add(userForm.decFunc[i]);
        }

        for (int j = 0; j < userForm.CtrlItems.Count; j++)
        {
            for (int i = 0; i < userForm.CtrlItems[j].decHandler.Count; i++)
            {
                decHandler.Add(userForm.CtrlItems[j].decHandler[i]);
                decFunc.Add(userForm.CtrlItems[j].decFunc[i]);
            }
        }
    }
    private static string Create_EventsSource(string[] split, List<string> decHandler, List<string> decFunc)
    {
        string eventSource = "";

        for (int i = 0; i < 4; i++)
        {
            if (eventSource == "")
            {
                eventSource = split[i];
            }
            else
            {
                eventSource += Environment.NewLine + split[i];
            }
        }

        eventSource += Environment.NewLine;
        eventSource += "    private void InitializeEvents()" + Environment.NewLine;
        eventSource += "    {" + Environment.NewLine;

        for (int i = 0; i < decHandler.Count; i++)
        {
            eventSource += "        " + decHandler[i] + Environment.NewLine;
        }

        eventSource += "    }" + Environment.NewLine + Environment.NewLine;

        for (int i = 0; i < decFunc.Count; i++)
        {
            eventSource += "    " + decFunc[i] + Environment.NewLine;
            eventSource += "    {" + Environment.NewLine + Environment.NewLine;
            eventSource += "    }" + Environment.NewLine + Environment.NewLine;
        }
        eventSource += "}" + Environment.NewLine;
        return eventSource;
    }
    private string Create_SourceCode()
    {
        string source = "";
        List<string> lstSuspend = new();
        List<string> lstResume = new();
        string space = "";

        if (fileInfo.source_base[0].IndexOf(";") == -1)
        {
            space = space.PadLeft(8);
        }
        else
        {
            space = space.PadLeft(4);
        }

        source = Create_Code_Instance(source, space);
        source = Create_Code_Suspend_Resume(source, lstSuspend, lstResume, space);

        // suspend
        for (int i = 0; i < lstSuspend.Count; i++)
        {
            source += lstSuspend[i];
        }

        source = Create_Code_Property(source, space);
        source = Create_Code_FormProperty(source, space);
        source = Create_Code_FormAddControl(source, space);

        // resume
        for (int i = 0; i < lstResume.Count; i++)
        {
            source += lstResume[i];
        }

        source = Create_Code_ControlDeclaration(source, space);

        if (fileInfo.source_base[0].IndexOf(";") == -1)
        {
            source += "    }" + Environment.NewLine;
        }
        source += "}" + Environment.NewLine;
        source += Environment.NewLine;

        // events function
        source = Create_Code_FuncDeclaration(source);

        if (source.IndexOf("using MaterialSkin.Controls;") == -1)
        {
            string usingDec = "using MaterialSkin.Controls;" + Environment.NewLine + Environment.NewLine;
            source = usingDec + source;
        }
        return source;
    }
    private string Create_Code_Instance(string source, string space)
    {
        // Instance
        for (int i = 0; i < fileInfo.source_base.Count; i++)
        {
            source += fileInfo.source_base[i] + Environment.NewLine;
        }
        source += space + "{" + Environment.NewLine;
        return source;
    }
    private string Create_Code_Suspend_Resume(string source, List<string> lstSuspend, List<string> lstResume, string space)
    {
        // Suspend & resume
        for (int i = 0; i < userForm!.CtrlItems.Count; i++)
        {
            if (userForm.CtrlItems[i].className!.IndexOf("Material") > -1)
            {
                source += space + "    this." + userForm.CtrlItems[i].ctrl!.Name + " = new MaterialSkin.Controls." + userForm.CtrlItems[i].className + "();" + Environment.NewLine;
            }
            else
            {
                source += space + "    this." + userForm.CtrlItems[i].ctrl!.Name + " = new System.Windows.Forms." + userForm.CtrlItems[i].className + "();" + Environment.NewLine;
            }

            List<string> className_group1 = new()
            {
                "DataGridView",
                "PictureBox",
                "SplitContainer"
            };
            for (int j = 0; j < className_group1.Count; j++)
            {
                if (userForm.CtrlItems[i].className == className_group1[j])
                {
                    lstSuspend.Add(space + "    ((System.ComponentModel.ISupportInitialize)(this." + userForm.CtrlItems[i].ctrl!.Name + ")).BeginInit();" + Environment.NewLine);
                    lstResume.Add(space + "    ((System.ComponentModel.ISupportInitialize)(this." + userForm.CtrlItems[i].ctrl!.Name + ")).EndInit();" + Environment.NewLine);
                }
            }

            List<string> className_group2 = new()
            {
                "GroupBox",
                "Panel",
                "StatusStrip",
                "TabControl",
                "TabPage"
            };
            for (int j = 0; j < className_group2.Count; j++)
            {
                if (userForm.CtrlItems[i].className == className_group2[j])
                {
                    lstSuspend.Add(space + "    this." + userForm.CtrlItems[i].ctrl!.Name + ".SuspendLayout();" + Environment.NewLine);
                    lstResume.Add(space + "    this." + userForm.CtrlItems[i].ctrl!.Name + ".ResumeLayout(false);" + Environment.NewLine);
                }
            }

            if (userForm.CtrlItems[i].className == "SplitContainer")
            {
                lstSuspend.Add(space + "    this." + userForm.CtrlItems[i].ctrl!.Name + ".Panel1.SuspendLayout();" + Environment.NewLine);
                lstSuspend.Add(space + "    this." + userForm.CtrlItems[i].ctrl!.Name + ".Panel2.SuspendLayout();" + Environment.NewLine);
                lstSuspend.Add(space + "    this." + userForm.CtrlItems[i].ctrl!.Name + ".SuspendLayout();" + Environment.NewLine);
                lstResume.Add(space + "    this." + userForm.CtrlItems[i].ctrl!.Name + ".Panel1.ResumeLayout(false);" + Environment.NewLine);
                lstResume.Add(space + "    this." + userForm.CtrlItems[i].ctrl!.Name + ".Panel2.ResumeLayout(false);" + Environment.NewLine);
                lstResume.Add(space + "    this." + userForm.CtrlItems[i].ctrl!.Name + ".ResumeLayout(false);" + Environment.NewLine);
            }
        }
        lstSuspend.Add(space + "    this.SuspendLayout();" + Environment.NewLine);
        lstResume.Add(space + "    this.ResumeLayout(false);" + Environment.NewLine);
        return source;
    }
    private string Create_Code_FuncDeclaration(string source)
    {
        // control
        for (int i = 0; i < userForm!.CtrlItems.Count; i++)
        {
            for (int j = 0; j < userForm.CtrlItems[i].decFunc.Count; j++)
            {
                source += "//" + userForm.CtrlItems[i].decFunc[j] + Environment.NewLine;
                source += "//{" + Environment.NewLine;
                source += "//" + Environment.NewLine;
                source += "//}" + Environment.NewLine;
                source += Environment.NewLine;
            }
        }

        // form
        for (int i = 0; i < userForm.decFunc.Count; i++)
        {
            source += "//" + userForm.decFunc[i] + Environment.NewLine;
            source += "//{" + Environment.NewLine;
            source += "//" + Environment.NewLine;
            source += "//}" + Environment.NewLine;
            source += Environment.NewLine;
        }
        return source;
    }
    private string Create_Code_ControlDeclaration(string source, string space)
    {
        source += space + "}" + Environment.NewLine;
        source += Environment.NewLine;
        source += space + "#endregion" + Environment.NewLine;
        source += Environment.NewLine;

        // declaration
        for (int i = 0; i < userForm!.CtrlItems.Count; i++)
        {
            string[] split = userForm.CtrlItems[i].ctrl!.GetType().ToString().Split(".");
            string dec;
            if (userForm.CtrlItems[i].className == "MaterialTabControl")
            {
                dec = "MaterialTabControl";
            }
            else
            {
                dec = split[split.Length - 1];
            }
            source += space + "private " + dec + " " + userForm.CtrlItems[i].ctrl!.Name + ";" + Environment.NewLine;
        }
        return source;
    }
    private string Create_Code_FormAddControl(string source, string space)
    {
        // AddControl
        for (int i = 0; i < userForm!.CtrlItems.Count; i++)
        {
            if (userForm.CtrlItems[i].ctrl!.Parent == userForm)
            {
                source += space + "    this.Controls.Add(this." + userForm.CtrlItems[i].ctrl!.Name + ");" + Environment.NewLine;
            }
        }
        return source;
    }
    private string Create_Code_FormProperty(string source, string space)
    {
        // form-property
        source += space + "    //" + Environment.NewLine;
        source += space + "    // form" + Environment.NewLine;
        source += space + "    //" + Environment.NewLine;

        foreach (PropertyInfo item in userForm!.GetType().GetProperties())
        {
            if (cls_controls.HideProperty(item.Name))
            {
                Control? baseForm = new MaterialForm();

                if (item.GetValue(userForm) != null && item.GetValue(baseForm) != null &&
                    item.GetValue(userForm)!.ToString() != item.GetValue(baseForm)!.ToString())
                {
                    string str1 = space + "    this." + item.Name;
                    string strProperty = cls_controls.Property2String(userForm, item);

                    if (strProperty != "" && item.Name != "Name" && item.Name != "Location")
                    {
                        source += str1 + strProperty + Environment.NewLine;
                    }
                }
            }
        }
        source = Create_Code_FormEventsDec(source, space, userForm);
        return source;
    }
    private string Create_Code_Property(string source, string space)
    {
        // Property
        for (int i = 0; i < userForm!.CtrlItems.Count; i++)
        {
            string memCode = "";
            source += space + "    //" + Environment.NewLine;
            source += space + "    // " + userForm.CtrlItems[i].ctrl!.Name + Environment.NewLine;
            source += space + "    //" + Environment.NewLine;

            source = Create_Code_AddControl(source, space, i);

            // Property
            foreach (PropertyInfo item in userForm.CtrlItems[i].ctrl!.GetType().GetProperties())
            {
                if (cls_controls.HideProperty(item.Name))
                {
                    Get_Code_Property(ref source, ref memCode, item, userForm.CtrlItems[i], space);
                }
            }
            if (memCode != "")
            {
                source += memCode;
            }

            source = Create_Code_EventsDec(source, space, userForm.CtrlItems[i]);
        }
        return source;
    }
    private static void Get_Code_Property(ref string source, ref string memCode, PropertyInfo item, cls_controls ctrlItems, string space)
    {
        Control? baseCtrl = ctrlItems.GetBaseCtrl();
        if (item.GetValue(ctrlItems.ctrl) != null && item.GetValue(baseCtrl) != null)
        {
            if (item.GetValue(ctrlItems.ctrl)!.ToString() != item.GetValue(baseCtrl)!.ToString())
            {
                string str1 = space + "    this." + ctrlItems.ctrl!.Name + "." + item.Name;
                string strProperty = cls_controls.Property2String(ctrlItems.ctrl!, item);
                if (strProperty != "")
                {
                    if (item.Name != "SplitterDistance" && item.Name != "Anchor")
                    {
                        source += str1 + strProperty + Environment.NewLine;
                    }
                    else
                    {
                        memCode += str1 + strProperty + Environment.NewLine;
                    }
                }
            }
        }
    }
    private static string Create_Code_EventsDec(string source, string space, cls_controls cls_ctrl)
    {
        for (int i = 0; i < cls_ctrl.decHandler.Count; i++)
        {
            source += space + "    " + cls_ctrl.decHandler[i] + Environment.NewLine;
        }
        return source;
    }
    private static string Create_Code_FormEventsDec(string source, string space, cls_userform userForm)
    {
        for (int i = 0; i < userForm.decHandler.Count; i++)
        {
            source += space + "    " + userForm.decHandler[i] + Environment.NewLine;
        }
        return source;
    }
    private string Create_Code_AddControl(string source, string space, int i)
    {
        // AddControl
        for (int j = 0; j < userForm!.CtrlItems.Count; j++)
        {
            if (userForm.CtrlItems[i].ctrl!.Name == userForm.CtrlItems[j].ctrl!.Parent!.Name)
            {
                source += space + "    this." + userForm.CtrlItems[i].ctrl!.Name + ".Controls.Add(this." + userForm.CtrlItems[j].ctrl!.Name + ");" + Environment.NewLine;
            }
            else if (userForm.CtrlItems[i].ctrl!.Name == userForm.CtrlItems[j].ctrl!.Parent!.Parent!.Name)
            {
                if (userForm.CtrlItems[j].ctrl!.Parent!.Name.IndexOf("Panel1") > -1)
                {
                    source += space + "    this." + userForm.CtrlItems[i].ctrl!.Name + ".Panel1.Controls.Add(this." + userForm.CtrlItems[j].ctrl!.Name + ");" + Environment.NewLine;
                }
                else if (userForm.CtrlItems[j].ctrl!.Parent!.Name.IndexOf("Panel2") > -1)
                {
                    source += space + "    this." + userForm.CtrlItems[i].ctrl!.Name + ".Panel2.Controls.Add(this." + userForm.CtrlItems[j].ctrl!.Name + ");" + Environment.NewLine;
                }
            }
        }
        return source;
    }
    // ********************************************************************************************
    // Event Function 
    // ********************************************************************************************
    private void deleteToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        userForm!.RemoveSelectedItem();
    }
    private void closeToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        Close();
    }
    private void saveToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        designeTab.SelectedIndex = 0;
        designeTab.SelectedIndex = 1;

        if (sourceFileName != "")
        {
            cls_file.SaveAs(sourceFileName, sourceTxtBox.Text);
        }
        else
        {
            cls_file.Save(sourceTxtBox.Text);
        }
        Close();
    }
    private void openToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        fileInfo = cls_file.OpenFile();
        sourceFileName = fileInfo.source_FileName;
        logTxtBox.Text = "";
        userForm!.Add_Controls(fileInfo.ctrlInfo);
    }
    private void designeTab_SelectedIndexChanged(System.Object? sender, System.EventArgs e)
    {
        switch (designeTab.SelectedIndex)
        {
            case 1:
                if (fileInfo.source_base == null)
                {
                    fileInfo.source_base = cls_file.NewFile();
                }
                sourceTxtBox.Text = Create_SourceCode();
                break;
            case 2:
                eventTxtBox.Text = Create_EventCode();
                break;
        }
    }
    private void nameTxtBox_TextChanged(System.Object? sender, System.EventArgs e)
    {
        if (propertyGrid!.SelectedObject != null && propertyGrid.SelectedObject is Form == false)
        {
            Control? ctrl = propertyGrid.SelectedObject as Control;
            ctrl!.Name = propertyCtrlName!.Text;
        }
    }
}
