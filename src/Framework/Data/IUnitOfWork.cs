using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Data
{
    public interface IUnitOfWork
    {

        void SaveChanges();

    }
}
