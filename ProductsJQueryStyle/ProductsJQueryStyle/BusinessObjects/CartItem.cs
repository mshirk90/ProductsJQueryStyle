using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessObjects
{
    public class CartItem
    {
        #region Private Member
        private Guid _Id = Guid.Empty;
        private string _Name = string.Empty;
        private decimal _Price = 0;
        private int _Quantity = 0;
        private decimal _Subtotal = 0;
        #endregion

        #region Public Properties
        public Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }
        public decimal Price
        {
            get
            {
                return _Price;
            }
            set
            {
                _Price = value;
            }
        }
        public String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        public decimal Subtotal
        {
            get
            {
                return _Subtotal;
            }
            set
            {
                _Subtotal = value;
            }
        }
        public int Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
            }
        }
        #endregion

        #region Constructor
        public CartItem(string productId, string name, string price)
        {
            _Id = new Guid(productId);
            _Name = name;
            _Price = Convert.ToDecimal(price);
            _Quantity = 1;
        }
        public CartItem()
        {
            _Quantity = 1;
        }
        #endregion
    }
}