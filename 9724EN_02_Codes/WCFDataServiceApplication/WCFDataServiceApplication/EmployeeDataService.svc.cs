//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel.Web;
using System.Web;

namespace WCFDataServiceApplication
{
    public class EmployeeDataService : DataService< OdataDbEntities>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.AllRead);

            //Set paging 
            config.SetEntitySetPageSize("*", 25);

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }

        protected override void OnStartProcessingRequest(ProcessRequestArgs args)
        {
            base.OnStartProcessingRequest(args);
            HttpContext context = HttpContext.Current;
            HttpCachePolicy c = HttpContext.Current.Response.Cache;
            c.SetCacheability(HttpCacheability.ServerAndPrivate);
            c.SetExpires(HttpContext.Current.Timestamp.AddSeconds(60));
            c.VaryByHeaders["Accept"] = true;
            c.VaryByHeaders["Accept-Charset"] = true;
            c.VaryByHeaders["Accept-Encoding"] = true;
            c.VaryByParams["*"] = true;
        }
        [QueryInterceptor("Emp")]
        public Expression<Func<Emp, bool>> OnQueryEmployee()
        {
            var user = HttpContext.Current.User;
            if (user.IsInRole("Administrators"))
                return e => true;
            else
                return e => false;
        }
        [ChangeInterceptor("Emp")]
        public void OnChangeEmployee(Emp emp, UpdateOperations operation)
        {
            if (operation == UpdateOperations.Add || operation == UpdateOperations.Change)
            {
                var user = HttpContext.Current.User;
                if (!user.IsInRole("Administrators"))
                    throw new DataServiceException(401, "User do not have permission to change employee credentials");
            }
        }
    }
}
