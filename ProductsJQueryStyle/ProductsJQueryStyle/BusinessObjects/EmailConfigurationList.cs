//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.ComponentModel;
//using DatabaseHelper;
//using System.Data;

//namespace BusinessObjects
//{
//    public class EmailConfigurationList : Event
//    {

//        #region  Private Members 
//        private BindingList<EmailConfigurationList> _List;


//        #endregion

//        #region  Public Properties
//        public BindingList<EmailConfigurationList> List
//        {
//            get
//            {
//                return _List;
//            }
//        }
//        #endregion

//        #region  Private Methods 

//        #endregion

//        #region Public Methods



//        public EmailConfigurationList GetByDisplayName(string DisplayName)
//        {
//            Database database = new Database("Customer");
//            database.Command.Parameters.Clear();
//            database.Command.CommandType = System.Data.CommandType.StoredProcedure;
//            database.Command.CommandText = "tblEmailConfigGetByDisplayName";
//            database.Command.Parameters.Add("@DisplayName", SqlDbType.UniqueIdentifier).Value = DisplayName;
//            DataTable dt = database.ExecuteQuery();
//            foreach (DataRow dr in dt.Rows)
//            {
//                Customer c = new Customer();
//                c.Initialize(dr);
//                c.InitializeBusinessData(dr);
//                c.IsNew = false;
//                c.IsDirty = false;
//                c.Savable += E_Savable;
              
//                _List.Add(c);
//            }

//            return this;
//        }


//        #endregion

//        #region Public Events
//        private void E_Savable(SavableEventArgs e)
//        {
//            RaiseEvent(e);
//        }

//        private void Phones_Savable(SavableEventArgs e)
//        {
//            RaiseEvent(e);
//        }

//        private void Emails_Savable(SavableEventArgs e)
//        {
//            RaiseEvent(e);
//        }

//        private void Subordinates_Savable(SavableEventArgs e)
//        {
//            RaiseEvent(e);
//        }

//        private void EContacts_Savable(SavableEventArgs e)
//        {
//            RaiseEvent(e);
//        }
//        #endregion

//        #region Construction
//        public EmailConfigurationList()
//        {
//            _List = new BindingList<EmailConfigurationList>();
//            _List.AddingNew += _List_AddingNew; ;
//        }

//        private void _List_AddingNew(object sender, AddingNewEventArgs c)
//        {
//            c.NewObject = new Customer();
//            Customer customer = (Customer)c.NewObject;
//            customer.Savable += E_Savable;
           
//        }
//        #endregion
//    }
//}
