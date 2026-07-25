using System;
using System.Collections.Generic;
using System.Linq;
namespace MediCore.Domain.Common;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; set; }

    string? CreatedBy { get; set; }

    DateTime? UpdatedAt { get; set; }

    string? UpdatedBy { get; set; }
}
