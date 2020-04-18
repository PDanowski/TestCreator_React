using System;
using System.Collections.Generic;
using System.Text;

namespace TestCreatorWebApp.Data.Queries
{
    public class GetRefreshTokenQuery
    {
        public string ClientId { get; set; }
        public string RefreshToken { get; set; }
    }
}
