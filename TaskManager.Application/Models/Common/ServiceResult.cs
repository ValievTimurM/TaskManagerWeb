using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Models.Common
{
  public class ServiceResult
  {
    private string? _message;
    private ServiceResult() { }

    public string Message
    {
      get => Exception?.Message ?? _message ?? string.Empty;
      private set => _message = value;
    }
    public Exception Exception { get; set; }
    public bool Success { get; set; }

    public static ServiceResult Ok() => new ServiceResult { Success = true };
    public static ServiceResult Fail(Exception ex) => new ServiceResult { Success = false, Exception = ex };
    public static ServiceResult Fail(string message) => new ServiceResult { Success = true, Message = message };
  }
}
