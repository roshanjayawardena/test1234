using MediatR;

namespace Sewa_Application.Features.Category.Queries.GetAllCategories
{
    public class GetCategoryListRequest: IRequest<List<CategoryDto>>
    {
    }
}
