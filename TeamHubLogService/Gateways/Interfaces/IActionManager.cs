using TeamHubLogService.DTOs;

namespace TeamHubLogService.Gateways.Interfaces;

public interface IActionManager
{
    void SaveUserAction(UserActionDTO userAction);
}