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

public partial class CommonForm_UserControl_TVProductSelector : System.Web.UI.UserControl
{
	public string DBConnectionName
	{
		get
		{
			object obj2 = this.ViewState["DBConnectionName"];
			if (obj2 != null)
			{
				return (string)obj2;
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["DBConnectionName"] = value;
		}
	}

	private string shopName = "T2CF";
	public string ShopName
	{
		get { return this.shopName; }
		set
		{
			if (string.IsNullOrEmpty(value) == false)
				this.shopName = value;
		}
	}

	public string ProductTable
	{
		get
		{
			if (ViewState["ProductTable"] == null || string.IsNullOrEmpty(ViewState["ProductTable"].ToString()))
				return "Product";
			return ViewState["ProductTable"].ToString();
		}
		set { ViewState["ProductTable"] = value; }
	}

	public string SubProductTable
	{
		get
		{
			if (ViewState["SubProductTable"] == null || string.IsNullOrEmpty(ViewState["SubProductTable"].ToString()))
				return "SubProduct";
			return ViewState["SubProductTable"].ToString();
		}
		set { ViewState["SubProductTable"] = value; }
	}

    // Todo: July 15, 2009 - by steven
    public bool SetAllSelected
    {
        get 
        {
            if (ViewState["SetAllSelected"] == null)
                return false;
            return bool.Parse(ViewState["SetAllSelected"].ToString());
        }
        set { ViewState["SetAllSelected"] = value; }
    }

	public bool IgnoreSubProduct
	{
		get
		{
			object obj2 = this.ViewState["IgnoreSubProduct"];
			return obj2 == null ? false : (bool)obj2;
		}
		set { this.ViewState["IgnoreSubProduct"] = value; }
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

	public string SelectedSize
	{
		get
		{
			return this.JoinNodeValue(this.GetCheckedNodesByLevel(0), ",", true);
		}
	}

	public string SelectedModel
	{
		get
		{
			return this.JoinNodeValue(this.GetCheckedNodesByLevel(1), ",", true);
		}
	}

	public string SelectedMainProdNames
	{
		get
		{
			return this.JoinNodeValue(this.GetCheckedNodesByLevel(2), ",", true);
		}
	}

	public string SelectedSubProdNames
	{
		get
		{
			return this.JoinNodeValue(this.CheckedSubProductNode, ",", true);
		}
	}

	public string SelectedProdFullName
	{
		get
		{
			List<RadTreeNode> productNodes = this.GetCheckedNodesByLevel(2);
			List<string> mainProdNames = new List<string>();
			foreach (RadTreeNode node in productNodes)
			{
				if (node.Nodes.Count > 0)
				{
					string temp = node.Text;

					for (int i = 0; i < node.Nodes.Count; i++)
					{
						if (i == 0)
						{
							temp = temp + "(" +node.Nodes[i].Text;
							if (i == node.Nodes.Count - 1)
							{
								temp = temp + ")";
							}
							else
							{
								temp = temp + ",";
							}
						}
						else if (i == node.Nodes.Count - 1)
						{
							temp = temp + node.Nodes[i].Text + ")";
						}
						else
						{
							temp = temp + node.Nodes[i].Text + ",";
						}
					}
					mainProdNames.Add(temp);
				}
			}
			return mainProdNames.Count > 0 ? string.Join(",", mainProdNames.ToArray()) : string.Empty;
		}
	}

    // Todo: 
    public string SelectedProductFullName
    {
        get
        {
            List<RadTreeNode> productNodes = this.GetCheckedNodesByLevel(2);
            string prods = string.Empty;
            foreach (RadTreeNode node in productNodes)
            {
                prods += string.Format(",{0}", node.Text).Replace("'", "");
                string subprods = string.Empty;
                foreach (RadTreeNode subnode in node.Nodes)
                    if (subnode.Checked == true)
                        subprods += string.Format(",{0}", subnode.Text).Replace("'", "");
                if (subprods.Length > 0)
                    prods = string.Format("{0}({1})", prods, subprods.Substring(1));
            }
            if (prods.Length > 0)
                prods = prods.Substring(1);
            return prods;
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

	public List<RadTreeNode> CheckedSubProductNode
	{
		get
		{
			return this.GetNodesByLevel(this.RadTreeView1.CheckedNodes, 3, false);
		}
	}

	public bool ShowSubProd
	{
		get { return this.trSubProd.Visible; }
		set { this.trSubProd.Visible = value; }
	}

	public bool ShowMainProd
	{
		get { return this.trMainProd.Visible; }
		set { this.trMainProd.Visible = value; }
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
            if (this.SetAllSelected == true)
                this.BtnClick_Click(sender, e);
		}
	}

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        LoadRootNodes();
		this.ckbAll.Attributes["onclick"] = "javascript:var tree = $find(\"" + this.RadTreeView1.ClientID + "\"); UpdateAllChildren(tree.get_nodes(), this.checked)";

    }

	protected void RadTreeView1_NodeExpand(object sender, RadTreeNodeEventArgs e)
	{
		string sql = "";
		switch (e.Node.Level)
		{
			case 0:
				sql = "SELECT distinct PROD_MODEL as text FROM " + this.ProductTable +
						" WHERE PROD_MODEL IS NOT NULL AND PROD_SIZE =" + e.Node.Value +
						" and SHOP='" + this.ShopName + "'";
				break;
			case 1:
				sql = "SELECT PROD_NAME as text FROM " + this.ProductTable +
						" WHERE ACTIVE_FLAG='1' AND PROD_SIZE=" + e.Node.ParentNode.Value +
						" AND PROD_MODEL=" + e.Node.Value +
						" and SHOP='" + this.ShopName + "'";
				break;
			case 2:
				sql = "SELECT prod_name as text FROM " + this.ProductTable + 
						" WHERE prod_name = '{0}' UNION SELECT SUBPROD_NAME AS prod_name FROM " + this.SubProductTable + 
						" T  WHERE T.PROD_NAME = " + e.Node.Value;
				break;
			default:
				return;
		}
		DbAccessHelper dbHelper = new DbAccessHelper(this.DBConnectionName);

		foreach (DataRow row in dbHelper.ExecuteDataSet(sql).Tables[0].Rows)
		{
			RadTreeNode node = new RadTreeNode();
			node.Text = row["text"].ToString();
			node.Value = "'" + row["text"].ToString() + "'";
			if (e.Node.Level < 2)
			{
				node.ExpandMode = TreeNodeExpandMode.ServerSideCallBack;
			}
			e.Node.Nodes.Add(node);
		}
		e.Node.Expanded = true;
	}

	protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
	{
		args.IsValid = RadTreeView1.CheckedNodes.Count > 0;
	}

	private List<RadTreeNode> GetCheckedNodesByLevel(int level)
	{
		List<RadTreeNode> result = new List<RadTreeNode>();
		List<RadTreeNode> checkedSubProdNode = this.CheckedSubProductNode;
		if (this.IgnoreSubProduct || checkedSubProdNode.Count == 0)
		{
			return this.GetNodesByLevel(this.RadTreeView1.CheckedNodes, level, false);
		}
		else
		{
			foreach (RadTreeNode subProdNode in checkedSubProdNode)
			{
				RadTreeNode node = this.GetParentNodeByLevel(subProdNode, level);
				if (node != null && !result.Contains(node))
				{
					result.Add(node);
				}
			}
		}
		return result;
	}

	private List<RadTreeNode> GetNodesByLevel(IList<RadTreeNode> nodes, int level, bool includeChildNodes)
	{
		List<RadTreeNode> result = new List<RadTreeNode>();
		foreach (RadTreeNode node in nodes)
		{
			if (node.Level == level && !result.Contains(node))
			{
				result.Add(node);
			}
			else
			{
				if (node.Level != 3)
				{
					RadTreeNode parentNode = this.GetParentNodeByLevel(node, level);
					if (parentNode != null && !result.Contains(parentNode))
					{
						result.Add(parentNode);
					}
				}

				if (includeChildNodes && node.Nodes.Count > 0)
				{
					List<RadTreeNode> childNodes = new List<RadTreeNode>();
					foreach (RadTreeNode childNode in node.Nodes)
					{
						childNodes.Add(childNode);
					}
					foreach (RadTreeNode cn in this.GetNodesByLevel(childNodes, level, includeChildNodes))
					{
						if (!result.Contains(cn))
						{
							result.Add(cn);
						}
					}
				}
			}
		}
		return result;
	}

	private RadTreeNode GetParentNodeByLevel(RadTreeNode node, int level)
	{
		if (node.Level == level)
		{
			return node;
		}
		else
		{
			return (node.ParentNode == null) ? null : this.GetParentNodeByLevel(node.ParentNode, level);
		}
	}

	private string JoinNodeValue(IList<RadTreeNode> nodes, string separator, bool isValue)
	{
		string temp = string.Empty;
		foreach (RadTreeNode node in nodes)
		{
			temp = isValue ? temp + node.Value + separator : temp + node.Text + separator;
		}
		return string.IsNullOrEmpty(temp) ? temp : temp.Substring(0, temp.Length - 1);
	}

	#region Private methods For Node Load
	private void LoadRootNodes()
	{
		DataTable dataSource = this.GetDataSource();
		foreach (DataRow row in dataSource.Rows)
		{
			string size = row["prod_size"].ToString();
			string model = row["prod_model"].ToString();
			string prod = row["prod_name"].ToString();
			string subprod = row["subprod_name"].ToString();

			RadTreeNode sizeNode = this.RetriveSizeNode(size, this.SetAllSelected);

			if (!string.IsNullOrEmpty(model))
			{
				RadTreeNode modelNode = this.RetriveRadTreeNode(sizeNode, model, this.SetAllSelected);
				if (!string.IsNullOrEmpty(model))
				{
					RadTreeNode prodNode = this.RetriveRadTreeNode(modelNode, prod, this.SetAllSelected);
					if (!string.IsNullOrEmpty(subprod) && this.ShowSubProd)
					{
						this.RetriveRadTreeNode(prodNode, subprod, this.SetAllSelected);
					}
				}
			}
		}
	}

    private DataTable GetDataSource()
    {
        DbAccessHelper dbHelper = new DbAccessHelper(this.DBConnectionName);
        string sql = @"select prod_size, prod_model, t.prod_name, s.subprod_name 
								from " + this.ProductTable + @" t, " + this.SubProductTable + @" s
								where t.fab_id ='Fab2' and t.prod_size is not null and t.shop='" + this.ShopName + @"'
								and t.prod_name = s.prod_name
								and T.active_flag = 1" +
                                @"union
               select prod_size, prod_model, t.prod_name, t.prod_name as subprod_name
                from " + this.ProductTable + @" t
								where t.fab_id ='Fab2' and t.prod_size is not null and t.shop='" + this.ShopName + @"'
								and T.active_flag = 1       " +
                                @"ORDER BY prod_size, prod_model, prod_name";
        try
        {
            return dbHelper.ExecuteDataSet(sql).Tables[0];
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

	private RadTreeNode RetriveSizeNode(string size, bool checkState)
	{
		RadTreeNode node = this.RadTreeView1.FindNodeByText(size);
		if (node == null)
		{
			node = new RadTreeNode(size, "'" + size + "'");
			node.ExpandMode = TreeNodeExpandMode.ClientSide;
			this.RadTreeView1.Nodes.Add(node);
		}
        node.Checked = checkState;
		return node;
	}

	private RadTreeNode RetriveRadTreeNode(RadTreeNode rootNode, string nodeText)
	{
		return RetriveRadTreeNode(rootNode, nodeText, false);
	}

	private RadTreeNode RetriveRadTreeNode(RadTreeNode rootNode, string nodeText, bool checkedState)
	{
		RadTreeNode node = rootNode.Nodes.FindNodeByText(nodeText);
		if (node == null)
		{
			node = new RadTreeNode(nodeText, "'" + nodeText + "'");
			node.ExpandMode = TreeNodeExpandMode.ClientSide;
			
			//node.Controls
            //node.Enabled = checkable;
            node.Checked = checkedState;
			rootNode.Nodes.Add(node);
		}
		return node;
	}
	#endregion

	protected void BtnClick_Click(object sender, EventArgs e)
	{
		this.txtSize.ToolTip = this.txtSize.Text = this.SelectedSize;
		this.txtModel.ToolTip = this.txtModel.Text = this.SelectedModel;
		this.txtProduct.ToolTip = this.txtProduct.Text = this.SelectedMainProdNames;
		this.txtSubProduct.ToolTip = this.txtSubProduct.Text = this.SelectedSubProdNames;

		this.modalPanel.Hide();
	}
}
