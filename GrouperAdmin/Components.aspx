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
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.1/css/font-awesome.min.css" rel="stylesheet" />
    <style>
        select.awesome {
            font-family: FontAwesome, 'Open Sans';
        }
    </style>
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
                                <asp:Label ID="EditCourseCodeLabel" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="EditCourseCodeTextBox" runat="server" CssClass="form-control input-sm" Text='<%# Eval("Code") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Course Name">
                            <ItemTemplate>
                                <asp:Label ID="EditCourseNameLabel" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="EditCourseNameTextBox" runat="server" CssClass="form-control input-sm" Text='<%# Eval("Name") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Core Course?">
                            <ItemTemplate>
                                <asp:Label ID="CoreCourseLabel" runat="server" Text='<%# ((bool)Eval("CoreCourseFlag")).ToString() == "True" ? "Yes" : "No" %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="CoreCourseFlagDropDownList" runat="server" CssClass="form-control input-sm" SelectedValue='<%# Eval("CoreCourseFlag") %>'>
                                    <asp:ListItem Text="True" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="False" Value="False"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Display on Questionnaire?">
                            <ItemTemplate>
                                <asp:Label ID="EditCourseDisplayLabel" runat="server" Text='<%# Eval("ActiveFlag").ToString() == "True" ? "Yes" : "No" %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="EditCourseDisplayDropDownList" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="False"></asp:ListItem>
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
                                    <asp:Label ID="LanguageNameLabel" CssClass="control-label" runat="server">Language Name: <span class="text-muted small">(ex. Racket)</span> </asp:Label>
                                    <asp:TextBox ID="LanguageNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="LanguageIconLabel" CssClass="control-label" runat="server">Icon</asp:Label>
                                    <select id='langageIconSelect' class="awesome form-control input-sm" runat="server" visible="false">
                                        			<option class='awesome' value="fa-500px">&#xf26e; 500Px</option>
			<option class='awesome' value="fa-adjust">&#xf042; Adjust</option>
			<option class='awesome' value="fa-adn">&#xf170; Adn</option>
			<option class='awesome' value="fa-align-center">&#xf037; Align-Center</option>
			<option class='awesome' value="fa-align-justify">&#xf039; Align-Justify</option>
			<option class='awesome' value="fa-align-left">&#xf036; Align-Left</option>
			<option class='awesome' value="fa-align-right">&#xf038; Align-Right</option>
			<option class='awesome' value="fa-amazon">&#xf270; Amazon</option>
			<option class='awesome' value="fa-ambulance">&#xf0f9; Ambulance</option>
			<option class='awesome' value="fa-american-sign-language-interpreting">&#xf2a3; American-Sign-Language-Interpreting</option>
			<option class='awesome' value="fa-anchor">&#xf13d; Anchor</option>
			<option class='awesome' value="fa-android">&#xf17b; Android</option>
			<option class='awesome' value="fa-angellist">&#xf209; Angellist</option>
			<option class='awesome' value="fa-angle-double-down">&#xf103; Angle-Double-Down</option>
			<option class='awesome' value="fa-angle-double-left">&#xf100; Angle-Double-Left</option>
			<option class='awesome' value="fa-angle-double-right">&#xf101; Angle-Double-Right</option>
			<option class='awesome' value="fa-angle-double-up">&#xf102; Angle-Double-Up</option>
			<option class='awesome' value="fa-angle-down">&#xf107; Angle-Down</option>
			<option class='awesome' value="fa-angle-left">&#xf104; Angle-Left</option>
			<option class='awesome' value="fa-angle-right">&#xf105; Angle-Right</option>
			<option class='awesome' value="fa-angle-up">&#xf106; Angle-Up</option>
			<option class='awesome' value="fa-apple">&#xf179; Apple</option>
			<option class='awesome' value="fa-archive">&#xf187; Archive</option>
			<option class='awesome' value="fa-area-chart">&#xf1fe; Area-Chart</option>
			<option class='awesome' value="fa-arrow-circle-down">&#xf0ab; Arrow-Circle-Down</option>
			<option class='awesome' value="fa-arrow-circle-left">&#xf0a8; Arrow-Circle-Left</option>
			<option class='awesome' value="fa-arrow-circle-o-down">&#xf01a; Arrow-Circle-O-Down</option>
			<option class='awesome' value="fa-arrow-circle-o-left">&#xf190; Arrow-Circle-O-Left</option>
			<option class='awesome' value="fa-arrow-circle-o-right">&#xf18e; Arrow-Circle-O-Right</option>
			<option class='awesome' value="fa-arrow-circle-o-up">&#xf01b; Arrow-Circle-O-Up</option>
			<option class='awesome' value="fa-arrow-circle-right">&#xf0a9; Arrow-Circle-Right</option>
			<option class='awesome' value="fa-arrow-circle-up">&#xf0aa; Arrow-Circle-Up</option>
			<option class='awesome' value="fa-arrow-down">&#xf063; Arrow-Down</option>
			<option class='awesome' value="fa-arrow-left">&#xf060; Arrow-Left</option>
			<option class='awesome' value="fa-arrow-right">&#xf061; Arrow-Right</option>
			<option class='awesome' value="fa-arrow-up">&#xf062; Arrow-Up</option>
			<option class='awesome' value="fa-arrows">&#xf047; Arrows</option>
			<option class='awesome' value="fa-arrows-alt">&#xf0b2; Arrows-Alt</option>
			<option class='awesome' value="fa-arrows-h">&#xf07e; Arrows-H</option>
			<option class='awesome' value="fa-arrows-v">&#xf07d; Arrows-V</option>
			<option class='awesome' value="fa-asl-interpreting">&#xf2a3; Asl-Interpreting</option>
			<option class='awesome' value="fa-assistive-listening-systems">&#xf2a2; Assistive-Listening-Systems</option>
			<option class='awesome' value="fa-asterisk">&#xf069; Asterisk</option>
			<option class='awesome' value="fa-at">&#xf1fa; At</option>
			<option class='awesome' value="fa-audio-description">&#xf29e; Audio-Description</option>
			<option class='awesome' value="fa-automobile">&#xf1b9; Automobile</option>
			<option class='awesome' value="fa-backward">&#xf04a; Backward</option>
			<option class='awesome' value="fa-balance-scale">&#xf24e; Balance-Scale</option>
			<option class='awesome' value="fa-ban">&#xf05e; Ban</option>
			<option class='awesome' value="fa-bank">&#xf19c; Bank</option>
			<option class='awesome' value="fa-bar-chart">&#xf080; Bar-Chart</option>
			<option class='awesome' value="fa-bar-chart-o">&#xf080; Bar-Chart-O</option>
			<option class='awesome' value="fa-barcode">&#xf02a; Barcode</option>
			<option class='awesome' value="fa-bars">&#xf0c9; Bars</option>
			<option class='awesome' value="fa-battery-0">&#xf244; Battery-0</option>
			<option class='awesome' value="fa-battery-1">&#xf243; Battery-1</option>
			<option class='awesome' value="fa-battery-2">&#xf242; Battery-2</option>
			<option class='awesome' value="fa-battery-3">&#xf241; Battery-3</option>
			<option class='awesome' value="fa-battery-4">&#xf240; Battery-4</option>
			<option class='awesome' value="fa-battery-empty">&#xf244; Battery-Empty</option>
			<option class='awesome' value="fa-battery-full">&#xf240; Battery-Full</option>
			<option class='awesome' value="fa-battery-half">&#xf242; Battery-Half</option>
			<option class='awesome' value="fa-battery-quarter">&#xf243; Battery-Quarter</option>
			<option class='awesome' value="fa-battery-three-quarters">&#xf241; Battery-Three-Quarters</option>
			<option class='awesome' value="fa-bed">&#xf236; Bed</option>
			<option class='awesome' value="fa-beer">&#xf0fc; Beer</option>
			<option class='awesome' value="fa-behance">&#xf1b4; Behance</option>
			<option class='awesome' value="fa-behance-square">&#xf1b5; Behance-Square</option>
			<option class='awesome' value="fa-bell">&#xf0f3; Bell</option>
			<option class='awesome' value="fa-bell-o">&#xf0a2; Bell-O</option>
			<option class='awesome' value="fa-bell-slash">&#xf1f6; Bell-Slash</option>
			<option class='awesome' value="fa-bell-slash-o">&#xf1f7; Bell-Slash-O</option>
			<option class='awesome' value="fa-bicycle">&#xf206; Bicycle</option>
			<option class='awesome' value="fa-binoculars">&#xf1e5; Binoculars</option>
			<option class='awesome' value="fa-birthday-cake">&#xf1fd; Birthday-Cake</option>
			<option class='awesome' value="fa-bitbucket">&#xf171; Bitbucket</option>
			<option class='awesome' value="fa-bitbucket-square">&#xf172; Bitbucket-Square</option>
			<option class='awesome' value="fa-bitcoin">&#xf15a; Bitcoin</option>
			<option class='awesome' value="fa-black-tie">&#xf27e; Black-Tie</option>
			<option class='awesome' value="fa-blind">&#xf29d; Blind</option>
			<option class='awesome' value="fa-bluetooth">&#xf293; Bluetooth</option>
			<option class='awesome' value="fa-bluetooth-b">&#xf294; Bluetooth-B</option>
			<option class='awesome' value="fa-bold">&#xf032; Bold</option>
			<option class='awesome' value="fa-bolt">&#xf0e7; Bolt</option>
			<option class='awesome' value="fa-bomb">&#xf1e2; Bomb</option>
			<option class='awesome' value="fa-book">&#xf02d; Book</option>
			<option class='awesome' value="fa-bookmark">&#xf02e; Bookmark</option>
			<option class='awesome' value="fa-bookmark-o">&#xf097; Bookmark-O</option>
			<option class='awesome' value="fa-braille">&#xf2a1; Braille</option>
			<option class='awesome' value="fa-briefcase">&#xf0b1; Briefcase</option>
			<option class='awesome' value="fa-btc">&#xf15a; Btc</option>
			<option class='awesome' value="fa-bug">&#xf188; Bug</option>
			<option class='awesome' value="fa-building">&#xf1ad; Building</option>
			<option class='awesome' value="fa-building-o">&#xf0f7; Building-O</option>
			<option class='awesome' value="fa-bullhorn">&#xf0a1; Bullhorn</option>
			<option class='awesome' value="fa-bullseye">&#xf140; Bullseye</option>
			<option class='awesome' value="fa-bus">&#xf207; Bus</option>
			<option class='awesome' value="fa-buysellads">&#xf20d; Buysellads</option>
			<option class='awesome' value="fa-cab">&#xf1ba; Cab</option>
			<option class='awesome' value="fa-calculator">&#xf1ec; Calculator</option>
			<option class='awesome' value="fa-calendar">&#xf073; Calendar</option>
			<option class='awesome' value="fa-calendar-check-o">&#xf274; Calendar-Check-O</option>
			<option class='awesome' value="fa-calendar-minus-o">&#xf272; Calendar-Minus-O</option>
			<option class='awesome' value="fa-calendar-o">&#xf133; Calendar-O</option>
			<option class='awesome' value="fa-calendar-plus-o">&#xf271; Calendar-Plus-O</option>
			<option class='awesome' value="fa-calendar-times-o">&#xf273; Calendar-Times-O</option>
			<option class='awesome' value="fa-camera">&#xf030; Camera</option>
			<option class='awesome' value="fa-camera-retro">&#xf083; Camera-Retro</option>
			<option class='awesome' value="fa-car">&#xf1b9; Car</option>
			<option class='awesome' value="fa-caret-down">&#xf0d7; Caret-Down</option>
			<option class='awesome' value="fa-caret-left">&#xf0d9; Caret-Left</option>
			<option class='awesome' value="fa-caret-right">&#xf0da; Caret-Right</option>
			<option class='awesome' value="fa-caret-square-o-down">&#xf150; Caret-Square-O-Down</option>
			<option class='awesome' value="fa-caret-square-o-left">&#xf191; Caret-Square-O-Left</option>
			<option class='awesome' value="fa-caret-square-o-right">&#xf152; Caret-Square-O-Right</option>
			<option class='awesome' value="fa-caret-square-o-up">&#xf151; Caret-Square-O-Up</option>
			<option class='awesome' value="fa-caret-up">&#xf0d8; Caret-Up</option>
			<option class='awesome' value="fa-cart-arrow-down">&#xf218; Cart-Arrow-Down</option>
			<option class='awesome' value="fa-cart-plus">&#xf217; Cart-Plus</option>
			<option class='awesome' value="fa-cc">&#xf20a; Cc</option>
			<option class='awesome' value="fa-cc-amex">&#xf1f3; Cc-Amex</option>
			<option class='awesome' value="fa-cc-diners-club">&#xf24c; Cc-Diners-Club</option>
			<option class='awesome' value="fa-cc-discover">&#xf1f2; Cc-Discover</option>
			<option class='awesome' value="fa-cc-jcb">&#xf24b; Cc-Jcb</option>
			<option class='awesome' value="fa-cc-mastercard">&#xf1f1; Cc-Mastercard</option>
			<option class='awesome' value="fa-cc-paypal">&#xf1f4; Cc-Paypal</option>
			<option class='awesome' value="fa-cc-stripe">&#xf1f5; Cc-Stripe</option>
			<option class='awesome' value="fa-cc-visa">&#xf1f0; Cc-Visa</option>
			<option class='awesome' value="fa-certificate">&#xf0a3; Certificate</option>
			<option class='awesome' value="fa-chain">&#xf0c1; Chain</option>
			<option class='awesome' value="fa-chain-broken">&#xf127; Chain-Broken</option>
			<option class='awesome' value="fa-check">&#xf00c; Check</option>
			<option class='awesome' value="fa-check-circle">&#xf058; Check-Circle</option>
			<option class='awesome' value="fa-check-circle-o">&#xf05d; Check-Circle-O</option>
			<option class='awesome' value="fa-check-square">&#xf14a; Check-Square</option>
			<option class='awesome' value="fa-check-square-o">&#xf046; Check-Square-O</option>
			<option class='awesome' value="fa-chevron-circle-down">&#xf13a; Chevron-Circle-Down</option>
			<option class='awesome' value="fa-chevron-circle-left">&#xf137; Chevron-Circle-Left</option>
			<option class='awesome' value="fa-chevron-circle-right">&#xf138; Chevron-Circle-Right</option>
			<option class='awesome' value="fa-chevron-circle-up">&#xf139; Chevron-Circle-Up</option>
			<option class='awesome' value="fa-chevron-down">&#xf078; Chevron-Down</option>
			<option class='awesome' value="fa-chevron-left">&#xf053; Chevron-Left</option>
			<option class='awesome' value="fa-chevron-right">&#xf054; Chevron-Right</option>
			<option class='awesome' value="fa-chevron-up">&#xf077; Chevron-Up</option>
			<option class='awesome' value="fa-child">&#xf1ae; Child</option>
			<option class='awesome' value="fa-chrome">&#xf268; Chrome</option>
			<option class='awesome' value="fa-circle">&#xf111; Circle</option>
			<option class='awesome' value="fa-circle-o">&#xf10c; Circle-O</option>
			<option class='awesome' value="fa-circle-o-notch">&#xf1ce; Circle-O-Notch</option>
			<option class='awesome' value="fa-circle-thin">&#xf1db; Circle-Thin</option>
			<option class='awesome' value="fa-clipboard">&#xf0ea; Clipboard</option>
			<option class='awesome' value="fa-clock-o">&#xf017; Clock-O</option>
			<option class='awesome' value="fa-clone">&#xf24d; Clone</option>
			<option class='awesome' value="fa-close">&#xf00d; Close</option>
			<option class='awesome' value="fa-cloud">&#xf0c2; Cloud</option>
			<option class='awesome' value="fa-cloud-download">&#xf0ed; Cloud-Download</option>
			<option class='awesome' value="fa-cloud-upload">&#xf0ee; Cloud-Upload</option>
			<option class='awesome' value="fa-cny">&#xf157; Cny</option>
			<option class='awesome' value="fa-code">&#xf121; Code</option>
			<option class='awesome' value="fa-code-fork">&#xf126; Code-Fork</option>
			<option class='awesome' value="fa-codepen">&#xf1cb; Codepen</option>
			<option class='awesome' value="fa-codiepie">&#xf284; Codiepie</option>
			<option class='awesome' value="fa-coffee">&#xf0f4; Coffee</option>
			<option class='awesome' value="fa-cog">&#xf013; Cog</option>
			<option class='awesome' value="fa-cogs">&#xf085; Cogs</option>
			<option class='awesome' value="fa-columns">&#xf0db; Columns</option>
			<option class='awesome' value="fa-comment">&#xf075; Comment</option>
			<option class='awesome' value="fa-comment-o">&#xf0e5; Comment-O</option>
			<option class='awesome' value="fa-commenting">&#xf27a; Commenting</option>
			<option class='awesome' value="fa-commenting-o">&#xf27b; Commenting-O</option>
			<option class='awesome' value="fa-comments">&#xf086; Comments</option>
			<option class='awesome' value="fa-comments-o">&#xf0e6; Comments-O</option>
			<option class='awesome' value="fa-compass">&#xf14e; Compass</option>
			<option class='awesome' value="fa-compress">&#xf066; Compress</option>
			<option class='awesome' value="fa-connectdevelop">&#xf20e; Connectdevelop</option>
			<option class='awesome' value="fa-contao">&#xf26d; Contao</option>
			<option class='awesome' value="fa-copy">&#xf0c5; Copy</option>
			<option class='awesome' value="fa-copyright">&#xf1f9; Copyright</option>
			<option class='awesome' value="fa-creative-commons">&#xf25e; Creative-Commons</option>
			<option class='awesome' value="fa-credit-card">&#xf09d; Credit-Card</option>
			<option class='awesome' value="fa-credit-card-alt">&#xf283; Credit-Card-Alt</option>
			<option class='awesome' value="fa-crop">&#xf125; Crop</option>
			<option class='awesome' value="fa-crosshairs">&#xf05b; Crosshairs</option>
			<option class='awesome' value="fa-css3">&#xf13c; Css3</option>
			<option class='awesome' value="fa-cube">&#xf1b2; Cube</option>
			<option class='awesome' value="fa-cubes">&#xf1b3; Cubes</option>
			<option class='awesome' value="fa-cut">&#xf0c4; Cut</option>
			<option class='awesome' value="fa-cutlery">&#xf0f5; Cutlery</option>
			<option class='awesome' value="fa-dashboard">&#xf0e4; Dashboard</option>
			<option class='awesome' value="fa-dashcube">&#xf210; Dashcube</option>
			<option class='awesome' value="fa-database">&#xf1c0; Database</option>
			<option class='awesome' value="fa-deaf">&#xf2a4; Deaf</option>
			<option class='awesome' value="fa-deafness">&#xf2a4; Deafness</option>
			<option class='awesome' value="fa-dedent">&#xf03b; Dedent</option>
			<option class='awesome' value="fa-delicious">&#xf1a5; Delicious</option>
			<option class='awesome' value="fa-desktop">&#xf108; Desktop</option>
			<option class='awesome' value="fa-deviantart">&#xf1bd; Deviantart</option>
			<option class='awesome' value="fa-diamond">&#xf219; Diamond</option>
			<option class='awesome' value="fa-digg">&#xf1a6; Digg</option>
			<option class='awesome' value="fa-dollar">&#xf155; Dollar</option>
			<option class='awesome' value="fa-dot-circle-o">&#xf192; Dot-Circle-O</option>
			<option class='awesome' value="fa-download">&#xf019; Download</option>
			<option class='awesome' value="fa-dribbble">&#xf17d; Dribbble</option>
			<option class='awesome' value="fa-dropbox">&#xf16b; Dropbox</option>
			<option class='awesome' value="fa-drupal">&#xf1a9; Drupal</option>
			<option class='awesome' value="fa-edge">&#xf282; Edge</option>
			<option class='awesome' value="fa-edit">&#xf044; Edit</option>
			<option class='awesome' value="fa-eject">&#xf052; Eject</option>
			<option class='awesome' value="fa-ellipsis-h">&#xf141; Ellipsis-H</option>
			<option class='awesome' value="fa-ellipsis-v">&#xf142; Ellipsis-V</option>
			<option class='awesome' value="fa-empire">&#xf1d1; Empire</option>
			<option class='awesome' value="fa-envelope">&#xf0e0; Envelope</option>
			<option class='awesome' value="fa-envelope-o">&#xf003; Envelope-O</option>
			<option class='awesome' value="fa-envelope-square">&#xf199; Envelope-Square</option>
			<option class='awesome' value="fa-envira">&#xf299; Envira</option>
			<option class='awesome' value="fa-eraser">&#xf12d; Eraser</option>
			<option class='awesome' value="fa-eur">&#xf153; Eur</option>
			<option class='awesome' value="fa-euro">&#xf153; Euro</option>
			<option class='awesome' value="fa-exchange">&#xf0ec; Exchange</option>
			<option class='awesome' value="fa-exclamation">&#xf12a; Exclamation</option>
			<option class='awesome' value="fa-exclamation-circle">&#xf06a; Exclamation-Circle</option>
			<option class='awesome' value="fa-exclamation-triangle">&#xf071; Exclamation-Triangle</option>
			<option class='awesome' value="fa-expand">&#xf065; Expand</option>
			<option class='awesome' value="fa-expeditedssl">&#xf23e; Expeditedssl</option>
			<option class='awesome' value="fa-external-link">&#xf08e; External-Link</option>
			<option class='awesome' value="fa-external-link-square">&#xf14c; External-Link-Square</option>
			<option class='awesome' value="fa-eye">&#xf06e; Eye</option>
			<option class='awesome' value="fa-eye-slash">&#xf070; Eye-Slash</option>
			<option class='awesome' value="fa-eyedropper">&#xf1fb; Eyedropper</option>
			<option class='awesome' value="fa-fa">&#xf2b4; Fa</option>
			<option class='awesome' value="fa-facebook">&#xf09a; Facebook</option>
			<option class='awesome' value="fa-facebook-f">&#xf09a; Facebook-F</option>
			<option class='awesome' value="fa-facebook-official">&#xf230; Facebook-Official</option>
			<option class='awesome' value="fa-facebook-square">&#xf082; Facebook-Square</option>
			<option class='awesome' value="fa-fast-backward">&#xf049; Fast-Backward</option>
			<option class='awesome' value="fa-fast-forward">&#xf050; Fast-Forward</option>
			<option class='awesome' value="fa-fax">&#xf1ac; Fax</option>
			<option class='awesome' value="fa-feed">&#xf09e; Feed</option>
			<option class='awesome' value="fa-female">&#xf182; Female</option>
			<option class='awesome' value="fa-fighter-jet">&#xf0fb; Fighter-Jet</option>
			<option class='awesome' value="fa-file">&#xf15b; File</option>
			<option class='awesome' value="fa-file-archive-o">&#xf1c6; File-Archive-O</option>
			<option class='awesome' value="fa-file-audio-o">&#xf1c7; File-Audio-O</option>
			<option class='awesome' value="fa-file-code-o">&#xf1c9; File-Code-O</option>
			<option class='awesome' value="fa-file-excel-o">&#xf1c3; File-Excel-O</option>
			<option class='awesome' value="fa-file-image-o">&#xf1c5; File-Image-O</option>
			<option class='awesome' value="fa-file-movie-o">&#xf1c8; File-Movie-O</option>
			<option class='awesome' value="fa-file-o">&#xf016; File-O</option>
			<option class='awesome' value="fa-file-pdf-o">&#xf1c1; File-Pdf-O</option>
			<option class='awesome' value="fa-file-photo-o">&#xf1c5; File-Photo-O</option>
			<option class='awesome' value="fa-file-picture-o">&#xf1c5; File-Picture-O</option>
			<option class='awesome' value="fa-file-powerpoint-o">&#xf1c4; File-Powerpoint-O</option>
			<option class='awesome' value="fa-file-sound-o">&#xf1c7; File-Sound-O</option>
			<option class='awesome' value="fa-file-text">&#xf15c; File-Text</option>
			<option class='awesome' value="fa-file-text-o">&#xf0f6; File-Text-O</option>
			<option class='awesome' value="fa-file-video-o">&#xf1c8; File-Video-O</option>
			<option class='awesome' value="fa-file-word-o">&#xf1c2; File-Word-O</option>
			<option class='awesome' value="fa-file-zip-o">&#xf1c6; File-Zip-O</option>
			<option class='awesome' value="fa-files-o">&#xf0c5; Files-O</option>
			<option class='awesome' value="fa-film">&#xf008; Film</option>
			<option class='awesome' value="fa-filter">&#xf0b0; Filter</option>
			<option class='awesome' value="fa-fire">&#xf06d; Fire</option>
			<option class='awesome' value="fa-fire-extinguisher">&#xf134; Fire-Extinguisher</option>
			<option class='awesome' value="fa-firefox">&#xf269; Firefox</option>
			<option class='awesome' value="fa-first-order">&#xf2b0; First-Order</option>
			<option class='awesome' value="fa-flag">&#xf024; Flag</option>
			<option class='awesome' value="fa-flag-checkered">&#xf11e; Flag-Checkered</option>
			<option class='awesome' value="fa-flag-o">&#xf11d; Flag-O</option>
			<option class='awesome' value="fa-flash">&#xf0e7; Flash</option>
			<option class='awesome' value="fa-flask">&#xf0c3; Flask</option>
			<option class='awesome' value="fa-flickr">&#xf16e; Flickr</option>
			<option class='awesome' value="fa-floppy-o">&#xf0c7; Floppy-O</option>
			<option class='awesome' value="fa-folder">&#xf07b; Folder</option>
			<option class='awesome' value="fa-folder-o">&#xf114; Folder-O</option>
			<option class='awesome' value="fa-folder-open">&#xf07c; Folder-Open</option>
			<option class='awesome' value="fa-folder-open-o">&#xf115; Folder-Open-O</option>
			<option class='awesome' value="fa-font">&#xf031; Font</option>
			<option class='awesome' value="fa-font-awesome">&#xf2b4; Font-Awesome</option>
			<option class='awesome' value="fa-fonticons">&#xf280; Fonticons</option>
			<option class='awesome' value="fa-fort-awesome">&#xf286; Fort-Awesome</option>
			<option class='awesome' value="fa-forumbee">&#xf211; Forumbee</option>
			<option class='awesome' value="fa-forward">&#xf04e; Forward</option>
			<option class='awesome' value="fa-foursquare">&#xf180; Foursquare</option>
			<option class='awesome' value="fa-frown-o">&#xf119; Frown-O</option>
			<option class='awesome' value="fa-futbol-o">&#xf1e3; Futbol-O</option>
			<option class='awesome' value="fa-gamepad">&#xf11b; Gamepad</option>
			<option class='awesome' value="fa-gavel">&#xf0e3; Gavel</option>
			<option class='awesome' value="fa-gbp">&#xf154; Gbp</option>
			<option class='awesome' value="fa-ge">&#xf1d1; Ge</option>
			<option class='awesome' value="fa-gear">&#xf013; Gear</option>
			<option class='awesome' value="fa-gears">&#xf085; Gears</option>
			<option class='awesome' value="fa-genderless">&#xf22d; Genderless</option>
			<option class='awesome' value="fa-get-pocket">&#xf265; Get-Pocket</option>
			<option class='awesome' value="fa-gg">&#xf260; Gg</option>
			<option class='awesome' value="fa-gg-circle">&#xf261; Gg-Circle</option>
			<option class='awesome' value="fa-gift">&#xf06b; Gift</option>
			<option class='awesome' value="fa-git">&#xf1d3; Git</option>
			<option class='awesome' value="fa-git-square">&#xf1d2; Git-Square</option>
			<option class='awesome' value="fa-github">&#xf09b; Github</option>
			<option class='awesome' value="fa-github-alt">&#xf113; Github-Alt</option>
			<option class='awesome' value="fa-github-square">&#xf092; Github-Square</option>
			<option class='awesome' value="fa-gitlab">&#xf296; Gitlab</option>
			<option class='awesome' value="fa-gittip">&#xf184; Gittip</option>
			<option class='awesome' value="fa-glass">&#xf000; Glass</option>
			<option class='awesome' value="fa-glide">&#xf2a5; Glide</option>
			<option class='awesome' value="fa-glide-g">&#xf2a6; Glide-G</option>
			<option class='awesome' value="fa-globe">&#xf0ac; Globe</option>
			<option class='awesome' value="fa-google">&#xf1a0; Google</option>
			<option class='awesome' value="fa-google-plus">&#xf0d5; Google-Plus</option>
			<option class='awesome' value="fa-google-plus-circle">&#xf2b3; Google-Plus-Circle</option>
			<option class='awesome' value="fa-google-plus-official">&#xf2b3; Google-Plus-Official</option>
			<option class='awesome' value="fa-google-plus-square">&#xf0d4; Google-Plus-Square</option>
			<option class='awesome' value="fa-google-wallet">&#xf1ee; Google-Wallet</option>
			<option class='awesome' value="fa-graduation-cap">&#xf19d; Graduation-Cap</option>
			<option class='awesome' value="fa-gratipay">&#xf184; Gratipay</option>
			<option class='awesome' value="fa-group">&#xf0c0; Group</option>
			<option class='awesome' value="fa-h-square">&#xf0fd; H-Square</option>
			<option class='awesome' value="fa-hacker-news">&#xf1d4; Hacker-News</option>
			<option class='awesome' value="fa-hand-grab-o">&#xf255; Hand-Grab-O</option>
			<option class='awesome' value="fa-hand-lizard-o">&#xf258; Hand-Lizard-O</option>
			<option class='awesome' value="fa-hand-o-down">&#xf0a7; Hand-O-Down</option>
			<option class='awesome' value="fa-hand-o-left">&#xf0a5; Hand-O-Left</option>
			<option class='awesome' value="fa-hand-o-right">&#xf0a4; Hand-O-Right</option>
			<option class='awesome' value="fa-hand-o-up">&#xf0a6; Hand-O-Up</option>
			<option class='awesome' value="fa-hand-paper-o">&#xf256; Hand-Paper-O</option>
			<option class='awesome' value="fa-hand-peace-o">&#xf25b; Hand-Peace-O</option>
			<option class='awesome' value="fa-hand-pointer-o">&#xf25a; Hand-Pointer-O</option>
			<option class='awesome' value="fa-hand-rock-o">&#xf255; Hand-Rock-O</option>
			<option class='awesome' value="fa-hand-scissors-o">&#xf257; Hand-Scissors-O</option>
			<option class='awesome' value="fa-hand-spock-o">&#xf259; Hand-Spock-O</option>
			<option class='awesome' value="fa-hand-stop-o">&#xf256; Hand-Stop-O</option>
			<option class='awesome' value="fa-hard-of-hearing">&#xf2a4; Hard-Of-Hearing</option>
			<option class='awesome' value="fa-hashtag">&#xf292; Hashtag</option>
			<option class='awesome' value="fa-hdd-o">&#xf0a0; Hdd-O</option>
			<option class='awesome' value="fa-header">&#xf1dc; Header</option>
			<option class='awesome' value="fa-headphones">&#xf025; Headphones</option>
			<option class='awesome' value="fa-heart">&#xf004; Heart</option>
			<option class='awesome' value="fa-heart-o">&#xf08a; Heart-O</option>
			<option class='awesome' value="fa-heartbeat">&#xf21e; Heartbeat</option>
			<option class='awesome' value="fa-history">&#xf1da; History</option>
			<option class='awesome' value="fa-home">&#xf015; Home</option>
			<option class='awesome' value="fa-hospital-o">&#xf0f8; Hospital-O</option>
			<option class='awesome' value="fa-hotel">&#xf236; Hotel</option>
			<option class='awesome' value="fa-hourglass">&#xf254; Hourglass</option>
			<option class='awesome' value="fa-hourglass-1">&#xf251; Hourglass-1</option>
			<option class='awesome' value="fa-hourglass-2">&#xf252; Hourglass-2</option>
			<option class='awesome' value="fa-hourglass-3">&#xf253; Hourglass-3</option>
			<option class='awesome' value="fa-hourglass-end">&#xf253; Hourglass-End</option>
			<option class='awesome' value="fa-hourglass-half">&#xf252; Hourglass-Half</option>
			<option class='awesome' value="fa-hourglass-o">&#xf250; Hourglass-O</option>
			<option class='awesome' value="fa-hourglass-start">&#xf251; Hourglass-Start</option>
			<option class='awesome' value="fa-houzz">&#xf27c; Houzz</option>
			<option class='awesome' value="fa-html5">&#xf13b; Html5</option>
			<option class='awesome' value="fa-i-cursor">&#xf246; I-Cursor</option>
			<option class='awesome' value="fa-ils">&#xf20b; Ils</option>
			<option class='awesome' value="fa-image">&#xf03e; Image</option>
			<option class='awesome' value="fa-inbox">&#xf01c; Inbox</option>
			<option class='awesome' value="fa-indent">&#xf03c; Indent</option>
			<option class='awesome' value="fa-industry">&#xf275; Industry</option>
			<option class='awesome' value="fa-info">&#xf129; Info</option>
			<option class='awesome' value="fa-info-circle">&#xf05a; Info-Circle</option>
			<option class='awesome' value="fa-inr">&#xf156; Inr</option>
			<option class='awesome' value="fa-instagram">&#xf16d; Instagram</option>
			<option class='awesome' value="fa-institution">&#xf19c; Institution</option>
			<option class='awesome' value="fa-internet-explorer">&#xf26b; Internet-Explorer</option>
			<option class='awesome' value="fa-intersex">&#xf224; Intersex</option>
			<option class='awesome' value="fa-ioxhost">&#xf208; Ioxhost</option>
			<option class='awesome' value="fa-italic">&#xf033; Italic</option>
			<option class='awesome' value="fa-joomla">&#xf1aa; Joomla</option>
			<option class='awesome' value="fa-jpy">&#xf157; Jpy</option>
			<option class='awesome' value="fa-jsfiddle">&#xf1cc; Jsfiddle</option>
			<option class='awesome' value="fa-key">&#xf084; Key</option>
			<option class='awesome' value="fa-keyboard-o">&#xf11c; Keyboard-O</option>
			<option class='awesome' value="fa-krw">&#xf159; Krw</option>
			<option class='awesome' value="fa-language">&#xf1ab; Language</option>
			<option class='awesome' value="fa-laptop">&#xf109; Laptop</option>
			<option class='awesome' value="fa-lastfm">&#xf202; Lastfm</option>
			<option class='awesome' value="fa-lastfm-square">&#xf203; Lastfm-Square</option>
			<option class='awesome' value="fa-leaf">&#xf06c; Leaf</option>
			<option class='awesome' value="fa-leanpub">&#xf212; Leanpub</option>
			<option class='awesome' value="fa-legal">&#xf0e3; Legal</option>
			<option class='awesome' value="fa-lemon-o">&#xf094; Lemon-O</option>
			<option class='awesome' value="fa-level-down">&#xf149; Level-Down</option>
			<option class='awesome' value="fa-level-up">&#xf148; Level-Up</option>
			<option class='awesome' value="fa-life-bouy">&#xf1cd; Life-Bouy</option>
			<option class='awesome' value="fa-life-buoy">&#xf1cd; Life-Buoy</option>
			<option class='awesome' value="fa-life-ring">&#xf1cd; Life-Ring</option>
			<option class='awesome' value="fa-life-saver">&#xf1cd; Life-Saver</option>
			<option class='awesome' value="fa-lightbulb-o">&#xf0eb; Lightbulb-O</option>
			<option class='awesome' value="fa-line-chart">&#xf201; Line-Chart</option>
			<option class='awesome' value="fa-link">&#xf0c1; Link</option>
			<option class='awesome' value="fa-linkedin">&#xf0e1; Linkedin</option>
			<option class='awesome' value="fa-linkedin-square">&#xf08c; Linkedin-Square</option>
			<option class='awesome' value="fa-linux">&#xf17c; Linux</option>
			<option class='awesome' value="fa-list">&#xf03a; List</option>
			<option class='awesome' value="fa-list-alt">&#xf022; List-Alt</option>
			<option class='awesome' value="fa-list-ol">&#xf0cb; List-Ol</option>
			<option class='awesome' value="fa-list-ul">&#xf0ca; List-Ul</option>
			<option class='awesome' value="fa-location-arrow">&#xf124; Location-Arrow</option>
			<option class='awesome' value="fa-lock">&#xf023; Lock</option>
			<option class='awesome' value="fa-long-arrow-down">&#xf175; Long-Arrow-Down</option>
			<option class='awesome' value="fa-long-arrow-left">&#xf177; Long-Arrow-Left</option>
			<option class='awesome' value="fa-long-arrow-right">&#xf178; Long-Arrow-Right</option>
			<option class='awesome' value="fa-long-arrow-up">&#xf176; Long-Arrow-Up</option>
			<option class='awesome' value="fa-low-vision">&#xf2a8; Low-Vision</option>
			<option class='awesome' value="fa-magic">&#xf0d0; Magic</option>
			<option class='awesome' value="fa-magnet">&#xf076; Magnet</option>
			<option class='awesome' value="fa-mail-forward">&#xf064; Mail-Forward</option>
			<option class='awesome' value="fa-mail-reply">&#xf112; Mail-Reply</option>
			<option class='awesome' value="fa-mail-reply-all">&#xf122; Mail-Reply-All</option>
			<option class='awesome' value="fa-male">&#xf183; Male</option>
			<option class='awesome' value="fa-map">&#xf279; Map</option>
			<option class='awesome' value="fa-map-marker">&#xf041; Map-Marker</option>
			<option class='awesome' value="fa-map-o">&#xf278; Map-O</option>
			<option class='awesome' value="fa-map-pin">&#xf276; Map-Pin</option>
			<option class='awesome' value="fa-map-signs">&#xf277; Map-Signs</option>
			<option class='awesome' value="fa-mars">&#xf222; Mars</option>
			<option class='awesome' value="fa-mars-double">&#xf227; Mars-Double</option>
			<option class='awesome' value="fa-mars-stroke">&#xf229; Mars-Stroke</option>
			<option class='awesome' value="fa-mars-stroke-h">&#xf22b; Mars-Stroke-H</option>
			<option class='awesome' value="fa-mars-stroke-v">&#xf22a; Mars-Stroke-V</option>
			<option class='awesome' value="fa-maxcdn">&#xf136; Maxcdn</option>
			<option class='awesome' value="fa-meanpath">&#xf20c; Meanpath</option>
			<option class='awesome' value="fa-medium">&#xf23a; Medium</option>
			<option class='awesome' value="fa-medkit">&#xf0fa; Medkit</option>
			<option class='awesome' value="fa-meh-o">&#xf11a; Meh-O</option>
			<option class='awesome' value="fa-mercury">&#xf223; Mercury</option>
			<option class='awesome' value="fa-microphone">&#xf130; Microphone</option>
			<option class='awesome' value="fa-microphone-slash">&#xf131; Microphone-Slash</option>
			<option class='awesome' value="fa-minus">&#xf068; Minus</option>
			<option class='awesome' value="fa-minus-circle">&#xf056; Minus-Circle</option>
			<option class='awesome' value="fa-minus-square">&#xf146; Minus-Square</option>
			<option class='awesome' value="fa-minus-square-o">&#xf147; Minus-Square-O</option>
			<option class='awesome' value="fa-mixcloud">&#xf289; Mixcloud</option>
			<option class='awesome' value="fa-mobile">&#xf10b; Mobile</option>
			<option class='awesome' value="fa-mobile-phone">&#xf10b; Mobile-Phone</option>
			<option class='awesome' value="fa-modx">&#xf285; Modx</option>
			<option class='awesome' value="fa-money">&#xf0d6; Money</option>
			<option class='awesome' value="fa-moon-o">&#xf186; Moon-O</option>
			<option class='awesome' value="fa-mortar-board">&#xf19d; Mortar-Board</option>
			<option class='awesome' value="fa-motorcycle">&#xf21c; Motorcycle</option>
			<option class='awesome' value="fa-mouse-pointer">&#xf245; Mouse-Pointer</option>
			<option class='awesome' value="fa-music">&#xf001; Music</option>
			<option class='awesome' value="fa-navicon">&#xf0c9; Navicon</option>
			<option class='awesome' value="fa-neuter">&#xf22c; Neuter</option>
			<option class='awesome' value="fa-newspaper-o">&#xf1ea; Newspaper-O</option>
			<option class='awesome' value="fa-object-group">&#xf247; Object-Group</option>
			<option class='awesome' value="fa-object-ungroup">&#xf248; Object-Ungroup</option>
			<option class='awesome' value="fa-odnoklassniki">&#xf263; Odnoklassniki</option>
			<option class='awesome' value="fa-odnoklassniki-square">&#xf264; Odnoklassniki-Square</option>
			<option class='awesome' value="fa-opencart">&#xf23d; Opencart</option>
			<option class='awesome' value="fa-openid">&#xf19b; Openid</option>
			<option class='awesome' value="fa-opera">&#xf26a; Opera</option>
			<option class='awesome' value="fa-optin-monster">&#xf23c; Optin-Monster</option>
			<option class='awesome' value="fa-outdent">&#xf03b; Outdent</option>
			<option class='awesome' value="fa-pagelines">&#xf18c; Pagelines</option>
			<option class='awesome' value="fa-paint-brush">&#xf1fc; Paint-Brush</option>
			<option class='awesome' value="fa-paper-plane">&#xf1d8; Paper-Plane</option>
			<option class='awesome' value="fa-paper-plane-o">&#xf1d9; Paper-Plane-O</option>
			<option class='awesome' value="fa-paperclip">&#xf0c6; Paperclip</option>
			<option class='awesome' value="fa-paragraph">&#xf1dd; Paragraph</option>
			<option class='awesome' value="fa-paste">&#xf0ea; Paste</option>
			<option class='awesome' value="fa-pause">&#xf04c; Pause</option>
			<option class='awesome' value="fa-pause-circle">&#xf28b; Pause-Circle</option>
			<option class='awesome' value="fa-pause-circle-o">&#xf28c; Pause-Circle-O</option>
			<option class='awesome' value="fa-paw">&#xf1b0; Paw</option>
			<option class='awesome' value="fa-paypal">&#xf1ed; Paypal</option>
			<option class='awesome' value="fa-pencil">&#xf040; Pencil</option>
			<option class='awesome' value="fa-pencil-square">&#xf14b; Pencil-Square</option>
			<option class='awesome' value="fa-pencil-square-o">&#xf044; Pencil-Square-O</option>
			<option class='awesome' value="fa-percent">&#xf295; Percent</option>
			<option class='awesome' value="fa-phone">&#xf095; Phone</option>
			<option class='awesome' value="fa-phone-square">&#xf098; Phone-Square</option>
			<option class='awesome' value="fa-photo">&#xf03e; Photo</option>
			<option class='awesome' value="fa-picture-o">&#xf03e; Picture-O</option>
			<option class='awesome' value="fa-pie-chart">&#xf200; Pie-Chart</option>
			<option class='awesome' value="fa-pied-piper">&#xf2ae; Pied-Piper</option>
			<option class='awesome' value="fa-pied-piper-alt">&#xf1a8; Pied-Piper-Alt</option>
			<option class='awesome' value="fa-pied-piper-pp">&#xf1a7; Pied-Piper-Pp</option>
			<option class='awesome' value="fa-pinterest">&#xf0d2; Pinterest</option>
			<option class='awesome' value="fa-pinterest-p">&#xf231; Pinterest-P</option>
			<option class='awesome' value="fa-pinterest-square">&#xf0d3; Pinterest-Square</option>
			<option class='awesome' value="fa-plane">&#xf072; Plane</option>
			<option class='awesome' value="fa-play">&#xf04b; Play</option>
			<option class='awesome' value="fa-play-circle">&#xf144; Play-Circle</option>
			<option class='awesome' value="fa-play-circle-o">&#xf01d; Play-Circle-O</option>
			<option class='awesome' value="fa-plug">&#xf1e6; Plug</option>
			<option class='awesome' value="fa-plus">&#xf067; Plus</option>
			<option class='awesome' value="fa-plus-circle">&#xf055; Plus-Circle</option>
			<option class='awesome' value="fa-plus-square">&#xf0fe; Plus-Square</option>
			<option class='awesome' value="fa-plus-square-o">&#xf196; Plus-Square-O</option>
			<option class='awesome' value="fa-power-off">&#xf011; Power-Off</option>
			<option class='awesome' value="fa-print">&#xf02f; Print</option>
			<option class='awesome' value="fa-product-hunt">&#xf288; Product-Hunt</option>
			<option class='awesome' value="fa-puzzle-piece">&#xf12e; Puzzle-Piece</option>
			<option class='awesome' value="fa-qq">&#xf1d6; Qq</option>
			<option class='awesome' value="fa-qrcode">&#xf029; Qrcode</option>
			<option class='awesome' value="fa-question">&#xf128; Question</option>
			<option class='awesome' value="fa-question-circle">&#xf059; Question-Circle</option>
			<option class='awesome' value="fa-question-circle-o">&#xf29c; Question-Circle-O</option>
			<option class='awesome' value="fa-quote-left">&#xf10d; Quote-Left</option>
			<option class='awesome' value="fa-quote-right">&#xf10e; Quote-Right</option>
			<option class='awesome' value="fa-ra">&#xf1d0; Ra</option>
			<option class='awesome' value="fa-random">&#xf074; Random</option>
			<option class='awesome' value="fa-rebel">&#xf1d0; Rebel</option>
			<option class='awesome' value="fa-recycle">&#xf1b8; Recycle</option>
			<option class='awesome' value="fa-reddit">&#xf1a1; Reddit</option>
			<option class='awesome' value="fa-reddit-alien">&#xf281; Reddit-Alien</option>
			<option class='awesome' value="fa-reddit-square">&#xf1a2; Reddit-Square</option>
			<option class='awesome' value="fa-refresh">&#xf021; Refresh</option>
			<option class='awesome' value="fa-registered">&#xf25d; Registered</option>
			<option class='awesome' value="fa-remove">&#xf00d; Remove</option>
			<option class='awesome' value="fa-renren">&#xf18b; Renren</option>
			<option class='awesome' value="fa-reorder">&#xf0c9; Reorder</option>
			<option class='awesome' value="fa-repeat">&#xf01e; Repeat</option>
			<option class='awesome' value="fa-reply">&#xf112; Reply</option>
			<option class='awesome' value="fa-reply-all">&#xf122; Reply-All</option>
			<option class='awesome' value="fa-resistance">&#xf1d0; Resistance</option>
			<option class='awesome' value="fa-retweet">&#xf079; Retweet</option>
			<option class='awesome' value="fa-rmb">&#xf157; Rmb</option>
			<option class='awesome' value="fa-road">&#xf018; Road</option>
			<option class='awesome' value="fa-rocket">&#xf135; Rocket</option>
			<option class='awesome' value="fa-rotate-left">&#xf0e2; Rotate-Left</option>
			<option class='awesome' value="fa-rotate-right">&#xf01e; Rotate-Right</option>
			<option class='awesome' value="fa-rouble">&#xf158; Rouble</option>
			<option class='awesome' value="fa-rss">&#xf09e; Rss</option>
			<option class='awesome' value="fa-rss-square">&#xf143; Rss-Square</option>
			<option class='awesome' value="fa-rub">&#xf158; Rub</option>
			<option class='awesome' value="fa-ruble">&#xf158; Ruble</option>
			<option class='awesome' value="fa-rupee">&#xf156; Rupee</option>
			<option class='awesome' value="fa-safari">&#xf267; Safari</option>
			<option class='awesome' value="fa-save">&#xf0c7; Save</option>
			<option class='awesome' value="fa-scissors">&#xf0c4; Scissors</option>
			<option class='awesome' value="fa-scribd">&#xf28a; Scribd</option>
			<option class='awesome' value="fa-search">&#xf002; Search</option>
			<option class='awesome' value="fa-search-minus">&#xf010; Search-Minus</option>
			<option class='awesome' value="fa-search-plus">&#xf00e; Search-Plus</option>
			<option class='awesome' value="fa-sellsy">&#xf213; Sellsy</option>
			<option class='awesome' value="fa-send">&#xf1d8; Send</option>
			<option class='awesome' value="fa-send-o">&#xf1d9; Send-O</option>
			<option class='awesome' value="fa-server">&#xf233; Server</option>
			<option class='awesome' value="fa-share">&#xf064; Share</option>
			<option class='awesome' value="fa-share-alt">&#xf1e0; Share-Alt</option>
			<option class='awesome' value="fa-share-alt-square">&#xf1e1; Share-Alt-Square</option>
			<option class='awesome' value="fa-share-square">&#xf14d; Share-Square</option>
			<option class='awesome' value="fa-share-square-o">&#xf045; Share-Square-O</option>
			<option class='awesome' value="fa-shekel">&#xf20b; Shekel</option>
			<option class='awesome' value="fa-sheqel">&#xf20b; Sheqel</option>
			<option class='awesome' value="fa-shield">&#xf132; Shield</option>
			<option class='awesome' value="fa-ship">&#xf21a; Ship</option>
			<option class='awesome' value="fa-shirtsinbulk">&#xf214; Shirtsinbulk</option>
			<option class='awesome' value="fa-shopping-bag">&#xf290; Shopping-Bag</option>
			<option class='awesome' value="fa-shopping-basket">&#xf291; Shopping-Basket</option>
			<option class='awesome' value="fa-shopping-cart">&#xf07a; Shopping-Cart</option>
			<option class='awesome' value="fa-sign-in">&#xf090; Sign-In</option>
			<option class='awesome' value="fa-sign-language">&#xf2a7; Sign-Language</option>
			<option class='awesome' value="fa-sign-out">&#xf08b; Sign-Out</option>
			<option class='awesome' value="fa-signal">&#xf012; Signal</option>
			<option class='awesome' value="fa-signing">&#xf2a7; Signing</option>
			<option class='awesome' value="fa-simplybuilt">&#xf215; Simplybuilt</option>
			<option class='awesome' value="fa-sitemap">&#xf0e8; Sitemap</option>
			<option class='awesome' value="fa-skyatlas">&#xf216; Skyatlas</option>
			<option class='awesome' value="fa-skype">&#xf17e; Skype</option>
			<option class='awesome' value="fa-slack">&#xf198; Slack</option>
			<option class='awesome' value="fa-sliders">&#xf1de; Sliders</option>
			<option class='awesome' value="fa-slideshare">&#xf1e7; Slideshare</option>
			<option class='awesome' value="fa-smile-o">&#xf118; Smile-O</option>
			<option class='awesome' value="fa-snapchat">&#xf2ab; Snapchat</option>
			<option class='awesome' value="fa-snapchat-ghost">&#xf2ac; Snapchat-Ghost</option>
			<option class='awesome' value="fa-snapchat-square">&#xf2ad; Snapchat-Square</option>
			<option class='awesome' value="fa-soccer-ball-o">&#xf1e3; Soccer-Ball-O</option>
			<option class='awesome' value="fa-sort">&#xf0dc; Sort</option>
			<option class='awesome' value="fa-sort-alpha-asc">&#xf15d; Sort-Alpha-Asc</option>
			<option class='awesome' value="fa-sort-alpha-desc">&#xf15e; Sort-Alpha-Desc</option>
			<option class='awesome' value="fa-sort-amount-asc">&#xf160; Sort-Amount-Asc</option>
			<option class='awesome' value="fa-sort-amount-desc">&#xf161; Sort-Amount-Desc</option>
			<option class='awesome' value="fa-sort-asc">&#xf0de; Sort-Asc</option>
			<option class='awesome' value="fa-sort-desc">&#xf0dd; Sort-Desc</option>
			<option class='awesome' value="fa-sort-down">&#xf0dd; Sort-Down</option>
			<option class='awesome' value="fa-sort-numeric-asc">&#xf162; Sort-Numeric-Asc</option>
			<option class='awesome' value="fa-sort-numeric-desc">&#xf163; Sort-Numeric-Desc</option>
			<option class='awesome' value="fa-sort-up">&#xf0de; Sort-Up</option>
			<option class='awesome' value="fa-soundcloud">&#xf1be; Soundcloud</option>
			<option class='awesome' value="fa-space-shuttle">&#xf197; Space-Shuttle</option>
			<option class='awesome' value="fa-spinner">&#xf110; Spinner</option>
			<option class='awesome' value="fa-spoon">&#xf1b1; Spoon</option>
			<option class='awesome' value="fa-spotify">&#xf1bc; Spotify</option>
			<option class='awesome' value="fa-square">&#xf0c8; Square</option>
			<option class='awesome' value="fa-square-o">&#xf096; Square-O</option>
			<option class='awesome' value="fa-stack-exchange">&#xf18d; Stack-Exchange</option>
			<option class='awesome' value="fa-stack-overflow">&#xf16c; Stack-Overflow</option>
			<option class='awesome' value="fa-star">&#xf005; Star</option>
			<option class='awesome' value="fa-star-half">&#xf089; Star-Half</option>
			<option class='awesome' value="fa-star-half-empty">&#xf123; Star-Half-Empty</option>
			<option class='awesome' value="fa-star-half-full">&#xf123; Star-Half-Full</option>
			<option class='awesome' value="fa-star-half-o">&#xf123; Star-Half-O</option>
			<option class='awesome' value="fa-star-o">&#xf006; Star-O</option>
			<option class='awesome' value="fa-steam">&#xf1b6; Steam</option>
			<option class='awesome' value="fa-steam-square">&#xf1b7; Steam-Square</option>
			<option class='awesome' value="fa-step-backward">&#xf048; Step-Backward</option>
			<option class='awesome' value="fa-step-forward">&#xf051; Step-Forward</option>
			<option class='awesome' value="fa-stethoscope">&#xf0f1; Stethoscope</option>
			<option class='awesome' value="fa-sticky-note">&#xf249; Sticky-Note</option>
			<option class='awesome' value="fa-sticky-note-o">&#xf24a; Sticky-Note-O</option>
			<option class='awesome' value="fa-stop">&#xf04d; Stop</option>
			<option class='awesome' value="fa-stop-circle">&#xf28d; Stop-Circle</option>
			<option class='awesome' value="fa-stop-circle-o">&#xf28e; Stop-Circle-O</option>
			<option class='awesome' value="fa-street-view">&#xf21d; Street-View</option>
			<option class='awesome' value="fa-strikethrough">&#xf0cc; Strikethrough</option>
			<option class='awesome' value="fa-stumbleupon">&#xf1a4; Stumbleupon</option>
			<option class='awesome' value="fa-stumbleupon-circle">&#xf1a3; Stumbleupon-Circle</option>
			<option class='awesome' value="fa-subscript">&#xf12c; Subscript</option>
			<option class='awesome' value="fa-subway">&#xf239; Subway</option>
			<option class='awesome' value="fa-suitcase">&#xf0f2; Suitcase</option>
			<option class='awesome' value="fa-sun-o">&#xf185; Sun-O</option>
			<option class='awesome' value="fa-superscript">&#xf12b; Superscript</option>
			<option class='awesome' value="fa-support">&#xf1cd; Support</option>
			<option class='awesome' value="fa-table">&#xf0ce; Table</option>
			<option class='awesome' value="fa-tablet">&#xf10a; Tablet</option>
			<option class='awesome' value="fa-tachometer">&#xf0e4; Tachometer</option>
			<option class='awesome' value="fa-tag">&#xf02b; Tag</option>
			<option class='awesome' value="fa-tags">&#xf02c; Tags</option>
			<option class='awesome' value="fa-tasks">&#xf0ae; Tasks</option>
			<option class='awesome' value="fa-taxi">&#xf1ba; Taxi</option>
			<option class='awesome' value="fa-television">&#xf26c; Television</option>
			<option class='awesome' value="fa-tencent-weibo">&#xf1d5; Tencent-Weibo</option>
			<option class='awesome' value="fa-terminal">&#xf120; Terminal</option>
			<option class='awesome' value="fa-text-height">&#xf034; Text-Height</option>
			<option class='awesome' value="fa-text-width">&#xf035; Text-Width</option>
			<option class='awesome' value="fa-th">&#xf00a; Th</option>
			<option class='awesome' value="fa-th-large">&#xf009; Th-Large</option>
			<option class='awesome' value="fa-th-list">&#xf00b; Th-List</option>
			<option class='awesome' value="fa-themeisle">&#xf2b2; Themeisle</option>
			<option class='awesome' value="fa-thumb-tack">&#xf08d; Thumb-Tack</option>
			<option class='awesome' value="fa-thumbs-down">&#xf165; Thumbs-Down</option>
			<option class='awesome' value="fa-thumbs-o-down">&#xf088; Thumbs-O-Down</option>
			<option class='awesome' value="fa-thumbs-o-up">&#xf087; Thumbs-O-Up</option>
			<option class='awesome' value="fa-thumbs-up">&#xf164; Thumbs-Up</option>
			<option class='awesome' value="fa-ticket">&#xf145; Ticket</option>
			<option class='awesome' value="fa-times">&#xf00d; Times</option>
			<option class='awesome' value="fa-times-circle">&#xf057; Times-Circle</option>
			<option class='awesome' value="fa-times-circle-o">&#xf05c; Times-Circle-O</option>
			<option class='awesome' value="fa-tint">&#xf043; Tint</option>
			<option class='awesome' value="fa-toggle-down">&#xf150; Toggle-Down</option>
			<option class='awesome' value="fa-toggle-left">&#xf191; Toggle-Left</option>
			<option class='awesome' value="fa-toggle-off">&#xf204; Toggle-Off</option>
			<option class='awesome' value="fa-toggle-on">&#xf205; Toggle-On</option>
			<option class='awesome' value="fa-toggle-right">&#xf152; Toggle-Right</option>
			<option class='awesome' value="fa-toggle-up">&#xf151; Toggle-Up</option>
			<option class='awesome' value="fa-trademark">&#xf25c; Trademark</option>
			<option class='awesome' value="fa-train">&#xf238; Train</option>
			<option class='awesome' value="fa-transgender">&#xf224; Transgender</option>
			<option class='awesome' value="fa-transgender-alt">&#xf225; Transgender-Alt</option>
			<option class='awesome' value="fa-trash">&#xf1f8; Trash</option>
			<option class='awesome' value="fa-trash-o">&#xf014; Trash-O</option>
			<option class='awesome' value="fa-tree">&#xf1bb; Tree</option>
			<option class='awesome' value="fa-trello">&#xf181; Trello</option>
			<option class='awesome' value="fa-tripadvisor">&#xf262; Tripadvisor</option>
			<option class='awesome' value="fa-trophy">&#xf091; Trophy</option>
			<option class='awesome' value="fa-truck">&#xf0d1; Truck</option>
			<option class='awesome' value="fa-try">&#xf195; Try</option>
			<option class='awesome' value="fa-tty">&#xf1e4; Tty</option>
			<option class='awesome' value="fa-tumblr">&#xf173; Tumblr</option>
			<option class='awesome' value="fa-tumblr-square">&#xf174; Tumblr-Square</option>
			<option class='awesome' value="fa-turkish-lira">&#xf195; Turkish-Lira</option>
			<option class='awesome' value="fa-tv">&#xf26c; Tv</option>
			<option class='awesome' value="fa-twitch">&#xf1e8; Twitch</option>
			<option class='awesome' value="fa-twitter">&#xf099; Twitter</option>
			<option class='awesome' value="fa-twitter-square">&#xf081; Twitter-Square</option>
			<option class='awesome' value="fa-umbrella">&#xf0e9; Umbrella</option>
			<option class='awesome' value="fa-underline">&#xf0cd; Underline</option>
			<option class='awesome' value="fa-undo">&#xf0e2; Undo</option>
			<option class='awesome' value="fa-universal-access">&#xf29a; Universal-Access</option>
			<option class='awesome' value="fa-university">&#xf19c; University</option>
			<option class='awesome' value="fa-unlink">&#xf127; Unlink</option>
			<option class='awesome' value="fa-unlock">&#xf09c; Unlock</option>
			<option class='awesome' value="fa-unlock-alt">&#xf13e; Unlock-Alt</option>
			<option class='awesome' value="fa-unsorted">&#xf0dc; Unsorted</option>
			<option class='awesome' value="fa-upload">&#xf093; Upload</option>
			<option class='awesome' value="fa-usb">&#xf287; Usb</option>
			<option class='awesome' value="fa-usd">&#xf155; Usd</option>
			<option class='awesome' value="fa-user">&#xf007; User</option>
			<option class='awesome' value="fa-user-md">&#xf0f0; User-Md</option>
			<option class='awesome' value="fa-user-plus">&#xf234; User-Plus</option>
			<option class='awesome' value="fa-user-secret">&#xf21b; User-Secret</option>
			<option class='awesome' value="fa-user-times">&#xf235; User-Times</option>
			<option class='awesome' value="fa-users">&#xf0c0; Users</option>
			<option class='awesome' value="fa-venus">&#xf221; Venus</option>
			<option class='awesome' value="fa-venus-double">&#xf226; Venus-Double</option>
			<option class='awesome' value="fa-venus-mars">&#xf228; Venus-Mars</option>
			<option class='awesome' value="fa-viacoin">&#xf237; Viacoin</option>
			<option class='awesome' value="fa-viadeo">&#xf2a9; Viadeo</option>
			<option class='awesome' value="fa-viadeo-square">&#xf2aa; Viadeo-Square</option>
			<option class='awesome' value="fa-video-camera">&#xf03d; Video-Camera</option>
			<option class='awesome' value="fa-vimeo">&#xf27d; Vimeo</option>
			<option class='awesome' value="fa-vimeo-square">&#xf194; Vimeo-Square</option>
			<option class='awesome' value="fa-vine">&#xf1ca; Vine</option>
			<option class='awesome' value="fa-vk">&#xf189; Vk</option>
			<option class='awesome' value="fa-volume-control-phone">&#xf2a0; Volume-Control-Phone</option>
			<option class='awesome' value="fa-volume-down">&#xf027; Volume-Down</option>
			<option class='awesome' value="fa-volume-off">&#xf026; Volume-Off</option>
			<option class='awesome' value="fa-volume-up">&#xf028; Volume-Up</option>
			<option class='awesome' value="fa-warning">&#xf071; Warning</option>
			<option class='awesome' value="fa-wechat">&#xf1d7; Wechat</option>
			<option class='awesome' value="fa-weibo">&#xf18a; Weibo</option>
			<option class='awesome' value="fa-weixin">&#xf1d7; Weixin</option>
			<option class='awesome' value="fa-whatsapp">&#xf232; Whatsapp</option>
			<option class='awesome' value="fa-wheelchair">&#xf193; Wheelchair</option>
			<option class='awesome' value="fa-wheelchair-alt">&#xf29b; Wheelchair-Alt</option>
			<option class='awesome' value="fa-wifi">&#xf1eb; Wifi</option>
			<option class='awesome' value="fa-wikipedia-w">&#xf266; Wikipedia-W</option>
			<option class='awesome' value="fa-windows">&#xf17a; Windows</option>
			<option class='awesome' value="fa-won">&#xf159; Won</option>
			<option class='awesome' value="fa-wordpress">&#xf19a; Wordpress</option>
			<option class='awesome' value="fa-wpbeginner">&#xf297; Wpbeginner</option>
			<option class='awesome' value="fa-wpforms">&#xf298; Wpforms</option>
			<option class='awesome' value="fa-wrench">&#xf0ad; Wrench</option>
			<option class='awesome' value="fa-xing">&#xf168; Xing</option>
			<option class='awesome' value="fa-xing-square">&#xf169; Xing-Square</option>
			<option class='awesome' value="fa-y-combinator">&#xf23b; Y-Combinator</option>
			<option class='awesome' value="fa-y-combinator-square">&#xf1d4; Y-Combinator-Square</option>
			<option class='awesome' value="fa-yahoo">&#xf19e; Yahoo</option>
			<option class='awesome' value="fa-yc">&#xf23b; Yc</option>
			<option class='awesome' value="fa-yc-square">&#xf1d4; Yc-Square</option>
			<option class='awesome' value="fa-yelp">&#xf1e9; Yelp</option>
			<option class='awesome' value="fa-yen">&#xf157; Yen</option>
			<option class='awesome' value="fa-yoast">&#xf2b1; Yoast</option>
			<option class='awesome' value="fa-youtube">&#xf167; Youtube</option>
			<option class='awesome' value="fa-youtube-play">&#xf16a; Youtube-Play</option>
			<option class='awesome' value="fa-youtube-square">&#xf166; Youtube-Square</option>
                                    </select>
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
                        <asp:TemplateField HeaderText="Language Name">
                            <ItemTemplate>
                                <asp:Label ID="EditLanguageNameLabel" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="EditLanguageNameTextBox" runat="server" CssClass="form-control input-sm" Text='<%# Eval("Name") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Display on Questionnaire?" HeaderStyle-Width="25%">
                            <ItemTemplate>
                                <asp:Label ID="EditDisplayLanguageLabel" runat="server" Text='<%# Eval("ActiveFlag").ToString() == "True" ? "Yes" : "No" %>'></asp:Label>
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
                        <asp:TemplateField HeaderText="Role Name">
                            <ItemTemplate>
                                <asp:Label ID="EditRoleNameLabel" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="EditRoleNameTextBox" runat="server" CssClass="form-control input-sm" Text='<%# Eval("Name") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Display on Questionnaire?" HeaderStyle-Width="25%">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("ActiveFlag").ToString() == "True" ? "Yes" : "No" %>'></asp:Label>
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
                                    <asp:Label ID="SkillNameLabel" CssClass="control-label" runat="server">Skill Name: <span class="text-muted small">(ex. Client Expectation Management)</span> </asp:Label>
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
                        <asp:TemplateField HeaderText="Skill Name">
                            <ItemTemplate>
                                <asp:Label ID="EditSkillNameLabel" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="EditSkillNameTextBox" runat="server" CssClass="form-control input-sm" Text='<%# Eval("Name") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Display on Questionnaire?" HeaderStyle-Width="25%">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("ActiveFlag").ToString() == "True" ? "Yes" : "No" %>'></asp:Label>
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
