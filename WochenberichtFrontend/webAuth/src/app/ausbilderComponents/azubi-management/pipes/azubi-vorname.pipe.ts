import { Pipe, PipeTransform } from '@angular/core';
import { Mitarbeiter } from 'src/app/Models/mitarbeiter';

@Pipe({
  name: 'sucheAzubiVorname'
})
export class AzubiVornamePipe implements PipeTransform {

  transform(azubiList: Mitarbeiter[], sucheVorname: string): Mitarbeiter[] {
    if (!azubiList || !sucheVorname) {
      return azubiList;
    }
    return azubiList.filter(azubi => 
      azubi.firstName.toLowerCase().indexOf(sucheVorname.toLowerCase()) !== -1);
  }
}
