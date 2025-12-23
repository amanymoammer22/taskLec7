using System;
using System.Collections.Generic;
using System.Linq;


namespace introToOOP
{
    internal class Program
    {
        static void Main(string[] args)
        {

            StudentManager manager = new StudentManager();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n==== Student Management System ====");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Instructor");
                Console.WriteLine("3. Add Course");
                Console.WriteLine("4. Enroll Student in Course");
                Console.WriteLine("5. Show All Students");
                Console.WriteLine("6. Show All Courses");
                Console.WriteLine("7. Show All Instructors");
                Console.WriteLine("8. Find Student by ID");
                Console.WriteLine("9. Find Course by ID");
                Console.WriteLine("10. Exit");
                Console.Write("Choose option: ");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Student ID: ");
                        int sid = int.Parse(Console.ReadLine());
                        Console.Write("Name: ");
                        string sname = Console.ReadLine();
                        Console.Write("Age: ");
                        int age = int.Parse(Console.ReadLine());

                        bool addedStudent = manager.AddStudent(new Student(sid, sname, age));
                        Console.WriteLine(addedStudent ? "Student added successfully" : "Student ID already exists");
                        break;

                    case 2:
                        Console.Write("Instructor ID: ");
                        int iid = int.Parse(Console.ReadLine());
                        Console.Write("Name: ");
                        string iname = Console.ReadLine();
                        Console.Write("Specialization: ");
                        string spec = Console.ReadLine();

                        bool addedInstructor = manager.AddInstructor(new Instructor(iid, iname, spec));
                        Console.WriteLine(addedInstructor ? "Instructor added successfully" : "Instructor ID already exists");
                        break;

                    case 3:
                        Console.Write("Course ID: ");
                        int cid = int.Parse(Console.ReadLine());
                        Console.Write("Title: ");
                        string title = Console.ReadLine();

                        Console.Write("Instructor ID: ");
                        int instId = int.Parse(Console.ReadLine());
                        Instructor instructor = manager.FindInstructor(instId);

                        if (instructor == null)
                        {
                            Console.WriteLine("Instructor not found");
                        }
                        else
                        {
                            bool addedCourse = manager.AddCourse(new Course(cid, title, instructor));
                            Console.WriteLine(addedCourse ? "Course added successfully" : "Course ID already exists");
                        }
                        break;

                    case 4:
                        Console.Write("Student ID: ");
                        int esid = int.Parse(Console.ReadLine());
                        Console.Write("Course ID: ");
                        int ecid = int.Parse(Console.ReadLine());

                        bool enrolled = manager.EnrollStudentInCourse(esid, ecid);
                        Console.WriteLine(enrolled ? "Student enrolled successfully" : "Enrollment failed");
                        break;

                    case 5:
                        Console.WriteLine("\n--- Students ---");
                        foreach (var student in manager.Students)
                        {
                            Console.WriteLine(student.PrintStudentDetails());
                        }
                        break;

                    case 6:
                        Console.WriteLine("\n--- Courses ---");
                        foreach (var course in manager.Courses)
                        {
                            Console.WriteLine(course.PrintDetails());
                        }
                        break;

                    case 7:
                        Console.WriteLine("\n--- Instructors ---");
                        foreach (var inst in manager.Instructors)
                        {
                            Console.WriteLine(inst.PrintDetails());
                        }
                        break;

                    case 8:
                        Console.Write("Student ID: ");
                        int fsid = int.Parse(Console.ReadLine());
                        Student studentFound = manager.FindStudent(fsid);

                        Console.WriteLine(studentFound != null
                            ? studentFound.PrintStudentDetails()
                            : "Student not found");
                        break;

                    case 9:
                        Console.Write("Course ID: ");
                        int fcid = int.Parse(Console.ReadLine());
                        Course courseFound = manager.FindCourse(fcid);

                        Console.WriteLine(courseFound != null
                            ? courseFound.PrintDetails()
                            : "Course not found");
                        break;

                    case 10:
                        exit = true;
                        Console.WriteLine("Goodbye 👋");
                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

    }
    }


class StudentManager
    {
        public List<Student> Students { get; set; } = new();
        public List<Course> Courses { get; set; } = new();
        public List<Instructor> Instructors { get; set; } = new();

        public bool AddStudent(Student student)
        {
            if (Students.Any(s => s.StudentId == student.StudentId))
                return false;

            Students.Add(student);
            return true;
        }

        public bool AddInstructor(Instructor instructor)
        {
            if (Instructors.Any(i => i.InstructorId == instructor.InstructorId))
                return false;

            Instructors.Add(instructor);
            return true;
        }

        public bool AddCourse(Course course)
        {
            if (Courses.Any(c => c.CourseId == course.CourseId))
                return false;

            Courses.Add(course);
            return true;
        }
        public Student FindStudent(int studentId)
        {
            return Students.FirstOrDefault(s => s.StudentId == studentId);
        }

        public Course FindCourse(int courseId)
        {
            return Courses.FirstOrDefault(c => c.CourseId == courseId);
        }

        public Instructor FindInstructor(int instructorId)
        {
            return Instructors.FirstOrDefault(i => i.InstructorId == instructorId);
        }

        public bool EnrollStudentInCourse(int studentId, int courseId)
        {
            Student student = FindStudent(studentId);
            Course course = FindCourse(courseId);

            if (student == null || course == null)
                return false;

            return student.Enroll(course);
        }

        // Bonus 11
        public bool IsStudentEnrolled(int studentId, string courseName)
        {
            Student student = FindStudent(studentId);
            if (student == null) return false;

            return student.Courses.Any(c => c.Title == courseName);
        }

        // Bonus 12
        public string GetInstructorNameByCourse(string courseName)
        {
            Course course = Courses.FirstOrDefault(c => c.Title == courseName);
            return course?.Instructor.Name;
        }


    }


    class Instructor
    {
        public int InstructorId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }

        public Instructor(int id, string name, string specialization)
        {
            InstructorId = id;
            Name = name;
            Specialization = specialization;
        }

        public string PrintDetails()
        {
            return $"ID: {InstructorId}, Name: {Name}, Specialization: {Specialization}";
        }


    }
    
    
    class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public Instructor Instructor { get; set; }


        public Course(int courseId, string title, Instructor instructor)
        {
            CourseId = courseId;
            Title = title;
            Instructor = instructor;
        }

        public string PrintDetails()
        {
            return $"ID: {CourseId}, Title: {Title}, Instructor: {Instructor.Name}";
        }

    }
   
    class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Course> Courses { get; set; }

        public Student(int studentId, string name, int age)
        {
            StudentId = studentId;
            Name = name;
            Age = age;
            Courses = new List<Course>();
        }

        public bool Enroll(Course course)
        {
            if (Courses.Contains(course))
                return false;

            Courses.Add(course);
            return true;
        }

        public string PrintStudentDetails()
        {
            string courseNames = Courses.Count == 0
                ? "No Courses"
                : string.Join(", ", Courses.Select(c => c.Title));

            return $"ID: {StudentId}, Name: {Name}, Age: {Age}, Courses: {courseNames}";
        }
    }

