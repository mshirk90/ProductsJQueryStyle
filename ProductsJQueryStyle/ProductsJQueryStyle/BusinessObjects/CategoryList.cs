using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DatabaseHelper;
using System.ComponentModel;

namespace BusinessObjects
{
    public class CategoryList : Event
    {
        #region Public Properties
        public BindingList<Category> List
        {
            get
            {
                return _List;
            }
        }
        #endregion

        #region Private Members
        private BindingList<Category> _List;

        #endregion

        #region Construction
        public CategoryList()
        {
            _List = new BindingList<Category>();
            _List.AddingNew += _List_AddingNew;
        }



        #endregion

        #region Public Methods
        public CategoryList GetAll()
        {
            Database database = new Database("Customer");
            database.Command.Parameters.Clear();
            database.Command.CommandType = System.Data.CommandType.StoredProcedure;
            database.Command.CommandText = "tblCategoryGetAll";
            DataTable dt = database.ExecuteQuery();

            Category select = new Category();
            select.Id = Guid.Empty;
            select.Name = "--Please Select a Catagory--";
            _List.Add(select);


            foreach (DataRow dr in dt.Rows)
            {
                Category e = new Category();
                e.Initialize(dr);
                e.InitializeBusinessData(dr);
                e.IsNew = false;
                e.IsDirty = false;
                e.Savable += CategoryType_Savable;
                _List.Add(e);
            }

            return this;
        }


        public CategoryList Save()
        {
            foreach (Category pt in _List)
            {
                if (pt.IsSavable() == true)
                {
                    pt.Save();
                }
            }
            return this;
        }
        public bool IsSavable()
        {
            bool result = false;
            foreach (Category h in _List)
            {
                if (h.IsSavable() == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        #endregion

        #region  Public Event Handlers
        private void _List_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = new Category();
            Category CategoryType = (Category)e.NewObject;
            CategoryType.Savable += CategoryType_Savable;
        }

        private void CategoryType_Savable(SavableEventArgs e)
        {
            RaiseEvent(e);
        }
        #endregion

    }
}
