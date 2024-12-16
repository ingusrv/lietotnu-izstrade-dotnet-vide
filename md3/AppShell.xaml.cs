namespace md3
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute(nameof(StudentPage), typeof(StudentPage));
            Routing.RegisterRoute(nameof(TeacherPage), typeof(TeacherPage));
            Routing.RegisterRoute(nameof(CoursePage), typeof(CoursePage));
            Routing.RegisterRoute(nameof(AssignmentPage), typeof(AssignmentPage));
            Routing.RegisterRoute(nameof(SubmissionPage), typeof(SubmissionPage));

            InitializeComponent();
        }
    }
}
