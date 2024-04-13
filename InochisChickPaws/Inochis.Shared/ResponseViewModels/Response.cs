using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Inochis.Shared.ResponseViewModels
{
    public class Response<T>
    {
        public T Data { get; set; }



        public string Error { get; set; }
        public string Message { get; set; }



        public bool IsSucceeded { get; set; }

       
 
        public static Response<T> Success(T data)
        {
            return new Response<T>
            {
                Data = data,
                IsSucceeded = true
            };
        }
        
       
       
        public static Response<T> Success()
        {
            return new Response<T>
            {
                Data = default(T),
                IsSucceeded = true
            };
        }
        

        /// <param name="error">Hata metni</param>

        public static Response<T> Fail(string error)
        {
            return new Response<T>
            {
                Error = error,
                IsSucceeded = false
            };
        }

    }
}
