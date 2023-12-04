﻿using DemoCICD.Domain.Entities;
using DemoCICD.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Action = DemoCICD.Domain.Entities.Identity.Action;

namespace DemoCICD.Persistence;
public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }

    public DbSet<Action> Actions { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<AppUser> AppUsers { get; set; }

    public DbSet<Function> Functions { get; set; }

    public DbSet<ActionInFunction> ActionInFunctions { get; set; }

    public DbSet<Permission> Permissions { get; set; }

}
