using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Phase1Project2Jan11
{

    class Teacher
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ClassAndSection { get; set; }
    }

    class TeacherManagementSystem
    {
        private const string FilePath = "teachers.txt";

        private List<Teacher> teachers;

        public TeacherManagementSystem()
        {
            teachers = new List<Teacher>();
            LoadTeachers();
        }

        public void AddTeacher(Teacher teacher)
        {
            teachers.Add(teacher);
            SaveTeachers();
            Console.WriteLine("Teacher added successfully.");
        }

        public void UpdateTeacher(int id, Teacher updatedTeacher)
        {
            Teacher existingTeacher = teachers.Find(t => t.ID == id);
            if (existingTeacher != null)
            {
                existingTeacher.Name = updatedTeacher.Name;
                existingTeacher.ClassAndSection = updatedTeacher.ClassAndSection;
                SaveTeachers();
                Console.WriteLine("Teacher updated successfully.");
            }
            else
            {
                Console.WriteLine("Teacher not found.");
            }
        }

        public void DisplayTeachers()
        {
            Console.WriteLine("Teacher List:");
            foreach (var teacher in teachers)
            {
                Console.WriteLine($"ID: {teacher.ID}, Name: {teacher.Name}, Class and Section: {teacher.ClassAndSection}");
            }
        }

        private void LoadTeachers()
        {
            if (File.Exists(FilePath))
            {
                string[] lines = File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 3 && int.TryParse(parts[0], out int id))
                    {
                        teachers.Add(new Teacher
                        {
                            ID = id,
                            Name = parts[1],
                            ClassAndSection = parts[2]
                        });
                    }
                }
            }
        }

        private void SaveTeachers()
        {
            List<string> lines = new List<string>();
            foreach (var teacher in teachers)
            {
                lines.Add($"{teacher.ID},{teacher.Name},{teacher.ClassAndSection}");
            }
            File.WriteAllLines(FilePath, lines);
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            TeacherManagementSystem teacherManagementSystem = new TeacherManagementSystem();

            while (true)
            {
                Console.WriteLine("Enter 1: Add Teacher 2: Update Teacher 3: Display Teachers 4: Exit");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter Teacher details - ID, Name, Class and Section:");
                        Teacher newTeacher = new Teacher
                        {
                            ID = int.Parse(Console.ReadLine()),
                            Name = Console.ReadLine(),
                            ClassAndSection = Console.ReadLine()
                        };
                        teacherManagementSystem.AddTeacher(newTeacher);
                        break;

                    case 2:
                        Console.WriteLine("Enter Teacher ID to update:");
                        int teacherIdToUpdate = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter updated Teacher details - Name, Class and Section:");
                        Teacher updatedTeacher = new Teacher
                        {
                            Name = Console.ReadLine(),
                            ClassAndSection = Console.ReadLine()
                        };
                        teacherManagementSystem.UpdateTeacher(teacherIdToUpdate, updatedTeacher);
                        break;

                    case 3:
                        teacherManagementSystem.DisplayTeachers();
                        break;

                    case 4:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
        }
    }
}
  
