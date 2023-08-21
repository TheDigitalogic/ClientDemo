
using ClientDemo.WebAPI.Models.Entity;
using System;
using System.Collections.Generic;
using TravelNinjaz.B2B.WebAPI.Models.Entity;

namespace ClientDemo.WebAPI.Models.Interface
{
    public interface IOrganizationRepository
    {
        List<Entity.Organization> GetOrganizationList();
        string SaveOrganization(Organization organization);
    }
}
