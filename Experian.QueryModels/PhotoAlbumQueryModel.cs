using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Experian.QueryModels
{
    public class PhotoAlbumQueryModel
    {
        public int UserId { get; set; }

        public int AlbumId { get; set; }

        public string AlbumTitle { get; set; }

        public int PhotoId { get; set; }

        public string PhotoTitle { get; set; }

        public string PhotoUrl { get; set; }

        public string PhotoThumbnailUrl { get; set; }
    }
}
