using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthorizationServer.Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }
    }
}