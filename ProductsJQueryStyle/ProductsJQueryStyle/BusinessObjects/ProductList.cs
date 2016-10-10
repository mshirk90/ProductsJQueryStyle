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
    public class ProductList : Event
    { 
        
        #region Private Members
        private BindingList<Product> _List;
        private string _path = string.Empty;
        #endregion

        #region Public Properties
        public BindingList<Product> List
        {
            get
            {
                return _List;
            }
        }
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }
        #endregion     

        #region Construction
        public ProductList()
        {
            _List = new BindingList<Product>();
            _List.AddingNew += _List_AddingNew;
        }



        #endregion

        #region Public Methods
        public ProductList GetByCategoryId(Guid categoryId)
        {
            Database database = new Database("Customer");
            database.Command.Parameters.Clear();
            database.Command.CommandType = System.Data.CommandType.StoredProcedure;
            database.Command.CommandText = "tblProductGetByCategoryId";
            database.Command.Parameters.Add("@CategoryId", SqlDbType.UniqueIdentifier).Value = categoryId;
            DataTable dt = database.ExecuteQuery();

            foreach (DataRow dr in dt.Rows)
            {
                Product product = new Product();
                product.FilePath = _path;
                product.Initialize(dr);
                product.InitializeBusinessData(dr);
                product.IsNew = false;
                product.IsDirty = false;
                product.Savable += ProductType_Savable;
                _List.Add(product);
            }

            return this;
        }

        public ProductList Save()
        {
            foreach (Product pt in _List)
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
            foreach (Product h in _List)
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
            e.NewObject = new Product();
            Product ProductType = (Product)e.NewObject;
            ProductType.Savable += ProductType_Savable;
        }

        private void ProductType_Savable(SavableEventArgs e)
        {
            RaiseEvent(e);
        }
        #endregion

    }
}
