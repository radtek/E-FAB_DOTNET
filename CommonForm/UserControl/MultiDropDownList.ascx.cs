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


public partial class CommonForm_UserControl_MultiDropDownList : System.Web.UI.UserControl
{
    private string _SelectedText;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PageInit();

            DDList.Height = 0;
            if (DDList.Items.Count > 0)
            {
                DDLabel.Text = DDList.Items[0].Text;
                System.Web.UI.WebControls.ListItem li = DDList.Items.FindByText(DDLabel.Text);
                if (li != null)
                    li.Selected = true;
            }
            else
                DDLabel.Text = string.Empty;
        }
        else
        {	// set the selected text and tooltip
            DDLabel.Text = SelectedText;
            DDLabel.ToolTip = SelectedText;

        }
    }

    /// <summary>
    /// Set the attributes of the controls
    /// </summary>
    public void PageInit()
    {
        string ctlID = this.UniqueID + "_";
        DDList.Attributes.Add("onchange", "SelectedIndexChanged('" + ctlID + "');");
        DDList.Attributes.Add("onmouseout", "CloseListBox('" + ctlID + "');");
        DDLabel.Attributes.Add("onclick", "OpenListBox('" + ctlID + "');");
        //colDDImage.Attributes.Add("onclick", "OpenListBox('" + ctlID + "');");
    }

    #region  Public properties

    /// <summary>
    /// Get and Set the width of the Dropdown
    /// </summary>
    public double ListWidth
    {
        get { return Panel2.Width.Value; }
        set { Panel2.Width = (Unit)value; }
    }

    /// <summary>
    /// Gets arraylist of  selected values 
    /// </summary>
    public ArrayList SelectedValues
    {
        get
        {
            ArrayList selectedValues = new ArrayList();
            foreach (System.Web.UI.WebControls.ListItem li in DDList.Items)
            {
                if (li.Selected)
                { selectedValues.Add(li.Value); }
            }
            return selectedValues;
        }
    }

    /// <summary>
    /// Gets arraylist of  selected texts 
    /// </summary>
    public ArrayList SelectedTexts
    {
        get
        {
            ArrayList selectedTexts = new ArrayList();
            foreach (System.Web.UI.WebControls.ListItem li in DDList.Items)
            {
                if (li.Selected)
                { selectedTexts.Add(li.Text); }
            }
            return selectedTexts;
        }
    }

    /// <summary>
    /// Gets the selected text , the items are separated by comma
    /// </summary>
    public string SelectedText
    {
        get
        {
            string selText = string.Empty;
            foreach (System.Web.UI.WebControls.ListItem li in DDList.Items)
            {
                if (li.Selected)
                { selText += li.Text + ","; }
            }
            if (selText.Length > 0)
                selText = selText.Length > 0 ? selText.Substring(0, selText.Length - 1) : selText;
            return selText;
        }
        set
        {
            _SelectedText = value;
            DDLabel.Text = _SelectedText;
            DDLabel.ToolTip = _SelectedText;
        }
    }

    /// <summary>
    /// Gets the selected items of the list
    /// </summary>
    public ArrayList SelectedItems
    {
        get
        {
            ArrayList selectedItems = new ArrayList();
            foreach (System.Web.UI.WebControls.ListItem li in DDList.Items)
            {
                if (li.Selected)
                { selectedItems.Add(li); }
            }
            return selectedItems;
        }
        set
        {
            ArrayList selectedItems = value;
            string selText = string.Empty;

            // Deselect all the selected items
            foreach (System.Web.UI.WebControls.ListItem li in DDList.Items)
            { li.Selected = false; }

            // Select the items from the list
            foreach (System.Web.UI.WebControls.ListItem selItem in selectedItems)
            {
                System.Web.UI.WebControls.ListItem li = DDList.Items.FindByText(selItem.Text);
                if (li != null)
                { li.Selected = true; selText += li.Text + ","; }
            }
            if (selText.Length > 0)
                selText = selText.Length > 0 ? selText.Substring(0, selText.Length - 1) : selText;

            SelectedText = selText;
        }
    }
    /// <summary>
    /// Gets the list
    /// </summary>
    public System.Web.UI.WebControls.ListBox List
    {
        get { return DDList; }
        set { DDList = List; }
    }
    #endregion

    #region Public methods

    /// <summary>
    /// Remove all items in the list 
    /// </summary>
    public void Clear()
    {
        DDList.Items.Clear();
    }
    #endregion

}
