import { Component, ChangeDetectionStrategy, ViewChild, TemplateRef, OnInit, Input, OnChanges, SimpleChanges, SimpleChange } from '@angular/core';
import { startOfDay, endOfDay, subDays, addDays, endOfMonth, isSameDay, isSameMonth, addHours, endOfWeek, startOfMonth, startOfWeek, format } from 'date-fns';
import { Subject } from 'rxjs/Subject';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CalendarEvent, CalendarEventAction, CalendarEventTimesChangedEvent, CalendarDateFormatter, DAYS_OF_WEEK } from 'angular-calendar';
import { AppuntamentiService, Appuntamento, MessageService } from '../../api';
import { DateFormatter } from './date-formatter.provider';
import { Observable } from 'rxjs/Observable';
import { HttpParams, HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators/map';
import { of } from 'rxjs/observable/of';
import { tap, catchError } from 'rxjs/operators';
interface Film {
  id: number;
  title: string;
  release_date: string;
}
@Component({
  selector: 'app-calendar',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css'],
  providers: [{ provide: CalendarDateFormatter, useClass: DateFormatter }]
})
export class CalendarComponent implements OnChanges, OnInit {
  @Input() AziendaId: number;
  @ViewChild('modalContent') modalContent: TemplateRef<any>;
  view: string = 'month';
  viewDate: Date = new Date();
  activeDayIsOpen: boolean = true;
  locale: string = 'it';
  weekStartsOn: number = DAYS_OF_WEEK.MONDAY;
  weekendDays: number[] = [DAYS_OF_WEEK.SATURDAY, DAYS_OF_WEEK.SUNDAY];
  constructor(private http: HttpClient, private modal: NgbModal, private messageService: MessageService) { }
  events$: Observable<Array<CalendarEvent<{ app: Appuntamento }>>>;
  //events$: Observable<Array<CalendarEvent<{ film: Film }>>>;
  refresh: Subject<any> = new Subject();
  getStart: any = {
    month: startOfMonth,
    week: startOfWeek,
    day: startOfDay
  }[this.view];
  getEnd: any = {
    month: endOfMonth,
    week: endOfWeek,
    day: endOfDay
  }[this.view];
  ngOnInit() {
    this.loadData();
  }
  ngOnChanges(changes: SimpleChanges) {
    const name: SimpleChange = changes.AziendaId;
    /* console.log('prev value: ', name.previousValue);
     console.log('got name: ', name.currentValue);
     console.log('Start date: ', format(this.getStart(this.viewDate),'YYYY-MM-DD HH:mm:ss.SSS'));
     console.log('End date: ', format(this.getEnd(this.viewDate),'YYYY-MM-DD HH:mm:ss.SSS'));*/
    this.loadData();
  }
  modalData: {
    action: string;
    event: CalendarEvent;
  };

  actions: CalendarEventAction[] = [
    {
      label: ' <i class="fas fa-pencil-alt"></i>',
      onClick: ({ event }: { event: CalendarEvent<{ app: Appuntamento }> }): void => {
        this.handleEvent('Edited', event);
        console.log(event);
      }
    },
    {
      label: ' <i class="fas fa-times"></i>',
      onClick: ({ event }: { event: CalendarEvent<{ app: Appuntamento }> }): void => {
        //this.events = this.events.filter(iEvent => iEvent !== event);
        this.handleEvent('Deleted', event);
        console.log(event);
      }
    }
  ];

  eventTimesChanged({ event, newStart, newEnd }: CalendarEventTimesChangedEvent): void {
    event.start = newStart;
    event.end = newEnd;
    this.handleEvent('Dropped or resized', event);
    this.refresh.next();
  }
  handleEvent(action: string, event: CalendarEvent): void {
    this.modalData = { event, action };
    this.modal.open(this.modalContent, { size: 'lg' });
  }

  addEvent(): void {
    /* this.events.push({
       title: 'New event',
       start: startOfDay(new Date()),
       end: endOfDay(new Date()),
       color: {
         primary: '#e3bc08',
         secondary: '#FDF1BA'
       },
       draggable: true,
       resizable: {
         beforeStart: true,
         afterEnd: true
       },
       actions: this.actions
 
     });*/
    this.refresh.next();
  }
  loadData() {
    if (this.AziendaId !== null && this.AziendaId !== undefined) {
      this.fetchEvents(this.AziendaId, format(this.getStart(this.viewDate), 'YYYY-MM-DD HH:mm:ss.SSS'), format(this.getEnd(this.viewDate), 'YYYY-MM-DD HH:mm:ss.SSS'));
    }
  }
  fetchEvents(id: number, from: string, to: string): void {
    let url = `http://api.fatture-online.local/api/v1/appuntamenti/${encodeURIComponent(String(id))}/${encodeURIComponent(String(from))}/${encodeURIComponent(String(to))}`;
    this.events$ = this.http.get<Array<Appuntamento>>(url)
      .pipe(
        map(results => {
          return results.map((app: Appuntamento) => {
            return {
              id: app.AppuntamentoID,
              title: app.Oggetto,
              start: new Date(app.StartTime),
              end: new Date(app.EndTime),
              color: {
                primary: app.Color,
                secondary: app.BorderColor
              },
              actions: this.actions,
              resizable: {
                beforeStart: true,
                afterEnd: true,
              },
              draggable:true,
              meta: {
                app
              }
            };
          });
        }),
        tap(appuntamenti => this.log(`fetched appuntamenti`)),
        catchError(this.handleError('getListByAziendaIdDate', []))
      );
  }

  dayClicked({
    date,
    events
  }: {
      date: Date;
      events: Array<CalendarEvent<{ app: Appuntamento }>>;
    }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
        this.viewDate = date;
      }
    }
  }


  /**
 * Handle Http operation that failed.
 * Let the app continue.
 * @param operation - name of the operation that failed
 * @param result - optional value to return as the observable result
 */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  /** Log a HeroService message with the MessageService */
  private log(message: string) {
    this.messageService.add('HeroService: ' + message);
  }
}