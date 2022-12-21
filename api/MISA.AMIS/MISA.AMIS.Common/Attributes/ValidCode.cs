using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Attributes
{
    public class ValidCode : Attribute
    {
        #region Field
        public string ErrorMessage { get; set; }
        #endregion


        #region Constructor
        public ValidCode(string error)
        {
            ErrorMessage = error;
        }
        #endregion
    }
}
