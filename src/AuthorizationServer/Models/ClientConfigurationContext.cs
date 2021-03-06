﻿using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwentyTwenty.IdentityServer4.EntityFramework7.DbContexts;

namespace AuthorizationServer.Models
{
    public class ClientConfigurationContext: ClientConfigurationContext<Guid>
    {
        public ClientConfigurationContext(DbContextOptions options)
        : base(options)
        { }
        
    }
}
