using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GroupBuilder;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Net.Mail;
using System.Threading;
using System.Web.Hosting;

namespace GroupBuilderAdmin
{
    public partial class Students : System.Web.UI.Page
    {
        #region Constants

        public static string FILE_PATH = @"E:\Inetpub\Files\grouper\";
        private static string LOCAL_PATH = "./App_Data/";
        private static bool LOCAL_FLAG = false;

        #endregion

        #region Variables and Page Properties

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
                        _Courses = GrouperMethods.GetCourses();
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
                if(_Roles == null)
                {
                    if (Session["Roles"] != null)
                    {
                        _Roles = (List<Role>)Session["Roles"];
                    }
                    else
                    {
                        _Roles = GrouperMethods.GetRoles();
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
                        _Languages = GrouperMethods.GetLanguages();
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
                        _Skills = GrouperMethods.GetSkills();
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
                if (_InstructorCourse == null)
                {
                    _InstructorCourse = GrouperMethods.GetInstructorCourse(InstructorCourseID);
                }
                return _InstructorCourse;
            }
            set
            {
                _InstructorCourse = value;
            }
        }

        private int _InstructorCourseID;
        public int InstructorCourseID
        {
            get
            {
                _InstructorCourseID = 0;
                if (Request.QueryString["ID"] != "" && Request.QueryString["ID"] != null)
                {
                    _InstructorCourseID = int.Parse(Request.QueryString["ID"].ToString());
                }
                return _InstructorCourseID;
            }
            set
            {
                _InstructorCourseID = value;
            }
        }

        #endregion

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

        private void ConfirmDeleteMessageBox(Student student)
        {
            SelectedStudentIDHiddenField.Value = student.StudentID.ToString();
            MessageBoxTitleLabel.Text = "Delete Student?";
            MessageBoxMessageLabel.Text = "Are you sure you want to delete the student '" + student.FirstName + " " + student.LastName + "?";
            MessageBoxOkayLinkButton.Text = "<span class='fa fa-ban'></span>&nbsp;&nbsp;Cancel";
            MessageBoxOkayLinkButton.Visible = true;
            MessageBoxCreateLinkButton.CssClass = "btn btn-danger";
            MessageBoxCreateLinkButton.Text = "<span class='fa fa-remove'></span>&nbsp;&nbsp;Delete Student";
            MessageBoxCreateLinkButton.Visible = true;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "messageBox", "$('#messageBox').modal();", true);
            upModal.Update();
        }

        private void ConfirmDeleteAllStudentsMessageBox()
        {
            SelectedStudentIDHiddenField.Value = 0.ToString();
            MessageBoxTitleLabel.Text = "Delete All Students?";
            MessageBoxMessageLabel.Text = "Are you sure you want to delete all students associated with this course section?";
            MessageBoxOkayLinkButton.Text = "<span class='fa fa-ban'></span>&nbsp;&nbsp;Cancel";
            MessageBoxOkayLinkButton.Visible = true;
            MessageBoxCreateLinkButton.CssClass = "btn btn-danger";
            MessageBoxCreateLinkButton.Text = "<span class='fa fa-remove'></span>&nbsp;&nbsp;Delete Students";
            MessageBoxCreateLinkButton.Visible = true;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "messageBox", "$('#messageBox').modal();", true);
            upModal.Update();
        }

        private void ConfirmSendWelcomeToAllStudents()
        {
            SelectedStudentIDHiddenField.Value = (-1).ToString();
            MessageBoxTitleLabel.Text = "Send Welcome to All Students?";
            MessageBoxMessageLabel.Text = "Send a welcome message to all previously unnotified students?";
            MessageBoxOkayLinkButton.Text = "<span class='fa fa-ban'></span>&nbsp;&nbsp;Cancel";
            MessageBoxOkayLinkButton.Visible = true;
            MessageBoxCreateLinkButton.CssClass = "btn btn-default";
            MessageBoxCreateLinkButton.Text = "<span class='fas fa-paper-plane'></span>&nbsp;&nbsp;Send Welcome to All";
            MessageBoxCreateLinkButton.Visible = true;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "messageBox", "$('#messageBox').modal();", true);
            upModal.Update();
        }

        protected void MessageBoxCreateLinkButton_Click(object sender, EventArgs e)
        {
            int studentID = int.Parse(SelectedStudentIDHiddenField.Value);

            if (studentID == -1)
            {
                foreach (Student student in InstructorCourse.Students)
                {
                    if (student.InitialNotificationSentDate == null)
                    {
                        SendSurveyLinkMessage(student);

                        student.InitialNotificationSentDate = DateTime.Now;
                        GrouperMethods.UpdateStudent(student);
                    }
                }

                StudentsGridView_BindGridView();

                MessageBox("Welcome Messages Sent", "Welcome messages have been sent to all previously unnotified students.", "Okay");
            }
            else if (studentID == 0)
            {
                foreach (Student student in InstructorCourse.Students)
                {
                    GrouperMethods.DeleteStudent(student.StudentID);
                }

                _InstructorCourse = null;

                StudentsGridView_BindGridView();
                MessageBox("Students Deleted", "All students have been deleted.", "Okay");
            }
            else
            {
                GrouperMethods.DeleteStudent(studentID);
                MessageBox("Student Deleted", "The student record has been deleted.", "Okay");

                _InstructorCourse = null;

                StudentsGridView_BindGridView();
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if(InstructorCourse != null)
            { 
                CourseNameLabel.Text = InstructorCourse.Course.FullName;
                if (InstructorCourse.Students.Count > 0)
                {
                    int submittedCount = InstructorCourse.Students.Where(x => x.SurveySubmittedDate != null).Count();

                    if (submittedCount > 0)
                    {
                        double studentSubmittedPercent = (double)decimal.Divide(submittedCount, InstructorCourse.Students.Count) * 100;
                        SubmittedPercentLabel.Text = "<b>Students Submitted: </b>" + submittedCount.ToString() + "/" + InstructorCourse.Students.Count.ToString() + " (" + studentSubmittedPercent.ToString("#.##") + "%)";
                    }
                    else
                    {
                        SubmittedPercentLabel.Text = "<b>Students Submitted: </b>No students have submitted their surveys yet.";
                    }
                }
                StudentsGridView_BindGridView();
            }
        }

        #region StudentsGridView

        // Binds the Students GridView
        protected void StudentsGridView_BindGridView()
        {
            StudentsGridView.DataSource = InstructorCourse.Students;
            StudentsGridView.DataBind();
        }

        // Adds event handlers to the Students GridView rows
        protected void StudentsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete_student")
            {
                int studentID = int.Parse(e.CommandArgument.ToString());
                Student student = GrouperMethods.GetStudent(studentID);

                ConfirmDeleteMessageBox(student);
            }
            else if (e.CommandName == "edit_student")
            {
                EditStudentLabel.Text = "Edit Student";

                int studentID = int.Parse(e.CommandArgument.ToString());

                SelectedStudentIDHiddenField.Value = studentID.ToString();

                Student student = GrouperMethods.GetStudent(studentID);

                PriorCoursesGridView.DataSource = student.PriorCourses;
                PriorCoursesGridView.DataBind();

                GPALabel.Text = "Core GPA: " + student.GPA;

                FirstNameTextBox.Text = student.FirstName;
                PreferredNameTextBox.Text = student.PreferredName;
                LastNameTextBox.Text = student.LastName;
                DuckIDTextBox.Text = student.DuckID;
                UOIDTextBox.Text = student.UOID.ToString();

                if (student.EnglishSecondLanguageFlag != null)
                {
                    EnglishLanguageDropDownList.SelectedValue = student.EnglishSecondLanguageFlag.ToString();
                }

                NativeLanguageTextBox.Text = student.NativeLanguage;

                WorkExperienceTextBox.Text = student.DevelopmentExperience;
                LearningExpectationsTextBox.Text = student.LearningExpectations;

                List<Course> coreCourses = Courses.Where(x => x.CoreCourseFlag == true).ToList();

                CoreCoursesDropDownList.DataSource = coreCourses;
                CoreCoursesDropDownList.DataBind();

                ViewState["PriorCourses"] = new List<Course>();

                foreach (Course course in student.PriorCourses)
                {
                    ((List<Course>)ViewState["PriorCourses"]).Add(course);
                    CoreCoursesDropDownList.Items.FindByValue(course.CourseID.ToString()).Enabled = false;
                }

                PriorCoursesGridView.DataSource = (List<Course>)ViewState["PriorCourses"];
                PriorCoursesGridView.DataBind();

                List<Course> currentCourses = Courses;

                CurrentCoursesDropDownList.DataSource = coreCourses;
                CurrentCoursesDropDownList.DataBind();

                ViewState["CurrentCourses"] = new List<Course>();

                foreach (Course course in student.CurrentCourses)
                {
                    ((List<Course>)ViewState["CurrentCourses"]).Add(course);
                    ListItem item = CurrentCoursesDropDownList.Items.FindByValue(course.CourseID.ToString());
                    if (item != null)
                    {
                        item.Enabled = false;
                    }
                }

                CurrentCoursesGridView.DataSource = (List<Course>)ViewState["CurrentCourses"];
                CurrentCoursesGridView.DataBind();

                if (((List<Course>)ViewState["CurrentCourses"]).Count == Courses.Count)
                {
                    AddCurrentCourseLinkButton.Enabled = false;
                    AddCurrentCourseLinkButton.CssClass = "btn btn-default btn-sm disabled";
                }
                else
                {
                    AddCurrentCourseLinkButton.Enabled = true;
                    AddCurrentCourseLinkButton.CssClass = "btn btn-default btn-sm";
                }

                ProgrammingLanguagesDropDownList.DataSource = Languages;
                ProgrammingLanguagesDropDownList.DataBind();

                ViewState["Languages"] = new List<ProgrammingLanguage>();

                foreach (ProgrammingLanguage language in student.Languages)
                {
                    ((List<ProgrammingLanguage>)ViewState["Languages"]).Add(language);
                    ProgrammingLanguagesDropDownList.Items.FindByValue(language.LanguageID.ToString()).Enabled = false;
                }

                ProgrammingLanguagesGridView.DataSource = (List<ProgrammingLanguage>)ViewState["Languages"];
                ProgrammingLanguagesGridView.DataBind();

                RolesDropDownList.DataSource = Roles;
                RolesDropDownList.DataBind();

                ViewState["Roles"] = new List<Role>();

                foreach (Role role in student.InterestedRoles)
                {
                    ((List<Role>)ViewState["Roles"]).Add(role);
                    RolesDropDownList.Items.FindByValue(role.RoleID.ToString()).Enabled = false;
                }

                RolesGridView.DataSource = (List<Role>)ViewState["Roles"];
                RolesGridView.DataBind();

                SkillsDropDownList.DataSource = Skills;
                SkillsDropDownList.DataBind();

                ViewState["Skills"] = new List<Skill>();

                foreach (Skill skill in student.Skills)
                {
                    ((List<Skill>)ViewState["Skills"]).Add(skill);
                    SkillsDropDownList.Items.FindByValue(skill.SkillID.ToString()).Enabled = false;
                }

                SkillsGridView.DataSource = (List<Skill>)ViewState["Skills"];
                SkillsGridView.DataBind();

                GUIDLabel.Text = "Student GUID: " + student.GUID;

                StudentListPanel.Visible = false;
                AddStudentPanel.Visible = true;

                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            }
            else if (e.CommandName == "send_welcome")
            {
                int studentID = int.Parse(e.CommandArgument.ToString());

                Student student = GrouperMethods.GetStudent(studentID);

                SendSurveyLinkMessage(student);

                student.InitialNotificationSentDate = DateTime.Now;
                GrouperMethods.UpdateStudent(student);

                StudentsGridView_BindGridView();
            }
            else if (e.CommandName == "re_open")
            {
                int studentID = int.Parse(e.CommandArgument.ToString());

                Student student = GrouperMethods.GetStudent(studentID);

                student.SurveySubmittedDate = null;

                GrouperMethods.UpdateStudent(student);

                InstructorCourse.Students = GrouperMethods.GetStudents(InstructorCourseID);

                StudentsGridView_BindGridView();
            }
        }

        // Renders each row of the GridView
        protected void StudentsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int studentID = Convert.ToInt32(StudentsGridView.DataKeys[e.Row.RowIndex].Values[0]);

                Student student = GrouperMethods.GetStudent(studentID);

                if (student != null)
                {
                    Label firstNameLabel = (Label)e.Row.FindControl("FirstNameLabel");

                    if (!string.IsNullOrEmpty(student.FirstName))
                    {
                        firstNameLabel.Text = student.FirstName;
                        if (!string.IsNullOrEmpty(student.PreferredName))
                        {
                            firstNameLabel.Text += "<br />(" + student.PreferredName + ")";
                        }
                    }

                    Label nativeLanguageLabel = (Label)e.Row.FindControl("NativeLanguageLabel");

                    if (student.EnglishSecondLanguageFlag != null)
                    {
                        if (student.EnglishSecondLanguageFlag == true)
                        {
                            if (!string.IsNullOrEmpty(student.NativeLanguage))
                            {
                                nativeLanguageLabel.Text = student.NativeLanguage;
                            }
                            else
                            {
                                nativeLanguageLabel.Text = "Unspecified";
                            }
                        }
                        else
                        {
                            nativeLanguageLabel.Text = "English";
                        }
                    }
                    else
                    {
                        nativeLanguageLabel.Text = "Unspecified";
                    }

                    string languages = "";

                    if (student.Languages != null)
                    {
                        if (student.Languages.Count > 0)
                        {
                            languages += "<table class='table table-bordered table-condensed' style='font-size: .9em;'>";
                            foreach (ProgrammingLanguage language in student.Languages.OrderByDescending(x => x.ProficiencyLevel))
                            {
                                languages += "<tr><td><span class='no-wrap'>" + language.Icon + " " + language.Name + " - " + language.ProficiencyLevel.ToString() + "</span></td></tr>";
                            }
                            languages += "</table>";
                        }
                    }
                    Label programmingLanguagesLabel = (Label)e.Row.FindControl("LanguagesLabel");
                    programmingLanguagesLabel.Text = languages;

                    string roles = "";

                    if (student.InterestedRoles != null)
                    {
                        if (student.InterestedRoles.Count > 0)
                        {
                            roles += "<table class='table table-bordered table-condensed' style='font-size: .9em;'>";
                            foreach (Role role in student.InterestedRoles.OrderByDescending(x => x.InterestLevel))
                            {
                                roles += "<tr><td><span class='no-wrap'>" + role.Icon + " " + role.Name + " - " + role.InterestLevel.ToString() + "</span></td></tr>";
                            }
                            roles += "</table>";
                        }
                    }
                    Label rolesLabel = (Label)e.Row.FindControl("RolesLabel");
                    rolesLabel.Text = roles;


                    string skills = "";

                    if (student.Skills != null)
                    {
                        if (student.Skills.Count > 0)
                        {
                            skills += "<table class='table table-bordered table-condensed' style='font-size: .9em;'>";
                            foreach (Skill skill in student.Skills)
                            {
                                skills += "<tr><td><span class='no-wrap'>" + skill.Name + " - " + skill.ProficiencyLevel.ToString() + "</span></td></tr>";
                            }
                            skills += "</table>";
                        }
                    }
                    Label skillsLabel = (Label)e.Row.FindControl("SkillsLabel");
                    skillsLabel.Text = skills;
                }
            }
        }

        #endregion

        #region Student Record Importing

        protected void ImportStudentsLinkButton_Click(object sender, EventArgs e)
        {
            ImportStudentsPanel.Visible = true;
            ImportStudentsLinkButton.Visible = false;

            StudentListPanel.Visible = false;
        }

        protected void CancelImportStudentsLinkButton_Click(object sender, EventArgs e)
        {
            ImportStudentsPanel.Visible = false;
            ImportStudentsLinkButton.Visible = true;

            StudentListPanel.Visible = true;
        }

        protected void ProcessStudentsFileLinkButton_Click(object sender, EventArgs e)
        {
            List<Student> students = new List<Student>();

            HttpPostedFile file = StudentsFileUpload.PostedFile;

            string fileName = "";
            string[] fileNameParts = file.FileName.Split('.');
            string extension = fileNameParts[1].Trim().ToLower();

            if (extension != "csv")
            {
                MessageBox("Invalid File Type", "'" + extension + "' is not a valid file type.  Please limit to proper .csv (comma sorted value) files containing student data.", "Okay");
            }
            else
            {
                if (file.ContentType != "text/csv" && file.ContentType != "application/vnd.ms-excel")
                {
                    MessageBox("Invalid File Type", "This does not appear to be a proper file type.  Please limit to proper .csv (comma sorted value) files containing student data.", "Okay");
                }
                else
                {
                    if (file.ContentLength > 0)
                    {
                        fileName = SaveFile(file);
                    }

                    string path = "";
                    if (!LOCAL_FLAG)
                    {
                        path = FILE_PATH + fileName;
                    }
                    else
                    {
                        path = Server.MapPath(LOCAL_PATH + fileName);
                    }
                    if (File.Exists(path))
                    {
                        StreamReader reader = File.OpenText(path);

                        students = ParseFileForStudents(reader);
                        int importedCount = 0;
                        foreach (Student student in students)
                        {
                            int studentID = GrouperMethods.InsertStudent(student);
                            if (studentID > 0)
                            {
                                foreach (Course course in student.PriorCourses)
                                {
                                    GrouperMethods.InsertStudentPriorCourse(studentID, course.CourseID, course.Grade);
                                }

                                foreach (ProgrammingLanguage language in student.Languages)
                                {
                                    GrouperMethods.InsertStudentLanguage(studentID, language.LanguageID, (int)language.ProficiencyLevel);
                                }

                                foreach (Role role in student.InterestedRoles)
                                {
                                    GrouperMethods.InsertStudentRoleInterest(studentID, role.RoleID, (int)role.InterestLevel);
                                }

                                foreach (Skill skill in student.Skills)
                                {
                                    GrouperMethods.InsertStudentSkill(studentID, skill.SkillID, (int)skill.ProficiencyLevel);
                                }
                                importedCount++;
                            }
                        }
                        if (importedCount > 0)
                        {
                            _InstructorCourse = null;

                            StudentsGridView_BindGridView();

                            ImportStudentsLinkButton.Visible = true;
                            ImportStudentsPanel.Visible = false;
                            StudentListPanel.Visible = true;
                            AddStudentLinkButton.Visible = true;

                            if (importedCount != students.Count)
                            {
                                MessageBox("Student Records Imported", importedCount.ToString() + " student records were found and imported, however one or more could not be imported.  Please ensure you are importing unique student records.", "Okay");
                            }
                            else
                            {
                                MessageBox("Student Records Imported", importedCount.ToString() + " student records were found and imported.", "Okay");
                            }
                        }
                        else if(importedCount == 0 && students.Count > 0)
                        {
                            MessageBox("Unable to Import Student Records", students.Count.ToString() + " student records were found found, however they appear to be duplicates of students which exist in this course already.  No student records have been imported.", "Okay");
                        }
                        else
                        {
                            MessageBox("Unable to Import Student Records", "No valid student records were found in the selected file.", "Okay");
                        }
                    }
                }
            }
        }

        protected List<Student> ParseFileForStudents(StreamReader reader)
        {
            List<Student> studentList = new List<Student>();

            TextFieldParser parser = new TextFieldParser(reader.BaseStream);

            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            int index = 0;

            while (!parser.EndOfData)
            {
                string[] fields;

                try
                {
                    fields = parser.ReadFields();
                }
                catch (Microsoft.VisualBasic.FileIO.MalformedLineException ex)
                {
                    fields = new string[] { "" };
                }

                if (index > 0)
                {
                    if (fields[0].Length > 0)
                    {
                        int errors = 0;

                        Student student = new Student();
                        student.InstructorCourseID = InstructorCourseID;

                        string[] names = fields[0].Trim().Split(',');

                        if(names.Length == 2)
                        {
                            student.LastName = names[0].Trim();
                            student.FirstName = names[1].Trim();
                        }
                        else
                        {
                            errors++;
                        }

                        if (errors == 0)
                        {
                            int uoid;

                            if (int.TryParse(fields[1], out uoid))
                            {
                                student.UOID = uoid;
                            }
                            else
                            {
                                errors++;
                            }

                            if (errors == 0)
                            {
                                string email = fields[4].Trim().ToLower();

                                string[] emailParts = email.Split('@');

                                if (emailParts.Length == 2)
                                {
                                    if (emailParts[0].Length < 15)
                                    {
                                        student.DuckID = emailParts[0];
                                    }
                                    else
                                    {
                                        errors++;
                                    }
                                }
                                else
                                {
                                    errors++;
                                }

                                if (fields.Length == 10 && errors == 0)
                                {

                                    string[] priorCourses = fields[6].Trim().Split(',');

                                    foreach (string coursePair in priorCourses)
                                    {
                                        string[] pair = coursePair.Split(':');
                                        int courseID = int.Parse(pair[0]);

                                        Course course = Courses.FirstOrDefault(x => x.CourseID == courseID);
                                        if (course != null)
                                        {
                                            double grade;

                                            if (double.TryParse(pair[1], out grade))
                                            {
                                                grade = double.Parse(pair[1]);
                                                course.Grade = grade;
                                                student.PriorCourses.Add(course);
                                            }
                                        }
                                    }

                                    string[] interestedRoles = fields[7].Trim().Split(',');

                                    foreach (string rolePair in interestedRoles)
                                    {
                                        string[] pair = rolePair.Split(':');
                                        int roleID = int.Parse(pair[0]);

                                        Role role = GrouperMethods.GetRole(roleID);
                                        if (role != null)
                                        {
                                            int interestLevel;

                                            if (int.TryParse(pair[1], out interestLevel))
                                            {
                                                role.InterestLevel = interestLevel;
                                                student.InterestedRoles.Add(role);
                                            }
                                        }
                                    }

                                    string[] languages = fields[8].Trim().Split(',');

                                    foreach (string languagePair in languages)
                                    {
                                        string[] pair = languagePair.Split(':');
                                        int languageID = int.Parse(pair[0]);

                                        ProgrammingLanguage language = GrouperMethods.GetLanguage(languageID);
                                        if (language != null)
                                        {
                                            int skillLevel;

                                            if (int.TryParse(pair[1], out skillLevel))
                                            {
                                                language.ProficiencyLevel = skillLevel;
                                                student.Languages.Add(language);
                                            }
                                        }
                                    }

                                    string[] skills = fields[9].Trim().Split(',');

                                    foreach (string skillPair in skills)
                                    {
                                        string[] pair = skillPair.Split(':');
                                        int skillID = int.Parse(pair[0]);

                                        Skill skill = GrouperMethods.GetSkill(skillID);
                                        if (skill != null)
                                        {
                                            int skillLevel;

                                            if (int.TryParse(pair[1], out skillLevel))
                                            {
                                                skill.ProficiencyLevel = skillLevel;
                                                student.Skills.Add(skill);
                                            }
                                        }
                                    }
                                }

                                if (errors == 0)
                                {
                                    studentList.Add(student);
                                }
                            }
                        }
                    }
                }
                index++;
            }
            return studentList;
        }

        private string SaveFile(HttpPostedFile file)
        {

            string fileName = "Import_" + DateTime.Now.Day.ToString();
            string extension = ".csv";
            string fullFileName = fileName + extension;

            string pathToCheck = "";

            if (LOCAL_FLAG == false)
            {
                pathToCheck = FILE_PATH + fullFileName;
            }
            else
            {
                pathToCheck = Server.MapPath(LOCAL_PATH + fullFileName);
            }

            string tempfileName = "";

            if (System.IO.File.Exists(pathToCheck))
            {
                int counter = 2;
                while (System.IO.File.Exists(pathToCheck))
                {
                    tempfileName = fileName + "_" + counter.ToString() + extension;
                    if (LOCAL_FLAG == false)
                    {
                        pathToCheck = FILE_PATH + tempfileName;
                    }
                    else
                    {
                        pathToCheck = Server.MapPath(LOCAL_PATH + tempfileName);
                    }
                    counter++;
                }

                fileName = tempfileName;
            }
            else
            {
                fileName = fullFileName;
            }

            string savePath = "";

            if (LOCAL_FLAG == false)
            {
                savePath = FILE_PATH + fileName;
            }
            else
            {
                savePath = Server.MapPath(LOCAL_PATH + fileName);
            }

            file.SaveAs(savePath);
            return fileName;
        }

        #endregion

        #region Student Record Creation

        protected void AddStudentLinkButton_Click(object sender, EventArgs e)
        {
            SelectedStudentIDHiddenField.Value = null;

            StudentListPanel.Visible = false;
            AddStudentPanel.Visible = true;

            EditStudentLabel.Text = "Add Student";
            DuckIDTextBox.Text = "";
            FirstNameTextBox.Text = "";
            PreferredNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            UOIDTextBox.Text = "";
            GUIDLabel.Text = "";

            AddStudentLinkButton.Visible = false;
            SendWelcomeToAllStudentsLinkButton.Visible = false;
            DeleteAllStudentsLinkButton.Visible = false;

            List<Course> courses = Courses;

            CoreCoursesDropDownList.DataSource = courses.Where(x => x.CoreCourseFlag == true);
            CoreCoursesDropDownList.DataBind();

            CurrentCoursesDropDownList.DataSource = courses;
            CurrentCoursesDropDownList.DataBind();

            ProgrammingLanguagesDropDownList.DataSource = Languages;
            ProgrammingLanguagesDropDownList.DataBind();

            RolesDropDownList.DataSource = Roles;
            RolesDropDownList.DataBind();

            SkillsDropDownList.DataSource = Skills;
            SkillsDropDownList.DataBind();

            RolesGridView.DataSource = null;
            RolesGridView.DataBind();

            ProgrammingLanguagesGridView.DataSource = null;
            ProgrammingLanguagesGridView.DataBind();

            SkillsGridView.DataSource = null;
            SkillsGridView.DataBind();

        }

        protected void SaveAddStudentLinkButton_Click(object sender, EventArgs e)
        {
            int errors = 0;

            if (String.IsNullOrEmpty(SelectedStudentIDHiddenField.Value))
            {

                Student student = new Student();
                student.InstructorCourseID = InstructorCourseID;
                student.FirstName = FirstNameTextBox.Text.Trim();
                student.PreferredName = PreferredNameTextBox.Text.Trim();
                student.LastName = LastNameTextBox.Text.Trim();

                if (!string.IsNullOrEmpty(DuckIDTextBox.Text.Trim()))
                {
                    student.DuckID = DuckIDTextBox.Text.Trim().ToLower();

                    Student lookupStudent = InstructorCourse.Students.FirstOrDefault(x => x.DuckID == student.DuckID);
                    if (lookupStudent != null)
                    {
                        MessageBox("Unable to Add Student", "You already have a student with the DuckID '" + student.DuckID + "' in this course section.", "Okay");
                        errors++;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(UOIDTextBox.Text.Trim()))
                        {
                            student.UOID = int.Parse(UOIDTextBox.Text.Trim());

                            lookupStudent = InstructorCourse.Students.FirstOrDefault(x => x.UOID == student.UOID);

                            if (lookupStudent != null)
                            {
                                MessageBox("Unable to Add Student", "You already have a student with the UOID '" + student.UOID + "' in this course section.", "Okay");
                                errors++;
                            }
                        }
                        else
                        {
                            student.UOID = null;
                        }

                        if (errors == 0)
                        {
                            bool englishAsSecondLanguageFlag;
                            if (bool.TryParse(EnglishLanguageDropDownList.SelectedValue, out englishAsSecondLanguageFlag))
                            {
                                student.EnglishSecondLanguageFlag = englishAsSecondLanguageFlag;
                            }

                            student.NativeLanguage = NativeLanguageTextBox.Text.Trim();

                            student.DevelopmentExperience = WorkExperienceTextBox.Text.Trim();
                            student.LearningExpectations = LearningExpectationsTextBox.Text.Trim();

                            int studentID = GrouperMethods.InsertStudent(student);

                            if (studentID > 0)
                            {
                                if (ViewState["Languages"] != null)
                                {
                                    if (((List<ProgrammingLanguage>)ViewState["Languages"]).Count() > 0)
                                    {
                                        foreach (ProgrammingLanguage language in ((List<ProgrammingLanguage>)ViewState["Languages"]))
                                        {
                                            GrouperMethods.InsertStudentLanguage(studentID, language.LanguageID, (int)language.ProficiencyLevel);
                                        }
                                    }
                                }
                                if (ViewState["Roles"] != null)
                                {
                                    if (((List<Role>)ViewState["Roles"]).Count() > 0)
                                    {
                                        foreach (Role role in ((List<Role>)ViewState["Roles"]))
                                        {
                                            GrouperMethods.InsertStudentRoleInterest(studentID, role.RoleID, (int)role.InterestLevel);
                                        }
                                    }
                                }
                                if (ViewState["Skills"] != null)
                                {
                                    if (((List<Skill>)ViewState["Skills"]).Count() > 0)
                                    {
                                        foreach (Skill skill in ((List<Skill>)ViewState["Skills"]))
                                        {
                                            GrouperMethods.InsertStudentSkill(studentID, skill.SkillID, (int)skill.ProficiencyLevel);
                                        }
                                    }
                                }
                                if (ViewState["PriorCourses"] != null)
                                {
                                    if (((List<Course>)ViewState["PriorCourses"]).Count() > 0)
                                    {
                                        foreach (Course course in ((List<Course>)ViewState["PriorCourses"]))
                                        {
                                            GrouperMethods.InsertStudentPriorCourse(studentID, course.CourseID, course.Grade);
                                        }
                                    }
                                }
                                if (ViewState["CurrentCourses"] != null)
                                {
                                    if (((List<Course>)ViewState["CurrentCourses"]).Count() > 0)
                                    {
                                        foreach (Course course in ((List<Course>)ViewState["CurrentCourses"]))
                                        {
                                            GrouperMethods.InsertStudentCurrentCourse(studentID, course.CourseID);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox("Unable to Create Student Record", "DuckID is a required field.  Please enter a DuckID for the student in order to create their record.", "Okay");
                    errors++;
                }
            }
            else
            {
                int studentID = int.Parse(SelectedStudentIDHiddenField.Value);

                Student student = GrouperMethods.GetStudent(studentID);

                student.FirstName = FirstNameTextBox.Text.Trim();
                student.PreferredName = PreferredNameTextBox.Text.Trim();
                student.LastName = LastNameTextBox.Text.Trim();

                if (!string.IsNullOrEmpty(DuckIDTextBox.Text.Trim()))
                {
                    student.DuckID = DuckIDTextBox.Text.Trim().ToLower();

                    Student lookupStudent = InstructorCourse.Students.Where(x => x.StudentID != studentID).FirstOrDefault(x => x.DuckID == student.DuckID);
                    if (lookupStudent != null)
                    {
                        MessageBox("Unable to Add Student", "You already have a student with the DuckID '" + student.DuckID + "' in this course section.", "Okay");
                        errors++;
                    }

                    if (!string.IsNullOrEmpty(UOIDTextBox.Text.Trim()))
                    {
                        student.UOID = int.Parse(UOIDTextBox.Text.Trim());

                        lookupStudent = InstructorCourse.Students.Where(x => x.StudentID != studentID).FirstOrDefault(x => x.UOID == student.UOID);

                        if (lookupStudent != null)
                        {
                            MessageBox("Unable to Add Student", "You already have a student with the UOID '" + student.UOID + "' in this course section.", "Okay");
                            errors++;
                        }
                    }
                    else
                    {
                        student.UOID = null;
                    }

                    if (errors == 0)
                    {
                        bool englishAsSecondLanguageFlag;
                        if (bool.TryParse(EnglishLanguageDropDownList.SelectedValue, out englishAsSecondLanguageFlag))
                        {
                            student.EnglishSecondLanguageFlag = englishAsSecondLanguageFlag;
                        }

                        student.NativeLanguage = NativeLanguageTextBox.Text.Trim();


                        if (ViewState["Languages"] != null)
                        {
                            student.Languages = (List<ProgrammingLanguage>)ViewState["Languages"];
                        }

                        if (ViewState["Roles"] != null)
                        {
                            student.InterestedRoles = (List<Role>)ViewState["Roles"];
                        }

                        if (ViewState["Skills"] != null)
                        {
                            student.Skills = (List<Skill>)ViewState["Skills"];
                        }

                        if (ViewState["PriorCourses"] != null)
                        {
                            student.PriorCourses = (List<Course>)ViewState["PriorCourses"];
                        }

                        if (ViewState["CurrentCourses"] != null)
                        {
                            student.CurrentCourses = (List<Course>)ViewState["CurrentCourses"];
                        }

                        student.DevelopmentExperience = WorkExperienceTextBox.Text.Trim();
                        student.LearningExpectations = WorkExperienceTextBox.Text.Trim();

                        GrouperMethods.UpdateStudent(student);
                    }
                }
                else
                {
                    MessageBox("Unable to Save Student Record", "DuckID is a required field.  Please enter a DuckID for the student in order to save their record.", "Okay");
                    errors++;
                }
            }

            if (errors == 0)
            {
                _InstructorCourse = null;

                StudentsGridView_BindGridView();

                SelectedStudentIDHiddenField.Value = null;

                RolesGridView.DataSource = null;
                RolesGridView.DataBind();
                ProgrammingLanguagesGridView.DataSource = null;
                ProgrammingLanguagesGridView.DataBind();
                SkillsGridView.DataSource = null;
                SkillsGridView.DataBind();
                PriorCoursesGridView.DataSource = null;
                PriorCoursesGridView.DataBind();
                CurrentCoursesGridView.DataSource = null;
                CurrentCoursesGridView.DataBind();

                EditStudentLabel.Text = "";

                AddStudentPanel.Visible = false;
                DuckIDTextBox.Text = "";
                FirstNameTextBox.Text = "";
                PreferredNameTextBox.Text = "";
                LastNameTextBox.Text = "";
                UOIDTextBox.Text = "";

                EnglishLanguageDropDownList.SelectedIndex = 0;
                NativeLanguageTextBox.Text = "";

                WorkExperienceTextBox.Text = "";
                LearningExpectationsTextBox.Text = "";

                AddStudentLinkButton.Visible = true;
                SendWelcomeToAllStudentsLinkButton.Visible = true;
                DeleteAllStudentsLinkButton.Visible = true;

                StudentListPanel.Visible = true;

                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);

            }
        }

        protected void CancelAddStudentLinkButton_Click(object sender, EventArgs e)
        {
            EditStudentLabel.Text = "";
            AddStudentPanel.Visible = false;
            DuckIDTextBox.Text = "";
            FirstNameTextBox.Text = "";
            PreferredNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            UOIDTextBox.Text = "";

            EnglishLanguageDropDownList.SelectedIndex = 0;
            NativeLanguageTextBox.Text = "";

            WorkExperienceTextBox.Text = "";
            LearningExpectationsTextBox.Text = "";

            AddStudentLinkButton.Visible = true;
            SendWelcomeToAllStudentsLinkButton.Visible = true;
            DeleteAllStudentsLinkButton.Visible = true;

            RolesGridView.DataSource = null;
            RolesGridView.DataBind();
            ProgrammingLanguagesGridView.DataSource = null;
            ProgrammingLanguagesGridView.DataBind();
            SkillsGridView.DataSource = null;
            SkillsGridView.DataBind();

            StudentListPanel.Visible = true;

            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
        }

        #region Student Editing

        #region Prior Courses

        protected void AddPriorCourseLinkButton_Click(object sender, EventArgs e)
        {
            if (GradesDropDownList.SelectedIndex > 0)
            {
                int courseID = int.Parse(CoreCoursesDropDownList.SelectedValue);
                double grade = double.Parse(GradesDropDownList.SelectedValue);

                int studentID = 0;
                if (!string.IsNullOrEmpty(SelectedStudentIDHiddenField.Value))
                {
                    studentID = int.Parse(SelectedStudentIDHiddenField.Value);
                }

                Course course = Courses.FirstOrDefault(x => x.CourseID == courseID);
                course.Grade = grade;

                if (ViewState["PriorCourses"] == null)
                {
                    ViewState["PriorCourses"] = new List<Course>();
                    ((List<Course>)ViewState["PriorCourses"]).Add(course);
                }
                else
                {
                    ((List<Course>)ViewState["PriorCourses"]).Add(course);
                }

                double gpa = (double)((List<Course>)ViewState["PriorCourses"]).Average(x => x.Grade);

                GPALabel.Text = "Core GPA: " + gpa.ToString("#.##");

                PriorCoursesGridView.DataSource = (List<Course>)ViewState["PriorCourses"];
                PriorCoursesGridView.DataBind();

                List<Course> courses = Courses.Where(x => x.CoreCourseFlag == true).ToList();

                CoreCoursesDropDownList.DataSource = courses;
                CoreCoursesDropDownList.DataBind();

                foreach (Course c in (List<Course>)ViewState["PriorCourses"])
                {
                    CoreCoursesDropDownList.Items.FindByValue(c.CourseID.ToString()).Enabled = false;
                }

                if (((List<Course>)ViewState["PriorCourses"]).Count == courses.Count)
                {
                    AddPriorCourseLinkButton.CssClass = "btn btn-default btn-sm disabled";
                }
                GradesDropDownList.SelectedIndex = 0;
            }
            else
            {
                MessageBox("Unable to Add Prior Course", "Please select a grade for this course.", "Okay");
            }
        }

        protected void PriorCoursesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete_prior_course")
            {
                int courseID = int.Parse(e.CommandArgument.ToString());

                int studentID = 0;

                if (!string.IsNullOrEmpty(SelectedStudentIDHiddenField.Value))
                {
                    studentID = int.Parse(SelectedStudentIDHiddenField.Value);
                }

                if (ViewState["PriorCourses"] != null)
                {
                    ((List<Course>)ViewState["PriorCourses"]).RemoveAll(x => x.CourseID == courseID);
                }
                else
                {
                    ViewState["PriorCourses"] = new List<Course>();
                }

                double gpa = 0.0;
                if (ViewState["PriorCourses"] != null && ((List<Course>)ViewState["PriorCourses"]).Count > 0)
                {
                    gpa = (double)((List<Course>)ViewState["PriorCourses"]).Average(x => x.Grade);
                }

                GPALabel.Text = "Core GPA: " + gpa.ToString("#.##");

                List<Course> coreCourses = Courses.Where(x => x.CoreCourseFlag == true).ToList();

                CoreCoursesDropDownList.DataSource = coreCourses;
                CoreCoursesDropDownList.DataBind();

                foreach (Course course in (List<Course>)ViewState["PriorCourses"])
                {
                    CoreCoursesDropDownList.Items.FindByValue(course.CourseID.ToString()).Enabled = false;
                }

                if (((List<Course>)ViewState["PriorCourses"]).Count == coreCourses.Count)
                {
                    AddPriorCourseLinkButton.CssClass = "btn btn-default btn-sm disabled";
                }
                else
                {
                    AddPriorCourseLinkButton.CssClass = "btn btn-default btn-sm";
                }

                PriorCoursesGridView.DataSource = (List<Course>)ViewState["PriorCourses"];
                PriorCoursesGridView.DataBind();
            }
        }

        #endregion

        #region Current Courses

        protected void AddCurrentCourseLinkButton_Click(object sender, EventArgs e)
        {
            int courseID = int.Parse(CurrentCoursesDropDownList.SelectedValue);

            int studentID = 0;

            if (!string.IsNullOrEmpty(SelectedStudentIDHiddenField.Value))
            {
                studentID = int.Parse(SelectedStudentIDHiddenField.Value);
            }

            Course course = Courses.FirstOrDefault(x => x.CourseID == courseID);

            if (ViewState["CurrentCourses"] == null)
            {
                ViewState["CurrentCourses"] = new List<Course>();
                ((List<Course>)ViewState["CurrentCourses"]).Add(course);
            }
            else
            {
                ((List<Course>)ViewState["CurrentCourses"]).Add(course);
            }

            CurrentCoursesGridView.DataSource = (List<Course>)ViewState["CurrentCourses"];
            CurrentCoursesGridView.DataBind();

            List<Course> courses = Courses;

            CurrentCoursesDropDownList.DataSource = courses;
            CurrentCoursesDropDownList.DataBind();

            foreach (Course c in (List<Course>)ViewState["CurrentCourses"])
            {
                CurrentCoursesDropDownList.Items.FindByValue(c.CourseID.ToString()).Enabled = false;
            }

            if (((List<Course>)ViewState["CurrentCourses"]).Count == courses.Count)
            {
                AddCurrentCourseLinkButton.Enabled = false;
                AddCurrentCourseLinkButton.CssClass = "btn btn-default btn-sm disabled";
            }
            else
            {
                AddCurrentCourseLinkButton.Enabled = true;
                AddCurrentCourseLinkButton.CssClass = "btn btn-default btn-sm";
            }
        }

        protected void CurrentCoursesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete_current_course")
            {
                int courseID = int.Parse(e.CommandArgument.ToString());

                int studentID = 0;

                if (!string.IsNullOrEmpty(SelectedStudentIDHiddenField.Value))
                {
                    studentID = int.Parse(SelectedStudentIDHiddenField.Value);
                }

                if (ViewState["CurrentCourses"] != null)
                {
                    ((List<Course>)ViewState["CurrentCourses"]).RemoveAll(x => x.CourseID == courseID);
                }
                else
                {
                    ViewState["CurrentCourses"] = new List<Course>();
                }

                double gpa = 0.0;
                if ((List<Course>)ViewState["PriorCourses"] != null && ((List<Course>)ViewState["PriorCourses"]).Count > 0)
                {
                    gpa = (double)((List<Course>)ViewState["PriorCourses"]).Average(x => x.Grade);
                    GPALabel.Text = "Core GPA: " + gpa.ToString("#.##");
                }

                List<Course> courses = Courses;

                CurrentCoursesDropDownList.DataSource = courses;
                CurrentCoursesDropDownList.DataBind();

                foreach (Course course in (List<Course>)ViewState["CurrentCourses"])
                {
                    CurrentCoursesDropDownList.Items.FindByValue(course.CourseID.ToString()).Enabled = false;
                }

                CurrentCoursesGridView.DataSource = (List<Course>)ViewState["CurrentCourses"];
                CurrentCoursesGridView.DataBind();

                if (((List<Course>)ViewState["CurrentCourses"]).Count == courses.Count)
                {
                    AddCurrentCourseLinkButton.Enabled = false;
                    AddCurrentCourseLinkButton.CssClass = "btn btn-default btn-sm disabled";
                }
                else
                {
                    AddCurrentCourseLinkButton.Enabled = true;
                    AddCurrentCourseLinkButton.CssClass = "btn btn-default btn-sm";
                }
            }
        }

        #endregion

        #region Programming Languages

        protected void AddProgrammingLanguageLinkButton_Click(object sender, EventArgs e)
        {
            int programmingLanguageID = int.Parse(ProgrammingLanguagesDropDownList.SelectedValue);

            if (ProgrammingAbilityLevelDropDownList.SelectedIndex > 0)
            {
                if (ViewState["Languages"] != null)
                {
                    List<ProgrammingLanguage> languages = (List<ProgrammingLanguage>)ViewState["Languages"];

                    ProgrammingLanguage language = new ProgrammingLanguage();
                    language.Name = ProgrammingLanguagesDropDownList.SelectedItem.Text;
                    language.LanguageID = int.Parse(ProgrammingLanguagesDropDownList.SelectedValue);
                    language.ProficiencyLevel = int.Parse(ProgrammingAbilityLevelDropDownList.SelectedValue);

                    ((List<ProgrammingLanguage>)ViewState["Languages"]).Add(language);

                    ProgrammingLanguagesGridView.DataSource = (List<ProgrammingLanguage>)ViewState["Languages"];
                    ProgrammingLanguagesGridView.DataBind();

                    ProgrammingLanguagesDropDownList.Items.FindByValue(language.LanguageID.ToString()).Enabled = false;
                }
                else
                {
                    ViewState["Languages"] = new List<ProgrammingLanguage>();
                    ProgrammingLanguage language = new ProgrammingLanguage();
                    language.Name = ProgrammingLanguagesDropDownList.SelectedItem.Text;
                    language.LanguageID = int.Parse(ProgrammingLanguagesDropDownList.SelectedValue);
                    language.ProficiencyLevel = int.Parse(ProgrammingAbilityLevelDropDownList.SelectedValue);

                    ((List<ProgrammingLanguage>)ViewState["Languages"]).Add(language);

                    ProgrammingLanguagesGridView.DataSource = (List<ProgrammingLanguage>)ViewState["Languages"];
                    ProgrammingLanguagesGridView.DataBind();
                    ProgrammingLanguagesDropDownList.Items.FindByValue(language.LanguageID.ToString()).Enabled = false;
                }

                if (((List<ProgrammingLanguage>)ViewState["Languages"]).Count == Languages.Count)
                {
                    AddProgrammingLanguageLinkButton.CssClass = "btn btn-default btn-sm disabled";
                }
                else
                {
                    AddProgrammingLanguageLinkButton.CssClass = "btn btn-default btn-sm";
                }

                ProgrammingAbilityLevelDropDownList.SelectedIndex = 0;
            }
            else
            {
                MessageBox("Unable to Add Programming Language", "Please select a level of ability.", "Okay");
            }
        }

        protected void ProgrammingLanguagesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete_language")
            {
                int languageID = int.Parse(e.CommandArgument.ToString());

                if (ViewState["Languages"] != null)
                {
                    ((List<ProgrammingLanguage>)ViewState["Languages"]).RemoveAll(x => x.LanguageID == languageID).ToString();
                    ProgrammingLanguagesGridView.DataSource = (List<ProgrammingLanguage>)ViewState["Languages"];
                    ProgrammingLanguagesGridView.DataBind();

                    ProgrammingLanguagesDropDownList.Items.FindByValue(languageID.ToString()).Enabled = true;


                    if (((List<ProgrammingLanguage>)ViewState["Languages"]).Count == Languages.Count)
                    {
                        AddProgrammingLanguageLinkButton.CssClass = "btn btn-default btn-sm disabled";
                    }
                    else
                    {
                        AddProgrammingLanguageLinkButton.CssClass = "btn btn-default btn-sm";
                    }
                }
            }
        }

        #endregion

        #region Roles

        protected void AddRoleLinkButton_Click(object sender, EventArgs e)
        {
            int roleID = int.Parse(RolesDropDownList.SelectedValue);

            if (RoleInterestDropDownList.SelectedIndex > 0)
            {
                if (ViewState["Roles"] != null)
                {
                    List<Role> roles = (List<Role>)ViewState["Roles"];

                    Role role = new Role();
                    role.Name = RolesDropDownList.SelectedItem.Text;
                    role.RoleID = int.Parse(RolesDropDownList.SelectedValue);
                    role.InterestLevel = int.Parse(RoleInterestDropDownList.SelectedValue);

                    ((List<Role>)ViewState["Roles"]).Add(role);

                    RolesGridView.DataSource = (List<Role>)ViewState["Roles"];
                    RolesGridView.DataBind();

                    RolesDropDownList.Items.FindByValue(role.RoleID.ToString()).Enabled = false;
                }
                else
                {
                    ViewState["Roles"] = new List<Role>();
                    Role role = new Role();
                    role.Name = RolesDropDownList.SelectedItem.Text;
                    role.RoleID = int.Parse(RolesDropDownList.SelectedValue);
                    role.InterestLevel = int.Parse(RoleInterestDropDownList.SelectedValue);

                    ((List<Role>)ViewState["Roles"]).Add(role);

                    RolesGridView.DataSource = (List<Role>)ViewState["Roles"];
                    RolesGridView.DataBind();
                    RolesDropDownList.Items.FindByValue(role.RoleID.ToString()).Enabled = false;

                }

                RoleInterestDropDownList.SelectedIndex = 0;

                if (((List<Role>)ViewState["Roles"]).Count == Roles.Count)
                {
                    AddRoleLinkButton.CssClass = "btn btn-default btn-sm disabled";
                }
                else
                {
                    AddRoleLinkButton.CssClass = "btn btn-default btn-sm";
                }
            }
            else
            {
                MessageBox("Unable to Add Role", "Please select a level of interest.", "Okay");
            }
        }

        protected void RolesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete_role")
            {
                int roleID = int.Parse(e.CommandArgument.ToString());

                if (ViewState["Roles"] != null)
                {
                    ((List<Role>)ViewState["Roles"]).RemoveAll(x => x.RoleID == roleID).ToString();
                    RolesGridView.DataSource = (List<Role>)ViewState["Roles"];
                    RolesGridView.DataBind();

                    RolesDropDownList.Items.FindByValue(roleID.ToString()).Enabled = true;

                    if (((List<Role>)ViewState["Roles"]).Count == Roles.Count)
                    {
                        AddRoleLinkButton.CssClass = "btn btn-default btn-sm disabled";
                    }
                    else
                    {
                        AddRoleLinkButton.CssClass = "btn btn-default btn-sm";
                    }
                }
            }
        }

        #endregion

        #region Skills

        protected void AddSkillLinkButton_Click(object sender, EventArgs e)
        {
            int skillID = int.Parse(SkillsDropDownList.SelectedValue);

            if (SkillsLevelDropDownList.SelectedIndex > 0)
            {
                if (ViewState["Skills"] != null)
                {
                    List<Skill> skills = (List<Skill>)ViewState["Skills"];

                    Skill skill = new Skill();
                    skill.Name = SkillsDropDownList.SelectedItem.Text;
                    skill.SkillID = int.Parse(SkillsDropDownList.SelectedValue);
                    skill.ProficiencyLevel = int.Parse(SkillsLevelDropDownList.SelectedValue);

                    ((List<Skill>)ViewState["Skills"]).Add(skill);

                    SkillsGridView.DataSource = (List<Skill>)ViewState["Skills"];
                    SkillsGridView.DataBind();

                    SkillsDropDownList.Items.FindByValue(skill.SkillID.ToString()).Enabled = false;
                }
                else
                {
                    ViewState["Skills"] = new List<Skill>();
                    Skill skill = new Skill();
                    skill.Name = SkillsDropDownList.SelectedItem.Text;
                    skill.SkillID = int.Parse(SkillsDropDownList.SelectedValue);
                    skill.ProficiencyLevel = int.Parse(SkillsLevelDropDownList.SelectedValue);

                    ((List<Skill>)ViewState["Skills"]).Add(skill);

                    SkillsGridView.DataSource = (List<Skill>)ViewState["Skills"];
                    SkillsGridView.DataBind();
                    SkillsDropDownList.Items.FindByValue(skill.SkillID.ToString()).Enabled = false;

                }

                SkillsLevelDropDownList.SelectedIndex = 0;

                if (((List<Skill>)ViewState["Skills"]).Count == Skills.Count)
                {
                    AddSkillLinkButton.CssClass = "btn btn-default btn-sm disabled";
                }
                else
                {
                    AddSkillLinkButton.CssClass = "btn btn-default btn-sm";
                }
            }
            else
            {
                MessageBox("Unable to Add Skill", "Please select a level of ability.", "Okay");
            }
        }

        protected void SkillsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete_skill")
            {
                int skillID = int.Parse(e.CommandArgument.ToString());

                if (ViewState["Skills"] != null)
                {
                    ((List<Skill>)ViewState["Skills"]).RemoveAll(x => x.SkillID == skillID).ToString();
                    SkillsGridView.DataSource = (List<Skill>)ViewState["Skills"];
                    SkillsGridView.DataBind();

                    SkillsDropDownList.Items.FindByValue(skillID.ToString()).Enabled = true;

                    if (((List<Skill>)ViewState["Skills"]).Count == Skills.Count)
                    {
                        AddSkillLinkButton.CssClass = "btn btn-default btn-sm disabled";
                    }
                    else
                    {
                        AddSkillLinkButton.CssClass = "btn btn-default btn-sm";
                    }
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region Notifications

        protected void SendWelcomeToAllStudentsLinkButton_Click(object sender, EventArgs e)
        {
            if (InstructorCourse.Students.Where(x => x.InitialNotificationSentDate == null).Count() == 0)
            {
                MessageBox("No Students to Notify", "All students have already been sent a welcome email.  To resend, select <b>Send Welcome</b> for the individual student row.", "Okay");
            }
            else
            {
                ConfirmSendWelcomeToAllStudents();
            }
        }

        protected void SendSurveyLinkMessage(Student student)
        {
            
            string header = @" <html>
                                <head>
                                    <style>
                                        body {
                                            font-size:15px;
	                                        font-family:'Calibri',sans-serif;
                                            }
                                        p, ul, li {
                                            font-size:15px;
	                                        font-family:'Calibri',sans-serif;
                                            }
                                    </style>
                                </head>
                                <body>";

            string footer = @"</body>
                        </html>";

            string messageBody = header;

            messageBody +=
                                @"<table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable'>
                        <tr>
                            <td>
                                <table border='0' cellpadding='20' cellspacing='0' width='800' id='emailContainer'>
                                    <tr>
                                        <td valign='top'>";
            messageBody += "<h3>" + InstructorCourse.Course.FullName + " - Welcome</h3>";

            string link = "";

            link = "https://www2.lcb.uoregon.edu/Forms/grouperapp?id=" + student.GUID;

            messageBody += @"</td>
                    </tr>
                    <tr>
                                    <td>
                                        <p>Welcome to " + InstructorCourse.Course.FullName + @".  This course requires you to complete a short survey to best match you to a group.</p>
                                        <p>
                                            At your earliest convenience, please visit <a href='" + link + "'>" + link + @"</a> and complete the survey.
                                        </p>
                                    </td>
                                </tr>";
            messageBody += @"
                            </table>
                        </td> 
                    </tr>
                </table> ";

            messageBody += footer;

            SmtpClient client = new SmtpClient();
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("donotreply@uoregon.edu", InstructorCourse.Course.FullName);

            mailMessage.To.Add(student.DuckID + "@uoregon.edu");
            mailMessage.Subject = "Welcome to " + InstructorCourse.Course.Code;
            mailMessage.Body = messageBody;
            mailMessage.IsBodyHtml = true;

            if (client.Host != null)
            {
                if (mailMessage.To.Count > 0)
                {
                    try
                    {
                        client.Send(mailMessage);
                    }
                    catch (SmtpFailedRecipientException ex)
                    {
                        SmtpStatusCode statusCode = ex.StatusCode;

                        if (statusCode == SmtpStatusCode.MailboxBusy || statusCode == SmtpStatusCode.MailboxUnavailable || statusCode == SmtpStatusCode.TransactionFailed)
                        {
                            Thread.Sleep(2000);

                            client.Send(mailMessage);
                        }
                        else
                        {
                            throw;
                        }
                    }
                    finally
                    {
                        mailMessage.Dispose();
                    }
                }
            }
        }

        #endregion

        #region Event Handlers

        protected void DeleteAllStudentsLinkButton_Click(object sender, EventArgs e)
        {
            ConfirmDeleteAllStudentsMessageBox();
        }

        protected void BeginGroupingLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Groups.aspx?ID=" + InstructorCourseID);
        }

        #endregion
    }
}