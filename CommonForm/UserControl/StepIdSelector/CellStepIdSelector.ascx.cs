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

public partial class CommonForm_UserControl_CellStepIdSelector : System.Web.UI.UserControl
{
    public string SelectedStepId
    {
        get { return this.ddlStepName.SelectedValue; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
