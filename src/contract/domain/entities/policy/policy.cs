using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.entities.policy.domain.events;
using OrdersMicroservice.src.contract.domain.entities.policy.value_objects;

namespace OrdersMicroservice.src.contract.domain.entities.policy
{
    public class Policy : Entity<PolicyId> 
    {
        private PolicyName _name;
        private PolicyMonetaryCoverage _monetaryCoverage;
        private PolicyKmCoverage _kmCoverage;

        public Policy(PolicyId id, PolicyName name, PolicyMonetaryCoverage monetaryCoverage, PolicyKmCoverage  kmCoverage) : base(id)
        {
            _name = name;
            _monetaryCoverage = monetaryCoverage;
            _kmCoverage = kmCoverage;

        }
        /*
        protected override void ValidateState()
        {
            if (_name == null || _monetaryCoverage == null || _kmCoverage == null)
            {
                throw new InvalidPolicyException();
            }
        }
        */
        public string GetId()
        {
            return _id.GetId();
        }


        public string GetName()
        {
            return _name.GetName();
        }

        public decimal GetMonetaryCoverage()
        {
            return _monetaryCoverage.GetMonetaryCoverage();
        }

        public decimal GetkmCoverage()
        {
            return _kmCoverage.GetKmCoverage();
        }


        /*
        
        public static Policy Create(PolicyId id, PolicyName name, PolicyMonetaryCoverage monetaryCoverage, PolicyKmCoverage kmCoverage)
        {
            Policy policy = new(id);
            policy.Apply(PolicyCreated.CreateEvent(id, name, monetaryCoverage, kmCoverage));
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

        private void OnPolicyNameUpdatedEvent(PolicyNameUpdated Event)
        {
            Console.WriteLine("Ya reacciono");
            _name = new PolicyName(Event.Name);
        }

        private void OnPolicyMonetaryCoverageUpdatedEvent(PolicyMonetaryCoverageUpdated Event)
        {
            Console.WriteLine("Ya reacciono");
            _monetaryCoverage = new PolicyMonetaryCoverage(Event.MonetaryCoverage);
        }

        private void OnPolicykmCoverageUpdatedEvent(PolicykmCoverageUpdated Event)
        {
            Console.WriteLine("Ya reacciono");
            _kmCoverage = new PolicyKmCoverage(Event.KmCoverage);
        }
     */ 
    }
}
