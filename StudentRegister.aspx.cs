using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace erpSoftTickets.Secured
{
    public partial class StudentRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CreateDataTable();
                PopulateStudentRegister();
                ClearFields();

            }
        }
        private void CreateDataTable()
        {
            DataTable dtTable = new DataTable();
            dtTable.Columns.Add("RollNo", typeof(long));
            dtTable.Columns.Add("Name", typeof(string));
            dtTable.Columns.Add("Gender", typeof(string));
            dtTable.Columns.Add("ClassName", typeof(string));
            ViewState["dtStudentTable"] = dtTable;

        }


        protected void lnkbtnDisplayData_Click(object sender, EventArgs e)
        {
            //Create a blank DataTable
            CreateDataTable();
            DataTable dtTable = new DataTable();
            
            
            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["CampusERPConnectionString"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                SqlCommand objCmd;
                objCmd = new SqlCommand(@"SELECT RollNo, Name, Gender, ClassName
                                            FROM   ClassMaster INNER JOIN  StudentMaster ON ClassMaster.ClassID = StudentMaster.ClassID
                                            Where RollNo=@RollNo;", sqlCon);
                objCmd.Parameters.Add("@RollNo", SqlDbType.BigInt).Value = hdRollNo.Value;
                try
                {
                    sqlCon.Open();
                    SqlDataReader rdr = objCmd.ExecuteReader();
                    rdr.Read();
                    txtName.Text = rdr["Name"].ToString();
                    txtRollNo.Text = hdRollNo.Value.ToString();
                    ddlGender.Text = rdr["Gender"].ToString();
                    ddlClass.Text = rdr["ClassName"].ToString();
                    rdr.Close();
                    sqlCon.Close();
                }
                catch (Exception ex)
                {
                    ShowMessage("Unable to populate Ticket Details: " + ex.Message, "Error");
                }
            }

        }
       
        private void PopulateStudentRegister()
        {


            DataTable dtRegister = new DataTable();
            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["CampusERPConnectionString"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                SqlCommand objCmd;
                objCmd = new SqlCommand(@"SELECT  RollNo, Name, Gender, ClassName
                                            FROM   ClassMaster INNER JOIN
                                         StudentMaster ON ClassMaster.ClassID = StudentMaster.ClassID;", sqlCon);
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
                    ShowMessage("Unable to load Student Register List : " + ex.Message, "Error");
                }
            }
        }

      
        protected void ShowMessage(string Message, string MessageType)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + MessageType + "');", true);
        }
        private void ClearFields()
        {
            txtName.Text = "";
            txtRollNo.Text = "";
            ddlClass.ClearSelection();
            ddlGender.ClearSelection();

            lnkbtnDelete.Visible = false;
            lnkbtnNew.Visible = false;
            lnkbtnUpdate.Visible = false;
            lnkbtnSave.Visible = true;
            hdRollNo.Value = "0";
        }
        protected void lnkbtnNew_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtRollNo.Text = "";
            ddlClass.ClearSelection();
            ddlGender.ClearSelection();
            hdRollNo.Value = "0";
            lnkbtnDelete.Visible = false;
            lnkbtnNew.Visible = false;
            lnkbtnUpdate.Visible = false;
            lnkbtnSave.Visible = true;
            
        }

        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["CampusERPConnectionString"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                SqlCommand objCmd,objQuery;
                objCmd = new SqlCommand(@"INSERT INTO StudentMaster 
                         ( Name , ClassID ,  Gender)
                         VALUES  ( @Name , @ClassID ,  @Gender);", sqlCon);
                objCmd.Parameters.Add("@ClassID", SqlDbType.BigInt).Value = ddlClass.SelectedValue;
                objCmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = txtName.Text;
                objCmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = ddlGender.SelectedValue;
                objQuery = new SqlCommand(@"Select MAX(RollNo) from StudentMaster;", sqlCon);
                

                try
                {
                    sqlCon.Open();
                    objCmd.ExecuteNonQuery();
                    SqlDataReader rdr = objQuery.ExecuteReader();
                    rdr.Read();
                    txtRollNo.Text = rdr[0].ToString();
                    hdRollNo.Value = txtRollNo.Text;
                    sqlCon.Close();
                    ShowMessage("Record Successfully Saved!", "Success");
                  

                     
                    lnkbtnDelete.Visible = true;
                    lnkbtnNew.Visible = true;
                    lnkbtnUpdate.Visible = true;
                    lnkbtnSave.Visible = false;
                    PopulateStudentRegister();
                }
                catch (Exception ex)
                {
                    ShowMessage("ERROR: " + ex.Message, "Error");
                }
            }
        }

        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["CampusERPConnectionString"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                SqlCommand objCmd;
                objCmd = new SqlCommand(@"UPDATE   StudentMaster 
                                           SET  Name=@Name, Gender=@Gender, ClassID=@ClassID where RollNo=@RollNo", sqlCon);
                objCmd.Parameters.Add("@RollNo", SqlDbType.BigInt).Value = hdRollNo.Value;
                objCmd.Parameters.Add("@ClassID", SqlDbType.BigInt).Value = ddlClass.SelectedValue;
                objCmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = txtName.Text;
                objCmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = ddlGender.SelectedValue;
                try
                {
                    sqlCon.Open();
                    objCmd.ExecuteNonQuery();
                    sqlCon.Close();

                    ShowMessage("Record Successfully Updated!", "Success");
                    

                     
                    lnkbtnDelete.Visible = true;
                    lnkbtnNew.Visible = true;
                    lnkbtnUpdate.Visible = true;
                    lnkbtnSave.Visible = false;
                    PopulateStudentRegister();
                }
                catch (Exception ex)
                {
                    ShowMessage("Unable to Update Student Register  : " + ex.Message, "Error");
                }
            }
        }
        
        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["CampusERPConnectionString"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                SqlCommand objCmd;
                objCmd = new SqlCommand(@"Delete From StudentMaster
                                             Where RollNo=@RollNo;", sqlCon);
                objCmd.Parameters.Add("@RollNo", SqlDbType.BigInt).Value = txtRollNo.Text;

                try
                {
                    sqlCon.Open();
                    objCmd.ExecuteNonQuery();
                    sqlCon.Close();

                    ShowMessage("Record Successfully Deleted!", "Success");
                    PopulateStudentRegister();

                     
                    lnkbtnDelete.Visible = false;
                    lnkbtnNew.Visible = true;
                    lnkbtnUpdate.Visible = false;
                    lnkbtnSave.Visible = false;
                     //clear fields
                    ClearFields();
                }
                catch (Exception ex)
                {
                    ShowMessage("Unable to Update Student Register  : " + ex.Message, "Error");
                }
            }
        }

        protected void lstRegister_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["CampusERPConnectionString"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                SqlCommand objCmd;
                objCmd = new SqlCommand(@"Delete From StudentMaster
                                             Where RollNo=@RollNo;", sqlCon);
                objCmd.Parameters.Add("@RollNo", SqlDbType.BigInt).Value = hdRollNo.Value;
                
                try
                {
                    sqlCon.Open();
                    objCmd.ExecuteNonQuery();
                    sqlCon.Close();

                    ShowMessage("Record Successfully Deleted!", "Success");
                    PopulateStudentRegister();

                    ClearFields();

                }
                catch (Exception ex)
                {
                    ShowMessage("Unable to Update Student Register  : " + ex.Message, "Error");
                }
            }
        }

        protected void lstRegister_ItemEditing(object sender, ListViewEditEventArgs e)
        {
          
            
            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["CampusERPConnectionString"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                SqlCommand objCmd;
                objCmd = new SqlCommand(@"SELECT RollNo, Name, Gender, ClassID
                                            FROM     StudentMaster
                                            Where RollNo=@RollNo;", sqlCon);
                objCmd.Parameters.Add("@RollNo", SqlDbType.BigInt).Value = hdRollNo.Value;
                try
                {
                    sqlCon.Open();
                    SqlDataReader rdr = objCmd.ExecuteReader();
                    rdr.Read();
                    txtName.Text = rdr["Name"].ToString();
                    txtRollNo.Text = hdRollNo.Value.ToString();
                    ddlGender.SelectedValue = rdr["Gender"].ToString();
                    ddlClass.SelectedValue = rdr["ClassID"].ToString();
                    rdr.Close();
                    sqlCon.Close();

                     
                    lnkbtnDelete.Visible = true;
                    lnkbtnNew.Visible = true;
                    lnkbtnUpdate.Visible = true;
                    lnkbtnSave.Visible = false;
                    
                }
                catch (Exception ex)
                {
                    ShowMessage("Unable to populate Register: " + ex.Message, "Error");
                }
            }

   


        }
    }
}