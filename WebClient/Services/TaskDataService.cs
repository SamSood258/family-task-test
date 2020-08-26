using Domain.Commands;
using Domain.Queries;
using System.Net.Http;
using System.Threading.Tasks;
using WebClient.Abstractions;
using Microsoft.AspNetCore.Components;

namespace WebClient.Services
{
    public class TaskDataService : ITaskDataService
    {
        private HttpClient _httpClient;
        public TaskDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CreateTaskCommandResult> Create(CreateTaskCommand command)
        {            
            return await _httpClient.PostJsonAsync<CreateTaskCommandResult>("tasks", command);
        }

        public async Task<CompleteTaskCommandResult> Complete(CompleteTaskCommand command)
        {
            return await _httpClient.PutJsonAsync<CompleteTaskCommandResult>($"tasks/{command.Id}:complete", command);
        }

        public async Task<AssignTaskCommandResult> Assign(AssignTaskCommand command)
        {
            return await _httpClient.PutJsonAsync<AssignTaskCommandResult>($"tasks/{command.Id}:assign", command);
        }
    }
}
