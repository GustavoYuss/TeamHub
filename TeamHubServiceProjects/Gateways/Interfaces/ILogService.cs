using TeamHubServiceProjects.DTOs;

namespace TeamHubServiceProjects.Gateways.Interfaces;

public interface ILogService
{
    public void SaveUserAction(UserActionDTO userAction);
}