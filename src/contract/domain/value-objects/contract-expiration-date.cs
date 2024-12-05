/*

using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.exceptions;

namespace OrdersMicroservice.src.contract.domain.value_objects;
public class   PolicyContract : IValueObject<PolicyContract >
{
    private readonly Vehicle _vehicleContract;
    public NumberContract(decimal numberContract)
        {
            if (vehicleContract < 0 )
            {
                throw new InvalidNumberContractException();
            }
            this._vehicleContract = vehicleContract;
        }
        public decimal GetVehicleContract()
        {
            return this._vehicleContract ;
        }
        public bool Equals(VehicleContract other)
        {
            return _vehicleContract == other.GetVehicleContract();
        }
}
*/