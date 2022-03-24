
import { FieldType } from './field-type.enum';
import { IFieldCompare } from './ifield-compare';
import { PageRequest, PageResult } from '@iedu/core';

export interface IField {
    label: string;
    icon?: string;
    value?: any;
    type?: FieldType;
    name: string;
    required: boolean;
    options?: Options;
    source?: string;
    validation?: any;
    subSelection?: string;
    compare?: IFieldCompare;
    col?: string;
    disabled?: boolean;
}

export interface Options{
    url?: string;
    value?: any;
    name?: string;
    data?: any;
    pageRequest?: PageRequest;
    pageResult ?: PageResult<any>;
}