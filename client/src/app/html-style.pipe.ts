import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

@Pipe({
  name: 'htmlStyle'
})
export class HtmlStylePipe implements PipeTransform {

  constructor(private sanitized: DomSanitizer){}

  transform(value: any) {
    return this.sanitized.bypassSecurityTrustHtml(value);
  }

}
