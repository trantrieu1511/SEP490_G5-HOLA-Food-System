﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HFS_BE.Models
{
    public partial class SEP490_HFS_2Context : DbContext
    {
        public SEP490_HFS_2Context()
        {
        }

        public SEP490_HFS_2Context(DbContextOptions<SEP490_HFS_2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<CartItem> CartItems { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<ChatMessage> ChatMessages { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Connection> Connections { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<FeedBackImage> FeedBackImages { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<FeedbackReply> FeedbackReplies { get; set; } = null!;
        public virtual DbSet<FeedbackVote> FeedbackVotes { get; set; } = null!;
        public virtual DbSet<Food> Foods { get; set; } = null!;
        public virtual DbSet<FoodImage> FoodImages { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Invitation> Invitations { get; set; } = null!;
        public virtual DbSet<MenuModerator> MenuModerators { get; set; } = null!;
        public virtual DbSet<MenuReport> MenuReports { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<OrderProgress> OrderProgresses { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostImage> PostImages { get; set; } = null!;
        public virtual DbSet<PostModerator> PostModerators { get; set; } = null!;
        public virtual DbSet<PostReport> PostReports { get; set; } = null!;
        public virtual DbSet<ProfileImage> ProfileImages { get; set; } = null!;
        public virtual DbSet<Seller> Sellers { get; set; } = null!;
        public virtual DbSet<SellerBan> SellerBans { get; set; } = null!;
        public virtual DbSet<SellerLicenseImage> SellerLicenseImages { get; set; } = null!;
        public virtual DbSet<SellerReport> SellerReports { get; set; } = null!;
        public virtual DbSet<SellerReportImage> SellerReportImages { get; set; } = null!;
        public virtual DbSet<ShipAddress> ShipAddresses { get; set; } = null!;
        public virtual DbSet<Shipper> Shippers { get; set; } = null!;
        public virtual DbSet<ShipperReport> ShipperReports { get; set; } = null!;
        public virtual DbSet<TransactionHistory> TransactionHistories { get; set; } = null!;
        public virtual DbSet<Voucher> Vouchers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;database=SEP490_HFS_2;Integrated security=true;TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.HasIndex(e => e.Email, "UQ__Admin__AB6E6164FCC116D0")
                    .IsUnique();

                entity.Property(e => e.AdminId)
                    .HasMaxLength(50)
                    .HasColumnName("adminId");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birthDate");

                entity.Property(e => e.ConfirmedEmail)
                    .IsRequired()
                    .HasColumnName("confirmedEmail")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("firstName");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .HasColumnName("gender");

                entity.Property(e => e.IsOnline).HasColumnName("isOnline");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("lastName");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(11)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.RefreshToken)
                    .IsUnicode(false)
                    .HasColumnName("refreshToken");

                entity.Property(e => e.RefreshTokenExpiryTime)
                    .HasColumnType("datetime")
                    .HasColumnName("refreshTokenExpiryTime");

                entity.Property(e => e.WalletBalance)
                    .HasColumnType("money")
                    .HasColumnName("walletBalance");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => new { e.FoodId, e.CartId })
                    .HasName("PK__CartItem__E3FF5A02A23628C0");

                entity.ToTable("CartItem");

                entity.Property(e => e.FoodId).HasColumnName("foodId");

                entity.Property(e => e.CartId)
                    .HasMaxLength(50)
                    .HasColumnName("cartId");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CartItem__cartId__4F7CD00D");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CartItem__foodId__5070F446");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("PK__ChatMess__C87C0C9CF36B3B25");

                entity.ToTable("ChatMessage");

                entity.Property(e => e.CustomerId).HasMaxLength(50);

                entity.Property(e => e.SellerId).HasMaxLength(50);

                entity.Property(e => e.SentAt).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ChatMessages)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChatMessage_Customer_Sender");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.ChatMessages)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChatMessage_Seller_Receiver");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CommentId).HasColumnName("commentId");

                entity.Property(e => e.CommentContent)
                    .HasMaxLength(1500)
                    .HasColumnName("commentContent");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("customerId");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__custome__245D67DE");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__postId__236943A5");
            });

            modelBuilder.Entity<Connection>(entity =>
            {
                entity.Property(e => e.ConnectionId).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.GroupName).HasMaxLength(150);

                entity.HasOne(d => d.GroupNameNavigation)
                    .WithMany(p => p.Connections)
                    .HasForeignKey(d => d.GroupName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChatMessage_Groups");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasIndex(e => e.Email, "UQ__Customer__AB6E6164A551E258")
                    .IsUnique();

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("customerId");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birthDate");

                entity.Property(e => e.ConfirmedEmail)
                    .IsRequired()
                    .HasColumnName("confirmedEmail")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("firstName");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .HasColumnName("gender");

                entity.Property(e => e.IsOnline)
                    .IsRequired()
                    .HasColumnName("isOnline")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("lastName");

                entity.Property(e => e.NumberOfViolations).HasColumnName("numberOfViolations");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(11)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.RefreshToken)
                    .IsUnicode(false)
                    .HasColumnName("refreshToken");

                entity.Property(e => e.RefreshTokenExpiryTime)
                    .HasColumnType("datetime")
                    .HasColumnName("refreshTokenExpiryTime");

                entity.Property(e => e.WalletBalance)
                    .HasColumnType("money")
                    .HasColumnName("walletBalance");
            });

            modelBuilder.Entity<FeedBackImage>(entity =>
            {
                entity.HasKey(e => e.ImagefeedbackId)
                    .HasName("PK__FeedBack__0AC9DBC4E45F1B34");

                entity.ToTable("FeedBackImage");

                entity.Property(e => e.ImagefeedbackId).HasColumnName("imagefeedbackId");

                entity.Property(e => e.FeedbackId).HasColumnName("feedbackId");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.FeedBackImages)
                    .HasForeignKey(d => d.FeedbackId)
                    .HasConstraintName("FK__FeedBackI__feedb__2739D489");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.FeedbackId).HasColumnName("feedbackId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("customerId");

                entity.Property(e => e.FeedbackMessage).HasColumnName("feedbackMessage");

                entity.Property(e => e.FoodId).HasColumnName("foodId");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.Star).HasColumnName("star");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Feedback__custom__71D1E811");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.FoodId)
                    .HasConstraintName("FK__Feedback__foodId__72C60C4A");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__Feedback__orderI__70DDC3D8");
            });

            modelBuilder.Entity<FeedbackReply>(entity =>
            {
                entity.HasKey(e => e.ReplyId)
                    .HasName("PK__Feedback__36BBF6885F2234A7");

                entity.ToTable("FeedbackReply");

                entity.Property(e => e.ReplyId).HasColumnName("replyId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("customerId");

                entity.Property(e => e.FeedbackId).HasColumnName("feedbackId");

                entity.Property(e => e.ReplyMessage).HasColumnName("replyMessage");

                entity.Property(e => e.SellerId)
                    .HasMaxLength(50)
                    .HasColumnName("sellerId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.FeedbackReplies)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__FeedbackR__custo__76969D2E");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.FeedbackReplies)
                    .HasForeignKey(d => d.FeedbackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FeedbackR__feedb__787EE5A0");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.FeedbackReplies)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK__FeedbackR__selle__778AC167");
            });

            modelBuilder.Entity<FeedbackVote>(entity =>
            {
                entity.HasKey(e => e.VoteId)
                    .HasName("PK__Feedback__78F0B9F347CE8EC9");

                entity.ToTable("FeedbackVote");

                entity.Property(e => e.VoteId).HasColumnName("voteId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.FeedbackId).HasColumnName("feedbackId");

                entity.Property(e => e.IsLike).HasColumnName("isLike");

                entity.Property(e => e.VoteBy)
                    .HasMaxLength(50)
                    .HasColumnName("voteBy");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.FeedbackVotes)
                    .HasForeignKey(d => d.FeedbackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FeedbackV__feedb__7C4F7684");

                entity.HasOne(d => d.VoteByNavigation)
                    .WithMany(p => p.FeedbackVotes)
                    .HasForeignKey(d => d.VoteBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FeedbackV__voteB__7B5B524B");
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("Food");

                entity.Property(e => e.FoodId).HasColumnName("foodId");

                entity.Property(e => e.BanBy)
                    .HasMaxLength(50)
                    .HasColumnName("banBy");

                entity.Property(e => e.BanDate)
                    .HasColumnType("datetime")
                    .HasColumnName("banDate");

                entity.Property(e => e.BanNote).HasColumnName("banNote");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.ReportedTimes)
                    .HasColumnName("reportedTimes")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SellerId)
                    .HasMaxLength(50)
                    .HasColumnName("sellerId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("unitPrice");

                entity.HasOne(d => d.BanByNavigation)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.BanBy)
                    .HasConstraintName("FK__Food__banBy__4AB81AF0");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Food__categoryId__4CA06362");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Food__sellerId__4BAC3F29");
            });

            modelBuilder.Entity<FoodImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__FoodImag__336E9B55D185FC80");

                entity.ToTable("FoodImage");

                entity.Property(e => e.ImageId).HasColumnName("imageId");

                entity.Property(e => e.FoodId).HasColumnName("foodId");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.FoodImages)
                    .HasForeignKey(d => d.FoodId)
                    .HasConstraintName("FK__FoodImage__foodI__534D60F1");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__Groups__737584F7A375F328");

                entity.Property(e => e.Name).HasMaxLength(150);
            });

            modelBuilder.Entity<Invitation>(entity =>
            {
                entity.ToTable("Invitation");

                entity.Property(e => e.SellerId)
                    .HasMaxLength(50)
                    .HasColumnName("SellerID");

                entity.Property(e => e.ShipperId)
                    .HasMaxLength(50)
                    .HasColumnName("ShipperID");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Invitations)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invitation_Seller");

                entity.HasOne(d => d.Shipper)
                    .WithMany(p => p.Invitations)
                    .HasForeignKey(d => d.ShipperId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invitation_Shipper");
            });

            modelBuilder.Entity<MenuModerator>(entity =>
            {
                entity.HasKey(e => e.ModId)
                    .HasName("PK__MenuMode__0B7D023B41E52D62");

                entity.ToTable("MenuModerator");

                entity.HasIndex(e => e.Email, "UQ__MenuMode__AB6E6164A2F8C95E")
                    .IsUnique();

                entity.Property(e => e.ModId)
                    .HasMaxLength(50)
                    .HasColumnName("modId");

                entity.Property(e => e.BanLimit)
                    .HasColumnName("banLimit")
                    .HasDefaultValueSql("((25))");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birthDate");

                entity.Property(e => e.ConfirmedEmail)
                    .IsRequired()
                    .HasColumnName("confirmedEmail")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("firstName");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .HasColumnName("gender");

                entity.Property(e => e.IsBanned)
                    .IsRequired()
                    .HasColumnName("isBanned")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.IsOnline).HasColumnName("isOnline");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("lastName");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(11)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.RefreshToken)
                    .IsUnicode(false)
                    .HasColumnName("refreshToken");

                entity.Property(e => e.RefreshTokenExpiryTime)
                    .HasColumnType("datetime")
                    .HasColumnName("refreshTokenExpiryTime");

                entity.Property(e => e.ReportApprovalLimit)
                    .HasColumnName("reportApprovalLimit")
                    .HasDefaultValueSql("((25))");
            });

            modelBuilder.Entity<MenuReport>(entity =>
            {
                entity.HasKey(e => new { e.FoodId, e.ReportBy })
                    .HasName("PK__MenuRepo__C62346BB9382F181");

                entity.ToTable("MenuReport");

                entity.Property(e => e.FoodId).HasColumnName("foodId");

                entity.Property(e => e.ReportBy)
                    .HasMaxLength(50)
                    .HasColumnName("reportBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.ReportContent).HasColumnName("reportContent");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.MenuReports)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MenuRepor__foodI__5812160E");

                entity.HasOne(d => d.ReportByNavigation)
                    .WithMany(p => p.MenuReports)
                    .HasForeignKey(d => d.ReportBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MenuRepor__repor__5629CD9C");

                entity.HasOne(d => d.UpdateByNavigation)
                    .WithMany(p => p.MenuReports)
                    .HasForeignKey(d => d.UpdateBy)
                    .HasConstraintName("FK__MenuRepor__updat__571DF1D5");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Lang })
                    .HasName("PK__Notifica__18576647BF859C3A");

                entity.ToTable("Notification");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Lang).HasColumnName("lang");

                entity.Property(e => e.Content)
                    .HasMaxLength(500)
                    .HasColumnName("content");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.IsRead).HasColumnName("isRead");

                entity.Property(e => e.Receiver)
                    .HasMaxLength(50)
                    .HasColumnName("receiver");

                entity.Property(e => e.SendBy)
                    .HasMaxLength(50)
                    .HasColumnName("sendBy");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("customerId");

                entity.Property(e => e.CustomerPhone)
                    .HasMaxLength(50)
                    .HasColumnName("customerPhone");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("orderDate");

                entity.Property(e => e.PaymentMethod).HasColumnName("paymentMethod");

                entity.Property(e => e.RequiredDate)
                    .HasColumnType("datetime")
                    .HasColumnName("requiredDate");

                entity.Property(e => e.SellerId)
                    .HasMaxLength(50)
                    .HasColumnName("sellerId");

                entity.Property(e => e.ShipAddress).HasColumnName("shipAddress");

                entity.Property(e => e.ShippedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("shippedDate");

                entity.Property(e => e.ShipperId)
                    .HasMaxLength(50)
                    .HasColumnName("shipperId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.VoucherId).HasColumnName("voucherId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Order__customerI__619B8048");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK__Order__sellerId__60A75C0F");

                entity.HasOne(d => d.Shipper)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipperId)
                    .HasConstraintName("FK__Order__shipperId__628FA481");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.VoucherId)
                    .HasConstraintName("FK__Order__voucherId__6383C8BA");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.FoodId })
                    .HasName("PK__OrderDet__8F779DFE159F5195");

                entity.ToTable("OrderDetail");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.FoodId).HasColumnName("foodId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("unitPrice");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__foodI__68487DD7");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__order__6754599E");
            });

            modelBuilder.Entity<OrderProgress>(entity =>
            {
                entity.ToTable("OrderProgress");

                entity.Property(e => e.OrderProgressId).HasColumnName("orderProgressId");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("customerId");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.SellerId)
                    .HasMaxLength(50)
                    .HasColumnName("sellerId");

                entity.Property(e => e.ShipperId)
                    .HasMaxLength(50)
                    .HasColumnName("shipperId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.OrderProgresses)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__OrderProg__custo__6C190EBB");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProgresses)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderProg__order__6E01572D");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.OrderProgresses)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK__OrderProg__selle__6B24EA82");

                entity.HasOne(d => d.Shipper)
                    .WithMany(p => p.OrderProgresses)
                    .HasForeignKey(d => d.ShipperId)
                    .HasConstraintName("FK__OrderProg__shipp__6D0D32F4");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.BanBy)
                    .HasMaxLength(50)
                    .HasColumnName("banBy");

                entity.Property(e => e.BanDate)
                    .HasColumnType("datetime")
                    .HasColumnName("banDate");

                entity.Property(e => e.BanNote).HasColumnName("banNote");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.PostContent)
                    .HasMaxLength(1500)
                    .HasColumnName("postContent");

                entity.Property(e => e.SellerId)
                    .HasMaxLength(50)
                    .HasColumnName("sellerId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.BanByNavigation)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.BanBy)
                    .HasConstraintName("FK__Post__banBy__00200768");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__sellerId__01142BA1");
            });

            modelBuilder.Entity<PostImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__PostImag__336E9B55FC7B7223");

                entity.ToTable("PostImage");

                entity.Property(e => e.ImageId).HasColumnName("imageId");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostImages)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__PostImage__postI__03F0984C");
            });

            modelBuilder.Entity<PostModerator>(entity =>
            {
                entity.HasKey(e => e.ModId)
                    .HasName("PK__PostMode__0B7D023B393B7570");

                entity.ToTable("PostModerator");

                entity.HasIndex(e => e.Email, "UQ__PostMode__AB6E61642EF26FEA")
                    .IsUnique();

                entity.Property(e => e.ModId)
                    .HasMaxLength(50)
                    .HasColumnName("modId");

                entity.Property(e => e.BanLimit)
                    .HasColumnName("banLimit")
                    .HasDefaultValueSql("((25))");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birthDate");

                entity.Property(e => e.ConfirmedEmail)
                    .IsRequired()
                    .HasColumnName("confirmedEmail")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("firstName");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .HasColumnName("gender");

                entity.Property(e => e.IsBanned)
                    .IsRequired()
                    .HasColumnName("isBanned")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.IsOnline).HasColumnName("isOnline");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("lastName");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(11)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.RefreshToken)
                    .IsUnicode(false)
                    .HasColumnName("refreshToken");

                entity.Property(e => e.RefreshTokenExpiryTime)
                    .HasColumnType("datetime")
                    .HasColumnName("refreshTokenExpiryTime");

                entity.Property(e => e.ReportApprovalLimit)
                    .HasColumnName("reportApprovalLimit")
                    .HasDefaultValueSql("((25))");
            });

            modelBuilder.Entity<PostReport>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.ReportBy })
                    .HasName("PK__PostRepo__6CC5DF18E9F516BE");

                entity.ToTable("PostReport");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.ReportBy)
                    .HasMaxLength(50)
                    .HasColumnName("reportBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.ReportContent).HasColumnName("reportContent");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostReports)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostRepor__postI__08B54D69");

                entity.HasOne(d => d.ReportByNavigation)
                    .WithMany(p => p.PostReports)
                    .HasForeignKey(d => d.ReportBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostRepor__repor__06CD04F7");

                entity.HasOne(d => d.UpdateByNavigation)
                    .WithMany(p => p.PostReports)
                    .HasForeignKey(d => d.UpdateBy)
                    .HasConstraintName("FK__PostRepor__updat__07C12930");
            });

            modelBuilder.Entity<ProfileImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__ProfileI__336E9B55CB3DF357");

                entity.ToTable("ProfileImage");

                entity.Property(e => e.ImageId).HasColumnName("imageId");

                entity.Property(e => e.IsReplaced).HasColumnName("isReplaced");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("userId");
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.ToTable("Seller");

                entity.HasIndex(e => e.Email, "UQ__Seller__AB6E616467D53B36")
                    .IsUnique();

                entity.Property(e => e.SellerId)
                    .HasMaxLength(50)
                    .HasColumnName("sellerId");

                entity.Property(e => e.ConfirmedEmail)
                    .IsRequired()
                    .HasColumnName("confirmedEmail")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.IsBanned)
                    .IsRequired()
                    .HasColumnName("isBanned")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.IsOnline).HasColumnName("isOnline");

                entity.Property(e => e.IsVerified)
                    .IsRequired()
                    .HasColumnName("isVerified")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(11)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.RefreshToken)
                    .IsUnicode(false)
                    .HasColumnName("refreshToken");

                entity.Property(e => e.RefreshTokenExpiryTime)
                    .HasColumnType("datetime")
                    .HasColumnName("refreshTokenExpiryTime");

                entity.Property(e => e.ShopAddress).HasColumnName("shopAddress");

                entity.Property(e => e.ShopName)
                    .HasMaxLength(50)
                    .HasColumnName("shopName");

                entity.Property(e => e.WalletBalance)
                    .HasColumnType("money")
                    .HasColumnName("walletBalance");
            });

            modelBuilder.Entity<SellerBan>(entity =>
            {
                entity.HasKey(e => e.BanSellerId)
                    .HasName("PK__SellerBa__CC1B046AFC2D035B");

                entity.ToTable("SellerBan");

                entity.Property(e => e.BanSellerId).HasColumnName("banSellerId");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Reason).HasMaxLength(255);

                entity.Property(e => e.SellerId)
                    .HasMaxLength(50)
                    .HasColumnName("sellerId");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.SellerBans)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SellerBan_Seller");
            });

            modelBuilder.Entity<SellerLicenseImage>(entity =>
            {
                entity.HasKey(e => e.ImageLicenseId)
                    .HasName("PK__SellerLi__928E624F3B2252ED");

                entity.ToTable("SellerLicenseImage");

                entity.Property(e => e.ImageLicenseId).HasColumnName("imageLicenseId");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.Property(e => e.SellerId)
                    .HasMaxLength(50)
                    .HasColumnName("sellerId");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.SellerLicenseImages)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SellerLic__selle__3587F3E0");
            });

            modelBuilder.Entity<SellerReport>(entity =>
            {
                entity.ToTable("SellerReport");

                entity.Property(e => e.SellerReportId).HasColumnName("sellerReportId");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.ReportBy)
                    .HasMaxLength(50)
                    .HasColumnName("reportBy");

                entity.Property(e => e.ReportContent).HasColumnName("reportContent");

                entity.Property(e => e.SellerId)
                    .HasMaxLength(50)
                    .HasColumnName("sellerId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.HasOne(d => d.ReportByNavigation)
                    .WithMany(p => p.SellerReports)
                    .HasForeignKey(d => d.ReportBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SellerRep__repor__2DE6D218");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.SellerReports)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SellerRep__selle__2FCF1A8A");

                entity.HasOne(d => d.UpdateByNavigation)
                    .WithMany(p => p.SellerReports)
                    .HasForeignKey(d => d.UpdateBy)
                    .HasConstraintName("FK__SellerRep__updat__2EDAF651");
            });

            modelBuilder.Entity<SellerReportImage>(entity =>
            {
                entity.HasKey(e => e.ImageSellerReportId)
                    .HasName("PK__SellerRe__197534352BC5DF9B");

                entity.ToTable("SellerReportImage");

                entity.Property(e => e.ImageSellerReportId).HasColumnName("imageSellerReportId");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.Property(e => e.SellerReportId).HasColumnName("sellerReportId");

                entity.HasOne(d => d.SellerReport)
                    .WithMany(p => p.SellerReportImages)
                    .HasForeignKey(d => d.SellerReportId)
                    .HasConstraintName("FK__SellerRep__selle__32AB8735");
            });

            modelBuilder.Entity<ShipAddress>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .HasName("PK__ShipAddr__26A111ADDEA2CEBF");

                entity.ToTable("ShipAddress");

                entity.Property(e => e.AddressId).HasColumnName("addressId");

                entity.Property(e => e.AddressInfo).HasColumnName("addressInfo");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("customerId");

                entity.Property(e => e.IsDefaultAddress).HasColumnName("isDefaultAddress");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ShipAddresses)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ShipAddre__custo__0B91BA14");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("Shipper");

                entity.HasIndex(e => e.Email, "UQ__Shipper__AB6E61641D75DBBE")
                    .IsUnique();

                entity.Property(e => e.ShipperId)
                    .HasMaxLength(50)
                    .HasColumnName("shipperId");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birthDate");

                entity.Property(e => e.ConfirmedEmail)
                    .IsRequired()
                    .HasColumnName("confirmedEmail")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("firstName");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .HasColumnName("gender");

                entity.Property(e => e.IsOnline)
                    .IsRequired()
                    .HasColumnName("isOnline")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.IsVerified)
                    .IsRequired()
                    .HasColumnName("isVerified")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("lastName");

                entity.Property(e => e.ManageBy)
                    .HasMaxLength(50)
                    .HasColumnName("manageBy");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(11)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.RefreshToken)
                    .IsUnicode(false)
                    .HasColumnName("refreshToken");

                entity.Property(e => e.RefreshTokenExpiryTime)
                    .HasColumnType("datetime")
                    .HasColumnName("refreshTokenExpiryTime");

                entity.HasOne(d => d.ManageByNavigation)
                    .WithMany(p => p.Shippers)
                    .HasForeignKey(d => d.ManageBy)
                    .HasConstraintName("FK_Shiper_Seller");
            });

            modelBuilder.Entity<ShipperReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ShipperReport");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.ReportBy)
                    .HasMaxLength(50)
                    .HasColumnName("reportBy");

                entity.Property(e => e.ReportContent).HasColumnName("reportContent");

                entity.Property(e => e.ShipperId)
                    .HasMaxLength(50)
                    .HasColumnName("shipperId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.HasOne(d => d.ReportByNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ReportBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ShipperRe__repor__29221CFB");

                entity.HasOne(d => d.Shipper)
                    .WithMany()
                    .HasForeignKey(d => d.ShipperId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ShipperRe__shipp__2B0A656D");

                entity.HasOne(d => d.UpdateByNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.UpdateBy)
                    .HasConstraintName("FK__ShipperRe__updat__2A164134");
            });

            modelBuilder.Entity<TransactionHistory>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__Transact__9B57CF72786032E0");

                entity.ToTable("TransactionHistory");

                entity.Property(e => e.TransactionId).HasColumnName("transactionId");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ExpiredDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(200);

                entity.Property(e => e.RecieverId)
                    .HasMaxLength(50)
                    .HasColumnName("recieverId");

                entity.Property(e => e.SenderId)
                    .HasMaxLength(50)
                    .HasColumnName("senderId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TransactionType).HasMaxLength(50);

                entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.ToTable("Voucher");

                entity.Property(e => e.VoucherId).HasColumnName("voucherId");

                entity.Property(e => e.Code)
                    .HasMaxLength(100)
                    .HasColumnName("code");

                entity.Property(e => e.DiscountAmount)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("discount_amount");

                entity.Property(e => e.EffectiveDate)
                    .HasColumnType("datetime")
                    .HasColumnName("effectiveDate");

                entity.Property(e => e.ExpireDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expireDate");

                entity.Property(e => e.MinimumOrderValue)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("minimum_order_value");

                entity.Property(e => e.SellerId)
                    .HasMaxLength(50)
                    .HasColumnName("sellerId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK__Voucher__sellerI__5CD6CB2B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
