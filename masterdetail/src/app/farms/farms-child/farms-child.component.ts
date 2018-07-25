import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Farm } from '../farm';

@Component({
  selector: '[app-farms-child]',
  templateUrl: './farms-child.component.html',
  styleUrls: ['./farms-child.component.css']
})
export class FarmsChildComponent implements OnInit {
  // @Input() farms: Farm[];
  @Input() rowData: Farm;
  @Output() farmDeletedEvent: EventEmitter<Farm> =  new EventEmitter<Farm>();

  deleteFarm(data: Farm) {
    console.log('emitting event to delete a farm ' + data.name);

    this.farmDeletedEvent.emit(data);
  }

  constructor() { }

  ngOnInit() {
  }

}
