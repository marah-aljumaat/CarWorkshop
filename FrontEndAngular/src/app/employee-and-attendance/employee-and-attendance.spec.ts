import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeAndAttendance } from './employee-and-attendance';

describe('EmployeeAndAttendance', () => {
  let component: EmployeeAndAttendance;
  let fixture: ComponentFixture<EmployeeAndAttendance>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeeAndAttendance]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeeAndAttendance);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
