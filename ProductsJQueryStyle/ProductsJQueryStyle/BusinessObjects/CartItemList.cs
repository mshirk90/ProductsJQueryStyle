using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace BusinessObjects
{
    public class CartItemList
    {
        #region Private Member
        private BindingList<CartItem> _List = new BindingList<CartItem>();
        private decimal _Total = 0;
        private int _TotalItems = 0;
        #endregion

        #region Public Properties
        public BindingList<CartItem> List
        {
            get
            {
                return _List;
            }         
        }
        public decimal Total
        {
            get
            {
                return _Total;
            }
        }

        public int TotalItems
        {
            get
            {
                _TotalItems = 0;
                foreach (CartItem ci in _List)
                {
                    _TotalItems += ci.Quantity;
                }
                return _TotalItems;
            }
        }
        #endregion

        #region Public Methods
        public void Calculate()
        {

        }          

        public bool Exists(Guid productId)
        {
            bool result = false;
            foreach(CartItem item in _List)
            {
                if (item.Id == productId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        public void IncementQuantity(Guid productId)
        {
            foreach (CartItem item in _List)
            {
                if (item.Id == productId)
                {
                    item.Quantity++;
                    break;
                }
            }
        }
        #endregion
    }
}