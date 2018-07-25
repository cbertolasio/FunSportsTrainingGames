import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FarmsParentComponent } from './farms-parent.component';

describe('FarmsParentComponent', () => {
  let component: FarmsParentComponent;
  let fixture: ComponentFixture<FarmsParentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FarmsParentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FarmsParentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
