using AutoMapper;
using MediatR;
using Sewa_Application.Contracts.Persistence;

namespace Sewa_Application.Features.Category.Queries.GetAllCategories
{
    public class GetCategoryListRequestHandler : IRequestHandler<GetCategoryListRequest, List<CategoryDto>>
    {
        private readonly IServiceTypeRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryListRequestHandler(IServiceTypeRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<CategoryDto>> Handle(GetCategoryListRequest request, CancellationToken cancellationToken)
        {
            var categoryList = await _categoryRepository.GetAllAsync();
            return _mapper.Map<List<CategoryDto>>(categoryList);
        }
    }
}
