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

        public virtual DbSet<Accountant> Accountants { get; set; } = null!;
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
        public virtual DbSet<TransactionHistory> TransactionHistories { get; set; } = null!;
        public virtual DbSet<Voucher> Vouchers { get; set; } = null!;
        public virtual DbSet<WalletTransferCode> WalletTransferCodes { get; set; } = null!;

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
            modelBuilder.Entity<Accountant>(entity =>
            {
                entity.ToTable("Accountant");

                entity.HasIndex(e => e.Email, "UQ__Accounta__AB6E6164F5666559")
                    .IsUnique();

                entity.Property(e => e.AccountantId)
                    .HasMaxLength(50)
                    .HasColumnName("accountantId");

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
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.HasIndex(e => e.Email, "UQ__Admin__AB6E616426B57D48")
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
                    .HasName("PK__CartItem__E3FF5A028935C5C6");

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
                    .HasConstraintName("FK__CartItem__cartId__6754599E");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CartItem__foodId__68487DD7");
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
                    .HasName("PK__ChatMess__C87C0C9C1D5A6193");

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
                    .HasConstraintName("FK__Comment__custome__3C34F16F");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__postId__3B40CD36");
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

                entity.HasIndex(e => e.Email, "UQ__Customer__AB6E61640FE9A449")
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

                entity.Property(e => e.IsPhoneVerified)
                    .IsRequired()
                    .HasColumnName("isPhoneVerified")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("lastName");

                entity.Property(e => e.NumberOfViolations).HasColumnName("numberOfViolations");

                entity.Property(e => e.OtpToken).HasColumnName("otpToken");

                entity.Property(e => e.OtpTokenExpiryTime).HasColumnName("otpTokenExpiryTime");

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
                    .HasName("PK__FeedBack__0AC9DBC46AFB8D37");

                entity.ToTable("FeedBackImage");

                entity.Property(e => e.ImagefeedbackId).HasColumnName("imagefeedbackId");

                entity.Property(e => e.FeedbackId).HasColumnName("feedbackId");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.FeedBackImages)
                    .HasForeignKey(d => d.FeedbackId)
                    .HasConstraintName("FK__FeedBackI__feedb__3F115E1A");
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
                    .HasConstraintName("FK__Feedback__custom__09A971A2");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.FoodId)
                    .HasConstraintName("FK__Feedback__foodId__0A9D95DB");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__Feedback__orderI__08B54D69");
            });

            modelBuilder.Entity<FeedbackReply>(entity =>
            {
                entity.HasKey(e => e.ReplyId)
                    .HasName("PK__Feedback__36BBF688F5790D06");

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
                    .HasConstraintName("FK__FeedbackR__custo__0E6E26BF");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.FeedbackReplies)
                    .HasForeignKey(d => d.FeedbackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FeedbackR__feedb__10566F31");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.FeedbackReplies)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK__FeedbackR__selle__0F624AF8");
            });

            modelBuilder.Entity<FeedbackVote>(entity =>
            {
                entity.HasKey(e => e.VoteId)
                    .HasName("PK__Feedback__78F0B9F3F67CF290");

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
                    .HasConstraintName("FK__FeedbackV__feedb__14270015");

                entity.HasOne(d => d.VoteByNavigation)
                    .WithMany(p => p.FeedbackVotes)
                    .HasForeignKey(d => d.VoteBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FeedbackV__voteB__1332DBDC");
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
                    .HasConstraintName("FK__Food__banBy__628FA481");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Food__categoryId__6477ECF3");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Food__sellerId__6383C8BA");
            });

            modelBuilder.Entity<FoodImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__FoodImag__336E9B551CC49A6A");

                entity.ToTable("FoodImage");

                entity.Property(e => e.ImageId).HasColumnName("imageId");

                entity.Property(e => e.FoodId).HasColumnName("foodId");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.FoodImages)
                    .HasForeignKey(d => d.FoodId)
                    .HasConstraintName("FK__FoodImage__foodI__6B24EA82");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__Groups__737584F74FC76002");

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
                    .HasName("PK__MenuMode__0B7D023B0C118163");

                entity.ToTable("MenuModerator");

                entity.HasIndex(e => e.Email, "UQ__MenuMode__AB6E61644945ACCB")
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
                    .HasName("PK__MenuRepo__C62346BB1B5D431A");

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
                    .HasConstraintName("FK__MenuRepor__foodI__6FE99F9F");

                entity.HasOne(d => d.ReportByNavigation)
                    .WithMany(p => p.MenuReports)
                    .HasForeignKey(d => d.ReportBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MenuRepor__repor__6E01572D");

                entity.HasOne(d => d.UpdateByNavigation)
                    .WithMany(p => p.MenuReports)
                    .HasForeignKey(d => d.UpdateBy)
                    .HasConstraintName("FK__MenuRepor__updat__6EF57B66");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Lang })
                    .HasName("PK__Notifica__18576647D0E8647D");

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
                    .HasConstraintName("FK__Order__customerI__797309D9");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK__Order__sellerId__787EE5A0");

                entity.HasOne(d => d.Shipper)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipperId)
                    .HasConstraintName("FK__Order__shipperId__7A672E12");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.VoucherId)
                    .HasConstraintName("FK__Order__voucherId__7B5B524B");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.FoodId })
                    .HasName("PK__OrderDet__8F779DFE7C1E574F");

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
                    .HasConstraintName("FK__OrderDeta__foodI__00200768");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__order__7F2BE32F");
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
                    .HasConstraintName("FK__OrderProg__custo__03F0984C");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProgresses)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderProg__order__05D8E0BE");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.OrderProgresses)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK__OrderProg__selle__02FC7413");

                entity.HasOne(d => d.Shipper)
                    .WithMany(p => p.OrderProgresses)
                    .HasForeignKey(d => d.ShipperId)
                    .HasConstraintName("FK__OrderProg__shipp__04E4BC85");
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
                    .HasConstraintName("FK__Post__banBy__17036CC0");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__sellerId__17F790F9");
            });

            modelBuilder.Entity<PostImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__PostImag__336E9B553C6727D2");

                entity.ToTable("PostImage");

                entity.Property(e => e.ImageId).HasColumnName("imageId");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostImages)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__PostImage__postI__1AD3FDA4");
            });

            modelBuilder.Entity<PostModerator>(entity =>
            {
                entity.HasKey(e => e.ModId)
                    .HasName("PK__PostMode__0B7D023BC1E7AA79");

                entity.ToTable("PostModerator");

                entity.HasIndex(e => e.Email, "UQ__PostMode__AB6E61648C8A908D")
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
                    .HasName("PK__PostRepo__6CC5DF18AD29B58B");

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
                    .HasConstraintName("FK__PostRepor__postI__1F98B2C1");

                entity.HasOne(d => d.ReportByNavigation)
                    .WithMany(p => p.PostReports)
                    .HasForeignKey(d => d.ReportBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostRepor__repor__1DB06A4F");

                entity.HasOne(d => d.UpdateByNavigation)
                    .WithMany(p => p.PostReports)
                    .HasForeignKey(d => d.UpdateBy)
                    .HasConstraintName("FK__PostRepor__updat__1EA48E88");
            });

            modelBuilder.Entity<ProfileImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__ProfileI__336E9B5567ACB450");

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

                entity.HasIndex(e => e.Email, "UQ__Seller__AB6E616408B70E93")
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

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Lng).HasColumnName("lng");

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
                    .HasName("PK__SellerBa__CC1B046A89AF6A89");

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
                    .HasName("PK__SellerLi__928E624FBA732F18");

                entity.ToTable("SellerLicenseImage");

                entity.Property(e => e.ImageLicenseId).HasColumnName("imageLicenseId");

                entity.Property(e => e.IsReplaced).HasColumnName("isReplaced");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.Property(e => e.SellerId)
                    .HasMaxLength(50)
                    .HasColumnName("sellerId");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.SellerLicenseImages)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SellerLic__selle__4B7734FF");
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
                    .HasConstraintName("FK__SellerRep__repor__43D61337");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.SellerReports)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SellerRep__selle__45BE5BA9");

                entity.HasOne(d => d.UpdateByNavigation)
                    .WithMany(p => p.SellerReports)
                    .HasForeignKey(d => d.UpdateBy)
                    .HasConstraintName("FK__SellerRep__updat__44CA3770");
            });

            modelBuilder.Entity<SellerReportImage>(entity =>
            {
                entity.HasKey(e => e.ImageSellerReportId)
                    .HasName("PK__SellerRe__197534356CFBFC2D");

                entity.ToTable("SellerReportImage");

                entity.Property(e => e.ImageSellerReportId).HasColumnName("imageSellerReportId");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.Property(e => e.SellerReportId).HasColumnName("sellerReportId");

                entity.HasOne(d => d.SellerReport)
                    .WithMany(p => p.SellerReportImages)
                    .HasForeignKey(d => d.SellerReportId)
                    .HasConstraintName("FK__SellerRep__selle__489AC854");
            });

            modelBuilder.Entity<ShipAddress>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .HasName("PK__ShipAddr__26A111AD24696444");

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
                    .HasConstraintName("FK__ShipAddre__custo__22751F6C");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("Shipper");

                entity.HasIndex(e => e.Email, "UQ__Shipper__AB6E6164F521F840")
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

            modelBuilder.Entity<TransactionHistory>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__Transact__9B57CF729F13A7A0");

                entity.ToTable("TransactionHistory");

                entity.Property(e => e.TransactionId).HasColumnName("transactionId");

                entity.Property(e => e.AcceptBy).HasMaxLength(50);

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

                entity.HasOne(d => d.AcceptByNavigation)
                    .WithMany(p => p.TransactionHistories)
                    .HasForeignKey(d => d.AcceptBy)
                    .HasConstraintName("FK__Transacti__Accep__2CF2ADDF");
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
                    .HasConstraintName("FK__Voucher__sellerI__74AE54BC");
            });

            modelBuilder.Entity<WalletTransferCode>(entity =>
            {
                entity.HasKey(e => e.CodeId)
                    .HasName("PK__WalletTr__47F8CFE473AC417B");

                entity.ToTable("WalletTransferCode");

                entity.Property(e => e.CodeId).HasColumnName("codeId");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .HasColumnName("code");

                entity.Property(e => e.ExpiredDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiredDate");

                entity.Property(e => e.IsUsed).HasColumnName("isUsed");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("userId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
