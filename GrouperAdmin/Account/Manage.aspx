<%@ Page Title="Manage Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="GroupBuilderAdmin.Account.Manage" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <div class="float-right">
            <asp:HyperLink ID="ReturnHomeHyperLink" runat="server" CssClass="btn btn-default btn-sm" NavigateUrl="~/Default.aspx"><span class="fa fa-home"></span>&nbsp;&nbsp;Return Home</asp:HyperLink>

    </div>

    <div>
        <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
            <p class="text-success"><%: SuccessMessage %></p>
        </asp:PlaceHolder>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="form-horizontal">
                <h4>Change your account settings</h4>

                <hr />
                <dl class="dl-horizontal">
                    <dt>Password:</dt>
                    <dd>
                        <asp:HyperLink CssClass="btn btn-default btn-sm" NavigateUrl="~/Account/ManagePassword" Visible="false" ID="ChangePassword" runat="server" ><span class="fa fa-asterisk"></span>&nbsp;&nbsp;Change Password</asp:HyperLink>
                        <asp:HyperLink CssClass="btn btn-default btn-sm" NavigateUrl="~/Account/ManagePassword" Visible="false" ID="CreatePassword" runat="server" ><span class="fa fa-asterisk"></span>&nbsp;&nbsp;Create Password</asp:HyperLink>
                    </dd>
                </dl>
            </div>
        </div>
    </div>

</asp:Content>
