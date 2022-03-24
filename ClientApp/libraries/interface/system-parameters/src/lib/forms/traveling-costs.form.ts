import { FieldType, IField } from "@iedu-ui-forms"

export const TRAVELINGCOSTSFIELDS = function get(item?: any): IField[]{
    return [
        { 
            label: 'Distance Lower Range', name: 'distanceLowerRange', required: true, value: item ? item.distanceLowerRange : null, disabled: false, type: FieldType.Number
        },
        { 
            label: 'Distance Upper Range', name: 'distanceUpperRange', required: true, value: item ? item.distanceUpperRange : null, disabled: false, type: FieldType.Number
        },
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