using admission_system.Server.Data;
using admission_system.Server.Enteties;
using Microsoft.EntityFrameworkCore;

namespace admission_system.Server.Services
{
    public class AdmissionService : IAdmissionService
    {
        private readonly ApplicationDbContext _context;

        public AdmissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        const string GUEST = "guest";
        const string MEETING_ROOM = "meeting room";
        const string LABORATORY = "laboratory";
        const string SECRET_LABORATORY = "secret laboratory";
        
        public async Task<string> CheckVisitor(VisitorRequest visitorRequest)
        {
            string result;

            if (visitorRequest.Age < 18)
            {
                result = "Denied: under 18 years of age";
                await SaveVisitorRequest(visitorRequest, result);
                return result;
            }

            if (!visitorRequest.IsPass)
            {
                result = "Denied: no pass";
                await SaveVisitorRequest(visitorRequest, result);
                return result;
            }

            RiskLevel riskLevel;
            if (visitorRequest.IsWeapons && visitorRequest.IsAggressive)
            {
                riskLevel = RiskLevel.HIGH;
            }
            else if (visitorRequest.IsWeapons || visitorRequest.IsAggressive)
            {
                riskLevel = RiskLevel.MEDIUM;
            }
            else
            {
                riskLevel = RiskLevel.LOW;
            }

            if (riskLevel == RiskLevel.HIGH)
            {
                if (visitorRequest.VisitorType?.ToLower() == GUEST)
                {
                    result = "Denied: high risk level";
                    await SaveVisitorRequest(visitorRequest, result);
                    return result;
                }

                if (visitorRequest.Zone?.ToLower() != MEETING_ROOM)
                {
                    result = "Denied: high risk level, only Meeting Room available";
                    await SaveVisitorRequest(visitorRequest, result);
                    return result;
                }

                result = "Admitted to Meeting Room (high risk)";
                await SaveVisitorRequest(visitorRequest, result);
                return result;
            }

            if (riskLevel == RiskLevel.MEDIUM)
            {
                if (visitorRequest.VisitorType?.ToLower() == GUEST)
                {
                    result = "Denied: normal risk level";
                    await SaveVisitorRequest(visitorRequest, result);
                    return result;
                }

                if (visitorRequest.Zone?.ToLower() == SECRET_LABORATORY)
                {
                    result = "Denied: insufficient clearance level";
                    await SaveVisitorRequest(visitorRequest, result);
                    return result;
                }

                result = "Admitted to " + visitorRequest.Zone + " (medium risk)";
                await SaveVisitorRequest(visitorRequest, result);
                return result;
            }

            if (visitorRequest.VisitorType?.ToLower() == GUEST)
            {
                if (visitorRequest.Zone?.ToLower() != MEETING_ROOM)
                {
                    result = "Denied: only Meeting Room available";
                    await SaveVisitorRequest(visitorRequest, result);
                    return result;
                }

                result = "Admitted to " + visitorRequest.Zone;
                await SaveVisitorRequest(visitorRequest, result);
                return result;
            }

            if ((visitorRequest.Zone?.ToLower() == LABORATORY || visitorRequest.Zone?.ToLower() == SECRET_LABORATORY) && visitorRequest.PassLevel < 2)
            {
                result = "Denied: insufficient clearance level";
                await SaveVisitorRequest(visitorRequest, result);
                return result;
            }

            if (visitorRequest.Zone?.ToLower() == SECRET_LABORATORY && visitorRequest.PassLevel < 3)
            {
                result = "Denied: insufficient clearance level";
                await SaveVisitorRequest(visitorRequest, result);
                return result;
            }

            result = "Admitted to " + visitorRequest.Zone;
            await SaveVisitorRequest(visitorRequest, result);
            return result;
        }

        private async Task SaveVisitorRequest(VisitorRequest visitorRequest, string result)
        {
            visitorRequest.VisitorId = Guid.NewGuid();
            visitorRequest.CreateAt = DateTime.UtcNow;

            _context.Requests.Add(visitorRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<VisitorRequest?> GetVisitorByID(Guid id) => await _context.Requests.FirstOrDefaultAsync(r => r.VisitorId == id);

        public async Task<List<VisitorRequest>> GetAllVisitor() => await _context.Requests.OrderByDescending(r => r.CreateAt).ToListAsync();
        
    }

    enum RiskLevel
    {
        LOW,
        MEDIUM,
        HIGH
    }
}
