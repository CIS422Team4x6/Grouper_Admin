using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GroupBuilder;
using System.Web.Services;
using System.Web.Script.Services;

namespace GroupBuilderAdmin
{
    public partial class Groups : System.Web.UI.Page
    {
        #region Page Variables and Properties

        private static int RECOMMENDED_GROUP_SIZE = 5;

        private int _InstructorCourseID;
        public int InstructorCourseID
        {
            get
            {
                _InstructorCourseID = 0;

                if (Request.QueryString["ID"] != "" && Request.QueryString["ID"] != null)
                {
                    _InstructorCourseID = int.Parse(Request.QueryString["ID"]);
                }

                return _InstructorCourseID;
            }
            set
            {
                _InstructorCourseID = value;
            }
        }

        //private List<ProgrammingLanguage> _Languages;
        //public List<ProgrammingLanguage> Languages
        //{
        //    get
        //    {
        //        if (ViewState["Languages"] != null)
        //        {
        //            _Languages = (List<ProgrammingLanguage>)ViewState["Languages"];
        //        }
        //        else
        //        {
        //            _Languages = GrouperMethods.GetLanguages();
        //            ViewState["Languages"] = _Languages;
        //        }
        //        return _Languages;
        //    }
        //    set
        //    {
        //        _Languages = value;
        //        ViewState["Languages"] = _Languages;
        //    }
        //}

        //private List<Role> _Roles;
        //public List<Role> Roles
        //{
        //    get
        //    {
        //        if (ViewState["Roles"] != null)
        //        {
        //            _Roles = (List<Role>)ViewState["Roles"];
        //        }
        //        else
        //        {
        //            _Roles = GrouperMethods.GetRoles();
        //            ViewState["Roles"] = _Roles;
        //        }
        //        return _Roles;
        //    }
        //    set
        //    {
        //        _Roles = value;
        //        ViewState["Roles"] = _Roles;
        //    }
        //}

        private List<Course> _Courses;
        public List<Course> Courses
        {
            get
            {
                if (_Courses == null)
                {
                    if (Session["Courses"] != null)
                    {
                        _Courses = (List<Course>)Session["Courses"];
                    }
                    else
                    {
                        _Courses = GrouperMethods.GetAllCourses();
                        Session["Courses"] = _Courses;
                    }
                }
                return _Courses;
            }
            set
            {
                _Courses = value;
                Session["Courses"] = _Courses;
            }
        }

        private List<Role> _Roles;
        public List<Role> Roles
        {
            get
            {
                if (_Roles == null)
                {
                    if (Session["Roles"] != null)
                    {
                        _Roles = (List<Role>)Session["Roles"];
                    }
                    else
                    {
                        _Roles = GrouperMethods.GetAllRoles();
                        Session["Roles"] = _Roles;
                    }
                }
                return _Roles;
            }
            set
            {
                _Roles = value;
                Session["Roles"] = _Roles;
            }
        }

        private List<ProgrammingLanguage> _Languages;
        public List<ProgrammingLanguage> Languages
        {
            get
            {
                if (_Languages == null)
                {
                    if (Session["Languages"] != null)
                    {
                        _Languages = (List<ProgrammingLanguage>)Session["Languages"];
                    }
                    else
                    {
                        _Languages = GrouperMethods.GetAllLanguages();
                        Session["Languages"] = _Languages;
                    }
                }
                return _Languages;
            }
            set
            {
                _Languages = value;
                Session["Languages"] = _Languages;
            }
        }

        private List<Skill> _Skills;
        public List<Skill> Skills
        {
            get
            {
                if (_Skills == null)
                {
                    if (Session["Skills"] != null)
                    {
                        _Skills = (List<Skill>)Session["Skills"];
                    }
                    else
                    {
                        _Skills = GrouperMethods.GetAllSkills();
                        Session["Skills"] = _Skills;
                    }
                }
                return _Skills;
            }
            set
            {
                _Skills = value;
                Session["Skills"] = _Skills;
            }
        }

        private InstructorCourse _InstructorCourse;
        public InstructorCourse InstructorCourse
        {
            get
            {
                if (Request.QueryString["ID"] != "" && Request.QueryString["ID"] != null)
                {
                    int instructorCourseID = int.Parse(Request.QueryString["ID"]);

                    InstructorCourseID = instructorCourseID;

                    _InstructorCourse = GrouperMethods.GetInstructorCourse(instructorCourseID);

                }
                return _InstructorCourse;
            }
            set
            {
                _InstructorCourse = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != "" && Request.QueryString["ID"] != null)
            {
                int instructorCourseID = int.Parse(Request.QueryString["ID"]);

                InstructorCourseIDHiddenField.Value = instructorCourseID.ToString();

                InstructorCourseID = instructorCourseID;
            }

            if (!IsPostBack)
            {
                InstructorCourse instructorCourse = GrouperMethods.GetInstructorCourse(InstructorCourseID);

                if (instructorCourse != null)
                {
                    string link = "mailto:";
                    foreach (Student student in instructorCourse.Students)
                    {
                        link += student.DuckID + "@uoregon.edu,";
                    }
                    link = link.Substring(0, link.Length - 1);

                    MailEntireClassLinkButton.NavigateUrl = link;

                    if (instructorCourse.Groups.Count == 0)
                    {
                        int groupCount = instructorCourse.Students.Count / RECOMMENDED_GROUP_SIZE;

                        if (groupCount > 0)
                        {
                            NumberOfGroupsDropDownList.SelectedValue = groupCount.ToString();
                            RecommendedGroupAmountLabel.Text = "You have " + instructorCourse.Students.Count + " students. The recommended amount of groups for a class this size is " + groupCount + " (recommended group size is 5).";
                        }
                        else
                        {
                            groupCount = 1;
                            NumberOfGroupsDropDownList.SelectedValue = groupCount.ToString();
                            RecommendedGroupAmountLabel.Text = "You have " + instructorCourse.Students.Count + " students. The recommended amount of groups for a class this size is " + groupCount + " (recommended group size is 5).";
                        }

                        CreateGroupsPanel.Visible = true;
                        ListPanel.Visible = false;
                    }
                    else
                    {
                        CreateGroupsPanel.Visible = false;

                        List<Student> ungroupedStudents = GrouperMethods.GetUngroupedStudents(InstructorCourseID);
                        if (ungroupedStudents.Count == 0)
                        {
                            ListPanel.Visible = true;
                            GroupingPanel.Visible = false;
                        }
                        else
                        {
                            ListPanel.Visible = false;
                            GroupingPanel.Visible = true;
                        }
                    }
                }

                if (instructorCourse != null)
                {
                    Course course = Courses.FirstOrDefault(x => x.CourseID == instructorCourse.CourseID);
                    if (course != null)
                    {
                        CourseNameLabel.Text = course.FullName;
                    }

                    LanguagesDropDownList.DataSource = Languages;
                    LanguagesDropDownList.DataBind();

                    RolesDropDownList.DataSource = Roles;
                    RolesDropDownList.DataBind();

                    GroupsRepeater_BindRepeater();
                    GroupsGridView_BindGridView();
                }
            }

            if (InstructorCourse != null)
            {
                StudentsGridView_BindGridView();
            }
        }

        #region Modal Dialogs

        private void MessageBox(string title, string message, string button)
        {
            MessageBoxTitleLabel.Text = title;
            MessageBoxMessageLabel.Text = message;
            MessageBoxOkayLinkButton.Text = button;
            MessageBoxOkayLinkButton.Visible = true;
            MessageBoxCreateLinkButton.Visible = false;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "messageBox", "$('#messageBox').modal();", true);
            upModal.Update();
        }

        private void ConfirmDeleteAllGroupsMessageBox()
        {
            SelectedGroupIDHiddenField.Value = null;
            SelectedGroupIDHiddenField.Value = null;
            MessageBoxTitleLabel.Text = "Delete All Groups?";
            MessageBoxMessageLabel.Text = "Are you sure you want to delete all created groups and start over?";
            MessageBoxOkayLinkButton.Text = "<span class='fa fa-ban'></span>&nbsp;&nbsp;Cancel";
            MessageBoxOkayLinkButton.Visible = true;
            MessageBoxCreateLinkButton.CssClass = "btn btn-danger";
            MessageBoxCreateLinkButton.Text = "<span class='fa fa-remove'></span>&nbsp;&nbsp;Delete All Groups";
            MessageBoxCreateLinkButton.Visible = true;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "messageBox", "$('#messageBox').modal();", true);
            upModal.Update();
        }

        private void ConfirmDeleteMessageBox(Group group)
        {
            SelectedGroupIDHiddenField.Value = group.GroupID.ToString();
            MessageBoxTitleLabel.Text = "Delete Group?";
            if(!string.IsNullOrEmpty(group.Name))
            {
                MessageBoxMessageLabel.Text = "Are you sure you want to delete the group '" + group.Name + "'?<br /><br />This will not actually delete the group, but will remove all group members and delete the group name.";
            }
            else
            {
                MessageBoxMessageLabel.Text = "Are you sure you want to delete group " + group.GroupNumber.ToString() + "<br /><br />This will not actually delete the group, but will remove all group members.";
            }
            MessageBoxOkayLinkButton.Text = "<span class='fa fa-ban'></span>&nbsp;&nbsp;Cancel";
            MessageBoxOkayLinkButton.Visible = true;
            MessageBoxCreateLinkButton.CssClass = "btn btn-danger";
            MessageBoxCreateLinkButton.Text = "<span class='fa fa-remove'></span>&nbsp;&nbsp;Delete Group";
            MessageBoxCreateLinkButton.Visible = true;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "messageBox", "$('#messageBox').modal();", true);
            upModal.Update();
        }

        private void ConfirmDeleteMessageBox(Student student)
        {
            SelectedStudentIDHiddenField.Value = student.StudentID.ToString();
            MessageBoxTitleLabel.Text = "Remove Student From Group?";
            MessageBoxMessageLabel.Text = "Are you sure you want to remove group member '" + student.FirstName + " " + student.LastName + "' from their current group?<br /><br />This will not actually delete the student (this must be done from the Students page).";
            MessageBoxOkayLinkButton.Text = "<span class='fa fa-ban'></span>&nbsp;&nbsp;Cancel";
            MessageBoxOkayLinkButton.Visible = true;
            MessageBoxCreateLinkButton.CssClass = "btn btn-danger";
            MessageBoxCreateLinkButton.Text = "<span class='fa fa-remove'></span>&nbsp;&nbsp;Remove Group Member";
            MessageBoxCreateLinkButton.Visible = true;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "messageBox", "$('#messageBox').modal();", true);
            upModal.Update();
        }

        protected void MessageBoxCreateLinkButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SelectedGroupIDHiddenField.Value))
            {
                int groupID = int.Parse(SelectedGroupIDHiddenField.Value);

                Group group = InstructorCourse.Groups.FirstOrDefault(x => x.GroupID == groupID);

                if (group != null)
                {
                    foreach (Student member in group.Members)
                    {
                        GrouperMethods.DeleteGroupMember(member.StudentID);
                    }
                    group.Name = "Group " + group.GroupNumber;
                    GrouperMethods.UpdateGroup(group);
                }

                _InstructorCourse = null;

                GroupsGridView_BindGridView();

                MessageBox("Group Deleted", "The group has been deleted.", "Okay");
            }
            else if (!string.IsNullOrEmpty(SelectedStudentIDHiddenField.Value))
            {
                int studentID = int.Parse(SelectedStudentIDHiddenField.Value);

                Student student = InstructorCourse.Students.FirstOrDefault(x => x.StudentID == studentID);

                GrouperMethods.DeleteGroupMember(studentID);

                _InstructorCourse = null;

                GroupsGridView_BindGridView();

                MessageBox("Group Member Removed", "'" + student.FirstName + " " + student.LastName + "' has been removed from their group.", "Okay");
            }
            else
            {
                foreach (Group group in InstructorCourse.Groups)
                {
                    GrouperMethods.DeleteGroup(group.GroupID);
                }

                StudentsGridView_BindGridView();
                GroupsRepeater_BindRepeater();

                ListPanel.Visible = false;
                GroupingPanel.Visible = false;

                int groupCount = InstructorCourse.Students.Count / RECOMMENDED_GROUP_SIZE;

                if (groupCount > 0)
                {
                    NumberOfGroupsDropDownList.SelectedValue = groupCount.ToString();
                }
                else
                {
                    NumberOfGroupsDropDownList.SelectedValue = 1.ToString();
                    groupCount = 1;
                }

                RecommendedGroupAmountLabel.Text = "You have " + InstructorCourse.Students.Count + " students. The recommended amount of groups for a class this size is " + groupCount + " (recommended group size is 5).";
                CreateGroupsPanel.Visible = true;

                MessageBox("Groups Deleted", "All groups have been deleted.", "Okay");
            }
        }

        #endregion

        #region Ajax Methods

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static int UpdateGroups(int instructorCourseID, int groupNumber, string[] studentIDs)
        {
            int existingGroup = 0;

            InstructorCourse instructorCourse = GrouperMethods.GetInstructorCourse(instructorCourseID);

            Group priorGroup = instructorCourse.Groups.FirstOrDefault(x => x.Members.Exists(y => y.StudentID == int.Parse(studentIDs[0])));

            if (priorGroup != null)
            {
                existingGroup = instructorCourse.Groups.IndexOf(priorGroup);

                GrouperMethods.DeleteGroupMember(int.Parse(studentIDs[0]));
            }

            Group group = instructorCourse.Groups.FirstOrDefault(x => x.GroupNumber == groupNumber + 1);
            int groupID = 0;

            if (group == null)
            {
                group = new Group();
                group.InstructorCourseID = instructorCourseID;
                group.GroupNumber = groupNumber + 1;

                groupID = GrouperMethods.InsertGroup(group);

                group.GroupID = groupID;
            }
            else
            {
                groupID = group.GroupID;
            }

            for (int i = 0; i < studentIDs.Length; i++)
            {
                int studentID = int.Parse(studentIDs[i]);
                GrouperMethods.InsertGroupMember(groupID, studentID);
            }
            return existingGroup;
        }

        [WebMethod]
        public static void DeleteStudentFromGroups(int studentID)
        {
            GrouperMethods.DeleteGroupMember(studentID);
        }

        [WebMethod]
        public static Student[] GetGroups(int instructorCourseID)
        {
            InstructorCourse instructorCourse = GrouperMethods.GetInstructorCourse(instructorCourseID);
            return instructorCourse.Groups[0].Members.ToArray();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Student[] GetGroup(int instructorCourseID, int groupNumber)
        {
            InstructorCourse instructorCourse = GrouperMethods.GetInstructorCourse(instructorCourseID);

            Group group = instructorCourse.Groups.FirstOrDefault(x => x.GroupNumber == groupNumber);
            if (group != null)
            {
                return group.Members.ToArray();
            }
            else
            {
                return null;
            }

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetStats(int instructorCourseID, int groupNumber)
        {
            InstructorCourse instructorCourse = GrouperMethods.GetInstructorCourse(instructorCourseID);

            Group group = instructorCourse.Groups.FirstOrDefault(x => x.GroupNumber == groupNumber);
            List<ProgrammingLanguage> languages = GrouperMethods.GetAllLanguages();

            string stats = "";

            if (group != null)
            {
                List<Student> students = group.Members;

                List<double> gpaList = new List<double>();
                foreach (Student student in students)
                {
                    if (!string.IsNullOrEmpty(student.GPA))
                    {
                        double gpa = double.Parse(student.GPA);
                        gpaList.Add(gpa);
                    }
                }

                double averageGPA = 0.0;

                if (gpaList.Count > 0)
                {
                    double lowGpa = gpaList.Min();
                    double highGpa = gpaList.Max();
                    double difference = highGpa - lowGpa;

                    averageGPA = gpaList.Average();
                    stats = "<b>GPA Average:</b> " + averageGPA.ToString("#.##") + "&nbsp;&nbsp;<b>GPA Range:</b> " + difference.ToString("#.##") + "<br />";
                }

                int languagesInCommon = 0;
                int languageLevel = 0;
                string languageName = "";

                foreach (ProgrammingLanguage language in languages)
                {
                    List<Student> languageStudents = students.Where(x => x.Languages.Exists(y => y.LanguageID == language.LanguageID)).ToList();
                    
                    int inCommon = languageStudents.Count();

                    int level = (int)languageStudents.Sum(x => x.Languages.First(y => y.LanguageID == language.LanguageID).ProficiencyLevel);

                    if (inCommon > languagesInCommon)
                    {
                        languagesInCommon = inCommon;
                        languageLevel = level;
                        languageName = language.Icon;
                    }
                    else if(inCommon == languagesInCommon && level > languageLevel)
                    {
                        languagesInCommon = inCommon;
                        languageLevel = level;
                        languageName = language.Icon;
                    }
                }

                decimal percentageInCommon = 0;

                if (languagesInCommon > 0)
                {
                    percentageInCommon = decimal.Divide(languagesInCommon, students.Count) * 100;
                    stats += "<b>Language Match: </b>" + percentageInCommon.ToString("#.##") + "%&nbsp;&nbsp;<b>Language: </b>" + languageName;
                }
            }
            return stats;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Student[] GetStudents(int? instructorCourseID, int? sortLanguageID, string sortGPA, int? sortRoleID)
        {
            List<Student> students = GrouperMethods.GetUngroupedStudents((int)instructorCourseID);

            if (sortLanguageID > 0)
            {
                students = students.OrderByDescending(x => x.Languages.Exists(y => y.LanguageID == sortLanguageID)).ToList();
            }

            return students.ToArray();
        }

        #endregion

        #region Create Groups View

        // Creates the selected number of groups
        protected void BuildGroupsLinkButton_Click(object sender, EventArgs e)
        {
            //InstructorCourse course = GrouperMethods.GetInstructorCourse(InstructorCourseID);
            //InstructorCourse
            int numberOfGroups = int.Parse(NumberOfGroupsDropDownList.SelectedValue);

            if (numberOfGroups > InstructorCourse.Groups.Count)
            {
                for (int i = 0; i < numberOfGroups; i++)
                {
                    Group group = new GroupBuilder.Group();
                    group.InstructorCourseID = InstructorCourseID;
                    group.GroupNumber = i + 1;
                    group.Name = "Group " + (i + 1).ToString();
                    GrouperMethods.InsertGroup(group);
                }
            }
            else if (numberOfGroups < InstructorCourse.Groups.Count)
            {
                for (int i = numberOfGroups; i < InstructorCourse.Groups.Count; i++)
                {
                    GrouperMethods.DeleteGroup(InstructorCourse.Groups[i].GroupID);
                }
                StudentsGridView_BindGridView();
            }

            CreateGroupsPanel.Visible = false;
            GroupingPanel.Visible = true;

            GroupsRepeater_BindRepeater();
        }

        #endregion

        #region Edit Groups View

        #region StudentsGridView

        protected void StudentsGridView_BindGridView()
        {
            StudentsGridView.DataSource = GrouperMethods.GetUngroupedStudents(InstructorCourse.InstructorCourseID);
            StudentsGridView.DataBind();
        }

        #endregion

        #region GroupsRepeater 

        // Closes Bootstrap rows after each third group
        protected void GroupsRepeater_PreRender(object sender, EventArgs e)
        {
            for (int i = 0; i < GroupsRepeater.Items.Count; i++)
            {
                Literal rowOpenLiteral = (Literal)GroupsRepeater.Items[i].FindControl("RowOpenLiteral");
                Literal rowCloseLiteral = (Literal)GroupsRepeater.Items[i].FindControl("RowCloseLiteral");

                if ((i) % 3 == 0)
                {
                    rowOpenLiteral.Text = @"<div class=""row"">";
                }

                if ((i % 3 == 2) || (i == GroupsRepeater.Items.Count - 1))
                {
                    rowCloseLiteral.Text = "</div>";
                }
            }
        }

        // Binds the Groups repeater control
        protected void GroupsRepeater_BindRepeater()
        {
            GroupsRepeater.DataSource = InstructorCourse.Groups;
            GroupsRepeater.DataBind();
        }

        // Called when each group is bound
        protected void GroupsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int groupIndex = e.Item.ItemIndex + 1;

            GridView groupMembersGridView = (GridView)e.Item.FindControl("GroupMembersGridView");

            Group group = InstructorCourse.Groups.FirstOrDefault(x => x.GroupNumber == groupIndex);

            if (group != null)
            {
                List<Student> students = group.Members.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList();

                groupMembersGridView.DataSource = students;
                groupMembersGridView.DataBind();

                List<double> gpaList = new List<double>();
                foreach (Student student in students)
                {
                    if (!string.IsNullOrEmpty(student.GPA))
                    {
                        double gpa = double.Parse(student.GPA);
                        gpaList.Add(gpa);
                    }
                }

                Label statsLabel = (Label)e.Item.FindControl("GroupStatsLabel");

                double averageGPA = 0.0;

                if (gpaList.Count > 0)
                {
                    double lowGpa = gpaList.Min();
                    double highGpa = gpaList.Max();
                    double difference = highGpa - lowGpa;

                    averageGPA = gpaList.Average();
                    statsLabel.Text += "<b>GPA Average:</b> " + averageGPA.ToString("#.##") + "&nbsp;&nbsp;<b>GPA Range:</b> " + difference.ToString("#.##") + "<br />";
                }

                //List<ProgrammingLanguage> languages = GrouperMethods.GetLanguages();

                int languagesInCommon = 0;
                int languageLevel = 0;
                string languageName = "";

                foreach (ProgrammingLanguage language in Languages)
                {
                    List<Student> languageStudents = students.Where(x => x.Languages.Exists(y => y.LanguageID == language.LanguageID)).ToList();

                    int inCommon = languageStudents.Count();

                    int level = (int)languageStudents.Sum(x => x.Languages.First(y => y.LanguageID == language.LanguageID).ProficiencyLevel);

                    if (inCommon > languagesInCommon)
                    {
                        languagesInCommon = inCommon;
                        languageLevel = level;
                        languageName = language.Icon;
                    }
                    else if (inCommon == languagesInCommon && level > languageLevel)
                    {
                        languagesInCommon = inCommon;
                        languageLevel = level;
                        languageName = language.Icon;
                    }
                }

                decimal percentageInCommon = 0;

                if (languagesInCommon > 0)
                {
                    percentageInCommon = decimal.Divide(languagesInCommon, students.Count) * 100;
                    statsLabel.Text += "<b>Language Match: </b>" + percentageInCommon.ToString("#.##") + "%&nbsp;&nbsp;<b>Language: </b>" + languageName;
                }

                if (string.IsNullOrEmpty(statsLabel.Text))
                {
                    statsLabel.Text = " ";
                }
            }
        }

        // Provides event handling for controls within Groups
        protected void GroupsRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Label groupNameLabel = (Label)e.Item.FindControl("GroupNameLabel");
            TextBox groupNameTextBox = (TextBox)e.Item.FindControl("GroupNameTextBox");
            LinkButton editNameLinkButton = (LinkButton)e.Item.FindControl("EditGroupNameLinkButton");
            LinkButton saveNameLinkButton = (LinkButton)e.Item.FindControl("SaveGroupNameLinkButton");
            LinkButton cancelEditNameLinkButton = (LinkButton)e.Item.FindControl("CancelEditGroupNameLinkButton");

            int groupID = int.Parse(e.CommandArgument.ToString());
            Group group = InstructorCourse.Groups.FirstOrDefault(x => x.GroupID == groupID);

            if (e.CommandName == "edit_group_name")
            {
                groupNameLabel.Visible = false;
                groupNameTextBox.Visible = true;

                groupNameTextBox.Text = group.Name;

                editNameLinkButton.Visible = false;
                saveNameLinkButton.Visible = true;
                cancelEditNameLinkButton.Visible = true;
            }
            else if (e.CommandName == "cancel_edit_group_name")
            {
                groupNameLabel.Visible = true;
                groupNameTextBox.Visible = false;
                editNameLinkButton.Visible = true;
                saveNameLinkButton.Visible = false;
                cancelEditNameLinkButton.Visible = false;
            }
            else if (e.CommandName == "save_group_name")
            {
                groupNameLabel.Visible = true;
                groupNameTextBox.Visible = false;
                editNameLinkButton.Visible = true;
                saveNameLinkButton.Visible = false;
                cancelEditNameLinkButton.Visible = false;

                if (string.IsNullOrEmpty(groupNameTextBox.Text.Trim()))
                {
                    group.Name = "Group " + group.GroupNumber.ToString();
                }
                else
                {
                    group.Name = groupNameTextBox.Text.Trim();
                }

                GrouperMethods.UpdateGroup(group);
                groupNameLabel.Text = group.Name;
                GroupsRepeater_BindRepeater();
            }
        }


        #endregion

        #region Grouping Tools

        // Automatically generates groups
        protected void AutoGenerateGroupsLinkButton_Click(object sender, EventArgs e)
        {
            ClearGroups();

            List<Student> students = InstructorCourse.Students;

            int groupCount = InstructorCourse.Groups.Count();
            int studentCount = students.Count();

            int groupSize = studentCount / groupCount;

            for (int i = 0; i < InstructorCourse.Groups.Count; i++)
            {
                Group group = InstructorCourse.Groups[i];

                ProgrammingLanguage language;

                if (i > Languages.Count - 1)
                {
                    language = Languages[i - (Languages.Count - 1)];
                }
                else
                {
                    language = Languages[i];
                }

                List<Student> groupMembers = new List<Student>();

                List<Student> languageStudents = students.Where(x => x.Languages.Exists(y => y.LanguageID == language.LanguageID)).ToList();

                if (languageStudents.Count >= groupSize)
                {
                    groupMembers.AddRange(languageStudents.Take(groupSize));
                }
                else
                {
                    groupMembers.AddRange(languageStudents.Take(languageStudents.Count));
                }

                foreach (Student student in groupMembers)
                {
                    students.RemoveAll(x => x.StudentID == student.StudentID);
                    GrouperMethods.InsertGroupMember(group.GroupID, student.StudentID);
                    group.Members.Add(student);
                }
            }

            Group minGroup = InstructorCourse.Groups.OrderBy(x => x.Members.Count).First();

            foreach (Group group in InstructorCourse.Groups)
            {
                while (group.Members.Count < groupSize && students.Count > 0)
                {
                    Student student = students.First();
                    group.Members.Add(student);
                    students.Remove(student);

                    GrouperMethods.InsertGroupMember(group.GroupID, student.StudentID);
                }
            }

            for (int i = 0; i < students.Count; i++)
            {
                Group group = InstructorCourse.Groups[i];
                GrouperMethods.InsertGroupMember(group.GroupID, students[i].StudentID);
            }

            InstructorCourse = GrouperMethods.GetInstructorCourse(InstructorCourseID);

            StudentsGridView_BindGridView();
            GroupsRepeater_BindRepeater();
        }

        // Clears groups/rebinds controls
        protected void ResetGroupsLinkButton_Click(object sender, EventArgs e)
        {
            ClearGroups();

            StudentsGridView_BindGridView();
            GroupsRepeater_BindRepeater();
        }

        // Clears all group members from their respective groups
        protected void ClearGroups()
        {
            foreach (Group group in InstructorCourse.Groups)
            {
                foreach (Student member in group.Members)
                {
                    GrouperMethods.DeleteGroupMember(member.StudentID);
                }
            }
        }

        // Displays the confirm delete groups dialog box
        protected void DeleteAllGroupsLinkButton_Click(object sender, EventArgs e)
        {
            GroupsRepeater_BindRepeater();
            ConfirmDeleteAllGroupsMessageBox();
        }

        #region Sorting

        // Sorts ungrouped students by language
        protected void LanguagesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LanguagesDropDownList.SelectedIndex > 0)
            {
                int languageID = int.Parse(LanguagesDropDownList.SelectedValue);

                SortLanguageHiddenField.Value = languageID.ToString();

                List<Student> students = GrouperMethods.GetUngroupedStudents(InstructorCourseID);

                students = students.OrderByDescending(x => x.Languages.Exists(y => y.LanguageID == languageID)).ToList();

                StudentsGridView.DataSource = students;
                StudentsGridView.DataBind();

                GroupsRepeater_BindRepeater();
            }
        }

        // Sorts ungrouped students by GPA
        protected void GPADropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GPADropDownList.SelectedIndex > 0)
            {
                List<Student> students = GrouperMethods.GetUngroupedStudents(InstructorCourseID);

                if (GPADropDownList.SelectedIndex == 1)
                {
                    SortGPAHiddenField.Value = "low";

                    students = students.OrderBy(x => x.GPA).ToList();

                    StudentsGridView.DataSource = students;
                    StudentsGridView.DataBind();
                }
                else if (GPADropDownList.SelectedIndex == 2)
                {
                    SortGPAHiddenField.Value = "high";

                    students = students.OrderByDescending(x => x.GPA).ToList();

                    StudentsGridView.DataSource = students;
                    StudentsGridView.DataBind();
                }
                GroupsRepeater_BindRepeater();
            }
        }

        // Sorts ungrouped students by Role
        protected void RolesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RolesDropDownList.SelectedIndex > 0)
            {
                int roleID = int.Parse(RolesDropDownList.SelectedValue);

                SortRoleHiddenField.Value = roleID.ToString();

                List<Student> students = GrouperMethods.GetUngroupedStudents(InstructorCourseID);

                students = students.OrderByDescending(x => x.InterestedRoles.Exists(y => y.RoleID == roleID)).ToList();

                StudentsGridView.DataSource = students;
                StudentsGridView.DataBind();

                GroupsRepeater_BindRepeater();
            }
        }

        #endregion

        #endregion

        #endregion

        #region Group List View

        #region Export CSV

        protected void ExportGroupsLinkButton_Click(object sender, EventArgs e)
        {
            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;

            //Add the Header row for CSV file.
            csv += "Group Number,Group Name,Member DuckID,Member Name, Member UOID,Member Email,Welcome Sent Date,Survey Submitted Date,Group Notes";

            ////Add new line.
            csv += "\r\n";

            csv += ",,,,,,,,";

            csv += "\r\n";

            csv += InstructorCourse.Course.FullName + ",";

            if (!string.IsNullOrEmpty(InstructorCourse.TermName))
            {
                csv += InstructorCourse.TermName + ",";
            }
            else
            {
                csv += ",";
            }

            if (InstructorCourse.Year > 0)
            {
                csv += InstructorCourse.Year.ToString() + ",";
            }
            else
            {
                csv += ",";
            }

            if (InstructorCourse.CRN > 0)
            {
                csv += InstructorCourse.CRN.ToString() + ",";
            }
            else
            {
                csv += ",";
            }

            if (!string.IsNullOrEmpty(InstructorCourse.DaysOfWeek))
            {
                csv += InstructorCourse.DaysOfWeek + ",";
            }
            else
            {
                csv += ",";
            }

            if (InstructorCourse.StartTime != null)
            {
                csv += InstructorCourse.StartTime.Value.ToString("h:mm") + ",";
            }
            else
            {
                csv += ",";
            }

            if (InstructorCourse.EndTime != null)
            {
                csv += InstructorCourse.EndTime.Value.ToString("h:mm") + ",";
            }
            else
            {
                csv += ",";
            }

            csv += ",";

            csv += "\r\n";

            csv += ",,,,,,,,";

            csv += "\r\n";

            int numberExported = 0;

            foreach (Group group in InstructorCourse.Groups)
            {
                csv += group.GroupNumber.ToString() + ",";

                if (!string.IsNullOrEmpty(group.Name))
                {
                    csv += group.Name + ",";
                }
                else
                {
                    csv += "Group " + group.GroupNumber.ToString() + ",";
                }

                csv += ",,,,,";

                csv += group.Notes + ",\r\n";


                foreach (Student student in group.Members)
                {
                    csv += ",,";
                    csv += student.DuckID + ",";
                    csv += student.FirstName + " " + student.LastName + ",";
                    csv += student.UOID.ToString() + ",";
                    csv += student.DuckID + "@uoregon.edu" + ",";
                    csv += student.InitialNotificationSentDate.ToString() + ",";
                    csv += student.SurveySubmittedDate.ToString() + ",";
                    csv += "\r\n";
                }
                numberExported++;
            }

            List<Student> ungroupedStudents = GrouperMethods.GetUngroupedStudents(InstructorCourseID);

            if (ungroupedStudents.Count > 0)
            {
                csv += ",,,,,,,,";

                csv += "\r\n";

                csv += "Currently Ungrouped" + ",,,,,,,";

                csv += "\r\n";

                foreach (Student student in ungroupedStudents)
                {
                    csv += ",,";
                    csv += student.DuckID + ",";
                    csv += student.FirstName + " " + student.LastName + ",";
                    csv += student.UOID.ToString() + ",";
                    csv += student.DuckID + "@uoregon.edu" + ",";
                    csv += student.InitialNotificationSentDate.ToString() + ",";
                    csv += student.SurveySubmittedDate.ToString() + ",";
                    csv += "\r\n";

                    numberExported++;
                }
            }

            if (numberExported > 0)
            {
                Response.ContentType = "text/csv";
                Response.Headers["Content-Disposition"] = "inline; filename=groups.csv";

                Response.Write(csv);
                Response.End();

                MessageBox("Student Records Exported", "Groups were sucessfully exported.", "Okay");
            }
            else
            {
                MessageBox("Unable to Export Groups", "There do not appear to be any groups to export.  Please create and build groups before exporting.", "Okay");
            }
        }

        #endregion

        #region GroupsGridView  

        // Binds the Group List GridView
        protected void GroupsGridView_BindGridView()
        {
            if (InstructorCourse.Groups.Count > 0)
            {
                NoGroupsPanel.Visible = false;
            }
            else
            {
                NoGroupsPanel.Visible = true;
            }
            GroupsGridView.DataSource = InstructorCourse.Groups;
            GroupsGridView.DataBind();
        }

        // Renders the proper output for each row
        protected void GroupsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    int groupID = Convert.ToInt32(GroupsGridView.DataKeys[e.Row.RowIndex].Values[0]);

                    GridView groupMembersGridView = (GridView)e.Row.FindControl("GroupMembersGridView");
                    Group group = InstructorCourse.Groups.FirstOrDefault(x => x.GroupID == groupID);

                    if (group != null)
                    {
                        groupMembersGridView.DataSource = group.Members;
                        groupMembersGridView.DataBind();
                    }

                    HyperLink emailGroupHyperLink = (HyperLink)e.Row.FindControl("EmailGroupHyperLink");
                    LinkButton editLinkButton = (LinkButton)e.Row.FindControl("EditLinkButton");
                    LinkButton removeLinkButton = (LinkButton)e.Row.FindControl("RemoveLinkButton");

                    string link = "mailto:";
                    foreach (Student member in group.Members)
                    {
                        link += member.DuckID + "@uoregon.edu,";
                    }
                    link = link.Substring(0, link.Length - 1);

                    if (!string.IsNullOrEmpty(group.Name))
                    {
                        link += "?subject=" + group.Name;
                    }
                    else
                    {
                        link += "?subject=Group " + group.GroupNumber.ToString();
                    }

                    emailGroupHyperLink.NavigateUrl = link;

                    if (GroupsGridView.EditIndex > 0)
                    {
                        emailGroupHyperLink.Visible = false;
                        editLinkButton.Visible = false;
                        removeLinkButton.Visible = false;
                    }

                }
                else if (e.Row.RowState == DataControlRowState.Edit)
                {
                    e.Row.CssClass = "selected";
                    int groupID = Convert.ToInt32(GroupsGridView.DataKeys[e.Row.RowIndex].Values[0]);

                    GridView groupMembersGridView = (GridView)e.Row.FindControl("EditGroupMembersGridView");
                    Group group = InstructorCourse.Groups.FirstOrDefault(x => x.GroupID == groupID);

                    if (group != null)
                    {
                        groupMembersGridView.DataSource = group.Members;
                        groupMembersGridView.DataBind();
                    }
                }
            }
        }

        // Handles commands from the controls in each row
        protected void GroupsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int groupID = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "delete_group")
            {
                Group group = InstructorCourse.Groups.FirstOrDefault(x => x.GroupID == groupID);

                if (group != null)
                {
                    ConfirmDeleteMessageBox(group);
                }
            }
            else if (e.CommandName == "edit_group")
            {
                GroupsGridView.DataSource = InstructorCourse.Groups;

                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                int RowIndex = gvr.RowIndex;

                GroupsGridView.EditIndex = RowIndex;

                GroupsGridView.DataBind();
            }
            else if (e.CommandName == "cancel_edit_group")
            {
                GroupsGridView.DataSource = InstructorCourse.Groups;

                GroupsGridView.EditIndex = -1;

                GroupsGridView.DataBind();
            }
            else if (e.CommandName == "save_group")
            {
                Group group = InstructorCourse.Groups.FirstOrDefault(x => x.GroupID == groupID);

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                TextBox nameTextBox = (TextBox)row.FindControl("EditGroupNameTextBox");
                group.Name = nameTextBox.Text.Trim();

                TextBox notesTextBox = (TextBox)row.FindControl("EditGroupNotesTextBox");
                group.Notes = notesTextBox.Text.Trim();

                GrouperMethods.UpdateGroup(group);
                _InstructorCourse = null;

                GroupsGridView.DataSource = InstructorCourse.Groups;

                GroupsGridView.EditIndex = -1;

                GroupsGridView.DataBind();
            }
        }

        #region Group Members GridView

        protected void GroupMembersGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    LinkButton removeLinkButton = (LinkButton)e.Row.FindControl("RemoveGroupMemberLinkButton");

                    if (GroupsGridView.EditIndex > 0)
                    {
                        removeLinkButton.Visible = false;
                    }

                }
            }
        }

        protected void GroupMembersGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int studentID = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "delete_group_member")
            {
                Student student = InstructorCourse.Students.FirstOrDefault(x => x.StudentID == studentID);

                if (student != null)
                {
                    ConfirmDeleteMessageBox(student);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region Event Handlers

        // Displays the Group List view
        protected void GroupListLinkButton_Click(object sender, EventArgs e)
        {
            ListPanel.Visible = true;
            GroupingPanel.Visible = false;
            GroupsGridView_BindGridView();
        }

        // Displays the Edit Groups view
        protected void EditGroupsLinkButton_Click(object sender, EventArgs e)
        {
            ListPanel.Visible = false;
            GroupingPanel.Visible = true;
            GroupsRepeater_BindRepeater();
        }

        // Returns the user to the Students page
        protected void ReturnToStudentsLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Students.aspx?ID=" + InstructorCourseID);
        }

        #endregion
    }
}