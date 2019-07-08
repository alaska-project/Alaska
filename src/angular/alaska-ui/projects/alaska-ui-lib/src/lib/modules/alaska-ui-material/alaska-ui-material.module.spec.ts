import { AlaskaUiMaterialModule } from './alaska-ui-material.module';

describe('AlaskaUiMaterialModule', () => {
  let alaskaUiMaterialModule: AlaskaUiMaterialModule;

  beforeEach(() => {
    alaskaUiMaterialModule = new AlaskaUiMaterialModule();
  });

  it('should create an instance', () => {
    expect(alaskaUiMaterialModule).toBeTruthy();
  });
});
