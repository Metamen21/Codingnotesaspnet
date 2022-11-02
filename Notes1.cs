 DataTable dtTable = new DataTable();
            dtTable = (DataTable)ViewState["dtApprovalTable"];

            //Check if this is duplicate
            DataRow dtRow = dtTable.Select("StaffID=" + ddlApprovalStaffList.SelectedValue).FirstOrDefault();
            if (dtRow != null)
            {
                ShowMessage("This staff is already present in the Aprroval List!", "Error");
                return;
            }
            
            
