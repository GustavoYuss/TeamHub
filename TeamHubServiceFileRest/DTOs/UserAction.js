
class UserActionDTO{
    constructor(idUser, idSession, action) {
        this.IdUser = idUser;
        this.IdSession = idSession;
        this.Action = action;
      }
}

module.exports  = UserActionDTO;