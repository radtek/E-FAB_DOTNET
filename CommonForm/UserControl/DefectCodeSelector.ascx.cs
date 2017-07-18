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

public partial class CommonForm_UserControl_DefectCodeSelector : System.Web.UI.UserControl
{
    #region Public members
    public DefShopName ShopName
    {
        get
        {
            object obj2 = this.ViewState["ShopName"];
            return (obj2 != null) ? (DefShopName)obj2 : DefShopName.CFT;
        }
        set
        {
            this.ViewState["ShopName"] = value;
        }
    }

    private string DBTableName
    {
        get
        {
            switch (this.ShopName)
            { 
                case DefShopName.CFT:
                    return "CFT_DEFECT_CODE";
                case DefShopName.ARRAY:
                    return "ARY_DEFECT_CODE";
                default:
                    return "CEL_DEFECT_CODE";
            }
        }
    }

    public string SelectedDefectCodes
    {
        get
        {
            return this.TextBox1.Text;
        }
    }

    public string[] SelectedDefectCategory
    {
        get
        {
            System.Collections.Generic.List<string> result = new System.Collections.Generic.List<string>();
            foreach (ListItem item in lsbChosenDefCode.Items)
            {
                string category = item.Value.Split(':')[0];
                if (!string.IsNullOrEmpty(category) && !result.Contains(category))
                {
                    result.Add(category);
                }
            }
            return result.ToArray();
        }
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
        if (!this.IsPostBack)
        {
            DbAccessHelper database = new DbAccessHelper("InnoluxDB");

            this.lsbDefGroup.DataSource =
                database.ExecuteDataSet(string.Format("SELECT DISTINCT DEFECT_CAT FROM {0} WHERE DEFECT_CAT IS NOT NULL", this.DBTableName));
            this.lsbDefGroup.DataTextField = "DEFECT_CAT";
            this.lsbDefGroup.DataValueField = "DEFECT_CAT";
            this.lsbDefGroup.DataBind();

            this.RequiredFieldValidator1.ValidationGroup = this.ValidationGroup;

            this.btnFilter.Enabled = this.lsbDefGroup.Items.Count > 0;
        }
    }

    protected void OkButton_Click(object sender, EventArgs e)
    {
        string temp = "";
        foreach (ListItem item in this.lsbChosenDefCode.Items)
        {
            temp += ("'" + item.Text + "',");
        }
        if (!string.IsNullOrEmpty(temp))
        {
            temp = temp.Substring(0, temp.Length - 1);
        }
        this.TextBox1.Text = temp;
        this.TextBox1.ToolTip = temp;
		this.ModalPopupExtender.Hide();
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        DbAccessHelper database = new DbAccessHelper("InnoluxDB");

        this.lsbAvaliableDefCode.DataSource = database.ExecuteDataSet(string.Format("SELECT DEFECT_CODE, DEFECT_CAT || ':' || DEFECT_CODE AS DEF_GROUP FROM {0} WHERE DEFECT_CODE LIKE '{1}%'", this.DBTableName, this.txtSearch.Text));
        this.lsbAvaliableDefCode.DataTextField = "DEFECT_CODE";
        this.lsbAvaliableDefCode.DataValueField = "DEF_GROUP";
        this.lsbAvaliableDefCode.DataBind();

    }

    protected void btnAll_Click(object sender, EventArgs e)
    {
        DbAccessHelper database = new DbAccessHelper("InnoluxDB");

        this.lsbAvaliableDefCode.DataSource =
            database.ExecuteDataSet(string.Format("SELECT DEFECT_CODE, DEFECT_CAT || ':' || DEFECT_CODE AS DEF_GROUP FROM {0}", this.DBTableName));
        this.lsbAvaliableDefCode.DataTextField = "DEFECT_CODE";
        this.lsbAvaliableDefCode.DataValueField = "DEF_GROUP";
        this.lsbAvaliableDefCode.DataBind();
    }

    private void ShowMessage(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(
            this.Page,
            this.Page.GetType(),
            "alertMes",
            string.Format(@"javascript:alert('{0}');", msg),
            true);
    }

    public enum DefShopName
    {
        CFT,
        ARRAY,
        CELL
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        string temp = "";
        foreach (ListItem item in this.lsbDefGroup.Items)
        {
            if (item.Selected == true)
            {
                temp += ("'" + item.Value + "',");
            }
        }
        if (!string.IsNullOrEmpty(temp))
        {
            DbAccessHelper database = new DbAccessHelper("InnoluxDB");
            temp = temp.Substring(0, temp.Length - 1);
            this.lsbAvaliableDefCode.DataSource = database.ExecuteDataSet(string.Format("SELECT DEFECT_CODE, DEFECT_CAT || ':' || DEFECT_CODE AS DEF_GROUP FROM {0} WHERE DEFECT_CAT IN ({1})", this.DBTableName, temp));
            this.lsbAvaliableDefCode.DataTextField = "DEFECT_CODE";
            this.lsbAvaliableDefCode.DataValueField = "DEF_GROUP";
            this.lsbAvaliableDefCode.DataBind();
        }
    }
}
