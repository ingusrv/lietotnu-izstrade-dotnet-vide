// See https://aka.ms/new-console-template for more information
using nmd3;

// šie avoti palīdzēja izprast kā strādā tipu refleksija
// https://stackoverflow.com/questions/20240092/use-reflection-to-search-in-dlls-c-sharp
// https://learn.microsoft.com/en-us/dotnet/standard/assembly/unloadability

// - Vai ir klases ar prasītajiem vārdiem?
// - Vai klase Person ir abstrakta?
// - Vai klasēm ir prasītās īpašības?
// - Vai konstruktoru skaits ir atbilst prasītajam (lielāks vienāds ar prasīto)?



static void checkProperty(Type type, string name)
{
    if (type.GetProperty(name) == null)
    {
        Console.WriteLine($"Tipam '{type.Name}' netika atrasta īpašība '{name}'!"); 
    }
}

static void checkConstructorCount(Type type, int count)
{
    var len = type.GetConstructors().Length;
    if (len < count)
    {
        Console.WriteLine($"Tipam '{type.Name}' konstruktoru skaits ir mazāks nekā sagaidītais! Ir {len}, bet sagaidīts ir {count}.");
    }
}

static void checkFile(string path)
{
    Console.WriteLine($"'{Path.GetRelativePath(Environment.CurrentDirectory, path)}' Pārbaude:");
    try
    {
        // jauns context mums ļauj neuztraukties par to vairākiem assembly sakrīt nosaukumi
        // citādāk būtu "Assemby with the same name already loaded"
        var ac = new AssemblyContext();
        var a = ac.LoadFromAssemblyPath(path);
        var types = a.GetTypes();

        //Console.WriteLine("Atrastie tipi:");
        //foreach (var t in types)
        //{
        //    Console.WriteLine(t.FullName);
        //}


        // Person
        // abstrakta
        // īpašības Name, Surname, FullName, Gender
        // 0 konstruktori 
        var personClass = types.Where(t => t.IsClass && t.Name == "Person").FirstOrDefault();
        if (personClass != null)
        {
            if (!personClass.IsAbstract)
            {
                Console.WriteLine("Tips 'Person' nav abstrakts!");
            }

            checkProperty(personClass, "Name");
            checkProperty(personClass, "Surname");
            checkProperty(personClass, "FullName");
            checkProperty(personClass, "Gender");
        }
        else
        {
            Console.WriteLine("Tips 'Person' netika atrasts!");
        }

        // Student
        // īpašības Name, Surname, FullName, Gender, StudentIdNumber
        // 0 konstruktori 
        var studentClass = types.Where(t => t.IsClass && t.Name == "Student").FirstOrDefault();
        if (studentClass != null)
        {
            checkProperty(studentClass, "Name");
            checkProperty(studentClass, "Surname");
            checkProperty(studentClass, "FullName");
            checkProperty(studentClass, "Gender");
            checkProperty(studentClass, "StudentIdNumber");
            checkConstructorCount(studentClass, 2);
        }
        else
        {
            Console.WriteLine("Tips 'Student' netika atrasts!");
        }

        // Teacher
        // īpašības Name, Surname, FullName, Gender, ContractDate
        // 0 konstruktori 
        var teacherClass = types.Where(t => t.IsClass && t.Name == "Teacher").FirstOrDefault();
        if (teacherClass != null)
        {
            checkProperty(teacherClass, "Name");
            checkProperty(teacherClass, "Surname");
            checkProperty(teacherClass, "FullName");
            checkProperty(teacherClass, "Gender");
            checkProperty(teacherClass, "ContractDate");
        }
        else
        {
            Console.WriteLine("Tips 'Teacher' netika atrasts!");
        }


        // Course
        // īpašības Name, Teacher
        // 0 konstruktori 
        var courseClass = types.Where(t => t.IsClass && t.Name == "Course").FirstOrDefault();
        if (courseClass != null)
        {
            checkProperty(courseClass, "Name");
            checkProperty(courseClass, "Teacher");
        }
        else
        {
            Console.WriteLine("Tips 'Course' netika atrasts!");
        }


        // Assignment
        // īpašības Deadline, Course, Description
        // 0 konstruktori 
        var assignmentClass = types.Where(t => t.IsClass && t.Name == "Assignment").FirstOrDefault();
        if (assignmentClass != null)
        {
            checkProperty(assignmentClass, "Deadline");
            checkProperty(assignmentClass, "Course");
            checkProperty(assignmentClass, "Description");
        }
        else
        {
            Console.WriteLine("Tips 'Assignment' netika atrasts!");
        }


        // Submission
        // īpašības Assignment, Student, SubmissionTime, Score
        // 0 konstruktori 
        var submissionClass = types.Where(t => t.IsClass && t.Name == "Submission").FirstOrDefault();
        if (submissionClass != null)
        {
            checkProperty(submissionClass, "Assignment");
            checkProperty(submissionClass, "Student");
            checkProperty(submissionClass, "SubmissionTime");
            checkProperty(submissionClass, "Score");
        }
        else
        {
            Console.WriteLine("Tips 'Submission' netika atrasts!");
        }


        ac.Unload();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Neizdevās pārbaudīt failu: {ex.Message}");
    }
}

var files = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "test"), "*.dll");
Console.WriteLine("Atrastie dll:");
foreach (var file in files)
{
    Console.WriteLine(Path.GetRelativePath(Environment.CurrentDirectory, file));
}

Console.WriteLine("\n\n\n");

foreach (var file in files)
{
    checkFile(file);
    Console.WriteLine("\n\n\n");
}

Console.Write("Pārbaude pabeigta\nNospiediet jebkuru taustiņu lai izietu");
Console.ReadLine();
   