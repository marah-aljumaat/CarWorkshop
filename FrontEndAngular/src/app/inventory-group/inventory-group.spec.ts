import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InventoryGroup } from './inventory-group';

describe('InventoryGroup', () => {
  let component: InventoryGroup;
  let fixture: ComponentFixture<InventoryGroup>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InventoryGroup]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InventoryGroup);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
