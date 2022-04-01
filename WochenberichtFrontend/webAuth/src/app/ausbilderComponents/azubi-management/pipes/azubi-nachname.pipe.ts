import { Pipe, PipeTransform } from '@angular/core';
import { Mitarbeiter } from 'src/app/Models/mitarbeiter';

@Pipe({
  name: 'sucheAzubiNachname'
})
export class AzubiNachnamePipe implements PipeTransform {
  transform(azubiList: Mitarbeiter[], sucheNachname: string): Mitarbeiter[] {
    if (!azubiList || !sucheNachname) {
      return azubiList;
    }
    return azubiList.filter(azubi => 
      azubi.firstName.toLowerCase().indexOf(sucheNachname.toLowerCase()) !== -1);
  }

}
