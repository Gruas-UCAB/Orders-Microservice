

using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.exceptions;

namespace OrdersMicroservice.src.contract.domain.value_objects;
public class   NumberContract : IValueObject<NumberContract>
{
    private readonly int _numberContract;
    public NumberContract(int numberContract)
        {
            if (numberContract < 1000 )
            {
                throw new InvalidNumberContractException();
            }
            _numberContract = numberContract;
        }
        public int GetNumberContract()
        {
            return _numberContract;
        }
        public bool Equals(NumberContract other)
        {
            return _numberContract == other.GetNumberContract();
        }
}
