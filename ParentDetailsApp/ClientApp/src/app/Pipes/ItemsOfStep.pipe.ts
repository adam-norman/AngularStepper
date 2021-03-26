import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'ItemsOfStep'
})
export class ItemsOfStepPipe implements PipeTransform {

  transform(items: any[], stepId: number): any {
    if (!items || !stepId) {
      return items;
  }
  // filter items array, items which match and return true will be
  // kept, false will be filtered out
  return items.filter(item => item.stepId === stepId);
  }

}
