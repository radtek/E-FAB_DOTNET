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
using Innolux.Portal.Report;
using Innolux.Portal.WebControls;
using Innolux.Portal.EntLibBlock.DataAccess;
using Innolux.Portal.CommonFunction;
using Telerik.Web.UI;

public partial class TFT_TP_KPI_IN_OUT_CVP_Report : System.Web.UI.Page
{
    DbAccessHelper m_objDB = new DbAccessHelper("T2PRPT");
    protected void Page_Load(object sender, EventArgs e)
    {                     
        if (!Page.IsPostBack)
        {
            this.Get_TFT_Data();
            this.Get_SG_Data();
        }
    }   

    protected void Get_TFT_Data()
    {        

        string sSql_IN = this.Get_Input_Grid_SQL();
        string sSql_OUT = this.Get_Output_Grid_SQL();
        DataTable dt_in = m_objDB.ExecuteDataSet(sSql_IN).Tables[0];
        DataTable dt_out = m_objDB.ExecuteDataSet(sSql_OUT).Tables[0];
        DataView dv_in = dt_in.DefaultView;
        DataView dv_out = dt_out.DefaultView;

        dv_in.RowFilter = "shop in ('T1$ARY','T1$CEL','T1$CF','T2$ARY','T2$CEL','T2$CF')";
        dv_out.RowFilter = "shop in ('T1$ARY','T1$CEL','T1$CF','T2$ARY','T2$CEL','T2$CF')";

        this.RG_TFT.DataSource = this.RoTation(dv_in.ToTable(), dv_out.ToTable(), "TFT");
        this.RG_TFT.DataBind();

    }

    protected void Get_SG_Data()
    {
        string sSql_IN = this.Get_Input_Grid_SQL();
        string sSql_OUT = this.Get_Output_Grid_SQL();
        DataTable dt_in = m_objDB.ExecuteDataSet(sSql_IN).Tables[0];
        DataTable dt_out = m_objDB.ExecuteDataSet(sSql_OUT).Tables[0];
        DataView dv_in = dt_in.DefaultView;
        DataView dv_out = dt_out.DefaultView;

        dv_in.RowFilter = "shop in ('C2$ARY','C2$CEL','C3$C3','C5$C5','FCTP$FCTP','LAM$LAM','WIS$WIS')";
        dv_out.RowFilter = "shop in ('C2$ARY','C2$CEL','C3$C3','C5$C5','FCTP$FCTP','LAM$LAM','WIS$WIS')";

        this.RG_SG.DataSource = this.RoTation(dv_in.ToTable(), dv_out.ToTable(),"SG");
        this.RG_SG.DataBind();
    }


    protected string Get_Input_Grid_SQL()
    {
        string sDate = System.DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
        string sSql_IN = @" 
                    select 
                    decode(MPS.SHOP,'T1CEL','T1$CEL','T1CF','T1$CF','T2ARY','T2$ARY','T0CEL','C2$CEL','T1ARY','T1$ARY','T2CEL','T2$CEL','T0ARY','C2$ARY','T2CF','T2$CF','C3','C3$C3','C5','C5$C5','FCTP','FCTP$FCTP','LAM','LAM$LAM','WIS','WIS$WIS',MPS.SHOP) as SHOP,
                    to_char( nvl((D_IN_OUT.MTD_IN_ACTUAL_QTY/  case when MPS.MTD_IN_PLAN_QTY=0 then 1 else MPS.MTD_IN_PLAN_QTY end)*100,0),'99990.99' ) || '%' as CVP,
                    MPS.INIT_IN_PLAN_QTY,
                    MPS.NEW_IN_PLAN_QTY,
                    (MPS.INIT_IN_PLAN_QTY-MPS.NEW_IN_PLAN_QTY) as MPS_DIFF_QTY,
                    MPS.MTD_IN_PLAN_QTY,
                    D_IN_OUT.MTD_IN_ACTUAL_QTY,
                    (MPS.MTD_IN_PLAN_QTY-D_IN_OUT.MTD_IN_ACTUAL_QTY) as D_IN_OUT_DIFF
                    from
                    (
                            select A.SHOP,A.TYPE,
                            sum(A.INIT_IN_PLAN_QTY) as INIT_IN_PLAN_QTY,
                            sum(NEW_IN_PLAN_QTY) as NEW_IN_PLAN_QTY,
                            sum(MTD_IN_PLAN_QTY) as MTD_IN_PLAN_QTY
                            from
                            (
                               select t.shop,t.type,t.prod_name,
                               --INIT_IN_PLAN_QTY
                                  decode(t.shop,
                                  'T0CEL',
                                  round(nvl(sum
                                  (
                                      CASE
                                          WHEN KIND='MONTH'
                                          THEN nvl(t.qty,0)
                                      END
                                  ),0)/DW_SUM.GET_PROD_MAPPING_MFG_MOVE(t.prod_name,'CUT','CHIP','T0Cell'),0),
                                  'T1CEL',
                                  round(nvl(sum
                                  (
                                      CASE
                                          WHEN KIND='MONTH'
                                          THEN nvl(t.qty,0)
                                      END
                                  )/DW_SUM.GET_PROD_MAPPING_MFG_MOVE(t.prod_name,'SUBSTRATE','CHIP','T1Cell'),0),0),
                                  nvl(sum
                                  (
                                      CASE
                                          WHEN KIND='MONTH'
                                          THEN nvl(t.qty,0)
                                      END
                                  ),0)
                                  
                                  )as INIT_IN_PLAN_QTY,    
                                  
                                  --NEW_IN_PLAN_QTY
                                  decode(t.shop,
                                  'T0CEL',
                                  round(nvl(sum
                                  (
                                      CASE
                                          WHEN KIND='DAILY'
                                          THEN nvl(t.qty,0)
                                      END
                                  ),0)/DW_SUM.GET_PROD_MAPPING_MFG_MOVE(t.prod_name,'CUT','CHIP','T0Cell'),0),
                                  'T1CEL',
                                  round(nvl(sum
                                  (
                                      CASE
                                          WHEN KIND='DAILY'
                                          THEN nvl(t.qty,0)
                                      END
                                  ),0)/DW_SUM.GET_PROD_MAPPING_MFG_MOVE(t.prod_name,'SUBSTRATE','CHIP','T1Cell'),0),      
                                  
                                  nvl(sum
                                  (
                                      CASE
                                          WHEN KIND='DAILY'
                                          THEN nvl(t.qty,0)
                                      END
                                  ),0)                
                                  ) as NEW_IN_PLAN_QTY,
                                  
                                  --MTD_IN_PLAN_QTY      
                                  decode(t.shop,
                                  'T0CEL',
                                  round(nvl(sum
                                  (
                                      CASE
                                          WHEN (SHIFT_DATE <= '{0}') and KIND='DAILY'
                                          THEN nvl(t.qty,0)
                                      END
                                  ),0)/DW_SUM.GET_PROD_MAPPING_MFG_MOVE(t.prod_name,'CUT','CHIP','T0Cell'),0),
                                  'T1CEL',
                                  round(nvl(sum
                                  (
                                      CASE
                                          WHEN (SHIFT_DATE <= '{0}') and KIND='DAILY'
                                          THEN nvl(t.qty,0)
                                      END
                                  ),0)/DW_SUM.GET_PROD_MAPPING_MFG_MOVE(t.prod_name,'SUBSTRATE','CHIP','T1Cell'),0),                
                                  sum
                                  (
                                      CASE
                                          WHEN (SHIFT_DATE <= '{0}') and KIND='DAILY'
                                          THEN nvl(t.qty,0)
                                      END
                                  )
                                  )as MTD_IN_PLAN_QTY  
                                  
                                  from mps_pp t
                                  where t.shift_date like substr('{0}',1,6) || '%'
                                  and t.type = 'IN'
                                  group by t.shop,t.type,t.prod_name      
                            )A
                            group by a.shop,a.type
                    )MPS,
                    (
                            select t.shop,t.data_type,
                            sum
                              (
                                  CASE
                                      WHEN (SHIFT_DATE <= '{0}')
                                      THEN nvl(t.qty,0)
                                  END
                              )as MTD_IN_ACTUAL_QTY  
                            
                            from daily_in_out_sum t
                            where t.shift_date like substr('{0}',1,6) || '%'
                            and t.data_type = 'IN'
                            group by t.shop,t.data_type
                    )D_IN_OUT
                    where MPS.SHOP = D_IN_OUT.SHOP and MPS.TYPE = D_IN_OUT.DATA_TYPE order by shop ";
        sSql_IN = string.Format(sSql_IN, sDate);
        return sSql_IN;
    }

    protected string Get_Output_Grid_SQL()
    {
       
        string sDate = System.DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
        string sSql_OUT = @" select
                            decode(MPS.SHOP,'T1CEL','T1$CEL','T1CF','T1$CF','T2ARY','T2$ARY','T0CEL','C2$CEL','T1ARY','T1$ARY','T2CEL','T2$CEL','T0ARY','C2$ARY','T2CF','T2$CF','C3','C3$C3','C5','C5$C5','FCTP','FCTP$FCTP','LAM','LAM$LAM','WIS','WIS$WIS',MPS.SHOP) as SHOP,
                            to_char( nvl((D_IN_OUT.MTD_OUT_ACTUAL_QTY/  case when MPS.MTD_OUT_PLAN_QTY=0 then 1 else MPS.MTD_OUT_PLAN_QTY end)*100,0),'99990.99' ) || '%' as CVP,
                            MPS.INIT_OUT_PLAN_QTY,
                            MPS.NEW_OUT_PLAN_QTY,
                            (MPS.INIT_OUT_PLAN_QTY-MPS.NEW_OUT_PLAN_QTY) as MPS_DIFF_QTY,
                            MPS.MTD_OUT_PLAN_QTY,
                            D_IN_OUT.MTD_OUT_ACTUAL_QTY,
                            (MPS.MTD_OUT_PLAN_QTY-D_IN_OUT.MTD_OUT_ACTUAL_QTY) as D_IN_OUT_DIFF
                            from
                            (
                                    select A.SHOP,A.TYPE,
                                    sum(A.INIT_OUT_PLAN_QTY) as INIT_OUT_PLAN_QTY,
                                    sum(NEW_OUT_PLAN_QTY) as NEW_OUT_PLAN_QTY,
                                    sum(MTD_OUT_PLAN_QTY) as MTD_OUT_PLAN_QTY
                                    from
                                    (
                                       select t.shop,t.type,t.prod_name,
                                       --INIT_OUT_PLAN_QTY
                                          decode(t.shop,
                                          'T0CEL',
                                          round(nvl(sum
                                          (
                                              CASE
                                                  WHEN KIND='MONTH'
                                                  THEN nvl(t.qty,0)
                                              END
                                          ),0)/DW_SUM.GET_PROD_MAPPING_MFG_MOVE(t.prod_name,'CUT','CHIP','T0Cell'),0),
                                          'T1CEL',
                                          round(nvl(sum
                                          (
                                              CASE
                                                  WHEN KIND='MONTH'
                                                  THEN nvl(t.qty,0)
                                              END
                                          )/DW_SUM.GET_PROD_MAPPING_MFG_MOVE(t.prod_name,'SUBSTRATE','CHIP','T1Cell'),0),0),
                                          nvl(sum
                                          (
                                              CASE
                                                  WHEN KIND='MONTH'
                                                  THEN nvl(t.qty,0)
                                              END
                                          ),0)
                                          
                                          )as INIT_OUT_PLAN_QTY,    
                                          
                                          --NEW_OUT_PLAN_QTY
                                          decode(t.shop,
                                          'T0CEL',
                                          round(nvl(sum
                                          (
                                              CASE
                                                  WHEN KIND='DAILY'
                                                  THEN nvl(t.qty,0)
                                              END
                                          ),0)/DW_SUM.GET_PROD_MAPPING_MFG_MOVE(t.prod_name,'CUT','CHIP','T0Cell'),0),
                                          'T1CEL',
                                          round(nvl(sum
                                          (
                                              CASE
                                                  WHEN KIND='DAILY'
                                                  THEN nvl(t.qty,0)
                                              END
                                          ),0)/DW_SUM.GET_PROD_MAPPING_MFG_MOVE(t.prod_name,'SUBSTRATE','CHIP','T1Cell'),0),      
                                          
                                          nvl(sum
                                          (
                                              CASE
                                                  WHEN KIND='DAILY'
                                                  THEN nvl(t.qty,0)
                                              END
                                          ),0)                
                                          ) as NEW_OUT_PLAN_QTY,
                                          
                                          --MTD_OUT_PLAN_QTY      
                                          decode(t.shop,
                                          'T0CEL',
                                          round(nvl(sum
                                          (
                                              CASE
                                                  WHEN (SHIFT_DATE <= '{0}') and KIND='DAILY'
                                                  THEN nvl(t.qty,0)
                                              END
                                          ),0)/DW_SUM.GET_PROD_MAPPING_MFG_MOVE(t.prod_name,'CUT','CHIP','T0Cell'),0),
                                          'T1CEL',
                                          round(nvl(sum
                                          (
                                              CASE
                                                  WHEN (SHIFT_DATE <= '{0}') and KIND='DAILY'
                                                  THEN nvl(t.qty,0)
                                              END
                                          ),0)/DW_SUM.GET_PROD_MAPPING_MFG_MOVE(t.prod_name,'SUBSTRATE','CHIP','T1Cell'),0),                
                                          sum
                                          (
                                              CASE
                                                  WHEN (SHIFT_DATE <= '{0}') and KIND='DAILY'
                                                  THEN nvl(t.qty,0)
                                              END
                                          )
                                          )as MTD_OUT_PLAN_QTY  
                                          
                                          from mps_pp t
                                          where t.shift_date like substr('{0}',1,6) || '%'
                                          and t.type = 'OUT'
                                          group by t.shop,t.type,t.prod_name      
                                    )A
                                    group by a.shop,a.type
                            )MPS,
                            (
                                    select t.shop,t.data_type,
                                    sum
                                      (
                                          CASE
                                              WHEN (SHIFT_DATE <= '{0}')
                                              THEN nvl(t.qty,0)
                                          END
                                      )as MTD_OUT_ACTUAL_QTY  
                                    
                                    from daily_in_out_sum t
                                    where t.shift_date like substr('{0}',1,6) || '%'
                                    and t.data_type = 'OUT'
                                    group by t.shop,t.data_type
                            )D_IN_OUT
                            where MPS.SHOP = D_IN_OUT.SHOP and MPS.TYPE = D_IN_OUT.DATA_TYPE order by shop ";

        sSql_OUT = string.Format(sSql_OUT, sDate);
        return sSql_OUT;
    }


    protected DataTable RoTation(DataTable dt_IN, DataTable dt_OUT, string sType)
    {
        DataTable TargetTable = new DataTable();
        string columnName = "";

        DataTable dt_defect = dt_IN.DefaultView.ToTable(true, "shop");

        TargetTable.Columns.Add("PRODUCT");
        TargetTable.Columns.Add("IN/OUT");
        TargetTable.Columns.Add("KPI");

        int i = 0;
        //先將橫向的column產生出來
        for (; i < dt_defect.Rows.Count; i++)
            TargetTable.Columns.Add(dt_defect.Rows[i][0].ToString(), typeof(string));

        foreach (DataColumn dc in dt_IN.Columns)
        {
            columnName = dc.ColumnName.ToUpper();
            if ( !columnName.Contains("SHOP") )
            {
                DataRow targetRow = TargetTable.NewRow();

                //targetRow["Product"] = "TFT";
                targetRow["Product"] = (sType == "TFT") ? "TFT" : "SG";
                targetRow["IN/OUT"] = "IN";
                targetRow["KPI"] = dc.ColumnName;
                

                for (int j = 0; j < dt_IN.Rows.Count ; j++)
                {
                    //targetRow[j + 2] = dc.ColumnName;
                    targetRow[j + 3] = dt_IN.Rows[j][dc.ColumnName].ToString();
                }

                TargetTable.Rows.Add(targetRow);
            }
        }

        foreach (DataColumn dc in dt_OUT.Columns)
        {
            columnName = dc.ColumnName.ToUpper();
            if (!columnName.Contains("SHOP"))
            {
                DataRow targetRow = TargetTable.NewRow();

                //targetRow["Product"] = "TFT";
                targetRow["Product"] = (sType == "TFT") ? "TFT" : "SG";
                targetRow["IN/OUT"] = "OUT";
                targetRow["KPI"] = dc.ColumnName;


                for (int j = 0; j < dt_OUT.Rows.Count; j++)
                {
                    //targetRow[j + 2] = dc.ColumnName;
                    targetRow[j + 3] = dt_OUT.Rows[j][dc.ColumnName].ToString();
                }

                TargetTable.Rows.Add(targetRow);
            }
        }

        return TargetTable;

    }

    protected void RG_TFT_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridHeaderItem)
        {
            for (int i = 5; i < e.Item.Cells.Count; i++)
            {
                //因為使用了合併功能，因此e.Item.Cells[i].Text無法找到T1及T2
                string sShop = string.Empty;
                if (i == 5 || i == 6 || i == 7)
                {
                    sShop = "T1" + e.Item.Cells[i].Text;
                }
                else
                {
                    sShop = "T2" + e.Item.Cells[i].Text;
                }
                HtmlAnchor haLot = new HtmlAnchor();
                haLot.HRef = "#";
                haLot.Attributes.Add("Onclick", "DrillDownClick('" + sShop + "'); return false;");
                haLot.InnerHtml = string.Format("<font style=\"font-weight: bold; color: blue\">{0}</font>", e.Item.Cells[i].Text);
                e.Item.Cells[i].Controls.Add(haLot);
                
                //e.Item.Cells[i].Text = "<a href='#' style='text-decoration: underline; font-weight: bold; color: blue;' onclick=\"javascript:CVP_DrillDownClick('" + e.Item.Cells[i].Text + "');\">" + e.Item.Cells[i].Text + "</a>";
            }
        }

        if (e.Item is GridDataItem)
        {
            if (!e.Item.Cells[4].Text.Equals("CVP"))
            {
                

                for (int i = 5; i < e.Item.Cells.Count; i++)
                {
                    e.Item.Cells[i].Text = string.Format("{0:#,##0}", Convert.ToInt32(e.Item.Cells[i].Text));                                     
                }
            }
            else
            {
                for (int i = 5; i < e.Item.Cells.Count; i++)
                {                                       
                    if (Convert.ToDouble(e.Item.Cells[i].Text.Replace("%", "0")) < 100)
                    {
                        e.Item.Cells[i].ForeColor = System.Drawing.Color.Red;
                    }
                    
                }
            }                               
        }
    }
    protected void RG_SG_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridHeaderItem)
        {
            for (int i = 5; i < e.Item.Cells.Count; i++)
            {                
                string sShop = string.Empty;
                if (i == 5 || i == 6)
                {
                    sShop = "T0" + e.Item.Cells[i].Text;
                }
                else
                {
                    sShop = e.Item.Cells[i].Text;
                }
                HtmlAnchor haLot = new HtmlAnchor();
                haLot.HRef = "#";
                haLot.Attributes.Add("Onclick", "DrillDownClick('" + sShop + "'); return false;");
                haLot.InnerHtml = string.Format("<font style=\"font-weight: bold; color: blue\">{0}</font>", e.Item.Cells[i].Text);
                e.Item.Cells[i].Controls.Add(haLot);
            }
        }

        if (e.Item is GridDataItem)
        {
            if (!e.Item.Cells[4].Text.Equals("CVP"))
            {
                for (int i = 5; i < e.Item.Cells.Count; i++)
                {
                    e.Item.Cells[i].Text = string.Format("{0:#,##0}", Convert.ToInt32(e.Item.Cells[i].Text));
                }

            }
            else
            {
                for (int i = 5; i < e.Item.Cells.Count; i++)
                {
                    if (Convert.ToDouble(e.Item.Cells[i].Text.Replace("%", "0")) < 100)
                    {
                        e.Item.Cells[i].ForeColor = System.Drawing.Color.Red;
                    }

                }
            }
        }
    }
}
