import { FieldType, IField } from "@iedu-ui-forms"

export const PERIODSFIELDS = function get(item?: any): IField[]{
    return [
        { label: 'Number Of Days', name: 'numberOfDays', required: false, value: item ? item.numberOfDays : null, disabled: false,
        type: FieldType.Number}
    ]
}
