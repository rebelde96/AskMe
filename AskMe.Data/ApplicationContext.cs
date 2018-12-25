using AskMe.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data
{
	public class ApplicationContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Category> Categories { get; set; }

		public DbSet<Conversation> Conversations { get; set; }

		public DbSet<Message> Messages { get; set; }

		public DbSet<ApplicationUserConversation> ApplicationUserConversations { get; set; }

		public DbSet<UserFile> UserFiles { get; set; }

		public DbSet<UserInfo> UserInfos { get; set; }

		public DbSet<ForgotenPassword> ForgotenPasswords { get; set; }

		public DbSet<Ad> Ads { get; set; }
		
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

			modelBuilder.Entity<ApplicationUser>()
				.HasOne(u => u.UserInfo)
				.WithOne(ui => ui.User)
				.HasForeignKey<UserInfo>(ui => ui.ApplicationUserId);

			modelBuilder.Entity<ApplicationUser>()
				.HasMany(u => u.UserFiles)
				.WithOne(uf => uf.User)
				.HasForeignKey(uf => uf.ApplicationUserId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ApplicationUser>()
				.HasMany(u => u.Messages)
				.WithOne(m => m.User)
				.HasForeignKey(m => m.ApplicationUserId)
				.OnDelete(DeleteBehavior.Cascade);
					
			modelBuilder.Entity<ApplicationUserConversation>()
				   .HasKey(us => new { us.ApplicationUserId, us.ConversationId });

			modelBuilder.Entity<ApplicationUserConversation>()
				.HasOne(u => u.Conversation)
				.WithMany(c => c.userConversations)
				.HasForeignKey(u => u.ConversationId);

			modelBuilder.Entity<ApplicationUserConversation>()
				.HasOne(c => c.User)
				.WithMany(u => u.UserConversations)
				.HasForeignKey(c => c.ApplicationUserId);

			modelBuilder.Entity<ForgotenPassword>()
				.HasOne(fp => fp.ApplicationUser)
				.WithMany(au => au.ForgotenPasswords)
				.HasForeignKey(fp => fp.ApplicationUserId);

			modelBuilder.Entity<Ad>()
				.HasOne(fp => fp.ApplicationUser)
				.WithMany(a => a.Ads)
				.HasForeignKey(fp => fp.ApplicationUserId);

			modelBuilder.Entity<Ad>()
				.HasOne(c => c.Category)
				.WithMany(a => a.Ads)
				.HasForeignKey(c => c.CategoryId);

			base.OnModelCreating(modelBuilder);
		}
	}
}
