﻿using TestCreator.Data.Commands.Interfaces;
using TestCreator.Data.Models.DTO;

namespace TestCreator.Data.Commands
{
    public class AddTestCommand : ICommand
    {
        public Test Test { get; set; }
    }
}
