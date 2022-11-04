<%@ Page Title="" Language="C#" MasterPageFile="~/Secured/MasterPage.Master" AutoEventWireup="true" CodeBehind="SoftTicket.aspx.cs" Inherits="erpSoftTickets.Secured.SoftTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
        <ContentTemplate>
            <section class="content-header">
                <h1>Soft Tickets</h1>
            </section>
            <!-- Main content -->
            <section class="content">
                <div class="messagealert col-lg-8 col-md-8 col-xs-11 col-sm-11" id="alert_container"></div>
                <asp:HiddenField ID="hdEntryID" runat="server" />
                <%--<asp:Button ID="btnDisplayData" runat="server" Style="display: none" OnClick="btnDisplayData_Click" />--%>
                <asp:LinkButton ID="lnkbtnDisplayData" runat="server" OnClick="lnkbtnDisplayData_Click"></asp:LinkButton>
                <ul class="nav nav-tabs">
                    <li id="liForm" role="presentation" class="active"><a data-toggle="tab" href="#FormTab">Form</a></li>
                    <li id="liRegister" role="presentation"><a data-toggle="tab" href="#RegisterTab">Register</a></li>
                </ul>
                <div class="tab-content">
                    <div id="FormTab" class="tab-pane fade in active">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group has-feedback">
                                            <label>Ticket No</label>
                                            <asp:TextBox ID="txtTicketNo" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group has-feedback">
                                            <label>Ticket Date</label>
                                            <asp:TextBox ID="txtTicketDate" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group has-feedback">
                                            <label>Project</label>
                                            <asp:DropDownList ID="ddlProject" runat="server" class="form-control" 
                                                AutoPostBack="true" 
                                                OnSelectedIndexChanged="ddlProject_SelectedIndexChanged">
                                                <asp:ListItem Text="CampusERP"></asp:ListItem>
                                                <asp:ListItem Text="Website"></asp:ListItem>
                                                <asp:ListItem Text="Emails"></asp:ListItem>
                                                <asp:ListItem Text="DentalHIS"></asp:ListItem>
                                                <asp:ListItem Text="AyurvedHIS"></asp:ListItem>
                                                <asp:ListItem Text="HomoeopathyHIS"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group has-feedback">
                                            <label>Ticket For</label>
                                            <asp:DropDownList ID="ddlTicketFor" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group has-feedback">
                                            <label>Page URL <span style="color: red">*</span></label>
                                            <asp:TextBox ID="txtPageURL" runat="server" class="form-control"
                                                MaxLength="500" required="" data-error="Please enter Page URL!"></asp:TextBox>
                                            <span class="help-block with-errors"></span>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group has-feedback">
                                            <label>Description</label>
                                            <asp:TextBox ID="txtDescription" runat="server" class="form-control"
                                                Rows="4" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group has-feedback">
                                            <asp:CheckBox ID="chkIsApprovalRequired" Text="Is Approval Required" class="form-control" runat="server" />
                                        </div>
                                    </div>
                                    <div id="divApprovalList">
                                        <div class="col-md-10">
                                            <div class="form-group has-feedback">
                                                <label>Select Staff</label>
                                                <asp:DropDownList ID="ddlApprovalStaffList" runat="server" class="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:LinkButton ID="lnkbtnAddApprovingStaff" runat="server" Text="Add Staff"
                                                Style="margin-top: 25px;"
                                                CssClass="btn btn-warning"
                                                OnClick="lnkbtnAddApprovingStaff_Click"></asp:LinkButton>
                                        </div>
                                        <div class="col-md-12">
                                            <table id="myTableAllocated" class="table-hover table-bordered  table-responsive table-striped responsiveTable" style="width: 100%">
                                                <asp:ListView ID="lstApprovalStaffList" runat="server" 
                                                    OnItemDeleting="lstApprovalStaffList_ItemDeleting"
                                                    DataKeyNames="StaffID">
                                                    <LayoutTemplate>
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th><i style="font-size: 20px" class="fa fa-close"></th>
                                                                <th>ERPStaffID</th>
                                                                <th>Staff Name</th>
                                                                <th>Status</th>
                                                                <th>Date</th>
                                                                <th>Remarks</th>
                                                            </tr>
                                                        </thead>
                                                        <div id="itemplaceholder" runat="server">
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%#Container.DataItemIndex+1 %></td>
                                                            <td>
                                                                <asp:LinkButton ToolTip="Remove"  CssClass="fa fa-close" Style="font-size: 20px"
                                                                    ForeColor="Red" runat="server" ID="imgDelete"
                                                                    OnClientClick="return getConfirmation(this, 'Please confirm','Are you sure you want to delete?');"
                                                                    CommandName="Delete"> </asp:LinkButton>
                                                            </td>
                                                            <td><%# Eval("ERPStaffID")%></td>
                                                            <td><%# Eval("StaffName")%></td>
                                                            <td><%# Eval("Status")%></td>
                                                            <td><%# Eval("Date")%></td>
                                                            <td><%# Eval("Remarks")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer">
                                <asp:LinkButton ID="lnkbtnNew" runat="server" Text="New" CssClass="btn btn-default" OnClick="lnkbtnNew_Click"></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="lnkbtnSave_Click">
                                </asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnUpdate" runat="server" Text="Update" CssClass="btn btn-warning" OnClick="lnkbtnUpdate_Click"></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnCancel" runat="server" Text="Cancel" CssClass="btn btn-info" OnClick="lnkbtnCancel_Click"></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" 
                                    OnClick="lnkbtnDelete_Click"
                                    OnClientClick="return getConfirmation(this, 'Please confirm','Are you sure you want to delete?');"></asp:LinkButton>
                                <asp:Label ID="lblOutput" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div id="RegisterTab" class="tab-pane fade in">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div id="no-more-tables">
                                    <table id="myTable" class="table-hover table-bordered  table-responsive table-striped responsiveTable" style="width: 100%;">
                                        <asp:ListView ID="lstRegister" runat="server">
                                            <LayoutTemplate>
                                                <thead>
                                                    <tr style="cursor: pointer">
                                                        <th style="width: 50px;">Sr.No</th>
                                                        <th>Ticket No</th>
                                                        <th>Date</th>
                                                        <th>Project</th>
                                                        <th>Ticket For</th>
                                                        <th>Status</th>
                                                    </tr>
                                                </thead>
                                                <div id="itemplaceholder" runat="server">
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr style="cursor: pointer" onclick="DisplayData('<%# DataBinder.Eval(Container.DataItem, "EntryID")%>');">
                                                    <td style="width: 8px;"><%#Container.DataItemIndex+1 %></td>
                                                    <td><%# Eval("TicketNo")%></td>
                                                    <td><%# Eval("TicketDate")%></td>
                                                    <td><%# Eval("Project")%>  </td>
                                                    <td><%# Eval("TicketFor")%>  </td>
                                                    <td><%# Eval("Status")%>  </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--========================================================================================================--%>
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
                    <div class="btn btn-default" data-dismiss="modal">Close</div>
                    <div id="btnConfirm" class="btn btn-danger">Yes, please</div>
                </div>
            </div>
        </div>
    </div>
    <%------------------------------------------------------------------------------------------------------------%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
    <script>
        function DisplayData(EntryID) {
            document.getElementById("<%=hdEntryID.ClientID%>").value = EntryID;
            document.getElementById("<%=lnkbtnDisplayData.ClientID%>").click();
        }
    </script>
    <%--========================================================================================================--%>
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
