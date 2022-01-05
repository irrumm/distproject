using System;

namespace PublicApi.DTO.v1.Identity
{
    public class UserRole
    {
        public string UserEmail { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }
}