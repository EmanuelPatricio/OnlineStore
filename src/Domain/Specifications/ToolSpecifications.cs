using Domain.Core.Specifications;
using Domain.Entities;

namespace Domain.Specifications;
public static class ToolSpecifications
{
    public static BaseSpecification<Tool> GetToolByIdSpec(Guid id)
    {
        return new BaseSpecification<Tool>(x => x.Id == id);
    }
    public static BaseSpecification<Tool> GetAllToolsSpec()
    {
        return new BaseSpecification<Tool>(x => !string.IsNullOrWhiteSpace(x.Name));
    }
}