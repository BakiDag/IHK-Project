import { Pipe, PipeTransform } from '@angular/core';
import { Mitarbeiter } from 'src/app/Models/mitarbeiter';

@Pipe({
  name: 'sucheMitarbeiterEmail'
})
export class MitarbeiterEmailPipe implements PipeTransform {

  transform(MitarbeiterListe: Mitarbeiter[], sucheEmail: string): Mitarbeiter[] {
    if (!MitarbeiterListe || !sucheEmail) {
      return MitarbeiterListe;
    }
    return MitarbeiterListe.filter(mitarbeiter => 
      mitarbeiter.email.toLowerCase().indexOf(sucheEmail.toLowerCase()) !== -1);
  }
}
