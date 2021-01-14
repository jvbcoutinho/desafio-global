using System;
using System.Collections.Generic;
using System.Net;

namespace Desafio.API.Filter
{
    public class HttpResponseException
    {
        public string Message { get; set; }


        public HttpResponseException(string message)
        {
            Message = message;
        }
    }

}