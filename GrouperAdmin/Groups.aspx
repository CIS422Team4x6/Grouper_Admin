<%@ Page Title="Groups" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Groups.aspx.cs" Inherits="GroupBuilderAdmin.Groups" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #sticky {
            width: 100%;
        }

            #sticky.fixed {
                width: 260px;
                position: fixed;
                top: 75px;
                max-height: 800px;
                overflow-y: auto;
            }
    </style>
    <script>
        // Initialize the sticky group list and setup event handlers for drag and drop divs
        $(document).ready(function () {
            $(window).scroll(function () {
                var distanceFromTop = $(document).scrollTop();
                if (distanceFromTop >= 280) {
                    $('#sticky').addClass('fixed');
                }
                else {
                    $('#sticky').removeClass('fixed');
                }
            });

            $("div .student").each(function () {
                this.addEventListener('dragstart', OnDragStart, false);
            });

            $("div .group").each(function () {
                this.addEventListener('dragenter', OnDragEnter, false);
                this.addEventListener('dragleave', OnDragLeave, false);
                this.addEventListener('dragover', OnDragOver, false);
                this.addEventListener('drop', OnDrop, false);
                this.addEventListener('dragend', OnDragEnd, false);
            });
        })

        // Callback function that updates groups after drag and drop
        function UpdateGroup(groupNumber) {
            var data = new Array();
            $("div .group div.selectedstudent").each(function (index) {
                data[index] = "'" + this.innerHTML + "'";
            });

            $("div .group div.selectedstudent").remove();

            var instructorCourseID = $("#MainContent_InstructorCourseIDHiddenField").val();
            $.ajax({
                type: 'POST',
                url: 'Groups.aspx/UpdateGroups',
                contentType: "application/json; charset=utf-8",
                data: '{ instructorCourseID:' + instructorCourseID + ', groupNumber:' + groupNumber + ', studentIDs:[' + data.join() + ']}',
                dataType: 'json',
                success: function (data) {
                    BindGridView(data.d);
                    UpdateStats(data.d);
                    BindGridView(groupNumber);
                    UpdateStats(groupNumber);
                    BindStudentsGridView();
                },
                error: function (result) {
                }
            });
        }

        // Callback function that updates group stat calculations
        function UpdateStats(number) {
            var instructorCourseID = $("#MainContent_InstructorCourseIDHiddenField").val();

            $.ajax({
                type: "POST",
                url: "Groups.aspx/GetStats",
                contentType: "application/json;charset=utf-8",
                data: "{'instructorCourseID':'" + instructorCourseID + "','groupNumber':'" + (number + 1).toString() + "'}",
                dataType: "json",
                success: function (data) {
                    var id = '#MainContent_GroupsRepeater_GroupStatsLabel_' + number.toString();
                    $(id).empty();

                    if (data.d != null) {
                        $(id).append(data.d);
                    }
                },
                error: function (result) {
                }
            });
        }

        // Callback function that binds a group gridview
        function BindGridView(number) {
            var instructorCourseID = $("#MainContent_InstructorCourseIDHiddenField").val();

            $.ajax({
                type: "POST",
                url: "Groups.aspx/GetGroup",
                contentType: "application/json;charset=utf-8",
                data: "{'instructorCourseID':'" + instructorCourseID + "','groupNumber':'" + (number + 1).toString() + "'}",
                dataType: "json",
                success: function (data) {
                    var id = '#MainContent_GroupsRepeater_GroupMembersGridView_' + number.toString();
                    $(id).empty();

                    if (data.d.length > 0) {
                        for (var i = 0; i < data.d.length; i++) {
                            $(id).append("<tr>" +
                                "<td>" +
                                    "<div class='panel panel-default panel-condensed student' title='" + data.d[i].Description + "' id='studentPanel' draggable='true'>" +
                                        "<div class='row'>" +
                                            "<div class='col-md-10'>" +
                                                "<header style='display: none;'>" + data.d[i].StudentID + "</header>" +
                                                "<input id='studentIDHiddenField' type='hidden' value='" + data.d[i].StudentID + "'>" +
                                                data.d[i].FirstName + " " + data.d[i].LastName +
                                                "<span class='float-right'>" + data.d[i].RolesDescription + "</span><br />" +
                                                data.d[i].ProgrammingLanguagesDescription +
                                                "<span class='float-right'>" + data.d[i].GPA + "</span>" +
                                            "</div>" +
                                            "<div class='col-md-2'>" +
                                                "<a onclick='DeleteStudent(" + number + ", " + data.d[i].StudentID + ");' class='btn btn-danger btn-xs float-right'>" +
                                                    "<span class='fas fa-times'></span>" +
                                                "</a>" +
                                            "</div>" +
                                        "</div>" +
                                    "</div>" +
                                "</td>" +
                            "</tr>");
                        }
                    }

                    $("div .student").each(function () {
                        this.addEventListener('dragstart', OnDragStart, false);
                    });

                    $("div .group").each(function () {
                        this.addEventListener('dragenter', OnDragEnter, false);
                        this.addEventListener('dragleave', OnDragLeave, false);
                        this.addEventListener('dragover', OnDragOver, false);
                        this.addEventListener('drop', OnDrop, false);
                        this.addEventListener('dragend', OnDragEnd, false);
                    });
                },
                error: function (result) {
                }
            });
        }

        // Callback function that binds the student list after a student is added back/removed
        function BindStudentsGridView() {
            var instructorCourseID = $("#MainContent_InstructorCourseIDHiddenField").val();
            var sortLanguageID = 0;
            var sortGPA = 0;
            var sortRoleID = 0;

            if ($("#MainContent_SortLanguageHiddenField").val().length > 0) {
                sortLanguageID = $("#MainContent_SortLanguageHiddenField").val();
            }

            if ($("#MainContent_SortGPAHiddenField").val().length > 0) {
                sortGPA = $("#MainContent_SortGPAHiddenField").val();
            }

            if ($("#MainContent_SortRoleHiddenField").val().length > 0) {
                sortRoleID = $("#MainContent_SortRoleHiddenField").val();
            }

            $.ajax({
                type: "POST",
                url: "Groups.aspx/GetStudents",
                contentType: "application/json;charset=utf-8",
                data: "{'instructorCourseID':'" + instructorCourseID + "', 'sortLanguageID':'" + sortLanguageID + "', 'sortGPA':'" + sortGPA + "', 'sortRoleID':'" + sortRoleID + "'}",
                dataType: "json",
                success: function (data) {
                    var studentsGridView = $("#MainContent_StudentsGridView");
                    studentsGridView.empty();

                    if (data.d.length > 0) {
                        for (var i = 0; i < data.d.length; i++) {

                            $("#MainContent_StudentsGridView").append("<tr><td><div style='min-height: 40px;'>" +
                                "<div class='panel panel-default panel-condensed student' title='" + data.d[i].Description + "' id='studentPanel' draggable='true'><header style='display: none;'>" + data.d[i].StudentID + "</header><input id='studentIDHiddenField' type='hidden' value='" + data.d[i].StudentID + "'>" + data.d[i].FirstName + " " + data.d[i].LastName +
                                "<span class='float-right'>" + data.d[i].RolesDescription + "</span><br />" +
                                data.d[i].ProgrammingLanguagesDescription + "<div class='float-right'>" + data.d[i].GPA + "</div></div></div></td>");
                            $("div .student").each(function () {
                                this.addEventListener('dragstart', OnDragStart, false);
                            });
                        }
                    }
                },
                error: function (result) {
                }
            });
        }

        // Delete function that removes a student from a group (after dropped on another group)
        function DeleteStudent(groupNumber, studentID) {
            $.ajax({
                type: 'POST',
                url: 'Groups.aspx/DeleteStudentFromGroups',
                contentType: "application/json; charset=utf-8",
                data: "{'studentID':'" + studentID.toString() + "'}",
                dataType: 'json',
                success: function (data) {
                    BindGridView(groupNumber);
                    UpdateStats(groupNumber);
                    BindStudentsGridView();
                },
                error: function (result) {
                }
            });
        }

        // General necessary drag and drop event handling functions
        function OnDragStart(e) {
            this.style.opacity = '0.3';
            srcElement = this;
            e.dataTransfer.effectAllowed = 'move';
            e.dataTransfer.setData('text/plain', $(this).find("header")[0].innerHTML);
        }

        function OnDragOver(e) {
            if (e.preventDefault) {
                e.preventDefault();
            }
            $(this).addClass('highlight');
            e.dataTransfer.dropEffect = 'move';
            return false;
        }

        function OnDragEnter(e) {
            $(this).addClass('highlight');
        }

        function OnDragLeave(e) {
            $(this).removeClass('highlight');
        }

        function OnDrop(e) {
            if (e.stopPropagation) {
                e.stopPropagation();
            }
            e.preventDefault();

            srcElement.style.opacity = '1';

            var groupNumber = parseInt(e.target.id.slice(-1));

            $(this).removeClass('highlight');
            var count = $(this).find("div[data-student-id='" + e.dataTransfer.getData('text/plain') + "']").length;
            if (count <= 0) {
                $(this).append("<div class='selectedstudent' data-group-id='" + (groupNumber + 1) + "' data-student-id='" + e.dataTransfer.getData('text/plain') + "' style='display: none;'>" + e.dataTransfer.getData('text/plain') + "</div>");
                UpdateGroup(groupNumber);
            }
            else {
                alert("This student is already added to your group!");
            }
            return false;
        }

        function OnDragEnd(e) {
            $("div .group").removeClass('highlight');
            srcElement.style.opacity = '1';
            this.style.opacity = '1';
        }
    </script>

    <!-- Modal Dialog -->
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
                            <asp:HiddenField ID="SelectedGroupIDHiddenField" runat="server" />
                            <asp:HiddenField ID="SelectedStudentIDHiddenField" runat="server" />
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
    <br />
    <asp:HiddenField ID="InstructorCourseIDHiddenField" runat="server" />
    <h3>Current Groups for
        <asp:Label ID="CourseNameLabel" runat="server"></asp:Label>
    </h3>
    <asp:HiddenField ID="SortLanguageHiddenField" runat="server" />
    <asp:HiddenField ID="SortGPAHiddenField" runat="server" />
    <asp:HiddenField ID="SortRoleHiddenField" runat="server" />
    <asp:LinkButton ID="ReturnToStudentsLinkButton" runat="server" CssClass="btn btn-default btn-sm" OnClick="ReturnToStudentsLinkButton_Click"><span class="fas fa-arrow-left"></span>&nbsp;&nbsp;Return to Students</asp:LinkButton>

    <asp:Panel ID="CreateGroupsPanel" runat="server" CssClass="panel panel-default">
        <asp:Panel ID="NoGroupsPanel" runat="server">
            <p class="lead">
                You have no groups created yet.  Select the number you would like to create and click <b>Create Groups</b> to create your groups.
            </p>
        </asp:Panel>
        <div class="row">
            <div class="col-md-2">
                <div class="form-group">
                    <asp:Label ID="NumberOfGroupsLabel" CssClass="control-label" runat="server">Number of Groups:</asp:Label>
                    <asp:DropDownList ID="NumberOfGroupsDropDownList" CssClass="form-control" runat="server">
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-2">
                <br />
                <asp:LinkButton ID="BuildGroupsLinkButton" CssClass="btn btn-default" runat="server" OnClick="BuildGroupsLinkButton_Click"><span class="fas fa-wrench"></span>&nbsp;&nbsp;Create Groups</asp:LinkButton>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <span class="text-muted inline-form"><span class="fa fa-info-circle"></span>&nbsp;<asp:Label ID="RecommendedGroupAmountLabel" runat="server"></asp:Label></span>
            </div>
        </div>
    </asp:Panel>
    <br />

    <asp:Panel ID="GroupingPanel" runat="server" Visible="false">
        <asp:LinkButton ID="GroupListLinkButton" runat="server" CssClass="btn btn-default btn-sm float-right" OnClick="GroupListLinkButton_Click"><span class="fa fa-list-alt"></span>&nbsp;&nbsp;Group List</asp:LinkButton>
        <div class="container">
            <div class="row">
                <div class="col-md-3">
                    <div>
                        <h4>Student List</h4>
                        <asp:GridView ID="StudentsGridView" runat="server" CssClass="table table-bordered table-condensed small" AutoGenerateColumns="false" DataKeyNames="StudentID" ShowHeader="false">
                            <EmptyDataTemplate>
                                &nbsp;
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div style='min-height: 40px;'>
                                            <div class="panel panel-default panel-condensed student" id="studentPanel" draggable="true">
                                                <div class="row">
                                                    <div class="col-md-12" title='<%# Eval("Description") %>'>
                                                        <header style="display: none;"><%# Eval("StudentID") %></header>
                                                        <%# Eval("FirstName") + " " + Eval("LastName") %><asp:Label ID="ESLLabel" runat="server"><%# ((bool)Eval("EnglishSecondLanguageFlag")).ToString() == "True" ? "<span class='fas fa-globe fa-lg' title='" + Eval("NativeLanguage") + "'></span>" : "" %></asp:Label><span class="float-right"><asp:Label ID="RolesLabel" runat="server" Text='<%# Eval("RolesDescription") %>'></asp:Label></span><br />
                                                        <asp:Label ID="LanguagesLabel" runat="server" Text='<%# Eval("ProgrammingLanguagesDescription") %>'></asp:Label>
                                                        <asp:Label ID="GPALabel" runat="server" Text='<%# Eval("GPA") %>' CssClass="float-right"></asp:Label>
                                                        <footer><span style='display: none'>0</span></footer>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-md-9">
                    <div class="row">
                        <div class="col-md-12">
                            <h4>Grouping Tools</h4>
                            <div class="panel panel-default">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="btn-group float-right">
                                            <asp:LinkButton ID="AutoGenerateGroupsLinkButton" runat="server" CssClass="btn btn-default float-right" OnClick="AutoGenerateGroupsLinkButton_Click"><span class="fa fa-magic"></span>&nbsp;&nbsp;Auto-Generate Groups</asp:LinkButton>
                                            <asp:LinkButton ID="ResetGroupsLinkButton" CssClass="btn btn-default" runat="server" OnClick="ResetGroupsLinkButton_Click"><span class="fas fa-sync-alt"></span>&nbsp;&nbsp;Reset Groups</asp:LinkButton>
                                            <asp:LinkButton ID="DeleteAllGroupsLinkButton" CssClass="btn btn-danger" runat="server" OnClick="DeleteAllGroupsLinkButton_Click"><span class="far fa-trash-alt"></span>&nbsp;&nbsp;Delete All Groups</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <h5>Sort Students</h5>
                                <div class="row panel panel-default" style="margin: 0px 2px 0px 2px;">
                                    <div class="col-md-3">
                                        <asp:Label ID="LanguageLabel" Text="Programming Language:" runat="server"></asp:Label>
                                        <asp:DropDownList ID="LanguagesDropDownList" CssClass="form-control" runat="server" AutoPostBack="true" DataTextField="Name" DataValueField="LanguageID" OnSelectedIndexChanged="LanguagesDropDownList_SelectedIndexChanged" AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select..." Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="GPALabel" Text="GPA:" runat="server"></asp:Label>
                                        <asp:DropDownList ID="GPADropDownList" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="GPADropDownList_SelectedIndexChanged" AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select..." Value=""></asp:ListItem>
                                            <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                                            <asp:ListItem Text="High" Value="High"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="RoleLabel" Text="Interested Role:" runat="server"></asp:Label>
                                        <asp:DropDownList ID="RolesDropDownList" CssClass="form-control" runat="server" AutoPostBack="true" DataTextField="Name" DataValueField="RoleID" OnSelectedIndexChanged="RolesDropDownList_SelectedIndexChanged" AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select..." Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="btn-group float-right">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <asp:Repeater ID="GroupsRepeater" runat="server" OnItemDataBound="GroupsRepeater_ItemDataBound" OnPreRender="GroupsRepeater_PreRender" OnItemCommand="GroupsRepeater_ItemCommand">
                            <ItemTemplate>
                                <asp:Literal ID="RowOpenLiteral" runat="server"></asp:Literal>
                                <div class="col-md-4">
                                    <asp:TextBox ID="GroupNameTextBox" runat="server" CssClass="form-control input-sm" Visible="false"></asp:TextBox>
                                    <asp:Label ID="GroupNameLabel" runat="server" CssClass="control-label" Text='<%# Eval("Name") %>'></asp:Label>&nbsp;<asp:LinkButton ID="EditGroupNameLinkButton" runat="server" CssClass="btn btn-xs" CommandName="edit_group_name" CommandArgument='<%# Eval("GroupID") %>'><span class="fa fa-pencil-alt"></span></asp:LinkButton>
                                    <asp:LinkButton ID="CancelEditGroupNameLinkButton" runat="server" CssClass="btn btn-xs float-right" Visible="false" CommandName="cancel_edit_group_name" CommandArgument='<%# Eval("GroupID") %>'><span class="fa fa-ban"></span></asp:LinkButton>
                                    <asp:LinkButton ID="SaveGroupNameLinkButton" runat="server" CssClass="btn btn-xs float-right" Visible="false" CommandName="save_group_name" CommandArgument='<%# Eval("GroupID") %>'><span class="fa fa-save"></span></asp:LinkButton><br />
                                    <asp:Label ID="GroupStatsLabel" runat="server" CssClass="text-muted small"></asp:Label>
                                    <div runat="server" id='groupPanel' class="panel panel-default group" style="min-height: 300px;">
                                        <asp:GridView ID="GroupMembersGridView" runat="server" CssClass="table table-bordered table-condensed small" AutoGenerateColumns="false" ShowHeader="false">
                                            <EmptyDataTemplate>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div class="panel panel-default panel-condensed student" id="studentPanel" title='<%# Eval("Description") %>' draggable="true">
                                                            <div class="row">
                                                                <div class="col-md-10">
                                                                    <header style="display: none;"><%# Eval("StudentID") %></header>
                                                                    <%# Eval("FirstName") + " " + Eval("LastName") %><span class="float-right"><asp:Label ID="RolesLabel" runat="server" Text='<%# Eval("RolesDescription") %>'></asp:Label></span><br />
                                                                    <asp:Label ID="LanguagesLabel" runat="server" Text='<%# Eval("ProgrammingLanguagesDescription") %>'></asp:Label>
                                                                    <asp:Label ID="GPALabel" runat="server" CssClass="float-right" Text='<%# Eval("GPA") %>'></asp:Label>
                                                                    <footer style="display: none;"><%# ((RepeaterItem)((GridViewRow)Container).NamingContainer.NamingContainer).ItemIndex.ToString() %></footer>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <a onclick='DeleteStudent(<%# ((RepeaterItem)((GridViewRow)Container).NamingContainer.NamingContainer).ItemIndex.ToString() + ", " + Eval("StudentID") %>);' class="btn btn-danger btn-xs float-right"><span class="fas fa-times"></span></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <asp:Literal ID="RowCloseLiteral" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="ListPanel" runat="server">
        <div class="text-right">
            <asp:LinkButton ID="EditGroupsLinkButton" runat="server" CssClass="btn btn-default btn-sm" OnClick="EditGroupsLinkButton_Click"><span class="fa fa-table"></span>&nbsp;&nbsp;Edit Groups</asp:LinkButton>
            <asp:HyperLink ID="MailEntireClassLinkButton" runat="server" CssClass="btn btn-default btn-sm"><span class="fa fa-paper-plane"></span>&nbsp;&nbsp;Mail Entire Class</asp:HyperLink>
            <asp:LinkButton ID="ExportGroupsLinkButton" runat="server" CssClass="btn btn-default btn-sm" OnClick="ExportGroupsLinkButton_Click"><span class="fa fa-download"></span>&nbsp;&nbsp;Export to CSV</asp:LinkButton>
        </div>
        <asp:GridView ID="GroupsGridView" runat="server" CssClass="table table-bordered table-condensed table-striped" AutoGenerateColumns="false" OnRowDataBound="GroupsGridView_RowDataBound" OnRowCommand="GroupsGridView_RowCommand" DataKeyNames="GroupID">
            <Columns>
                <asp:TemplateField HeaderText="Number" HeaderStyle-Width="1%">
                    <ItemTemplate>
                        <asp:Label ID="GroupNumberLabel" runat="server" CssClass="control-label" Text='<%# Eval("GroupNumber") %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="EditGroupNumberLabel" runat="server" CssClass="control-label" Text='<%# Eval("GroupNumber") %>'>
                        </asp:Label>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Group Name" HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <asp:Label ID="GroupNameLabel" runat="server" CssClass="control-label" Text='<%# Eval("Name") %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="EditGroupNameTextBox" runat="server" CssClass="form-control input-sm" Text='<%# Eval("Name") %>'>
                        </asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Group Members" HeaderStyle-Width="50%">
                    <ItemTemplate>
                        <asp:GridView ID="GroupMembersGridView" runat="server" CssClass="table table-bordered table-condensed small" AutoGenerateColumns="false" OnRowCommand="GroupMembersGridView_RowCommand" OnRowDataBound="GroupMembersGridView_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="DuckID" HeaderText="DuckID" HeaderStyle-Width="10%" />
                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:Label ID="StudentNameLabel" runat="server" Text='<%# Eval("FirstName") + " " + Eval("LastName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Languages" HeaderStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:Label ID="LanguagesLabel" runat="server" Text='<%# Eval("ProgrammingLanguagesDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Roles" HeaderStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:Label ID="RolesLabel" runat="server" Text='<%# Eval("RolesDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="RemoveGroupMemberLinkButton" runat="server" CssClass="btn btn-danger btn-xs" CommandName="delete_group_member" CommandArgument='<%# Eval("StudentID") %>'><span class="fas fa-times"></span></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:GridView ID="EditGroupMembersGridView" runat="server" CssClass="table table-bordered table-condensed small" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="DuckID" HeaderStyle-Width="10%" />
                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:Label ID="StudentNameLabel" runat="server" Text='<%# Eval("FirstName") + " " + Eval("LastName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Languages" HeaderStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:Label ID="LanguagesLabel" runat="server" Text='<%# Eval("ProgrammingLanguagesDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Roles" HeaderStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:Label ID="RolesLabel" runat="server" Text='<%# Eval("RolesDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Notes">
                    <ItemTemplate>
                        <asp:Label ID="GroupNotesLabel" runat="server" CssClass="control-label" Text='<%# Eval("Notes") %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="EditGroupNotesTextBox" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control input-sm" Text='<%# Eval("Notes") %>'>
                        </asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="1%">
                    <ItemTemplate>
                        <div class="btn-group-vertical">
                            <asp:LinkButton ID="EditLinkButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="edit_group" CommandArgument='<%# Eval("GroupID") %>'><span class="fas fa-pencil-alt"></span>&nbsp;&nbsp;Edit</asp:LinkButton>
                            <asp:LinkButton ID="RemoveLinkButton" runat="server" CssClass="btn btn-danger btn-xs" CommandName="delete_group" CommandArgument='<%# Eval("GroupID") %>'><span class="fas fa-times"></span>&nbsp;&nbsp;Delete</asp:LinkButton>
                            <asp:HyperLink ID="EmailGroupHyperLink" runat="server" CssClass="btn btn-default btn-xs"><span class="fa fa-paper-plane"></span>&nbsp;&nbsp;New Email</asp:HyperLink>
                        </div>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <div class="btn-group-vertical">
                            <asp:LinkButton ID="SaveChangesLinkButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="save_group" CommandArgument='<%# Eval("GroupID") %>'><span class="fa fa-save"></span>&nbsp;&nbsp;Save</asp:LinkButton>
                            <asp:LinkButton ID="CancelSaveChangesLinkButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="cancel_edit_group" CommandArgument='<%# Eval("GroupID") %>'><span class="fa fa-ban"></span>&nbsp;&nbsp;Cancel</asp:LinkButton>
                        </div>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
