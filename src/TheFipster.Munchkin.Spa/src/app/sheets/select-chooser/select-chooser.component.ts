import { Component, OnInit, Input, Output, Inject } from '@angular/core';
import { MAT_BOTTOM_SHEET_DATA, MatBottomSheetRef } from '@angular/material/bottom-sheet';

@Component({
  selector: 'app-select-chooser',
  templateUrl: './select-chooser.component.html',
  styleUrls: ['./select-chooser.component.scss']
})
export class SelectChooserComponent implements OnInit {
  title: string;
  originalOptions: string[];
  options: any[] = [];
  values: any[] = [];

  constructor(
    @Inject(MAT_BOTTOM_SHEET_DATA) public data: any,
    private bottomSheetRef: MatBottomSheetRef<SelectChooserComponent>
    ) {
    this.title = data.title;
    this.originalOptions = data.values;
    data.options.forEach(option => {
      if (data.values.indexOf(option) !== -1) {
        this.options.push({ value: option, selected: true});
      } else {
        this.options.push({ value: option, selected: false});
      }
    });
  }

  ngOnInit() {  }

  save(selection: any[]) {
    const newOptions: string[] = selection.map((item: any) => item.value);
    const removedItems: string[] = this.getRemovedOptions(newOptions);
    const addedItems: string[] = this.getAddedOptions(newOptions);
    this.bottomSheetRef.dismiss({ add: addedItems, remove: removedItems});
  }

  private getAddedOptions(newOptions: string[]) {
    const addedItems: string[] = [];
    newOptions.forEach(element => {
      if (this.originalOptions.indexOf(element) === -1) {
        addedItems.push(element);
      }
    });
    return addedItems;
  }

  private getRemovedOptions(newOptions: string[]) {
    const removedItems: string[] = [];
    this.originalOptions.forEach(element => {
      if (newOptions.indexOf(element) === -1) {
        removedItems.push(element);
      }
    });
    return removedItems;
  }
}
