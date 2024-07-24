﻿using BestBoardGameApi.Dtos;

namespace PortalAboutEverything.Services.Apis
{
    public class HttpBestBoardGameServise
    {
        private readonly HttpClient _httpClient;

        public HttpBestBoardGameServise(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DtoBestBoardGame> GetBestBoardGameAsync()
        {
            var response = await _httpClient.GetAsync("/getBestBoardGame");
            var dto = await response
                .Content
                .ReadFromJsonAsync<DtoBestBoardGame>();
            return dto;
        }
    }
}
