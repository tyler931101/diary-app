using Microsoft.EntityFrameworkCore.Sqlite.Storage.Json.Internal;

namespace Diary.Models
{
    public class DiaryEntry
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}