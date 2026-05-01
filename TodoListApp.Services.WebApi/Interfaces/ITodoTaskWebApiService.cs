using TodoListApp.WebApi.Models;

namespace TodoListApp.Services.WebApi.Interfaces;

public interface ITodoTaskWebApiService
{
    void SetBearerToken(string token);
    List<TodoTaskDto> GetTodoTasks();
    void CreateTodoTask(CreateTodoTaskDto data);
    UpdateTodoTaskDto GetTodoTaskByIdForUpdate(long id);
    void UpdateTodoTask(UpdateTodoTaskDto data);
    TodoTaskDto GetTodoTaskByIdForDelete(long id);
    void Delete(long id);

    IEnumerable<TodoTaskDto> GetTasksByListId(long id);
    TodoTaskDto GetTaskById(long id);

    IEnumerable<TodoTaskFullDetailsDto> GetTodoTasksAssignedToMe();
    void MakeItDone(long id);
    List<TodoTaskCommentDto> GetComments(long id);

    void CreateComment(CreateTodoTaskCommentDto data);

    TodoTaskCommentDto GetTodoTaskCommentByIdForDelete(long id);

    void DeleteComment(long id);

    UpdateTodoTaskCommentDto GetTodoTaskCommentByIdForUpdate(long id);

    void UpdateTodoTaskComment(UpdateTodoTaskCommentDto data);

    List<TagsDto> GetAllTags();

    List<TagsDto> GetTagsOfTheTask(long taskId);

    void DeleteTag(long tagId, long taskId);

    void AddTag(long tagId, long taskId);

    List<TodoTaskDto> GetTasksByTag(long tagId);

    IEnumerable<TodoTaskDto> FilterTasksByTagIdOrAssignedToMe(bool assignedToMe, long tagId);

}
