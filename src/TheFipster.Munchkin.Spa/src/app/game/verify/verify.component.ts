import { Component, OnInit } from '@angular/core';
import { VerifyService } from 'src/app/services/verify.service';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';

@Component({
  selector: 'app-verify',
  templateUrl: './verify.component.html',
  styleUrls: ['./verify.component.scss']
})
export class VerifyComponent implements OnInit {

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private verify: VerifyService
  ) { }

  ngOnInit() {
    this.route.paramMap
      .subscribe(params => this.verifyInitCode(params));
  }

  private verifyInitCode(params: ParamMap) {
    const initCode = params.get('initCode');
    this.verify
      .checkInitCode(initCode)
      .subscribe((result: any) => this.router.navigate(['master', result.gameId]));
  }
}
