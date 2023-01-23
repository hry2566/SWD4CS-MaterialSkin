namespace SWD4CS;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.mainWndSplitContainer = new System.Windows.Forms.SplitContainer();
        this.ctrlsTab = new System.Windows.Forms.TabControl();
        this.tabPage1 = new System.Windows.Forms.TabPage();
        this.ctrlLstBox = new System.Windows.Forms.ListBox();
        this.tabPage2 = new System.Windows.Forms.TabPage();
        this.ctrlTreeView = new System.Windows.Forms.TreeView();
        this.subWndSplitContainer = new System.Windows.Forms.SplitContainer();
        this.designeTab = new System.Windows.Forms.TabControl();
        this.designePage = new System.Windows.Forms.TabPage();
        this.sourcePage = new System.Windows.Forms.TabPage();
        this.sourceTxtBox = new System.Windows.Forms.TextBox();
        this.eventsPage = new System.Windows.Forms.TabPage();
        this.eventTxtBox = new System.Windows.Forms.TextBox();
        this.logPage = new System.Windows.Forms.TabPage();
        this.logTxtBox = new System.Windows.Forms.TextBox();
        this.tabControl1 = new System.Windows.Forms.TabControl();
        this.tabPage3 = new System.Windows.Forms.TabPage();
        this.nameTxtBox = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.propertyBox = new System.Windows.Forms.PropertyGrid();
        this.tabPage4 = new System.Windows.Forms.TabPage();
        this.statusStrip1 = new System.Windows.Forms.StatusStrip();
        ((System.ComponentModel.ISupportInitialize)(this.mainWndSplitContainer)).BeginInit();
        this.mainWndSplitContainer.Panel1.SuspendLayout();
        this.mainWndSplitContainer.Panel2.SuspendLayout();
        this.mainWndSplitContainer.SuspendLayout();
        this.ctrlsTab.SuspendLayout();
        this.tabPage1.SuspendLayout();
        this.tabPage2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.subWndSplitContainer)).BeginInit();
        this.subWndSplitContainer.Panel1.SuspendLayout();
        this.subWndSplitContainer.Panel2.SuspendLayout();
        this.subWndSplitContainer.SuspendLayout();
        this.designeTab.SuspendLayout();
        this.designePage.SuspendLayout();
        this.sourcePage.SuspendLayout();
        this.eventsPage.SuspendLayout();
        this.logPage.SuspendLayout();
        this.tabControl1.SuspendLayout();
        this.tabPage3.SuspendLayout();
        this.tabPage4.SuspendLayout();
        this.statusStrip1.SuspendLayout();
        this.SuspendLayout();
        //
        // mainWndSplitContainer
        //
        this.mainWndSplitContainer.Panel1.Controls.Add(this.ctrlsTab);
        this.mainWndSplitContainer.Panel2.Controls.Add(this.subWndSplitContainer);
        this.mainWndSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
        this.mainWndSplitContainer.Text =  "SplitContainer0";
        this.mainWndSplitContainer.BackColor = System.Drawing.Color.WhiteSmoke;
        this.mainWndSplitContainer.Location = new System.Drawing.Point(0,31);
        this.mainWndSplitContainer.Name =  "mainWndSplitContainer";
        this.mainWndSplitContainer.Size = new System.Drawing.Size(1003,608);
        this.mainWndSplitContainer.SplitterDistance = 199;
        this.mainWndSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        //
        // ctrlsTab
        //
        this.ctrlsTab.Controls.Add(this.tabPage1);
        this.ctrlsTab.Controls.Add(this.tabPage2);
        this.ctrlsTab.ItemSize = new System.Drawing.Size(59,20);
        this.ctrlsTab.Text =  "TabControl0";
        this.ctrlsTab.Dock = System.Windows.Forms.DockStyle.Fill;
        this.ctrlsTab.Name =  "ctrlsTab";
        this.ctrlsTab.Size = new System.Drawing.Size(197,606);
        this.ctrlsTab.TabIndex = 1;
        //
        // tabPage1
        //
        this.tabPage1.Controls.Add(this.ctrlLstBox);
        this.tabPage1.BackColor = Color.Transparent;
        this.tabPage1.Location = new System.Drawing.Point(4,24);
        this.tabPage1.TabIndex = 2;
        this.tabPage1.Text =  "ToolsBox";
        this.tabPage1.Name =  "tabPage1";
        this.tabPage1.Size = new System.Drawing.Size(189,578);
        //
        // ctrlLstBox
        //
        this.ctrlLstBox.ItemHeight = 15;
        this.ctrlLstBox.Sorted =  true;
        this.ctrlLstBox.Text =  "ListBox0";
        this.ctrlLstBox.FormattingEnabled =  true;
        this.ctrlLstBox.Dock = System.Windows.Forms.DockStyle.Fill;
        this.ctrlLstBox.Name =  "ctrlLstBox";
        this.ctrlLstBox.Size = new System.Drawing.Size(189,574);
        this.ctrlLstBox.TabIndex = 3;
        //
        // tabPage2
        //
        this.tabPage2.Controls.Add(this.ctrlTreeView);
        this.tabPage2.BackColor = Color.Transparent;
        this.tabPage2.Location = new System.Drawing.Point(4,24);
        this.tabPage2.TabIndex = 1;
        this.tabPage2.Text =  "TreeView";
        this.tabPage2.Name =  "tabPage2";
        this.tabPage2.Size = new System.Drawing.Size(189,578);
        //
        // ctrlTreeView
        //
        this.ctrlTreeView.Text =  "TreeView0";
        this.ctrlTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
        this.ctrlTreeView.Name =  "ctrlTreeView";
        this.ctrlTreeView.Size = new System.Drawing.Size(189,578);
        this.ctrlTreeView.TabIndex = 5;
        //
        // subWndSplitContainer
        //
        this.subWndSplitContainer.Panel1.Controls.Add(this.designeTab);
        this.subWndSplitContainer.Panel2.Controls.Add(this.tabControl1);
        this.subWndSplitContainer.Panel2.Controls.Add(this.nameTxtBox);
        this.subWndSplitContainer.Panel2.Controls.Add(this.label1);
        this.subWndSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
        this.subWndSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
        this.subWndSplitContainer.Text =  "SplitContainer1";
        this.subWndSplitContainer.BackColor = System.Drawing.Color.WhiteSmoke;
        this.subWndSplitContainer.Name =  "subWndSplitContainer";
        this.subWndSplitContainer.Size = new System.Drawing.Size(800,608);
        this.subWndSplitContainer.TabIndex = 6;
        this.subWndSplitContainer.SplitterDistance = 525;
        //
        // designeTab
        //
        this.designeTab.Controls.Add(this.designePage);
        this.designeTab.Controls.Add(this.sourcePage);
        this.designeTab.Controls.Add(this.eventsPage);
        this.designeTab.Controls.Add(this.logPage);
        this.designeTab.ItemSize = new System.Drawing.Size(54,20);
        this.designeTab.Text =  "TabControl1";
        this.designeTab.Dock = System.Windows.Forms.DockStyle.Fill;
        this.designeTab.Name =  "designeTab";
        this.designeTab.Size = new System.Drawing.Size(523,606);
        this.designeTab.TabIndex = 1;
        //
        // designePage
        //
        this.designePage.BackColor = Color.Transparent;
        this.designePage.Location = new System.Drawing.Point(4,24);
        this.designePage.TabIndex = 8;
        this.designePage.Text =  "Design";
        this.designePage.AutoScroll =  true;
        this.designePage.Name =  "designePage";
        this.designePage.Size = new System.Drawing.Size(515,578);
        //
        // sourcePage
        //
        this.sourcePage.Controls.Add(this.sourceTxtBox);
        this.sourcePage.BackColor = Color.Transparent;
        this.sourcePage.Location = new System.Drawing.Point(4,24);
        this.sourcePage.TabIndex = 1;
        this.sourcePage.Text =  "Source";
        this.sourcePage.Name =  "sourcePage";
        this.sourcePage.Size = new System.Drawing.Size(515,578);
        //
        // sourceTxtBox
        //
        this.sourceTxtBox.Multiline =  true;
        this.sourceTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        this.sourceTxtBox.Text =  "TextBox0";
        this.sourceTxtBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
        this.sourceTxtBox.ReadOnly =  true;
        this.sourceTxtBox.WordWrap =  false;
        this.sourceTxtBox.Dock = System.Windows.Forms.DockStyle.Fill;
        this.sourceTxtBox.Name =  "sourceTxtBox";
        this.sourceTxtBox.Size = new System.Drawing.Size(515,578);
        this.sourceTxtBox.TabIndex = 10;
        //
        // eventsPage
        //
        this.eventsPage.Controls.Add(this.eventTxtBox);
        this.eventsPage.BackColor = Color.Transparent;
        this.eventsPage.Location = new System.Drawing.Point(4,24);
        this.eventsPage.TabIndex = 2;
        this.eventsPage.Text =  "Events";
        this.eventsPage.Name =  "eventsPage";
        this.eventsPage.Size = new System.Drawing.Size(515,578);
        //
        // eventTxtBox
        //
        this.eventTxtBox.Multiline =  true;
        this.eventTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        this.eventTxtBox.Text =  "TextBox1";
        this.eventTxtBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
        this.eventTxtBox.ReadOnly =  true;
        this.eventTxtBox.WordWrap =  false;
        this.eventTxtBox.Dock = System.Windows.Forms.DockStyle.Fill;
        this.eventTxtBox.Name =  "eventTxtBox";
        this.eventTxtBox.Size = new System.Drawing.Size(515,578);
        this.eventTxtBox.TabIndex = 12;
        //
        // logPage
        //
        this.logPage.Controls.Add(this.logTxtBox);
        this.logPage.BackColor = Color.Transparent;
        this.logPage.Location = new System.Drawing.Point(4,24);
        this.logPage.TabIndex = 3;
        this.logPage.Text =  "log";
        this.logPage.Name =  "logPage";
        this.logPage.Size = new System.Drawing.Size(515,578);
        //
        // logTxtBox
        //
        this.logTxtBox.Multiline =  true;
        this.logTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        this.logTxtBox.Text =  "TextBox2";
        this.logTxtBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
        this.logTxtBox.ReadOnly =  true;
        this.logTxtBox.WordWrap =  false;
        this.logTxtBox.Dock = System.Windows.Forms.DockStyle.Fill;
        this.logTxtBox.Name =  "logTxtBox";
        this.logTxtBox.Size = new System.Drawing.Size(515,578);
        this.logTxtBox.TabIndex = 14;
        //
        // tabControl1
        //
        this.tabControl1.Controls.Add(this.tabPage3);
        this.tabControl1.Controls.Add(this.tabPage4);
        this.tabControl1.ItemSize = new System.Drawing.Size(57,20);
        this.tabControl1.Text =  "TabControl2";
        this.tabControl1.Name =  "tabControl1";
        this.tabControl1.Size = new System.Drawing.Size(269,576);
        this.tabControl1.TabIndex = 15;
        this.tabControl1.Location = new System.Drawing.Point(4,32);
        this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        //
        // tabPage3
        //
        this.tabPage3.Controls.Add(this.propertyBox);
        this.tabPage3.BackColor = Color.Transparent;
        this.tabPage3.Location = new System.Drawing.Point(4,24);
        this.tabPage3.TabIndex = 16;
        this.tabPage3.Text =  "Property";
        this.tabPage3.Name =  "tabPage3";
        this.tabPage3.Size = new System.Drawing.Size(261,548);
        //
        // nameTxtBox
        //
        this.nameTxtBox.Text =  "TextBox3";
        this.nameTxtBox.Location = new System.Drawing.Point(58,6);
        this.nameTxtBox.Name =  "nameTxtBox";
        this.nameTxtBox.Size = new System.Drawing.Size(206,23);
        this.nameTxtBox.TabIndex = 2;
        this.nameTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        //
        // label1
        //
        this.label1.AutoSize =  true;
        this.label1.Text =  "Name";
        this.label1.BackColor = Color.Transparent;
        this.label1.Location = new System.Drawing.Point(8,9);
        this.label1.Name =  "label1";
        this.label1.Size = new System.Drawing.Size(38,15);
        this.label1.TabIndex = 1;
        //
        // propertyBox
        //
        this.propertyBox.Text =  "PropertyGrid0";
        this.propertyBox.Dock = System.Windows.Forms.DockStyle.Fill;
        this.propertyBox.Name =  "propertyBox";
        this.propertyBox.Size = new System.Drawing.Size(261,548);
        this.propertyBox.TabIndex = 19;
        //
        // tabPage4
        //
        this.tabPage4.Location = new System.Drawing.Point(4,24);
        this.tabPage4.TabIndex = 20;
        this.tabPage4.Text =  "Event";
        this.tabPage4.Name =  "tabPage4";
        this.tabPage4.Size = new System.Drawing.Size(261,548);
        //
        // statusStrip1
        //
        this.statusStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
        this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20,20);
        this.statusStrip1.Location = new System.Drawing.Point(0,642);
        this.statusStrip1.Name =  "statusStrip1";
        this.statusStrip1.Size = new System.Drawing.Size(1003,22);
        this.statusStrip1.TabIndex = 1;
        this.statusStrip1.Text =  "statusStrip1";
        //
        // form
        //
        this.BackColor = System.Drawing.Color.WhiteSmoke;
        this.KeyPreview =  true;
        this.Size = new System.Drawing.Size(1019,703);
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text =  "SWD4CS";
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.mainWndSplitContainer);
        this.Controls.Add(this.statusStrip1);
        ((System.ComponentModel.ISupportInitialize)(this.mainWndSplitContainer)).EndInit();
        this.mainWndSplitContainer.Panel1.ResumeLayout(false);
        this.mainWndSplitContainer.Panel2.ResumeLayout(false);
        this.mainWndSplitContainer.ResumeLayout(false);
        this.ctrlsTab.ResumeLayout(false);
        this.tabPage1.ResumeLayout(false);
        this.tabPage2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.subWndSplitContainer)).EndInit();
        this.subWndSplitContainer.Panel1.ResumeLayout(false);
        this.subWndSplitContainer.Panel2.ResumeLayout(false);
        this.subWndSplitContainer.ResumeLayout(false);
        this.designeTab.ResumeLayout(false);
        this.designePage.ResumeLayout(false);
        this.sourcePage.ResumeLayout(false);
        this.eventsPage.ResumeLayout(false);
        this.logPage.ResumeLayout(false);
        this.tabControl1.ResumeLayout(false);
        this.tabPage3.ResumeLayout(false);
        this.tabPage4.ResumeLayout(false);
        this.statusStrip1.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    #endregion

    private SplitContainer mainWndSplitContainer;
    private TabControl ctrlsTab;
    private TabPage tabPage1;
    private ListBox ctrlLstBox;
    private TabPage tabPage2;
    private TreeView ctrlTreeView;
    private SplitContainer subWndSplitContainer;
    private TabControl designeTab;
    private TabPage designePage;
    private TabPage sourcePage;
    private TextBox sourceTxtBox;
    private TabPage eventsPage;
    private TextBox eventTxtBox;
    private TabPage logPage;
    private TextBox logTxtBox;
    private TabControl tabControl1;
    private TabPage tabPage3;
    private TextBox nameTxtBox;
    private Label label1;
    private PropertyGrid propertyBox;
    private TabPage tabPage4;
    private StatusStrip statusStrip1;
}

