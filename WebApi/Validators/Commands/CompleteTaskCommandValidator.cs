using Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Validators.Commands
{
    public class CompleteTaskCommandValidator: AbstractValidator<CompleteTaskCommand>
    {
        public CompleteTaskCommandValidator()
        {
           RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}
