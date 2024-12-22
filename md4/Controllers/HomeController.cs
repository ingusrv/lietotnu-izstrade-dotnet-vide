using System.Diagnostics;
using md4.Data;
using md4.Data.Entities;
using md4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace md4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CreateTestData()
        {
            try
            {
                var teacher = new Teacher
                {
                    Name = "Elîna",
                    Surname = "Kalniòa",
                    Gender = Gender.Woman,
                    ContractDate = new DateTime(2020, 9, 28)
                };
                var teacher2 = new Teacher
                {
                    Name = "Andris",
                    Surname = "Bçrziòð",
                    Gender = Gender.Man,
                    ContractDate = new DateTime(2023, 12, 7)
                };

                _context.Teachers.Add(teacher);
                _context.Teachers.Add(teacher2);

                var course = new Course
                {
                    Teacher = teacher,
                    Name = "Lietotòu izstrâde .NET vidç"
                };
                var course2 = new Course
                {
                    Teacher = teacher2,
                    Name = "Spçïu izstrâde ar Unity"
                };

                _context.Courses.Add(course);
                _context.Courses.Add(course2);

                var assignment = new Assignment
                {
                    Deadline = DateTime.Today,
                    Course = course,
                    Description = "Treðâ mâjasdarba uzdevums"
                };
                var assignment2 = new Assignment
                {
                    Deadline = DateTime.Today,
                    Course = course2,
                    Description = "Pirmâs spçles nodevums"
                };

                _context.Assignments.Add(assignment);
                _context.Assignments.Add(assignment2);

                var student = new Student("Ingus", "Valheims", Gender.Man, "abc1234");
                var student2 = new Student("Elîza", "Avotiòa", Gender.Woman, "def4321");

                _context.Students.Add(student);
                _context.Students.Add(student2);

                var submission = new Submission
                {
                    Assignment = assignment,
                    Student = student,
                    SubmissionTime = DateTime.Now,
                    Score = 100
                };
                var submission2 = new Submission
                {
                    Assignment = assignment2,
                    Student = student2,
                    SubmissionTime = DateTime.Now,
                    Score = 100
                };

                _context.Submissions.Add(submission);
                _context.Submissions.Add(submission2);

                _context.SaveChanges();

                TempData["SuccessMessage"] = "Test data created successfully!";
                return RedirectToAction(nameof(Index));
            } catch
            {
                TempData["ErrorMessage"] = "Unable to create test data!";
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult ResetDatabase()
        {
            try {
                _context.Submissions.ExecuteDelete();
                _context.Assignments.ExecuteDelete();
                _context.Courses.ExecuteDelete();
                _context.Teachers.ExecuteDelete();
                _context.Students.ExecuteDelete();

                TempData["SuccessMessage"] = "Database reset successfully!";
                return RedirectToAction(nameof(Index));
            } catch
            {
                TempData["ErrorMessage"] = "Unable to reset database!";
                return RedirectToAction(nameof(Index));
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
