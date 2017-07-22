using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
    public class EfUnitOfWork<T> : IUnitOfWork where T: DbContext
    {


        public EfUnitOfWork(T dbContext)
        {
            this.dataContext = dbContext;
        }


        private T dataContext;



        public void SaveChanges()
        {
            this.dataContext.SaveChanges();            
        }
    }
}
