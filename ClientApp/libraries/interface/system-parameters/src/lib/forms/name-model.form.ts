import { IField, FieldType } from '@iedu-ui-forms';

export const NAMEFIELDS = function get(item?: any, disabled?): IField[]{
    return [
        { label: 'Name', name: 'name', required: true, value: item ? item.name : null, disabled: disabled ? disabled : false,
        type: FieldType.Text}
    ]
}

export const ACTIVEFIELDS = function get(item?: any): IField[]{
    return [
        { label: 'Active', name: 'active', required: false, value: item ? item.active : false, type: FieldType.Boolean}
    ]
}