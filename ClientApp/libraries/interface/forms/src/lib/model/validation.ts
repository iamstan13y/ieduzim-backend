import { FormGroup } from '@angular/forms';
import { IField } from './ifield';

export class Validation {
    fields: IField[];
    constructor(fields: IField[]) {
        this.fields = fields;
    }

    static validateField(group: FormGroup, field: IField) {
        const ctl1 = group.controls[field.name];
        const ctl2 = group.controls[field.compare.name];
        if (!ctl1 || ctl1.errors || !ctl2 || ctl2.errors) return;
        if (field.compare.value == 0)
            ctl1.setErrors(ctl1.value === ctl2.value ? null : { compare: true });
        else if (field.compare.value < 0)
            ctl1.setErrors(ctl1.value < ctl2.value ? null : { compare: true });
        else
            ctl1.setErrors(ctl1.value > ctl2.value ? null : { compare: true });
    }
}


export function FieldComparer(fields: IField[]) {
    return (formGroup: FormGroup) => {
        fields.filter(f => f.compare != null).forEach(field => Validation.validateField(formGroup, field));
    }
}