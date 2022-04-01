import { Pipe, PipeTransform } from '@angular/core';
import { Mitarbeiter } from 'src/app/Models/mitarbeiter';

@Pipe({
  name: 'sucheAzubiEmail'
})
export class AzubiEmailPipe implements PipeTransform {

  transform(azubiList: Mitarbeiter[], sucheEmail: string): Mitarbeiter[] {
    if (!azubiList || !sucheEmail) {
      return azubiList;
    }
    return azubiList.filter(azubi => 
      azubi.email.toLowerCase().indexOf(sucheEmail.toLowerCase()) !== -1);
  }

}
