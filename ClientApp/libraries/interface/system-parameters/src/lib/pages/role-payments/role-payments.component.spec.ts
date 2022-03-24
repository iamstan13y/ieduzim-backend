import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RolePaymentsComponent } from './role-payments.component';

describe('RolePaymentsComponent', () => {
  let component: RolePaymentsComponent;
  let fixture: ComponentFixture<RolePaymentsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RolePaymentsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RolePaymentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
