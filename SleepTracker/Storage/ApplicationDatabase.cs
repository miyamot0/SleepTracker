/*

Copyright (c) 2018 Shawn Patrick Gilroy, www.smallnstats.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

*/

using SQLite;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SleepTracker.Storage
{
    public class ApplicationDatabase
    {
        readonly SQLiteAsyncConnection database;
        private static object collisionLock = new object();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbPath"></param>
        public ApplicationDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<SleepRecordModel>().Wait();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Init() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
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
