/*
 * Squidex Headless CMS
 *
 * @license
 * Copyright (c) Squidex UG (haftungsbeschränkt). All rights reserved.
 */

import { FormControl, Validators } from '@angular/forms';
import { debounceTime, map, shareReplay } from 'rxjs/operators';
import { Form, hasNoValue$, Types, ExtendedFormGroup, value$ } from '@app/framework';
import { AssignContributorDto } from './../services/contributors.service';
import { UserDto } from './../services/users.service';

export class AssignContributorForm extends Form<ExtendedFormGroup, AssignContributorDto> {
    public get user() {
        return this.form.controls['user'];
    }

    public hasNoUser = hasNoValue$(this.user);

    constructor() {
        super(new ExtendedFormGroup({
            user: new FormControl('',
                Validators.required,
            ),
            role: new FormControl('',
                Validators.required,
            ),
        }));
    }

    protected transformSubmit(value: any) {
        let contributorId = value.user;

        if (Types.is(contributorId, UserDto)) {
            contributorId = contributorId.id;
        }

        return { contributorId, role: value.role, invite: true };
    }
}

type ImportContributorsFormType = ReadonlyArray<AssignContributorDto>;

export class ImportContributorsForm extends Form<ExtendedFormGroup, ImportContributorsFormType> {
    public get import() {
        return this.form.controls['import'];
    }

    public numberOfEmails = value$(this.import).pipe(debounceTime(100), map(v => extractEmails(v).length), shareReplay(1));

    public hasNoUser = this.numberOfEmails.pipe(map(v => v === 0));

    constructor() {
        super(new ExtendedFormGroup({
            import: new FormControl('',
                Validators.required,
            ),
        }));
    }

    protected transformSubmit(value: any) {
        return extractEmails(value.import);
    }
}

function extractEmails(value: string) {
    const result: AssignContributorDto[] = [];

    if (value) {
        const added: { [email: string]: boolean } = {};

        const emails = value.match(EMAIL_REGEX);

        if (emails) {
            for (const match of emails) {
                if (!added[match]) {
                    result.push({ contributorId: match, role: 'Editor', invite: true });

                    added[match] = true;
                }
            }
        }
    }

    return result;
}

// eslint-disable-next-line no-useless-escape
const EMAIL_REGEX = /(?=.{1,254}$)(?=.{1,64}@)[-!#$%&'*+\/0-9=?A-Z^_`a-z{|}~]+(\.[-!#$%&'*+\/0-9=?A-Z^_`a-z{|}~]+)*@[A-Za-z0-9]([A-Za-z0-9-]{0,61}[A-Za-z0-9])?(\.[A-Za-z0-9]([A-Za-z0-9-]{0,61}[A-Za-z0-9])?)*/gim;
