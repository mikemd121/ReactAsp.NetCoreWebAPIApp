using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactAsp.NetCoreWebAPIApp.Core.Common
{
   public class ResponseModel
    {
        public bool IsSuccess
        {
            get;
            set;
        }
        public string Messsage
        {
            get;
            set;
        }
    }
}
