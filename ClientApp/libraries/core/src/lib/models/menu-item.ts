import { IForm } from '@iedu-ui-forms';

export interface MenuItem {
  name: string;
  keywords?: string;
  icon?: string;
  routerLink?: string;
  subMenuItems?: MenuItem[];
  isOpen?: boolean;
  role?: Array<string>;
  authenticated?: boolean;
  count?: number;
  badgeColor?: string;
}
