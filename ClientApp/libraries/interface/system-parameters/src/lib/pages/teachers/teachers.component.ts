import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-teachers',
  templateUrl: './teachers.component.html',
  styleUrls: []
})
export class TeachersComponent implements OnInit {
  title: string = 'Verified Teachers';

  constructor(private router:Router) { }

  ngOnInit() {
    this.router.navigateByUrl('/admin/users/teachers/verified')
  }

  setTitle(title){
    this.title = title
  }

}
