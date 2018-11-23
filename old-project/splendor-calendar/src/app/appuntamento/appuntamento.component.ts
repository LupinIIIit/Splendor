import { Component, OnInit } from '@angular/core';
import { AziendeService, Azienda } from '../api';

@Component({
  selector: 'app-appuntamento',
  templateUrl: './appuntamento.component.html',
  styleUrls: ['./appuntamento.component.css']
})
export class AppuntamentoComponent implements OnInit {
  aziende: Azienda[];
  azienda: Azienda;
  constructor(protected az: AziendeService) { }

  ngOnInit() {
    this.az.getAziende().subscribe(items => this.aziende = items);
  }
}
