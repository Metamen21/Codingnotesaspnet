<%@ Page Title="" Language="C#" MasterPageFile="~/Secured/MasterPage.Master" AutoEventWireup="true" CodeBehind="Test Requistionaspx.aspx.cs" Inherits="erpSoftTickets.Secured.Test_Requistionaspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
        <ContentTemplate>
            <section class="content-header">
                <h1>Test Requistion</h1>
            </section>
            <section class="content">
                <div class="card">
                    <div class="card-header">
                        <label class="card-title">Requistions</label>
                    </div>
                    <div class="card-body">
                        <div class="col-md-6">
                            <label>Enter Name</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-12">
                            <asp:LinkButton ID="lnkbtnShowModal" runat="server" CssClass="btn btn-primary"
                                OnClientClick="return getConfirmation(this);"
                                OnClick="lnkbtnShowModal_Click" />
                        </div>
                    </div>
                    <div class="card-footer">
                        <br />
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="modalPopUp" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Requistion
                    </h4>
                </div>
                <div class="modal-body">
                    <asp:TextBox ID="txtTest" runat="server" CssClass="form-control" />
                    <br />
                    <br />
                    <br />
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
    <%--SCRIPT FOR DELETE CONFIRMATION --%>
    <script type="text/javascript">
        //script for confirm yes/no box
        function getConfirmation(sender, title, message) {

            $('#modalPopUp').modal('show');
            $('#btnConfirm').attr('onclick', "$('#modalPopUp').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
            return false;
        }
    </script>
</asp:Content>
