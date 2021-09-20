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

        /// <summary>
        /// Returns a list photo albums
        /// </summary>
        /// <param name="numberOfRecords">Number Of Records</param>
        /// <remarks>Gets a list of photos and albums from api and returns combined result of photo albums</remarks>
        /// <response code="200">OK</response>
        /// <response code="204">No Content - The Json placeholder api has no records</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
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

        /// <summary>
        /// Returns a list photo albums of a given User Id
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <remarks>Gets a list of photos and albums from api and returns combined result of photo albums for a given User Id</remarks>
        /// <response code="200">OK</response>
        /// <response code="204">No Content - The Json placeholder api has no records</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not Found - there is no record for given user Id</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
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
