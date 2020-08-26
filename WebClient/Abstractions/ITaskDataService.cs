using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Commands;
using Domain.Queries;

namespace WebClient.Abstractions
{
    public interface ITaskDataService
    {
        public Task<CreateTaskCommandResult> Create(CreateTaskCommand command);
        public Task<CompleteTaskCommandResult> Complete(CompleteTaskCommand command);
        public Task<AssignTaskCommandResult> Assign(AssignTaskCommand command);
    }
}
