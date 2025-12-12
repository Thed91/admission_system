namespace admission_system.Server.Models
{
    public class VisitorRequest
    {
        private Guid VisitorId { get; set; }
        private int Age { get; set; }
        private string? VisitorType {  get; set; }
        private bool IsPass {  get; set; }
        private int PassLevel { get; set; }
        private bool IsWeapons { get; set; }
        private bool IsAggressive { get; set; }
        private string? Zone { get; set; }
    }
}
