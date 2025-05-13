import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { Helper } from '../../Shared/helper';
import { Subscription } from 'rxjs';
import { CompanyModel } from '../../Model/CompanyModel.model'; import { ApiServices } from '../../Services/petShopApi.service';
import { AuthenticationService } from '../../Services/authentication.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink, RouterOutlet],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit, OnDestroy {
  companie: CompanyModel;
  loged: boolean = false;
  private subs: Subscription[] = [];

  constructor(private apiservices: ApiServices, private router: Router, private auth: AuthenticationService) { }

  toggleActive(event: MouseEvent) {
    Helper.selectActiveHandler(event.currentTarget as HTMLEmbedElement);
  }

  ngOnInit(): void {
    this.router.navigate(['/home']);
    this.subs.push(this.auth.loged$.subscribe(v => this.loged = v));

    try {
      this.subs.push(
        this.apiservices.get<CompanyModel>("api/v1/company").subscribe(res => this.companie = res)
      );

      if(!this.companie.instagramAddress){
        this.companie.instagramAddress = "#";
      }
    }
    catch (err) {
      console.error(err);
    }
  }

  navigateTo(url: string) {
    this.router.navigate([url])
  }

  logOut() {
    this.auth.logout();
  }

  ngOnDestroy(): void {
    this.subs.forEach(sub => sub.unsubscribe());
  }
}
