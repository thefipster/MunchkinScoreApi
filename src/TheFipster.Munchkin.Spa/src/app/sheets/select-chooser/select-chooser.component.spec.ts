import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectChooserComponent } from './select-chooser.component';

describe('SelectChooserComponent', () => {
  let component: SelectChooserComponent;
  let fixture: ComponentFixture<SelectChooserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelectChooserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectChooserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
