import { TestBed } from '@angular/core/testing';

import { BerichtRequestService } from './bericht-request.service';

describe('BerichtRequestService', () => {
  let service: BerichtRequestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BerichtRequestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
