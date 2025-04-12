namespace WebMVC.Models;

public partial class Institute
{
    public short InstituteId { get; set; }

    public string InstituteName { get; set; } = null!;

    public string DeanLastname { get; set; } = null!;

    public string DeanFirstname { get; set; } = null!;

    public string DeanPatronymic { get; set; } = null!;

    public string DeanDegree { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
