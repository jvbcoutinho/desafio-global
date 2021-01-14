using System.Collections.Generic;
using System.Linq;

namespace Desafio.Shared.Exception
{
    public class AuthenticationException : System.Exception
    {
        public AuthenticationException(string message) : base(message)
        {
        }

        public AuthenticationException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}