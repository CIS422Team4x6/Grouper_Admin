<%@ Page Title="Components" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Components.aspx.cs" Inherits="GroupBuilderAdmin.Components" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="modal fade" id="messageBox">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="MessageBoxCreateLinkButton" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="SelectedCourseIDHiddenField" runat="server" />
                    <asp:HiddenField ID="SelectedRoleIDHiddenField" runat="server" />
                    <asp:HiddenField ID="SelectedLanguageIDHiddenField" runat="server" />
                    <asp:HiddenField ID="SelectedSkillIDHiddenField" runat="server" />
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">
                                <asp:Label ID="MessageBoxTitleLabel" runat="server"></asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="MessageBoxMessageLabel" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <p>
                                <asp:LinkButton ID="MessageBoxOkayLinkButton" runat="server" data-dismiss="modal" CssClass="btn btn-default" Style="margin-bottom: 0px;">
                                </asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton ID="MessageBoxCreateLinkButton" runat="server" OnClick="MessageBoxCreateLinkButton_Click" CssClass="btn btn-default">
                            </asp:LinkButton>
                            </p>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <h3>Form Components</h3>
    <p class="lead">Below is a list of form components (the components Students will interact with on the Questionnaire Form).</p>
    <div class="text-right">
        <asp:HyperLink ID="ReturnToInstructorCourses" runat="server" CssClass="btn btn-default btn-sm" NavigateURL="~/Default.aspx">Return to Course Sections&nbsp;&nbsp;<span class="fa fa-arrow-right"></span></asp:HyperLink>
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

          <asp:Panel ID="CoursesPanel" runat="server" CssClass="panel panel-default">
            <asp:Panel ID="AddCoursePanel" runat="server" Visible="false">
                <h3>Add Course</h3>
                <div class="panel panel-default">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="CodeLabel" CssClass="control-label" runat="server">Code: <span class="text-muted small">(ex. CIS 422)</span> </asp:Label>
                                    <asp:TextBox ID="CodeTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:Label ID="NameLabel" CssClass="control-label" runat="server">Name: <span class="text-muted small">(ex. Programming Systems Concepts)</span></asp:Label>
                                    <asp:TextBox ID="NameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="CoreCourseLabel" CssClass="control-label" runat="server">Core Course?</asp:Label>
                                    <asp:DropDownList ID="CoreCourseDropDownList" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:LinkButton ID="SaveAddCourseLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="SaveAddCourseLinkButton_Click"><span class="fa fa-save"></span>&nbsp;&nbsp;Save Course</asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton ID="CancelAddCourseLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="CancelAddCourseLinkButton_Click"><span class="fa fa-ban"></span>&nbsp;&nbsp;Cancel</asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="CoursesListPanel" runat="server">
                <h3>Courses</h3>

                <asp:LinkButton ID="AddCourseLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="AddCourseLinkButton_Click"><span class="fa fa-plus"></span>&nbsp;&nbsp;Add Course</asp:LinkButton>

                <asp:GridView ID="CoursesGridView" runat="server" CssClass="table table-bordered table-condensed small" AutoGenerateColumns="false" DataKeyNames="CourseID" OnRowCommand="CoursesGridView_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control input-sm" Text='<%# Eval("Code") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control input-sm" Text='<%# Eval("Name") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Core Course?">
                            <ItemTemplate>
                                <asp:Label ID="CoreCourseFlagLabel" runat="server" Text='<%# ((bool)Eval("CoreCourseFlag")).ToString() == "True" ? "Yes" : "No" %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="CoreCourseFlagDropDownList" runat="server" CssClass="form-control input-sm" SelectedValue='<%# Eval("CoreCourseFlag") %>'>
                                    <asp:ListItem Text="True" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="False" Value="False"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <div class="btn-group">
                                    <asp:LinkButton ID="EditLinkButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="edit_course"><span class="fas fa-pencil-alt"></span>&nbsp;&nbsp;Edit</asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div class="btn-group">
                                    <asp:LinkButton ID="CancelLinkButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="cancel_edit"><span class="fas fa-ban"></span>&nbsp;&nbsp;Cancel</asp:LinkButton>
                                </div>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <div class="btn-group">
                                    <asp:LinkButton ID="DeleteLinkButton" runat="server" CssClass="btn btn-danger btn-xs" CommandName="delete_course"><span class="fa fa-times"></span>&nbsp;&nbsp;Delete</asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div class="btn-group">
                                    <asp:LinkButton ID="SaveLinkButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="save_changes"><span class="fa fa-save"></span>&nbsp;&nbsp;Save</asp:LinkButton>
                                </div>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </asp:Panel>
                    <asp:Panel ID="LanguagesPanel" runat="server" CssClass="panel panel-default">
            <asp:Panel ID="AddLanguagePanel" runat="server" Visible="false">
                <h3>Add Language</h3>
                <div class="panel panel-default">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:Label ID="LanguageNameLabel" CssClass="control-label" runat="server">Language Name: <span class="text-muted small">(ex. Requirements Analyst)</span> </asp:Label>
                                    <asp:TextBox ID="LanguageNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:LinkButton ID="SaveAddLanguageLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="SaveAddLanguageLinkButton_Click"><span class="fa fa-save"></span>&nbsp;&nbsp;Save Language</asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton ID="CancelAddLanguageLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="CancelAddLanguageLinkButton_Click"><span class="fa fa-ban"></span>&nbsp;&nbsp;Cancel</asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="LanguagesListPanel" runat="server">
                <h3>Programming Languages</h3>

                <asp:LinkButton ID="AddLanguageLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="AddLanguageLinkButton_Click"><span class="fa fa-plus"></span>&nbsp;&nbsp;Add Language</asp:LinkButton>

                <asp:GridView ID="LanguagesGridView" runat="server" CssClass="table table-bordered table-condensed small" AutoGenerateColumns="false" DataKeyNames="LanguageID" OnRowCommand="LanguagesGridView_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="EditLanguageNameLabel" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="EditLanguageNameTextBox" runat="server" CssClass="form-control input-sm" Text='<%# Eval("Name") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Display on Questionnaire?">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DisplayLanguageDropDownList" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <div class="btn-group">
                                    <asp:LinkButton ID="EditLanguageLinkButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="edit_language"><span class="fas fa-pencil-alt"></span>&nbsp;&nbsp;Edit</asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div class="btn-group">
                                    <asp:LinkButton ID="CancelEditLanguageLinkButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="cancel_edit_language"><span class="fas fa-ban"></span>&nbsp;&nbsp;Cancel</asp:LinkButton>
                                </div>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <div class="btn-group">
                                    <asp:LinkButton ID="DeleteLanguageLinkButton" runat="server" CssClass="btn btn-danger btn-xs" CommandName="delete_language"><span class="fa fa-times"></span>&nbsp;&nbsp;Delete</asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div class="btn-group">
                                    <asp:LinkButton ID="SaveLanguageLinkButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="save_language_changes"><span class="fa fa-save"></span>&nbsp;&nbsp;Save</asp:LinkButton>
                                </div>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="RolesPanel" runat="server" CssClass="panel panel-default">
            <asp:Panel ID="AddRolePanel" runat="server" Visible="false">
                <h3>Add Role</h3>
                <div class="panel panel-default">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:Label ID="RoleNameLabel" CssClass="control-label" runat="server">Role Name: <span class="text-muted small">(ex. Requirements Analyst)</span> </asp:Label>
                                    <asp:TextBox ID="RoleNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:LinkButton ID="SaveAddRoleLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="SaveAddRoleLinkButton_Click"><span class="fa fa-save"></span>&nbsp;&nbsp;Save Role</asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton ID="CancelAddRoleLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="CancelAddRoleLinkButton_Click"><span class="fa fa-ban"></span>&nbsp;&nbsp;Cancel</asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="RolesListPanel" runat="server">
                <h3>Roles</h3>

                <asp:LinkButton ID="AddRoleLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="AddRoleLinkButton_Click"><span class="fa fa-plus"></span>&nbsp;&nbsp;Add Role</asp:LinkButton>

                <asp:GridView ID="RolesGridView" runat="server" CssClass="table table-bordered table-condensed small" AutoGenerateColumns="false" DataKeyNames="RoleID" OnRowCommand="RolesGridView_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="EditRoleNameLabel" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="EditRoleNameTextBox" runat="server" CssClass="form-control input-sm" Text='<%# Eval("Name") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Display on Questionnaire?">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DisplayRoleDropDownList" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <div class="btn-group">
                                    <asp:LinkButton ID="EditRoleLinkButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="edit_role"><span class="fas fa-pencil-alt"></span>&nbsp;&nbsp;Edit</asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div class="btn-group">
                                    <asp:LinkButton ID="CancelEditRoleLinkButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="cancel_edit_role"><span class="fas fa-ban"></span>&nbsp;&nbsp;Cancel</asp:LinkButton>
                                </div>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <div class="btn-group">
                                    <asp:LinkButton ID="DeleteRoleLinkButton" runat="server" CssClass="btn btn-danger btn-xs" CommandName="delete_role"><span class="fa fa-times"></span>&nbsp;&nbsp;Delete</asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div class="btn-group">
                                    <asp:LinkButton ID="SaveRoleLinkButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="save_role_changes"><span class="fa fa-save"></span>&nbsp;&nbsp;Save</asp:LinkButton>
                                </div>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </asp:Panel>
                                <asp:Panel ID="SkillsPanel" runat="server" CssClass="panel panel-default">
            <asp:Panel ID="AddSkillPanel" runat="server" Visible="false">
                <h3>Add Skill</h3>
                <div class="panel panel-default">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:Label ID="SkillNameLabel" CssClass="control-label" runat="server">Skill Name: <span class="text-muted small">(ex. Requirements Analyst)</span> </asp:Label>
                                    <asp:TextBox ID="SkillNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:LinkButton ID="SaveAddSkillLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="SaveAddSkillLinkButton_Click"><span class="fa fa-save"></span>&nbsp;&nbsp;Save Skill</asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton ID="CancelAddSkillLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="CancelAddSkillLinkButton_Click"><span class="fa fa-ban"></span>&nbsp;&nbsp;Cancel</asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="SkillsListPanel" runat="server" >
                <h3>Skills</h3>

                <asp:LinkButton ID="AddSkillLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="AddSkillLinkButton_Click"><span class="fa fa-plus"></span>&nbsp;&nbsp;Add Skill</asp:LinkButton>

                <asp:GridView ID="SkillsGridView" runat="server" CssClass="table table-bordered table-condensed small" AutoGenerateColumns="false" DataKeyNames="SkillID" OnRowCommand="SkillsGridView_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="EditSkillNameLabel" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="EditSkillNameTextBox" runat="server" CssClass="form-control input-sm" Text='<%# Eval("Name") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Display on Questionnaire?">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DisplaySkillDropDownList" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <div class="btn-group">
                                    <asp:LinkButton ID="EditSkillLinkButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="edit_skill"><span class="fas fa-pencil-alt"></span>&nbsp;&nbsp;Edit</asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div class="btn-group">
                                    <asp:LinkButton ID="CancelEditSkillLinkButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="cancel_edit_skill"><span class="fas fa-ban"></span>&nbsp;&nbsp;Cancel</asp:LinkButton>
                                </div>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <div class="btn-group">
                                    <asp:LinkButton ID="DeleteSkillLinkButton" runat="server" CssClass="btn btn-danger btn-xs" CommandName="delete_skill"><span class="fa fa-times"></span>&nbsp;&nbsp;Delete</asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div class="btn-group">
                                    <asp:LinkButton ID="SaveSkillLinkButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="save_skill_changes"><span class="fa fa-save"></span>&nbsp;&nbsp;Save</asp:LinkButton>
                                </div>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
