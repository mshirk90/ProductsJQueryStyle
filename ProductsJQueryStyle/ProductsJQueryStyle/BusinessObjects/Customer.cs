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
    public class Customer : HeaderData
    {

        #region  Private Members 
        private string _FirstName = string.Empty;
        private string _LastName = string.Empty;
        private string _Email = string.Empty;
        private string _Password = string.Empty;
        private bool _EmailSent = false;
        private bool _IsPasswordPending = false;
        private BrokenRuleList _BrokenRules = new BrokenRuleList();

        #endregion

        #region  Public Properties


        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                if (_FirstName != value)
                {
                    _FirstName = value;
                    base.IsDirty = true;
                    bool Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }

        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                if (_LastName != value)
                {
                    _LastName = value;
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
                    _LastName = value;
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

        public string Fullname
        {
            get
            {
                return String.Concat(_FirstName + " " + _LastName);
            }

        }

        public bool EmailSent
        {
            get
            {
                return _EmailSent;
            }
            set
            {
                _EmailSent = value;
            }
        }

        public bool IsPasswordPending
        {
            get
            {
                return _IsPasswordPending;
            }
            set           
            {
                    if (_IsPasswordPending != value)
                    {
                    _IsPasswordPending = value;
                        base.IsDirty = true;
                        bool Savable = IsSavable();
                        SavableEventArgs e = new SavableEventArgs(Savable);
                        RaiseEvent(e);
                    }                
            }
        }

        public Customer Exists(string email)
        {
            Database database = new Database("Customer");
            DataTable dt = new DataTable();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblCustomerEXISTS";
            database.Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
            dt = database.ExecuteQuery();
            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                base.Initialize(dr);
                InitializeBusinessData(dr);
                base.IsNew = false;
                base.IsDirty = false;
                return this;
            }
            else
            {
                return null;
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
                database.Command.CommandText = "tblCustomerINSERT";
                database.Command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = _FirstName;
                database.Command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = _LastName;
                database.Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = _Email;
                database.Command.Parameters.Add("@Password", SqlDbType.VarChar).Value = _Password;


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
                database.Command.CommandText = "tblCustomerUPDATE";
                database.Command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = _FirstName;
                database.Command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = _LastName;
                database.Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = _Email;
                database.Command.Parameters.Add("@Password", SqlDbType.VarChar).Value = _Password; base.Initialize(database, base.Id);
                database.Command.Parameters.Add("@IsPasswordPassword", SqlDbType.Bit).Value = _IsPasswordPending; base.Initialize(database, base.Id);
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
                database.Command.CommandText = "tblCustomerDELETE";
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

            if (_FirstName.Trim() == string.Empty)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Invalid First Name; cannot be empty.");
                _BrokenRules.List.Add(rule);
            }
            if (_LastName.Trim() == string.Empty)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Invalid Last Name; cannot be empty.");
                _BrokenRules.List.Add(rule);
            }
            if (_FirstName.Length > 20)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Invalid First Name; cannot be more than 20 characters.");
                _BrokenRules.List.Add(rule);
            }
            if (_LastName.Length > 20)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Invalid Last Name; cannot be more than 20 characters.");
                _BrokenRules.List.Add(rule);
            }
            if (_FirstName.Trim().Length < 1)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Invalid First Name; cannot be empty.");
                _BrokenRules.List.Add(rule);
            }
            if (_LastName.Trim().Length < 1)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Invalid Last Name; cannot be empty.");
                _BrokenRules.List.Add(rule);
            }
            if (_Email == String.Empty)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Invalid Email; cannot be empty.");
                _BrokenRules.List.Add(rule);
            }

            string pattern = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
            Regex expression = new Regex(pattern);
            if (expression.IsMatch(_Email) == false)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Invalid Email Format");
                _BrokenRules.List.Add(rule);
            }
            if (_Password == null)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Invalid Password; cannot be empty.");
                _BrokenRules.List.Add(rule);
            }
            if (IsStrongPassword( _Password) == false)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Weak Password");
                _BrokenRules.List.Add(rule);
            }

            return result;
        }

        public bool IsStrongPassword(string password)
        {
            HashSet<char> specialCharacters = new HashSet<char>() { '%', '$', '#' };
            bool result = false;

            int conditionsCount = 0;
            if (password.Any(char.IsLower))
                conditionsCount++;
            if (password.Any(char.IsUpper))
                conditionsCount++;
            if (password.Any(char.IsDigit))
                conditionsCount++;
            if (password.Any(specialCharacters.Contains))
                conditionsCount++;

            if (conditionsCount >= 3)
            {
                result = true;
            }

            return result;
        }
        #endregion

        #region Public Methods

        public Customer GetById(Guid id)
        {
            Database database = new Database("Customer");
            DataTable dt = new DataTable();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblCustomerGetById";
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
            _FirstName = dr["FirstName"].ToString();
            _LastName = dr["LastName"].ToString();
            _Email = dr["Email"].ToString();
            _Password = dr["Password"].ToString();

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

        public Customer Save()
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

            //if (result == true)
            //{
            //    database.EndTransaction();
            //}
            //else
            //{
            //    database.RollBack();
            //}
            return this;
        }

        public Customer Login(string email, string password)
        {

            Database database = new Database("Customer");
            DataTable dt = new DataTable();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblCustomerLOGIN";
            database.Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
            database.Command.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
            dt = database.ExecuteQuery();
            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                base.Initialize(dr);
                InitializeBusinessData(dr);
                base.IsNew = false;
                base.IsDirty = false;
                return this;
            }
            else
            {
                return null;
            }

        }

        public Customer Register(string firstname, string lastname, string email)
        {
            {
                string password = Membership.GeneratePassword(12, 1);

                try
                {
                    Database database = new Database("Customer");
                    database.Command.Parameters.Clear();
                    database.Command.CommandType = System.Data.CommandType.StoredProcedure;
                    database.Command.CommandText = "tblCustomerINSERT";
                    database.Command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = firstname;
                    database.Command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lastname;
                    database.Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                    database.Command.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
                    _FirstName = firstname;
                    _LastName = lastname;
                    _Email = email;
                    _Password = password;
                    base.IsDirty = true;

                    if (this.IsSavable() == true)
                    {
                        base.Initialize(database, Guid.Empty);
                        database.ExecuteNonQuery();
                        base.Initialize(database.Command);
                        base.IsNew = false;
                    }
                    else
                    {
                        throw new Exception("");
                    }
                }
                catch (Exception e)
                {

                    throw;
                }
                return this;
            }

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
