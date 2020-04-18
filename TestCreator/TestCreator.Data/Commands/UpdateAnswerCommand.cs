using System;
using System.Collections.Generic;
using System.Text;
using TestCreator.Data.Commands.Interfaces;
using TestCreator.Data.Models.DTO;

namespace TestCreator.Data.Commands
{
    public class UpdateAnswerCommand : ICommand
    {
        public Answer Answer { get; set; }
    }
}
