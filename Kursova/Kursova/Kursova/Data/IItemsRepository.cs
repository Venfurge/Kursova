using Kursova.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kursova.Data
{
    public interface IItemsRepository
    {
        Task<IEnumerable<ActivityItem>> GetActivityItemsAsync();

        Task<ActivityItem> GetActivityItemByIdAsync(int id);

        Task<bool> AddActivityItemAsync(ActivityItem activityItem);

        Task<bool> UpdateActivityItemAsync(ActivityItem activityItem);

        Task<bool> RemoveActivityItemAsync(int id);

        Task<IEnumerable<ActivityItem>> QueryActivityItemsAsync(Func<ActivityItem, bool> predicate);

        Task<IEnumerable<StatisticItem>> GetStatisticItemsAsync();

        Task<StatisticItem> GetStatisticItemByIdAsync(int id);

        Task<bool> AddStatisticItemAsync(StatisticItem activityItem);

        Task<bool> AddStatisticItemsAsync(IEnumerable<StatisticItem> activityItems);

        Task<bool> UpdateStatisticItemAsync(StatisticItem activityItem);

        Task<bool> RemoveStatisticItemAsync(int id);

        Task<IEnumerable<StatisticItem>> QueryStatisticItemsAsync(Func<StatisticItem, bool> predicate);
    }
}
