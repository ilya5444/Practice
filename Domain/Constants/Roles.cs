namespace WebAPI.Domain.Constants;

public static class Roles
{
    public const string Admin = "admin";
    public const string User = "user";
    public const string Anonymous = "anonymous";

    public static string GetLocalized(string role)
        => role switch
        {
            Admin => "Администратор",
            _ => "Пользователь",
        };
}
