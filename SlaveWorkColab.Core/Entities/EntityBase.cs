using System;

namespace SlaveWorkColab.Core.Entities;

public abstract class EntityBase
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; } = false;
   
}