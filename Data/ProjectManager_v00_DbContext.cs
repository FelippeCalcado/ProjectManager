using Microsoft.EntityFrameworkCore;
using ProjectManager.Interfaces;
using ProjectManager.Models;
using System.Security.Cryptography;

namespace ProjectManager.Data
{
	public class ProjectManager_v00_DbContext : DbContext
	{
		public ProjectManager_v00_DbContext(DbContextOptions<ProjectManager_v00_DbContext> options) : base(options) { }
		public virtual DbSet<Field> Field { get; set; }
		public virtual DbSet<Project> Project { get; set; }
		public virtual DbSet<Job> Job { get; set; }
		public virtual DbSet<Person> Person { get; set; }
		public virtual DbSet<WorkSection> WorkSection { get; set; }
		public virtual DbSet<JobComponent> JobComponent { get; set; }
		public virtual DbSet<JobRequirement> JobRequirement { get; set; }
		public virtual DbSet<JobPerson> JobPerson { get; set; }
		public virtual DbSet<CardFormat> CardFormat { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<JobComponent>()
				.HasOne(j => j.Job)
				.WithMany(jc => jc.JobComponents);

			modelBuilder.Entity<JobRequirement>()
				.HasOne(j => j.Job)
				.WithMany(jr => jr.JobRequirements);

			modelBuilder.Entity<Job>()
				.HasMany(jr => jr.JobRequirements)
				.WithOne(j => j.Job);

			modelBuilder.Entity<Job>()
				.HasMany(jc => jc.JobComponents)
				.WithOne(j => j.Job);

			modelBuilder.Entity<Job>()
				.HasOne(jc => jc.Project)
				.WithMany(j => j.Jobs);


			modelBuilder.Entity<Job>()
				.HasMany(j => j.WorkSections)
			.WithOne(jc => jc.Job);

			modelBuilder.Entity<WorkSection>()
				.HasOne(j => j.Job)
				.WithMany(jc => jc.WorkSections);

		}


	}
}
