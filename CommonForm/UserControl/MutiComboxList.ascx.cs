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
using Innolux.Portal.CommonFunction;

public partial class MutiComboxList : System.Web.UI.UserControl
{
    public delegate void RefreshUIHandler();
    public event RefreshUIHandler RefreshUI;
    public ArrayList array_Select = new ArrayList();
    private CommonUI m_objUI = new CommonUI();
    private string _strConnectionString = "";
    private string _strConnectionChar = ",";


    public string ConnectionChar
    {
        get { return (_strConnectionChar); }
        set { _strConnectionChar = value; }
       
    }

    public string Get_SelectValue(string Connection)
    {
         
         string strSelect = "";
         foreach (ListItem item in this.CheckList_Select.Items)
         {

             if (item.Selected == true)
             {
                 strSelect = strSelect + item.Value + Connection;
                 array_Select.Add(item.Value);
             }

         }
         if (strSelect.Length > 0)
         {
             strSelect = strSelect.Substring(0, strSelect.Length - Connection.Length);
         }


         return (strSelect);
    }

    public void Set_DataSource(DataTable dataSource)
    {
        m_objUI.ListDataToCheckBox(dataSource, 0, CheckList_Select);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void cmdOK_Click(object sender, EventArgs e)
    {
        GetSelectString();
        if (RefreshUI != null)
        {
            RefreshUI();
        }
    }

    protected void cmdSelectAll(object sender, EventArgs e)
    {
        for (int intI = 0; intI < CheckList_Select.Items.Count; intI++)
        {
            CheckList_Select.Items[intI].Selected = true;
        }
        GetSelectString();
        if (RefreshUI != null)
        {
            RefreshUI();
        }
    }

    private void GetSelectString()
    {
        string strSelect = "";
        foreach (ListItem item in this.CheckList_Select.Items)
        {

            if (item.Selected == true)
            {
                strSelect = strSelect + item.Value + _strConnectionChar;
                array_Select.Add(item.Value);
            }
            
        }
        if (strSelect.Length > 0)
        {
            strSelect = strSelect.Substring(0, strSelect.Length - _strConnectionChar.Length);
        }
        _strConnectionString = strSelect;
        PopupControlExtender1.Commit(strSelect);
    }



    
}
