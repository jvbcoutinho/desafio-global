using System;

namespace Desafio.Domain.UserAggregate
{
    public class Phone : Entity
    {
        public string UserId { get; set; }
        public string Number { get; set; }
        public string Ddd { get; set; }

        public Phone(string number, string ddd)
        {
            Number = number;
            Ddd = ddd;
        }

        private Phone()
        {
        }
    }
}