

using MongoDB.Bson;
using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.exceptions;

namespace OrdersMicroservice.src.contract.domain.value_objects;
public class   ContractExpitionDate : IValueObject<ContractExpitionDate >
{
    private readonly DateTime _expirationDate;
    private BsonDateTime asBsonDateTime;

    public ContractExpitionDate(DateTime expirationDate)
        {
            if (expirationDate > DateTime.Now) 
            {
                throw new InvalidExpirationDateException();
            }
            this._expirationDate = expirationDate;
        }

    public ContractExpitionDate(BsonDateTime asBsonDateTime)
    {
        this.asBsonDateTime = asBsonDateTime;
    }

    public DateTime GetExpirationDateContract()
        {
            return this._expirationDate ;
        }
        public bool Equals(ContractExpitionDate other)
        {
            return _expirationDate == other.GetExpirationDateContract();
        }
}
