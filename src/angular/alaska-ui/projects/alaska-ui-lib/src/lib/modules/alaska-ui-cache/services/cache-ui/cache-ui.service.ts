import { Injectable, Optional, Inject, InjectionToken } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UriUtil } from '../../../alaska-ui-core/utils/uri-utils';
import { NotificationService } from '../../../alaska-ui-core/services/notification.service';

export const CACHE_API_BASE_URL = new InjectionToken<string>('CACHE_API_BASE_URL');

@Injectable({
  providedIn: 'root'
})
export class CacheUiService {

  private baseAddress: string = undefined;
  private apiRelativeUrl = '/alaska/api/cacheUi';

  constructor(private http: HttpClient,
    private uriUtil: UriUtil,
    private notificationService: NotificationService,
    @Optional() @Inject(CACHE_API_BASE_URL) baseAddress?: string) {
    if (baseAddress) {
      this.baseAddress = baseAddress;
    }
  }

  getEndpoints(): Promise<string[]> {
    const url = this.getApiUrl('GetEndpoints');
    return this.http.get<string[]>(url)
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

  private getApiUrl (action, queryParameters?: any) {
    const endpoint = this.getApiEndpoint();
    const actionUrl = this.uriUtil.combine(endpoint, action);
    if (!queryParameters) {
      return actionUrl;
    }
    return this.uriUtil.formatUri(actionUrl, queryParameters);
  }

  private getApiEndpoint () {
    return this.baseAddress ?
      this.uriUtil.combine(this.baseAddress, this.apiRelativeUrl) :
      this.apiRelativeUrl;
  }
}
