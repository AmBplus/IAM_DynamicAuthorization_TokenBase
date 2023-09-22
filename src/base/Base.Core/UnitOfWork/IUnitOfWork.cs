﻿namespace Base.Core.UnitOfWork;

public interface IUnitOfWork
{
    void SaveChanges();
    Task SaveChangesAsync();
}
