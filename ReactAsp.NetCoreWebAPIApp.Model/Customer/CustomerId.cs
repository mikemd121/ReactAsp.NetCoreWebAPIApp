using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactAsp.NetCoreWebAPIApp.Model.Customer
{
    public class CustomerId
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary> 
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary> 
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary> 
        public string Address { get; set; }


        /// <summary>
        /// Gets or sets the value.
        /// </summary> 
        public string Email { get; set; }
    }
}
