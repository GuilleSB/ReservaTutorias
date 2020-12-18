using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaTutorias.Frontend.Utils.Filters
{
    public class AuthorizeViewAttribute: TypeFilterAttribute
    {
        public AuthorizeViewAttribute(string modulo) : base(typeof(AuthorizeViewFilter))
        {
            Arguments = new object[] { modulo };
        }
    }
}
