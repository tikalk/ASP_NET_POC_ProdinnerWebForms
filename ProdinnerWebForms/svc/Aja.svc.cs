using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web;
using Omu.AwesomeWebForms;
using ProdinnerWebForms.Model;
using Resources;

namespace ProdinnerWebForms.svc
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Aja
    {
        #region Meals MultiLookup
        [WebGet]
        [OperationContract]
        public IEnumerable<KeyContent> MealGetMultiple(string v)
        {
            var ids = v.GetIntArray();
            return Db.Meals.Where(o => ids.Contains(o.Id)).Select(f => new KeyContent(f.Id, f.Name));
        }

        [WebGet]
        [OperationContract]
        public LookupResult MealSearch(string search, string selected, int page)
        {
            var sel = selected.GetIntArray();

            var items = Db.Meals.Where(o => o.Name.ToLower().Contains(search.ToLower()) && !sel.Contains(o.Id))
                .Select(f => new KeyContent { Key = f.Id, Content = f.Name });

            const int pageSize = 5;
            return new LookupResult
            {
                Items = items.Skip((page - 1) * pageSize).Take(pageSize),
                More = items.Count() > page * pageSize
            };
        }

        [WebGet]
        [OperationContract]
        public IEnumerable<KeyContent> MealSelected(string selected)
        {
            var sel = selected.GetIntArray();
            return Db.Meals.Where(o => sel.Contains(o.Id))
                .Select(f => new KeyContent { Key = f.Id.ToString(), Content = f.Name });
        }
        #endregion

        [WebGet]
        [OperationContract]
        public IEnumerable<SelectListItem> ChefsDropdown(string v)
        {
            var list = new List<SelectListItem> { new SelectListItem("", Mui.NotSelected, false) };

            list.AddRange(Db.Chefs.Select(o => new SelectListItem
            {
                Text = o.FirstName + " " + o.LastName,
                Value = o.Id,
                Selected = v == o.Id.ToString()
            }));

            return list;

        }

        [WebGet]
        [OperationContract]
        public KeyContent CountryGet(string v)
        {
            var f = Db.Countries.Where(o => o.Id.ToString() == v).Single();
            return new KeyContent(f.Id, f.Name);
        }

        [WebGet]
        [OperationContract]
        public LookupResult CountriesSearch(string search, int page, bool isMoreClick)
        {
            search = (search ?? "").ToLower();
            var items = Db.Countries.Where(o => o.Name.ToLower().Contains(search)).OrderByDescending(o => o.Id)
                .Select(f => new KeyContent(f.Id, f.Name));

            const int pageSize = 5;
            IEnumerable<KeyContent> Items;

            if (!isMoreClick && page > 1)
                //if on page load page > 1 
                Items = items.Take(page * pageSize);//return all data up to this page
            else
                Items = items.Skip((page - 1) * pageSize).Take(pageSize);

            return new LookupResult
            {
                Items = Items,
                More = items.Count() > page * pageSize
            };
        }

        [WebGet]
        [OperationContract]
        public LookupResult ChefsSearch(string search, int page, bool isMoreClick)
        {
            search = (search ?? "").ToLower();

            var items = Db.Chefs
                .Where(o => o.FirstName.ToLower().Contains(search) || o.LastName.ToLower().Contains(search))
                .OrderByDescending(o => o.Id)
                .Select(f => new KeyContent(f.Id, f.FirstName + " " + f.LastName));

            const int pageSize = 5;
            IEnumerable<KeyContent> Items;

            if (!isMoreClick && page > 1)
                //if on page load page > 1 
                Items = items.Take(page * pageSize);//return all data up to this page
            else
                Items = items.Skip((page - 1) * pageSize).Take(pageSize);

            return new LookupResult
            {
                Items = Items,
                More = items.Count() > page * pageSize
            };
        }

        [WebGet]
        [OperationContract]
        public LookupResult MealsSearch(string search, string description, int page, bool isMoreClick)
        {
            search = (search ?? "").ToLower();
            description = (description ?? "").ToLower();

            var items = Db.Meals
                .Where(o => o.Name.ToLower().Contains(search) && o.Description.ToLower().Contains(description))
                .OrderByDescending(o => o.Id)
                .Select(f => new KeyContent(
                    f.Id,
                    string.Format("<div>{0}</div><div class='description'>{1}</div>", HttpUtility.HtmlEncode(f.Name), HttpUtility.HtmlEncode(f.Description)),
                    false));

            const int pageSize = 5;
            IEnumerable<KeyContent> Items;

            if (!isMoreClick && page > 1)
                //if on page load page > 1 
                Items = items.Take(page * pageSize);//return all data up to this page
            else
                Items = items.Skip((page - 1) * pageSize).Take(pageSize);

            return new LookupResult
            {
                Items = Items,
                More = items.Count() > page * pageSize
            };
        }

        [WebGet]
        [OperationContract]
        public LookupResult DinnersSearch(string search, string chef, string meals, string orderBy, int pageSize, int page, bool isMoreClick, string ascDesc)
        {
            search = (search ?? "").ToLower();
            var mealIds = meals.GetIntArray();

            const string templ = @"
<div>{0}, {1} is cooking in {5} <div class='awf-list-buttons'><button type='button' onclick='javascript:del({2})' class='bdel' ></button></div></div>
<div>Meals: {3}</div>
<div>Date: {4} <div class='awf-list-buttons'><button type='button' onclick='javascript:edit({2})' class='bedit'></button></div></div>";

            var dinners = Db.Dinners
                .Where(o => o.Name.ToLower().Contains(search) &&
                (string.IsNullOrEmpty(chef) || o.Chef.Id.ToString() == chef) &&
                (mealIds.Count() == 0 || mealIds.All(mid => o.Meals.Any(m => m.Id == mid))))
                .OrderByDescending(o => o.Id);

            if (ascDesc == "asc")
            {
                if (orderBy == "date") dinners = dinners.OrderBy(o => o.Date);
                if (orderBy == "name") dinners = dinners.OrderBy(o => o.Name);
                if (orderBy == "meals") dinners = dinners.OrderBy(o => o.Meals.Count());
            }
            else if (ascDesc == "dsc")
            {
                if (orderBy == "date") dinners = dinners.OrderByDescending(o => o.Date);
                if (orderBy == "name") dinners = dinners.OrderByDescending(o => o.Name);
                if (orderBy == "meals") dinners = dinners.OrderByDescending(o => o.Meals.Count());
            }

            IEnumerable<Dinner> items;

            if (!isMoreClick && page > 1)//if on page load page > 1 
                items = dinners.Take(page * pageSize);//return all data up to this page
            else
                items = dinners.Skip((page - 1) * pageSize).Take(pageSize);

            return new LookupResult
            {
                Items = items.Select(f => new KeyContent(f.Id, string.Format(templ,
f.Name, f.Chef.FirstName + " " + f.Chef.LastName, f.Id, string.Join(", ", f.Meals.Select(m => m.Name).ToArray()), f.Date.ToShortDateString(), f.Country.Name), false)),
                More = dinners.Count() > page * pageSize
            };
        }

        [WebGet]
        [OperationContract]
        public IEnumerable<SelectListItem> AscDesc(string v)
        {
            return new[] { 
                new SelectListItem("asc","Asc",v == "asc"),
                new SelectListItem("dsc","Desc",v == "dsc")
            };
        }

    }
}
