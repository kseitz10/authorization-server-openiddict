using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AuthorizationServer.Infrastructure.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OpenIddict.EntityFrameworkCore.Models;

namespace AuthorizationServer.Infrastructure.Persistence.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(_ => _.ConcurrencyStamp).IsETagConcurrency();
        }
    }

    public class ApplicationConfiguration : IEntityTypeConfiguration<OpenIddictEntityFrameworkCoreApplication>
    {
        public void Configure(EntityTypeBuilder<OpenIddictEntityFrameworkCoreApplication> builder)
        {
            builder.Property(_ => _.ConcurrencyToken).IsETagConcurrency();
        }
    }

    public class AuthorizationConfiguration : IEntityTypeConfiguration<OpenIddictEntityFrameworkCoreAuthorization>
    {
        public void Configure(EntityTypeBuilder<OpenIddictEntityFrameworkCoreAuthorization> builder)
        {
            builder.Property(_ => _.ConcurrencyToken).IsETagConcurrency();
        }
    }

    public class ScopeConfiguration : IEntityTypeConfiguration<OpenIddictEntityFrameworkCoreScope>
    {
        public void Configure(EntityTypeBuilder<OpenIddictEntityFrameworkCoreScope> builder)
        {
            builder.Property(_ => _.ConcurrencyToken).IsETagConcurrency();
        }
    }

    public class TokenConfiguration : IEntityTypeConfiguration<OpenIddictEntityFrameworkCoreToken>
    {
        public void Configure(EntityTypeBuilder<OpenIddictEntityFrameworkCoreToken> builder)
        {
            builder.Property(_ => _.ConcurrencyToken).IsETagConcurrency();
        }
    }
}
