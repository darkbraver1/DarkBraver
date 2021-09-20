using Experian.ApiServices.Interfaces.Services;
using Experian.QueryModels;
using Experian.QueryServices.Interfaces.PhotoAlbum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Experian.JsonFakeApi.Controllers.JsonPlaceHolder
{
    [ApiController]
    [Area("JsonPlaceHolder")]
    [Route("api/v2/PlaceHolderFakeApi")]
    public class JsonPlaceHolderController: ControllerBase
    {
        private readonly IPhotoAlbumQueryService _photoAlbumQueryService;
        private readonly IPlaceHolderApiService _placeHolderApiService;

        public JsonPlaceHolderController(IPhotoAlbumQueryService photoAlbumQueryService,
            IPlaceHolderApiService placeHolderApiService)
        {
            _photoAlbumQueryService = photoAlbumQueryService;
            _placeHolderApiService = placeHolderApiService;
        }

        [HttpGet]
        [Route("/PhotoAlbum")]
        [Produces(typeof(IEnumerable<PhotoAlbumQueryModel>))]
        public async Task<IActionResult> GetPhotoAlbums(int? numberOfRecords)
        {
            var albumApiModel = await _placeHolderApiService.GetAlbums();
            var photoApiModel = await _placeHolderApiService.GetPhotos();

            var photoAlbums = _photoAlbumQueryService.GetPhotoAlbums(albumApiModel, photoApiModel);

            if (numberOfRecords.HasValue)
            {
                photoAlbums = photoAlbums.Take(numberOfRecords.Value);
                
            }

            return Ok(photoAlbums);
        }

        [HttpGet]
        [Route("/PhotoAlbums/userId")]
        [Produces(typeof(IEnumerable<PhotoAlbumQueryModel>))]
        public async Task<IActionResult> GetPhotoAlbumByUserId(int userId)
        {
            var albumApiModel = await _placeHolderApiService.GetAlbums();
            var photoApiModel = await _placeHolderApiService.GetPhotos();

            var photoAlbums = _photoAlbumQueryService.GetPhotoAlbumByUserId(albumApiModel, photoApiModel, userId);

            return Ok(photoAlbums);
        }
    }
}
