using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Innolux.Portal.EntLibBlock.DataAccess;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections;
using Telerik.Web.UI;
using System.IO;
using AjaxControlToolkit;
using Innolux.Portal.EntLibBlock;

namespace Innolux.Portal.CommonFunction
{
    /// <summary>
    /// Summary description for CommonUI
    /// </summary>
    public class CommonUI
    {
        private RadTreeView _objTreeView = null;
        private string _strErrorMessage = "";

        public string ErrorMessage
        {
            get { return (_strErrorMessage); }
            set { _strErrorMessage = value; }
        }

         
        public CommonUI()
        {

        }

        /// <summary>
        /// Add data to Combox
        /// </summary>
        /// <param name="objTable"></param>
        /// <param name="strFieldName"></param>
        /// <param name="objBox"></param>
        /// <param name="IgnoreSameValue"></param>
        /// 
        public string GetSelectedItem(ListBox objList, string splitChart)
        {
            string strReturn = "";
            for (int intI = 0; intI < objList.Items.Count; intI++)
            {
                ListItem item = objList.Items[intI];
                if (item.Selected == true)
                {
                    strReturn += item.Text.ToString() + splitChart;
                }
            }
            if (strReturn.Length > 0)
            {
                strReturn = strReturn.Substring(0, strReturn.Length - splitChart.Length);
            }
            return (strReturn);
        }

        public string GetSelectedItem(CheckBoxList objList, string splitChart)
        {
            string strReturn = "";
            for (int intI = 0; intI < objList.Items.Count; intI++)
            {
                ListItem item = objList.Items[intI];
                if (item.Selected == true)
                {
                    strReturn += item.Text.ToString() + splitChart;
                }
            }
            if (strReturn.Length > 0)
            {
                strReturn = strReturn.Substring(0, strReturn.Length - splitChart.Length);
            }
            return (strReturn);
        }


        public void ListDataToCombox(DataTable objTable, string strFieldName, RadComboBox objBox, bool IgnoreSameValue)
        {
            if (objTable == null)
            {
                return;
            }
            else
            {
                objBox.Items.Clear();
                for (int intI = 0; intI < objTable.Rows.Count; intI++)
                {
                    string strData = objTable.Rows[intI][strFieldName].ToString();
                    RadComboBoxItem objItem = new RadComboBoxItem(strData);
                    if (IgnoreSameValue)
                    {
                        if (objBox.Items.IndexOf(objItem) < 0)
                        {
                            objBox.Items.Add(objItem);
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        objBox.Items.Add(objItem);
                    }
                }
            }
        }

        public void ListDataToCombox(DataTable objTable, string strFieldName, RadComboBox objBox, bool IgnoreSameValue, bool AddAllFirstly)
        {
            if (objTable == null)
            {
                return;
            }
            else
            {
                objBox.Items.Clear();
                if (AddAllFirstly == true)
                {
                    objBox.Items.Add(new RadComboBoxItem("ALL"));
                }
                for (int intI = 0; intI < objTable.Rows.Count; intI++)
                {
                    string strData = objTable.Rows[intI][strFieldName].ToString();
                    RadComboBoxItem objItem = new RadComboBoxItem(strData);
                    if (IgnoreSameValue)
                    {
                        if (objBox.Items.IndexOf(objItem) < 0)
                        {
                            objBox.Items.Add(objItem);
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        objBox.Items.Add(objItem);
                    }
                }
            }
        }

        public void ListDataToList(DataTable objTable, string strField, ListBox objList)
        {
            string strItem = "";
            if (objTable == null)
            {
                return;
            }
            else
            {
                objList.Items.Clear();
                for (int intI = 0; intI < objTable.Rows.Count; intI++)
                {
                    strItem = objTable.Rows[intI][strField].ToString();
                    ListItem objItem = new ListItem(strItem);
                    if (objList.Items.IndexOf(objItem) < 0)
                    {
                        objList.Items.Add(objItem);
                    }
                }
            }
        }

        public void ListDataToList(DataTable objTable, int columnIndex, ListBox objList)
        {
            string strItem = "";
            if (objTable == null)
            {
                return;
            }
            else
            {
                objList.Items.Clear();
                for (int intI = 0; intI < objTable.Rows.Count; intI++)
                {
                    strItem = objTable.Rows[intI][columnIndex].ToString();
                    ListItem objItem = new ListItem(strItem);
                    if (objList.Items.IndexOf(objItem) < 0)
                    {
                        objList.Items.Add(objItem);
                    }
                }
            }
        }

        public void ListDataToList(DataTable objTable, string strField, ListBox objList, string appendField)
        {
            string strItem = "";
            if (objTable == null)
            {
                return;
            }
            else
            {
                objList.Items.Clear();
                for (int intI = 0; intI < objTable.Rows.Count; intI++)
                {
                    strItem = objTable.Rows[intI][strField].ToString() + "(" + objTable.Rows[intI][appendField].ToString() + ")";
                    ListItem objItem = new ListItem(strItem);
                    if (objList.Items.IndexOf(objItem) < 0)
                    {
                        objList.Items.Add(objItem);
                    }
                }

            }
        }

        public void ListDataToCheckBox(DataTable table, string  fieldName, CheckBoxList list)
        {
            string strItem = "";
            if (table == null)
            {
                return;
            }
            list.Items.Clear();
            for (int intI = 0; intI < table.Rows.Count; intI++)
            {
                strItem = table.Rows[intI][fieldName].ToString();
                ListItem objItem = new ListItem(strItem);
                if (list.Items.IndexOf(objItem) < 0)
                {
                    list.Items.Add(objItem);
                }
            }
        }

        public void ListDataToCheckBox(DataTable table,int ColumnIndex, CheckBoxList list)
        {
            string strItem = "";
            if (table == null)
            {
                return;
            }
            list.Items.Clear();
            for (int intI = 0; intI < table.Rows.Count; intI++)
            {
                strItem = table.Rows[intI][ColumnIndex].ToString();
                ListItem objItem = new ListItem(strItem);
                if (list.Items.IndexOf(objItem) < 0)
                {
                    list.Items.Add(objItem);
                }
            }
        }

        public void ListDataToTreeView(DataTable table, string fieldName, RadTreeView  treeView)
        {
            string strItem = "";
            if (table == null)
            {
                return;
            }
            treeView.Nodes.Clear();
            for (int intI = 0; intI < table.Rows.Count; intI++)
            {
                strItem = table.Rows[intI][fieldName].ToString();
                RadTreeNode objItem = new RadTreeNode(strItem);
                
                if (treeView.FindNodeByText(strItem) == null)
                {
                    treeView.Nodes.Add(objItem);
                }
            }


        }
        public void OpenRadWindow(RadWindowManager RadWinMan, RadWindow RadWin, Button sBnttonID, string sNavigateUrl, string sTaitle, string ReturnElementID, string clientFormID)
        {
            //RadWindow RadWin = new RadWindow();
            // RadWinMan.Windows.Add(RadWin);
            RadWin.NavigateUrl = sNavigateUrl;
            RadWin.OpenerElementID = sBnttonID.ClientID;
            //sBnttonID;
            RadWin.VisibleOnPageLoad = true;
            RadWin.Title = sTaitle;
            RadWin.Width = Unit.Parse("340");
            RadWin.Height = Unit.Parse("220");
            RadWin.ReloadOnShow = false;
            //RadWin.Left = Unit.Parse("300");
            //RadWin.Top = Unit.Parse("0");
            //RadWin.Animation = WindowAnimation.Fade;
            RadWin.Modal = true;
            RadWin.OffsetElementID = ReturnElementID;
            //"RadDateInput1";
            //RadWin.VisibleTitlebar = true;
            RadWin.InitialBehaviors = WindowBehaviors.Close;
            RadWin.Behaviors = WindowBehaviors.Close;
            if (clientFormID.Length > 0)
            {
                RadWin.ID = clientFormID;
                RadWin.ClientCallBackFunction = "CallBackFunctionUC"; 
            }
            else
            {
                RadWin.ClientCallBackFunction = "CallBackFunction";
            }
            RadWin.Overlay = true;
           // RadWin.ID = "RadWin_" + sTaitle;
            
            RadWin.VisibleStatusbar = false;
            RadWin.Skin = "Office2007";
            RadWin.DestroyOnClose = true;
            RadWin.ReloadOnShow = true;
            // RadWin.Attributes.Add("sql", sSql);
            //RadWindowManager RadWinMan=new RadWindowManager();
            RadWinMan.Windows.Add(RadWin);
            RadWinMan.Title = sTaitle;
        }

       

        public string PaserMutilSelectText(string sText)
        {
            if (sText.Contains(","))
            {
                sText = sText.Replace(",", "', '");
                sText = "'" + sText + "'";
            }
            else if (sText.Trim().Length <= 0)
            {
                sText ="";
            }
            else
            {
                sText = "'" + sText + "'";
            }
            return (sText);
        }
        public string GetStringChkList(CheckBoxList dscol)
        {
            string var = "";
            for (int i = 0; i <= dscol.Items.Count - 1; i++)
            {
                if (dscol.Items[i].Selected)
                {
                    var = var + "'" + dscol.Items[i].Value + "'" + ",";
                }
            }
            if (var.Length > 0)
            {
                var = var.Substring(0, var.Length - 1);
            }
            return (var);
        }

        #region "Select user defined ComboxList"

        public  void OnPopSelectCheckBoxList(CheckBoxList list, PopupControlExtender popControl)
        {
            string itemSelect = "";

            foreach (ListItem item in list.Items)
            {
                if (item.Selected == true)
                {
                    itemSelect += item.Value + ";";
                }
            }
            if (itemSelect.Length > 0)
            {
                itemSelect = itemSelect.Substring(0, itemSelect.Length - 1);
            }
            popControl.Commit(itemSelect);
        }

        #endregion

        #region "Add data to Tree"
        public void ListDataToRadTree(DataTable tableSource, string parentField, RadTreeView treeView)
        {
            string strItemNo = "";
            string strItemName = "";
            string strurl = "";
            string strParentNo = "";
            RadTreeNode objMidNode = null;
            treeView.Nodes.Clear();
            for (int intIndex = 0; intIndex < tableSource.Rows.Count; intIndex++)
            {
                strItemNo = tableSource.Rows[intIndex]["itemno"].ToString();
                strItemName = tableSource.Rows[intIndex]["itemname"].ToString();
                strurl = tableSource.Rows[intIndex]["URL"].ToString();
                strParentNo = tableSource.Rows[intIndex]["parentno"].ToString();

                if (tableSource.Rows[intIndex][parentField].ToString() == "0")
                {
                    RadTreeNode objNode = new RadTreeNode(strItemName, strItemNo);
                    treeView.Nodes.Add(objNode);
                }
                else
                {
                    objMidNode = treeView.FindNodeByValue(strParentNo);
                    if (objMidNode != null)
                    {
                        objMidNode.Nodes.Add(new RadTreeNode(strItemName, strItemNo));
                    }
                }
            }
        }

        public void ListDataToRadTree(DataTable tableSource, string parentField, RadTreeView treeView, string parentValue)
        {
            string strItemNo = "";
            string strItemName = "";
            string strurl = "";
            string strParentNo = "";
            RadTreeNode objMidNode = null;
            treeView.Nodes.Clear();
            for (int intIndex = 0; intIndex < tableSource.Rows.Count; intIndex++)
            {
                strItemNo = tableSource.Rows[intIndex]["itemno"].ToString();
                strItemName = tableSource.Rows[intIndex]["itemname"].ToString();
                strurl = tableSource.Rows[intIndex]["URL"].ToString();
                strParentNo = tableSource.Rows[intIndex]["parentno"].ToString();

                if (tableSource.Rows[intIndex][parentField].ToString() == parentValue)
                {
                    RadTreeNode objNode = new RadTreeNode(strItemName, strItemNo);
                    treeView.Nodes.Add(objNode);
                }
                else
                {
                    objMidNode = treeView.FindNodeByValue(strParentNo);
                    if (objMidNode != null)
                    {
                        objMidNode.Nodes.Add(new RadTreeNode(strItemName, strItemNo));
                    }
                }
            }
        }

        public void ListDataToRadTree(DataTable tableSource, string parentField, RadTreeView treeView, bool showTip)
        {
            string strItemNo = "";
            string strItemName = "";
            string strurl = "";
            string strParentNo = "";
            string strTip = "";
            RadTreeNode objMidNode = null;
            treeView.Nodes.Clear();
            for (int intIndex = 0; intIndex < tableSource.Rows.Count; intIndex++)
            {
                strItemNo = tableSource.Rows[intIndex]["itemno"].ToString();
                strItemName = tableSource.Rows[intIndex]["itemname"].ToString();
                strurl = tableSource.Rows[intIndex]["URL"].ToString();
                strParentNo = tableSource.Rows[intIndex]["parentno"].ToString();
                strTip = "No:" + strItemNo + System.Environment.NewLine;
                strTip += "Name:" + strItemName + System.Environment.NewLine;
                strTip += "url:" + strurl;

                if (tableSource.Rows[intIndex][parentField].ToString() == "0")
                {
                    RadTreeNode objNode = new RadTreeNode(strItemName, strItemNo);
                    if (showTip)
                    {
                        objNode.ToolTip = strTip;
                    }
                    treeView.Nodes.Add(objNode);
                }
                else
                {
                    objMidNode = treeView.FindNodeByValue(strParentNo);
                    if (objMidNode != null)
                    {
                        RadTreeNode objsubNode = new RadTreeNode(strItemName, strItemNo);
                        if (showTip)
                        {
                            objsubNode.ToolTip = strTip;
                        }
                        objMidNode.Nodes.Add(objsubNode);
                    }
                }
            }
        }

        private RadTreeNode GetTreeNodeByValue(RadTreeView objTree, string value)
        {
            RadTreeNode valueNode = null;
            for (int intI = 0; intI < objTree.Nodes.Count; intI++)
            {
                if (objTree.Nodes[intI].Value == value)
                {
                    valueNode = objTree.Nodes[intI];
                    break;
                }
                else
                {
                    foreach (RadTreeNode node in objTree.Nodes[intI].Nodes)
                    {
                        valueNode = GetTreeNodeByValue(node, value);
                    }
                }
            }
            return (valueNode);
        }

        private RadTreeNode GetTreeNodeByValue(RadTreeNode objNode, string value)
        {
            RadTreeNode subNode = null;
            if (objNode.Value == value)
            {
                subNode = objNode;
            }
            else
            {

                foreach (RadTreeNode node in objNode.Nodes)
                {
                    GetTreeNodeByValue(node, value);
                }

            }
            return (subNode);
        }

        #endregion

        #region "For folder operation"


        public bool CreateSubFolder(string path, string strFolder)
        {
            try
            {
                string strFolderpath = "~/" + path;
                strFolderpath = HttpContext.Current.Server.MapPath(strFolderpath);
                if (Directory.Exists(strFolderpath) == true)
                {
                    Directory.CreateDirectory(strFolderpath + "\\" + strFolder);
                }
                else
                {
                    _strErrorMessage = "The folder [" + strFolderpath + "] not exist!";
                }
                return (true);
            }
            catch (Exception ex)
            {
                _strErrorMessage = ex.Message;
                return (false);
            }
        }

        public bool CreateSubFolder(string SubFolderName)
        {
            try
            {
                string strFolder = GetRootFolder();
                Directory.CreateDirectory(strFolder + @SubFolderName);
                return (true);
            }
            catch (Exception ex)
            {
                _strErrorMessage = ex.Message;
                return (false);
            }
        }

        private string GetRootFolder()
        {
            string strPath = "";
            strPath = HttpContext.Current.Server.MapPath("~/Emeeting/meetingfiles");
            return (strPath);
        }
        #endregion

        #region "For TransForm DataGrid From Row To Column"

        public DataTable TransformDataTable(DataTable dt, String Header, String HeaderField)
        {
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add(Header, typeof(string));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtNew.Columns.Add(dt.Rows[i][HeaderField].ToString(), typeof(string));
            }
            foreach (DataColumn dc in dt.Columns)
            {
                DataRow drNew = dtNew.NewRow();
                drNew[Header] = dc.ColumnName;
                if (dc.ColumnName.ToUpper() != Header.ToUpper())
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        drNew[i + 1] = dt.Rows[i][dc].ToString();
                    }
                    dtNew.Rows.Add(drNew);
                }
            }
            return dtNew;
        }
        #endregion

        #region "For Message"
        public  void ShowAlertMessage(string message, Page page)
        {
            string strMsg = "<script type= 'text/javascript' language  ='javascript'>" + "alert('" + message + "')" + "</script>";
            ScriptManager.RegisterStartupScript(page, GetType(), "MSG001", strMsg, false);
        }

        public void AlertBizExcMessage(string msg, Page page)
        {
            //转义提示信息中的单引号 "'"
            msg = msg.Replace("'", "\\'");
            //去掉换行符"\n"
            msg = msg.Replace("\n", "");
            string js = " <script language='javascript'>"
                 + "alert('" + msg + "');"
                 + " </script>";
            page.ClientScript.RegisterClientScriptBlock( page.GetType(), "PopupMessage", js);
        }
        #endregion

    }

}
