using System;
using System.Collections.Generic;
using System.Text;
using TestCreator.Data.Commands.Interfaces;

namespace TestCreator.Data.Commands
{
    public class RemoveQuestionCommand : ICommand
    {
        public int Id { get; set; }
    }
}
