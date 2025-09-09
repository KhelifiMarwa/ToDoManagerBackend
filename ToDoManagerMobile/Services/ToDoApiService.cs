using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoManagerMobile.Models;
using System.Net.Http.Json;
namespace ToDoManagerMobile.Services
{
    public class ToDoApiService
    {
        private readonly HttpClient _httpClient;

        public ToDoApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://192.168.137.41:45455/") // URL de ton API
            };
        }


        public async Task<List<ToDoTask>> GetAllAsync()
        {
            // l’endpoint exact est api/todo → pas /
            return await _httpClient.GetFromJsonAsync<List<ToDoTask>>("api/todo");
        }

        public async Task<ToDoTask> GetByIdAsync(int id) =>
            await _httpClient.GetFromJsonAsync<ToDoTask>($"api/todo/{id}");

        public async Task<ToDoTask> CreateAsync(ToDoTask task)
        {
            var response = await _httpClient.PostAsJsonAsync("api/todo", task);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ToDoTask>();
        }

        public async Task<bool> UpdateAsync(ToDoTask task)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/todo/{task.Id}", task);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/todo/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}