﻿using BOBS_Backend.Database;
using BOBS_Backend.Models.Book;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Bobs_Backend.Test.Repository
{
    class MockDatabaseRepo : IDisposable
    {
        // to avoid redundant calls;
        private bool disposedValue = false;
        public DatabaseContext CreateInMemoryContext()
        {
            string newDatabaseName = "BobsBookstoreDatabase_" + DateTime.Now.ToFileTimeUtc();  
            var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: newDatabaseName)
            .Options;
            var context = new DatabaseContext(options);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                
                context.Database.EnsureCreated();
            }

            return context;
        }
        

       
        

        

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}