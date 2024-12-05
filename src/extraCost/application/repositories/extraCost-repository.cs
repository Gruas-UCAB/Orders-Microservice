using OrdersMicroservice.core.Common;
using OrdersMicroservice.src.extracost.application.commands.update_extracos.types;
using OrdersMicroservice.src.extracost.application.commands.update_extracost.types;
using OrdersMicroservice.src.extracost.application.repositories.dto;
using OrdersMicroservice.src.extracost.domain;
using OrdersMicroservice.src.extracost.domain.value_objects;
/*using OrdersMicroservice.src.extracost.infrastructure.dto;*/


namespace OrdersMicroservice.src.extracost.application.repositories
{
    public interface IExtraCostRepository
    {
        public Task<ExtraCost> SaveExtraCost(ExtraCost extraCost);
        public Task<_Optional<List<ExtraCost>>> GetAllExtraCosts();
        public Task<_Optional<ExtraCost>> GetExtraCostById(ExtraCostId id);
        public Task<ExtraCostId> UpdateExtraCostById(UpdateExtraCostByIdCommand command);
        public Task<ExtraCostId> ToggleActivityExtraCostById(ExtraCostId id);

    }
}
