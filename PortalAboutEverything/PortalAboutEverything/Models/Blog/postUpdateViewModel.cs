﻿namespace PortalAboutEverything.Models.Blog
{

    public class PostUpdateViewModel
    {
        public int Id { get; set; }

        public string? message { get; set; }

        public string? Name { get; set; }

        public DateTime? Now { get; set; }
    }
}
