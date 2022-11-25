
using Domain.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Core.Models
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public T Data { get; set; }
        public StatusCodeResult StatusCode { get; set; }
        public string Description { get; set; }
    }
}
