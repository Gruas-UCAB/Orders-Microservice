using OrdersMicroservice.core.Common;
using OrdersMicroservice.src.extracost.domain.value_objects;
using OrdersMicroservice.src.order.application.repositories.dto;
using OrdersMicroservice.src.order.domain.entities.extraCost;

namespace OrdersMicroservice.src.order.application.repositories
{
    public interface IExtraCostRepository
    {
        Task<ExtraCost> SaveExtraCost(ExtraCost extraCost);
        Task<_Optional<List<ExtraCost>>> GetAllExtraCosts(GetAllExtraCostsDto data);
        Task<_Optional<ExtraCost>> GetExtraCostById(ExtraCostId id);
    }
}
