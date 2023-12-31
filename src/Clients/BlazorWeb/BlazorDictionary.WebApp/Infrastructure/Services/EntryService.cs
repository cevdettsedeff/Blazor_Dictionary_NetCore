﻿using BlazorDictionary.Common.Models.Page;
using BlazorDictionary.Common.Models.Queries;
using BlazorDictionary.Common.Models.RequestModels;
using BlazorDictionary.WebApp.Infrastructure.Services.Interfaces;
using System.Net.Http.Json;

namespace BlazorDictionary.WebApp.Infrastructure.Services
{
    public class EntryService : IEntryService
    {
        private readonly HttpClient client;

        public EntryService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<GetEntriesViewModel>> GetEntires()
        {
            var result = await client.GetFromJsonAsync<List<GetEntriesViewModel>>("/api/Entry?TodaysEntries=false&Count=30");

            return result;
        }

        public async Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId)
        {
            var result = await client.GetFromJsonAsync<GetEntryDetailViewModel>($"/api/Entry/{entryId}");

            return result;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize)
        {
            var result = await client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/Entry/MainPageEntries?page={page}&pageSize={pageSize}");

            return result;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string userName = null)
        {
            var result = await client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/Entry/UserEntries?userName={userName}&page={page}&pageSize={pageSize}");

            return result;
        }

        public async Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize)
        {
            var result = await client.GetFromJsonAsync<PagedViewModel<GetEntryCommentsViewModel>>($"/api/Cntry/Comments/{entryId}?page={page}&pageSize={pageSize}");

            return result;
        }


        public async Task<Guid> CreateEntry(CreateEntryCommand command)
        {
            var res = await client.PostAsJsonAsync("/api/Entry/CreateEntry", command);

            if (!res.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await res.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }

        public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand command)
        {
            var res = await client.PostAsJsonAsync("/api/Entry/CreateEntryComment", command);

            if (!res.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await res.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }

        public async Task<List<SearchEntryViewModel>> SearchBySubject(string searchText)
        {
            var result = await client.GetFromJsonAsync<List<SearchEntryViewModel>>($"/api/Entry/Search?searchText={searchText}");

            return result;
        }
    }
}
