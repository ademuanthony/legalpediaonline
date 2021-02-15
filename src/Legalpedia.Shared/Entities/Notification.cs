using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Entities
{
    public enum NotificationStatus
    {
        New = 0,
        Read = 1
    }

    public enum NotificationType
    {
        Notice = 0,
        Success = 1,
        Error = 2
    }
    [Table("notifications")]
    public class Notification
    {
        public Notification()
        {
            Status = NotificationStatus.New;
            Type = NotificationType.Notice;
        }
        [Column("rowid")]
        public int Id { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("message")]
        public string Message { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }

        [Column("status")]
        public NotificationStatus Status { get; set; }

        [Column("type")]
        public NotificationType Type { get; set; }
    }
}
