﻿namespace VPMS.SharedKernel.Interfaces;

public interface ICreatedAudit
{
    DateTimeOffset CreatedOn { get; set; }

    string? CreatedBy { get; set; }
}
