import { FieldType, IField } from "@iedu-ui-forms"

export const CODEEFIELDS = function get(item?: any, disabled?): IField[]{
    return [
        { label: 'Code', name: 'code', required: true, value: item ? item.code : null, disabled: false,
        type: FieldType.Text}
    ]
}
