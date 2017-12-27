using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Framework
{
    public class EfUnitOfWork<T> : IUnitOfWork where T : DbContext
    {


        public EfUnitOfWork(T dbContext)
        {
            this.dataContext = dbContext;
        }


        private T dataContext;



        public void SaveChanges()
        {
            try
            {
                this.dataContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ce)
            {
                // We need to convert the EF specific exception to the concurrency exception 
                // used in System.Data because that is what the service layer expects
                throw new DBConcurrencyException("Unable to update date due to a concurency exception.  Typically this means the object has been updated by another process.  Re-fetch the object and try again", ce);
            }
        }
    }
}
    

