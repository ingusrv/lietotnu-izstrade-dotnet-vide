public class Course
{
    public string Name { get; set; }
    public Teacher Teacher { get; set; }

    public override string ToString()
    {
        // pieņemu, ka teacher informācijai jāizmanto iepriekš pārdefinēto ToString
        return base.ToString() + " - Name: " + Name + ", Teacher: " + Teacher.ToString();
    }
}
