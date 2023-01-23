namespace SWD4CS;

public partial class MainForm : Form
{
    cls_userform? userForm;
    internal PropertyGrid? propertyGrid;
    internal TextBox? propertyCtrlName;
    internal ListBox? toolLstBox;
    internal TreeView? ctrlTree;
    // internal cls_user_datagridview? eventView;

    public MainForm()
    {
        InitializeComponent();
        Private2Internal_Controls();
        Init_OtherControls();

        cls_controls.AddToolList(ctrlLstBox);

    }

    private void Init_OtherControls()
    {
        userForm = new SWD4CS.cls_userform();
        // 
        // userForm
        // 
        userForm.Location = new System.Drawing.Point(16, 16);
        userForm.Size = new System.Drawing.Size(480, 400);
        userForm.TabIndex = 0;
        userForm!.Init(this, designePage);

        this.designePage.Controls.Add(this.userForm);


    }
    private void Private2Internal_Controls()
    {
        propertyGrid = propertyBox;
        propertyCtrlName = nameTxtBox;
        toolLstBox = ctrlLstBox;
        ctrlTree = ctrlTreeView;
        // eventView = evtGridView;
    }


}
