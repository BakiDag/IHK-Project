import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-wochenbericht-liste',
  templateUrl: './wochenbericht-liste.component.html',
  styleUrls: ['./wochenbericht-liste.component.scss']
})
export class WochenberichtListeComponent implements OnInit {
  public wochenberichtListe:string;
  constructor() { }

  ngOnInit(): void {
  }

}
