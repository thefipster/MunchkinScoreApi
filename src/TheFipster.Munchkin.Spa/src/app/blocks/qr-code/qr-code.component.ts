import { Component, OnInit, Input } from '@angular/core';
import { Router } from "@angular/router";
import { Location } from '@angular/common';

@Component({
  selector: 'app-qr-code',
  templateUrl: './qr-code.component.html',
  styleUrls: ['./qr-code.component.scss']
})
export class QrCodeComponent implements OnInit {
  @Input() initCode: string;
  url: string;

  constructor (
    private router: Router
  ) { }

  ngOnInit() {
    this.url = window.location.protocol + 
      "//" + 
      window.location.hostname + 
      ':' + 
      window.location.port + 
      '/verify/' + 
      this.initCode;
   }
}
