using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

public partial class CommonForm_UserControl_PopupListBox : System.Web.UI.UserControl
{
    public delegate void PopListBoxClick();
    public event PopListBoxClick PopListSelect;
    

    private DataTable  m_strSource_DataList = new DataTable();
    private string m_strTitle = "";
    private string m_strWidth = "150px";
    private ArrayList m_ArraySource = new ArrayList();
    private PopupListBox_Provider m_PopData = new PopupListBox_Provider();


    #region Public properties 
    public DataTable Source_TableSource
    {
        get { return m_strSource_DataList; }
        set 
        { 
            m_strSource_DataList = value;
            ViewState["POP_DataSource"] = m_strSource_DataList;
        }
    }

    public ArrayList Source_listSource
    {
        get { return (m_ArraySource); }
        set 
        { 
            m_ArraySource = value;
            TransferToTable(m_ArraySource);
        }
    }



    public string Title
    {
        get { return m_strTitle; }
        set
        {
            m_strTitle = value;
            ViewState["POP_Title"] = m_strTitle;
        }
    }
    public string  Width
    {
        get { return m_strWidth; }
        set { m_strWidth = value; }
    }

    public string defaultValue = "";
    public string DefaultValue
    {
        get { return this.defaultValue; }
        set 
        {
            this.defaultValue = value;
            this.HiddenField1.Value = this.txtSelect.Text = this.defaultValue;
        }
    }


    public void SetSelectedValue(string selectedValue)
    {
        this.txtSelect.Text = selectedValue;
    }
    public string GetSelectedValue()
    {
        return string.Format("{0}", this.HiddenField1.Value);
        //return (txtSelect.Text.Trim());
    }
    public string ClearPopupListBox()
    {
        txtSelect.Text = "";
        return (txtSelect.Text.Trim());
    }
    // Todo: add
    public bool IsFieldValidator
    {
        get
        {
            if (ViewState["IsFieldValidator"] != null)
                return bool.Parse(ViewState["IsFieldValidator"].ToString());
            return false;
        }
        set { ViewState["IsFieldValidator"] = value; }
    }

    public string ValidationGroup
    {
        get
        {
            if (ViewState["ValidationGroup"] == null)
                return string.Empty;
            return ViewState["ValidationGroup"].ToString();
        }
        set { ViewState["ValidationGroup"] = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //Todo:
        if (Page.IsPostBack)
        {
            string script = "return changeTooltips_" + this.txtSelect.ClientID + "();";
            this.txtSelect.Attributes.Add("onmouseover", script);
            this.ImageButton1.Attributes.Add("onmouseover", script);
            SetTxtBoxWidth();
        }
        else
        {
            this.RequiredFieldValidator1.Enabled = this.IsFieldValidator;
            this.RequiredFieldValidator1.ValidationGroup = this.ValidationGroup;
        }
    }

    #region Private function
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (PopListSelect != null)
            PopListSelect();

        if (m_strTitle.Length == 0)
            m_strTitle = "New";
        if (ViewState["POP_Title"] != null)
            m_strTitle = (String)ViewState["POP_Title"];
        RadWindow objWindow = new RadWindow();

        objWindow.ID = string.Format("Win_{0}", this.txtSelect.ClientID);
        objWindow.Title = m_strTitle;
        objWindow.VisibleOnPageLoad = true;
        objWindow.Visible = true;
        objWindow.Width = 370;
        objWindow.Height = 320;
        objWindow.VisibleStatusbar = false;
        objWindow.Skin = "Office2007";
        objWindow.DestroyOnClose = true;
        objWindow.ReloadOnShow = true;

        objWindow.OffsetElementID = this.Page.ClientID;// this.txtSelect.ClientID;
        objWindow.ClientCallBackFunction = string.Format("CallBackFunction2_{0}", this.txtSelect.ClientID);
        objWindow.NavigateUrl = "../../CommonForm/WebForm/Muti_SelectWin.aspx";
         
        SaveQueryParameters();
        this.RadWindowManager1.Windows.Add(objWindow);
        this.txtSelect.ToolTip = this.txtSelect.Text;
    }

    private void  SaveQueryParameters()
    {
        m_PopData.Type = "Source";
        m_PopData.Parameter = GetSelectedSource();
        m_PopData.SaveQueryParameter();
        m_PopData.Type = "Selected";
        m_PopData.Parameter = GetSelectedValues();
        m_PopData.SaveQueryParameter();
    }

    private string GetSelectedSource()
    {
        string strDataList = "";
        DataTable table = (DataTable)ViewState["POP_DataSource"];
        if (table != null && table.Rows.Count > 0)
        {
            for (int intI = 0; intI < table.Rows.Count; intI++)
            {
                strDataList += table.Rows[intI][0].ToString() + ",";
            }
            if (strDataList.Length > 0)
            {
                strDataList = strDataList.Substring(0, strDataList.Length - 1);
            }
        }
        //if (strDataList.Length > 4000)
        //    strDataList = strDataList.Substring(0, 4000);
        return (strDataList);
    }

    private string GetSelectedValues()
    {
        string strSelectedItem = txtSelect.Text.ToString();
        return (strSelectedItem);
    }

    private void SetTxtBoxWidth()
    {
        int intWidth=150;
        if (m_strWidth.IndexOf("px") >0)
            m_strWidth =m_strWidth.Replace("px","");
        intWidth=(int)Convert.ToInt32(m_strWidth);
        this.txtSelect.Width = Unit.Pixel(intWidth);
        
    }

    private void TransferToTable(ArrayList listFrom)
    {
        DataTable table = new DataTable();
        table.Columns.Add("Item", typeof(string));
        for (Int32 intRow = 0; intRow < listFrom.Count; intRow++)
        {
            DataRow row = table.NewRow();
            row["Item"] = listFrom[intRow].ToString();
            table.Rows.Add(row);
        }
        this.Source_TableSource = table;
    }
    #endregion
}
