using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.policy.domain.value_objects;
using OrdersMicroservice.src.policy.domain.events;
using OrdersMicroservice.src.policy.domain.exceptions;



namespace OrdersMicroservice.src.policy.domain
{
    public class Policy(PolicyId id) : AggregateRoot<PolicyId>(id)
    {
        private PolicyName _name;
        private PolicyMonetaryCoverage _monetaryCoverage;
        private PolicyKmCoverage _kmCoverage;
        
        
        protected override void ValidateState()
        {
            if (_name == null || _monetaryCoverage == null || _kmCoverage== null )
            {
                throw new InvalidPolicyException();
            }
        }

        public string GetId()
        {
            return _id.GetId();
        }

    
        public  string GetName()
        {
            return _name.GetName();
        }
        
        public string GetMonetaryCoverage()
        {
            return _monetaryCoverage.GetMonetaryCoverage();
        }

        public string GetkmCoverage()
        {
            return _kmCoverage.GetKmCoverage();
        }

    


        public static Policy Create(PolicyId id, PolicyName name,   PolicyMonetaryCoverage monetaryCoverage, PolicyKmCoverage kmCoverage)
        {
            Policy policy = new(id);
            policy.Apply( PolicyCreated.CreateEvent(id, name, monetaryCoverage, kmCoverage)   );
            return policy;
           
        }

        public void UpdateName(PolicyName name)
        {
            Apply(PolicyNameUpdated.CreateEvent(_id, name));
            Console.WriteLine("Ya aplico");
        }

        public void UpdateMonetaryCoverage(PolicyMonetaryCoverage monetaryCoverage)
        {
            Apply(PolicyMonetaryCoverageUpdated.CreateEvent(_id, monetaryCoverage));
            Console.WriteLine("Ya aplico");
        }

        public void UpdateKmCoverage(PolicyKmCoverage kmCoverage)
        {
            Apply(PolicykmCoverageUpdated.CreateEvent(_id, kmCoverage));
            Console.WriteLine("Ya aplico");
        }
    
        private void OnPolicyCreatedEvent(PolicyCreated Event)
        {
            _name = new PolicyName(Event.Name);
            _monetaryCoverage = new PolicyMonetaryCoverage(Event.MonetaryCoverage);
            _kmCoverage = new PolicyKmCoverage(Event.KmCoverage);
            
        }

        private void OnPolicyNameUpdatedEvent( PolicyNameUpdated Event)
        {
            Console.WriteLine("Ya reacciono");
            _name = new PolicyName(Event.Name);
        }

    }
}
