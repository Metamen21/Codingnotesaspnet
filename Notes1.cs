 DataTable dtTable = new DataTable();
            dtTable = (DataTable)ViewState["dtApprovalTable"];

            //Check if this is duplicate
            DataRow dtRow = dtTable.Select("StaffID=" + ddlApprovalStaffList.SelectedValue).FirstOrDefault();
            if (dtRow != null)
            {
                ShowMessage("This staff is already present in the Aprroval List!", "Error");
                return;
            }
            


SELECT * FROM [dbo].[BloodGroup]
ORDER BY [BLOODGROUPID]
OFFSET 5 ROWS
FETCH  NEXT 3 ROWS ONLY;

DECLARE @PageNumber AS INT
DECLARE @RowsOfPage AS INT
DECLARE @SortingCol AS VARCHAR(100) ='FruitName'
DECLARE @SortType AS VARCHAR(100) = 'DESC'
SET @PageNumber=1
SET @RowsOfPage=4
SELECT FruitName,Price FROM SampleFruits
ORDER BY 
CASE WHEN @SortingCol = 'Price' AND @SortType ='ASC' THEN Price END ,
CASE WHEN @SortingCol = 'Price' AND @SortType ='DESC' THEN Price END DESC,
CASE WHEN @SortingCol = 'FruitName' AND @SortType ='ASC' THEN FruitName END ,
CASE WHEN @SortingCol = 'FruitName' AND @SortType ='DESC' THEN FruitName END DESC
OFFSET (@PageNumber-1)*@RowsOfPage ROWS
FETCH NEXT @RowsOfPage ROWS ONLY

            
