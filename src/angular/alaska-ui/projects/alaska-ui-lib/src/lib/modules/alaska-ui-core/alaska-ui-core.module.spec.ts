import { AlaskaUiCoreModule } from './alaska-ui-core.module';

describe('AlaskaUiCoreModule', () => {
  let alaskaUiCoreModule: AlaskaUiCoreModule;

  beforeEach(() => {
    alaskaUiCoreModule = new AlaskaUiCoreModule();
  });

  it('should create an instance', () => {
    expect(alaskaUiCoreModule).toBeTruthy();
  });
});
