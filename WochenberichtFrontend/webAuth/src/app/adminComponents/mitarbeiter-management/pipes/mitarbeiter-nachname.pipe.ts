import { Pipe, PipeTransform } from '@angular/core';
import { Mitarbeiter } from 'src/app/Models/mitarbeiter';

@Pipe({
  name: 'sucheMitarbeiterNachname'
})
export class MitarbeiterNachnamePipe implements PipeTransform {

  transform(MitarbeiterListe: Mitarbeiter[], sucheNachname: string): Mitarbeiter[] {
    if (!MitarbeiterListe || !sucheNachname) {
      return MitarbeiterListe;
    }
    return MitarbeiterListe.filter(mitarbeiter => 
      mitarbeiter.lastName.toLowerCase().indexOf(sucheNachname.toLowerCase()) !== -1);
  }

}
