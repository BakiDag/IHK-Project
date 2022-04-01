import { Pipe, PipeTransform } from '@angular/core';
import { Mitarbeiter } from 'src/app/Models/mitarbeiter';

@Pipe({
  name: 'sucheMitarbeiterRole'
})
export class MitarbeiterRolePipe implements PipeTransform {

  transform(MitarbeiterListe: Mitarbeiter[], searchRole: string): Mitarbeiter[] {
    if (!MitarbeiterListe || !searchRole) {
      return MitarbeiterListe;
    }
    return MitarbeiterListe.filter(mitarbeiter => 
      mitarbeiter.role.toLowerCase().indexOf(searchRole.toLowerCase()) !== -1);
  }

}
