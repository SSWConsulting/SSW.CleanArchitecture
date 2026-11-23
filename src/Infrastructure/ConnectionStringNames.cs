namespace SSW.CleanArchitecture.Infrastructure;

public static class ConnectionStringNames
{
    // Must match the database resource name in AppHost.cs (AddDatabase("CleanArchitecture", ...)),
    // because Aspire injects the connection string as ConnectionStrings:{resource name}.
    public const string CleanArchitecture = "CleanArchitecture";
}
