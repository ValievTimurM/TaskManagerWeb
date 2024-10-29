using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models.Enum;
using TaskManager.Core.Models;
using TaskManager.Application.Helpers.Extension;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Application.Models.ViewModels
{
  public class TaskViewModel
  {
    public TaskViewModel() { }
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Это поле не может быть пустым!")]
    [StringLength(250, ErrorMessage = "Количество символов не может превышать 250!", MinimumLength = 2)]
    public string Name { get; set; } = "";
    [Required(ErrorMessage = "Это поле не может быть пустым!")]
    [StringLength(250, ErrorMessage = "Количество символов не может превышать 250!", MinimumLength = 2)]
    public string Description { get; set; } = "";
    public string Creator { get; set; }
    public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
    public string DisplayStatus { get { return Status.ToDescription(); } private set{} }
    public StatusKind Status { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? PlanDateEnd { get; set; }
    public TimeSpan FactSpendTime { get { return CalculateFactSpendTime(); } private set { } }
    public string DisplayFactSpendTime { get { return FactSpendTime.FormatToString(); }  set { } }
    public DateTime? DateEnd { get; set; }
    public DateTime? DateAdd { get; set; }

    public static TaskViewModel New(string? creator)
      => new TaskViewModel()
      {
        DateAdd = DateTime.Now,
        Creator = creator ?? ""
      };

    public static TaskViewModel New(Guid id,
                                    string name,
                                    string desc,
                                    string creator,
                                    StatusKind status,
                                    DateTime? dateStart,
                                    DateTime? planEnd,
                                    TimeSpan factSpendTime,
                                    DateTime? dateEnd,
                                    DateTime? dateAdd)
      => new TaskViewModel()
      {
        Id = id,
        Name = name,
        Description = desc,
        Creator = creator,
        Status = status,
        DateStart = dateStart,
        PlanDateEnd = planEnd,
        FactSpendTime = factSpendTime,
        DateEnd = dateEnd,
        DateAdd = dateAdd
      };

    public TimeSpan CalculateFactSpendTime()
    {
      if (DateEnd != null && DateStart != null)
      {
        return (TimeSpan)(DateEnd - DateStart);
      }
      else if(DateEnd == null && DateStart != null)
      {
        return (TimeSpan)(DateTime.Now - DateStart);
      }
      return TimeSpan.Zero;
    }
  }
}
