import { Component, OnInit, Input } from '@angular/core';
import { Azienda, Appuntamento } from '../../api';
import { AppuntamentiService } from '../../api/services/api'
@Component({
  selector: 'app-today',
  templateUrl: './today.component.html',
  styleUrls: ['./today.component.css']
})
export class TodayComponent implements OnInit {
  @Input() azienda: Azienda;
  lists: Appuntamento[];
  constructor(private app: AppuntamentiService) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    if (this.azienda !== null && this.azienda !== undefined) {
      this.app.getList(this.azienda.AziendaId).subscribe(list => this.lists = list);
    }
  }
}
