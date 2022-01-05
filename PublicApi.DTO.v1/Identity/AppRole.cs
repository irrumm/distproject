using System;

namespace PublicApi.DTO.v1.Identity
{
    public class AppRole
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
    }

    public class AppRoleAdd
    {
        public string Name { get; set; } = default!;
    }
}