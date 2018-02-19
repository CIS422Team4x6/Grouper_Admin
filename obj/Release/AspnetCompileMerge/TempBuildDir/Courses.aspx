<%@ Page Title="Courses" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="GroupBuilderAdmin.Courses" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="modal fade" id="messageBox">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="MessageBoxCreateLinkButton" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="SelectedCourseIDHiddenField" runat="server" />

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

    <asp:UpdatePanel runat="server">
        <ContentTemplate>

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
                <h3>Current Course List</h3>

                <asp:LinkButton ID="AddCourseLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="AddCourseLinkButton_Click"><span class="fa fa-plus"></span>&nbsp;&nbsp;Add Course</asp:LinkButton>

                <asp:GridView ID="CoursesGridView" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="false" DataKeyNames="CourseID" OnRowCommand="CoursesGridView_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="CodeLabel" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="CodeTextBox" runat="server" CssClass="form-control input-sm" Text='<%# Eval("Code") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="NameTextBox" runat="server" CssClass="form-control input-sm" Text='<%# Eval("Name") %>'></asp:TextBox>
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
                                    <asp:LinkButton ID="EditLinkButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="edit_course"><span class="fa fa-pencil"></span>&nbsp;&nbsp;Edit</asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div class="btn-group">
                                    <asp:LinkButton ID="CancelLinkButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="cancel_edit"><span class="fa fa-ban"></span>&nbsp;&nbsp;Cancel</asp:LinkButton>
                                </div>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <div class="btn-group">
                                    <asp:LinkButton ID="DeleteLinkButton" runat="server" CssClass="btn btn-danger btn-xs" CommandName="delete_course"><span class="fa fa-remove"></span>&nbsp;&nbsp;Delete</asp:LinkButton>
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
