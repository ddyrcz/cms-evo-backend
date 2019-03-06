﻿using CMS.Core.Query;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace CMS.Cars.Application.Query.GetCars
{
    public class GetCarsQuery : IQuery
    {
        
    }

    public class GetCarsQueryResult : IQueryResult
    {
        public GetCarsQueryResult()
        {
            Cars = new List<Car>();
        }

        public List<Car> Cars { get; set; }

        public class Car
        {
            public Car(Guid id,
                string name,
                string registrationNumber,
                bool approachingExpiration)
            {
                Id = id;
                Name = name;
                RegistrationNumber = registrationNumber;
                ApproachingExpiration = approachingExpiration;
            }

            public Guid Id { get; }
            public string Name { get;}
            public string RegistrationNumber { get; }
            public bool ApproachingExpiration { get;  }
        }
    }
}
