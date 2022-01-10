using System;
using System.Collections.Generic;

namespace Portfolio.Application.Models.Files
{
    public class UrslDto
    {
        public ICollection<string> Urls { get; set; }

        public UrslDto(ICollection<string> urls)
        {
            Urls = urls;
        }
    }
}
