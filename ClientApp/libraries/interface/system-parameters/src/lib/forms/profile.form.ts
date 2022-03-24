import { IField, FieldType } from '@iedu-ui-forms';

export const EMAILFIELD = function get(item): IField[]{
    return [
        { label: 'Email', name: 'email', required: true, value: item ? item.email : null, type: FieldType.Email}
    ]
}

export const PHONENUMBERFIELD = function get(item?: any): IField[]{
    return [
        { label: 'Phone Number', name: 'phoneNumber', required: false, value: item ? item.phoneNumber : null}
    ]
}