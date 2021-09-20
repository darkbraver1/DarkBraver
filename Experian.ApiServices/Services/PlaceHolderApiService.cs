using Experian.ApiModels.JsonPlaceHolder;
using Experian.ApiServices.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Experian.ApiServices.Services
{
    public class PlaceHolderApiService : IPlaceHolderApiService
    {
        private readonly IJsonFakeApiCallerService _jsonFakeApiCallerService;
        public PlaceHolderApiService(IJsonFakeApiCallerService jsonFakeApiCallerService)
        {
            _jsonFakeApiCallerService = jsonFakeApiCallerService;
        }

        public Task<IEnumerable<AlbumApiModel>> GetAlbums() => _jsonFakeApiCallerService.GetAsync<IEnumerable<AlbumApiModel>>($"albums");

        public Task<IEnumerable<PhotoApiModel>> GetPhotos() => _jsonFakeApiCallerService.GetAsync<IEnumerable<PhotoApiModel>>($"photos");
    }
}
