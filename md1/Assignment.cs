using System.Text.Json.Serialization;

public class Assignment
{
    // ja pieminēts vienkārši tips "Date", tad gan jau sagaidīts ka izmanto DateOnly nevis DateTime?
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly Deadline { get; set; }
    public Course Course { get; set; }
    public string Description { get; set; }

    public override string ToString()
    {
        return base.ToString() + " - Description: " + Description + ", Deadline: " + Deadline.ToString() + ", Course: " + Course.ToString();
    }
}


