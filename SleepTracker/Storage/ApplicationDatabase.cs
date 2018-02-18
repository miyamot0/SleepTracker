using System;
using SQLite;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SleepTracker.Storage
{
    public class ApplicationDatabase
    {
        readonly SQLiteAsyncConnection database;
        private static object collisionLock = new object();
        
        public ApplicationDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<SleepRecordModel>().Wait();
        }

        public void Init() { }

        public Task<int> SaveItemAsync(SleepRecordModel item)
        {
            return database.InsertAsync(item);            
        }

        /// <summary>
        /// Gets the sleep records async.
        /// </summary>
        /// <returns>The sleep records async.</returns>
        public Task<List<SleepRecordModel>> GetSleepRecordsAsync()
        {
            lock (collisionLock)
            {
                return database.Table<SleepRecordModel>().ToListAsync();
            }
        }

        /// <summary>
        /// Gets the sleep records async.
        /// </summary>
        /// <returns>The sleep records async.</returns>
        /// <param name="date">Date.</param>
        public Task<List<SleepRecordModel>> GetSleepRecordsAsync(string date)
        {
            lock (collisionLock)
            {
                return database.QueryAsync<SleepRecordModel>("select * from SleepRecordModel where Date = ?", date);
            }
        }

        /// <summary>
        /// Deletes the records async.
        /// </summary>
        /// <param name="date">Date.</param>
        public async void DeleteRecordsAsync(string date)
        {
            foreach (var item in await GetSleepRecordsAsync(date))
            {
                await database.DeleteAsync(item);
            }
        } 
    }
}
