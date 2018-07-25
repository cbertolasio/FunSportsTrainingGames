import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FarmsChildComponent } from './farms-child.component';

describe('FarmsChildComponent', () => {
  let component: FarmsChildComponent;
  let fixture: ComponentFixture<FarmsChildComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FarmsChildComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FarmsChildComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
