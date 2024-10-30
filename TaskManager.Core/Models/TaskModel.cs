using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskManager.Core.Extension;
using TaskManager.Core.Models.Common;
using TaskManager.Core.Models.Enum;

namespace TaskManager.Core.Models
{
  public class TaskModel : EntityBase
  {
    private TaskModel(Guid id) : base(id) { }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Creator { get; set; }
    public IList<Comment> Comments { get; set; } = new List<Comment>();
    public StatusKind Status { get; set; } = StatusKind.Created;
    public DateTime? DateStart { get; set; }
    public DateTime? PlanDateEnd { get; set; }
    public TimeSpan FactSpendTime { get; set; }
    public DateTime? DateEnd { get; set; }
    public DateTime? DateAdd { get; set; }

    public static TaskModel New(string creator)
      => new TaskModel(Guid.NewGuid()) { 
        DateAdd = DateTime.Now
      };
    public static TaskModel New(string name, 
                                string desc,
                                string creator,
                                StatusKind status,
                                DateTime? dateStart,
                                DateTime? planEnd,
                                TimeSpan factSpendTime,
                                DateTime? dateEnd,
                                DateTime? dateAdd)
      => new TaskModel(Guid.NewGuid())
      {
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

    public static TaskModel New(Guid id,
                                string name,
                                string desc,
                                string creator,
                                StatusKind status,
                                DateTime? dateStart,
                                DateTime? planEnd,
                                TimeSpan factSpendTime,
                                DateTime? dateEnd,
                                DateTime? dateAdd)
      => new TaskModel(id)
      {
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

    public void SyncProperties(TaskModel item)
    {
      Name = item.Name;
      Description = item.Description;
      Status = item.Status;
      DateStart = item.DateStart;
      PlanDateEnd = item.PlanDateEnd;
      FactSpendTime = item.FactSpendTime;
      DateEnd = item.DateEnd;
		}

    
  }
}
