
using AppServiciosIdentidad.Gateways.Interfaces;
using TeamHubSessionsServices.DTOs;
using TeamHubSessionsServices.Entities;
using TeamHubSessionsServices.UseCases.Interfaces;

namespace TeamHubSessionsServices.UseCases.Provider;

public class ValidateUser : IValidateUser
{
    IServicesUsers servicesUsers;
    public ValidateUser(IServicesUsers servicesusers) {
        this.servicesUsers = servicesusers;
    }

    public student? Validate(SessionLoginRequest sessionLoginRequest)
    {
        student user = servicesUsers.SearchUser(sessionLoginRequest.Email,
                                                    sessionLoginRequest.password);
        return user;
    }
}