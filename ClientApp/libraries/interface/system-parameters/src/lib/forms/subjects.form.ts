import { FieldType, IField } from "@iedu-ui-forms"

export const SUBJECTSFIELDS = function get(item?: any): IField[]{
    return [
        { 
            label: 'Currency', name: 'currencyId', required: true, value: item ? item.currencyId : 0, type: FieldType.Select,
            options:{
                data: [],
                url: `Currency`,
                name: 'name',
                value: 'id'
            }
        },
        { 
            label: 'Price', name: 'price', required: true, value: item ? item.price : null, disabled: false, type: FieldType.Number
        }
    ]
}