import { Component, OnInit } from '@angular/core';
import { Farm, FARMS } from '../farm';

@Component({
  selector: 'app-farms-parent',
  templateUrl: './farms-parent.component.html',
  styleUrls: ['./farms-parent.component.css']
})
export class FarmsParentComponent implements OnInit {

  farms = FARMS;

  constructor() { }

  ngOnInit() {
  }
}
