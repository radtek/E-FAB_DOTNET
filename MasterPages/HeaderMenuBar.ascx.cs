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
using System.Data.OracleClient;
using System.Data.OleDb;
using System.IO;
using Telerik.Web.UI;
using Innolux.Portal.EntLibBlock.DataAccess;

public partial class UserControls_HeaderMenuBar : System.Web.UI.UserControl
{
    protected DbAccessHelper m_objDB = new DbAccessHelper("SSODB");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.MenuDatabind();
        }

    }

    private void MenuDatabind()
    {
       
        string sql = @" select distinct d.* from ws_usergroup a,ws_menugroup b, ws_menu d
                        where a.groupid=b.groupid and b.itemno=d.itemno
                        and d.moduleid=1 and a.empno='{0}'
                        and d.sysname = 'Web_System' order by d.itemno";

        sql = string.Format(sql, HttpContext.Current.User.Identity.Name.ToUpper());
        DataSet ds = m_objDB.ExecuteDataSet(sql);

        // module id = 1 for hearder menu item
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            RadMenuItem item = new RadMenuItem();
            item.Value = row["itemno"].ToString();
            item.Text = row["itemname"].ToString();
            item.ToolTip = row["itemhint"] is DBNull ? "" : row["itemhint"].ToString();
            // Set Default url if db setting is empty
            //item.NavigateUrl = row["url"] is DBNull ? "~/Default.aspx" : row["url"].ToString();
            item.NavigateUrl = row["url"] is DBNull ? "http://www.innolux.com/" : string.Format(row["url"].ToString(), HttpContext.Current.User.Identity.Name.ToUpper());

            //設定開啟頁面的顯示位置
            if (row["outsideflag"].ToString().Equals("0"))
            {
                item.Target = "_self";
            }
            else
            {
                item.Target = "_blank";
            }

            // Replace user id if out system need authenticate
            //item.NavigateUrl = row["authflag"] is DBNull || row["authflag"].ToString() == "0" ? item.NavigateUrl : string.Format(item.NavigateUrl, "TWN050314");

            // Add to parent item if the item is child item
            object parentNo = row["parentno"];
            if (parentNo is DBNull || parentNo.ToString() == "0")
            {
                this.RadMenu1.Items.Add(item);
            }
            else
            {
                RadMenuItem parentItem = this.RadMenu1.FindItemByValue(parentNo.ToString());
                if (parentItem != null)
                {
                    parentItem.Items.Add(item);
                }

            }
        }

     }

    protected void RadMenu1_ItemClick(object sender, RadMenuEventArgs e)
    {
        if (e.Item.Text.EndsWith("回到首頁"))
        {
            e.Item.NavigateUrl = "~/Default.aspx";
            e.Item.Target = "_self";
        }
    }
}
