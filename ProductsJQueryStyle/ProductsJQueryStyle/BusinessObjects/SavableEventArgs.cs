using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class SavableEventArgs
    {

        #region  Private Members 
        private bool _Savable = false;
        #endregion

        #region  Public Properties
        public bool Savable
        {
            get
            {
                return _Savable;
            }
            set
            {
                _Savable = value;
            }
        }
        #endregion

        #region  Private Methods 

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Public Event Handlers

        #endregion

        #region Construction
        public SavableEventArgs(bool Savable)
        {
            _Savable = Savable;
        }
        #endregion

    }
}
