﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace Primus.DAL.Security
{
    public class SchoolPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            if (roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public SchoolPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int SchoolId { get; set; }
        public string[] roles { get; set; }
    }

    public class SchoolPrincipalSerializeModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int SchoolId { get; set; }
        public string[] roles { get; set; }
    }

}