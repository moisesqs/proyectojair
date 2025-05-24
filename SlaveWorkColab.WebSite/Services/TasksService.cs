using Newtonsoft.Json;
using SlaveWorkColab.Core.Dto;
using SlaveWorkColab.Core.Http;
using SlaveWorkColab.WebSite.Services.Interfaces;

namespace SlaveWorkColab.WebSite.Services;

public class TasksService : ITasksService
{
    private readonly string _baseUrl = "http://localhost:5140/";
    private readonly string _endpoint = "api/Tasks";

        
    public TasksService()
    {
        
    }
    
    public async Task<Response<List<TasksDto>>> GetAllAsync()
    {
        var url = $"{_baseUrl}{_endpoint}";
        
        var client = new HttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<List<TasksDto>>>(json);
        
        return response;
    }

    public async Task<Response<TasksDto>> GetByIdAsync(int id)
    {
        var url = $"{_baseUrl}{_endpoint}/{id}";
        
        var client = new HttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<TasksDto>>(json);

        return response;
    }

    public async Task<Response<TasksDto>> SaveAsync(TasksDto task)
    {
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(task);
        var content  = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<TasksDto>>(json);
        return response;
    }

    public async Task<Response<TasksDto>> UpdateAsync(TasksDto task)
    {
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(task);
        var content  = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PutAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<TasksDto>>(json);
        return response;
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var  url = $"{_baseUrl}{_endpoint}/{id}";
        var client = new HttpClient();
        var res = await client.DeleteAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<bool>>(json);
        
        return response;
    }
}