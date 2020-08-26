using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebClient.Abstractions;

namespace WebClient.Pages
{
    public class TasksBase: ComponentBase
    {       
        protected List<TaskModel> allTasks = new List<TaskModel>();
        protected List<TaskModel> tasksToShow = new List<TaskModel>();
        protected Guid? familyMemberId;

        [Inject]
        public ITaskDataService TaskDataService { get; set; }

        protected async Task onAddTask(TaskModel task)
        {
            var result = await TaskDataService.Create(new Domain.Commands.CreateTaskCommand()
            {
                Subject = task.text,
                AssignedToId = familyMemberId

            });

            if (result != null && result.Payload != null && result.Payload.Id != Guid.Empty)
            {
                var newTask = new TaskModel()
                {
                    text = result.Payload.Subject,
                    isDone = result.Payload.IsComplete,
                    id = result.Payload.Id
                };
                allTasks.Insert(0, newTask);
                tasksToShow.Insert(0, newTask);

                StateHasChanged();
            }
        }

        protected async Task onDoneTask(TaskModel task)
        {
            await TaskDataService.Complete(new Domain.Commands.CompleteTaskCommand()
            {
                Id = task.id
            });
        }

        protected async Task onAssignTask(AssignTaskModel task)
        {
            await TaskDataService.Assign(new Domain.Commands.AssignTaskCommand()
            {
                Id = task.id,
                AssignedToId = task.AssignToId
            });
        }

        
    }
}
