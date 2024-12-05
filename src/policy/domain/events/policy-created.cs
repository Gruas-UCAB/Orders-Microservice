﻿using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.policy.domain.value_objects;
namespace OrdersMicroservice.src.policy.domain.events
{
    public class PolicyCreatedEvent : DomainEvent<object>
        {   
        public PolicyCreatedEvent(string dispatcherId, string name, PolicyCreated context) : base(dispatcherId, name, context){ }
    }

    public class PolicyCreated(string name, decimal monetaryCoverage, decimal kmCoverage)
    {
        public string Name = name;
        public decimal MonetaryCoverage = monetaryCoverage;
        public decimal KmCoverage = kmCoverage;
        
        static public PolicyCreatedEvent CreateEvent(PolicyId dispatcherId, PolicyName name, PolicyMonetaryCoverage monetaryCoverage, PolicyKmCoverage kmCoverage)
        {
            return new PolicyCreatedEvent(
                dispatcherId.GetId(),
                typeof(PolicyCreated).Name,
                new PolicyCreated(
                    name.GetName(),
                    monetaryCoverage.GetMonetaryCoverage(),
                    kmCoverage.GetKmCoverage()
                )
            );
        }
    }
}



