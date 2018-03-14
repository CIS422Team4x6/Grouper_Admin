using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GroupBuilder;
using Microsoft.AspNet.Identity;

namespace GroupBuilderAdmin
{
    public partial class Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CourseSectionsGridView_BindGridView();
                BindDropDownLists();
            }
        }

        #region Dialogs

        // Confirmation Modal
        private void ConfirmDeleteMessageBox(InstructorCourse course)
        {
            SelectedInstructorCourseHiddenField.Value = course.InstructorCourseID.ToString();
            MessageBoxTitleLabel.Text = "Delete Course Section?";
            MessageBoxMessageLabel.Text = "Are you sure you want to delete the course section '" + course.Course.FullName + "'?<br /><br/><span class='text-danger'>This will delete all associated Groups and Student Records and cannot be undone.</span>";
            MessageBoxOkayLinkButton.Text = "<span class='fa fa-ban'></span>&nbsp;&nbsp;Cancel";
            MessageBoxOkayLinkButton.Visible = true;
            MessageBoxCreateLinkButton.CssClass = "btn btn-danger";
            MessageBoxCreateLinkButton.Text = "<span class='fas fa-times'></span>&nbsp;&nbsp;Delete Course Section";
            MessageBoxCreateLinkButton.Visible = true;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "messageBox", "$('#messageBox').modal();", true);
            upModal.Update();
        }

        // Confirms the deletion of a Course Section
        protected void MessageBoxCreateLinkButton_Click(object sender, EventArgs e)
        {
            int instructorCourseID = int.Parse(SelectedInstructorCourseHiddenField.Value);

            GrouperMethods.DeleteInstructorCourse(instructorCourseID);

            CourseSectionsGridView_BindGridView();

            AddCourseSectionLinkButton.Visible = true;
        }

        #endregion

        #region Bind Objects
        
        // Binds drop down lists throughout the page
        protected void BindDropDownLists()
        {
            List<Course> courses = GrouperMethods.GetCourses();

            CoursesDropDownList.DataSource = courses;
            CoursesDropDownList.DataBind();

            List<ListItem> years = new List<ListItem>();

            for (int i = DateTime.Now.Year; i < DateTime.Now.Year + 10; i++)
            {
                ListItem year = new ListItem { Text = i.ToString(), Value = i.ToString() };
                years.Add(year);
            }

            YearsDropDownList.DataSource = years;
            YearsDropDownList.DataBind();

            List<ListItem> startTimes = new List<ListItem>();
            List<ListItem> endTimes = new List<ListItem>();

            for (int i = 8; i < 19; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    int hour = i;
                    if (i > 12)
                    {
                        hour = i - 12;
                    }

                    string minutes = (j * 30).ToString();

                    if (minutes.Length == 1)
                    {
                        minutes += "0";
                    }

                    string time = hour.ToString() + ":" + minutes;

                    ListItem item = new ListItem { Text = time, Value = time };
                    startTimes.Add(item);

                }
            }

            for (int i = 8; i < 19; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    int hour = i;
                    if (i > 12)
                    {
                        hour = i - 12;
                    }

                    string minutes = (j * 10).ToString();

                    if (minutes.Length == 1)
                    {
                        minutes += "0";
                    }

                    string time = hour.ToString() + ":" + minutes;

                    ListItem item = new ListItem { Text = time, Value = time };
                    endTimes.Add(item);

                }
            }

            StartTimeDropDownList.DataSource = startTimes;
            StartTimeDropDownList.DataBind();

            EndTimeDropDownList.DataSource = endTimes;
            EndTimeDropDownList.DataBind();
        }

        #endregion

        #region CourseSectionsGridView

        protected void CourseSectionsGridView_BindGridView()
        {
            string userName = "";

            if (Context.User.Identity != null)
            {
                userName = Context.User.Identity.GetUserName();
            }

            List<InstructorCourse> courses = new List<InstructorCourse>();

            if (!string.IsNullOrEmpty(userName))
            {
                Instructor instructor = GrouperMethods.GetInstructor(userName);

                if (instructor != null)
                {
                    InstructorNameLabel.Text = instructor.FirstName + " " + instructor.LastName;
                }

                courses = GrouperMethods.GetInstructorCourses(instructor.InstructorID);

                if (courses.Count > 0)
                {
                    CoursesPanel.Visible = true;
                    NoCoursesPanel.Visible = false;
                    AddCourseSectionPanel.Visible = false;
                }
                else
                {
                    AddCourseSectionPanel.Visible = false;
                    NoCoursesPanel.Visible = true;
                    CoursesPanel.Visible = false;
                }
            }

            CourseSectionsGridView.DataSource = courses;
            CourseSectionsGridView.DataBind();
        }

        protected void CourseSectionsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int instructorCourseID = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "delete_instructor_course")
            {
                InstructorCourse instructorCourse = GrouperMethods.GetInstructorCourse(instructorCourseID);

                ConfirmDeleteMessageBox(instructorCourse);
            }
            else if (e.CommandName == "select_instructor_course")
            {
                Response.Redirect("students.aspx?ID=" + instructorCourseID);
            }
            else if (e.CommandName == "edit_instructor_course")
            {
                SelectedInstructorCourseHiddenField.Value = instructorCourseID.ToString();
                upModal.Update();

                InstructorCourse course = GrouperMethods.GetInstructorCourse(instructorCourseID);

                if (course != null)
                {
                    AddCourseSectionPanel.Visible = true;
                    CoursesPanel.Visible = false;

                    if (course.CRN > 0)
                    {
                        CRNTextBox.Text = course.CRN.ToString();
                    }

                    CoursesDropDownList.SelectedValue = course.CourseID.ToString();

                    if (course.Year > 0)
                    {
                        YearsDropDownList.SelectedValue = course.Year.ToString();
                    }

                    if (course.TermNumber > 0)
                    {
                        TermsDropDownList.SelectedValue = course.TermNumber.ToString();
                    }

                    foreach (ListItem item in DaysOfWeekCheckBox.Items)
                    {
                        foreach (char day in course.DaysOfWeek)
                        {
                            if (item.Value.ToLower() == day.ToString().ToLower())
                            {
                                item.Selected = true;
                            }
                        }
                    }

                    if (course.StartTime != null)
                    {
                        StartTimeDropDownList.SelectedValue = course.StartTime.Value.ToString("h:mm");
                    }

                    if (course.EndTime != null)
                    {
                        EndTimeDropDownList.SelectedValue = course.EndTime.Value.ToString("h:mm");
                    }
                }
            }
        }

        #endregion

        #region Event Handlers

        protected void AddCourseSectionLinkButton_Click(object sender, EventArgs e)
        {
            CoursesDropDownList.SelectedIndex = 0;
            TermsDropDownList.SelectedIndex = 0;
            YearsDropDownList.SelectedIndex = 0;
            StartTimeDropDownList.SelectedIndex = 0;
            EndTimeDropDownList.SelectedIndex = 0;
            CRNTextBox.Text = "";

            foreach (ListItem item in DaysOfWeekCheckBox.Items)
            {
                item.Selected = false;
            }

            AddCourseSectionPanel.Visible = true;
            AddCourseSectionLinkButton.Visible = false;
            CoursesPanel.Visible = false;
        }

        protected void CancelAddCourseSectionLinkButton_Click(object sender, EventArgs e)
        {
            CoursesDropDownList.SelectedIndex = 0;
            TermsDropDownList.SelectedIndex = 0;
            YearsDropDownList.SelectedIndex = 0;
            StartTimeDropDownList.SelectedIndex = 0;
            EndTimeDropDownList.SelectedIndex = 0;
            CRNTextBox.Text = "";

            AddCourseSectionPanel.Visible = false;
            AddCourseSectionLinkButton.Visible = true;
            CoursesPanel.Visible = true;

            string userName = "";


            if (Context.User.Identity != null)
            {
                userName = Context.User.Identity.GetUserName();
            }

            List<InstructorCourse> courses = new List<InstructorCourse>();

            if (!string.IsNullOrEmpty(userName))
            {
                Instructor instructor = GrouperMethods.GetInstructor(userName);

                if (instructor != null)
                {
                    InstructorNameLabel.Text = instructor.FirstName + " " + instructor.LastName;
                }

                courses = GrouperMethods.GetInstructorCourses(instructor.InstructorID);

                if (courses.Count > 0)
                {
                    NoCoursesPanel.Visible = false;
                    CoursesPanel.Visible = true;
                }
                else
                {
                    NoCoursesPanel.Visible = true;
                    CoursesPanel.Visible = false;
                }
            }
        }

        protected void SaveCourseSectionLinkButton_Click(object sender, EventArgs e)
        {
            string userName = Context.User.Identity.GetUserName();
            Instructor instructor = GrouperMethods.GetInstructor(userName);

            if (!string.IsNullOrEmpty(SelectedInstructorCourseHiddenField.Value))
            {
                int instructorCourseID = int.Parse(SelectedInstructorCourseHiddenField.Value);

                InstructorCourse course = GrouperMethods.GetInstructorCourse(instructorCourseID);

                course.CourseID = int.Parse(CoursesDropDownList.SelectedValue);

                if (TermsDropDownList.SelectedIndex > 0)
                {
                    course.TermNumber = int.Parse(TermsDropDownList.SelectedValue);
                }

                if (YearsDropDownList.SelectedIndex > 0)
                {
                    course.Year = int.Parse(YearsDropDownList.SelectedValue);
                }

                if (course.TermNumber > 0)
                {
                    switch (course.TermNumber)
                    {
                        case 1:
                            course.TermName = "Fall " + course.Year.ToString();
                            break;
                        case 2:
                            course.TermName = "Winter " + course.Year.ToString();
                            break;
                        case 3:
                            course.TermName = "Spring " + course.Year.ToString();
                            break;
                        case 4:
                            course.TermName = "Summer " + course.Year.ToString();
                            break;
                    }
                }


                if (StartTimeDropDownList.SelectedIndex > 0)
                {
                    course.StartTime = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + StartTimeDropDownList.SelectedValue);
                }

                if (EndTimeDropDownList.SelectedIndex > 0)
                {
                    course.EndTime = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + EndTimeDropDownList.SelectedValue);
                }

                string daysOfWeek = "";

                foreach (ListItem item in DaysOfWeekCheckBox.Items)
                {
                    if (item.Selected == true)
                    {
                        daysOfWeek += item.Value;
                    }
                }

                if (!string.IsNullOrEmpty(daysOfWeek))
                {
                    course.DaysOfWeek = daysOfWeek;
                }

                if (!string.IsNullOrEmpty(CRNTextBox.Text.Trim()) && CRNTextBox.Text.Trim().Length == 5)
                {
                    int crn;
                    if (int.TryParse(CRNTextBox.Text.Trim(), out crn))
                    {
                        course.CRN = crn;
                    }
                }

                GrouperMethods.UpdateInstructorCourse(course);
                SelectedInstructorCourseHiddenField.Value = null;
                upModal.Update();

                CourseSectionsGridView_BindGridView();

                AddCourseSectionLinkButton.Visible = true;
            }
            else
            {
                if (instructor != null)
                {
                    InstructorCourse course = new InstructorCourse();
                    course.InstructorID = instructor.InstructorID;
                    course.CourseID = int.Parse(CoursesDropDownList.SelectedValue);

                    if (TermsDropDownList.SelectedIndex > 0)
                    {
                        course.TermNumber = int.Parse(TermsDropDownList.SelectedValue);
                    }

                    if (YearsDropDownList.SelectedIndex > 0)
                    {
                        course.Year = int.Parse(YearsDropDownList.SelectedValue);
                    }

                    if (course.TermNumber > 0)
                    {
                        switch (course.TermNumber)
                        {
                            case 1:
                                course.TermName = "Fall " + course.Year.ToString();
                                break;
                            case 2:
                                course.TermName = "Winter " + course.Year.ToString();
                                break;
                            case 3:
                                course.TermName = "Spring " + course.Year.ToString();
                                break;
                            case 4:
                                course.TermName = "Summer " + course.Year.ToString();
                                break;
                        }
                    }


                    if (StartTimeDropDownList.SelectedIndex > 0)
                    {
                        course.StartTime = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + StartTimeDropDownList.SelectedValue);
                    }

                    if (EndTimeDropDownList.SelectedIndex > 0)
                    {
                        course.EndTime = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + EndTimeDropDownList.SelectedValue);
                    }

                    string daysOfWeek = "";

                    foreach (ListItem item in DaysOfWeekCheckBox.Items)
                    {
                        if (item.Selected == true)
                        {
                            daysOfWeek += item.Value;
                        }
                    }

                    if (!string.IsNullOrEmpty(daysOfWeek))
                    {
                        course.DaysOfWeek = daysOfWeek;
                    }

                    if (!string.IsNullOrEmpty(CRNTextBox.Text.Trim()) && CRNTextBox.Text.Trim().Length == 5)
                    {
                        int crn;
                        if (int.TryParse(CRNTextBox.Text.Trim(), out crn))
                        {
                            course.CRN = crn;
                        }
                    }

                    int instructorCourseID = GrouperMethods.InsertInstructorCourse(course);

                    CourseSectionsGridView_BindGridView();
                    AddCourseSectionLinkButton.Visible = true;
                }
            }
        }

        #endregion

    }
}