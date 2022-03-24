import { Component, OnInit, Input, ChangeDetectorRef } from '@angular/core';
import { MenuItem } from 'libraries/core/src/lib/models/menu-item';
import { Roles } from '@iedu-data-accounts';
import { AuthService } from '@iedu/core';

@Component({
  selector: 'top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.less']
})
export class TopMenuComponent implements OnInit {
  @Input() menuItems: MenuItem[];
  @Input() links: MenuItem[];
  show: boolean = false;
  searchValue
  visible: boolean = false;
  change: boolean;
  messages: any[] = [];
  messagesMenu: { name: string; icon: string; routerLink: string; role: Roles[]; authenticated: boolean; count: any; badgeColor: string; }[];
  quotations: any;
  alert: any;
  user: any;
  waiting: boolean;
  constructor(public authService: AuthService, private cdr: ChangeDetectorRef) { }

  ngOnInit() {
    this.loadMenu();
  }

  toggleDrawer(visible: boolean) {
    this.visible = visible;
  }

  search() { }

  onSelect() { }

  loadMenu() {
    this.user = this.authService.tokenPayload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']
      ? this.authService.tokenPayload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'].charAt(0).toUpperCase() : 'U';
    this.menuItems = [
      {
        name: ``,
        icon: 'user',
        routerLink: '#',
        authenticated: true,
        role: [Roles.Client, Roles.Admin],
        subMenuItems: [
          { name: 'Change Password', icon: 'edit' },
          { name: 'Sign Out', routerLink: '/account/logout', icon: 'logout' }
        ]
      },
      {
        name: 'System Parameters',
        icon: 'setting',
        routerLink: '/admin/system-parameters',
        authenticated: true,
        role: [Roles.Admin]
      },
      {
        name: 'Profile',
        icon: 'idcard',
        routerLink: '/',
        role: [Roles.Admin],
        authenticated: true
      },
    ]

    if (!this.authService.token || !this.authService.isAuthenticated())
      this.menuItems = this.menuItems.filter(a => !a.authenticated)
    else if (this.authService.isAdmin())
      this.menuItems = this.menuItems.filter(a => a.role.indexOf(Roles.Admin) > -1 && (a.authenticated || a.authenticated == undefined))
    else this.menuItems = this.menuItems.filter(a => a.role.indexOf(Roles.Client) > -1 && (a.authenticated || a.authenticated == undefined))
  }

  changeClick(change) {
    this.change = change;
  }
}
