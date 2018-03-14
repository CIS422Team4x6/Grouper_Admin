using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GroupBuilder;

namespace GroupBuilderAdmin
{
    public partial class Components : System.Web.UI.Page
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
            SelectedRoleIDHiddenField.Value = null;
            SelectedLanguageIDHiddenField.Value = null;
            SelectedSkillIDHiddenField.Value = null;
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

        private void ConfirmDeleteMessageBox(ProgrammingLanguage language)
        {
            SelectedCourseIDHiddenField.Value = null;
            SelectedRoleIDHiddenField.Value = null;
            SelectedSkillIDHiddenField.Value = null;
            SelectedLanguageIDHiddenField.Value = language.LanguageID.ToString();

            MessageBoxTitleLabel.Text = "Delete Programming Language?";
            MessageBoxMessageLabel.Text = "Are you sure you want to delete the language '" + language.Name + "?<br /><br /><span class='text-danger'>This will result in the loss of any Student related information connected to this language for any users of the system.</span>";
            MessageBoxOkayLinkButton.Text = "<span class='fa fa-ban'></span>&nbsp;&nbsp;Cancel";
            Page.SetFocus(MessageBoxOkayLinkButton);
            MessageBoxOkayLinkButton.Visible = true;
            MessageBoxCreateLinkButton.CssClass = "btn btn-danger";
            MessageBoxCreateLinkButton.Text = "<span class='fa fa-remove'></span>&nbsp;&nbsp;Delete Language";
            MessageBoxCreateLinkButton.Visible = true;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "messageBox", "$('#messageBox').modal();", true);
            upModal.Update();
        }

        private void ConfirmDeleteMessageBox(Role role)
        {
            SelectedCourseIDHiddenField.Value = null;
            SelectedLanguageIDHiddenField.Value = null;
            SelectedSkillIDHiddenField.Value = null;
            SelectedRoleIDHiddenField.Value = role.RoleID.ToString();

            MessageBoxTitleLabel.Text = "Delete Role?";
            MessageBoxMessageLabel.Text = "Are you sure you want to delete the role '" + role.Name + "?<br /><br /><span class='text-danger'>This will result in the loss of any Student related information connected to this role for any users of the system.</span>";
            MessageBoxOkayLinkButton.Text = "<span class='fa fa-ban'></span>&nbsp;&nbsp;Cancel";
            Page.SetFocus(MessageBoxOkayLinkButton);
            MessageBoxOkayLinkButton.Visible = true;
            MessageBoxCreateLinkButton.CssClass = "btn btn-danger";
            MessageBoxCreateLinkButton.Text = "<span class='fa fa-remove'></span>&nbsp;&nbsp;Delete Role";
            MessageBoxCreateLinkButton.Visible = true;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "messageBox", "$('#messageBox').modal();", true);
            upModal.Update();
        }

        private void ConfirmDeleteMessageBox(Skill skill)
        {
            SelectedCourseIDHiddenField.Value = null;
            SelectedLanguageIDHiddenField.Value = null;
            SelectedRoleIDHiddenField.Value = null;
            SelectedSkillIDHiddenField.Value = skill.SkillID.ToString();

            MessageBoxTitleLabel.Text = "Delete Skill?";
            MessageBoxMessageLabel.Text = "Are you sure you want to delete the skill '" + skill.Name + "?<br /><br /><span class='text-danger'>This will result in the loss of any Student related information connected to this skill for any users of the system.</span>";
            MessageBoxOkayLinkButton.Text = "<span class='fa fa-ban'></span>&nbsp;&nbsp;Cancel";
            Page.SetFocus(MessageBoxOkayLinkButton);
            MessageBoxOkayLinkButton.Visible = true;
            MessageBoxCreateLinkButton.CssClass = "btn btn-danger";
            MessageBoxCreateLinkButton.Text = "<span class='fa fa-remove'></span>&nbsp;&nbsp;Delete Skill";
            MessageBoxCreateLinkButton.Visible = true;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "messageBox", "$('#messageBox').modal();", true);
            upModal.Update();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CoursesGridView_BindGridView();
                LanguagesGridView_BindGridView();
                RolesGridView_BindGridView();
                SkillsGridView_BindGridView();
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
            else if (e.CommandName == "cancel_edit")
            {
                CoursesGridView_BindGridView(-1);
            }
            else if (e.CommandName == "save_changes")
            {


                if (course != null)
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
            else if (e.CommandName == "delete_course")
            {
                ConfirmDeleteMessageBox(course);
            }
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

        protected void MessageBoxCreateLinkButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SelectedCourseIDHiddenField.Value))
            {
                int courseID = int.Parse(SelectedCourseIDHiddenField.Value);

                GrouperMethods.DeleteCourse(courseID);

                CoursesGridView_BindGridView();

                MessageBox("Course Deleted", "The course was deleted.", "Okay");
            }
            else if (!string.IsNullOrEmpty(SelectedLanguageIDHiddenField.Value))
            {
                int languageID = int.Parse(SelectedLanguageIDHiddenField.Value);

                GrouperMethods.DeleteLanguage(languageID);

                LanguagesGridView_BindGridView();

                MessageBox("Language Deleted", "The language was deleted.", "Okay");
            }
            else if (!string.IsNullOrEmpty(SelectedRoleIDHiddenField.Value))
            {
                int roleID = int.Parse(SelectedRoleIDHiddenField.Value);

                GrouperMethods.DeleteRole(roleID);

                RolesGridView_BindGridView();

                MessageBox("Role Deleted", "The role was deleted.", "Okay");
            }
            if (!string.IsNullOrEmpty(SelectedSkillIDHiddenField.Value))
            {
                int skillID = int.Parse(SelectedSkillIDHiddenField.Value);

                GrouperMethods.DeleteSkill(skillID);

                SkillsGridView_BindGridView();

                MessageBox("Skill Deleted", "The skill was deleted.", "Okay");
            }

        }


        #region Languages

        protected void LanguagesGridView_BindGridView()
        {
            List<ProgrammingLanguage> languages = GrouperMethods.GetLanguages();
            LanguagesGridView.DataSource = languages;
            LanguagesGridView.DataBind();
        }

        protected void LanguagesGridView_BindGridView(int editIndex)
        {
            List<ProgrammingLanguage> languages = GrouperMethods.GetLanguages();
            LanguagesGridView.DataSource = languages;
            LanguagesGridView.EditIndex = editIndex;
            LanguagesGridView.DataBind();
        }

        protected void LanguagesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;
            int languageID = int.Parse(LanguagesGridView.DataKeys[index].Values[0].ToString());
            ProgrammingLanguage language = GrouperMethods.GetLanguage(languageID);

            if (e.CommandName == "edit_language")
            {
                LanguagesGridView_BindGridView(index);
            }
            else if (e.CommandName == "cancel_edit_language")
            {
                LanguagesGridView_BindGridView(-1);
            }
            else if (e.CommandName == "save_language_changes")
            {
                if (language != null)
                {
                    TextBox nameTextBox = (TextBox)row.FindControl("EditLanguageNameTextBox");
                    language.Name = nameTextBox.Text.Trim();

                    GrouperMethods.UpdateLanguage(language);

                    LanguagesGridView_BindGridView(-1);
                }
            }
            else if (e.CommandName == "delete_language")
            {
                ConfirmDeleteMessageBox(language);
            }
        }

        protected void AddLanguageLinkButton_Click(object sender, EventArgs e)
        {
            AddLanguagePanel.Visible = true;
            LanguagesListPanel.Visible = false;
            LanguageNameTextBox.Text = "";
            AddLanguageLinkButton.Visible = false;
        }

        protected void CancelAddLanguageLinkButton_Click(object sender, EventArgs e)
        {
            AddLanguagePanel.Visible = false;
            LanguagesListPanel.Visible = true;
            LanguageNameTextBox.Text = "";
            AddLanguagePanel.Visible = false;
            AddLanguageLinkButton.Visible = true;
        }

        protected void SaveAddLanguageLinkButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LanguageNameTextBox.Text.Trim()))
            {
                ProgrammingLanguage language = new ProgrammingLanguage();
                language.Name = LanguageNameTextBox.Text.Trim();

                int languageID = GrouperMethods.InsertLanguage(language);
                AddLanguagePanel.Visible = false;
                LanguagesListPanel.Visible = true;
                AddLanguageLinkButton.Visible = true;

                LanguagesGridView_BindGridView();

                MessageBox("Language Added", "'" + language.Name + "' has been added to the list of languages.", "Okay");
            }
            else
            {
                MessageBox("Unable to Add Language", "Please provide a Language name.", "Okay");
            }
        }

        #endregion

        #region Roles

        protected void RolesGridView_BindGridView()
        {
            List<Role> roles = GrouperMethods.GetRoles();
            RolesGridView.DataSource = roles;
            RolesGridView.DataBind();
        }

        protected void RolesGridView_BindGridView(int editIndex)
        {
            List<Role> roles = GrouperMethods.GetRoles();
            RolesGridView.DataSource = roles;
            RolesGridView.EditIndex = editIndex;
            RolesGridView.DataBind();
        }

        protected void RolesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;
            int roleID = int.Parse(RolesGridView.DataKeys[index].Values[0].ToString());
            Role role = GrouperMethods.GetRole(roleID);

            if (e.CommandName == "edit_role")
            {
                RolesGridView_BindGridView(index);
            }
            else if (e.CommandName == "cancel_edit_role")
            {
                RolesGridView_BindGridView(-1);
            }
            else if (e.CommandName == "save_role_changes")
            {
                if (role != null)
                {
                    TextBox nameTextBox = (TextBox)row.FindControl("EditRoleNameTextBox");
                    role.Name = nameTextBox.Text.Trim();

                    GrouperMethods.UpdateRole(role);

                    RolesGridView_BindGridView(-1);
                }
            }
            else if (e.CommandName == "delete_role")
            {
                ConfirmDeleteMessageBox(role);
            }
        }

        protected void AddRoleLinkButton_Click(object sender, EventArgs e)
        {
            AddRolePanel.Visible = true;
            RolesListPanel.Visible = false;
            RoleNameTextBox.Text = "";
            AddRoleLinkButton.Visible = false;
        }

        protected void CancelAddRoleLinkButton_Click(object sender, EventArgs e)
        {
            AddRolePanel.Visible = false;
            RolesListPanel.Visible = true;
            RoleNameTextBox.Text = "";
            AddRolePanel.Visible = false;
            AddRoleLinkButton.Visible = true;
        }

        protected void SaveAddRoleLinkButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(RoleNameTextBox.Text.Trim()))
            {
                Role role = new Role();
                role.Name = RoleNameTextBox.Text.Trim();

                int roleID = GrouperMethods.InsertRole(role);
                AddRolePanel.Visible = false;
                RolesListPanel.Visible = true;
                AddRoleLinkButton.Visible = true;

                RolesGridView_BindGridView();

                MessageBox("Role Added", "'" + role.Name + "' has been added to the list of roles.", "Okay");
            }
            else
            {
                MessageBox("Unable to Add Role", "Please provide a Role name.", "Okay");
            }
        }

        #endregion

        #region Skills

        protected void SkillsGridView_BindGridView()
        {
            List<Skill> skills = GrouperMethods.GetSkills();
            SkillsGridView.DataSource = skills;
            SkillsGridView.DataBind();
        }

        protected void SkillsGridView_BindGridView(int editIndex)
        {
            List<Skill> skills = GrouperMethods.GetSkills();
            SkillsGridView.DataSource = skills;
            SkillsGridView.EditIndex = editIndex;
            SkillsGridView.DataBind();
        }

        protected void SkillsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;
            int skillID = int.Parse(SkillsGridView.DataKeys[index].Values[0].ToString());
            Skill skill = GrouperMethods.GetSkill(skillID);

            if (e.CommandName == "edit_skill")
            {
                SkillsGridView_BindGridView(index);
            }
            else if (e.CommandName == "cancel_edit_skill")
            {
                SkillsGridView_BindGridView(-1);
            }
            else if (e.CommandName == "save_skill_changes")
            {
                if (skill != null)
                {
                    TextBox nameTextBox = (TextBox)row.FindControl("EditSkillNameTextBox");
                    skill.Name = nameTextBox.Text.Trim();

                    GrouperMethods.UpdateSkill(skill);

                    SkillsGridView_BindGridView(-1);
                }
            }
            else if (e.CommandName == "delete_skill")
            {
                ConfirmDeleteMessageBox(skill);
            }
        }

        protected void AddSkillLinkButton_Click(object sender, EventArgs e)
        {
            AddSkillPanel.Visible = true;
            SkillsListPanel.Visible = false;
            SkillNameTextBox.Text = "";
            AddSkillLinkButton.Visible = false;
        }

        protected void CancelAddSkillLinkButton_Click(object sender, EventArgs e)
        {
            AddSkillPanel.Visible = false;
            SkillsListPanel.Visible = true;
            SkillNameTextBox.Text = "";
            AddSkillPanel.Visible = false;
            AddSkillLinkButton.Visible = true;
        }

        protected void SaveAddSkillLinkButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SkillNameTextBox.Text.Trim()))
            {
                Skill skill = new Skill();
                skill.Name = SkillNameTextBox.Text.Trim();

                int skillID = GrouperMethods.InsertSkill(skill);
                AddSkillPanel.Visible = false;
                SkillsListPanel.Visible = true;
                AddSkillLinkButton.Visible = true;

                SkillsGridView_BindGridView();

                MessageBox("Skill Added", "'" + skill.Name + "' has been added to the list of skills.", "Okay");
            }
            else
            {
                MessageBox("Unable to Add Skill", "Please provide a Skill name.", "Okay");
            }
        }

        #endregion
    }
}