using System.ComponentModel.DataAnnotations;

namespace TodoListApp.Services.Database;
public class TodoListEntity
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public ICollection<EditorEntity> Editors { get; set; } = new List<EditorEntity>();

    public ICollection<TodoTaskEntity> TodoTaskEntities { get; set; } = new List<TodoTaskEntity>();

}
