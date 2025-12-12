using admission_system.Server.Models;

namespace admission_system.Server.Services
{
    public interface IAdmissionService
    {
        string CheckVisitor(VisitorRequest visitorRequest);
    }
}
