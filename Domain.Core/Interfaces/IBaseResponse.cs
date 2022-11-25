using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Core.Interfaces
{
	public interface IBaseResponse<T>
	{
		T Data { get; set; }
		public StatusCodeResult StatusCode { get; set; }
	}
}
