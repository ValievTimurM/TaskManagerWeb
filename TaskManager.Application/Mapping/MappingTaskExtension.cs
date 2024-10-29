using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Models.ViewModels;
using TaskManager.Core.Models;

namespace TaskManager.Application.Mapping
{
  public static class MappingTaskExtension
  {
    public static TaskModel ToAddModel(this TaskViewModel model)
      => TaskModel.New(name: model.Name,
                        desc: model.Description,
                        creator: model.Creator,
                        status: model.Status,
                        dateStart: model.DateStart,
                        planEnd: model.PlanDateEnd,
                        factSpendTime: model.FactSpendTime,
                        dateEnd: model.DateEnd,
                        dateAdd: model.DateAdd);
    public static TaskModel ToUpdateModel(this TaskViewModel model)
      => TaskModel.New(id: model.Id,
                        name: model.Name,
                        desc: model.Description,
                        creator: model.Creator,
                        status: model.Status,
                        dateStart: model.DateStart,
                        planEnd: model.PlanDateEnd,
                        factSpendTime: model.FactSpendTime,
                        dateEnd: model.DateEnd,
                        dateAdd: model.DateAdd);

    public static TaskViewModel ToViewModel(this TaskModel model)
      => TaskViewModel.New(id: model.Id,
                        name: model.Name,
                        desc: model.Description,
                        creator: model.Creator,
                        status: model.Status,
                        dateStart: model.DateStart,
                        planEnd: model.PlanDateEnd,
                        factSpendTime: model.FactSpendTime,
                        dateEnd: model.DateEnd,
                        dateAdd: model.DateAdd);
  }
}
