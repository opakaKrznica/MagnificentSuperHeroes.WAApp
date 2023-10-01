﻿using MagnificentSuperHeroes.WAApp.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace MagnificentSuperHeroes.WAApp.SuperHeroesService
{
    public class SuperHeroesService : ISuperHeroesService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public SuperHeroesService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public SuperHero Hero { get; set; } = new SuperHero();
        public List<SuperHero> SuperHeroes { get; set; } = new List<SuperHero>();
        public List<Comic> Comics { get; set; } = new List<Comic>();
        public List<Team> Teams { get; set; } = new List<Team>();
        public List<Difficulty> Difficulties { get; set; } = new List<Difficulty>();


        public async Task GetSuperHeroes()
        {
            var result = await _httpClient.GetFromJsonAsync<List<SuperHero>>($"{Constants.BaseUrl}superheroes");
            if (result != null)
            {
                SuperHeroes = result;
            }
        }

        public async Task CreateSuperHero(SuperHero hero)
        {
            var result = await _httpClient.PostAsJsonAsync($"{Constants.BaseUrl}superheroes", hero);
            await SetSuperHeroes(result);
        }
        
        public async Task<SuperHero> GetSingleHero(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<SuperHero>($"{Constants.BaseUrl}superheroes/{id}");
            if (result != null)
            {
                return result;
            }
            throw new Exception("Hero not found!");
        }




        public async Task DeleteSuperHero(int id)
        {
            var result = await _httpClient.DeleteAsync($"{Constants.BaseUrl}superheroes/{id}");
            await SetSuperHeroes(result);
        }

        public async Task UpdateSuperHero(SuperHero hero)
        {
            var result = await _httpClient.PutAsJsonAsync($"{Constants.BaseUrl}superheroes/{hero.Id}", hero);
            await SetSuperHeroes(result);
        }

        public async Task SetSuperHeroes(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<SuperHero>>();
            SuperHeroes = response;
            _navigationManager.NavigateTo("superheroes");
        }

        public async Task GetComic()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Comic>>($"{Constants.BaseUrl}superheroes/comics");
            if (result != null)
            {
                Comics = result;
            }
        }

        public async Task GetTeam()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Team>>($"{Constants.BaseUrl}superheroes/teams");
            if (result != null)
            {
                Teams = result;
            }
        }

        public async Task GetDifficulty()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Difficulty>>($"{Constants.BaseUrl}superheroes/difficulties");
            if (result != null)
            {
                Difficulties = result;
            }
        }

    }
}
