import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'race'
})
export class RacePipe implements PipeTransform {

  transform(value: string[]): string {
    if (value.length === 0) {
      return 'Mensch';
    }
    return value.join(', ');
  }

}
