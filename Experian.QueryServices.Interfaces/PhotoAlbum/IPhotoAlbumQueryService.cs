using Experian.ApiModels.JsonPlaceHolder;
using Experian.QueryModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Experian.QueryServices.Interfaces.PhotoAlbum
{
    public interface IPhotoAlbumQueryService
    {
        IEnumerable<PhotoAlbumQueryModel> GetPhotoAlbums(IEnumerable<AlbumApiModel> albums, IEnumerable<PhotoApiModel> photos);

        IEnumerable<PhotoAlbumQueryModel> GetPhotoAlbumByUserId(IEnumerable<AlbumApiModel> albums, IEnumerable<PhotoApiModel> photos, int userId);
    }
}
