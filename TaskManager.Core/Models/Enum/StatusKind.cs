using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Models.Enum
{
  public enum  StatusKind
  {
    /// <summary>
    /// Создана
    /// </summary>
    Created,
    /// <summary>
    /// В работе
    /// </summary>
    InProcess,
    /// <summary>
    /// Завершена
    /// </summary>
    Finished
  }

  public static class TaskStatusKindExtension
  {
    public static string ToDescription(this StatusKind obj)
      => obj switch
      {
        StatusKind.Created => "Задача создана",
        StatusKind.Finished => "Задача завершена",
        StatusKind.InProcess => "Задача в работе",
        _ => "Неизвестный статус задачи"
      };
  }
}
