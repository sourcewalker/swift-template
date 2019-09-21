﻿using Core.Model;
using Core.Service.Interfaces;
using Core.Shared.DTO;
using GraphQL.Types;

namespace $safeprojectname$
{
    public class ParticipationType : ObjectGraphType<ParticipationDto>
    {
        public ParticipationType(ISiteService siteService)
        {
            var typeName = nameof(Participation);

            Field(x => x.Id, type: typeof(IdGraphType))
                .Description($"Id property from the {typeName} object.");
            Field(x => x.SiteId, type: typeof(IdGraphType))
                .Description($"SiteId property from the {typeName} object.");
            Field(x => x.Status, type: typeof(StringGraphType))
                .Description($"Status property from the {typeName} object.");
            Field(x => x.ApiStatus, type: typeof(StringGraphType))
                .Description($"Api Status property from the {typeName} object.");
            Field(x => x.ApiMessage, type: typeof(StringGraphType))
                .Description($"Api Message property from the {typeName} object.");
            Field(x => x.CreatedDate, type: typeof(DateTimeOffsetGraphType))
                .Description($"Created date property from the {typeName} object.");
            Field(x => x.ModifiedDate, type: typeof(DateTimeOffsetGraphType))
                .Description($"Modified date property from the {typeName} object.");
            Field<SiteType>(
                "site",
                resolve: context => siteService.GetSite(context.Source.SiteId.Value)
            );
        }
    }
}
