import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CoreComponent } from './core.component';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { routerReducer, StoreRouterConnectingModule } from '@ngrx/router-store';
import { NgZorroAntdModule } from 'ng-zorro-antd';
import { CORESERVICES, AuthService } from './guards';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';

export const STATE_KEY = 'router';


@NgModule({
  declarations: [CoreComponent],
  imports: [
    StoreModule.forRoot({router: routerReducer}),
    JwtModule.forRoot({
      config: { tokenGetter: token, skipWhenExpired: true }
    }),
    StoreRouterConnectingModule.forRoot({stateKey: STATE_KEY}),
    EffectsModule.forRoot([]),
    NgZorroAntdModule
  ],
  exports: [CoreComponent],
  providers: [...CORESERVICES, JwtHelperService]
})


export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    throwIfAlreadyLoaded(parentModule, 'CoreModule');
  }
}

export function token(): string { return AuthService.TOKEN }

function throwIfAlreadyLoaded(parentModule: any, moduleName: string) {
  if (!parentModule) return;
  throw new Error(`${moduleName} has already been loaded. Import Core modules in the AppModule only.`);
}
