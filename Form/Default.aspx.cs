using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GroupBuilder;

namespace GroupBuilderAdmin.Form
{
    public partial class Default : System.Web.UI.Page
    {
        private static bool TEST_FLAG = true;

        private string _GUID;
        protected string GUID
        {
            get
            {
                _GUID = "";
                if (Request.QueryString["id"] != "" && Request.QueryString["id"] != null)
                {
                    _GUID = Request.QueryString["id"].Trim().ToLower();
                }
                return _GUID;
            }
            set
            {
                _GUID = value;
            }
        }

        private Student _Student;
        protected Student ThisStudent
        {
            get
            {
                if (_Student == null)
                {
                    // Try to get the student by the GUID
                    if (!String.IsNullOrEmpty(GUID))
                    {
                        _Student = GrouperMethods.GetStudentByGUID(GUID);
                        // Bad GUID - so, give them back an empty student
                    }
                }
                return _Student;
            }
            set
            {
                _Student = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(ThisStudent == null)
            {
                Response.Redirect("Oops.aspx");
            }
            else if (ThisStudent.SurveySubmittedDate != null)
            {
                if (!TEST_FLAG)
                {
                    Response.Redirect("Oops.aspx");
                }
            }

            if (!IsPostBack)
            {
                PreferedNameTextBox.Text = ThisStudent.FirstName;

                ClassesRepeater_BindRepeater();
                RolesRepeater_BindRepeater();
                LanguagesRepeater_BindRepeater();
                SkillsRepeater_BindRepeater();
            }

        }

        protected void ClassesRepeater_BindRepeater()
        {
            List<Course> courses = GrouperMethods.GetCourses().Where(x => x.CoreCourseFlag == true).ToList();

            ClassesRepeater.DataSource = courses;
            ClassesRepeater.DataBind();
        }

        protected void RolesRepeater_BindRepeater()
        {
            List<Role> roles = GrouperMethods.GetRoles();

            RolesRepeater.DataSource = roles;
            RolesRepeater.DataBind();

        }

        protected void LanguagesRepeater_BindRepeater()
        {
            List<ProgrammingLanguage> languages = GrouperMethods.GetLanguages();

            LanguagesRepeater.DataSource = languages;
            LanguagesRepeater.DataBind();
        }

        protected void SkillsRepeater_BindRepeater()
        {
            List<Skill> skills = GrouperMethods.GetSkills();

            SkillsRepeater.DataSource = skills;
            SkillsRepeater.DataBind();
        }

        protected void SubmitLinkButton_Click(object sender, EventArgs e)
        {
            Student student = ThisStudent;

            //Set submit time
            student.SurveySubmittedDate = DateTime.Now;

            //Set prefered name
            student.PreferredName = PreferedNameTextBox.Text;

            //Set prior courses
            foreach (RepeaterItem courseItem in ClassesRepeater.Items)
            {
                HiddenField courseIdHiddenField = (HiddenField)courseItem.FindControl("CourseIdHiddenField");
                int courseID = int.Parse(courseIdHiddenField.Value);
                Course course = GrouperMethods.GetCourse(courseID);

                DropDownList courseGradeDropDownList = (DropDownList)courseItem.FindControl("GradeDropDownList");
                int grade = int.Parse(courseGradeDropDownList.SelectedValue);

                if (grade > 0)
                {
                    course.Grade = grade;
                    student.PriorCourses.Add(course);
                }
            }

            //Set Prefered Roles
            foreach (RepeaterItem roleItem in RolesRepeater.Items)
            {
                HiddenField roleIDHiddenField = (HiddenField)roleItem.FindControl("RoleIDHiddenField");
                int roleID = int.Parse(roleIDHiddenField.Value);
                Role role = GrouperMethods.GetRole(roleID);

                DropDownList roleInterestDropDownList = (DropDownList)roleItem.FindControl("InterestDropDownList");
                int interestLevel = int.Parse(roleInterestDropDownList.SelectedValue);

                if (interestLevel > 0)
                {
                    role.InterestLevel = interestLevel;
                    student.InterestedRoles.Add(role);
                }
            }

            //Set languages
            foreach (RepeaterItem languageItem in LanguagesRepeater.Items)
            {
                HiddenField languageHiddenField = (HiddenField)languageItem.FindControl("LanguageIDHiddenField");
                int languageID = int.Parse(languageHiddenField.Value);
                ProgrammingLanguage language = GrouperMethods.GetLanguage(languageID);

                DropDownList languageDropDownList = (DropDownList)languageItem.FindControl("LanguageDropDownList");
                int languageProficiency = int.Parse(languageDropDownList.SelectedValue);

                if (languageProficiency > 0)
                {
                    language.ProficiencyLevel = languageProficiency;
                    student.Languages.Add(language);
                }
            }

            //Set skills
            foreach (RepeaterItem skillItem in SkillsRepeater.Items)
            {
                HiddenField skillHiddenField = (HiddenField)skillItem.FindControl("SkillIDHiddenField");
                int skillID = int.Parse(skillHiddenField.Value);
                Skill skill = GrouperMethods.GetSkill(skillID);

                DropDownList skillDropDownList = (DropDownList)skillItem.FindControl("SkillDropDownList");
                int skillProficiency = int.Parse(skillDropDownList.SelectedValue);

                //Check for Outgoing Level
                if (skill.Name == "OutgoingLevel")
                {
                    student.OutgoingLevel = skill.ProficiencyLevel;
                }
                else
                {
                    skill.ProficiencyLevel = skillProficiency;
                    student.Skills.Add(skill);
                }
            }

            GrouperMethods.UpdateStudent(student);
            Response.Redirect("ThankYou.aspx");
        }
    }
}