/*
 * Squidex Headless CMS
 *
 * @license
 * Copyright (c) Squidex UG (haftungsbeschränkt). All rights reserved.
 */

/* eslint-disable no-useless-escape */

import { FormControl, Validators } from '@angular/forms';
import { Form, hasNoValue$, ExtendedFormGroup, ValidatorsEx } from '@app/framework';
import { StartRestoreDto } from './../services/backups.service';

export class RestoreForm extends Form<ExtendedFormGroup, StartRestoreDto> {
    public get url() {
        return this.form.controls['url'];
    }

    public hasNoUrl = hasNoValue$(this.url);

    constructor() {
        super(
            new ExtendedFormGroup({
                name: new FormControl('', [
                    Validators.maxLength(40),
                    ValidatorsEx.pattern('[a-z0-9]+(\-[a-z0-9]+)*', 'i18n:apps.appNameValidationMessage'),
                ]),
                url: new FormControl('',
                    Validators.required,
                ),
            }),
        );
    }
}
