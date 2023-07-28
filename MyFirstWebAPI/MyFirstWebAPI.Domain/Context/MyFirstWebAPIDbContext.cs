﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyFirstWebAPI.Domain.Common.Extensions;
using MyFirstWebAPI.Domain.Common.Interfaces;

namespace MyFirstWebAPI.Domain
{
    public class MyFirstWebAPIDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public MyFirstWebAPIDbContext(DbContextOptions<MyFirstWebAPIDbContext> options) : base(options) { }

        public MyFirstWebAPIDbContext(DbContextOptions<MyFirstWebAPIDbContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #region Tables
        public DbSet<Country> Countries { get; set; }
        public DbSet<Risk> Risks { get; set; }
        public DbSet<RiskDetail> RiskDetails { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }

        #endregion

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed

            try
            {
                var result = await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
