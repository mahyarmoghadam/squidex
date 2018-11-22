﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschränkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Squidex.Infrastructure;
using System.Collections.Immutable;

namespace Squidex.Domain.Apps.Core.Schemas
{
    [TypeName("TagsField")]
    public sealed class TagsFieldProperties : FieldProperties
    {
        public ImmutableList<string> AllowedValues { get; set; }

        public int? MinItems { get; set; }

        public int? MaxItems { get; set; }

        public TagsFieldEditor Editor { get; set; }

        public TagsFieldNormalization Normalization { get; set; }

        public override T Accept<T>(IFieldPropertiesVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override T Accept<T>(IFieldVisitor<T> visitor, IField field)
        {
            return visitor.Visit((IField<TagsFieldProperties>)field);
        }

        public override RootField CreateRootField(long id, string name, Partitioning partitioning)
        {
            return Fields.Tags(id, name, partitioning, this);
        }

        public override NestedField CreateNestedField(long id, string name)
        {
            return Fields.Tags(id, name, this);
        }
    }
}
