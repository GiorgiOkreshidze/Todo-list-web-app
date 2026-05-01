using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using TodoListApp.Services.WebApi.Interfaces;
using TodoListApp.WebApi.Models;

namespace TodoListApp.Services.WebApi.Services;

public class TodoTaskWebApiService : ITodoTaskWebApiService
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

    public void CreateTodoTask(CreateTodoTaskDto data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = this.httpClient.PostAsync(this.httpClient.BaseAddress + "/TodoTask/CreateTodoTask", content).Result;

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }

    public UpdateTodoTaskDto GetTodoTaskByIdForUpdate(long id)
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

    public void UpdateTodoTask(UpdateTodoTaskDto data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = this.httpClient.PutAsync(this.httpClient.BaseAddress + "/TodoTask/UpdateTodoTask", content).Result;

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }

    public TodoTaskDto GetTodoTaskByIdForDelete(long id)
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

    public void Delete(long id)
    {
        HttpResponseMessage response = this.httpClient.DeleteAsync(this.httpClient.BaseAddress + "/TodoTask/DeleteTodoTask/" + id).Result;
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }

    public IEnumerable<TodoTaskDto> GetTasksByListId(long id)
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

    public TodoTaskDto GetTaskById(long id)
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

    public IEnumerable<TodoTaskFullDetailsDto> GetTodoTasksAssignedToMe()
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

    public void MakeItDone(long id)
    {
        string jsonData = JsonConvert.SerializeObject(id);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = this.httpClient.PutAsync(this.httpClient.BaseAddress + "/TodoTask/MoveToDone/" + id, content).Result;
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }


    public List<TodoTaskCommentDto> GetComments(long id)
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

    public void CreateComment(CreateTodoTaskCommentDto data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = this.httpClient.PostAsync(this.httpClient.BaseAddress + "/TodoTaskComment/CreateTodoTaskComment", content).Result;
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }





    public TodoTaskCommentDto GetTodoTaskCommentByIdForDelete(long id)
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

    public void DeleteComment(long id)
    {
        HttpResponseMessage response = this.httpClient.DeleteAsync(this.httpClient.BaseAddress + "/TodoTaskComment/DeleteTodoTaskComment/" + id).Result;
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }






    public UpdateTodoTaskCommentDto GetTodoTaskCommentByIdForUpdate(long id)
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

    public void UpdateTodoTaskComment(UpdateTodoTaskCommentDto data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = this.httpClient.PutAsync(this.httpClient.BaseAddress + "/TodoTaskComment/UpdateTodoTaskComment", content).Result;
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }

    public List<TagsDto> GetAllTags()
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

    public List<TagsDto> GetTagsOfTheTask(long taskId)
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

    public void DeleteTag(long tagId, long taskId)
    {
        HttpResponseMessage response = this.httpClient.DeleteAsync(this.httpClient.BaseAddress + "/TodoTask/RemoveTagFromTheTask/" + taskId + "/" + tagId).Result;
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }

    public void AddTag(long tagId, long taskId)
    {
        string jsonData = JsonConvert.SerializeObject(tagId);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = this.httpClient.PostAsync(this.httpClient.BaseAddress + "/TodoTask/AddTagToTheTask/" + taskId + "/" + tagId, content).Result;
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }


    public List<TodoTaskDto> GetTasksByTag(long tagId)
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

    public IEnumerable<TodoTaskDto> FilterTasksByTagIdOrAssignedToMe(bool assignedToMe, long tagId)
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
