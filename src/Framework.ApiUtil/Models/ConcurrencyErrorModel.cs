using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.ApiUtil.Models
{
    public class ConcurrencyErrorModel<T>
    {

        public String Message { get; set; }

        public T CurrentObject { get; set; }
    }
}
