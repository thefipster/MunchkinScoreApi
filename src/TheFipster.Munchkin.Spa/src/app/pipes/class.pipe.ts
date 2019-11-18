import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'class'
})
export class ClassPipe implements PipeTransform {

  transform(value: string[]): string {
    if (value.length === 0) {
      return '---';
    }

    return value.join(', ');
  }

}
