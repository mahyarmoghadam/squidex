﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Squidex.Infrastructure.Translations;

namespace Squidex.Domain.Apps.Core.ValidateContent.Validators
{
    public sealed class RangeValidator<TValue> : IValidator where TValue : struct, IComparable<TValue>
    {
        private readonly TValue? min;
        private readonly TValue? max;

        public RangeValidator(TValue? min, TValue? max)
        {
            if (min != null && max != null && min.Value.CompareTo(max.Value) > 0)
            {
                throw new ArgumentException("Min value must be greater than max value.", nameof(min));
            }

            this.min = min;
            this.max = max;
        }

        public ValueTask ValidateAsync(object? value, ValidationContext context, AddError addError)
        {
            if (value is TValue typedValue)
            {
                if (min != null && max != null)
                {
                    if (Equals(min, max) && Equals(min.Value, max.Value))
                    {
                        addError(context.Path, T.Get("contents.validation.exactValue", new { value = max.Value }));
                    }
                    else if (typedValue.CompareTo(min.Value) < 0 || typedValue.CompareTo(max.Value) > 0)
                    {
                        addError(context.Path, T.Get("contents.validation.between", new { min, max }));
                    }
                }
                else
                {
                    if (min != null && typedValue.CompareTo(min.Value) < 0)
                    {
                        addError(context.Path, T.Get("contents.validation.min", new { min }));
                    }

                    if (max != null && typedValue.CompareTo(max.Value) > 0)
                    {
                        addError(context.Path, T.Get("contents.validation.max", new { max }));
                    }
                }
            }

            return default;
        }
    }
}
