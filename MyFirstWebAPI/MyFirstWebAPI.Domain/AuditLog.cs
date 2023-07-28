using MyFirstWebAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Domain
{
   /// <summary>
   /// MXP AuditLog Format
   /// </summary>
    public class AuditLog : EntityBase
    {
        /// <summary>
        /// log transaction
        /// </summary>
        public string Log { get; set; }
        /// <summary>
        /// use for update
        /// </summary>
        public string OldValue { get; set; }
        /// <summary>
        /// use for update
        /// </summary>
        public string NewValue { get; set; }
        public Guid UserId { get; set; }
        public string LoggedBy { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    #region Tempest AuditLogFormat
    /*
     
     public class AuditLog : Entity, IAggregateRoot
    {
        public AuditLog()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public Guid ReferenceId { get; set; }
        public DateTime LogDate { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string Reference { get; set; }
        public string Field { get; set; }
        public string PreviousValue { get; set; }
        public string NewValue { get; set; }
     }
     
     */

    #endregion

    #region RivTech AuditLogFormat
    /*
      public class AuditLog : BaseEntity<long>
    {
        public long UserId { get; set; }
        public string KeyId { get; set; }
        public string AuditType { get; set; }
        public string Description { get; set; }
        public string Method { get; set; }
        public string IPAddress { get; set; }
        public string DeviceType { get; set; }
        public string OS { get; set; }
        public string Browser { get; set; }
        public string BrowserVersion { get; set; }
        public DateTime CreatedDate { get; set; }
    }
     
     */
    #endregion
}
