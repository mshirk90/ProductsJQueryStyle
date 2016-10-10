using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DatabaseHelper;
using System.Data;
using System.Web.Security;
using PhotoHelper;

namespace BusinessObjects
{
    public class Product : HeaderData
    {

        #region  Private Members 
        private Guid _CategoryId = Guid.Empty;
        private string _Name = string.Empty;
        private string _Description = string.Empty;
        private string _FilePath = string.Empty;
        private Decimal _Price = 0;
        private byte[] _Image = null;
        private BrokenRuleList _BrokenRules = new BrokenRuleList();
        private string _RelativeFileName = string.Empty;
        private string _ImageName = string.Empty;

        #endregion

        #region  Public Properties

        public string RelativeFileName
        {
            get
            {
                return _RelativeFileName;
            }
            set
            {
                _RelativeFileName = value;
            }
        }

        public Guid CategoryId
        {
            get
            {
                return _CategoryId;
            }
            set
            {
                if (_CategoryId != value)
                {
                    _CategoryId = value;
                    base.IsDirty = true;
                    bool Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }

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

        public Decimal Price
        {
            get
            {
                return _Price;
            }
            set
            {
                if (_Price != value)
                {
                    _Price = value;
                    base.IsDirty = true;
                    bool Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }

        public string ImageName
        {
            get
            {
                return _ImageName;
            }
            set
            {
                if (_ImageName != value)
                {
                    _ImageName = value;
                    base.IsDirty = true;
                    bool Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }

        public string FilePath
        {
            get
            {
                return _FilePath;
            }
            set
            {
                _FilePath = value;
            }
        }

        public byte[] Image
        {
            get
            {
                return _Image;
            }
            set
            {
                if (_Image != value)
                {
                    _Image = value;
                    base.IsDirty = true;
                    bool Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
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
                database.Command.CommandText = "tblProductINSERT";
                database.Command.Parameters.Add("@CategoryId", SqlDbType.UniqueIdentifier).Value = _CategoryId;
                database.Command.Parameters.Add("@Name", SqlDbType.VarChar).Value = _Name;
                database.Command.Parameters.Add("@Price", SqlDbType.Decimal).Value = _Price;
                database.Command.Parameters.Add("@Image", SqlDbType.Image).Value = Photo.ImageToByteArray(_FilePath);
                database.Command.Parameters.Add("@Description", SqlDbType.VarChar).Value = _Description;


                base.Initialize(database, Guid.Empty);
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

        private bool Update(Database database)
        {
            bool result = true;
            try
            {
                database.Command.Parameters.Clear();
                database.Command.CommandType = System.Data.CommandType.StoredProcedure;
                database.Command.CommandText = "tblProductUPDATE";
                database.Command.Parameters.Add("@CategoryId", SqlDbType.UniqueIdentifier).Value = _CategoryId;
                database.Command.Parameters.Add("@Name", SqlDbType.VarChar).Value = _Name;
                database.Command.Parameters.Add("@Price", SqlDbType.Decimal).Value = _Price;
                database.Command.Parameters.Add("@Image", SqlDbType.Image).Value = _Image;
                database.Command.Parameters.Add("@Description", SqlDbType.VarChar).Value = _Description;
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
                database.Command.CommandText = "tblProductDELETE";
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
            if (_Description.Trim() == string.Empty)
            {
                result = false;
                BrokenRule rule = new BrokenRule("Invalid Description; cannot be empty.");
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

        public Product GetById(Guid id)
        {
            Database database = new Database("Customer");
            DataTable dt = new DataTable();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblProductGetById";
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
            _CategoryId =(Guid) dr["CategoryId"];
            _Name = dr["Name"].ToString();
            _Price = (Decimal) dr["Price"];
            _Description = dr["Description"].ToString();
            _ImageName = dr["ImageName"].ToString();
        //    _Image = (byte[])dr["Image"];
            string filepath = System.IO.Path.Combine(_FilePath, Id + ".jpg");
            _RelativeFileName = System.IO.Path.Combine("UploadedImages", Id + ".jpg");
       //     Photo.ByteArrayToFile(_Image, filepath);

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

        public Product Save()
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
        #endregion

        #region Public Events

        #endregion

        #region Public Event Handlers

        #endregion

        #region Construction

        #endregion

        #region Dispose

        #endregion

    }
}
