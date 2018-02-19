<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GroupBuilderAdmin.Form.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>STeam Formation Survey</h2>
        <p class="lead">This information will be used to assign groups.</p>
    </div>

    <h4>General</h4>

    <div class="container">
    <div class="row">
        <div class="col-md-3">
            <div class="form-group">
                <label class="control-label">I prefer to be called: </label>
                <asp:TextBox ID="PreferedNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label class="control-label">UO Student ID: </label>
                <asp:TextBox ID="UOIDTextBox" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>

    <br />
    <h4>CIS Course History</h4>

    <asp:Repeater ID="ClassesRepeater" runat="server">
        <HeaderTemplate>
            <div class="row">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("Code") %>' CssClass="control-label"></asp:Label>
                    <asp:HiddenField ID="CourseIDHiddenField" runat="server" Value='<%# Eval("CourseID") %>' />
                    <asp:DropDownList ID="GradeDropDownList" runat="server" CssClass="form-control input-sm">
                        <asp:ListItem Text="Not Taken" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="A" Value="4"></asp:ListItem>
                        <asp:ListItem Text="B" Value="3"></asp:ListItem>
                        <asp:ListItem Text="C" Value="2"></asp:ListItem>
                        <asp:ListItem Text="D" Value="1"></asp:ListItem>
                        <asp:ListItem Text="F" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>

    <br />
    <h4>Role Interests</h4>

    <asp:Repeater ID="RolesRepeater" runat="server">
        <HeaderTemplate>
            <div class="row">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="RoleNameLabel" runat="server" Text='<%# Eval("Name") %>' CssClass="control-label"></asp:Label>
                    <asp:HiddenField ID="RoleIDHiddenField" runat="server" Value='<%# Eval("RoleID") %>' />
                    <asp:DropDownList ID="InterestDropDownList" runat="server" CssClass="form-control input-sm">
                        <asp:ListItem Text="Not Interested" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Not Very Interested" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Indifferent" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Somewhat Interested" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Very Interested" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>

    <br />
    <h4>Programming Languages</h4>

    <asp:Repeater ID="LanguagesRepeater" runat="server">
        <HeaderTemplate>
            <div class="row">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="LanguageNameLabel" runat="server" Text='<%# Eval("Name") %>' CssClass="control-label"></asp:Label>
                    <asp:HiddenField ID="LanguageIDHiddenField" runat="server" Value='<%# Eval("LanguageID") %>' />
                    <asp:DropDownList ID="LanguageDropDownList" runat="server" CssClass="form-control input-sm">
                        <asp:ListItem Text="No Experience" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Somewhat Experienced" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Very Experienced" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>

    <br />
    <h4>Skills</h4>

    <asp:Repeater ID="SkillsRepeater" runat="server">
        <HeaderTemplate>
            <div class="row">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="SkillNameLabel" runat="server" Text='<%# Eval("Name") %>' CssClass="control-label"></asp:Label>
                    <asp:HiddenField ID="SkillIDHiddenField" runat="server" Value='<%# Eval("SkillID") %>' />
                    <asp:DropDownList ID="SkillDropDownList" runat="server" CssClass="form-control input-sm">
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>

    <br />    
    <h4>Free Response</h4>

    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <label class="control-label">Professional Development Experience (if any):</label>
                <asp:TextBox ID="DevelopmentExperienceTextBox" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <label class="control-label">Briefly describe what you expect to learn in this class: </label>
                <asp:TextBox ID="LearningExpectationsTextBox" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-3">
            <asp:LinkButton ID="SubmitLinkButton" runat="server" CssClass="btn btn-default" OnClick="SubmitLinkButton_Click">Submit</asp:LinkButton>
        </div>
    </div>
    </div>
</asp:Content>
