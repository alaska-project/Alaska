
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UriUtil {

  formatUri(basePath: string, queryParameters: any) {
    var params: string[] = [];
    for (let key in queryParameters) {
      params.push(key + "=" + encodeURIComponent(queryParameters[key]));
    }
    return basePath + "?" + params.join("&");
  }

  combine(basePath: string, other: string) {
    if (!basePath) {
      throw 'Empty basePath';
    }
    return this.removeSlashSuffix(basePath) + this.ensureSlashPrefix(other);
  }

  private removeSlashSuffix(value: string) {
    return value.endsWith('/') ?
      value.substring(0, value.length - 1) :
      value;
  }

  private ensureSlashPrefix(value: string) {
    return value.startsWith('/') ?
      value :
      '/' + value;
  }
}
