using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvcApp.Services.Exceptions
{
    public class DbConurrencyException : ApplicationException
    {
        public DbConurrencyException(string message) : base(message)
        {

        }
    }
}
