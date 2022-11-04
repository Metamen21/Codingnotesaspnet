<%@ Page Title="Student Register" Language="C#" MasterPageFile="~/Secured/MasterPage.Master" AutoEventWireup="true" CodeBehind="StudentRegister.aspx.cs" Inherits="erpSoftTickets.Secured.StudentRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
        <ContentTemplate>
            <section class="content-header">
                <h1>Student Register</h1>
            </section>

            <!-- Main content -->
            <section class="content">
                <div class="messagealert col-lg-8 col-md-8 col-xs-11 col-sm-11" id="alert_container"></div>
                <asp:HiddenField ID="hdRollNo" runat="server" />
                <asp:LinkButton ID="lnkbtnDisplayData" runat="server" OnClick="lnkbtnDisplayData_Click"></asp:LinkButton>

                <div class="box box-primary">
                    <div class="box-body">
                        <div class="row">
                            <div class="record">
                                <div class="col-md-4">
                                    <div class="form-group has-feedback">
                                        <label>Roll No</label>
                                        <asp:TextBox ID="txtRollNo" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group has-feedback">
                                        <label>Name</label>
                                        <asp:TextBox ID="txtName" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="record">
                                <div class="col-md-4">
                                    <div class="form-group has-feedback">
                                        <label>Gender</label>
                                        <asp:DropDownList ID="ddlGender" runat="server" class="form-control">
                                            <asp:ListItem Text="Male" />
                                            <asp:ListItem Text="Female" />
                                            <asp:ListItem Text="Other" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group has-feedback">
                                        <label>Class</label>
                                        <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control" DataSourceID="SqlDataSource1" DataTextField="ClassName" DataValueField="ClassID">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CampusERPConnectionString %>" SelectCommand="SELECT [ClassID], [ClassName] FROM [ClassMaster]"></asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <asp:LinkButton ID="lnkbtnNew" runat="server" Text="New" CssClass="btn btn-default" OnClick="lnkbtnNew_Click"  Visible="true"/>
                                <asp:LinkButton ID="lnkbtnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="lnkbtnSave_Click" />
                                <asp:LinkButton ID="lnkbtnUpdate" runat="server" Text="Update" CssClass="btn btn-warning" OnClick="lnkbtnUpdate_Click" Visible="false"/>
                                <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" 
                                    OnClientClick="return getConfirmation(this, 'Please confirm','Are you sure you want to delete?');"
                                    OnClick="lnkbtnDelete_Click" Visible="false"/>
                            </div>
                            <div class="col-md-12">

                                <div id="no-more-tables">
                                    <table id="myTable" class="table-hover table-bordered  table-responsive table-striped responsiveTable" style="width: 100%;">
                                        <asp:ListView ID="lstRegister" runat="server"
                                            OnItemDeleting="lstRegister_ItemDeleting"
                                            OnItemEditing="lstRegister_ItemEditing"
                                            DataKeyNames="RollNo">
                                            <LayoutTemplate>
                                                <thead>
                                                    <tr style="cursor: pointer">
                                                        <th><i style="font-size: 20px" class="fa fa-close"></th>
                                                        <th><i style="font-size: 20px" class="fa fa-edit"></th>
                                                        <th style="width: 50px;">Sr.no</th>
                                                        <th>Student Name</th>
                                                        <th>Gender</th>
                                                        <th>Class</th>
                                                    </tr>
                                                </thead>
                                                <div id="itemplaceholder" runat="server">
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr style="cursor: pointer" onclick="DisplayData('<%# DataBinder.Eval(Container.DataItem, "RollNo")%>');">
                                                    <td>
                                                        <asp:LinkButton ToolTip="Remove" CssClass="fa fa-close" Style="font-size: 20px"
                                                            ForeColor="Red" runat="server" ID="imgDelete"
                                                            OnClientClick="return getConfirmation(this, 'Please confirm','Are you sure you want to delete?');"
                                                            CommandName="Delete"> </asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ToolTip="Edit" CssClass="fa fa-edit" Style="font-size: 20px"
                                                            ForeColor="Black" runat="server" ID="imgEdit"
                                                            CommandName="Edit"> </asp:LinkButton>
                                                    </td>
                                                    <td style="width: 8px;"><%#Container.DataItemIndex+1 %></td>
                                                    <td><%# Eval("Name")%></td>
                                                    <td><%# Eval("Gender")%></td>
                                                    <td><%# Eval("ClassName")%>  </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--Model Popup for Delete Confirmation--%>
    <div id="modalPopUp" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">
                        <span id="spnTitle"></span>
                    </h4>
                </div>
                <div class="modal-body">
                    <p>
                        <span id="spnMsg"></span>.
                    </p>
                </div>
                <div class="modal-footer">
                    <div id="btnConfirm" class="btn btn-danger">Yes</div>
                    <div class="btn btn-default" data-dismiss="modal">Close</div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
    <script>
        function DisplayData(RollNo) {
            document.getElementById("<%=hdRollNo.ClientID%>").value = RollNo;
            document.getElementById("<%=lnkbtnDisplayData.ClientID%>").click();
        }
    </script>


    <%--SCRIPT FOR DELETE CONFIRMATION --%>
    <script type="text/javascript">
        //script for confirm yes/no box
        function getConfirmation(sender, title, message) {
            $("#spnTitle").text(title);
            $("#spnMsg").text(message);
            $('#modalPopUp').modal('show');
            $('#btnConfirm').attr('onclick', "$('#modalPopUp').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
            return false;
        }
    </script>
</asp:Content>
