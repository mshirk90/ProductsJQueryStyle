using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseHelper;
using System.Data;
using System.Web.Security;

namespace BusinessObjects
{
    public class EmailConfiguration : HeaderData
    {

        #region  Private Members 
        private string _Email = string.Empty;
        private string _DisplayName = string.Empty;
        private string _Host = string.Empty;
        private string _Password = string.Empty;
        private int _Port = 0;

        #endregion

        #region  Public Properties


        public string DisplayName
        {
            get
            {
                return _DisplayName;
            }
            set
            {
                if (_DisplayName != value)
                {
                    _DisplayName = value;
                    base.IsDirty = true;
                    bool Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }

        public string Host
        {
            get
            {
                return _Host;
            }
            set
            {
                if (_Host != value)
                {
                    _Host = value;
                    base.IsDirty = true;
                    bool Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }

        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    base.IsDirty = true;
                    bool Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }

        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    base.IsDirty = true;
                    bool Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }

        public int Port
        {
            get
            {
                return _Port;
            }
            set
            {
                if (_Port != value)
                {
                    _Port = value;
                    base.IsDirty = true;
                    bool Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
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
                database.Command.CommandText = "tblEmailConfigINSERT";
                database.Command.Parameters.Add("@DisplayName", SqlDbType.VarChar).Value = _DisplayName;
                database.Command.Parameters.Add("@Host", SqlDbType.VarChar).Value = _Host;
                database.Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = _Email;
                database.Command.Parameters.Add("@Password", SqlDbType.VarChar).Value = _Password;
                database.Command.Parameters.Add("@Port", SqlDbType.Int).Value = _Port;

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
                database.Command.CommandText = "tblEmailConfigINSERT";
                database.Command.Parameters.Add("@DisplayName", SqlDbType.VarChar).Value = _DisplayName;
                database.Command.Parameters.Add("@Host", SqlDbType.VarChar).Value = _Host;
                database.Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = _Email;
                database.Command.Parameters.Add("@Password", SqlDbType.VarChar).Value = _Password;
                database.Command.Parameters.Add("@Port", SqlDbType.Int).Value = _Port;
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

        private bool Delete(Database database)
        {
            bool result = true;
            try
            {
                database.Command.Parameters.Clear();
                database.Command.CommandType = System.Data.CommandType.StoredProcedure;
                database.Command.CommandText = "tblEmailConfigDELETE";
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
            bool result = true;

            if (_Email.Trim() == string.Empty)
            {
                result = false;
            }
            if (_DisplayName.Trim() == string.Empty)
            {
                result = false;
            }
            if (_Port == 0)
            {
                result = false;
            }
            if (_Email == String.Empty)
            {
                result = false;
            }
            if (_Password == String.Empty)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region Public Methods



        public void InitializeBusinessData(DataRow dr)
        {
            _Email = dr["Email"].ToString();
            _DisplayName = dr["DisplayName"].ToString();
            _Email = dr["Email"].ToString();
            _Password = dr["Password"].ToString();
            _Host = dr["Host"].ToString();
            _Port = (int)dr["Port"];

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

        public EmailConfiguration Save()
        {
            bool result = true;
            Database database = new Database("Customer");
            database.BeginTransaction();

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

            if (result == true)
            {
                database.EndTransaction();
            }
            else
            {
                database.RollBack();
            }
            return this;
        }



        public EmailConfiguration GetByDisplayName(string displayName)
        {
            Database database = new Database("Customer");
            DataTable dt = new DataTable();
            database.Command.Parameters.Clear();
            database.Command.CommandType = System.Data.CommandType.StoredProcedure;
            database.Command.CommandText = "tblEmailConfigGetByDisplayName";
            database.Command.Parameters.Add("@DisplayName", SqlDbType.VarChar).Value = displayName;
            dt = database.ExecuteQuery();
            if (dt!= null && dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                base.Initialize(dr);
                InitializeBusinessData(dr);
                base.IsNew = false;
                base.IsDirty = false;
            }

            return this;
        }

        private void C_Savable(SavableEventArgs e)
        {
            throw new NotImplementedException();
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
