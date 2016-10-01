using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.SharePoint;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Microsoft.SharePoint.Mobile.Controls;
using Sadara.DigiCom.ReportFinder.ViewModel;
using System.ComponentModel;

namespace Sadara.DigiCom.ReportFinder.ViewModel
{
    class ExportToExcel
    {
            public static DataTable dataTable;
            public static SPList list;

        public void ExportListToExcel(string _listName)
        {
            #region Export List

            if (!string.IsNullOrEmpty(_listName))
            {
                list = Utility.getWeb().Lists[_listName];

                if (list != null)
                {
                    dataTable = new DataTable();

                    //Adds Columns to SpreadSheet
                    InitializeExcel(list, dataTable);

                    string _schemaXML = list.DefaultView.ViewFields.SchemaXml;

                    if (list.Items != null && list.ItemCount > 0)
                    {
                        foreach (SPListItem _item in list.Items)
                        {
                            DataRow dr = dataTable.NewRow();
                            foreach (DataColumn _column in dataTable.Columns)
                            {
                                if (dataTable.Columns[_column.ColumnName] != null && _item[_column.ColumnName] != null)
                                {
                                    dr[_column.ColumnName] = _item[_column.ColumnName].ToString();
                                }
                            }
                            dataTable.Rows.Add(dr);

                        }
                    }

                }
            }

            System.Web.UI.WebControls.DataGrid grid = new System.Web.UI.WebControls.DataGrid();

            grid.HeaderStyle.Font.Bold = true;
            grid.DataSource = dataTable;
            grid.DataBind();

            using (StreamWriter streamWriter = new StreamWriter(@"C:\Vaibhav\" + list.Title + ".xls", false, Encoding.UTF8))
            {
                using (HtmlTextWriter htmlTextWriter = new HtmlTextWriter(streamWriter))
                {
                    grid.RenderControl(htmlTextWriter);
                }
            }

            Console.WriteLine("File Created");

            #endregion
        }

        public static void InitializeExcel(SPList list, DataTable _datatable)
        {
            if (list != null)
            {
                string _schemaXML = list.DefaultView.ViewFields.SchemaXml;
                if (list.Items != null && list.ItemCount > 0)
                {
                    foreach (SPListItem _item in list.Items)
                    {
                        foreach (SPField _itemField in _item.Fields)
                        {
                            if (_schemaXML.Contains(_itemField.InternalName))
                            {
                                if (_item[_itemField.InternalName] != null)
                                {
                                    if (!_datatable.Columns.Contains(_itemField.InternalName))
                                    {
                                        _datatable.Columns.Add(new DataColumn(_itemField.StaticName, Type.GetType("System.String")));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /*private static Table GetListTableControl(string strListName)
        {
            Table tblListView = new Table();
            tblListView.ID = "_tblListView";
            tblListView.BorderStyle = BorderStyle.Solid;
            tblListView.BorderWidth = Unit.Pixel(1);
            tblListView.BorderColor = Color.Silver;

            using (SPSite site = new SPSite(strListURL.Trim()))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[strListName.Trim()];

                    SPView wpView = list.Views["All Items"];
                    wpView.RowLimit = 2147483647;

                    SPQuery query = new SPQuery(wpView);
                    SPListItemCollection items = list.GetItems(query);

                    if (items != null && items.Count > 0)
                    {
                        DataTable tbListViewData = items.GetDataTable();
                        DataView dvListViewData = tbListViewData.DefaultView;
                        if (dvListViewData != null && dvListViewData.Count > 0)
                        {
                            tblListView.Rows.Add(new TableRow());
                            tblListView.Rows[0].BackColor = Color.Gainsboro;
                            tblListView.Rows[0].Font.Bold = true;

                            for (int i = 0; i < wpView.ViewFields.Count; i++)
                            {
                                tblListView.Rows[0].Cells.Add(new TableCell());
                                tblListView.Rows[0].Cells[i].Text = list.Fields.GetFieldByInternalName(wpView.ViewFields[i].ToString()).Title;
                            }

                            for (int i = 0; i < dvListViewData.Count; i++)
                            {
                                tblListView.Rows.Add(new TableRow());

                                for (int j = 0; j < wpView.ViewFields.Count; j++)
                                {
                                    tblListView.Rows[i + 1].Cells.Add(new TableCell());

                                    if (tbListViewData.Columns.Contains(wpView.ViewFields[j].ToString()))
                                    {
                                        tblListView.Rows[i + 1].Cells[j].Text = dvListViewData[i][wpView.ViewFields[j].ToString()].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            } return tblListView;
        }*/
    }
}
