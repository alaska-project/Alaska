import { Component, OnInit } from '@angular/core';
import { CacheService, CacheInstance } from '../../services/cache/cache.service';
import { CacheUiService } from '../../services/cache-ui/cache-ui.service';
import { MatDialog } from '@angular/material/dialog';
import { CacheEntryDialogComponent } from '../cache-entry-dialog/cache-entry-dialog.component';

@Component({
  selector: 'alui-cache-admin',
  templateUrl: './cache-admin.component.html',
  styleUrls: [
    './cache-admin.component.css',
    '../../../../../../../../node_modules/bootstrap/dist/css/bootstrap.css'
  ]
})
export class CacheAdminComponent implements OnInit {

  servers: string[] = undefined;
  selectedServer: string = undefined;

  cacheInstances: CacheInstance[] = [];
  selectedCacheInstance: CacheInstance = undefined;

  cacheKeys: string[] = [];

  constructor(public dialog: MatDialog,
    private cacheService: CacheService, 
    private cacheUiService: CacheUiService) { }

  ngOnInit() {
    this.cacheUiService.getEndpoints()
      .then(x => {
        this.servers = x;
        this.selectedServer = this.servers.length > 0 ? this.servers[0] : undefined;
        this.updateCacheList();
      });
  }

  updateCacheList() {
    this.selectedCacheInstance = undefined;
    this.cacheService.getCacheInstances(this.selectedServer)
      .then(x => {
        this.cacheInstances = x;
        this.selectedCacheInstance = this.cacheInstances.length > 0 ?
         this.cacheInstances[0] :
         undefined;
        this.updateCacheInfo();
      });
  }
  
  updateCacheInfo() {
    this.cacheService.getCacheKeys(this.selectedServer, this.selectedCacheInstance.cacheId)
      .then(x => {
        this.cacheKeys = x;
      });
  }

  clearCache() {
    this.cacheService.clearCache(this.selectedServer, this.selectedCacheInstance.cacheId)
      .then(x => {
        this.updateCacheInfo();
      });
  }

  deleteEntry(cacheKey) {
    this.cacheService.removeCacheEntry(this.selectedServer, this.selectedCacheInstance.cacheId, cacheKey)
      .then(x => {
        this.updateCacheInfo();
      });
  }

  displayEntryValue(cacheKey) {
    this.cacheService.getCacheEntry(this.selectedServer, this.selectedCacheInstance.cacheId, cacheKey)
    .then(x => {
      let data = x ? {
        key: x.key,
        expiration: x.expiration,
        expirationTime: x.expirationTime,
        value: x.value ? x.value : x.serializedValue
      } : {
        key: cacheKey,
        expiration: '',
        expirationTime: undefined,
        value: ''
      }
      const dialogRef = this.dialog.open(CacheEntryDialogComponent, {
        width: '450px',
        data: data
      });
    });
  }
}
