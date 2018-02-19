<%@ Page Title="Groups" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Groups.aspx.cs" Inherits="GroupBuilderAdmin.Groups" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .highlight
        {
            background-color:Yellow;
        }
    </style>
    <script>
        //function allowDrop(ev) {
        //    ev.preventDefault();
        //}

        //function drag(ev) {
        //    ev.dataTransfer.setData("innerhtml", ev.target.id);
        //}

        //function drop(ev) {
        //    ev.preventDefault();
        //    var data = ev.dataTransfer.getData("innerhtml");
        //    ev.target.appendChild(document.getElementById(data));
        //}
        $(document).ready(function () {
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

            $(".UpdateButton").click(function () {
                var data = new Array();
                $("div .group div").each(function (index) {
                    data[index] = "'" + this.innerHTML + "'";
                });
                alert(data.join());
                $.ajax({
                    type: 'POST',
                    url: 'Groups.aspx/UpdateGroups',
                    contentType: "application/json; charset=utf-8",
                    data: '{ studentIDs:[' + data.join() + ']}',
                    dataType: 'json',
                    success: function (results) { alert(results.getData()); },
                    error: function () { alert('error'); }
                });
            });
        })

        function OnDragStart(e) {
            this.style.opacity = '0.3';
            srcElement = this;
            e.dataTransfer.effectAllowed = 'move';
            e.dataTransfer.setData('text/html', $(this).find("header")[0].innerHTML);
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
            srcElement.style.opacity = '1';
            $(this).removeClass('highlight');
            var count = $(this).find("div[data-student-id='" + e.dataTransfer.getData('text/html') + "']").length;
            if (count <= 0) {
                $(this).append("<div class='selectedstudent' data-student-id='" + e.dataTransfer.getData('text/html') + "'>" + e.dataTransfer.getData('text/html') + "</div>");
            }
            else {
                alert("This student is already added to your group!");
            }
            return false;
        }

        function OnDragEnd(e) {
            $("div .group").removeClass('highlight');
            this.style.opacity = '1';
        }



    </script>
    <h4>
        Current Group List
    </h4>
    <asp:Panel ID="NoGroupsPanel" runat="server">
        <p class="lead">
            You have no groups created yet.  Click <b>Create Groups</b> to create some groups.
        </p>
        <asp:LinkButton ID="CreateGroupsLinkButton" runat="server" CssClass="btn btn-default btn-sm float-rigtht" OnClick="CreateGroupsLinkButton_Click"><span class="fa fa-plus"></span>&nbsp;&nbsp;Create Groups</asp:LinkButton>
    </asp:Panel>
    <asp:Panel ID="AddGroupsPanel" runat="server" CssClass="panel panel-default">
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
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-2">
                <br />
                <asp:LinkButton ID="BuildGroupsLinkButton" CssClass="btn btn-default btn-sm" runat="server" OnClick="BuildGroupsLinkButton_Click"><span class="fa fa-check"></span>&nbsp;&nbsp;Build Groups</asp:LinkButton>
            </div>
            <div class="col-md-4">
                <br />
                <div class="btn-group">
<%--                <asp:LinkButton ID="CancelBuildGroupsLinkButton" CssClass="btn btn-default btn-sm" runat="server" OnClick="CancelBuildGroupsLinkButton_Click"><span class="fa fa-ban"></span>&nbsp;&nbsp;Cancel</asp:LinkButton>--%>
                <asp:LinkButton ID="ResetGroupsLinkButton" CssClass="btn btn-default btn-sm" runat="server" OnClick="ResetGroupsLinkButton_Click"><span class="fa fa-refresh"></span>&nbsp;&nbsp;Reset Groups</asp:LinkButton>
                <asp:LinkButton ID="DeleteAllGroupsLinkButton" CssClass="btn btn-danger btn-sm" runat="server" OnClick="DeleteAllGroupsLinkButton_Click"><span class="fa fa-remove"></span>&nbsp;&nbsp;Delete All Groups</asp:LinkButton>
                </div>
            </div>
        </div>


    </asp:Panel>
    <asp:LinkButton ID="ReturnToStudentsLinkButton" runat="server" CssClass="btn btn-default btn-sm" OnClick="ReturnToStudentsLinkButton_Click"><span class="fa fa-arrow-left"></span>&nbsp;&nbsp;Return to Students</asp:LinkButton>
    <div class="container">
        <div class="row">
            <div class="col-md-3">
                <asp:GridView ID="StudentsGridView" runat="server" CssClass="table table-bordered table-condensed small" AutoGenerateColumns="false" DataKeyNames="StudentID" OnRowDataBound="StudentsGridView_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Students">
                            <ItemTemplate>
                                <div style='min-height: 40px;'>
                                <div class="panel panel-default panel-condensed student" id="studentPanel" draggable="true">
                                    <div class="row">
                                        <div class="col-md-9">
                                            <header><%# Eval("StudentID") %></header>
                                            <%# Eval("FirstName") + " " + Eval("LastName") %><br />
                                            <asp:Label ID="LanguagesLabel" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:LinkButton ID="RemoveLinkButton" runat="server" CssClass="btn btn-danger btn-xs"><span class="fa fa-remove"></span></asp:LinkButton>
                                        </div>
                                    </div>
<%--                                    <asp:Label ID="StudentLabel" runat="server" Text=''></asp:Label>--%>
                                </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-md-9">
                <div class="row">
                    <asp:Repeater ID="GroupsRepeater" runat="server">
                        <ItemTemplate>
                            <div class="col-md-4">
                                <asp:Label ID="GroupNameLabel" runat="server" CssClass="control-label" Text='<%# Eval("Name") %>'></asp:Label>
                                <div class="panel panel-default group" style="min-height: 300px;">

                                        <input type="button" class="btn btn-default btn-sm UpdateButton" value="Update Group"/>

                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>

    <asp:GridView ID="GroupsGridView" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="Group Number" DataField="DuckID" />
            <asp:BoundField HeaderText="Name" DataField="FirstName" />
            <asp:TemplateField HeaderText="Group Members">
                <ItemTemplate>
                    <asp:GridView ID="GroupMembersGridView" runat="server" CssClass="table table-bordered table-condensed">
                        <Columns>
                            <asp:BoundField DataField="Name" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                        <asp:LinkButton ID="RemoveLinkButton" runat="server" CssClass="btn btn-danger btn-xs" CommandName="delete_student" CommandArgument='<%# Eval("StudentID") %>'><span class="fa fa-remove"></span></asp:LinkButton> 

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Notes" DataField="LastName" />
            <asp:TemplateField HeaderStyle-Width="1%">
                <ItemTemplate>
                    <asp:LinkButton ID="EditLinkButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="edit_student" CommandArgument='<%# Eval("StudentID") %>'><span class="fa fa-pencil"></span></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="1%">
                <ItemTemplate>
                    <asp:LinkButton ID="RemoveLinkButton" runat="server" CssClass="btn btn-danger btn-xs" CommandName="delete_student" CommandArgument='<%# Eval("StudentID") %>'><span class="fa fa-remove"></span></asp:LinkButton> 
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
