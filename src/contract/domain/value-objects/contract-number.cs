
/*
using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.exceptions;

namespace OrdersMicroservice.src.contract.domain.value_objects;
public class   NumberContract : IValueObject<NumberContract>
{
    private readonly decimal _numberContract;
    public NumberContract(decimal numberContract)
        {
            if (numberContract < 0 )
            {
                throw new InvalidNumberContractException();
            }
            this._numberContract = numberContract;
        }
        public decimal GetNumberContract()
        {
            return this._numberContract ;
        }
        public bool Equals(NumberContract other)
        {
            return _numberContract == other.GetNumberContract();
        }
}
*/