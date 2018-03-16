<%@ Page Title="Students" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Students.aspx.cs" Inherits="GroupBuilderAdmin.Students" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="modal fade" id="messageBox">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="MessageBoxCreateLinkButton" />
                </Triggers>
                <ContentTemplate>
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

    <asp:Panel ID="ImportStudentsPanel" runat="server" CssClass="panel panel-default" Visible="false">
        <h3>Import Students
        </h3>
        <div class="row">
            <div class="col-md-6">
                <asp:FileUpload ID="StudentsFileUpload" runat="server" />
            </div>
            <div class="col-md-3">
                <asp:LinkButton ID="CancelImportStudentsLinkButton" runat="server" CssClass="btn btn-default btn-sm" OnClick="CancelImportStudentsLinkButton_Click"><span class="fa fa-ban"></span>&nbsp;&nbsp;Cancel</asp:LinkButton>
                <asp:LinkButton ID="ProcessStudentsFileLinkButton" runat="server" CssClass="btn btn-default btn-sm" OnClick="ProcessStudentsFileLinkButton_Click"><span class="fa fa-upload"></span>&nbsp;&nbsp;Process File</asp:LinkButton>
            </div>
            <div class="col-md-3"></div>
        </div>
    </asp:Panel>

    <asp:UpdatePanel runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="ImportStudentsLinkButton" />
        </Triggers>
        <ContentTemplate>
            <asp:Timer ID="StudentsTimer" Interval="10000" runat="server" OnTick="StudentsTimer_Tick"></asp:Timer>
            <br />
            <asp:HiddenField ID="SelectedStudentIDHiddenField" runat="server" />
            <asp:Panel ID="StudentListPanel" runat="server">
                <h3>Current Student List for
                    <asp:Label ID="CourseNameLabel" runat="server"></asp:Label>
                </h3>
                <div class="row">
                    <div class="col-md-6">
                        <div class="btn-group">
                            <asp:HyperLink ID="ReturnToCoursesHyperLink" runat="server" CssClass="btn btn-default btn-sm" NavigateUrl="~/Default.aspx"><span class="fa fa-arrow-left"></span>&nbsp;&nbsp;Return to Course Sections</asp:HyperLink>
                            <asp:LinkButton ID="ImportStudentsLinkButton" runat="server" CssClass="btn btn-default btn-sm" OnClick="ImportStudentsLinkButton_Click"><span class="fa fa-upload"></span>&nbsp;&nbsp;Import Students From CSV</asp:LinkButton>
                            <asp:LinkButton ID="AddStudentLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="AddStudentLinkButton_Click"><span class="fa fa-plus"></span>&nbsp;&nbsp;Add Student</asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <asp:LinkButton ID="BeginGroupingLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="BeginGroupingLinkButton_Click"><span class="fa fa-users"></span>&nbsp;&nbsp;Manage Groups&nbsp;&nbsp;<span class="fa fa-arrow-right"></span></asp:LinkButton>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <p>
                            <asp:Label ID="SubmittedPercentLabel" runat="server" CssClass="control-label small"></asp:Label>
                        </p>
                    </div>
                    <div class="col-md-6">
                        <div class="float-right">
                            <asp:LinkButton ID="DeleteAllStudentsLinkButton" runat="server" CssClass="btn btn-danger btn-sm float-right" OnClick="DeleteAllStudentsLinkButton_Click"><span class="fas fa-times"></span>&nbsp;&nbsp;Delete All Students</asp:LinkButton>
                            <asp:LinkButton ID="SendWelcomeToAllStudentsLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="SendWelcomeToAllStudentsLinkButton_Click"><span class="fas fa-paper-plane"></span>&nbsp;&nbsp;Send Welcome to All</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="StudentsGridView" runat="server" CssClass="table table-bordered table-striped table-condensed small" DataKeyNames="StudentID" AutoGenerateColumns="false" OnRowCommand="StudentsGridView_RowCommand" OnRowDataBound="StudentsGridView_RowDataBound">
                    <EmptyDataTemplate>
                        <h4>No current student records for this course section.</h4>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField HeaderText="DuckID" DataField="DuckID" />
                        <asp:TemplateField HeaderText="First Name">
                            <ItemTemplate>
                                <asp:Label ID="FirstNameLabel" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Last Name" DataField="LastName" />
                        <asp:TemplateField HeaderText="Native Language">
                            <ItemTemplate>
                                <asp:Label ID="NativeLanguageLabel" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Interested Roles">
                            <ItemTemplate>
                                <asp:Label ID="RolesLabel" runat="server" Text='<%# Eval("RolesDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Languages">
                            <ItemTemplate>
                                <asp:Label ID="LanguagesLabel" runat="server" Text='<%# Eval("ProgrammingLanguagesDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Skills" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="SkillsLabel" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Core GPA">
                            <ItemTemplate>
                                <asp:Label ID="GPALabel" runat="server" Text='<%# Eval("GPA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Survey Submitted">
                            <ItemTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="border: none;">
                                            <asp:Label ID="SubmittedDateLabel" Text='<%# Eval("SurveySubmittedDate") %>' runat="server"></asp:Label>
                                        </td>
                                        <td style="border: none; width: 1%;">
                                            <asp:LinkButton ID="ReOpenLinkButton" runat="server" CommandName="re_open" CommandArgument='<%# Eval("StudentID") %>' Visible='<%# Eval("SurveySubmittedDate") == null ? false : true %>' CssClass="btn btn-default btn-xs"><span class="fa fa-redo"></span></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Welcome Sent" DataField="InitialNotificationSentDate" />
                        <asp:TemplateField HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <div class="btn-group-vertical">
                                    <asp:LinkButton ID="EditLinkButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="edit_student" CommandArgument='<%# Eval("StudentID") %>'><span class="fas fa-pencil-alt"></span>&nbsp;&nbsp;Edit</asp:LinkButton>
                                    <asp:LinkButton ID="SendWelcomeEmail" runat="server" CssClass="btn btn-default btn-xs" CommandName="send_welcome" CommandArgument='<%# Eval("StudentID") %>'><span class="fas fa-paper-plane"></span>&nbsp;&nbsp;Send Welcome</asp:LinkButton>
                                    <asp:LinkButton ID="RemoveLinkButton" runat="server" CssClass="btn btn-danger btn-xs" CommandName="delete_student" CommandArgument='<%# Eval("StudentID") %>'><span class="fas fa-times"></span>&nbsp;&nbsp;Delete</asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>

            <asp:Panel ID="AddStudentPanel" runat="server" CssClass="panel panel-default" Visible="false">
                <div class="row">
                    <div class="col-md-9">
                        <h3>
                            <asp:Label ID="EditStudentLabel" runat="server"></asp:Label>
                        </h3>
                    </div>
                    <div class="col-md-3">
                        <asp:LinkButton ID="TopCancelAddStudentLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="CancelAddStudentLinkButton_Click"><span class="fa fa-ban"></span>&nbsp;&nbsp;Cancel</asp:LinkButton>
                        <asp:LinkButton ID="TopSaveAddStudentLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="SaveAddStudentLinkButton_Click"><span class="fa fa-save"></span>&nbsp;&nbsp;Save Changes</asp:LinkButton>
                    </div>
                </div>
                <div class="panel panel-default">
                    <h4>Student Information</h4>
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="DuckIDLabel" CssClass="control-label" runat="server">DuckID: </asp:Label>
                                <asp:TextBox ID="DuckIDTextBox" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="UOIDLabel" CssClass="control-label" runat="server">UOID: </asp:Label>
                                <asp:TextBox ID="UOIDTextBox" runat="server" CssClass="form-control input-sm" MaxLength="9"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="FirstNameLabel" CssClass="control-label" runat="server">First Name: </asp:Label>
                                <asp:TextBox ID="FirstNameTextBox" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="PreferredNameLabel" CssClass="control-label" runat="server">Preferred: </asp:Label>
                                <asp:TextBox ID="PreferredNameTextBox" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="LastNameLabel" CssClass="control-label" runat="server">Last Name: </asp:Label>
                                <asp:TextBox ID="LastNameTextBox" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="LanguageLabel" runat="server" CssClass="control-label">English as a Second Language:</asp:Label>
                                <asp:DropDownList ID="EnglishLanguageDropDownList" runat="server" CssClass="form-control input-sm" SelectedValue='<%# Eval("EnglishSecondLanguageFLag") %>'>
                                    <asp:ListItem Text="Unspecified" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="NativeLanguageLabel" runat="server" CssClass="control-label">Native Language:</asp:Label>
                                <asp:TextBox ID="NativeLanguageTextBox" runat="server" CssClass="form-control input-sm" Text='<%# Eval("NativeLanguage") %>'></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="GUIDLabel" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default">
                    <h4>Prior Courses</h4>
                    <div class="row">
                        <div class="col-md-5">
                            <div class="form-group">
                                <asp:Label ID="CoreCoursesLabel" runat="server" CssClass="control-label">Core Course: </asp:Label>
                                <asp:DropDownList ID="CoreCoursesDropDownList" runat="server" CssClass="form-control input-sm" DataTextField="FullName" DataValueField="CourseID">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="GradeLabel" runat="server" CssClass="control-label">Grade:</asp:Label>
                                <asp:DropDownList ID="GradesDropDownList" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Text="Select..." Value=""></asp:ListItem>
                                    <asp:ListItem Text="A+" Value="4.3"></asp:ListItem>
                                    <asp:ListItem Text="A" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="A-" Value="3.7"></asp:ListItem>
                                    <asp:ListItem Text="B+" Value="3.3"></asp:ListItem>
                                    <asp:ListItem Text="B" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="B-" Value="2.7"></asp:ListItem>
                                    <asp:ListItem Text="C+" Value="2.3"></asp:ListItem>
                                    <asp:ListItem Text="C" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="C-" Value="1.7"></asp:ListItem>
                                    <asp:ListItem Text="D+" Value="1.3"></asp:ListItem>
                                    <asp:ListItem Text="D" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="D-" Value="0.7"></asp:ListItem>
                                    <asp:ListItem Text="F" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="No Pass" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="Pass" Value="-2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="col-md-1">
                            <br />
                            <asp:LinkButton ID="AddPriorCourseLinkButton" runat="server" CssClass="btn btn-default btn-sm" OnClick="AddPriorCourseLinkButton_Click"><span class="fa fa-plus"></span>&nbsp;&nbsp;Add</asp:LinkButton>
                        </div>
                        <div class="col-md-4">
                            <asp:GridView ID="PriorCoursesGridView" runat="server" OnRowCommand="PriorCoursesGridView_RowCommand" CssClass="table table-bordered table-condensed small" ShowHeader="false" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="CourseLabel" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="GradeLabel" runat="server" Text='<%# Eval("LetterGrade") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="RemoveLinkButton" runat="server" CssClass="btn btn-danger btn-xs" CommandArgument='<%# Eval("CourseID") %>' CommandName="delete_prior_course"><span class="fas fa-times"></span></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <div class="float-right">
                                <asp:Label ID="GPALabel" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default">
                    <h4>Current Courses</h4>
                    <div class="row">
                        <div class="col-md-5">
                            <div class="form-group">
                                <asp:Label ID="CurrentCourseLabel" runat="server" CssClass="control-label">Current Course:</asp:Label>
                                <asp:DropDownList ID="CurrentCoursesDropDownList" runat="server" CssClass="form-control input-sm" DataTextField="FullName" DataValueField="CourseID">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <br />
                            <asp:LinkButton ID="AddCurrentCourseLinkButton" runat="server" CssClass="btn btn-default btn-sm" OnClick="AddCurrentCourseLinkButton_Click"><span class="fa fa-plus"></span>&nbsp;&nbsp;Add</asp:LinkButton>
                        </div>
                        <div class="col-md-2">

                        </div>
                        <div class="col-md-4">
                            <asp:GridView ID="CurrentCoursesGridView" runat="server" OnRowCommand="CurrentCoursesGridView_RowCommand" CssClass="table table-bordered table-condensed small" ShowHeader="false" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="CourseLabel" runat="server" Text='<%# Eval("Code") %>'></asp:Label>:&nbsp;
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="RemoveLinkButton" runat="server" CssClass="btn btn-danger btn-xs" CommandArgument='<%# Eval("CourseID") %>' CommandName="delete_current_course"><span class="fas fa-times"></span></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default">
                    <h4>Role Interest</h4>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="RolesLabel" runat="server" CssClass="control-label">Interested Roles: </asp:Label>
                                <asp:DropDownList ID="RolesDropDownList" runat="server" CssClass="form-control input-sm" DataTextField="Name" DataValueField="RoleID">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="RoleInterestLabel" runat="server" CssClass="control-label">Interest Level: </asp:Label>
                                <asp:DropDownList ID="RoleInterestDropDownList" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Text="Select..." Value=""></asp:ListItem>
                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2" style="vertical-align: bottom;">
                            <br />
                            <asp:LinkButton ID="AddRoleLinkButton" runat="server" CssClass="btn btn-default btn-sm" OnClick="AddRoleLinkButton_Click"><span class="fa fa-plus"></span>&nbsp;&nbsp;Add Role</asp:LinkButton>
                        </div>
                        <div class="col-md-4">
                            <asp:GridView ID="RolesGridView" runat="server" CssClass="table table-bordered table-condensed table-striped small" AutoGenerateColumns="false" OnRowCommand="RolesGridView_RowCommand">
                                <Columns>
                                    <asp:BoundField HeaderText="Role" DataField="Name" />
                                    <asp:BoundField HeaderText="Level of Interest" DataField="InterestLevel" />
                                    <asp:TemplateField HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" CssClass="btn btn-danger btn-xs" CommandName="delete_role" CommandArgument='<%# Eval("RoleID") %>'><span class="fas fa-times"></span></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default">
                    <h4>Programming Languages</h4>

                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="ProgrammingLanguagesLabel" runat="server" CssClass="control-label">Programming Languages: </asp:Label>
                                <asp:DropDownList ID="ProgrammingLanguagesDropDownList" runat="server" CssClass="form-control input-sm" DataTextField="Name" DataValueField="LanguageID">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="LevelOfAbilityLabel" runat="server" CssClass="control-label">Ability Level: </asp:Label>
                                <asp:DropDownList ID="ProgrammingAbilityLevelDropDownList" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Text="Select..." Value=""></asp:ListItem>
                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2" style="vertical-align: bottom;">
                            <br />
                            <asp:LinkButton ID="AddProgrammingLanguageLinkButton" runat="server" CssClass="btn btn-default btn-sm" OnClick="AddProgrammingLanguageLinkButton_Click"><span class="fa fa-plus"></span>&nbsp;&nbsp;Add Language</asp:LinkButton>
                        </div>
                        <div class="col-md-4">
                            <asp:GridView ID="ProgrammingLanguagesGridView" runat="server" CssClass="table table-bordered table-condensed table-striped small" AutoGenerateColumns="false" OnRowCommand="ProgrammingLanguagesGridView_RowCommand">
                                <Columns>
                                    <asp:BoundField HeaderText="Language" DataField="Name" />
                                    <asp:BoundField HeaderText="Level of Ability" DataField="ProficiencyLevel" />
                                    <asp:TemplateField HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" CssClass="btn btn-danger btn-xs" CommandName="delete_language" CommandArgument='<%# Eval("LanguageID") %>'><span class="fas fa-times"></span></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default">
                    <h4>Skills</h4>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="SkillsLabel" runat="server" CssClass="control-label">Skills: </asp:Label>
                                <asp:DropDownList ID="SkillsDropDownList" runat="server" CssClass="form-control input-sm" DataTextField="Name" DataValueField="SkillID">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="SkillsLevel" runat="server" CssClass="control-label">Ability Level: </asp:Label>
                                <asp:DropDownList ID="SkillsLevelDropDownList" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Text="Select..." Value=""></asp:ListItem>
                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2" style="vertical-align: bottom;">
                            <br />
                            <asp:LinkButton ID="AddSkillLinkButton" runat="server" CssClass="btn btn-default btn-sm" OnClick="AddSkillLinkButton_Click"><span class="fa fa-plus"></span>&nbsp;&nbsp;Add Skill</asp:LinkButton>
                        </div>
                        <div class="col-md-4">
                            <asp:GridView ID="SkillsGridView" runat="server" CssClass="table table-bordered table-condensed table-striped small" AutoGenerateColumns="false" OnRowCommand="SkillsGridView_RowCommand">
                                <Columns>
                                    <asp:BoundField HeaderText="Skill" DataField="Name" />
                                    <asp:BoundField HeaderText="Level of Ability" DataField="ProficiencyLevel" />
                                    <asp:TemplateField HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" CssClass="btn btn-danger btn-xs" CommandName="delete_skill" CommandArgument='<%# Eval("SkillID") %>'><span class="fas fa-times"></span></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default">
                    <h4>Short Answer</h4>

                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:Label ID="WorkExperienceLabel" runat="server" CssClass="control-label" Text="Work Experience:"></asp:Label>
                            <asp:TextBox ID="WorkExperienceTextBox" runat="server" CssClass="form-control input-sm" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:Label ID="LearningExpectationsLabel" runat="server" CssClass="control-label" Text="Learning Expectations:"></asp:Label>
                            <asp:TextBox ID="LearningExpectationsTextBox" runat="server" CssClass="form-control input-sm" TextMode="MultiLine" Rows="4"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:LinkButton ID="CancelAddStudentLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="CancelAddStudentLinkButton_Click"><span class="fa fa-ban"></span>&nbsp;&nbsp;Cancel</asp:LinkButton>
                        <asp:LinkButton ID="SaveAddStudentLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="SaveAddStudentLinkButton_Click"><span class="fa fa-save"></span>&nbsp;&nbsp;Save Changes</asp:LinkButton>
                    </div>
                </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
