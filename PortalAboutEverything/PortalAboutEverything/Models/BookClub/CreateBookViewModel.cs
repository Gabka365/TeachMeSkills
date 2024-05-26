﻿

namespace PortalAboutEverything.Models.BookClub
{
    public class CreateBookViewModel
    {
        public required string BookAuthor { get; set; }
        public required string BookTitle { get; set; }
        public required string SummaryOfBook { get; set; }
        public int YearOfPublication { get; set; }
    }
}
