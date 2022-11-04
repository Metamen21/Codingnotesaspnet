<%@ Page Title="" Language="C#" MasterPageFile="~/Secured/MasterPage.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="erpSoftTickets.Secured.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
    <asp:Table ID="Table1" runat="server">
    </asp:Table>
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="RollNo" DataSourceID="SqlDataSource1">
        <AlternatingItemTemplate>
            <tr style="background-color: #FFFFFF;color: #284775;">
                <td>
                    <asp:DynamicControl runat="server" DataField="RollNo" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Gender" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Name" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ClassName" Mode="ReadOnly" />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="background-color: #999999;">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="RollNo" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Gender" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Name" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ClassName" Mode="Edit" />
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" ValidationGroup="Insert" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Gender" Mode="Insert" ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Name" Mode="Insert" ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ClassName" Mode="Insert" ValidationGroup="Insert" />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="background-color: #E0FFFF;color: #333333;">
                <td>
                    <asp:DynamicControl runat="server" DataField="RollNo" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Gender" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Name" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ClassName" Mode="ReadOnly" />
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                            <tr runat="server" style="background-color: #E0FFFF;color: #333333;">
                                <th runat="server">RollNo</th>
                                <th runat="server">Gender</th>
                                <th runat="server">Name</th>
                                <th runat="server">ClassName</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="text-align: center;background-color: #5D7B9D;font-family: Verdana, Arial, Helvetica, sans-serif;color: #FFFFFF">
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="background-color: #E2DED6;font-weight: bold;color: #333333;">
                <td>
                    <asp:DynamicControl runat="server" DataField="RollNo" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Gender" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Name" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ClassName" Mode="ReadOnly" />
                </td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CampusERPConnectionString %>" SelectCommand="SELECT StudentMaster.RollNo, StudentMaster.Gender, StudentMaster.Name, ClassMaster.ClassName FROM ClassMaster INNER JOIN StudentMaster ON ClassMaster.ClassID = StudentMaster.ClassID"></asp:SqlDataSource>
</asp:Content>
