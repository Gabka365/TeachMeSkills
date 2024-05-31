﻿using PortalAboutEverything.Data.Enums;
using System.Security.Principal;

namespace PortalAboutEverything.Data.Model
{
    public class User : BaseModel
    {
        // TODO [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UserName { get; set; }
        public string Password { get; set; } // TODO Store only hash
        
        public Permission Permission { get; set; }
        public UserRole Role {  get; set; }

        public virtual List<Game> FavoriteGames { get; set; }
        public virtual List<BoardGame> FavoriteBoardsGames { get; set; }
        public virtual List<Traveling> Travelings { get; set; }

        public virtual List<GameStore> MyGames { get; set; }

        public virtual List<HistoryEvent> FavoriteHistoryEvents { get; set; }
    }
}
