using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Application.Common.ErrorHandlers
{
    [Serializable]
    public class CustomErrorException : Exception
    {
        public CustomErrorException()
        {

        }

        public CustomErrorException(string errorMessage) : base(string.Format("{0}", errorMessage))
        {

        }
    }
}
