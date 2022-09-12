using RestWithASPNET.Models.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNET.Models.Entites
{
    [Table("user")]
    public class User : BaseEntity
    {
        [Column("user_name")]
        public string UserName { get; set; }
        [Column("password")]
        public string PassWord { get; set; }
        [Column("full_name")]
        public string FullName { get; set; }
        [Column("refresh_token")]
        public string RefreshToken { get; set; }
        [Column("refresh_token_expiry_time")]
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
