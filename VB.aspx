<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VB.aspx.vb" Inherits="VB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type = "text/javascript">
        var ddlCountries;
        function GetCountries() {
            ddlCountries = document.getElementById("<%=ddlCountries.ClientID %>");
            ddlCountries.options.length == 0;
            AddOption("Loading", "0");
            PageMethods.GetCountries(OnSuccess);
        }
        window.onload = GetCountries;

        function OnSuccess(response) {
            ddlCountries.options.length = 0;
            AddOption("Please select", "0");
            for (var i in response) {
                AddOption(response[i].Name, response[i].value);
            }
        }
        function AddOption(text, value) {
            var option = document.createElement('option');
            option.value = value;
            option.innerHTML = text;
            ddlCountries.options.add(option);
        }
    </script>
</head>
<body>
<form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true">
    </asp:ScriptManager>
    <asp:DropDownList ID="ddlCountries" runat="server">
    </asp:DropDownList>
</form>
</body>
</html>
