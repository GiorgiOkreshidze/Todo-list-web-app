using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using TodoListApp.WebApi.Models;

namespace TodoListApp.WebApp.Services;

#pragma warning disable CS8600
#pragma warning disable CS8603
#pragma warning disable IDE0059
#pragma warning disable S1075
#pragma warning disable S1854
#pragma warning disable S112

public class TodoTaskWebApiService
{
    private readonly Uri baseAddress = new Uri("https://localhost:7226/api");
    private readonly HttpClient httpClient;

    public TodoTaskWebApiService()
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

    public List<TodoTaskDto> GetTodoTasks()
    {
        List<TodoTaskDto> todoTaskDtos = new List<TodoTaskDto>();

        HttpResponseMessage response = this.httpClient.GetAsync(this.httpClient.BaseAddress + "/TodoTask/GetTodoTasks").Result;

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            todoTaskDtos = JsonConvert.DeserializeObject<List<TodoTaskDto>>(data);
        }
        else
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }

        return todoTaskDtos;
    }

    internal void CreateTodoTask(CreateTodoTaskDto data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = this.httpClient.PostAsync(this.httpClient.BaseAddress + "/TodoTask/CreateTodoTask", content).Result;

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }

    internal UpdateTodoTaskDto GetTodoTaskByIdForUpdate(long id)
    {
        HttpResponseMessage response = this.httpClient.GetAsync(this.httpClient.BaseAddress + "/TodoTask/GetTodoTaskById/" + id).Result;
        UpdateTodoTaskDto todoTaskDto = new UpdateTodoTaskDto();

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            todoTaskDto = JsonConvert.DeserializeObject<UpdateTodoTaskDto>(data);
        }
        else
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }

        return todoTaskDto;
    }

    internal void UpdateTodoTask(UpdateTodoTaskDto data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = this.httpClient.PutAsync(this.httpClient.BaseAddress + "/TodoTask/UpdateTodoTask", content).Result;

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }

    internal TodoTaskDto GetTodoTaskByIdForDelete(long id)
    {
        HttpResponseMessage response = this.httpClient.GetAsync(this.httpClient.BaseAddress + "/TodoTask/GetTodoTaskById/" + id).Result;
        TodoTaskDto todoTaskDto = new TodoTaskDto();

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            todoTaskDto = JsonConvert.DeserializeObject<TodoTaskDto>(data);
        }
        else
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }

        return todoTaskDto;
    }

    internal void Delete(long id)
    {
        HttpResponseMessage response = this.httpClient.DeleteAsync(this.httpClient.BaseAddress + "/TodoTask/DeleteTodoTask/" + id).Result;
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }

    internal IEnumerable<TodoTaskDto> GetTasksByListId(long id)
    {
        List<TodoTaskDto> todoTaskDtos = new List<TodoTaskDto>();

        HttpResponseMessage response = this.httpClient.GetAsync(this.httpClient.BaseAddress + "/TodoTask/GetTodoTasksByListId/" + id).Result;

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            todoTaskDtos = JsonConvert.DeserializeObject<List<TodoTaskDto>>(data);
        }
        else
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }

        return todoTaskDtos;
    }

    internal TodoTaskDto GetTaskById(long id)
    {
        TodoTaskDto todoTaskDtos = new TodoTaskDto();

        // I think it needs Change
        HttpResponseMessage response = this.httpClient.GetAsync(this.httpClient.BaseAddress + "/TodoTask/GetTodoTaskById/" + id).Result;

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            todoTaskDtos = JsonConvert.DeserializeObject<TodoTaskDto>(data);
        }
        else
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }

        return todoTaskDtos;
    }

    internal IEnumerable<TodoTaskFullDetailsDto> GetTodoTasksAssignedToMe()
    {
        List<TodoTaskFullDetailsDto> todoTaskDetailsDtos = new List<TodoTaskFullDetailsDto>();

        HttpResponseMessage response = this.httpClient.GetAsync(this.httpClient.BaseAddress + "/TodoTask/GetTodoTasksAssignedToMe").Result;

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            todoTaskDetailsDtos = JsonConvert.DeserializeObject<List<TodoTaskFullDetailsDto>>(data);
        }
        else
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }

        return todoTaskDetailsDtos;
    }

    internal void MakeItDone(long id)
    {
        string jsonData = JsonConvert.SerializeObject(id);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = this.httpClient.PutAsync(this.httpClient.BaseAddress + "/TodoTask/MoveToDone/" + id, content).Result;
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }


    internal List<TodoTaskCommentDto> GetComments(long id)
    {
        List<TodoTaskCommentDto> todoTaskComments = new List<TodoTaskCommentDto>();

        HttpResponseMessage response = this.httpClient.GetAsync(this.httpClient.BaseAddress + "/TodoTaskComment/GetTodoTaskCommentsByTaskId/" + id).Result;

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            todoTaskComments = JsonConvert.DeserializeObject<List<TodoTaskCommentDto>>(data);
        }
        else
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }

        return todoTaskComments;
    }

    internal void CreateComment(CreateTodoTaskCommentDto data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = this.httpClient.PostAsync(this.httpClient.BaseAddress + "/TodoTaskComment/CreateTodoTaskComment", content).Result;
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }





    internal TodoTaskCommentDto GetTodoTaskCommentByIdForDelete(long id)
    {
        HttpResponseMessage response = this.httpClient.GetAsync(this.httpClient.BaseAddress + "/TodoTaskComment/GetTodoTaskCommentById/" + id).Result;
        TodoTaskCommentDto todoTaskCommentDto = new TodoTaskCommentDto();

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            todoTaskCommentDto = JsonConvert.DeserializeObject<TodoTaskCommentDto>(data);
        }
        else
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }

        return todoTaskCommentDto;
    }

    internal void DeleteComment(long id)
    {
        HttpResponseMessage response = this.httpClient.DeleteAsync(this.httpClient.BaseAddress + "/TodoTaskComment/DeleteTodoTaskComment/" + id).Result;
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }






    internal UpdateTodoTaskCommentDto GetTodoTaskCommentByIdForUpdate(long id)
    {
        HttpResponseMessage response = this.httpClient.GetAsync(this.httpClient.BaseAddress + "/TodoTaskComment/GetTodoTaskCommentById/" + id).Result;
        UpdateTodoTaskCommentDto todoTaskCommentDto = new UpdateTodoTaskCommentDto();

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            todoTaskCommentDto = JsonConvert.DeserializeObject<UpdateTodoTaskCommentDto>(data);
        }
        else
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }

        return todoTaskCommentDto;
    }

    internal void UpdateTodoTaskComment(UpdateTodoTaskCommentDto data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = this.httpClient.PutAsync(this.httpClient.BaseAddress + "/TodoTaskComment/UpdateTodoTaskComment", content).Result;
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }

    internal List<TagsDto> GetAllTags()
    {
        HttpResponseMessage response = this.httpClient.GetAsync(this.httpClient.BaseAddress + "/TodoTask/GetAllTags/").Result;
        List<TagsDto> Tags = new List<TagsDto>();

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            Tags = JsonConvert.DeserializeObject<List<TagsDto>>(data);
        }
        else
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }

        return Tags;
    }

    internal List<TagsDto> GetTagsOfTheTask(long taskId)
    {
        HttpResponseMessage response = this.httpClient.GetAsync(this.httpClient.BaseAddress + "/TodoTask/GetTagsOfTheTask/" + taskId).Result;
        List<TagsDto> Tags = new List<TagsDto>();

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            Tags = JsonConvert.DeserializeObject<List<TagsDto>>(data);
        }
        else
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }

        return Tags;
    }

    internal void DeleteTag(long tagId, long taskId)
    {
        HttpResponseMessage response = this.httpClient.DeleteAsync(this.httpClient.BaseAddress + "/TodoTask/RemoveTagFromTheTask/" + taskId + "/" + tagId).Result;
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }

    internal void AddTag(long tagId, long taskId)
    {
        string jsonData = JsonConvert.SerializeObject(tagId);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = this.httpClient.PostAsync(this.httpClient.BaseAddress + "/TodoTask/AddTagToTheTask/" + taskId + "/" + tagId, content).Result;
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }


    internal List<TodoTaskDto> GetTasksByTag(long tagId)
    {
        HttpResponseMessage response = this.httpClient.GetAsync(this.httpClient.BaseAddress + "/TodoTask/GetTasksByTag/" + tagId).Result;
        List<TodoTaskDto> todoTasks = new List<TodoTaskDto>();

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            todoTasks = JsonConvert.DeserializeObject<List<TodoTaskDto>>(data);
        }
        else
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }

        return todoTasks;
    }

    internal IEnumerable<TodoTaskDto> FilterTasksByTagIdOrAssignedToMe(bool assignedToMe, long tagId)
    {
        HttpResponseMessage response = this.httpClient.GetAsync(this.httpClient.BaseAddress + "/TodoTask/GetFilteredTasks/" + assignedToMe + "/" + tagId).Result;
        List<TodoTaskDto> filteredTodoTasks = new List<TodoTaskDto>();

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            filteredTodoTasks = JsonConvert.DeserializeObject<List<TodoTaskDto>>(data);
        }
        else
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }

        return filteredTodoTasks;
    }
}
