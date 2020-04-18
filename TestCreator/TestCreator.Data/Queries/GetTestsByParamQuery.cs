using TestCreator.Data.Queries.Consts;
using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries
{
    public class GetTestsByParamQuery : IQuery
    {
        public TestsOrder Param { get; set; }
        public int Number { get; set; }
    }
}
