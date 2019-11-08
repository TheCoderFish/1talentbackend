using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;


namespace OneTalentResignation.BLL.Shared
{
    public class Claims : IClaims
    {
        private readonly IHttpContextAccessor _context;

        public Claims(IHttpContextAccessor context)
        {
            this._context = context;
        }


        public Claim this[string claimName]
        {
            get
            {
                return _context.HttpContext.Request.HttpContext.User.Claims.FirstOrDefault(x => x.Type == claimName);
            }
        }


        public string UserId
        {
            get
            {
                return this["sub"].Value;
            }
        }

        /// <summary>
        /// To return ClaimsPrincipal from context
        /// </summary>
        public ClaimsPrincipal User
        {
            get
            {
                return _context.HttpContext.Request.HttpContext.User;
            }
        }


        public string Role
        {
            get { return this["role"].Value; }
        }

        public List<string> Roles
        {
            get
            {
                return _context.HttpContext.Request.HttpContext.User.Claims.Where(x => x.Type == "role").Select(x => x.Value).ToList();
            }
        }

    }

}
