using AskMe.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data
{
	public class ApplicationContext : DbContext
	{
		public DbSet<Category> Categories { get; set; }

		public DbSet<Conversation> Conversations { get; set; }

		public DbSet<Message> Messages { get; set; }

		public DbSet<User> Users { get; set; }

		public DbSet<UserConversation> UserConversations { get; set; }

		public DbSet<UserFile> UserFiles { get; set; }

		public DbSet<UserInfo> UserInfos { get; set; }

		public ApplicationContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Conversation>()
				.HasMany(c => c.Messages)
				.WithOne(m => m.Conversation)
				.HasForeignKey(m => m.ConversationId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<User>()
				.HasOne(u => u.UserInfo)
				.WithOne(ui => ui.User)
				.HasForeignKey<UserInfo>(ui => ui.UserId);

			modelBuilder.Entity<User>()
				.HasMany(u => u.UserFiles)
				.WithOne(uf => uf.User)
				.HasForeignKey(uf => uf.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<User>()
				.HasMany(u => u.Messages)
				.WithOne(m => m.User)
				.HasForeignKey(m => m.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<UserConversation>()
				   .HasKey(us => new { us.UserId, us.ConversationId });

			modelBuilder.Entity<UserConversation>()
				.HasOne(u => u.Conversation)
				.WithMany(c => c.userConversations)
				.HasForeignKey(u => u.ConversationId);

			modelBuilder.Entity<UserConversation>()
				.HasOne(c => c.User)
				.WithMany(u => u.userConversations)
				.HasForeignKey(c => c.UserId);
		}
	}
}
