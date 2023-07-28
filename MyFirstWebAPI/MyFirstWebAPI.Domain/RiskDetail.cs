using MyFirstWebAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Domain
{
    public class RiskDetail : EntityBase
    {
        public Guid RiskId { get; set; }
        public int EndorsementNumber { get; set; }
        public bool IsCurrentEndorsement { get; set; }
        public string RiskSubStatusCode { get; set; }
        public DateTime PolicyEffectiveDate { get; set; }
        public DateTime PolicyExpirationDate { get; set; }
    }
}
