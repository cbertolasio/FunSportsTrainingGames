import { Component, OnInit } from '@angular/core';
import { Farm, FARMS } from '../farm';

@Component({
  selector: 'app-farms-parent',
  templateUrl: './farms-parent.component.html',
  styleUrls: ['./farms-parent.component.css']
})
export class FarmsParentComponent implements OnInit {

  farms = FARMS;

  onDeleted(data: Farm) {
    console.log('deleting farm: ' + data.name);
    const index = this.farms.indexOf(data);
    this.farms.splice(index, 1);
  }

  constructor() { }

  ngOnInit() {
  }
}
