<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationPage1.aspx.cs" Inherits="ECMADemo.Layouts.ECMADemo.ApplicationPage1" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <SharePoint:ScriptLink ID="SPScriptLink" runat="server" LoadAfterUI="true" Localizable="false" Name="sp.js" />
    <script type="text/javascript">
        function execClientOM() {
            var context = new SP.ClientContext.get_current();

            this.web = context.get_web();
            context.load(this.web);

            this.lists = web.get_lists();
            context.load(lists);

            context.executeQueryAsync(
                Function.createDelegate(this, this.onSuccessMethod),
                Function.createDelegate(this, this.onFailureMethod)
                );
        }
        function onSuccessMethod(sender, args) {
            alert("web title: " + this.web.get_title() + "\n" + "Total List in Site: " + this.lists.get_count());
        }
        function onFailureMethod(sender, args) {
            alert("Request Failed" + args.get_message() + "\n" + args.get_stackTrace());
        }

    </script>

    <input type="button" onclick="execClientOM()" value="Execute Client OM" />

</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Application Page
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    My Application Page
</asp:Content>
