using TeamHubSessionsServices.DTOs;

namespace TeamHubSessionsServices.Gateways.Interfaces;

public interface ILogService
{
    public void SaveUserAction(UserActionDTO userAction);
}