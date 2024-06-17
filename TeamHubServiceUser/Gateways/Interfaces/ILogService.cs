using TeamHubServiceUser.DTOs;

namespace TeamHubServiceUser.Gateways.Interfaces;

public interface ILogService
{
    public int SaveUserAction(UserActionDTO userAction);
}