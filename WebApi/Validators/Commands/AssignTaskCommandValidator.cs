using Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Validators.Commands
{
    public class AssignTaskCommandValidator: AbstractValidator<AssignTaskCommand>
    {
        public AssignTaskCommandValidator()
        {
           RuleFor(x => x.Id).NotNull().NotEmpty();
           RuleFor(x => x.AssignedToId).NotNull().NotEmpty();
        }
    }
}
