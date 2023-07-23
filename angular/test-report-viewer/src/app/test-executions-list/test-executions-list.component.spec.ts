import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestExecutionsListComponent } from './test-executions-list.component';

describe('TestExecutionsListComponent', () => {
  let component: TestExecutionsListComponent;
  let fixture: ComponentFixture<TestExecutionsListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TestExecutionsListComponent]
    });
    fixture = TestBed.createComponent(TestExecutionsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
