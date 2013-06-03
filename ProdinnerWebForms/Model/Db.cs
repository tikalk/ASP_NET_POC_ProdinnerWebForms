using System;
using System.Collections.Generic;
using System.Linq;
using Omu.ValueInjecter;

namespace ProdinnerWebForms.Model
{
    public static class Db
    {
        public static IList<Country> Countries = new List<Country>();
        public static IList<Chef> Chefs = new List<Chef>();
        public static IList<Meal> Meals = new List<Meal>();
        public static IList<Dinner> Dinners = new List<Dinner>();


        public static int Gid;


        public static object Set<T>()
        {
            if (typeof(T) == typeof(Country)) return Countries;
            if (typeof(T) == typeof(Chef)) return Chefs;
            if (typeof(T) == typeof(Meal)) return Meals;
            if (typeof(T) == typeof(Dinner)) return Dinners;
            return null;
        }

        public static IDictionary<Type, IList<object>> Tables = new Dictionary<Type, IList<object>>
                                                                    {
                                                                        { typeof(Country), Countries.Cast<object>().ToList() },
                                                                        { typeof(Chef), Chefs.Cast<object>().ToList() },
                                                                        { typeof(Meal), Meals.Cast<object>().ToList() },
                                                                        { typeof(Dinner), Dinners.Cast<object>().ToList() },
                                                                    };


        public static void Insert<T>(T o) where T : Entity
        {
            o.Id = ++Gid;
            ((IList<T>)Set<T>()).Add(o);
        }

        public static T Get<T>(int id) where T : Entity
        {
            return ((IList<T>)Set<T>()).SingleOrDefault(o => o.Id == id);
        }

        public static void Update<T>(T o) where T : Entity
        {
            var t = Get<T>(o.Id);
            t.InjectFrom(o);
        }

        public static void Delete<T>(int id) where T : Entity
        {
            ((IList<T>)Set<T>()).Remove(Get<T>(id));
        }

        static Db()
        {
            Insert(new Country { Name = "Elwynn Forest" });
            Insert(new Country { Name = "Stormwind" });
            Insert(new Country { Name = "Loch Modan" });
            Insert(new Country { Name = "Redridge Mountains" });
            Insert(new Country { Name = "Westfall" });
            Insert(new Country { Name = "Ironforge" });
            Insert(new Country { Name = "Orgrimmar" });
            Insert(new Country { Name = "Feralas" });
            Insert(new Country { Name = "Darnassus" });
            Insert(new Country { Name = "Teldrassil" });
            Insert(new Country { Name = "Winterspring" });
            Insert(new Country { Name = "Goldshire" });
            Insert(new Country { Name = "Sylvanaar" });

            Insert(new Chef { FirstName = "Naked", LastName = "Chef", Country = Countries[Rnd(Countries)] });
            Insert(new Chef { FirstName = "Chef", LastName = "Chef", Country = Countries[Rnd(Countries)] });
            Insert(new Chef { FirstName = "Athene", LastName = "Broccoli", Country = Countries[Rnd(Countries)] });
            Insert(new Chef { FirstName = "Omu", LastName = "Man", Country = Countries[Rnd(Countries)] });
            Insert(new Chef { FirstName = "Pepper", LastName = "Tomato", Country = Countries[Rnd(Countries)] });

            Insert(new Meal { Name = "Banana", Description = "Wonderfully sweet with firm and creamy flesh, bananas come prepackaged in their own yellow jackets and are available for harvest throughout the year" });
            Insert(new Meal { Name = "Orange", Description = "Nutrients in orange fruit are plentiful and diverse. The fruit is low in calories, contains no saturated fats or cholesterol, but rich in anti-oxidants" });
            Insert(new Meal { Name = "Kiwi", Description = "kiwi fruits contain about as much potassium as bananas. It is also rich in Vitamins A, C and E" });
            Insert(new Meal { Name = "Blueberry", Description = "Wild Blueberries are the superfruit that packs a healthy punch. Rich in phytonutrients – antioxidants such anthrocyanin – as well as anti-inflammatories" });
            Insert(new Meal { Name = "Carrot", Description = "Carrots are nutritional heroes, they store a goldmine of nutrients. No other vegetable or fruit contains as much carotene as carrots, which the body converts to ..." });
            Insert(new Meal { Name = "Lemon", Description = "The lemon is both a small evergreen tree native to Asia, and the tree's ellipsoidal yellow fruit. The fruit is used for culinary and non-culinary purposes throughout the world" });
            Insert(new Meal { Name = "Pomegranate", Description = "Pomegranates have been cherished for their exquisite beauty, flavor, color, and health benefits for centuries. From their distinctive crown to their ruby red arils" });
            Insert(new Meal { Name = "Mango", Description = "Mango is one of the delicious tropical seasonal fruit and believed to be originated in the sub-Himalayan plains of Indian subcontinent" });
            Insert(new Meal { Name = "Apple", Description = "The science of apple growing is called pomology. Apple trees take four to five years to produce their first fruit. Most apples are still picked by hand in the fall. ..." });
            Insert(new Meal { Name = "Cranberries", Description = "Among the fruits and vegetables richest in health-promoting antioxidants berries such as cranberries rank right up there at the top of the list" });

            Insert(new Dinner { Country = Countries[Rnd(Countries)], Chef = Chefs[Rnd(Chefs)], Meals = RndMeals(), Date = RndDate(), Name = "dinner with friends" });
            Insert(new Dinner { Country = Countries[Rnd(Countries)], Chef = Chefs[Rnd(Chefs)], Meals = RndMeals(), Date = RndDate(), Name = "eating at the pub" });
            Insert(new Dinner { Country = Countries[Rnd(Countries)], Chef = Chefs[Rnd(Chefs)], Meals = RndMeals(), Date = RndDate(), Name = "cooking in the garden" });
            Insert(new Dinner { Country = Countries[Rnd(Countries)], Chef = Chefs[Rnd(Chefs)], Meals = RndMeals(), Date = RndDate(), Name = "ninja dinner" });
            Insert(new Dinner { Country = Countries[Rnd(Countries)], Chef = Chefs[Rnd(Chefs)], Meals = RndMeals(), Date = RndDate(), Name = "broccoli power" });
            Insert(new Dinner { Country = Countries[Rnd(Countries)], Chef = Chefs[Rnd(Chefs)], Meals = RndMeals(), Date = RndDate(), Name = "eating out" });
            Insert(new Dinner { Country = Countries[Rnd(Countries)], Chef = Chefs[Rnd(Chefs)], Meals = RndMeals(), Date = RndDate(), Name = "Awesome dinner" });
            Insert(new Dinner { Country = Countries[Rnd(Countries)], Chef = Chefs[Rnd(Chefs)], Meals = RndMeals(), Date = RndDate(), Name = "Uber dinner" });
            Insert(new Dinner { Country = Countries[Rnd(Countries)], Chef = Chefs[Rnd(Chefs)], Meals = RndMeals(), Date = RndDate(), Name = "Fruity dish" });
            Insert(new Dinner { Country = Countries[Rnd(Countries)], Chef = Chefs[Rnd(Chefs)], Meals = RndMeals(), Date = RndDate(), Name = "Nice dish" });
            Insert(new Dinner { Country = Countries[Rnd(Countries)], Chef = Chefs[Rnd(Chefs)], Meals = RndMeals(), Date = RndDate(), Name = "Nerds eating" });
            Insert(new Dinner { Country = Countries[Rnd(Countries)], Chef = Chefs[Rnd(Chefs)], Meals = RndMeals(), Date = RndDate(), Name = "Pros eating" });
            Insert(new Dinner { Country = Countries[Rnd(Countries)], Chef = Chefs[Rnd(Chefs)], Meals = RndMeals(), Date = RndDate(), Name = "hungry men" });
            Insert(new Dinner { Country = Countries[Rnd(Countries)], Chef = Chefs[Rnd(Chefs)], Meals = RndMeals(), Date = RndDate(), Name = "snacks and movie" });
            Insert(new Dinner { Country = Countries[Rnd(Countries)], Chef = Chefs[Rnd(Chefs)], Meals = RndMeals(), Date = RndDate(), Name = "fruit salad" });
            Insert(new Dinner { Country = Countries[Rnd(Countries)], Chef = Chefs[Rnd(Chefs)], Meals = RndMeals(), Date = RndDate(), Name = "Morning meal" });
            Insert(new Dinner { Country = Countries[Rnd(Countries)], Chef = Chefs[Rnd(Chefs)], Meals = RndMeals(), Date = RndDate(), Name = "Morning Tea" });
            Insert(new Dinner { Country = Countries[Rnd(Countries)], Chef = Chefs[Rnd(Chefs)], Meals = RndMeals(), Date = RndDate(), Name = "Cookies and milk" });

        }
        private static readonly Random R = new Random();
        private static int Rnd<T>(ICollection<T> list)
        {
            return R.Next(0, list.Count);
        }

        private static DateTime RndDate()
        {
            return new DateTime(R.Next(2009, 2013), R.Next(1, 12), R.Next(1, 28));
        }

        private static IEnumerable<Meal> RndMeals()
        {
            var list = new List<Meal>();
            var x = R.Next(1, 9);
            for (int i = 0; i < x; i++)
            {
                list.Add(Meals[R.Next(Meals.Count - 1)]);
            }
            return list.Distinct();
        }
    }
}