﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Squidex.Infrastructure;
using Squidex.Infrastructure.Translations;

namespace Squidex.Domain.Apps.Core.ValidateContent.Validators
{
    public sealed class AllowedValuesValidator<TValue> : IValidator
    {
        private readonly IEnumerable<TValue> allowedValues;

        public AllowedValuesValidator(params TValue[] allowedValues)
            : this((IEnumerable<TValue>)allowedValues)
        {
        }

        public AllowedValuesValidator(IEnumerable<TValue> allowedValues)
        {
            Guard.NotNull(allowedValues);

            this.allowedValues = allowedValues;
        }

        public ValueTask ValidateAsync(object? value, ValidationContext context, AddError addError)
        {
            if (value is TValue typedValue && !allowedValues.Contains(typedValue))
            {
                addError(context.Path, T.Get("contents.validation.notAllowed"));
            }

            return default;
        }
    }
}
