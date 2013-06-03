using System;
using ProdinnerWebForms.Model;
using Resources;

namespace ProdinnerWebForms
{
    public partial class Chefs : System.Web.UI.Page
    {
        public string Msg;
        public bool ShowForm;

        protected void Save(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            if (string.IsNullOrEmpty(eId.Value))
                Db.Insert(new Chef { FirstName = eFirstName.Text, LastName = eLastName.Text});
            else
            {
                var id = Convert.ToInt32(eId.Value);
                Db.Update(new Chef { Id = id, FirstName = eFirstName.Text, LastName = eLastName.Text});
            }
            Msg = Mui.ItemSavedMsg;
        }

        protected void Delete(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(jsArg.Value);
            Db.Delete<Chef>(id);
            Msg = Mui.ItemDeletedMsg;
        }

        protected void Edit(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(jsArg.Value);
            var c = Db.Get<Chef>(id);
            eId.Value = c.Id.ToString();
            eFirstName.Text = c.FirstName;
            eLastName.Text = c.LastName;
            ShowForm = true;
        }
    }
}
