﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Squidex.Infrastructure.Reflection;

namespace Squidex.Infrastructure.States
{
    public sealed class DefaultStreamNameResolver : IStreamNameResolver
    {
        private static readonly string[] Suffixes = { "Grain", "DomainObject", "State" };

        public string GetStreamName(Type aggregateType, string id)
        {
            Guard.NotNullOrEmpty(id);
            Guard.NotNull(aggregateType);

            return $"{aggregateType.TypeName(true, Suffixes)}-{id}";
        }
    }
}
