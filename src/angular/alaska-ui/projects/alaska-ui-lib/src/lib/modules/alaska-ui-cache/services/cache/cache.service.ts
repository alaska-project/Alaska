import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UriUtil } from '../../../alaska-ui-core/utils/uri-utils';
import { NotificationService } from '../../../alaska-ui-core/services/notification.service';

@Injectable({
  providedIn: 'root'
})
export class CacheService {

  private apiRelativeUrl = '/alaska/api/cache';

  constructor(
    private http: HttpClient,
    private uriUtil: UriUtil,
    private notificationService: NotificationService) {
  }

  getCacheInstances(baseAddress: string): Promise<CacheInstance[]> {
    const url = this.getApiUrl(baseAddress, 'GetCacheInstances');
    return this.http.get<CacheInstance[]>(url)
      .toPromise()
      .catch(x => this.handleError(x));
  }

  getCacheKeys(baseAddress: string, cacheId: string): Promise<string[]> {
    const url = this.getApiUrl(baseAddress, 'GetCacheKeys', {
      cacheId: cacheId
    });
    return this.http.get<string[]>(url)
      .toPromise()
      .catch(x => this.handleError(x));
  }

  getCacheEntry(baseAddress: string, cacheId: string, cacheKey: string): Promise<CacheItem> {
    const url = this.getApiUrl(baseAddress, 'GetCacheEntry', {
      cacheId: cacheId,
      cacheKey: cacheKey
    });
    return this.http.get<CacheItem>(url)
      .toPromise()
      .catch(x => this.handleError(x));
  }

  removeCacheEntry(baseAddress: string, cacheId: string, cacheKey: string): Promise<void> {
    const url = this.getApiUrl(baseAddress, 'RemoveCacheEntry', {
      cacheId: cacheId,
      cacheKey: cacheKey
    });
    return this.http.post<void>(url, undefined)
      .toPromise()
      .catch(x => this.handleError(x));
  }

  clearCache(baseAddress: string, cacheId: string): Promise<void> {
    const url = this.getApiUrl(baseAddress, 'ClearCache', {
      cacheId: cacheId
    });
    return this.http.post<void>(url, undefined)
      .toPromise()
      .catch(x => this.handleError(x));
  }

  private handleError<T> (error: any): Promise<T> {
    this.notificationService.showErrorDialog({
      title: 'Cache Error',
      message: error.message,
      errorDetails: error.stactTrage
    });
    throw(error);
  }

  private getApiUrl (baseAddress: string, action: string, queryParameters?: any) {
    const endpoint = this.getApiEndpoint(baseAddress);
    const actionUrl = this.uriUtil.combine(endpoint, action);
    if (!queryParameters) {
      return actionUrl;
    }
    return this.uriUtil.formatUri(actionUrl, queryParameters);
  }

  private getApiEndpoint (baseAddress: string) {
    return this.uriUtil.combine(baseAddress, this.apiRelativeUrl);
  }
}


export class CacheInstance {
  cacheId?: string | undefined;
  cacheEngine?: string | undefined;
  isDisabled?: boolean | undefined;
  keys?: string[] | undefined;
}

export class CacheItem {
  value?: any | undefined;
  serializedValue?: any | undefined;
  key?: string | undefined;
  expiration?: string | undefined;
  expirationTime?: Date | undefined;
}

