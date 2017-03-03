
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="EDSNCalendar_ProjectBlue.Views.Home.WebForm1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>

    </title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <style type="text/css">
        .Background
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .Popup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 400px;
            height: 350px;
        }
        .lbl
        {
            font-size:16px;
            font-style:italic;
            font-weight:bold;
        }
    </style>

        <asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
        <asp:Button ID="SubmitEventPopOut" runat="server" Text="Submit Event" />

        <asp:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
    PopupControlID="SubmitPopOutPanel" TargetControlID="SubmitEvenPopOut" CancelControlID = "CancelSubmitEvent">
        </asp:ModalPopupExtender>

        <asp:Panel ID="SubmitPopOutPanel" runat="server" CssClass="Popup" align="center" >
            <asp:Button ID="CancelSubmitEvent" runat="server" Text="Cancel" />

        </asp:Panel>

    </form>
</body>
</html>
