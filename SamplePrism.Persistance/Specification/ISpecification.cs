﻿using System;
using System.Linq.Expressions;

namespace SamplePrism.Persistance.Specification
{
    public interface ISpecification<TEntity>
        where TEntity : class
    {
        Expression<Func<TEntity, bool>> SatisfiedBy();
    }
}
