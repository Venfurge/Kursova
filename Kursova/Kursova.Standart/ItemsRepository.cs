using Kursova.Data;
using Kursova.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursova.Standart
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ItemsRepository(string dbPath)
        {
            _databaseContext = new DatabaseContext(dbPath);
        }

        public async Task<IEnumerable<ActivityItem>> GetActivityItemsAsync()
        {
            try
            {
                var activityItems = await _databaseContext.ActivityItems.ToListAsync();
                return activityItems;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<ActivityItem> GetActivityItemByIdAsync(int id)
        {
            try
            {
                var activityItem = await _databaseContext.ActivityItems.FindAsync(id);
                return activityItem;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> AddActivityItemAsync(ActivityItem activityItem)
        {
            try
            {
                var tracking = await _databaseContext.ActivityItems.AddAsync(activityItem);
                await _databaseContext.SaveChangesAsync();
                var isAdded = tracking.State == EntityState.Added;
                return isAdded;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UpdateActivityItemAsync(ActivityItem activityItem)
        {
            try
            {
                var tracking = _databaseContext.Update(activityItem);
                await _databaseContext.SaveChangesAsync();
                var isModified = tracking.State == EntityState.Modified;
                return isModified;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> RemoveActivityItemAsync(int id)
        {
            try
            {
                var activityItem = await _databaseContext.ActivityItems.FindAsync(id);
                var tracking = _databaseContext.ActivityItems.Remove(activityItem);
                await _databaseContext.SaveChangesAsync();
                var isDeleted = tracking.State == EntityState.Deleted;
                return isDeleted;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ActivityItem>> QueryActivityItemsAsync(Func<ActivityItem, bool> predicate)
        {
            try
            {
                var activityItems = _databaseContext.ActivityItems.Where(predicate);
                return activityItems.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
