import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { Helper } from '../../Shared/helper';
import { HttpClient } from '@angular/common/http';
import { catchError, of, Subscription, tap } from 'rxjs';
import { CompanyModel } from '../../Model/CompanyModel.model';;


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink, RouterOutlet],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit, OnDestroy{
    companie: CompanyModel;
    private subs: Subscription[] = [];
  
  
  constructor(private http: HttpClient, private router: Router) { }

  toggleActive(event: MouseEvent) {
    Helper.selectActiveHandler(event);
  }

  ngOnInit(): void {
      this.subs.push(
        this.http.get<CompanyModel>("api/v1/company")
        .pipe(
          tap(res =>  this.companie = res),
          catchError(err => {
            console.error(err);
            return of(null);
          })
        ).subscribe()
      );
    }

    navigateTo(url: string){
      this.router.navigate([url])
    }
  
    ngOnDestroy(): void {
      this.subs.forEach(sub => sub.unsubscribe());
    }
}
