using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Experian.ApiServices.Interfaces.Services
{
    public interface IApiCallerService
    {
        Task<T> GetAsync<T>(string uri);
    }
}
