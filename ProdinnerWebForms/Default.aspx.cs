using System;
using System.Linq;
using Omu.AwesomeWebForms;
using ProdinnerWebForms.Model;
using Resources;

namespace ProdinnerWebForms
{
    public partial class Default : System.Web.UI.Page
    {
        public string Msg;
        public bool ShowForm;

        protected void Save(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var chef = Db.Chefs.SingleOrDefault(o => o.Id.ToString() == eChef.Value);
            var country = Db.Countries.SingleOrDefault(o => o.Id.ToString() == eCountry.Value);
            var meals = Db.Meals.Where(o => eMeals.Value.GetIntArray().Contains(o.Id));

            var dinner = new Dinner { Chef = chef, Country = country, Meals = meals, Name = eName.Text, Date = DateTime.Parse(eDate.Text) };

            if (string.IsNullOrEmpty(eId.Value))
                Db.Insert(dinner);
            else
            {
                dinner.Id = Convert.ToInt32(eId.Value);
                Db.Update(dinner);
            }
            Msg = Mui.ItemSavedMsg;
        }

        protected void Delete(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(jsArg.Value);
            Db.Delete<Dinner>(id);
            Msg = Mui.ItemDeletedMsg;
        }

        protected void Edit(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(jsArg.Value);
            var d = Db.Get<Dinner>(id);
            eId.Value = d.Id.ToString();
            eName.Text = d.Name;
            eChef.Value = d.Chef.Id.ToString();
            eCountry.Value = d.Country.Id.ToString();
            if(d.Meals != null)
            eMeals.Value = d.Meals.Select(o => o.Id).ToJson();
            eDate.Text = d.Date.ToShortDateString();

            ShowForm = true;
        }
    }
}
