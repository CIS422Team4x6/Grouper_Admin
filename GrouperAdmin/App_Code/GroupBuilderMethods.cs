using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace GroupBuilder 
{

    #region Classes

    [Serializable]
    public class Course
    {
        public int CourseID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string FullName
        {
            get
            {
                return Code + " - " + Name;
            }
            set
            {

            }
        }
        public bool CoreCourseFlag { get; set; }
        public double? Grade { get; set; }
        public string LetterGrade
        {
            get
            {
                if (Grade != null)
                {
                    switch (Grade.ToString())
                    {
                        case "4.3":
                            return "A+";
                        case "4":
                            return "A";
                        case "3.7":
                            return "A-";
                        case "3.3":
                            return "B+";
                        case "3":
                            return "B";
                        case "2.7":
                            return "B-";
                        case "2.3":
                            return "C+";
                        case "2":
                            return "C";
                        case "1.7":
                            return "C-";
                        case "1.3":
                            return "D+";
                        case "1":
                            return "D";
                        default:
                            return "N/A";
                    }
                }
                return "N/A";
            }
        }
    }

    public class Instructor
    {
        public int InstructorID { get; set; }
        public string DuckID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityUserID { get; set; }
        public List<InstructorCourse> Courses { get; set; }

        public Instructor()
        {
            Courses = new List<InstructorCourse>();
        }
    }

    public class InstructorCourse
    {
        public int InstructorCourseID { get; set; }
        public int InstructorID { get; set; }
        public int CourseID { get; set; }
        public Course Course { get; set; }
        public int TermNumber { get; set; }
        public string TermName { get; set; }
        public int Year { get; set; }
        public bool? CurrentCourseSectionFlag { get; set; }
        public string DaysOfWeek { get; set; }
        public int? CRN { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<Group> Groups { get; set; }
        public List<Student> Students { get; set; }
        public InstructorCourse()
        {
            Groups = new List<Group>();
            Students = new List<Student>();
        }
    }

    [Serializable]
    public class Skill
    {
        public int SkillID { get; set; }
        public string Name { get; set; }
        public int? ProficiencyLevel { get; set; }
    }

    [Serializable]
    public class Role
    {
        public int RoleID { get; set; }
        public string Name { get; set; }
        public string Icon { get
            {
                string iconText = "";
                if(InterestLevel == null)
                {
                    switch (Name)
                    {
                        case "Programmer":
                            iconText += "<span class='fas fa-code fa-lg' title='Programmer'></span>";
                            break;
                        case "Quality Assurance (Testing)":
                            iconText += "<span class='fas fa-bug fa-lg' title='QA Tester'></span>";
                            break;
                        case "Technical Writer":
                            iconText += "<span class='far fa-edit fa-lg' title='Writer'></span>";
                            break;
                        case "Manager":
                            iconText += "<span class='fas fa-chess-queen fa-lg' title='Manager'></span>";
                            break;
                        case "UI Designer":
                            iconText += "<span class='far fa-object-group fa-lg' title='UI Design'></span>";
                            break;
                        case "Requirements Analyst":
                            iconText += "<span class='far fa-chart-bar fa-lg' title='Requirements Analyst'></span>";
                            break;
                    }
                }
                else
                {
                    if(InterestLevel > 3)
                    {
                        switch (Name)
                        {
                            case "Programmer":
                                iconText += "<span class='fas fa-code fa-lg expert' title='Programmer " + InterestLevel.ToString() + "'></span>";
                                break;
                            case "Quality Assurance (Testing)":
                                iconText += "<span class='fas fa-bug fa-lg expert' title='QA Tester " + InterestLevel.ToString() + "'></span>";
                                break;
                            case "Technical Writer":
                                iconText += "<span class='far fa-edit fa-lg expert' title='Writer " + InterestLevel.ToString() + "'></span>";
                                break;
                            case "Manager":
                                iconText += "<span class='fas fa-chess-queen fa-lg expert' title='Manager " + InterestLevel.ToString() + "'></span>";
                                break;
                            case "UI Designer":
                                iconText += "<span class='far fa-object-group fa-lg expert' title='UI Design " + InterestLevel.ToString() + "'></span>";
                                break;
                            case "Requirements Analyst":
                                iconText += "<span class='far fa-chart-bar fa-lg expert' title='Requirements Analyst " + InterestLevel.ToString() + "'></span>";
                                break;
                        }
                    }
                    else
                    {
                        switch (Name)
                        {
                            case "Programmer":
                                iconText += "<span class='fas fa-code fa-lg novice' title='Programmer " + InterestLevel.ToString() + "'></span>";
                                break;
                            case "Quality Assurance (Testing)":
                                iconText += "<span class='fas fa-bug fa-lg novice' title='QA Tester " + InterestLevel.ToString() + "'></span>";
                                break;
                            case "Technical Writer":
                                iconText += "<span class='far fa-edit fa-lg novice' title='Writer " + InterestLevel.ToString() + "'></span>";
                                break;
                            case "Manager":
                                iconText += "<span class='fas fa-chess-queen fa-lg novice' title='Manager " + InterestLevel.ToString() + "'></span>";
                                break;
                            case "UI Designer":
                                iconText += "<span class='far fa-object-group fa-lg novice' title='UI Design " + InterestLevel.ToString() + "'></span>";
                                break;
                            case "Requirements Analyst":
                                iconText += "<span class='far fa-chart-bar fa-lg novice' title='Requirements Analyst " + InterestLevel.ToString() + "'></span>";
                                break;
                        }
                    }
                }
                return iconText;
            } }
        public int? InterestLevel { get; set; }
    }

    [Serializable]
    public class ProgrammingLanguage
    {
        public int LanguageID { get; set; }
        public string Name { get; set; }
        public string Icon { get
        {
            string iconText = "";
                if (ProficiencyLevel == null)
                {
                    switch (Name)
                    {
                        case "Java":
                            iconText = "<span class='fas fa-coffee fa-lg' title='Java'></span>";
                            break;
                        case "Python":
                            iconText = "<span class='fab fa-python fa-lg' title='Python'></span>";
                            break;
                        case "Android":
                            iconText = "<span class='fab fa-android fa-lg' title='Android'></span>";
                            break;
                        case "Web Programming (PHP)":
                            iconText = "<span class='fab fa-php fa-lg' title='Web Programming'></span>";
                            break;
                        case "Web Design (HTML/XML)":
                            iconText = "<span class='fab fa-html5 fa-lg' title='Web Design'></span>";
                            break;
                        case "C++":
                            iconText = "<span style='font-size: medium; font-family: Courier; font-weight: bold; margin-left: 3px; margin-right: 3px;' title='C++'>C++</span>";
                            break;
                        case "C":
                            iconText = "<span style='font-size: medium; font-family: Courier; font-weight: bold; margin-left: 3px; margin-right: 3px;' title='C'>C</span>";
                            break;
                    }
                }
                else
                {
                    if(ProficiencyLevel > 3)
                    {
                        switch (Name)
                        {
                            case "Java":
                                iconText = "<span class='fas fa-coffee fa-lg expert' title='Java'></span>";
                                break;
                            case "Python":
                                iconText = "<span class='fab fa-python fa-lg expert' title='Python'></span>";
                                break;
                            case "Android":
                                iconText = "<span class='fab fa-android fa-lg expert' title='Android'></span>";
                                break;
                            case "Web Programming (PHP)":
                                iconText = "<span class='fab fa-php fa-lg expert' title='Web Programming'></span>";
                                break;
                            case "Web Design (HTML/XML)":
                                iconText = "<span class='fab fa-html5 fa-lg expert' title='Web Design'></span>";
                                break;
                            case "C++":
                                iconText = "<span style='font-size: medium; font-family: Courier; font-weight: bold; margin-left: 3px; margin-right: 3px;' class='expert' title='C++'>C++</span>";
                                break;
                            case "C":
                                iconText = "<span style='font-size: medium; font-family: Courier; font-weight: bold; margin-left: 3px; margin-right: 3px;' class='expert' title='C'>C</span>";
                                break;
                        }
                    }
                    else
                    {
                        switch (Name)
                        {
                            case "Java":
                                iconText = "<span class='fas fa-coffee fa-lg novice' title='Java'></span>";
                                break;
                            case "Python":
                                iconText = "<span class='fab fa-python fa-lg novice' title='Python'></span>";
                                break;
                            case "Android":
                                iconText = "<span class='fab fa-android fa-lg novice' title='Android'></span>";
                                break;
                            case "Web Programming (PHP)":
                                iconText = "<span class='fab fa-php fa-lg novice' title='Web Programming'></span>";
                                break;
                            case "Web Design (HTML/XML)":
                                iconText = "<span class='fab fa-html5 fa-lg novice' title='Web Design'></span>";
                                break;
                            case "C++":
                                iconText = "<span style='font-size: medium; font-family: Courier; font-weight: bold; margin-left: 3px; margin-right: 3px;' class='novice' title='C++'>C++</span>";
                                break;
                            case "C":
                                iconText = "<span style='font-size: medium; font-family: Courier; font-weight: bold; margin-left: 3px; margin-right: 3px;' class='novice' title='C'>C</span>";
                                break;
                        }
                    }
                }
            return iconText;
            }
        }
        public int? ProficiencyLevel { get; set; }
    }

    public class Student
    {
        public int StudentID { get; set; }
        public int InstructorCourseID { get; set; }
        public string DuckID { get; set; }
        public int? UOID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PreferredName { get; set; }
        public int? OutgoingLevel { get; set;}
        public string DevelopmentExperience { get; set; }
        public string LearningExpectations { get; set; }
        public string ContributingRole { get; set; }
        public bool? EnglishSecondLanguageFlag { get; set; }
        public string NativeLanguage { get; set; }
        public string OtherProgrammingLanguage { get; set; }
        public int? OtherProgrammingLanguageProficiency { get; set; }
        public string ProgrammingLanguagesDescription { get
        {
                string languages = "";
                foreach (ProgrammingLanguage language in Languages.OrderByDescending(x => x.ProficiencyLevel))
                {
                    if (language.ProficiencyLevel > 3)
                    {
                        
                        switch (language.Name)
                        {
                            case "Java":
                                languages += "<span class='fas fa-coffee fa-lg expert' title='Java " + language.ProficiencyLevel.ToString() + "'></span>";
                                break;
                            case "Python":
                                languages += "<span class='fab fa-python fa-lg expert' title='Python " + language.ProficiencyLevel.ToString() + "'></span>";
                                break;
                            case "Android":
                                languages += "<span class='fab fa-android fa-lg expert' title='Android " + language.ProficiencyLevel.ToString() + "'></span>";
                                break;
                            case "Web Programming (PHP)":
                                languages += "<span class='fab fa-php fa-lg expert' title='Web Programming " + language.ProficiencyLevel.ToString() + "'></span>";
                                break;
                            case "Web Design (HTML/XML)":
                                languages += "<span class='fab fa-html5 fa-lg expert' title='Web Design " + language.ProficiencyLevel.ToString() + "'></span>";
                                break;
                            case "C++":
                                languages += "<span style='font-size: medium; font-family: Courier; font-weight: bold; margin-left: 3px; margin-right: 3px;' class='expert' title='" + language.ProficiencyLevel.ToString() + "'>C++</span>";
                                break;
                            case "C":
                                languages += "<span style='font-size: medium; font-family: Courier; font-weight: bold; margin-left: 3px; margin-right: 3px;' class='expert' title='" + language.ProficiencyLevel.ToString() + "'>C</span>";
                                break;
                        }
                    }
                    else
                    {
                        switch (language.Name)
                        {
                            case "Java":
                                languages += "<span class='fas fa-coffee fa-lg novice' title='Java " + language.ProficiencyLevel.ToString() + "'></span>";
                                break;
                            case "Python":
                                languages += "<span class='fab fa-python fa-lg novice' title='Python " + language.ProficiencyLevel.ToString() + "'></span>";
                                break;
                            case "Android":
                                languages += "<span class='fab fa-android fa-lg novice' title='Android " + language.ProficiencyLevel.ToString() + "'></span>";
                                break;
                            case "Web Programming (PHP)":
                                languages += "<span class='fab fa-php fa-lg novice' title='Web Programming " + language.ProficiencyLevel.ToString() + "'></span>";
                                break;
                            case "Web Design (HTML/XML)":
                                languages += "<span class='fab fa-html5 fa-lg novice' title='Web Design " + language.ProficiencyLevel.ToString() + "'></span>";
                                break;
                            case "C++":
                                languages += "<span style='font-size: medium; font-family: Courier; font-weight: bold; margin-left: 3px; margin-right: 3px;' class='novice' title='" + language.ProficiencyLevel.ToString() + "'>C++</span>";
                                break;
                            case "C":
                                languages += "<span style='font-size: medium; font-family: Courier; font-weight: bold; margin-left: 3px; margin-right: 3px;' class='novice title='" + language.ProficiencyLevel.ToString() + "'>C</span>";
                                break;
                        }
                    }
                }
                return languages;
            }
        }

        public string RolesDescription
        {
            get
            {
                string roles = "";
                foreach (Role role in InterestedRoles.OrderByDescending(x => x.InterestLevel))
                {
                    if (role.InterestLevel > 3)
                    {
                        switch (role.Name)
                        {
                            case "Programmer":
                                roles += "<span class='fas fa-code fa-lg expert' title='Programmer " + role.InterestLevel.ToString() + "'></span>";
                                break;
                            case "Quality Assurance (Testing)":
                                roles += "<span class='fas fa-bug fa-lg expert' title='QA Tester" + role.InterestLevel.ToString() + "'></span>";
                                break;
                            case "Technical Writer":
                                roles += "<span class='far fa-edit fa-lg expert' title='Writer " + role.InterestLevel.ToString() + "'></span>";
                                break;
                            case "Manager":
                                roles += "<span class='fas fa-chess-queen fa-lg expert' title='Manager " + role.InterestLevel.ToString() + "'></span>";
                                break;
                            case "UI Designer":
                                roles += "<span class='far fa-object-group fa-lg expert' title='UI Design " + role.InterestLevel.ToString() + "'></span>";
                                break;
                            case "Requirements Analyst":
                                roles += "<span class='far fa-chart-bar fa-lg expert' title='Requirements Analyst " + role.InterestLevel.ToString() + "'></span>";
                                break;
                        }
                    }
                    else
                    {
                        switch (role.Name)
                        {
                            case "Programmer":
                                roles += "<span class='fas fa-code fa-lg novice' title='Programmer " + role.InterestLevel.ToString() + "'></span>";
                                break;
                            case "Quality Assurance (Testing)":
                                roles += "<span class='fas fa-bug fa-lg novice' title='QA Tester" + role.InterestLevel.ToString() + "'></span>";
                                break;
                            case "Technical Writer":
                                roles += "<span class='far fa-edit fa-lg novice' title='Writer " + role.InterestLevel.ToString() + "'></span>";
                                break;
                            case "Manager":
                                roles += "<span class='fas fa-chess-queen fa-lg novice' title='Manager " + role.InterestLevel.ToString() + "'></span>";
                                break;
                            case "UI Designer":
                                roles += "<span class='far fa-object-group fa-lg novice' title='UI Design " + role.InterestLevel.ToString() + "'></span>";
                                break;
                            case "Requirements Analyst":
                                roles += "<span class='far fa-chart-bar fa-lg novice' title='Requirements Analyst " + role.InterestLevel.ToString() + "'></span>";
                                break;
                        }
                    }
                }
                return roles;
            }
        }
        public string Description
        {
            
            get
            {
                string description = "";
                if (PriorCourses.Count > 0)
                {
                    description += "Prior Courses:\n";
                    foreach(Course course in PriorCourses)
                    {
                        description += "\t" + course.Code + " - " + course.LetterGrade + "\n";
                    }
                    description += "\n";
                }

                if(CurrentCourses.Count > 0)
                {
                    description += "Current Courses:\n";
                    foreach(Course course in CurrentCourses)
                    {
                        description += "\t" + course.Code + " ";
                    }
                    description += "\n\n";
                }

                if(InterestedRoles.Count > 0)
                {
                    description += "Interested Roles:\n";
                    foreach(Role role in InterestedRoles)
                    {
                        description += "\t" + role.Name + " - " + role.InterestLevel;
                    }
                    description += "\n\n";
                }

                if (Skills.Count > 0)
                {
                    description += "Skills:\n";
                    foreach (Skill skill in Skills)
                    {
                        description += "\t" + skill.Name + " - " + skill.ProficiencyLevel;
                    }
                    description += "\n\n";
                }

                return description;
            }
        }
        public DateTime? InitialNotificationSentDate { get; set; }
        public DateTime? SurveySubmittedDate { get; set; }
        public string GUID { get; set; }
        public List<Role> InterestedRoles { get; set; }
        public List<Skill> Skills { get; set; }
        public List<ProgrammingLanguage> Languages { get; set; }
        public List<Course> PriorCourses { get; set; }
        public List<Course> CurrentCourses { get; set; }
        public string GPA { get
            {
                List<Course> priorCourses = PriorCourses.Where(x => x.Grade != null).ToList();
                string formattedGPA = "";

                if (priorCourses.Count > 0)
                {
                    double gpa = (double)priorCourses.Average(x => x.Grade);

                    double roundedGpa = Math.Truncate(gpa * 100) / 100;

                    formattedGPA = string.Format("{0:N2}", roundedGpa);
                }
                    return formattedGPA;
            } }
        public Student()
        {
            Guid g;
            g = Guid.NewGuid();
            GUID = g.ToString();
            InterestedRoles = new List<Role>();
            Skills = new List<Skill>();
            Languages = new List<ProgrammingLanguage>();
            PriorCourses = new List<Course>();
            CurrentCourses = new List<Course>();
        }
    }

    public class Group
    {
        public int GroupID { get; set; }
        public int InstructorCourseID { get; set; }
        public int GroupNumber { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public List<Student> Members { get; set; }
    }

    #endregion

    public class GrouperMethods
    {
        private static System.Configuration.ConnectionStringSettings GrouperConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"];

        #region Helper Methods

        public static string GetSafeString(SqlDataReader reader, string key)
        {
            string returnValue = "";

            returnValue = reader[key].ToString();

            return returnValue;
        }

        public static int GetSafeInteger(SqlDataReader reader, string key)
        {
            int returnValue = 0;
            string rawValue = GetSafeString(reader, key);

            Int32.TryParse(rawValue, out returnValue);
            return returnValue;
        }

        public static double GetSafeDouble(SqlDataReader reader, string key)
        {
            double returnValue = 0.0;
            string rawValue = GetSafeString(reader, key);

            double.TryParse(rawValue, out returnValue);
            return returnValue;
        }

        public static bool GetSafeBoolean(SqlDataReader reader, string key)
        {
            bool returnValue = false;
            string rawValue = GetSafeString(reader, key);

            Boolean.TryParse(rawValue, out returnValue);
            return returnValue;
        }

        public static DateTime? GetSafeDateTime(SqlDataReader reader, string key)
        {
            DateTime returnValue;
            string rawValue = GetSafeString(reader, key);

            if (DateTime.TryParse(rawValue, out returnValue))
            {
                return returnValue;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Instructors

        public static int InsertInstructor(Instructor instructor)
        {
            int instructorID = 0;

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
            @"INSERT INTO Instructors    
                (DuckID, LastName, FirstName, IdentityUserID) 
                VALUES 
                (@DuckID, @LastName, @FirstName, @IdentityUserID);
                SELECT SCOPE_IDENTITY();", con);
            cmd.Parameters.AddWithValue("@DuckID", instructor.DuckID ??(Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@LastName", instructor.LastName ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FirstName", instructor.FirstName ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@IdentityUserID", instructor.IdentityUserID);
            con.Open();
            instructorID = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return instructorID;
        }

        public static List<Instructor> GetInstructors()
        {
            List<Instructor> instructors = new List<Instructor>();
            List<int> instructorIDs = new List<int>();

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Instructors ORDER BY LastName, FirstName;", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Instructor instructor = new Instructor();

                int instructorID = GetSafeInteger(reader, "InstructorID");

                instructorIDs.Add(instructorID);
            }
            con.Close();

            foreach(int id in instructorIDs)
            {
                Instructor instructor = GetInstructor(id);
                if (instructor != null)
                {
                    instructors.Add(instructor);
                }
            }

            return instructors;
        }

        public static Instructor GetInstructor(int instructorID)
        {
            Instructor instructor = null;

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Instructors WHERE InstructorID = @InstructorID;", con);
            cmd.Parameters.AddWithValue("@InstructorID", instructorID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                instructor = new Instructor();

                instructor.InstructorID = GetSafeInteger(reader, "InstructorID");
                instructor.FirstName = GetSafeString(reader, "FirstName");
                instructor.LastName = GetSafeString(reader, "LastName");
                instructor.DuckID = GetSafeString(reader, "DuckID");
                instructor.IdentityUserID = GetSafeString(reader, "IdentityUserID");
            }
            con.Close();

            if (instructor != null)
            {
                instructor.Courses = GetInstructorCourses(instructor.InstructorID);
            }
            return instructor;
        }

        public static Instructor GetInstructor(string userName)
        {
            Instructor instructor = null;
            int instructorID = 0;

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Instructors WHERE IdentityUserID = @IdentityUserID;", con);
            cmd.Parameters.AddWithValue("@IdentityUserID", userName);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                instructorID = GetSafeInteger(reader, "InstructorID");
            }
            con.Close();

            if (instructorID > 0)
            {
                instructor = GetInstructor(instructorID);
            }
            return instructor;
        }

        public static void UpdateInstructor(Instructor instructor)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
                @"UPDATE Instructors 
                    SET DuckID = @DuckID,
                        FirstName = @FirstName,
                        LastName = @LastName, 
                        IdentityUserID = @IdentityUserID 
                    WHERE InstructorID = @InstructorID;", con);
            cmd.Parameters.AddWithValue("@InstructorID", instructor.InstructorID);
            cmd.Parameters.AddWithValue("@DuckID", instructor.DuckID ??(Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FirstName", instructor.FirstName ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@LastName", instructor.LastName ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@IdentityUserID", instructor.IdentityUserID ?? (Object)DBNull.Value);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void DeleteInstructor(int instructorID)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
                @"DELETE FROM Instructors WHERE InstructorID = @InstructorID;", con);
            cmd.Parameters.AddWithValue("@InstructorID", instructorID);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        #endregion

        #region Programming Languages 

        public static int InsertLanguage(ProgrammingLanguage language)
        {
            int languageID = 0;

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(
                @"SELECT *  
                FROM ProgrammingLanguages     
                WHERE Name = @Name;", con);
            cmd.Parameters.AddWithValue("@Name", language.Name);

            languageID = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            if (languageID == 0)
            {
                cmd = new SqlCommand(
                @"INSERT INTO ProgrammingLanguages    
                    (Name) 
                    VALUES 
                    (@Name);
                    SELECT SCOPE_IDENTITY();", con);
                cmd.Parameters.AddWithValue("@Name", language.Name);
                con.Open();
                languageID = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            return languageID;
        }

        public static void UpdateLanguage(ProgrammingLanguage language)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
                @"UPDATE ProgrammingLanguages  
                    SET Name = @Name
                WHERE LanguageID = @LanguageID;", con);
            cmd.Parameters.AddWithValue("@LanguageID", language.LanguageID);
            cmd.Parameters.AddWithValue("@Name", language.Name);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void DeleteLanguage(int languageID)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
                @"DELETE FROM Students_ProgrammingLanguages WHERE LanguageID = @LanguageID;
                  DELETE FROM ProgrammingLanguages WHERE LanguageID = @LanguageID;", con);
            cmd.Parameters.AddWithValue("@LanguageID", languageID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static List<ProgrammingLanguage> GetLanguages()
        {
            List<ProgrammingLanguage> languages = new List<ProgrammingLanguage>();
            List<int> languageIDs = new List<int>();

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM ProgrammingLanguages ORDER BY Name;", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ProgrammingLanguage language = new ProgrammingLanguage();

                int languageID = GetSafeInteger(reader, "LanguageID");

                languageIDs.Add(languageID);
            }
            con.Close();

            foreach (int id in languageIDs)
            {
                ProgrammingLanguage language = GetLanguage(id);
                if (languages != null)
                {
                    languages.Add(language);
                }
            }

            return languages;
        }

        public static ProgrammingLanguage GetLanguage(int languageID)
        {
            ProgrammingLanguage language = null;

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM ProgrammingLanguages WHERE LanguageID = @LanguageID;", con);
            cmd.Parameters.AddWithValue("@LanguageID", languageID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                language = new ProgrammingLanguage();
                language.LanguageID = GetSafeInteger(reader, "LanguageID");
                language.Name = GetSafeString(reader, "Name");
            }
            con.Close();

            return language;
        }

        public static List<ProgrammingLanguage> GetStudentLanguages(int studentID)
        {
            List<ProgrammingLanguage> languages = new List<ProgrammingLanguage>();

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT pl.*, spl.ProficiencyRank FROM Students_ProgrammingLanguages spl JOIN ProgrammingLanguages pl ON spl.LanguageID = pl.LanguageID WHERE spl.StudentID = @StudentID ORDER BY pl.Name;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ProgrammingLanguage language = new ProgrammingLanguage();

                language.LanguageID = GetSafeInteger(reader, "LanguageID");
                language.Name = GetSafeString(reader, "Name");
                language.ProficiencyLevel = GetSafeInteger(reader, "ProficiencyRank");

                languages.Add(language);
            }
            con.Close();

            return languages;
        }

        public static void DeleteStudentLanguages(int studentID)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"DELETE FROM Students_ProgrammingLanguages WHERE StudentID = @StudentID;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void InsertStudentLanguage(int studentID, int languageID, int proficiencyLevel)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(
                @"SELECT *  FROM Students_ProgrammingLanguages  
                WHERE StudentID = @StudentID 
                AND LanguageID = @LanguageID;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            cmd.Parameters.AddWithValue("@LanguageID", languageID);

            var exists = cmd.ExecuteScalar();
            con.Close();
            if (exists == null)
            {
                cmd = new SqlCommand(
                @"INSERT INTO Students_ProgrammingLanguages 
                    (StudentID, LanguageID, ProficiencyRank) 
                    VALUES 
                    (@StudentID, @LanguageID, @ProficiencyRank);
                    SELECT SCOPE_IDENTITY();", con);
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@LanguageID", languageID);
                cmd.Parameters.AddWithValue("@ProficiencyRank", proficiencyLevel);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        #endregion

        #region Roles 

        public static int InsertRole(Role role)
        {
            int roleID = 0;

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(
                @"SELECT *  
                FROM Roles     
                WHERE Name = @Name;", con);
            cmd.Parameters.AddWithValue("@Name", role.Name);

            roleID = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            if (roleID == 0)
            {
                cmd = new SqlCommand(
                @"INSERT INTO Roles    
                    (Name) 
                    VALUES 
                    (@Name);
                    SELECT SCOPE_IDENTITY();", con);
                cmd.Parameters.AddWithValue("@Name", role.Name);
                con.Open();
                roleID = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            return roleID;
        }

        public static void UpdateRole(Role role)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
                @"UPDATE Roles  
                    SET Name = @Name
                WHERE RoleID = @RoleID;", con);
            cmd.Parameters.AddWithValue("@RoleID", role.RoleID);
            cmd.Parameters.AddWithValue("@Name", role.Name);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void DeleteRole(int roleID)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
                @"DELETE FROM Students_RoleInterests WHERE RoleID = @RoleID;
                  DELETE FROM Roles WHERE RoleID = @RoleID;", con);
            cmd.Parameters.AddWithValue("@RoleID", roleID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public static List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>();
            List<int> roleIDs = new List<int>();

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Roles ORDER BY Name;", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Role role = new Role();

                int roleID = GetSafeInteger(reader, "RoleID");

                roleIDs.Add(roleID);
            }
            con.Close();

            foreach (int id in roleIDs)
            {
                Role role = GetRole(id);
                if (roles != null)
                {
                    roles.Add(role);
                }
            }
            return roles;
        }

        public static Role GetRole(int roleID)
        {
            Role role = null;

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Roles WHERE RoleID = @RoleID;", con);
            cmd.Parameters.AddWithValue("@RoleID", roleID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                role = new Role();
                role.RoleID = GetSafeInteger(reader, "RoleID");
                role.Name = GetSafeString(reader, "Name");
            }
            con.Close();

            return role;
        }

        public static List<Role> GetStudentRoleInterests(int studentID)
        {
            List<Role> roles = new List<Role>();

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT r.RoleID, r.Name, sr.InterestLevel FROM Roles r JOIN Students_RoleInterests sr ON r.RoleID = sr.RoleID WHERE sr.StudentID = @StudentID;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Role role = new Role();
                role.RoleID = GetSafeInteger(reader, "RoleID");
                role.Name = GetSafeString(reader, "Name");
                role.InterestLevel = GetSafeInteger(reader, "InterestLevel");
                roles.Add(role);
            }
            con.Close();

            return roles;
        }

        public static void InsertStudentRoleInterest(int studentID, int roleID, int interestLevel)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(
                @"SELECT *  FROM Students_RoleInterests   
                WHERE StudentID = @StudentID 
                AND RoleID = @RoleID;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            cmd.Parameters.AddWithValue("@RoleID", roleID);

            var exists = cmd.ExecuteScalar();
            con.Close();
            if (exists == null)
            {
                cmd = new SqlCommand(
                @"INSERT INTO Students_RoleInterests 
                    (StudentID, RoleID, InterestLevel) 
                    VALUES 
                    (@StudentID, @RoleID, @InterestLevel);
                    SELECT SCOPE_IDENTITY();", con);
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@RoleID", roleID);
                cmd.Parameters.AddWithValue("@InterestLevel", interestLevel);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public static void DeleteStudentRoleInterest(int studentID, int roleID)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
                @"DELETE FROM Students_RoleInterests WHERE StudentID = @StudentID AND RoleID = @RoleID;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            cmd.Parameters.AddWithValue("@RoleID", roleID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void DeleteStudentRoleInterests(int studentID)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"DELETE FROM Students_RoleInterests WHERE StudentID = @StudentID;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        #endregion

        #region Skills 

        public static int InsertSkill(Skill skill)
        {
            int skillID = 0;

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(
                @"SELECT *  
                FROM Skills      
                WHERE Name = @Name;", con);
            cmd.Parameters.AddWithValue("@Name", skill.Name);

            skillID = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            if (skillID == 0)
            {
                cmd = new SqlCommand(
                @"INSERT INTO Skills     
                    (Name) 
                    VALUES 
                    (@Name);
                    SELECT SCOPE_IDENTITY();", con);
                cmd.Parameters.AddWithValue("@Name", skill.Name);
                con.Open();
                skillID = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            return skillID;
        }

        public static void UpdateSkill(Skill skill)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
                @"UPDATE Skills  
                    SET Name = @Name
                WHERE SkillID = @SkillID;", con);
            cmd.Parameters.AddWithValue("@SkillID", skill.SkillID);
            cmd.Parameters.AddWithValue("@Name", skill.Name);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void DeleteSkill(int skillID)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
                @"DELETE FROM Students_Skills WHERE SkillID = @SkillID;
                  DELETE FROM Skills WHERE SkillID = @SkillID;", con);
            cmd.Parameters.AddWithValue("@SkillID", skillID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static List<Skill> GetSkills()
        {
            List<Skill> skills = new List<Skill>();
            List<int> skillIDs = new List<int>();

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Skills ORDER BY Name;", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Skill skill = new Skill();
                int skillID = GetSafeInteger(reader, "SkillID");
                skillIDs.Add(skillID);
            }
            con.Close();

            foreach (int id in skillIDs)
            {
                Skill skill = GetSkill(id);
                if (skills != null)
                {
                    skills.Add(skill);
                }
            }

            return skills;
        }

        public static Skill GetSkill(int skillID)
        {
            Skill skill = null;

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Skills WHERE SkillID = @SkillID;", con);
            cmd.Parameters.AddWithValue("@SkillID", skillID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                skill = new Skill();
                skill.SkillID = GetSafeInteger(reader, "SkillID");
                skill.Name = GetSafeString(reader, "Name");
            }
            con.Close();

            return skill;
        }

        public static List<Skill> GetStudentSkillInterests(int studentID)
        {
            List<Skill> skills = new List<Skill>();

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT s.SkillID, s.Name, ss.ProficiencyLevel FROM Skills s JOIN Students_Skills ss ON s.SkillID = ss.SkillID WHERE ss.StudentID = @StudentID;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Skill skill = new Skill();
                skill.SkillID = GetSafeInteger(reader, "SkillID");
                skill.Name = GetSafeString(reader, "Name");
                skill.ProficiencyLevel = GetSafeInteger(reader, "ProficiencyLevel");
                skills.Add(skill);
            }
            con.Close();

            return skills;
        }

        public static void InsertStudentSkill(int studentID, int skillID, int proficiencyLevel)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(
                @"SELECT *  FROM Students_Skills    
                WHERE StudentID = @StudentID 
                AND SkillID = @SkillID;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            cmd.Parameters.AddWithValue("@SkillID", skillID);

            var exists = cmd.ExecuteScalar();
            con.Close();
            if (exists == null)
            {
                cmd = new SqlCommand(
                @"INSERT INTO Students_Skills 
                    (StudentID, SkillID, ProficiencyLevel) 
                    VALUES 
                    (@StudentID, @SkillID, @ProficiencyLevel);
                    SELECT SCOPE_IDENTITY();", con);
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@SkillID", skillID);
                cmd.Parameters.AddWithValue("@ProficiencyLevel", proficiencyLevel);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public static void DeleteStudentSkillInterest(int studentID, int skillID)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
                @"DELETE FROM Students_Skills WHERE StudentID = @StudentID AND SkillID = @SkillID;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            cmd.Parameters.AddWithValue("@SkillID", skillID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void DeleteStudentSkills(int studentID)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"DELETE FROM Students_Skills WHERE StudentID = @StudentID;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        #endregion

        #region Courses

        public static int InsertCourse(Course course)
        {
            int courseID = 0;

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(
                @"SELECT *  
                FROM Courses    
                WHERE Code = @Code 
                AND Name = @Name;", con);
            cmd.Parameters.AddWithValue("@Code", course.Code);
            cmd.Parameters.AddWithValue("@Name", course.Name);

            courseID = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            if (courseID == 0)
            {
                cmd = new SqlCommand(
                @"INSERT INTO Courses    
                    (Code, Name, CoreCourseFlag) 
                    VALUES 
                    (@Code, @Name, @CoreCourseFlag);
                    SELECT SCOPE_IDENTITY();", con);
                cmd.Parameters.AddWithValue("@Code", course.Code);
                cmd.Parameters.AddWithValue("@Name", course.Name);
                cmd.Parameters.AddWithValue("@CoreCourseFlag", course.CoreCourseFlag);
                con.Open();
                courseID = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            return courseID;
        }

        public static List<Course> GetCourses()
        {
            List<Course> courses = new List<Course>();
            List<int> courseIDs = new List<int>();

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Courses ORDER BY Code, Name;", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Course course = new Course();

                int courseID = GetSafeInteger(reader, "CourseID");
                courseIDs.Add(courseID);
            }
            con.Close();

            foreach (int id in courseIDs)
            {
                Course course = GetCourse(id);
                if (course != null)
                {
                    courses.Add(course);
                }
            }

            return courses;
        }

        public static Course GetCourse(int courseID)
        {
            Course course = null;

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Courses WHERE CourseID = @CourseID;", con);
            cmd.Parameters.AddWithValue("@CourseID", courseID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                course = new Course();
                course.CourseID = courseID;
                course.Code = GetSafeString(reader, "Code");
                course.Name = GetSafeString(reader, "Name");
                course.CoreCourseFlag = GetSafeBoolean(reader, "CoreCourseFlag");
            }
            con.Close();

            return course;
        }

        public static void UpdateCourse(Course course)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
                @"UPDATE Courses 
                    SET Code = @Code,
                        Name = @Name,
                        CoreCourseFlag = @CoreCourseFlag 
                    WHERE CourseID = @CourseID;", con);
            cmd.Parameters.AddWithValue("@CourseID", course.CourseID);
            cmd.Parameters.AddWithValue("@Code", course.Code);
            cmd.Parameters.AddWithValue("@Name", course.Name);
            cmd.Parameters.AddWithValue("@CoreCourseFlag", course.CoreCourseFlag);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void DeleteCourse(int courseID)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
                @"DELETE FROM Courses WHERE CourseID = @CourseID;", con);
            cmd.Parameters.AddWithValue("@CourseID", courseID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        #endregion

        #region Prior Courses

        public static void InsertStudentPriorCourse(int studentID, int courseID, double? grade)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(
                @"SELECT CourseID  
                FROM Students_CoursesCompleted     
                WHERE CourseID = @CourseID  
                AND StudentID = @StudentID;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            cmd.Parameters.AddWithValue("@CourseID", courseID);

            int existingCourseID = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            if (existingCourseID == 0)
            {
                cmd = new SqlCommand(
                @"INSERT INTO Students_CoursesCompleted     
                    (StudentID, CourseID, Grade) 
                    VALUES 
                    (@StudentID, @CourseID, @Grade);", con);
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@CourseID", courseID);
                cmd.Parameters.AddWithValue("@Grade", grade);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public static List<Course> GetStudentPriorCourses(int studentID)
        {
            List<Course> courses = new List<Course>();

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT c.*, scc.Grade FROM Students_CoursesCompleted scc JOIN Courses c ON scc.CourseID = c.CourseID WHERE scc.StudentID = @StudentID ORDER BY c.Code;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Course course = new Course();

                course.CourseID = GetSafeInteger(reader, "CourseID");
                course.Name = GetSafeString(reader, "Name");
                course.Code = GetSafeString(reader, "Code");
                course.Grade = GetSafeDouble(reader, "Grade");

                courses.Add(course);

            }
            con.Close();

            return courses;
        }

        public static void DeleteStudentPriorCourse(int studentID, int courseID)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"DELETE FROM Students_CoursesCompleted WHERE StudentID = @StudentID AND CourseID = @CourseID;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            cmd.Parameters.AddWithValue("@CourseID", courseID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void DeleteStudentPriorCourses(int studentID)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"DELETE FROM Students_CoursesCompleted WHERE StudentID = @StudentID;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        #endregion

        #region Current Courses

        public static void InsertStudentCurrentCourse(int studentID, int courseID)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(
                @"SELECT CourseID  
                FROM Students_EnrolledCourses      
                WHERE CourseID = @CourseID  
                AND StudentID = @StudentID;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            cmd.Parameters.AddWithValue("@CourseID", courseID);

            int existingCourseID = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            if (existingCourseID == 0)
            {
                cmd = new SqlCommand(
                @"INSERT INTO Students_EnrolledCourses      
                    (StudentID, CourseID) 
                    VALUES 
                    (@StudentID, @CourseID);", con);
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@CourseID", courseID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public static List<Course> GetStudentCurrentCourses(int studentID)
        {
            List<Course> courses = new List<Course>();

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT c.* FROM Students_EnrolledCourses sec JOIN Courses c ON sec.CourseID = c.CourseID WHERE sec.StudentID = @StudentID ORDER BY c.Code;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Course course = new Course();

                course.CourseID = GetSafeInteger(reader, "CourseID");
                course.Name = GetSafeString(reader, "Name");
                course.Code = GetSafeString(reader, "Code");

                courses.Add(course);
            }
            con.Close();

            return courses;
        }

        public static void DeleteStudentCurrentCourse(int studentID, int courseID)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"DELETE FROM Students_EnrolledCourses WHERE StudentID = @StudentID AND CourseID = @CourseID;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            cmd.Parameters.AddWithValue("@CourseID", courseID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void DeleteStudentCurrentCourses(int studentID)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"DELETE FROM Students_EnrolledCourses WHERE StudentID = @StudentID;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        #endregion

        #region Students

        public static int InsertStudent(Student student)
        {
            int studentID = 0;

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(
                @"SELECT *  
                FROM Students     
                WHERE (DuckID = @DuckID OR
                UOID = @UOID) 
                AND InstructorCourseID = @InstructorCourseID;", con);
            cmd.Parameters.AddWithValue("@DuckID", student.DuckID ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@UOID", student.UOID ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@InstructorCourseID", student.InstructorCourseID);
            studentID = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            if (studentID == 0)
            {
                cmd = new SqlCommand(
                @"INSERT INTO Students    
                    (DuckID, InstructorCourseID, LastName, FirstName, PreferredName, UOID, OutgoingLevel, EnglishSecondLanguageFlag, NativeLanguage, DevelopmentExperience, LearningExpectations, GUID, SurveySubmittedDate) 
                    VALUES 
                    (@DuckID, @InstructorCourseID, @LastName, @FirstName, @PreferredName, @UOID, @OutgoingLevel, @EnglishSecondLanguageFlag, @NativeLanguage, @DevelopmentExperience, @LearningExpectations, @GUID, @SurveySubmittedDate);
                    SELECT SCOPE_IDENTITY();", con);
                cmd.Parameters.AddWithValue("@DuckID", student.DuckID ?? (Object)DBNull.Value);
                cmd.Parameters.AddWithValue("@InstructorCourseID", student.InstructorCourseID);
                cmd.Parameters.AddWithValue("@LastName", student.LastName ?? (Object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName ?? (Object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PreferredName", student.PreferredName ?? (Object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UOID", student.UOID ?? (Object)DBNull.Value);
                cmd.Parameters.AddWithValue("@OutgoingLevel", student.OutgoingLevel ?? (Object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EnglishSecondLanguageFlag", student.EnglishSecondLanguageFlag ?? (Object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NativeLanguage", student.NativeLanguage ?? (Object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DevelopmentExperience", student.DevelopmentExperience ?? (Object)DBNull.Value);
                cmd.Parameters.AddWithValue("@LearningExpectations", student.LearningExpectations ?? (Object)DBNull.Value);
                cmd.Parameters.AddWithValue("@GUID", student.GUID ?? (Object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SurveySubmittedDate", student.SurveySubmittedDate ?? (Object)DBNull.Value);
                con.Open();
                studentID = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            else
            {
                studentID = 0;
            }
            return studentID;
        }

        public static Student GetStudent(int studentID)
        {
            Student student = null;

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Students WHERE StudentID = @StudentID;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                student = new Student();

                student.StudentID = studentID;
                student.InstructorCourseID = GetSafeInteger(reader, "InstructorCourseID");
                student.DuckID = GetSafeString(reader, "DuckID");
                student.FirstName = GetSafeString(reader, "FirstName");
                student.LastName = GetSafeString(reader, "LastName");
                student.PreferredName = GetSafeString(reader, "PreferredName");
                student.UOID = GetSafeInteger(reader, "UOID");
                student.EnglishSecondLanguageFlag = GetSafeBoolean(reader, "EnglishSecondLanguageFlag");
                student.NativeLanguage = GetSafeString(reader, "NativeLanguage");
                student.DevelopmentExperience = GetSafeString(reader, "DevelopmentExperience");
                student.LearningExpectations = GetSafeString(reader, "LearningExpectations");
                student.InitialNotificationSentDate = GetSafeDateTime(reader, "InitialNotificationSentDate");
                student.SurveySubmittedDate = GetSafeDateTime(reader, "SurveySubmittedDate");
                student.GUID = GetSafeString(reader, "GUID");
            }
            con.Close();

            if(student != null)
            {
                student.PriorCourses = GetStudentPriorCourses(student.StudentID);
                student.CurrentCourses = GetStudentCurrentCourses(student.StudentID);
                student.Languages = GetStudentLanguages(student.StudentID);
                student.InterestedRoles = GetStudentRoleInterests(student.StudentID);
                student.Skills = GetStudentSkillInterests(student.StudentID);
            }
            return student;
        }

        public static Student GetStudentByGUID(string guid)
        {
            Student student = null;
            int studentID = 0;

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Students WHERE GUID = @GUID;", con);
            cmd.Parameters.AddWithValue("@GUID", guid);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                studentID = GetSafeInteger(reader, "StudentID");
            }
            con.Close();

            if (studentID > 0)
            {
                student = GetStudent(studentID);
            }

            return student;
        }

        public static List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            List<int> studentIDs = new List<int>();

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Students ORDER BY LastName, FirstName;", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int studentID = GetSafeInteger(reader, "StudentID");
                studentIDs.Add(studentID);
            }
            con.Close();

            foreach (int studentID in studentIDs)
            {
                Student student = GrouperMethods.GetStudent(studentID);
                if(student != null)
                {
                    students.Add(student);
                }
            }
            return students;
        }

        public static List<Student> GetStudents(int instructorCourseID)
        {
            List<Student> students = new List<Student>();
            List<int> studentIDs = new List<int>();

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Students WHERE InstructorCourseID = @InstructorCourseID ORDER BY LastName, FirstName;", con);
            cmd.Parameters.AddWithValue("@InstructorCourseID", instructorCourseID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int studentID = GetSafeInteger(reader, "StudentID");
                studentIDs.Add(studentID);
            }
            con.Close();

            foreach (int studentID in studentIDs)
            {
                Student student = GrouperMethods.GetStudent(studentID);
                if (student != null)
                {
                    students.Add(student);
                }
            }
            return students;
        }

        public static List<Student> GetUngroupedStudents(int instructorCourseID)
        {
            List<Student> students = new List<Student>();
            List<int> studentIDs = new List<int>();

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Students WHERE InstructorCourseID = @InstructorCourseID AND StudentID NOT IN 
                                                (SELECT StudentID FROM GroupStudents) ORDER BY FirstName, LastName;", con);
            cmd.Parameters.AddWithValue("@InstructorCourseID", instructorCourseID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int studentID = GetSafeInteger(reader, "StudentID");
                studentIDs.Add(studentID);
            }
            con.Close();

            foreach (int studentID in studentIDs)
            {
                Student student = GrouperMethods.GetStudent(studentID);
                if (student != null)
                {
                    students.Add(student);
                }
            }
            return students;
        }

        public static void UpdateStudent(Student student)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
                @"UPDATE Students  
                    SET DuckID = @DuckID,
                        UOID = @UOID, 
                        FirstName = @FirstName,
                        LastName = @LastName, 
                        PreferredName = @PreferredName, 
                        EnglishSecondLanguageFlag = @EnglishSecondLanguageFlag, 
                        NativeLanguage = @NativeLanguage, 
                        DevelopmentExperience = @DevelopmentExperience, 
                        LearningExpectations = @LearningExpectations, 
                        InitialNotificationSentDate = @InitialNotificationSentDate, 
                        SurveySubmittedDate = @SurveySubmittedDate                       
                    WHERE StudentID = @StudentID;", con);
            cmd.Parameters.AddWithValue("@StudentID", student.StudentID);
            cmd.Parameters.AddWithValue("@DuckID", student.DuckID ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@UOID", student.UOID ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FirstName", student.FirstName ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@LastName", student.LastName ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@PreferredName", student.PreferredName ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@EnglishSecondLanguageFlag", student.EnglishSecondLanguageFlag ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@NativeLanguage", student.NativeLanguage ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@DevelopmentExperience", student.DevelopmentExperience ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@LearningExpectations", student.LearningExpectations ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@SurveySubmittedDate", student.SurveySubmittedDate ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@InitialNotificationSentDate", student.InitialNotificationSentDate ?? (Object)DBNull.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            DeleteStudentLanguages(student.StudentID);
            DeleteStudentRoleInterests(student.StudentID);
            DeleteStudentSkills(student.StudentID);
            DeleteStudentPriorCourses(student.StudentID);
            DeleteStudentCurrentCourses(student.StudentID);

            foreach(Course course in student.PriorCourses)
            {
                InsertStudentPriorCourse(student.StudentID, course.CourseID, course.Grade);
            }
            foreach (Course course in student.CurrentCourses)
            {
                InsertStudentCurrentCourse(student.StudentID, course.CourseID);
            }
            foreach (ProgrammingLanguage language in student.Languages)
            {
                InsertStudentLanguage(student.StudentID, language.LanguageID, (int)language.ProficiencyLevel);
            }

            foreach(Role role in student.InterestedRoles)
            {
                InsertStudentRoleInterest(student.StudentID, role.RoleID, (int)role.InterestLevel);
            }

            foreach (Skill skill in student.Skills)
            {
                InsertStudentSkill(student.StudentID, skill.SkillID, (int)skill.ProficiencyLevel);
            }
        }

        public static void DeleteStudent(int studentID)
        {

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
                @"DELETE FROM Students_CoursesCompleted WHERE StudentID = @StudentID;
                DELETE FROM Students_EnrolledCourses WHERE StudentID = @StudentID;
                DELETE FROM Students_ProgrammingLanguages WHERE StudentID = @StudentID;
                DELETE FROM Students_RoleInterests WHERE StudentID = @StudentID;
                DELETE FROM Students_Skills WHERE StudentID = @StudentID;
                DELETE FROM GroupStudents WHERE StudentID = @StudentID;
                DELETE FROM Students WHERE StudentID = @StudentID;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        #endregion

        #region Instructor Courses

        public static int InsertInstructorCourse(InstructorCourse course)
        {
            int instructorCourseID = 0;

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(
                @"SELECT *  FROM Instructors_CourseSections 
                WHERE InstructorID = @InstructorID 
                AND CourseID = @CourseID 
                AND TermNumber = @TermNumber 
                AND Year = @Year 
                AND DaysOfWeek = @DaysOfWeek 
                AND StartTime = @StartTime 
                AND EndTime = @EndTime 
                AND CRN = @CRN;", con);
            cmd.Parameters.AddWithValue("@InstructorID", course.InstructorID);
            cmd.Parameters.AddWithValue("@CourseID", course.CourseID);
            if (course.TermNumber > 0)
            {
                cmd.Parameters.AddWithValue("@TermNumber", course.TermNumber);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TermNumber", (Object)DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@DaysOfWeek", course.DaysOfWeek ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@StartTime", course.StartTime ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@EndTime", course.EndTime ?? (Object)DBNull.Value);
            if (course.Year > 0)
            {
                cmd.Parameters.AddWithValue("@Year", course.Year);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Year", (Object)DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@CRN", course.CRN ?? (Object)DBNull.Value);

            instructorCourseID = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            if (instructorCourseID == 0)
            {
                cmd = new SqlCommand(
                @"INSERT INTO Instructors_CourseSections     
                    (InstructorID, CourseID, TermNumber, TermName, Year, DaysOfWeek, StartTime, EndTime, CRN) 
                    VALUES 
                    (@InstructorID, @CourseID, @TermNumber, @TermName, @Year, @DaysOfWeek, @StartTime, @EndTime, @CRN);
                    SELECT SCOPE_IDENTITY();", con);
                cmd.Parameters.AddWithValue("@InstructorID", course.InstructorID);
                cmd.Parameters.AddWithValue("@CourseID", course.CourseID);
                if (course.TermNumber > 0)
                {
                    cmd.Parameters.AddWithValue("@TermNumber", course.TermNumber);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@TermNumber", (Object)DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@TermName", course.TermName ?? (Object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DaysOfWeek", course.DaysOfWeek ?? (Object)DBNull.Value);
                cmd.Parameters.AddWithValue("@StartTime", course.StartTime ?? (Object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EndTime", course.EndTime ?? (Object)DBNull.Value);
                if (course.Year > 0)
                {
                    cmd.Parameters.AddWithValue("@Year", course.Year);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Year", (Object)DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@CRN", course.CRN ?? (Object)DBNull.Value);
                con.Open();
                instructorCourseID = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            return instructorCourseID;
        }

        public static void UpdateInstructorCourse(InstructorCourse instructorCourse)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
                @"UPDATE Instructors_CourseSections  
                    SET CourseID = @CourseID, 
                        TermNumber = @TermNumber, 
                        TermName = @TermName, 
                        Year = @Year, 
                        DaysOfWeek = @DaysOfWeek, 
                        StartTime = @StartTime, 
                        EndTime = @EndTime, 
                        CRN = @CRN 
                    WHERE InstructorCourseID = @InstructorCourseID;", con);
            cmd.Parameters.AddWithValue("@InstructorCourseID", instructorCourse.InstructorCourseID);
            cmd.Parameters.AddWithValue("@CourseID", instructorCourse.CourseID);
            if (instructorCourse.TermNumber > 0)
            {
                cmd.Parameters.AddWithValue("@TermNumber", instructorCourse.TermNumber);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TermNumber", (Object)DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@TermName", instructorCourse.TermName ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@DaysOfWeek", instructorCourse.DaysOfWeek ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@StartTime", instructorCourse.StartTime ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@EndTime", instructorCourse.EndTime ?? (Object)DBNull.Value);
            if (instructorCourse.Year > 0)
            {
                cmd.Parameters.AddWithValue("@Year", instructorCourse.Year);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Year", (Object)DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@CRN", instructorCourse.CRN ?? (Object)DBNull.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static List<InstructorCourse> GetInstructorCourses()
        {
            List<InstructorCourse> instructorCourses = new List<InstructorCourse>();
            List<int> instructorCourseIDs = new List<int>();

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Instructors_CourseSections ORDER BY Year, TermNumber;", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int instructorCourseID = GetSafeInteger(reader, "InstructorCourseID");

                instructorCourseIDs.Add(instructorCourseID);
            }
            con.Close();

            foreach (int id in instructorCourseIDs)
            {
                InstructorCourse course = GetInstructorCourse(id);
                instructorCourses.Add(course);
            }

            return instructorCourses;
        }

        public static List<InstructorCourse> GetInstructorCourses(int instructorID)
        {
            List<InstructorCourse> instructorCourses = new List<InstructorCourse>();
            List<int> instructorCourseIDs = new List<int>();

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Instructors_CourseSections WHERE InstructorID = @InstructorID ORDER BY Year, TermNumber;", con);
            cmd.Parameters.AddWithValue("@InstructorID", instructorID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int instructorCourseID = GetSafeInteger(reader, "InstructorCourseID");
               
                instructorCourseIDs.Add(instructorCourseID);
            }
            con.Close();

            foreach(int id in instructorCourseIDs)
            {
                InstructorCourse course = GetInstructorCourse(id);
                instructorCourses.Add(course);
            }

            return instructorCourses;
        }

        public static InstructorCourse GetInstructorCourse(int instructorCourseID)
        {
            InstructorCourse course = null;

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Instructors_CourseSections WHERE InstructorCourseID = @InstructorCourseID;", con);
            cmd.Parameters.AddWithValue("@InstructorCourseID", instructorCourseID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                course = new InstructorCourse();

                course.InstructorCourseID = GetSafeInteger(reader, "InstructorCourseID");
                course.InstructorID = GetSafeInteger(reader, "InstructorID");
                course.CourseID = GetSafeInteger(reader, "CourseID");
                course.TermNumber = GetSafeInteger(reader, "TermNumber");
                course.TermName = GetSafeString(reader, "TermName");
                course.Year = GetSafeInteger(reader, "Year");
                course.CurrentCourseSectionFlag = GetSafeBoolean(reader, "CurrentCourseSectionFlag");
                course.DaysOfWeek = GetSafeString(reader, "DaysOfWeek");
                course.StartTime = GetSafeDateTime(reader, "StartTime");
                course.EndTime = GetSafeDateTime(reader, "EndTime");
                course.CRN = GetSafeInteger(reader, "CRN");
            }
            con.Close();

            if (course != null)
            {
                if (course.CourseID > 0)
                {
                    course.Course = GetCourse(course.CourseID);
                    course.Groups = GetInstructorCourseGroups(course.InstructorCourseID);
                    course.Students = GetStudents(course.InstructorCourseID);
                }
            }
            return course;
        }

        public static void DeleteInstructorCourse(int instructorCourseID)
        {
            InstructorCourse course = GetInstructorCourse(instructorCourseID);

            foreach(Group group in course.Groups)
            {
                foreach(Student member in group.Members)
                {
                    DeleteStudent(member.StudentID);
                }
                DeleteGroup(group.GroupID);
            }

            foreach(Student student in course.Students)
            {
                DeleteStudent(student.StudentID);
            }

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
                @"DELETE FROM Instructors_CourseSections WHERE InstructorCourseID = @InstructorCourseID;", con);
            cmd.Parameters.AddWithValue("@InstructorCourseID", instructorCourseID);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public InstructorCourse GetCurrentInstructorCourse(int instructorID)
        {
            InstructorCourse course = null;
            int courseSectionID = 0;

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Instructors_CourseSections WHERE InstructorID = @InstructorID 
                                                AND CurrentInstructorCourseFlag = 'True';", con);
            cmd.Parameters.AddWithValue("@InstructorID", instructorID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                courseSectionID = GetSafeInteger(reader, "CourseSectionID");
            }
            con.Close();

            if (courseSectionID > 0)
            {
                course = GrouperMethods.GetInstructorCourse(courseSectionID);
            }

            return course;
        }

        #endregion

        #region Groups

        public static int InsertGroup(Group group)
        {
            int groupID = 0;

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(
                @"SELECT GroupID FROM Groups WHERE
                CourseSectionID = @InstructorCourseID 
                AND GroupNumber = @GroupNumber;", con);
            cmd.Parameters.AddWithValue("@InstructorCourseID", group.InstructorCourseID);
            cmd.Parameters.AddWithValue("@GroupNumber", group.GroupNumber);

            groupID = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            if (groupID == 0)
            {
                cmd = new SqlCommand(
                @"INSERT INTO Groups     
                    (CourseSectionID, GroupNumber, Name, Notes) 
                    VALUES 
                    (@InstructorCourseID, @GroupNumber, @Name, @Notes);
                    SELECT SCOPE_IDENTITY();", con);
                cmd.Parameters.AddWithValue("@InstructorCourseID", group.InstructorCourseID);
                cmd.Parameters.AddWithValue("@GroupNumber", group.GroupNumber);
                cmd.Parameters.AddWithValue("@Name", group.Name ?? (Object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Notes", group.Notes ?? (Object)DBNull.Value);

                con.Open();
                groupID = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            return groupID;
        }

        public static Group GetGroup(int groupID)
        {
            Group group = null;

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Groups WHERE GroupID = @GroupID;", con);
            cmd.Parameters.AddWithValue("@GroupID", groupID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                group = new Group();

                group.InstructorCourseID = GetSafeInteger(reader, "CourseSectionID");
                group.GroupID = groupID;
                group.GroupNumber = GetSafeInteger(reader, "GroupNumber");
                group.Name = GetSafeString(reader, "Name");
                group.Notes = GetSafeString(reader, "Notes");

            }
            con.Close();

            if(group != null)
            {
                group.Members = GetGroupMembers(group.GroupID);
            }

            return group;
        }

        public static void UpdateGroup(Group group)
        {

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
                @"UPDATE Groups  
                    SET Name = @Name, 
                    Notes = @Notes 
                    WHERE GroupID = @GroupID;", con);
            cmd.Parameters.AddWithValue("@GroupID", group.GroupID);
            cmd.Parameters.AddWithValue("@Name", group.Name ?? (Object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Notes", group.Notes ?? (Object)DBNull.Value);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void DeleteGroup(int groupID)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"DELETE FROM GroupStudents WHERE GroupID = @GroupID;
                                              DELETE FROM Groups WHERE GroupID = @GroupID;", con);
            cmd.Parameters.AddWithValue("@GroupID", groupID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static List<Group> GetInstructorCourseGroups(int instructorCourseID)
        {
            List<Group> groups = new List<Group>();
            List<int> groupIDs = new List<int>();

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Groups WHERE CourseSectionID = @InstructorCourseID ORDER BY GroupNumber;", con);
            cmd.Parameters.AddWithValue("@InstructorCourseID", instructorCourseID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int groupID = GetSafeInteger(reader, "GroupID");

                groupIDs.Add(groupID);
            }
            con.Close();

            foreach(int id in groupIDs)
            {
                Group group = GetGroup(id);
                
                if(group != null)
                {
                    groups.Add(group);
                }
            }

            return groups;
        }

        public static void InsertGroupMember(int groupID, int studentID)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
                @"SELECT GroupID FROM GroupStudents 
                WHERE GroupID = @GroupID 
                AND StudentID = @StudentID;", con);
            cmd.Parameters.AddWithValue("@GroupID", groupID);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            con.Open();
            int existingGroupID = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            if (existingGroupID == 0)
            {
                cmd = new SqlCommand(
                @"INSERT INTO GroupStudents      
                    (GroupID, StudentID) 
                    VALUES 
                    (@GroupID, @StudentID);", con);
                cmd.Parameters.AddWithValue("@GroupID", groupID);
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public static void DeleteGroupMember(int studentID)
        {
            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(
                @"DELETE FROM GroupStudents WHERE StudentID = @StudentID;", con);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
           
        }

        public static List<Student> GetGroupMembers(int groupID)
        {
            List<Student> students = new List<Student>();
            List<int> studentIDs = new List<int>();

            SqlConnection con = new SqlConnection(GrouperConnectionString.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT StudentID FROM GroupStudents WHERE GroupID = @GroupID;", con);
            cmd.Parameters.AddWithValue("@GroupID", groupID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int studentID = GetSafeInteger(reader, "StudentID");
                studentIDs.Add(studentID);
            }
            con.Close();

            foreach (int studentID in studentIDs)
            {
                Student student = GrouperMethods.GetStudent(studentID);
                if (student != null)
                {
                    students.Add(student);
                }
            }
            return students;
        }

        #endregion
    }
}