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
using Innolux.Portal.EntLibBlock.DataAccess;
using Telerik.Web.UI;

public partial class Muti_SelectWin : System.Web.UI.Page
{
    DbAccessHelper Innodb = new DbAccessHelper("Innoview DB");
    DbAccessHelper Arydb = new DbAccessHelper("ARY_DEV DB");
    DbAccessHelper Celdb = new DbAccessHelper("CEL_DEV DB");
    DbAccessHelper Cfdb = new DbAccessHelper("CFT_DEV DB");
    private string m_strListDataSource = "";
    private string m_cboDataSource = "";
    private string m_strSelectData = "";
    private PopupListBox_Provider m_popData = new PopupListBox_Provider();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetParametersByTableMode();
        }
    }

    private bool GetParametersByTableMode()
    {
        string strSource_Paramemter="";
        string strSelect_Parameter = "";
        strSource_Paramemter = m_popData.GetParameter("Source");
        strSelect_Parameter = m_popData.GetParameter("Selected");

        if (strSource_Paramemter.Length>0)
        {
            string[] listSoruce = strSource_Paramemter.Split(',');
            AddDataToList(listSoruce,listBoxL);
            AddDataToCombox(listSoruce, RadComboBox1);
        }

        if (strSelect_Parameter.Length >0)
        { 
            string[] SelectSoruce = strSelect_Parameter.Split(',');
            AddDataToList(SelectSoruce, this.listBoxR);
        }
        return (true);
       
    }

    private void AddDataToList(string[] data, ListBox List)
    {
        List.Items.Clear();
        for (int intI = 0; intI < data.Length; intI++)
        {
            string strItem = data[intI].ToString();
            if (!string.IsNullOrEmpty(strItem))
            {
                List.Items.Add(strItem);
            }
        }
    }
    private void AddDataToCombox(string[] data,RadComboBox List)
    {
        List.Items.Clear();
        for (int intI = 0; intI < data.Length; intI++)
        {
            string strItem = data[intI].ToString();
            if (!string.IsNullOrEmpty(strItem))
            {
                RadComboBoxItem item=new RadComboBoxItem(strItem);
                List.Items.Add(item);
            }
        }
    }


   


    protected void Button_Delete_Click(object sender, EventArgs e)
    {
    }
    protected void Button_ok_Click(object sender, EventArgs e)
    {
         string script = "";
         script = "<script type='text/javascript'>OK_Clicked("+ listBoxR.Items.Count+")</" + "script>";
         ScriptManager.RegisterStartupScript(this, this.GetType(),"", script, false);

    }

    protected void Button_cancel_Click(object sender, EventArgs e)
    {
        string script = "";
        script = "<script type='text/javascript'>Cancel_Clicked()</" + "script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, false);
    }
    protected void Button_Select_Click(object sender, EventArgs e)
    {
        ListToList(listBoxL, listBoxR, false);
    }

    protected void Button_UnSelect_Click(object sender, EventArgs e)
    {
        ListToList(listBoxR, listBoxL , true);

    }


    protected void Button_RadSelect_Click(object sender, EventArgs e)
    {
        string sListItem = "";
        int ExistFlag = 0;
        sListItem = RadComboBox1.SelectedItem.Text;
        ListItem item = new ListItem(sListItem);
        if (listBoxR.Items.FindByText(sListItem) == null)
        {
            listBoxR.Items.Add(item);
        }
    }

    protected void Button_SelectAll_Click(object sender, EventArgs e)
    {
        listBoxR.Items.Clear();

        for (int i = 0; i < listBoxL.Items.Count; i++)
        {
            listBoxR.Items.Add(listBoxL.Items[i].Value);
        }
    }

    protected void Button_UnSelectAll_Click(object sender, EventArgs e)
    {
        listBoxR.Items.Clear();
    }

    private void ListToList(ListBox listFrom,ListBox listTo, bool needRemovefromSource)
    {
        int intLength=listTo.Items.Count;
        for (int intI = listFrom.Items.Count - 1; intI >= 0; intI--)
        {
            if (listFrom.Items[intI].Selected == true)
            {
                string item = "";
                string strOlditem = "";
                int intEqualPosition = 0;
                item = listFrom.Items[intI].Text;
                if(intLength<0)
                    intLength=0;
                if (listTo.Items.FindByText(item) == null && needRemovefromSource == false)
                {

                    listTo.Items.Insert(intLength, item);
                }
                if (needRemovefromSource)
                {
                    listFrom.Items.Remove(item);
                }
            }
        }
    }
}
