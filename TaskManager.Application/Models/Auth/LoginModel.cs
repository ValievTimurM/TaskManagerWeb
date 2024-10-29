using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Models.Auth
{
  public class LoginModel
  {
    public LoginModel() { }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
    public bool RememberMe { get; set; }

    public static LoginModel New(string userName, string password)
      => new LoginModel()
      {
        UserName = userName,
        Password = password,
      };

    public static LoginModel New()
      => new LoginModel()
      {
      };

  }
}
