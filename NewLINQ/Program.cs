using NewLINQ.Models;
using System.ComponentModel;

namespace NewLINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyDB context = new MyDB();
            do
            {
                Console.WriteLine("\t\t\t-----Main Menu-----\n\t\t-----Choose by entering a number:-----\n\n\t\t1)Change teacher");
                Console.WriteLine("\t\t2)Change subject\n\t\t3)Check Subject\n\t\t4)Show students and their teacher\n\t\t5)Math Teachers");
                Console.WriteLine("\n\t\t-----Observe: 'SaveChanges' is inactive!----- ");
                string val = Console.ReadLine();
                if (val == "1")
                {
                    ChangeTeacher(context);   //Uppdatera en student record om sin lärare är Anas till Reidar.
                }
                else if (val == "2")
                {
                    ChangeSubject(context);    //Editera en Ämne från programmering2 till OOP

                }
                else if (val == "3")
                {
                    CheckSubject(context);     //Kolla om ämnen tabell Contains programmering1 eller inte.
                }
                else if (val == "4")
                {
                    StudentsAndTeachers(context);     //    Hämta alla elever med sina lärare.
                }
                else if (val == "5")
                {
                    MathTeachers(context);           //    Hämta alla lärare som undervisar matte
                }
                else
                {
                    Console.WriteLine("You can only enter a numberin the menu");
                }
                Console.WriteLine("\n\tPress any key to continue:");
                Console.ReadKey();
                Console.Clear();

            } while (true == true);
        }
        public static void MathTeachers(MyDB context)
        {
            //    Hämta alla lärare som undervisar matte

            List<Teacher> mathTeachers = context.Teachers.Where(t => t.Subjects.Any(s => s.SubjectName == "Math")).ToList();

            foreach (var teacher in mathTeachers)
            {
                Console.WriteLine($"Math teacher : {teacher.Name}");
            }
        }
        public static void StudentsAndTeachers(MyDB context)
        {
            //    Hämta alla elever med sina lärare.

            //Writing all teachers and their studends,  sorted by courses
            var result = from tea in context.Teachers
                         from sub in tea.Subjects
                         from cour in sub.Courses
                         from stud in cour.Students
                         orderby sub.SubjectName
                         select new { TeacherName = tea.Name, StudentName = stud.Name, SubjectName = sub.SubjectName };

            //list to store subjectname, so as not to write it twice
            List<string> dub = new List<string>();
            foreach (var item in result)
            {
                if (!dub.Contains(item.SubjectName))
                {
                    Console.WriteLine($"\t\t-----Subject: {item.SubjectName}-----");
                    dub.Add(item.SubjectName);
                }
                Console.WriteLine($"\n\tTeacher: {item.TeacherName}\t\tStudent: {item.StudentName}\n\n");
            }
            dub.Clear();
        }
        public static void CheckSubject(MyDB context)
        {
            //    Kolla om ämnen tabell Contains programmering1 eller inte.
            //Checking if the subjects name is Prog1 and saying if its found
            var check = context.Subjects.Where(x => x.SubjectName == "Prog1");
            foreach (var item in check)
            {
                Console.WriteLine("Found the subject: '" + item.SubjectName + "'");
            }
        }
        public static void ChangeTeacher(MyDB context)
        {
            //    Uppdatera en student record om sin lärare är Anas till Reidar.
            var oldTeacher = context.Teachers.FirstOrDefault(t => t.Name == "Anas");
            var newTeacher = context.Teachers.FirstOrDefault(t => t.Name == "Reidar");
            //Identifying teachers and where they are teaching, to switch from Anas to Reidar as teacher
            var coursesToUpdate = context.Courses.Where(c => c.Teacher.Id == oldTeacher.Id);
            foreach (var course in coursesToUpdate)
            {
                Console.WriteLine($"{course.Teacher.Name} is the teacher for: {course.CourseName}");
                course.Teacher = newTeacher;
            }
            Console.WriteLine("\nAfter changeing: this is the new order!\n");
            foreach (var course in coursesToUpdate)
            {
                Console.WriteLine($"{course.Teacher.Name} is the teacher for: {course.CourseName}");
            }
            //context.SaveChanges();
        }
        public static void ChangeSubject(MyDB context)
        {
            //    Editera en Ämne från programmering2 till OOP
            Console.WriteLine("This method will show that if we find the subject : 'Prog2' we will change it to 'OOP'");
            //finding subject where subjectname is Prog2 and changeing to OOP
            var change = context.Subjects.Where(x => x.SubjectName == "Prog2");
            foreach (var item in change)
            {
                Console.WriteLine("\n\nFound the subject: " + item.SubjectName);
                item.SubjectName = "OOP";
                Console.WriteLine("The subject is changed to: " + item.SubjectName);
            }
            //context.SaveChanges();
        }
    }
}