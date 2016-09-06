using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Policy;
using System.IdentityModel.Claims;
using System.Security.Principal;

namespace TechSupportService.Security
{
    public class AuthorizationPolicy : IAuthorizationPolicy
    {
        private Guid _id;

        public AuthorizationPolicy()
        {
            _id = new Guid();
        }
        public string Id { get { return _id.ToString(); } }
        public ClaimSet Issuer { get { return ClaimSet.System; } }
        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            IIdentity client = GetClientIdentity(evaluationContext);

            evaluationContext.Properties["Principal"] = new SupportPrincipal(client);

            return true;
        }

        private IIdentity GetClientIdentity(EvaluationContext evaluationContext)
        {
            object obj;
            if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
                throw new ArgumentException("No identity");

            IList<IIdentity> identities = obj as IList<IIdentity>;
            if (identities == null || identities.Count <= 0)
                throw new ArgumentException("No identity");

            return identities[0];
        }


    }
}