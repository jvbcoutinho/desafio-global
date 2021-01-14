using System.Collections.Generic;
using System.Linq;

namespace Desafio.Shared.Exception
{
    public class NotFoundException : System.Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

    }
}