using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Helpers.Extension
{
  public static class TaskExtension
  {
    public static string FormatToString(this TimeSpan timeSpan)
    {
      var parts = new List<string>();

      if (timeSpan.Days > 0)
        parts.Add($"{timeSpan.Days} дней");
      if (timeSpan.Hours > 0)
        parts.Add($"{timeSpan.Hours} часов");
      if (timeSpan.Minutes > 0)
        parts.Add($"{timeSpan.Minutes} минут");

      return string.Join(", ", parts);
    }
  }
}
