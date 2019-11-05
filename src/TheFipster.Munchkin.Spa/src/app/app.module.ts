import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';

import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FlexLayoutModule } from '@angular/flex-layout';
import { AppRoutingModule } from './app-routing.module';
import { NgxQRCodeModule } from 'ngx-qrcode2';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';

import { AppComponent } from './app.component';
import { IndexComponent } from './web/index/index.component';
import { MasterComponent } from './game/master/master.component';
import { ViewerComponent } from './game/viewer/viewer.component';
import { DashboardComponent } from './web/dashboard/dashboard.component';
import { NotFoundComponent } from './error/not-found/not-found.component';

import { QrCodeComponent } from './blocks/qr-code/qr-code.component';

import { InitGameService } from './services/init-game.service';

import { StateComponent } from './game/state/state.component';
import { VerifyComponent } from './game/verify/verify.component';
import { WaitComponent } from './game/state/wait/wait.component';
import { SetupComponent } from './game/master/setup/setup.component';
import { NavLinksComponent } from './blocks/nav-links/nav-links.component';
import { ProfileComponent } from './web/profile/profile.component';
import { PlayComponent } from './game/master/play/play.component';
import { FightComponent } from './game/master/fight/fight.component';
import { EndComponent } from './game/master/end/end.component';
import { WatchComponent } from './game/state/watch/watch.component';
import { ResultsComponent } from './game/state/results/results.component';
import { HeroComponent } from './blocks/hero/hero.component';
import { DungeonComponent } from './blocks/dungeon/dungeon.component';
import { SelectChooserComponent } from './sheets/select-chooser/select-chooser.component';
import { CallbackComponent } from './core/callback/callback.component';

import { RacePipe } from './pipes/race.pipe';
import { ClassPipe } from './pipes/class.pipe';

@NgModule({
  declarations: [
    AppComponent,
    IndexComponent,
    MasterComponent,
    ViewerComponent,
    DashboardComponent,
    NotFoundComponent,
    QrCodeComponent,
    StateComponent,
    VerifyComponent,
    WaitComponent,
    SetupComponent,
    NavLinksComponent,
    ProfileComponent,
    PlayComponent,
    FightComponent,
    EndComponent,
    WatchComponent,
    ResultsComponent,
    HeroComponent,
    DungeonComponent,
    SelectChooserComponent,
    RacePipe,
    ClassPipe,
    CallbackComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
    AppRoutingModule,
    HttpClientModule,
    NgxQRCodeModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatCheckboxModule,
    MatMenuModule,
    MatToolbarModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatStepperModule,
    MatSlideToggleModule,
    MatGridListModule,
    MatCardModule,
    MatChipsModule,
    MatBottomSheetModule
  ],
  providers: [
    InitGameService,
    SelectChooserComponent

  ],
  bootstrap: [
    AppComponent
  ],
  entryComponents: [
    SelectChooserComponent
  ]
})
export class AppModule { }
