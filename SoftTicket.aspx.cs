using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace erpSoftTickets.Secured
{
    public partial class SoftTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["StaffID"] == null)
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.User = null;
                System.Web.Security.FormsAuthentication.SignOut();
                Response.Redirect("~/default.aspx", true);
                return;
            }

            if (!Page.IsPostBack)
            {
                txtTicketDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                PopulateTicketForDropDownList();
                PopulateInstituteStaff();
                CreateDataTable();
                PopulateTicketRegister();
            }
        }

        private void CreateDataTable()
        {
            DataTable dtTable = new DataTable();
            dtTable.Columns.Add("StaffID", typeof(long));
            dtTable.Columns.Add("ERPStaffID", typeof(string));
            dtTable.Columns.Add("StaffName", typeof(string));
            dtTable.Columns.Add("Date", typeof(string));
            dtTable.Columns.Add("Status", typeof(string));
            dtTable.Columns.Add("Remarks", typeof(string));
            ViewState["dtApprovalTable"] = dtTable;

        }
        protected void ShowMessage(string Message, string MessageType)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + MessageType + "');", true);
        }
        private void PopulateTicketForDropDownList()
        {
            ddlTicketFor.Items.Clear();
            switch (ddlProject.SelectedValue)
            {
                case "CampusERP":
                case "DentalHIS":
                case "AyurvedHIS":
                case "HomoeopathyHIS":
                    ddlTicketFor.Items.Add("New Development");
                    ddlTicketFor.Items.Add("Modification Required");
                    ddlTicketFor.Items.Add("Training Required");
                    ddlTicketFor.Items.Add("Error Correction");
                    break;
                case "Website":
                    ddlTicketFor.Items.Add("New Page Development");
                    ddlTicketFor.Items.Add("Content Updation");
                    ddlTicketFor.Items.Add("Event Addition");
                    ddlTicketFor.Items.Add("Design Updation");
                    ddlTicketFor.Items.Add("Spelling Correction");
                    ddlTicketFor.Items.Add("Other Correction");
                    break;
                case "Emails":
                    ddlTicketFor.Items.Add("New Email");
                    ddlTicketFor.Items.Add("Remove Email");
                    ddlTicketFor.Items.Add("Suspend Email");
                    ddlTicketFor.Items.Add("Correction In Email");
                    break;
            }
        }
        private void PopulateInstituteStaff()
        {
            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["CampusERPConnectionString"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                SqlCommand objCmd;
                objCmd = new SqlCommand(@"SELECT StaffID, ERPStaffID + '-' + [Designation] + ' - ' + [StaffName] As StaffName
                    FROM [dbo].[InstituteMasterStaff];", sqlCon);
                try
                {
                    sqlCon.Open();
                    SqlDataReader rdr = objCmd.ExecuteReader();
                    ddlApprovalStaffList.DataValueField = "StaffID";
                    ddlApprovalStaffList.DataTextField = "StaffName";
                    ddlApprovalStaffList.DataSource = rdr;
                    ddlApprovalStaffList.DataBind();
                    rdr.Close();

                    sqlCon.Close();
                }
                catch (Exception ex)
                {
                    ShowMessage("Unable to load Approval Staff List : " + ex.Message, "Error");
                }
            }
        }
        private void PopulateTicketRegister()
        {
            DataTable dtRegister = new DataTable();
            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["CampusERPConnectionString"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                SqlCommand objCmd;
                objCmd = new SqlCommand(@"SELECT EntryID,TicketNo, TicketDate, Project, TicketFor, Status
                    FROM   SoftTickets
                    WHERE StaffID=@StaffID;", sqlCon);
                objCmd.Parameters.Add("@StaffID", SqlDbType.BigInt).Value = Session["StaffID"].ToString();
                try
                {
                    sqlCon.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter(objCmd);
                    sqlDA.Fill(dtRegister);
                    sqlDA.Dispose();
                    sqlCon.Close();

                    lstRegister.DataSource = dtRegister;
                    lstRegister.DataBind();
                }
                catch (Exception ex)
                {
                    ShowMessage("Unable to load Approval Staff List : " + ex.Message, "Error");
                }
            }
        }


        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateTicketForDropDownList();
        }


        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["CampusERPConnectionString"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                SqlCommand objCmd;
                objCmd = new SqlCommand(@"INSERT INTO SoftTickets
                    (FinYear, TicketNo, TicketDate, Project, TicketFor, IsApprovalRequired, InstituteID,
	                    StaffID, StaffName, PageURL, Description, Status)
                    VALUES(@FinYear, (SELECT ISNULL(MAX(TicketNo),0)+1 FROM SoftTickets), GETDATE(), 
                        @Project, @TicketFor, @IsApprovalRequired, 
                        @InstituteID,	@StaffID, @StaffName, @PageURL, @Description, @Status);
                    SELECT MAX(TicketNo) FROM SoftTickets;
                    SELECT MAX(EntryID) FROM SoftTickets;", sqlCon);
                objCmd.Parameters.Add("@FinYear", SqlDbType.VarChar).Value = Session["FinYear"];
                objCmd.Parameters.Add("@Project", SqlDbType.VarChar).Value = ddlProject.SelectedValue;
                objCmd.Parameters.Add("@TicketFor", SqlDbType.VarChar).Value = ddlTicketFor.SelectedValue;
                objCmd.Parameters.Add("@IsApprovalRequired", SqlDbType.Bit).Value = chkIsApprovalRequired.Checked;
                objCmd.Parameters.Add("@InstituteID", SqlDbType.Int).Value = Session["InstituteID"];
                objCmd.Parameters.Add("@StaffID", SqlDbType.BigInt).Value = Session["StaffID"];
                objCmd.Parameters.Add("@StaffName", SqlDbType.VarChar).Value = Session["UserName"];
                objCmd.Parameters.Add("@PageURL", SqlDbType.VarChar).Value = txtPageURL.Text;
                objCmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = txtDescription.Text;
                objCmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "Pending";
                try
                {
                    sqlCon.Open();
                    SqlDataReader rdr = objCmd.ExecuteReader();
                    rdr.Read();
                    txtTicketNo.Text = rdr[0].ToString();

                    rdr.NextResult();
                    rdr.Read();
                    hdEntryID.Value = rdr[0].ToString();
                    rdr.Close();

                    //Now Save the Approval List
                    if (chkIsApprovalRequired.Checked)
                    {
                        DataTable dtTable = new DataTable();
                        dtTable = (DataTable)ViewState["dtApprovalTable"];

                        //Now loop through all rows and save the data in Database
                        int Counter = 1;
                        foreach (DataRow row in dtTable.Rows)
                        {
                            objCmd = new SqlCommand(@"INSERT INTO SoftTicketsApprovals
                                (TicketEntryID, StaffID, ERPStaffID, StaffName, Status)
                                VALUES(@TicketEntryID, @StaffID, @ERPStaffID, @StaffName, @Status);", sqlCon);
                            objCmd.Parameters.Add("@TicketEntryID", SqlDbType.BigInt).Value = hdEntryID.Value;
                            objCmd.Parameters.Add("@StaffID", SqlDbType.BigInt).Value = row["StaffID"].ToString();
                            objCmd.Parameters.Add("@ERPStaffID", SqlDbType.VarChar).Value = row["ERPStaffID"].ToString();
                            objCmd.Parameters.Add("@StaffName", SqlDbType.VarChar).Value = row["StaffName"].ToString();
                            if (Counter == 1)
                                objCmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "Pending";
                            else
                                objCmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = DBNull.Value;

                            objCmd.ExecuteNonQuery();

                            Counter++;
                        }
                    }



                    sqlCon.Close();
                    ShowMessage("Record Successfully saved!", "Success");
                }
                catch (Exception ex)
                {
                    ShowMessage("ERROR: " + ex.Message, "Error");
                }
            }
        }

        protected void lnkbtnAddApprovingStaff_Click(object sender, EventArgs e)
        {
            DataTable dtTable = new DataTable();
            dtTable = (DataTable)ViewState["dtApprovalTable"];

            //Check if this is duplicate
            DataRow dtRow = dtTable.Select("StaffID=" + ddlApprovalStaffList.SelectedValue).FirstOrDefault();
            if (dtRow != null)
            {
                ShowMessage("This staff is already present in the Aprroval List!", "Error");
                return;
            }


            dtRow = dtTable.NewRow();
            //Value assigning
            dtRow["StaffID"] = ddlApprovalStaffList.SelectedValue;
            string strERPStaffID = ddlApprovalStaffList.SelectedItem.Text;
            int indexPos = strERPStaffID.IndexOf('-');
            strERPStaffID = strERPStaffID.Substring(0, indexPos);
            dtRow["ERPStaffID"] = strERPStaffID;
            string strStaffName = ddlApprovalStaffList.SelectedItem.Text;
            strStaffName = strStaffName.Substring(indexPos + 1);
            dtRow["StaffName"] = strStaffName;
            dtRow["Date"] = "";
            dtRow["Status"] = "";
            dtRow["Remarks"] = "";
            dtTable.Rows.Add(dtRow);

            lstApprovalStaffList.DataSource = dtTable;
            lstApprovalStaffList.DataBind();
            ViewState["dtApprovalTable"] = dtTable;
        }

protected void lnkbtnDisplayData_Click(object sender, EventArgs e)
{
            //Create a blank DataTable
            CreateDataTable();
            DataTable dtTable = new DataTable();
            dtTable = (DataTable)ViewState["dtApprovalTable"];

            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["CampusERPConnectionString"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                SqlCommand objCmd;
                objCmd = new SqlCommand(@"SELECT EntryID,TicketNo, TicketDate, Project, TicketFor, Status,IsApprovalRequired,PageURL,Description
                    FROM   SoftTickets
                    WHERE EntryID=@EntryID;", sqlCon);
                objCmd.Parameters.Add("@EntryID", SqlDbType.BigInt).Value = hdEntryID.Value;
                try
                {
                    sqlCon.Open();
                    SqlDataReader rdr = objCmd.ExecuteReader();
                    rdr.Read();
                    txtTicketNo.Text = rdr["TicketNo"].ToString();
                    txtTicketDate.Text = rdr["TicketDate"].ToString();
                    ddlProject.SelectedValue = rdr["Project"].ToString();
                    ddlTicketFor.SelectedValue = rdr["TicketFor"].ToString();
                    chkIsApprovalRequired.Checked = Convert.ToBoolean(rdr["IsApprovalRequired"]);
                    txtPageURL.Text = rdr["PageURL"].ToString();
                    txtDescription.Text = rdr["Description"].ToString();
                    string Status = rdr["Status"].ToString();
                    rdr.Close();


                    objCmd = new SqlCommand(@"SELECT StaffID, ERPStaffID, StaffName, ApprovalDate, Status, Remarks
                        FROM   SoftTicketsApprovals
                        WHERE TicketEntryID=@TicketEntryID;", sqlCon);
                    objCmd.Parameters.Add("@TicketEntryID", SqlDbType.BigInt).Value = hdEntryID.Value;
                    rdr = objCmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow dtRow = dtTable.NewRow();
                            //Value assigning
                            dtRow["StaffID"] = rdr["StaffID"];
                            dtRow["ERPStaffID"] = rdr["ERPStaffID"];
                            dtRow["StaffName"] = rdr["StaffName"];
                            dtRow["Date"] = rdr["ApprovalDate"];
                            dtRow["Status"] = rdr["Status"];
                            dtRow["Remarks"] = rdr["Remarks"];
                            dtTable.Rows.Add(dtRow);
                        }
                    }
                    rdr.Close();
                    sqlCon.Close();


                    ViewState["dtApprovalTable"] = dtTable;
                    lstApprovalStaffList.DataSource = dtTable;
                    lstApprovalStaffList.DataBind();

                    lnkbtnSave.Visible = false;
                    if (Status == "Pending")
                    {
                        lnkbtnUpdate.Visible = true;
                        lnkbtnDelete.Visible = true;
                    }
                    else
                    {
                        lnkbtnUpdate.Visible = false;
                        lnkbtnDelete.Visible = false;
                    }
                    lnkbtnNew.Visible = true;


                }
                catch (Exception ex)
                {
                    ShowMessage("Unable to populate Ticket Details: " + ex.Message, "Error");
                }
            }
        }

        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["CampusERPConnectionString"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                SqlCommand objCmd;
                objCmd = new SqlCommand(@"UPDATE SoftTickets
                    SET FinYear=@FinYear, TicketDate= GETDATE(), Project=@Project, TicketFor=@TicketFor, 
                    IsApprovalRequired=@IsApprovalRequired, PageURL=@PageURL, Description=@Description
                    WHERE EntryID=@EntryID;", sqlCon);
                objCmd.Parameters.Add("@FinYear", SqlDbType.VarChar).Value = Session["FinYear"];
                objCmd.Parameters.Add("@Project", SqlDbType.VarChar).Value = ddlProject.SelectedValue;
                objCmd.Parameters.Add("@TicketFor", SqlDbType.VarChar).Value = ddlTicketFor.SelectedValue;
                objCmd.Parameters.Add("@IsApprovalRequired", SqlDbType.Bit).Value = chkIsApprovalRequired.Checked;
                objCmd.Parameters.Add("@PageURL", SqlDbType.VarChar).Value = txtPageURL.Text;
                objCmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = txtDescription.Text;
                objCmd.Parameters.Add("@EntryID", SqlDbType.BigInt).Value = hdEntryID.Value;
                try
                {
                    sqlCon.Open();
                    objCmd.ExecuteNonQuery();

                    //Now delete all approvals
                    objCmd = new SqlCommand(@"DELETE FROM SoftTicketsApprovals
                    WHERE TicketEntryID=@TicketEntryID;", sqlCon);
                    objCmd.Parameters.Add("@TicketEntryID", SqlDbType.BigInt).Value = hdEntryID.Value;
                    objCmd.ExecuteNonQuery();

                    //Now Save the Approval List
                    if (chkIsApprovalRequired.Checked)
                    {
                        DataTable dtTable = new DataTable();
                        dtTable = (DataTable)ViewState["dtApprovalTable"];

                        //Now loop through all rows and save the data in Database
                        int Counter = 1;
                        foreach (DataRow row in dtTable.Rows)
                        {
                            objCmd = new SqlCommand(@"INSERT INTO SoftTicketsApprovals
                                (TicketEntryID, StaffID, ERPStaffID, StaffName, Status)
                                VALUES(@TicketEntryID, @StaffID, @ERPStaffID, @StaffName, @Status);", sqlCon);
                            objCmd.Parameters.Add("@TicketEntryID", SqlDbType.BigInt).Value = hdEntryID.Value;
                            objCmd.Parameters.Add("@StaffID", SqlDbType.BigInt).Value = row["StaffID"].ToString();
                            objCmd.Parameters.Add("@ERPStaffID", SqlDbType.VarChar).Value = row["ERPStaffID"].ToString();
                            objCmd.Parameters.Add("@StaffName", SqlDbType.VarChar).Value = row["StaffName"].ToString();
                            if (Counter == 1)
                                objCmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "Pending";
                            else
                                objCmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = DBNull.Value;

                            objCmd.ExecuteNonQuery();

                            Counter++;
                        }
                    }
                    sqlCon.Close();

                    lnkbtnSave.Visible = false;
                    lnkbtnUpdate.Visible = true;
                    lnkbtnDelete.Visible = true;
                    lnkbtnNew.Visible = true;
                    ShowMessage("Record Successfully Updated!", "Success");
                }
                catch (Exception ex)
                {
                    ShowMessage("ERROR: " + ex.Message, "Error");
                }
            }
        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["CampusERPConnectionString"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                SqlCommand objCmd;
                objCmd = new SqlCommand(@"DELETE FROM SoftTicketsApprovals
                    WHERE TicketEntryID=@TicketEntryID;
                DELETE FROM SoftTickets
                    WHERE EntryID=@EntryID;", sqlCon);
                objCmd.Parameters.Add("@EntryID", SqlDbType.BigInt).Value = hdEntryID.Value;
                objCmd.Parameters.Add("@TicketEntryID", SqlDbType.BigInt).Value = hdEntryID.Value;
                try
                {
                    sqlCon.Open();
                    objCmd.ExecuteNonQuery();
                    sqlCon.Close();

                    lnkbtnSave.Visible = false;
                    lnkbtnUpdate.Visible = false;
                    lnkbtnDelete.Visible = false;
                    lnkbtnNew.Visible = true;
                    lnkbtnCancel.Visible = false;

                    txtDescription.Text = "";
                    txtPageURL.Text = "";
                    txtTicketDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtTicketNo.Text = "";
                    hdEntryID.Value = "0";

                    ShowMessage("Record Successfully Deleted!", "Success");
                }
                catch (Exception ex)
                {
                    ShowMessage("ERROR: " + ex.Message, "Error");
                }
            }
        }

        protected void lnkbtnNew_Click(object sender, EventArgs e)
        {
            lnkbtnNew.Visible = false;
            lnkbtnSave.Visible = true;
            lnkbtnCancel.Visible = true;
            lnkbtnUpdate.Visible = false;
            lnkbtnDelete.Visible = false;

            txtDescription.Text = "";
            txtPageURL.Text = "";
            txtTicketDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtTicketNo.Text = "";
            hdEntryID.Value = "0";


            CreateDataTable();
            lstApprovalStaffList.DataSource = null;
            lstApprovalStaffList.DataBind();
        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            lnkbtnNew.Visible = true;
            lnkbtnSave.Visible = false;
            lnkbtnCancel.Visible = false;
            lnkbtnUpdate.Visible = false;
            lnkbtnDelete.Visible = false;

            txtDescription.Text = "";
            txtPageURL.Text = "";
            txtTicketDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtTicketNo.Text = "";
            hdEntryID.Value = "0";

        }

        protected void lstApprovalStaffList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            string StaffID = lstApprovalStaffList.DataKeys[e.ItemIndex].Value.ToString();
            DataTable dtTable = new DataTable();
            dtTable = (DataTable)ViewState["dtApprovalTable"];

            DataRow dtRow = dtTable.Select("StaffID=" + StaffID).FirstOrDefault();
            if (dtRow != null)
            {
                dtRow.Delete();
                dtTable.AcceptChanges();
            }

            lstApprovalStaffList.DataSource = dtTable;
            lstApprovalStaffList.DataBind();
            ViewState["dtApprovalTable"] = dtTable;

        }
    }
}