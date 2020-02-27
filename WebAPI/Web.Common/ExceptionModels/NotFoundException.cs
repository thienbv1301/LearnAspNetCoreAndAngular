using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Common.ExceptionModels
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string mess) : base(mess)
        {
        }
    }
}
