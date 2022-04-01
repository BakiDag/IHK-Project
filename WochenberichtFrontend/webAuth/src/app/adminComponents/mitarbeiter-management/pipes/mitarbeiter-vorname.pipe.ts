import { Pipe, PipeTransform } from '@angular/core';
import { Mitarbeiter } from 'src/app/Models/mitarbeiter';

@Pipe({
  name: 'sucheMitarbeiterVorname'
})
export class MitarbeiterVornamePipe implements PipeTransform {

  transform(MitarbeiterListe: Mitarbeiter[], searchTerm: string): Mitarbeiter[] {
    if (!MitarbeiterListe || !searchTerm) {
      return MitarbeiterListe;
    }
    return MitarbeiterListe.filter(mitarbeiter => 
      mitarbeiter.firstName.toLowerCase().indexOf(searchTerm.toLowerCase()) !== -1);
  }

}
