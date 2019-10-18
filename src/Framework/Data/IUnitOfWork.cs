using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
    public interface IUnitOfWork
    {

        void SaveChanges();

    }
}
