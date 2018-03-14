using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GroupBuilder;

namespace GroupBuilderAdmin
{
    public partial class Courses : System.Web.UI.Page
    {
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

        private void ConfirmDeleteMessageBox(Course course)
        {
            SelectedCourseIDHiddenField.Value = course.CourseID.ToString();
            MessageBoxTitleLabel.Text = "Delete Course?";
            MessageBoxMessageLabel.Text = "Are you sure you want to delete the course '" + course.FullName + "?<br /><br /><span class='text-danger'>This will result in the loss of any Student related information connected to this course for any users of the system.</span>";
            MessageBoxOkayLinkButton.Text = "<span class='fa fa-ban'></span>&nbsp;&nbsp;Cancel";
            Page.SetFocus(MessageBoxOkayLinkButton);
            MessageBoxOkayLinkButton.Visible = true;
            MessageBoxCreateLinkButton.CssClass = "btn btn-danger";
            MessageBoxCreateLinkButton.Text = "<span class='fa fa-remove'></span>&nbsp;&nbsp;Delete Course";
            MessageBoxCreateLinkButton.Visible = true;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "messageBox", "$('#messageBox').modal();", true);
            upModal.Update();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CoursesGridView_BindGridView();
            }
        }

        protected void CoursesGridView_BindGridView()
        {
            List<Course> courses = GrouperMethods.GetCourses();
            CoursesGridView.DataSource = courses;
            CoursesGridView.DataBind();
        }

        protected void CoursesGridView_BindGridView(int editIndex)
        {
            List<Course> courses = GrouperMethods.GetCourses();
            CoursesGridView.DataSource = courses;
            CoursesGridView.EditIndex = editIndex;
            CoursesGridView.DataBind();
        }

        protected void CoursesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;
            int courseID = int.Parse(CoursesGridView.DataKeys[index].Values[0].ToString());
            Course course = GrouperMethods.GetCourse(courseID);

            if (e.CommandName == "edit_course")
            {
                CoursesGridView_BindGridView(index);
            }
            else if(e.CommandName == "cancel_edit")
            {
                CoursesGridView_BindGridView(-1);
            }
            else if(e.CommandName == "save_changes")
            {


                if(course != null)
                {
                    TextBox codeTextBox = (TextBox)row.FindControl("CodeTextBox");
                    course.Code = codeTextBox.Text.Trim();

                    TextBox nameTextBox = (TextBox)row.FindControl("NameTextBox");
                    course.Name = nameTextBox.Text.Trim();

                    DropDownList coreCourseDropDownList = (DropDownList)row.FindControl("CoreCourseFlagDropDownList");
                    course.CoreCourseFlag = bool.Parse(coreCourseDropDownList.SelectedValue);

                    GrouperMethods.UpdateCourse(course);

                    CoursesGridView_BindGridView(-1);
                }
            }
            else if(e.CommandName == "delete_course")
            {
                ConfirmDeleteMessageBox(course);
            }
        }

        protected void MessageBoxCreateLinkButton_Click(object sender, EventArgs e)
        {
            int courseID = int.Parse(SelectedCourseIDHiddenField.Value);

            GrouperMethods.DeleteCourse(courseID);

            CoursesGridView_BindGridView();

            MessageBox("Course Deleted", "The course was deleted.", "Okay");

        }

        protected void CancelAddCourseLinkButton_Click(object sender, EventArgs e)
        {
            AddCoursePanel.Visible = false;
            CoursesListPanel.Visible = true;
            CodeTextBox.Text = "";
            NameTextBox.Text = "";
            CoreCourseDropDownList.SelectedIndex = 0;
            AddCoursePanel.Visible = false;
            AddCourseLinkButton.Visible = true;

        }

        protected void SaveAddCourseLinkButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CodeTextBox.Text.Trim()) && !string.IsNullOrEmpty(NameTextBox.Text.Trim()))
            {
                Course course = new GroupBuilder.Course();
                course.Code = CodeTextBox.Text.Trim();
                course.Name = NameTextBox.Text.Trim();
                course.CoreCourseFlag = bool.Parse(CoreCourseDropDownList.SelectedValue);

                int courseID = GrouperMethods.InsertCourse(course);
                AddCoursePanel.Visible = false;
                CoursesListPanel.Visible = true;
                AddCourseLinkButton.Visible = true;

                CoursesGridView_BindGridView();

                MessageBox("Course Added", "'" + course.FullName + "' has been added to the list of courses.", "Okay");
            }
            else
            {
                MessageBox("Unable to Add Course", "Please provide a course Code and Name.", "Okay");
            }
        }

        protected void AddCourseLinkButton_Click(object sender, EventArgs e)
        {
            AddCoursePanel.Visible = true;
            CoursesListPanel.Visible = false;
            CodeTextBox.Text = "";
            NameTextBox.Text = "";
            CoreCourseDropDownList.SelectedIndex = 0;
            AddCourseLinkButton.Visible = false;

        }
    }
}