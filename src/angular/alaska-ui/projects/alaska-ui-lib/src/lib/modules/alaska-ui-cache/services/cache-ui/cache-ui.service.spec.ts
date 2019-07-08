import { TestBed, inject } from '@angular/core/testing';

import { CacheUiService } from './cache-ui.service';

describe('CacheUiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CacheUiService]
    });
  });

  it('should be created', inject([CacheUiService], (service: CacheUiService) => {
    expect(service).toBeTruthy();
  }));
});
