using System.ComponentModel.DataAnnotations;

namespace admission_system.Server.Enteties
{
    public class VisitorRequest
    {
        [Key]
        public Guid VisitorId { get; set; }
        public int Age { get; set; }
        public string? VisitorType {  get; set; }
        public bool IsPass {  get; set; }
        public int PassLevel { get; set; }
        public bool IsWeapons { get; set; }
        public bool IsAggressive { get; set; }
        public string? Zone { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}
