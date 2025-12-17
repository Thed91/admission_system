using admission_system.Server.Enteties;

namespace admission_system.Server.Services
{
    public interface IAdmissionService
    {
        string CheckVisitor(VisitorRequest visitorRequest);
        Task<List<VisitorRequest>> GetAllVisitor();
        Task<VisitorRequest> GetVisitorByID(Guid id);

    }
}
