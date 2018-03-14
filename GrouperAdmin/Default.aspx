<%@ Page Title="Course Sections" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GroupBuilderAdmin.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="modal fade" id="messageBox">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="MessageBoxCreateLinkButton" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <asp:HiddenField ID="SelectedInstructorCourseHiddenField" runat="server" />
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
            <h3>
                Welcome <asp:Label ID="InstructorNameLabel" runat="server"></asp:Label>!
            </h3>
            <asp:Panel ID="NoCoursesPanel" runat="server" Visible="false">
                <p class="lead">
                    You have not yet created any course sections.&nbsp;&nbsp;Click <b>Create Course Section</b> to create a new course section.
                </p>
            </asp:Panel>
            <asp:Panel ID="AddCourseSectionPanel" runat="server" Visible="false" CssClass="panel panel-default">
                <div class="container">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="CourseDropDownLabel" runat="server" Text="Course"></asp:Label>
                                        <asp:DropDownList ID="CoursesDropDownList" runat="server" CssClass="form-control" DataTextField="FullName" DataValueField="CourseID"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="TermLabel" runat="server" Text="Term:"></asp:Label>
                                        <asp:DropDownList ID="TermsDropDownList" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Select..." Value=""></asp:ListItem>
                                            <asp:ListItem Text="Fall" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Winter" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Spring" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Summer" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="YearLabel" runat="server" Text="Year:"></asp:Label>
                                        <asp:DropDownList ID="YearsDropDownList" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select..." Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="DaysOfWeekLabel" runat="server" Text="Course Days:"></asp:Label>
                                        <asp:CheckBoxList ID="DaysOfWeekCheckBox" runat="server" CssClass="checkboxlist" RepeatColumns="5">
                                            <asp:ListItem Text="Mon" Value="M"></asp:ListItem>
                                            <asp:ListItem Text="Tues" Value="T"></asp:ListItem>
                                            <asp:ListItem Text="Wed" Value="W"></asp:ListItem>
                                            <asp:ListItem Text="Thurs" Value="R"></asp:ListItem>
                                            <asp:ListItem Text="Fri" Value="F"></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="StartTimeLabel" runat="server" Text="Start Time:"></asp:Label>
                                        <asp:DropDownList ID="StartTimeDropDownList" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select..." Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="EndTimeLabel" runat="server" Text="End Time:"></asp:Label>
                                        <asp:DropDownList ID="EndTimeDropDownList" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select..." Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="CRNLabel" runat="server" Text="CRN:"></asp:Label>
                                        <asp:TextBox ID="CRNTextBox" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12" style="vertical-align: bottom;">
                                    <div class="text-right">
                                        <asp:LinkButton ID="CancelAddCourseSectionLinkButton" runat="server" CssClass="btn btn-default btn-sm" OnClick="CancelAddCourseSectionLinkButton_Click"><span class="fa fa-ban"></span>&nbsp;&nbsp;Cancel</asp:LinkButton>&nbsp;&nbsp;
                                        <asp:LinkButton ID="SaveCourseSectionLinkButton" runat="server" CssClass="btn btn-default btn-sm" OnClick="SaveCourseSectionLinkButton_Click"><span class="fa fa-save"></span>&nbsp;&nbsp;Save Course Section</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <div class="row">
                <div class="col-md-12">
                    <asp:LinkButton ID="AddCourseSectionLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="AddCourseSectionLinkButton_Click"><span class="fa fa-plus"></span>&nbsp;&nbsp;Create Course Section</asp:LinkButton>
                </div>
            </div>

            <asp:Panel ID="CoursesPanel" runat="server" Visible="false">
                <p class="lead">
                    Below is a list of your current course sections.  Click <b>Select Section</b> to view your students and groups.
                </p>
                <div class="container">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="CourseSectionsGridView" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" OnRowCommand="CourseSectionsGridView_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Course">
                                        <ItemTemplate>
                                            <asp:Label ID="CourseLabel" Text='<%# Eval("Course.FullName") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Term">
                                        <ItemTemplate>
                                            <asp:Label ID="TermLabel" runat="server" Text='<%# Eval("TermName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Days">
                                        <ItemTemplate>
                                            <asp:Label ID="DaysOfWeekLabel" runat="server" Text='<%# Eval("DaysOfWeek") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Time">
                                        <ItemTemplate>
                                            <asp:Label ID="TimesLabel" runat="server" Text='<%# Eval("StartTime", "{0:h:mm - }") + Eval("EndTime", "{0:h:mm}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CRN">
                                        <ItemTemplate>
                                            <asp:Label ID="CRNLabel" runat="server" Text='<%# ((int)Eval("CRN")) == 0 ? "" : Eval("CRN") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="SelectInstructorCourseLinkButton" runat="server" CssClass="btn btn-default btn-sm" CommandName="select_instructor_course" CommandArgument='<%# Eval("InstructorCourseID") %>'><span class="fa fa-check"></span>&nbsp;&nbsp;Select Section</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="EditInstructorCourseLinkButton" runat="server" CssClass="btn btn-default btn-sm" CommandName="edit_instructor_course" CommandArgument='<%# Eval("InstructorCourseID") %>'><span class="fas fa-pencil-alt"></span>&nbsp;&nbsp;Edit</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="RemoveInstructorCourse" runat="server" CssClass="btn btn-danger btn-sm" CommandName="delete_instructor_course" CommandArgument='<%# Eval("InstructorCourseID") %>'><span class="fas fa-times"></span>&nbsp;&nbsp;Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HyperLink ID="FormComponentsHyperLink" runat="server" CssClass="btn btn-default btn-sm" NavigateUrl="~/Components.aspx"><span class="fa fa-pencil-alt"></span>&nbsp;&nbsp;Edit Form Components</asp:HyperLink>

</asp:Content>
