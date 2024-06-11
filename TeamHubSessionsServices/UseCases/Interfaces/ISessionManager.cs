
using TeamHubSessionsServices.Entities;

namespace TeamHubSessionsServices.UseCases.Interfaces;

public interface ISessionManager 
{
    public studentsession? SearchCurrentSession(student student, int numberHours);
    public studentsession CreateSesion(student student, string ip, int numberHours);
    
}