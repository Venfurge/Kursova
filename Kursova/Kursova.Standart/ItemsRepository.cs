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

        public async Task<bool> AddActivityItemsAsync(IEnumerable<ActivityItem> activityItems)
        {
            try
            {
                await _databaseContext.ActivityItems.AddRangeAsync(activityItems);
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
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

        public async Task<IEnumerable<StatisticItem>> GetStatisticItemsAsync()
        {
            try
            {
                var statisticItems = await _databaseContext.StatisticItems.ToListAsync();
                return statisticItems;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<StatisticItem> GetStatisticItemByIdAsync(int id)
        {
            try
            {
                var statisticItem = await _databaseContext.StatisticItems.FindAsync(id);
                return statisticItem;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> AddStatisticItemsAsync(IEnumerable<StatisticItem> statisticItems)
        {
            try
            {
                await _databaseContext.StatisticItems.AddRangeAsync(statisticItems);
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> AddStatisticItemAsync(StatisticItem statisticItem)
        {
            try
            {
                var tracking = await _databaseContext.StatisticItems.AddAsync(statisticItem);
                await _databaseContext.SaveChangesAsync();
                var isAdded = tracking.State == EntityState.Added;
                return isAdded;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UpdateStatisticItemAsync(StatisticItem statisticItem)
        {
            try
            {
                var tracking = _databaseContext.Update(statisticItem);
                await _databaseContext.SaveChangesAsync();
                var isModified = tracking.State == EntityState.Modified;
                return isModified;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> RemoveStatisticItemAsync(int id)
        {
            try
            {
                var statisticItem = await _databaseContext.StatisticItems.FindAsync(id);
                var tracking = _databaseContext.StatisticItems.Remove(statisticItem);
                await _databaseContext.SaveChangesAsync();
                var isDeleted = tracking.State == EntityState.Deleted;
                return isDeleted;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<IEnumerable<StatisticItem>> QueryStatisticItemsAsync(Func<StatisticItem, bool> predicate)
        {
            try
            {
                var statisticItems = _databaseContext.StatisticItems.Where(predicate);
                return statisticItems.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
