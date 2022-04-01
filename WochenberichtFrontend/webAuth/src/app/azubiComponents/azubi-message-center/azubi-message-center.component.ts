import { Component, OnInit } from '@angular/core';
import { MessageCenterService } from 'src/app/services/message-center.service';

@Component({
  selector: 'app-azubi-message-center',
  templateUrl: './azubi-message-center.component.html',
  styleUrls: ['./azubi-message-center.component.scss']
})
export class AzubiMessageCenterComponent implements OnInit {

  constructor(public message : MessageCenterService) { }

  ngOnInit(): void {

    //implement message service with request
  }

}
