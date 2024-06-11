
using TeamHubSessionsServices.Entities;

namespace TeamHubSessionsServices.Gateways.Interfaces;

public interface ITokenGenerator 
{
    public string GenerateToken(student studentDTO, int numberHours, int IdSession);
}