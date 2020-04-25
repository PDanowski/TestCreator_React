using System;
using System.Collections.Generic;
using System.Text;

namespace TestCreator.Data.Exceptions
{
    public class DataLayerException : Exception
    {
        public DataLayerException(string message, Exception innerException) : base(message, innerException)
        {
            
        }

        public DataLayerException(string message) : base(message)
        {

        }
    }
}
