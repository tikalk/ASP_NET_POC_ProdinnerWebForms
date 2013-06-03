using System;
using ProdinnerWebForms.Model;
using Resources;

namespace ProdinnerWebForms
{
    public partial class Countries : System.Web.UI.Page
    {
        public string Msg;
        public bool ShowForm;

        protected void Save(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            if (string.IsNullOrEmpty(eId.Value))
                Db.Insert(new Country { Name = eName.Text });
            else
            {
                var id = Convert.ToInt32(eId.Value);
                Db.Update(new Country { Id = id, Name = eName.Text });
            }
            Msg = Mui.ItemSavedMsg;
        }

        protected void Delete(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(jsArg.Value);
            Db.Delete<Country>(id);
            Msg = Mui.ItemDeletedMsg;
        }

        protected void Edit(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(jsArg.Value);
            var c = Db.Get<Country>(id);
            eId.Value = c.Id.ToString();
            eName.Text = c.Name;
            ShowForm = true;
        }
    }
}
