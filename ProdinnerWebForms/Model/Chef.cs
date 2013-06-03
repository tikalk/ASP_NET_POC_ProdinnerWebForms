using System;
using System.Collections.Generic;

namespace ProdinnerWebForms.Model
{
    public class Entity
    {
        public int Id { get; set; }
    }

    public class Chef : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Country Country { get; set; }
    }

    public class Country : Entity
    {
        public string Name { get; set; }
    }

    public class Meal : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Dinner : Entity
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Chef Chef { get; set; }
        public Country Country { get; set; }
        public IEnumerable<Meal> Meals { get; set; }
    }
}


