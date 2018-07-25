import { Component, OnInit, Input } from '@angular/core';
import { Farm } from '../farm';

@Component({
  selector: '[app-farms-child]',
  templateUrl: './farms-child.component.html',
  styleUrls: ['./farms-child.component.css']
})
export class FarmsChildComponent implements OnInit {
  // @Input() farms: Farm[];
  @Input() rowData: Farm;

  constructor() { }

  ngOnInit() {
  }

}
