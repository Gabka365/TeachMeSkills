﻿using BoardGamesReviewsApi.Dtos;

namespace PortalAboutEverything.Services
{
    public class HttpBoardGamesReviewsApiService
    {
        private readonly HttpClient _httpClient;

        public HttpBoardGamesReviewsApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DtoBoardGameReview> GetReviewAsync(int id)
        {
            var response = await _httpClient.GetAsync($"get?id={id}");
            var dto = await response
                .Content
                .ReadFromJsonAsync<DtoBoardGameReview>();
            return dto;
        }

        public async Task CreateReviewAsync(DtoBoardGameReviewCreate review)
        {
            var response = await _httpClient.PostAsJsonAsync("createReview", review);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateReviewAsync(DtoBoardGameReviewUpdate review)
        {
            (await _httpClient
               .PostAsJsonAsync("updateReview", review))
               .EnsureSuccessStatusCode();
        }
    }
}
