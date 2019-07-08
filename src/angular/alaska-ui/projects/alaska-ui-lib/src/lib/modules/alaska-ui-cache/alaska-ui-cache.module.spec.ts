import { AlaskaUiCacheModule } from './alaska-ui-cache.module';

describe('AlaskaUiCacheModule', () => {
  let alaskaUiCacheModule: AlaskaUiCacheModule;

  beforeEach(() => {
    alaskaUiCacheModule = new AlaskaUiCacheModule();
  });

  it('should create an instance', () => {
    expect(alaskaUiCacheModule).toBeTruthy();
  });
});
