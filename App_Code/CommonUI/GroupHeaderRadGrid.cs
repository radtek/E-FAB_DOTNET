using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Collections.Generic;

namespace Innolux.Portal.WebControls
{
    /// <summary>
    /// Summary description for GroupHeaderRadGrid
    /// </summary>
    public class GroupHeaderRadGrid : RadGrid
    {
        #region Members
		//public int GroupColumnCount
		//{
		//    get
		//    {
		//        return (int)this.ViewState["GroupColumnCount"];
		//    }
		//    set
		//    {
		//        this.ViewState["GroupColumnCount"] = value;
		//    }
		//}

		//public int GroupStartIndex
		//{
		//    get
		//    {
		//        if (this.ViewState["GroupStartIndex"] == null)
		//        {
		//            return -1;
		//        }
		//        return (int)this.ViewState["GroupStartIndex"];
		//    }
		//    set
		//    {
		//        this.ViewState["GroupStartIndex"] = value;
		//    }
		//}

        public char HeaderTextSplitChar
        {
            get
            {
                return (char)this.ViewState["HeaderTextSplitChar"];
            }
            set
            {
                this.ViewState["HeaderTextSplitChar"] = value;
            }
        }

        public string TitleText
        {
            get
            {
                return this.ViewState["TitleTest"] as string;
            }
            set
            {
                this.ViewState["TitleTest"] = value;
            }
        }

        public string GroupedHeaderText
        {
            get
            {
                return (string)this.ViewState["GroupedHeaderText"];
            }
            set
            {
                this.ViewState["GroupedHeaderText"] = value;
            }
        }

        public bool AutoHeaderGroupBySplit
        {
            get 
            {
                if (this.ViewState["AutoHeaderGroupBySplit"] == null)
                    return false;
                return (bool)this.ViewState["AutoHeaderGroupBySplit"]; 
            }
            set { this.ViewState["AutoHeaderGroupBySplit"] = value; }
        }

        public bool AutoSameMergeRow
        {
            get{
                if (this.ViewState["AutoSameMergeRow"] == null)
                    return false;
                return (bool)this.ViewState["AutoSameMergeRow"];}
            set { this.ViewState["AutoSameMergeRow"] = value; }
        }

        public string MergeColumnIndexes
        {
            get
            {
                return (string)this.ViewState["MergeColumnIndexes"];
            }
            set
            {
                this.ViewState["MergeColumnIndexes"] = value;
            }
        }
        #endregion

        public GroupHeaderRadGrid()
        {
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (this.AutoSameMergeRow)
            {
                
                string[] mergeColumnIndexes = this.MergeColumnIndexes.Split(',');
                string[] tempText = new string[mergeColumnIndexes.Length];
                int[] tempTextRowNum = new int[mergeColumnIndexes.Length];
                foreach (GridDataItem item in this.Items)
                {
                    if (item.ItemType == GridItemType.Item || item.ItemType == GridItemType.AlternatingItem)
                    {
                        if (!string.IsNullOrEmpty(this.MergeColumnIndexes))
                        {
                            for (int j = 0; j < mergeColumnIndexes.Length; j++)
                            {
                                int i = int.Parse(mergeColumnIndexes[j]);
                                if (tempText[j] == item.Cells[i + 1].Text)
                                {
                                    this.Items[tempTextRowNum[j]].Cells[i + 1].RowSpan += 1;
                                    item.Cells[i + 1].Text = "&nbsp;";
                                    item.Cells[i + 1].Visible = false;
                                    if (i < item.Cells.Count - 3)
                                    {
                                        item.Cells[i + 2].Attributes["style"] = "border-left-color:#D0D7E5;";
                                    }
                                }
                                else
                                {
                                    tempText[j] = item.Cells[i + 1].Text;
                                    tempTextRowNum[j] = item.ItemIndex;

                                    this.Items[tempTextRowNum[j]].Cells[i + 1].RowSpan = 1;
                                }
                            }
                        }
                    }
                }

            }

        }

        private int GetGroupColSpan(TableCellCollection cells, int idx)
        {
            string[] splitedStr = cells[idx].Text.Split(this.HeaderTextSplitChar);
            int colSpan = 0;
            if (splitedStr.Length == 1)
            {
                return colSpan;
            }
            for (int i = idx; i < cells.Count; i++)
            {
                if (cells[i].Text.Split(this.HeaderTextSplitChar)[0] == splitedStr[0])
                {
                    colSpan++;
                }
            }
            return colSpan;
        }

        private string GetThString(string text, string cssName, int colSpan)
        {
            string format = "";
            if (colSpan == 0)
            {
                format = "<th scope=\"col\" rowspan=\"2\" colspan=\"{0}\" class=\"{1}\" style=\"text-align:center;\">{2}</th>";
            }
            else
            {
                format = "<th scope=\"col\" colspan=\"{0}\" class=\"{1}\" style=\"text-align:center;\">{2}</th>";
            }
            return string.Format(format, colSpan, cssName, text);
        }

        protected override void OnItemDataBound(GridItemEventArgs e)
        {
            if (e.Item is GridHeaderItem)
            {
                string css = "GridHeader_" + this.Skin;
                string titleTr = "";
                string groupTr = "";
                if (e.Item.Cells.Count > 2)
                {
                    string cell2Text = e.Item.Cells[2].Text;

                    if (!string.IsNullOrEmpty(this.TitleText))
                    {
                        e.Item.Cells[2].ColumnSpan = e.Item.Cells.Count - 2;
                        e.Item.Cells[2].Font.Italic = true;
                        e.Item.Cells[2].Font.Bold = true;
                        titleTr = this.TitleText + "</th></tr><tr>";
                        if (!this.AutoHeaderGroupBySplit)
                        {
                            e.Item.Cells[2].Text = this.TitleText + "</th></tr><tr><th scope=\"col\" class=\"" + css + "\">" + cell2Text + "</th>";
                        }
                    }

                    if (this.AutoHeaderGroupBySplit)
                    {
                        int firstCellColspan = this.GetGroupColSpan(e.Item.Cells, 2);
                        int startGroupIdx = 2;
                        if (string.IsNullOrEmpty(this.TitleText))
                        {
                            e.Item.Cells[2].ColumnSpan = firstCellColspan;
							e.Item.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                            if (firstCellColspan == 0 )//|| firstCellColspan == 1)
                            {
                                e.Item.Cells[2].RowSpan = 2;
                                e.Item.Cells[2].ColumnSpan = 1;
                                groupTr = groupTr + cell2Text +"</th>";
                            }
                            else
                            {
                                groupTr = groupTr + cell2Text.Split(this.HeaderTextSplitChar)[0] + "</th>";
                            }
                        }
                        else
                        {
                            groupTr = groupTr + this.GetThString(cell2Text, css, firstCellColspan);
                        }

                        for (int i = 3; i < e.Item.Cells.Count; i++)
                        {
                            string[] cellText = e.Item.Cells[i].Text.Split(this.HeaderTextSplitChar);

                            if (i >= startGroupIdx + firstCellColspan)
                            {
                                firstCellColspan = this.GetGroupColSpan(e.Item.Cells, i);
                                startGroupIdx = i;
                                groupTr = groupTr + this.GetThString(cellText[0], css, firstCellColspan);
                            }
                            if (cellText.Length > 1)
                            {
                                e.Item.Cells[i].Text = cellText[1];
                                e.Item.Cells[i].Attributes["style"] = "border-left:solid 1px #9EB6CE;text-align:center;";
                            }
                            else
                            {
                                e.Item.Cells[i].Visible = false;
                            }
                        }
                        if (cell2Text.Split(this.HeaderTextSplitChar).Length > 1)
                        {
                            groupTr = groupTr + "</tr><tr>" + "<th scope=\"col\"  class=\"" + css + "\">" + cell2Text.Split(this.HeaderTextSplitChar)[1] + "</th>";;
                        }
                        else
                        {
                            groupTr = groupTr + "</tr><tr>";
                        }

                        if (string.IsNullOrEmpty(this.TitleText))
                        {
                            e.Item.Cells[2].Text = groupTr;
                        }
                        else
                        {
                            e.Item.Cells[2].Text = titleTr + groupTr;
                        }
                    }
                    
                }
            }

            base.OnItemDataBound(e);
        }
    }
}