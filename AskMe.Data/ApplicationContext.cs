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

		public DbSet<AdRating> AdRatings { get; set; }

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
				.WithOne(m => m.ApplicationUser)
				.HasForeignKey(m => m.ApplicationUserId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ApplicationUserConversation>()
				   .HasKey(us => new { us.ApplicationUserId, us.ConversationId });

			modelBuilder.Entity<ApplicationUserConversation>()
				.HasOne(u => u.Conversation)
				.WithMany(c => c.userConversations)
				.HasForeignKey(u => u.ConversationId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ApplicationUserConversation>()
				.HasOne(c => c.ApplicationUser)
				.WithMany(u => u.UserConversations)
				.HasForeignKey(c => c.ApplicationUserId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ForgotenPassword>()
				.HasOne(fp => fp.ApplicationUser)
				.WithMany(au => au.ForgotenPasswords)
				.HasForeignKey(fp => fp.ApplicationUserId);

			modelBuilder.Entity<Ad>()
				.HasOne(ap => ap.ApplicationUser)
				.WithMany(a => a.Ads)
				.HasForeignKey(ap => ap.ApplicationUserId);

			modelBuilder.Entity<Ad>()
				.HasOne(c => c.Category)
				.WithMany(a => a.Ads)
				.HasForeignKey(c => c.CategoryId);

			modelBuilder.Entity<AdRating>()
				.HasOne(au => au.ApplicationUser)
				.WithMany(ar => ar.AdRatings)
				.HasForeignKey(au => au.ApplicationUserId);

			modelBuilder.Entity<AdRating>()
				.HasOne(a => a.Ad)
				.WithMany(ar => ar.AdRatings)
				.HasForeignKey(a => a.AdId);

			base.OnModelCreating(modelBuilder);
		}
	}
}
