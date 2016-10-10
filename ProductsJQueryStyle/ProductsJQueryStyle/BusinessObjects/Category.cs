using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DatabaseHelper;
using System.Data;
using System.Web.Security;

namespace BusinessObjects
{
    public class Category : HeaderData
    {

        #region  Private Members 
        private string _Name = string.Empty;
        private BrokenRuleList _BrokenRules = new BrokenRuleList();

        #endregion

        #region  Public Properties

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    base.IsDirty = true;
                    bool Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }
        public BrokenRuleList BrokenRules
        {
            get
            {
                return _BrokenRules;
            }
        }

        #endregion

        #region  Private Methods 
        private bool Insert(Database database)
        {
            bool result = true;
            try
            {
                database.Command.Parameters.Clear();
                database.Command.CommandType = System.Data.CommandType.StoredProcedure;
                database.Command.CommandText = "tblCategoryINSERT";
                database.Command.Parameters.Add("@Name", SqlDbType.VarChar).Value = _Name;

                base.Initialize(database, Guid.Empty);
                database.ExecuteNonQueryWithTransaction();
                base.Initialize(database.Command);

            }
            catch (Exception e)
            {
                result = false;
                throw;
            }
            return result;
        }

        private bool Update(Database database)
        {
            bool result = true;
            try
            {
                database.Command.Parameters.Clear();
                database.Command.CommandType = System.Data.CommandType.StoredProcedure;
                database.Command.CommandText = "tblCategoryUPDATE";
                database.Command.Parameters.Add("@Name", SqlDbType.VarChar).Value = _Name;
                database.ExecuteNonQuery();
                base.Initialize(database.Command);
            }
            catch (Exception e)
            {
                result = false;
                throw;
            }
            return result;
        }

        private bool Delete(Database database)
        {
            bool result = true;
            try
            {
                database.Command.Parameters.Clear();
                database.Command.CommandType = System.Data.CommandType.StoredProcedure;
                database.Command.CommandText = "tblCategoryDELETE";
                base.Initialize(database, base.Id);
                database.ExecuteNonQueryWithTransaction();
                base.Initialize(database.Command);

            }
            catch (Exception e)
            {
                result = false;
                throw;
            }
            return result;
        }

        private bool IsValid()
        {
            _BrokenRules.List.Clear();
            bool result = true;

            if (_Name.Trim() == string.Empty)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Invalid Name; cannot be empty.");
                _BrokenRules.List.Add(rule);
            }
          
            if (_Name.Length > 20)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Invalid Name; cannot be more than 20 characters.");
                _BrokenRules.List.Add(rule);
            }

            return result;
        }

    
        #endregion

        #region Public Methods

        public Category GetById(Guid id)
        {
            Database database = new Database("Customer");
            DataTable dt = new DataTable();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblCategoryGetById";
            base.Initialize(database, base.Id);
            dt = database.ExecuteQuery();
            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                base.Initialize(dr);
                InitializeBusinessData(dr);
                base.IsNew = false;
                base.IsDirty = false;
            }
            return this;
        }

        public void InitializeBusinessData(DataRow dr)
        {
            _Name = dr["Name"].ToString();
        }

        public bool IsSavable()
        {
            bool result = false;
            if (base.IsDirty == true && IsValid() == true)
            {
                result = true;
            }

            return result;
        }

        public Category Save()
        {
            bool result = true;
            Database database = new Database("Customer");
            //database.BeginTransaction();

            if (base.IsNew == true && IsDirty == true && IsValid() == true)
            {
                result = Insert(database);
            }
            else if (base.Deleted == true && base.IsDirty == true)
            {
                result = Delete(database);
            }
            else if (base.IsNew == false && IsDirty == true && IsValid() == true)
            {
                result = Update(database);
            }
            if (result == true)
            {
                base.IsDirty = false;
                base.IsNew = false;
            }
            
            return this;
        }
        #endregion

        #region Public Events

        #endregion

        #region Public Event Handlers

        #endregion

        #region Construction

        #endregion

    }
}
