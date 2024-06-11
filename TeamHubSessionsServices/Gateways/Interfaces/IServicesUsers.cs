
using TeamHubSessionsServices.Entities;

namespace AppServiciosIdentidad.Gateways.Interfaces;

public interface IServicesUsers {
    public student? SearchUser(string email, string password);
}