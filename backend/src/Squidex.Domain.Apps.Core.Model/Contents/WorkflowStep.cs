﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Squidex.Infrastructure.Collections;

namespace Squidex.Domain.Apps.Core.Contents
{
    public sealed record WorkflowStep
    {
        public ReadonlyDictionary<Status, WorkflowTransition> Transitions { get; } = ReadonlyDictionary.Empty<Status, WorkflowTransition>();

        public string? Color { get; }

        public NoUpdate? NoUpdate { get; }

        public WorkflowStep(ReadonlyDictionary<Status, WorkflowTransition>? transitions = null, string? color = null, NoUpdate? noUpdate = null)
        {
            Color = color;

            if (transitions != null)
            {
                Transitions = transitions;
            }

            NoUpdate = noUpdate;
        }
    }
}
