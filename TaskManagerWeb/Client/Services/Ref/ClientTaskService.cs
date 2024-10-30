using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using TaskManager.Application.Interfaces.Services.Ref;
using TaskManager.Application.Models.Auth;
using TaskManager.Application.Models.ViewModels;
using TaskManager.Core.Models;

namespace TaskManagerWeb.Client.Services.Ref
{
  public class ClientTaskService : IClientTaskService
  {
    private readonly HttpClient _httpClient;

    public ClientTaskService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    #region Task
    public async Task CreateTask(TaskViewModel item)
    {

      var result = await _httpClient.PostAsJsonAsync("api/task/add", item);

      result.EnsureSuccessStatusCode();
    }

    public async Task UpdateTask(TaskViewModel item)
    {
      var response = await _httpClient.PostAsJsonAsync("api/task/update", item);

      response.EnsureSuccessStatusCode();
    }

    public async Task DeleteTask(TaskViewModel item)
    {
      var response = await _httpClient.PostAsJsonAsync("api/task/delete", item);

      response.EnsureSuccessStatusCode();
    }

    public async Task<IList<TaskViewModel>> GetTasks()
    {
      var response = await _httpClient.GetFromJsonAsync<IList<TaskViewModel>>("api/task/get");

      return response;
    }

    public async Task<TaskViewModel> GetTaskBy(Guid id)
    {
      var response = await _httpClient.GetFromJsonAsync<TaskViewModel>($"api/task/getby/{id}");
      
      return response;
    }
    #endregion

    #region Comment
    public async Task CreateComment(CommentViewModel item)
    {
      var result = await _httpClient.PostAsJsonAsync("api/comment/add", item);

      result.EnsureSuccessStatusCode();
    }
    #endregion
  }
}
