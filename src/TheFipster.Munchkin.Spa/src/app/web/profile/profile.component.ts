import { Component, OnInit } from '@angular/core';
import { ProfileService } from 'src/app/services/profile.service';
import { GameMaster } from 'src/app/interfaces/game-master';
import { BaseDataService } from 'src/app/services/base-data.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  public profile: GameMaster;
  public genders: string[];
  public viewGender: boolean = true;
  public viewFriend: boolean = true;
  public selectedGender: string;
  public friendGender: string;
  public friendName: string = '';

  constructor(
    private profileService: ProfileService,
    private baseData: BaseDataService
  ) { }

  ngOnInit() {
    this.getGenders();
    this.getProfile();
  }

  getGenders() {
    this.baseData.getGenders().subscribe((genders: string[]) => {
      this.genders = genders;
      console.log(genders);
    });
  }

  getProfile() {
    this.profileService.getProfile().subscribe((profile: any) => {
      this.profile = profile;
      this.selectedGender = this.profile.gender;
    });
  }

  toggleGenderMode() {
    this.viewGender = !this.viewGender;
  }

  setGender() {
    this.profile.gender = this.selectedGender;
    this.profileService
    .postProfile(this.profile)
    .toPromise()
    .then(value => console.log('Save completed'))
    .catch((error) => console.log(error));

    this.toggleGenderMode();
  }

  addFriend() {
    this.profileService.postFriend(this.friendName, this.friendGender).subscribe((response: any) => {
      this.getProfile();
    });
    this.toggleFriendMode();
    this.resetFriendForm();
  }

  resetFriendForm() {
    this.friendName = '';
    this.friendGender = '';
  }

  toggleFriendMode() {
    this.viewFriend = !this.viewFriend;
  }
}
