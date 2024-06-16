
public static class InputValidator
{
    public static bool IsValidString(string input)
    {
        return !string.IsNullOrWhiteSpace(input) && input.All(c => !Char.IsControl(c));
    }

    public static bool IsValidId(int id)
    {
        return id > 0;
    }

    public static bool IsValidRequest(HttpContext context, out int idUserClaim, out int idSessionClaim)
    {
        idUserClaim = Int32.Parse(context.User.Claims.Where(c => c.Type == "IdUser").Select(c => c.Value).SingleOrDefault());
        idSessionClaim = Int32.Parse(context.User.Claims.Where(c => c.Type == "IdSession").Select(c => c.Value).SingleOrDefault());

        return idUserClaim > 0 && idSessionClaim > 0;
    }
}
