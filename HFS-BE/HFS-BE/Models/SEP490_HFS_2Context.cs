using System;
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
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<FeedbackReply> FeedbackReplies { get; set; } = null!;
        public virtual DbSet<FeedbackVote> FeedbackVotes { get; set; } = null!;
        public virtual DbSet<Food> Foods { get; set; } = null!;
        public virtual DbSet<FoodImage> FoodImages { get; set; } = null!;
        public virtual DbSet<MenuModerator> MenuModerators { get; set; } = null!;
        public virtual DbSet<MenuReport> MenuReports { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<NotificationType> NotificationTypes { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<OrderProgress> OrderProgresses { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostImage> PostImages { get; set; } = null!;
        public virtual DbSet<PostModerator> PostModerators { get; set; } = null!;
        public virtual DbSet<PostReport> PostReports { get; set; } = null!;
        public virtual DbSet<Seller> Sellers { get; set; } = null!;
        public virtual DbSet<ShipAddress> ShipAddresses { get; set; } = null!;
        public virtual DbSet<Shipper> Shippers { get; set; } = null!;
        public virtual DbSet<Voucher> Vouchers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =localhost; database =SEP490_HFS_2;uid=sa;pwd=123456;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.HasIndex(e => e.Email, "UQ__Admin__AB6E6164673AC4CD")
                    .IsUnique();

                entity.Property(e => e.AdminId)
                    .HasMaxLength(50)
                    .HasColumnName("adminId");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(100)
                    .HasColumnName("avatar");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birthDate");

                entity.Property(e => e.ConfirmedEmail)
                    .IsRequired()
                    .HasColumnName("confirmedEmail")
                    .HasDefaultValueSql("('false')");

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

                entity.Property(e => e.WalletBalance)
                    .HasColumnType("money")
                    .HasColumnName("walletBalance");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => new { e.FoodId, e.CartId })
                    .HasName("PK__CartItem__E3FF5A02B53B3E85");

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
                    .HasConstraintName("FK__CartItem__cartId__4AB81AF0");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CartItem__foodId__4BAC3F29");
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

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasIndex(e => e.Email, "UQ__Customer__AB6E61644904BB1D")
                    .IsUnique();

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("customerId");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(100)
                    .HasColumnName("avatar");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birthDate");

                entity.Property(e => e.ConfirmedEmail)
                    .IsRequired()
                    .HasColumnName("confirmedEmail")
                    .HasDefaultValueSql("('false')");

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

                entity.Property(e => e.IsOnline)
                    .IsRequired()
                    .HasColumnName("isOnline")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("lastName");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(11)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.WalletBalance)
                    .HasColumnType("money")
                    .HasColumnName("walletBalance");
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

                entity.Property(e => e.Star).HasColumnName("star");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Feedback__custom__7A672E12");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.FoodId)
                    .HasConstraintName("FK__Feedback__foodId__7B5B524B");
            });

            modelBuilder.Entity<FeedbackReply>(entity =>
            {
                entity.HasKey(e => e.ReplyId)
                    .HasName("PK__Feedback__36BBF68822C2775D");

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
                    .HasConstraintName("FK__FeedbackR__custo__7F2BE32F");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.FeedbackReplies)
                    .HasForeignKey(d => d.FeedbackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FeedbackR__feedb__01142BA1");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.FeedbackReplies)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK__FeedbackR__selle__00200768");
            });

            modelBuilder.Entity<FeedbackVote>(entity =>
            {
                entity.HasKey(e => e.VoteId)
                    .HasName("PK__Feedback__78F0B9F3A9CA5ACC");

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
                    .HasConstraintName("FK__FeedbackV__feedb__04E4BC85");

                entity.HasOne(d => d.VoteByNavigation)
                    .WithMany(p => p.FeedbackVotes)
                    .HasForeignKey(d => d.VoteBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FeedbackV__voteB__03F0984C");
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("Food");

                entity.Property(e => e.FoodId).HasColumnName("foodId");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.SellerId)
                    .HasMaxLength(50)
                    .HasColumnName("sellerId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("unitPrice");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Food__categoryId__47DBAE45");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Food__sellerId__46E78A0C");
            });

            modelBuilder.Entity<FoodImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__FoodImag__336E9B55F91439B9");

                entity.ToTable("FoodImage");

                entity.Property(e => e.ImageId).HasColumnName("imageId");

                entity.Property(e => e.FoodId).HasColumnName("foodId");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.FoodImages)
                    .HasForeignKey(d => d.FoodId)
                    .HasConstraintName("FK__FoodImage__foodI__4E88ABD4");
            });

            modelBuilder.Entity<MenuModerator>(entity =>
            {
                entity.HasKey(e => e.ModId)
                    .HasName("PK__MenuMode__0B7D023BF31D75AB");

                entity.ToTable("MenuModerator");

                entity.HasIndex(e => e.Email, "UQ__MenuMode__AB6E6164C79353FC")
                    .IsUnique();

                entity.Property(e => e.ModId)
                    .HasMaxLength(50)
                    .HasColumnName("modId");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(100)
                    .HasColumnName("avatar");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birthDate");

                entity.Property(e => e.ConfirmedEmail)
                    .IsRequired()
                    .HasColumnName("confirmedEmail")
                    .HasDefaultValueSql("('false')");

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
            });

            modelBuilder.Entity<MenuReport>(entity =>
            {
                entity.HasKey(e => new { e.FoodId, e.ReportBy })
                    .HasName("PK__MenuRepo__C62346BBD91569D6");

                entity.ToTable("MenuReport");

                entity.Property(e => e.FoodId).HasColumnName("foodId");

                entity.Property(e => e.ReportBy)
                    .HasMaxLength(50)
                    .HasColumnName("reportBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.IsDone).HasColumnName("isDone");

                entity.Property(e => e.ReportContent).HasColumnName("reportContent");

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
                    .HasConstraintName("FK__MenuRepor__foodI__534D60F1");

                entity.HasOne(d => d.ReportByNavigation)
                    .WithMany(p => p.MenuReports)
                    .HasForeignKey(d => d.ReportBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MenuRepor__repor__5165187F");

                entity.HasOne(d => d.UpdateByNavigation)
                    .WithMany(p => p.MenuReports)
                    .HasForeignKey(d => d.UpdateBy)
                    .HasConstraintName("FK__MenuRepor__updat__52593CB8");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Receiver)
                    .HasMaxLength(50)
                    .HasColumnName("receiver");

                entity.Property(e => e.SendBy)
                    .HasMaxLength(50)
                    .HasColumnName("sendBy");

                entity.Property(e => e.TypeId).HasColumnName("typeId");

                entity.HasOne(d => d.ReceiverNavigation)
                    .WithMany(p => p.NotificationReceiverNavigations)
                    .HasForeignKey(d => d.Receiver)
                    .HasConstraintName("FK__Notificat__recei__5FB337D6");

                entity.HasOne(d => d.Receiver1)
                    .WithMany(p => p.NotificationReceiver1s)
                    .HasForeignKey(d => d.Receiver)
                    .HasConstraintName("FK__Notificat__recei__5DCAEF64");

                entity.HasOne(d => d.Receiver2)
                    .WithMany(p => p.NotificationReceiver2s)
                    .HasForeignKey(d => d.Receiver)
                    .HasConstraintName("FK__Notificat__recei__619B8048");

                entity.HasOne(d => d.Receiver3)
                    .WithMany(p => p.NotificationReceiver3s)
                    .HasForeignKey(d => d.Receiver)
                    .HasConstraintName("FK__Notificat__recei__628FA481");

                entity.HasOne(d => d.Receiver4)
                    .WithMany(p => p.NotificationReceiver4s)
                    .HasForeignKey(d => d.Receiver)
                    .HasConstraintName("FK__Notificat__recei__5EBF139D");

                entity.HasOne(d => d.Receiver5)
                    .WithMany(p => p.NotificationReceiver5s)
                    .HasForeignKey(d => d.Receiver)
                    .HasConstraintName("FK__Notificat__recei__60A75C0F");

                entity.HasOne(d => d.SendByNavigation)
                    .WithMany(p => p.NotificationSendByNavigations)
                    .HasForeignKey(d => d.SendBy)
                    .HasConstraintName("FK__Notificat__sendB__59FA5E80");

                entity.HasOne(d => d.SendBy1)
                    .WithMany(p => p.NotificationSendBy1s)
                    .HasForeignKey(d => d.SendBy)
                    .HasConstraintName("FK__Notificat__sendB__5812160E");

                entity.HasOne(d => d.SendBy2)
                    .WithMany(p => p.NotificationSendBy2s)
                    .HasForeignKey(d => d.SendBy)
                    .HasConstraintName("FK__Notificat__sendB__5BE2A6F2");

                entity.HasOne(d => d.SendBy3)
                    .WithMany(p => p.NotificationSendBy3s)
                    .HasForeignKey(d => d.SendBy)
                    .HasConstraintName("FK__Notificat__sendB__5CD6CB2B");

                entity.HasOne(d => d.SendBy4)
                    .WithMany(p => p.NotificationSendBy4s)
                    .HasForeignKey(d => d.SendBy)
                    .HasConstraintName("FK__Notificat__sendB__59063A47");

                entity.HasOne(d => d.SendBy5)
                    .WithMany(p => p.NotificationSendBy5s)
                    .HasForeignKey(d => d.SendBy)
                    .HasConstraintName("FK__Notificat__sendB__5AEE82B9");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Notificat__typeI__6383C8BA");
            });

            modelBuilder.Entity<NotificationType>(entity =>
            {
                entity.ToTable("NotificationType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("customerId");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("orderDate");

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
                    .HasConstraintName("FK__Order__customerI__6B24EA82");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK__Order__sellerId__6A30C649");

                entity.HasOne(d => d.Shipper)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipperId)
                    .HasConstraintName("FK__Order__shipperId__6C190EBB");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.VoucherId)
                    .HasConstraintName("FK__Order__voucherId__6D0D32F4");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.FoodId })
                    .HasName("PK__OrderDet__8F779DFE8E916054");

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
                    .HasConstraintName("FK__OrderDeta__foodI__71D1E811");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__order__70DDC3D8");
            });

            modelBuilder.Entity<OrderProgress>(entity =>
            {
                entity.ToTable("OrderProgress");

                entity.Property(e => e.OrderProgressId).HasColumnName("orderProgressId");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("createdBy");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.OrderProgresses)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__OrderProg__creat__75A278F5");

                entity.HasOne(d => d.CreatedBy1)
                    .WithMany(p => p.OrderProgresses)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__OrderProg__creat__74AE54BC");

                entity.HasOne(d => d.CreatedBy2)
                    .WithMany(p => p.OrderProgresses)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__OrderProg__creat__76969D2E");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProgresses)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderProg__order__778AC167");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.PostContent).HasColumnName("postContent");

                entity.Property(e => e.SellerId)
                    .HasMaxLength(50)
                    .HasColumnName("sellerId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__sellerId__07C12930");
            });

            modelBuilder.Entity<PostImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__PostImag__336E9B55A30180B8");

                entity.ToTable("PostImage");

                entity.Property(e => e.ImageId).HasColumnName("imageId");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostImages)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__PostImage__postI__0A9D95DB");
            });

            modelBuilder.Entity<PostModerator>(entity =>
            {
                entity.HasKey(e => e.ModId)
                    .HasName("PK__PostMode__0B7D023B0D37BA89");

                entity.ToTable("PostModerator");

                entity.HasIndex(e => e.Email, "UQ__PostMode__AB6E6164E9E9868F")
                    .IsUnique();

                entity.Property(e => e.ModId)
                    .HasMaxLength(50)
                    .HasColumnName("modId");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(100)
                    .HasColumnName("avatar");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birthDate");

                entity.Property(e => e.ConfirmedEmail)
                    .IsRequired()
                    .HasColumnName("confirmedEmail")
                    .HasDefaultValueSql("('false')");

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
            });

            modelBuilder.Entity<PostReport>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.ReportBy })
                    .HasName("PK__PostRepo__6CC5DF1891E21F49");

                entity.ToTable("PostReport");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.ReportBy)
                    .HasMaxLength(50)
                    .HasColumnName("reportBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.IsDone).HasColumnName("isDone");

                entity.Property(e => e.ReportContent).HasColumnName("reportContent");

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
                    .HasConstraintName("FK__PostRepor__postI__0F624AF8");

                entity.HasOne(d => d.ReportByNavigation)
                    .WithMany(p => p.PostReports)
                    .HasForeignKey(d => d.ReportBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostRepor__repor__0D7A0286");

                entity.HasOne(d => d.UpdateByNavigation)
                    .WithMany(p => p.PostReports)
                    .HasForeignKey(d => d.UpdateBy)
                    .HasConstraintName("FK__PostRepor__updat__0E6E26BF");
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.ToTable("Seller");

                entity.HasIndex(e => e.Email, "UQ__Seller__AB6E6164A0F8D65C")
                    .IsUnique();

                entity.Property(e => e.SellerId)
                    .HasMaxLength(50)
                    .HasColumnName("sellerId");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(100)
                    .HasColumnName("avatar");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birthDate");

                entity.Property(e => e.ConfirmedEmail)
                    .IsRequired()
                    .HasColumnName("confirmedEmail")
                    .HasDefaultValueSql("('false')");

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

                entity.Property(e => e.IsVerified)
                    .IsRequired()
                    .HasColumnName("isVerified")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("lastName");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(11)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.ShopAddress).HasColumnName("shopAddress");

                entity.Property(e => e.ShopName)
                    .HasMaxLength(50)
                    .HasColumnName("shopName");

                entity.Property(e => e.WalletBalance)
                    .HasColumnType("money")
                    .HasColumnName("walletBalance");
            });

            modelBuilder.Entity<ShipAddress>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .HasName("PK__ShipAddr__26A111AD4DA47B4F");

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
                    .HasConstraintName("FK__ShipAddre__custo__123EB7A3");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("Shipper");

                entity.HasIndex(e => e.Email, "UQ__Shipper__AB6E61641D53CA56")
                    .IsUnique();

                entity.Property(e => e.ShipperId)
                    .HasMaxLength(50)
                    .HasColumnName("shipperId");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(100)
                    .HasColumnName("avatar");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birthDate");

                entity.Property(e => e.ConfirmedEmail)
                    .IsRequired()
                    .HasColumnName("confirmedEmail")
                    .HasDefaultValueSql("('false')");

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

                entity.HasOne(d => d.ManageByNavigation)
                    .WithMany(p => p.Shippers)
                    .HasForeignKey(d => d.ManageBy)
                    .HasConstraintName("FK_Shiper_Seller");
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
                    .HasColumnType("date")
                    .HasColumnName("effectiveDate");

                entity.Property(e => e.ExpireDate)
                    .HasColumnType("date")
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
                    .HasConstraintName("FK__Voucher__sellerI__66603565");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
