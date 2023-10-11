namespace Application.Models.DTOs;
public class ValidationDto<T>
{
    public bool SavedSuccessfully { get; set; } = false;
    public List<string> ValidationErrors { get; set; } = new();
    public T? AffectedObject { get; set; } = default;
}
public class ValidationDto : ValidationDto<object> { }
