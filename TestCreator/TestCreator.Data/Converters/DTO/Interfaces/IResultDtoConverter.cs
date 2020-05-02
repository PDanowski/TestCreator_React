using System.Collections.Generic;
using Result = TestCreator.Data.Models.DTO.Result;

namespace TestCreator.Data.Converters.DTO.Interfaces
{
    public interface IResultDtoConverter
    {
        Result Convert(Models.DAO.Result result);

        IEnumerable<Result> Convert(IEnumerable<Models.DAO.Result> results);
    }
}
