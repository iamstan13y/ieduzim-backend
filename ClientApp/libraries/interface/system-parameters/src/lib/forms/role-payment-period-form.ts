import { FieldType, IField } from "@iedu-ui-forms"

export const ROLEPAYMENTPERIODFIELDS = function get(item?: any): IField[] {
    return [
        {
            label: 'Role', name: 'roleId', required: true, value: item ? item.roleId : 0, type: FieldType.Select,
            options: {
                data: [],
                url: `Roles`,
                name: 'name',
                value: 'id'
            }
        },
        {
            label: 'Payment Period', name: 'paymentPeriodId', required: true, value: item ? item.paymentPeriod.id : 0, type: FieldType.Select,
            options: {
                data: [],
                url: `PaymentPeriods`,
                name: 'name',
                value: 'id'
            }
        },
        {
            label: 'Payment Description', name: 'paymentDescription', required: true, value: item ? item.paymentDescription : null, type: FieldType.Text,
        },
        ...item.role.name == 'Teacher' ?
            [
                {
                    label: 'Currency', name: 'currency', required: true, value: item ? item.currency : null, type: FieldType.Select,
                    options: {
                        data: [],
                        url: `Currency`,
                        name: 'name',
                        value: 'code'
                    }
                },
                {
                    label: 'Total Amount', name: 'totalAmount', required: true, value: item ? item.totalAmount : null, type: FieldType.Number,
                }
            ] : []
    ]
}