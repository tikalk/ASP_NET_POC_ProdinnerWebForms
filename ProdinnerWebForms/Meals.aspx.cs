using System;
using ProdinnerWebForms.Model;
using Resources;

namespace ProdinnerWebForms
{
    public partial class Meals : System.Web.UI.Page
    {
        public string Msg;
        public bool ShowForm;

        protected void Save(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            if (string.IsNullOrEmpty(eId.Value))
                Db.Insert(new Meal { Name = eName.Text, Description = eDescription.Text});
            else
            {
                var id = Convert.ToInt32(eId.Value);
                Db.Update(new Meal { Id = id, Name = eName.Text, Description = eDescription.Text});
            }
            Msg = Mui.ItemSavedMsg;
        }

        protected void Delete(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(jsArg.Value);
            Db.Delete<Meal>(id);
            Msg = Mui.ItemDeletedMsg;
        }

        protected void Edit(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(jsArg.Value);
            var c = Db.Get<Meal>(id);
            eId.Value = c.Id.ToString();
            eName.Text = c.Name;
            eDescription.Text = c.Description;
            ShowForm = true;
        }
    }
}
