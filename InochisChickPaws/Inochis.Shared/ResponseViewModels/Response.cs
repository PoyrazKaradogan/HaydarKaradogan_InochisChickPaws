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
        public bool IsSucceeded { get; set; }

       
        /// <param name="data">Geri döndürülecek veri</param>
        /// <returns>Response<typeparamref name="T"/></returns>
        public static Response<T> Success(T data)
        {
            return new Response<T>
            {
                Data = data,
                IsSucceeded = true
            };
        }
        
       
        /// <returns>Response<typeparamref name="T"/></returns>
        public static Response<T> Success()
        {
            return new Response<T>
            {
                Data = default(T),
                IsSucceeded = true
            };
        }
        

        /// <param name="error">Hata metni</param>
        /// <returns>Response<typeparamref name="T"/></returns>
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
