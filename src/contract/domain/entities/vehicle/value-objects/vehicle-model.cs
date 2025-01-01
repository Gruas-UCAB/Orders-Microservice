using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions;


namespace OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;

public class VehicleModel : IValueObject<VehicleModel>
{
    private string _model;
    public VehicleModel(string model)
    {
        if (string.IsNullOrEmpty(model) || model.Length > 30 || model.Length < 2)
        {
            throw new InvalidVehicleModelException();
        }
        _model = model;
    }
    public string GetModel()
    {
        return _model;
    }   
    public bool Equals(VehicleModel other)
    {
        return other.GetModel() == _model;
    }
}


