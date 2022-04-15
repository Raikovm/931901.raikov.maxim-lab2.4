using System.Collections.Generic;

namespace lab4.Models;

public class AccountsBase
{
    private static readonly AccountsBase Instance = new();
    private readonly Dictionary<string, AuthModel> accounts = new ();
    public static Dictionary<string, AuthModel> Get() => 
        Instance.accounts;
}