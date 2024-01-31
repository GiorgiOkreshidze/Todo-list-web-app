using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using TodoListApp.WebApi.Models;

namespace TodoListApp.WebApp.Services;

#pragma warning disable CS8600
#pragma warning disable CS8603
#pragma warning disable S112
#pragma warning disable S1075

public class TodoListWebApiService
{
    private readonly Uri baseAddress = new Uri("https://localhost:7226/api");
    private readonly HttpClient httpClient;

    public TodoListWebApiService()
    {
        this.httpClient = new HttpClient
        {
            BaseAddress = this.baseAddress
        };
    }

    public void SetBearerToken(string token)
    {
        this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public List<TodoListDto> GetTodoLists()
    {
        List<TodoListDto> todoListDtos;

        HttpResponseMessage response = this.httpClient.GetAsync(this.httpClient.BaseAddress + "/TodoList/GetTodoLists").Result;

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            todoListDtos = JsonConvert.DeserializeObject<List<TodoListDto>>(data);
        }
        else
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }

        return todoListDtos;
    }

    internal void CreateTodoList(TodoListDto data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = this.httpClient.PostAsync(this.httpClient.BaseAddress + "/TodoList/CreateTodoList", content).Result;

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }

    internal UpdateTodoListDto GetTodoListByIdForUpdate(long id)
    {
        HttpResponseMessage response = this.httpClient.GetAsync(this.httpClient.BaseAddress + "/TodoList/GetTodoListById/" + id).Result;
        UpdateTodoListDto todoListDto;

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            todoListDto = JsonConvert.DeserializeObject<UpdateTodoListDto>(data);
        }
        else
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }

        return todoListDto;
    }

    internal TodoListDto GetTodoListByIdForDelete(long id)
    {
        HttpResponseMessage response = this.httpClient.GetAsync(this.httpClient.BaseAddress + "/TodoList/GetTodoListById/" + id).Result;
        TodoListDto todoListDto;

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            todoListDto = JsonConvert.DeserializeObject<TodoListDto>(data);
        }
        else
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }

        return todoListDto;
    }

    internal void UpdateTodoList(UpdateTodoListDto data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = this.httpClient.PutAsync(this.httpClient.BaseAddress + "/TodoList/UpdateTodoList", content).Result;

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }

    internal void Delete(long id)
    {
        HttpResponseMessage response = this.httpClient.DeleteAsync(this.httpClient.BaseAddress + "/TodoList/DeleteTodoList/" + id).Result;

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }
}
