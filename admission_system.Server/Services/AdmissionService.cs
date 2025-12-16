using admission_system.Server.Models;

namespace admission_system.Server.Services
{
    public class AdmissionService : IAdmissionService
    {
        const string GUEST = "guest";
        const string MEETING_ROOM = "meeting room";
        const string LABORATORY = "laboratory";
        const string SECRET_LABORATORY = "secret laboratory";
        RiskLevel _RiskLevel;
        public string CheckVisitor(VisitorRequest visitorRequest)
        {
            if (visitorRequest.Age < 18)
            {
                return "Denied: under 18 years of age";
            }
            else if (!visitorRequest.IsPass)
            {
                return "Denied: no pass";
            }

            if (!visitorRequest.IsWeapons && !visitorRequest.IsAggressive)
            {
                _RiskLevel = RiskLevel.LOW;
            }

            if (visitorRequest.IsWeapons || visitorRequest.IsAggressive)
            {
                _RiskLevel = RiskLevel.MEDIUM;
            }

            if (visitorRequest.IsWeapons && visitorRequest.IsAggressive)
            {
                _RiskLevel = RiskLevel.HIGH;
            }

            if (_RiskLevel == RiskLevel.HIGH)
            {
                if (visitorRequest.VisitorType?.ToLower() == GUEST)
                {
                    return "Denied: high risk level";
                }

                if (visitorRequest.Zone?.ToLower() != MEETING_ROOM)
                {
                    return "Denied: high risk level, only Meeting Room available";
                }

                return "Admitted to Meeting Room (high risk)";
            }

            if (_RiskLevel == RiskLevel.MEDIUM)
            {
                if (visitorRequest.VisitorType?.ToLower() == GUEST)
                {
                    return "Denied: normal risk level";
                }

                if (visitorRequest.Zone?.ToLower() == SECRET_LABORATORY)
                {
                    return "Denied: insufficient clearance level";
                }

                return "Admitted to " + visitorRequest.Zone + " (medium risk)";
            }

            if (_RiskLevel == RiskLevel.LOW)
            {
                if (visitorRequest.VisitorType?.ToLower() == GUEST)
                {
                    if (visitorRequest.Zone?.ToLower() != MEETING_ROOM)
                    {
                        return "Denied: only Meeting Room available";
                    }

                    return "Admitted to " + visitorRequest.Zone;
                }

                if ((visitorRequest.Zone?.ToLower() == LABORATORY || visitorRequest.Zone?.ToLower() == SECRET_LABORATORY) && visitorRequest.PassLevel < 2)
                {
                    return "Denied: insufficient clearance level";
                }
                if (visitorRequest.Zone?.ToLower() == SECRET_LABORATORY && visitorRequest.PassLevel < 3)
                {
                    return "Denied: insufficient clearance level";
                }
                return "Admitted to " + visitorRequest.Zone;
            }

            return "";
        }
    }

    enum RiskLevel
    {
        LOW,
        MEDIUM,
        HIGH
    }
}
