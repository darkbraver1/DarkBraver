using Experian.ApiModels.JsonPlaceHolder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Experian.ApiServices.Interfaces.Services
{
    public interface IPlaceHolderApiService
    {
        Task<IEnumerable<AlbumApiModel>> GetAlbums();
        Task<IEnumerable<PhotoApiModel>> GetPhotos();
    }
}
