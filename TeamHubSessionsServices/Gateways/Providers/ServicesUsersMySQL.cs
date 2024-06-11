

using AppServiciosIdentidad.Gateways.Interfaces;
using TeamHubSessionsServices.DTOs;
using TeamHubSessionsServices.Entities;

namespace TeamHubSessionsServices.Gateways.Providers;

public class ServicesUsersMySQL : IServicesUsers
{
    private TeamHubContext dbcontext;
    public ServicesUsersMySQL(TeamHubContext context){
        this.dbcontext = context;
    }

    public student? SearchUser(string email, string password)
    {
        var student = dbcontext.student
            .FirstOrDefault(x => x.Email.Equals(email) 
            && x.Password.Equals(password));
        return student;
    }
} 