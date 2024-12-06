/*using MongoDB.Bson.Serialization;*/
using OrdersMicroservice.core.Application;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;

using OrdersMicroservice.src.policy.application.repositories;
using OrdersMicroservice.src.policy.application.repositories.exceptions;
using OrdersMicroservice.src.policy.application.commands.update_policy.types;


namespace OrdersMicroservice.src.policy.application.commands.update_user
{
    public class UpdatePolicyByIdCommandHandler(IPolicyRepository policyRepository) : IApplicationService<UpdatePolicyByIdCommand, UpdatePolicyByIdResponse>
    {
        private readonly IPolicyRepository _policyRepository = policyRepository;
        public async Task<Result<UpdatePolicyByIdResponse>> Execute(UpdatePolicyByIdCommand data)
        {
            try
            {
                var policy = await _policyRepository.GetPolicyById(new domain.value_objects.PolicyId(data.Id));
                if (!policy.HasValue())
                {
                    return Result<UpdatePolicyByIdResponse>.Failure(new PolicyNotFoundException());
                }

                var policyToUpdate = policy.Unwrap();
                if (data.Name != null)
                {
                    policyToUpdate.UpdateName(new domain.value_objects.PolicyName(data.Name));
                    Console.WriteLine("Cambio nombre");
                }

                if (data.KmCoverage != null)
                {
                    policyToUpdate.UpdateKmCoverage(new domain.value_objects.PolicyKmCoverage(data.KmCoverage));
                    Console.WriteLine("Cambio KM");
                }

                if (data.MonetaryCoverage != null)
                {
                    policyToUpdate.UpdateMonetaryCoverage(new domain.value_objects.PolicyMonetaryCoverage(data.MonetaryCoverage));
                    Console.WriteLine("Cambio MO");
                }
                ///los otros update

                await _policyRepository.UpdatePolicyById(data);

                return Result<UpdatePolicyByIdResponse>.Success(new UpdatePolicyByIdResponse(policyToUpdate.GetId()));
            }
            catch (Exception e) 
            {
                return Result<UpdatePolicyByIdResponse>.Failure(e);
            }
        }
    }
}
