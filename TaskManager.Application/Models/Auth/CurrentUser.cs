using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Models.Auth
{
  public class CurrentUser
  {
    public CurrentUser() { }
    public bool IsAuthenticated { get; set; }
    public string? UserName { get; set; }
    public Dictionary<string, string>? Claims { get; set; }
    public static CurrentUser New(bool isAuth, string? userName, Dictionary<string, string>? claims)
      => new CurrentUser()
      {
        IsAuthenticated = isAuth,
        UserName = userName,
        Claims = claims
      };
    
  }
}
