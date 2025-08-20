using AbadiAgroApi.Model.UserInfo;
using Microsoft.EntityFrameworkCore;

namespace AgroErp.Data.DbConnection
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {

        }
        
        public virtual DbSet<UserModel> Users { get; set; }
        public virtual DbSet<UserDetails> UserDetails { get; set; }
        public virtual DbSet<ApiAuthInfo> Api_AuthInfo { get; set; }
        public virtual DbSet<API_OTP> API_OTP { get; set; }
        public virtual DbSet<API_Token> API_Token { get; set; }        
        public virtual DbSet<Password> Password { get; set; }
        public virtual DbSet<EmpInfo> EmpInfo { get; set; }
  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>(entity =>
            {

                entity.ToView("USERS");

                entity.Property(e => e.Userid).HasColumnName("USERID");
                entity.Property(e => e.EmployeeId).HasColumnName("EMPLOYEEID");
                entity.Property(e => e.EmployeeCode).HasColumnName("EMPLOYEECODE");
                entity.Property(e => e.LoginId).HasColumnName("LOGINID");
                entity.Property(e => e.Pwd).HasColumnName("PWD");
                entity.Property(e => e.Salt).HasColumnName("SALT");
                entity.Property(e => e.ExpiryDate).HasColumnName("EXPIRYDATE");
                entity.Property(e => e.Locked).HasColumnName("LOCKED");
                entity.Property(e => e.Email_OTP).HasColumnName("EMAIL_OTP");
                entity.Property(e => e.Mobile_OTP).HasColumnName("MOBILE_OTP");
            });

            modelBuilder.Entity<EmployeeDetails>(entity =>
            {
                entity.ToView("EMPLOYEEDETAILS");

                entity.Property(e => e.EmployeeID).HasColumnName("EMPLOYEEID");
                entity.Property(e => e.EmployeeCode).HasColumnName("EMPLOYEECODE");
                entity.Property(e => e.EmployeeName).HasColumnName("EMPLOYEENAME");
                entity.Property(e => e.DesignationName).HasColumnName("DESIGNATIONNAME");
                entity.Property(e => e.Gender).HasColumnName("GENDER");
                entity.Property(e => e.Branch).HasColumnName("BRANCH");
                entity.Property(e => e.SiteCode).HasColumnName("SITECODE");
                entity.Property(e => e.CoreFinanceSiteCode).HasColumnName("COREFINANCESITECODE");
                entity.Property(e => e.GradeID).HasColumnName("GRADEID");

            });

            modelBuilder.Entity<ApiAuthInfo>(entity =>
            {
                entity.ToView("API_AUTHINFO");

                entity.Property(e => e.loginID).HasColumnName("LOGINID");
                entity.Property(e => e.otpToMobile).HasColumnName("OTP_TOMOBILE");
                entity.Property(e => e.otpToEmail).HasColumnName("OTP_TOEMAIL");
                entity.Property(e => e.mobile).HasColumnName("MOBILE");
                entity.Property(e => e.email).HasColumnName("EMAIL");
                entity.Property(e => e.mobileOtp).HasColumnName("MOBILE_OTP");
                entity.Property(e => e.emailOtp).HasColumnName("EMAIL_OTP");
                entity.Property(e => e.token).HasColumnName("TOKEN");
                entity.Property(e => e.validateUpToMobileOtp).HasColumnName("VALIDATEUPTO_MOBILEOTP");
                entity.Property(e => e.validateUpToEmailOtp).HasColumnName("VALIDATEUPTO_EMAILOTP");
                entity.Property(e => e.validateUpToToken).HasColumnName("VALIDATEUPTO_TOKEN");
            });

            modelBuilder.Entity<API_OTP>(entity =>
            {
                entity.ToTable("API_OTP");

                entity.Property(e => e.LoginID).HasColumnName("LOGINID");
                entity.Property(e => e.mobileOtp).HasColumnName("MOBILE_OTP");
                entity.Property(e => e.emailOtp).HasColumnName("EMAIL_OTP");
                entity.Property(e => e.validateUpToMobileOtp).HasColumnName("VALIDATEUPTO_MOBILEOTP");
                entity.Property(e => e.validateUpToEmailOtp).HasColumnName("VALIDATEUPTO_EMAILOTP");
            });

            modelBuilder.Entity<API_Token>(entity =>
            {
                entity.ToTable("API_TOKEN");

                entity.Property(e => e.LoginID).HasColumnName("LOGINID");
                entity.Property(e => e.Token).HasColumnName("TOKEN");
                entity.Property(e => e.ValidateUpToToken).HasColumnName("VALIDATEUPTO_TOKEN");
            });            
            
        }
    }
}
