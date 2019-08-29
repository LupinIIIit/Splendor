import { CalendarDateFormatter, DateFormatterParams } from 'angular-calendar';
import { getISOWeek } from 'date-fns';
import { DatePipe, TitleCasePipe, DeprecatedDatePipe } from '@angular/common';

export class DateFormatter extends CalendarDateFormatter {
    public weekViewTitle({ date, locale }: DateFormatterParams): string {
        const year: string = this.getStrDate(date, 'y', locale);
        const weekNumber: number = getISOWeek(date);
        return `Settimana ${weekNumber} del ${year}`;
    }
    public weekViewColumnHeader({ date, locale }: DateFormatterParams): string {
        return new TitleCasePipe().transform(this.getStrDate(date, 'EEEE', locale));
    }
    public dayViewHour({ date, locale }: DateFormatterParams): string {
        return this.getStrDate(date, 'HH:mm', locale);
    }
    public dayViewTitle({ date, locale }: DateFormatterParams): string {
        return new TitleCasePipe().transform(this.getStrDate(date, 'EEEE, dd MMMM y', locale));
    }
    public monthViewColumnHeader({ date, locale }: DateFormatterParams): string {
        return new TitleCasePipe().transform(this.getStrDate(date, 'EEE', locale));
    }
    public monthViewTitle({ date, locale }: DateFormatterParams): string {
        return new TitleCasePipe().transform(this.getStrDate(date, 'MMMM y', locale));
    }
    private getStrDate(date:Date,format:string,locale:string):string{
        return new DatePipe(locale).transform(date,format,locale);
    }
}