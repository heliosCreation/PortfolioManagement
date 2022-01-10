using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Application.Models.Index
{
    public class GalleryItemModel
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        [Display(Name="Should be deleted")]
        public bool ShouldBeDeleted { get; set; } = false;
    }
}
