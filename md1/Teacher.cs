using System.Text.Json.Serialization;

public class Teacher : Person
{
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly ContractDate { get; set; }

    public override string ToString()
    {
        return base.ToString() + ", Contract Date: " + ContractDate;
    }
}
