
using TeamHubSessionsServices.DTOs;
using TeamHubSessionsServices.Entities;

namespace TeamHubSessionsServices.UseCases.Interfaces;

public interface IValidateUser
{
    public student? Validate(SessionLoginRequest sessionLoginRequest);
}