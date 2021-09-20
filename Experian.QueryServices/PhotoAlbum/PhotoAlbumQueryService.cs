using Experian.ApiModels.JsonPlaceHolder;
using Experian.QueryModels;
using Experian.QueryServices.Interfaces.PhotoAlbum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experian.QueryServices.PhotoAlbum
{
    public class PhotoAlbumQueryService : IPhotoAlbumQueryService
    {
        public PhotoAlbumQueryService()
        {
                
        }

        public IEnumerable<PhotoAlbumQueryModel> GetPhotoAlbums(IEnumerable<AlbumApiModel> albums, IEnumerable<PhotoApiModel> photos) => GetAlbumQueryModel(albums, photos);                             

        public IEnumerable<PhotoAlbumQueryModel> GetPhotoAlbumByUserId(IEnumerable<AlbumApiModel> albums, IEnumerable<PhotoApiModel> photos, int userId)
        {
            var result = GetAlbumQueryModel(albums, photos);

            return result.Where(pal => pal.UserId == userId);
        }

        private  List<PhotoAlbumQueryModel> GetAlbumQueryModel(IEnumerable<AlbumApiModel> albums, IEnumerable<PhotoApiModel> photos)
        {
            var query = (from alb in albums
                         join pto in photos
                         on new { a = alb.Id }
                         equals new { a = pto.AlbumId }
                         select new PhotoAlbumQueryModel
                         {
                             UserId = alb.UserId,
                             AlbumId = alb.Id,
                             AlbumTitle = alb.Title,
                             PhotoId = pto.Id,
                             PhotoTitle = pto.Title,
                             PhotoUrl = pto.Url,
                             PhotoThumbnailUrl = pto.ThumbnailUrl
                         })
                         .OrderBy(pal => pal.AlbumId)
                        .ThenBy(pal => pal.PhotoId)
                        .ToList();
            return query;
        }
    }
}
