import { Component, OnInit } from '@angular/core';
import { Appuntamento, Azienda } from '../api/models/models';
import { AziendeService } from '../api/services/api'
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  aziende: Azienda[];
  constructor(private az: AziendeService) { }
  ngOnInit() {
    this.loadData();
  }
  loadData(): void {
    this.az.getAziende().subscribe(az => this.aziende = az);
  }
} 