using Kursova.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kursova.Data.ActivityStore
{
    public interface IActivityStore
    {
        Task<IEnumerable<ActivityItem>> GetActivitiesAsync();
        Task<ActivityItem> GetActivity(int id);
        Task AddActivity(ActivityItem item);
        Task UpdateActivity(ActivityItem item);
        Task DeleteActivity(ActivityItem item);
    }
}
