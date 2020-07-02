using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace Volo.Abp.IdentityServer.ApiResources
{
    public class ApiScope :  FullAuditedAggregateRoot<Guid>
    {
        public virtual bool Enabled { get; set; }

        [NotNull]
        public virtual string Name { get; protected set; }

        public virtual string DisplayName { get; set; }

        public virtual string Description { get; set; }

        public virtual bool Required { get; set; }

        public virtual bool Emphasize { get; set; }

        public virtual bool ShowInDiscoveryDocument { get; set; }

        public virtual List<ApiScopeClaim> UserClaims { get; protected set; }

        public virtual List<ApiScopeProperty> Properties { get; protected set; }

        protected ApiScope()
        {

        }

        protected internal ApiScope(
            [NotNull] string name,
            string displayName = null,
            string description = null,
            bool required = false,
            bool emphasize = false,
            bool showInDiscoveryDocument = true,
            bool enabled = true)
        {
            Check.NotNull(name, nameof(name));

            Name = name;
            DisplayName = displayName ?? name;
            Description = description;
            Required = required;
            Emphasize = emphasize;
            ShowInDiscoveryDocument = showInDiscoveryDocument;
            Enabled = enabled;

            UserClaims = new List<ApiScopeClaim>();
            Properties = new List<ApiScopeProperty>();
        }

        public virtual void AddUserClaim([NotNull] string type)
        {
            UserClaims.Add(new ApiScopeClaim(Id, Name, type));
        }

        public virtual void RemoveAllUserClaims()
        {
            UserClaims.Clear();
        }

        public virtual void RemoveClaim(string type)
        {
            UserClaims.RemoveAll(r => r.Type == type);
        }

        public virtual ApiScopeClaim FindClaim(string type)
        {
            return UserClaims.FirstOrDefault(r => r.Name == Name && r.Type == type);
        }
    }
}
