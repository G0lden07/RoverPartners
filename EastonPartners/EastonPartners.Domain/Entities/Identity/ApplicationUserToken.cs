﻿using Microsoft.AspNetCore.Identity;

namespace EastonPartners.Domain.Entities.Identity
{
    public class ApplicationUserToken : IdentityUserToken<string>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
