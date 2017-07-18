using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using Innolux.Portal.EntLibBlock.DataAccess;

public partial class CommonForm_UserControl_CellGradeSelector : System.Web.UI.UserControl
{
	private string shopName = "T2Cell";
	public string ShopName
	{
		get { return this.shopName; }
		set
		{
			if (string.IsNullOrEmpty(value) == false)
				this.shopName = value;
		}
	}

	public double Width
	{
		get
		{
			if (ViewState["Width"] == null || string.IsNullOrEmpty(ViewState["Width"].ToString()))
				return 250;
			return double.Parse(ViewState["Width"].ToString());
		}
		set { ViewState["Width"] = value; }
	}	

    // Todo: 
    public string SelectedCellGradeList
    {
        get
        {
               string  resultStr = "";
               
               if ( this.rbG0.Checked == true )
                    resultStr += ",G0";        
               if ( this.rbG1.Checked == true )
                    resultStr += ",G1";                    
               if ( this.rbG2.Checked == true )
                    resultStr += ",G2";        
               if ( this.rbG3.Checked == true )
                    resultStr += ",G3";
               if ( this.rbG4.Checked == true )
                    resultStr += ",G4";        
               if ( this.rbG5.Checked == true )
                    resultStr += ",G5";                    
               if ( this.rbG6.Checked == true )
                    resultStr += ",G6";        
               if ( this.rbG7.Checked == true )
                    resultStr += ",G7";       
               if ( this.rbG8.Checked == true )
                    resultStr += ",G8";        
               if ( this.rbG9.Checked == true )
                    resultStr += ",G9";                    
               if ( this.rbNG.Checked == true )
                    resultStr += ",NG";        
               if ( this.rbLR.Checked == true )
                    resultStr += ",LR";
               if ( this.rbLL.Checked == true )
                    resultStr += ",LL";
               if ( this.rbPR.Checked == true )
                    resultStr += ",PR";
               if ( this.rbHO.Checked == true )
                    resultStr += ",HO";
               if ( this.rbLT.Checked == true)
                    resultStr += ",LT";
                if (this.rbNA.Checked == true)
                    resultStr += ",NA";

                if (resultStr.Length > 1)
                    return resultStr.Substring(1);
                else
                    return "";
        }
    }

	public string ValidationGroup
	{
		get
		{
			return this.CustomValidator1.ValidationGroup;
		}
		set
		{
			this.CustomValidator1.ValidationGroup = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{          
            PageInit();
		}
	}

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    /// <summary>
    /// Set the attributes of the controls
    /// </summary>
    public void PageInit()
    {
       //this.txtCellGrades.Text = "G0,G1,G2,G3,G4,G5,G6,G7,G8,G9,NG";
        this.txtCellGrades.Text = "G0,G1,G2,G3,G4,G5,G6,G7,G9,NG";
    }

	protected void BtnClick_Click(object sender, EventArgs e)
	{
        this.txtCellGrades.ToolTip = this.txtCellGrades.Text = this.SelectedCellGradeList;

		this.modalPanel.Hide();
	}

    public void ResetCT1CT3Grades()
    {
        rbLT.Enabled = false;
        rbPR.Enabled = false;

        rbLT.Checked = false;
        rbPR.Checked = false;
        rbLR.Checked = false;
        rbLL.Checked = false;
        rbHO.Checked = false;

        rbG0.Checked = rbG1.Checked = rbG2.Checked = rbG3.Checked = true;
        rbG4.Checked = rbG5.Checked = rbG6.Checked = rbG7.Checked = true;
        //rbG8.Checked = rbG9.Checked = rbNG.Checked = rbG0NG.Checked = true;
        rbG9.Checked = rbNG.Checked = rbG0NG.Checked = true;

        //txtCellGrades.Text = "G0,G1,G2,G3,G4,G5,G6,G7,G8,G9,NG";
        txtCellGrades.Text = "G0,G1,G2,G3,G4,G5,G6,G7,G9,NG";
    }

    public void ResetCT2CT4Grades()
    {
        rbLT.Enabled = true;
        rbPR.Enabled = true;

        rbLT.Checked = false;
        rbPR.Checked = false;
        rbLR.Checked = false;
        rbLL.Checked = false;
        rbHO.Checked = false;
        rbNA.Checked = false;

        rbG0.Checked = rbG1.Checked = rbG2.Checked = rbG3.Checked = true;
        rbG4.Checked = rbG5.Checked = rbG6.Checked = rbG7.Checked = true;
        //rbG8.Checked = rbG9.Checked = rbNG.Checked = rbG0NG.Checked = true;
        rbG9.Checked = rbNG.Checked = rbG0NG.Checked = true;

        //this.txtCellGrades.Text = "G0,G1,G2,G3,G4,G5,G6,G7,G8,G9,NG";
        this.txtCellGrades.Text = "G0,G1,G2,G3,G4,G5,G6,G7,G9,NG";
    }

}
