using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Models.Auth
{
  public class RegisterModel
  {
    public RegisterModel() { }  

    [Required]
    public string UserName { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают!")]
    public string PasswordConfirm { get; set; }
        public static RegisterModel New()
      => new RegisterModel()
      {
      };
    }
}
