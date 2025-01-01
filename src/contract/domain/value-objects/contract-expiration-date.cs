using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.exceptions;

namespace OrdersMicroservice.src.contract.domain.value_objects;
public class   ContractExpitionDate : IValueObject<ContractExpitionDate >
{
    private readonly DateTime _expirationDate;

    public ContractExpitionDate(DateTime expirationDate)
        {
            if (expirationDate < DateTime.Now) 
            {
                throw new InvalidExpirationDateException();
            }
            _expirationDate = expirationDate;
        }
    public DateTime GetExpirationDateContract()
        {
            return _expirationDate ;
        }
        public bool Equals(ContractExpitionDate other)
        {
            return _expirationDate == other.GetExpirationDateContract();
        }
}
