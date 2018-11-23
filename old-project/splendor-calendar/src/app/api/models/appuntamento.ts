import { Azienda } from './azienda';
export class Appuntamento {
    AppuntamentoID: number;
    AziendaId: number;
    Oggetto: string;
    Localita: string;
    Body: string;
    IsPromemoria: boolean;
    IsDeleted: boolean;
    StartTime: Date;
    EndTime: Date;
    BorderColor: string;
    Color: string;
    AddedBy: string;
    TimeAdded: Date;
    EditdBy: string;
    TimeEdit: Date;
    Azienda: Azienda;
}