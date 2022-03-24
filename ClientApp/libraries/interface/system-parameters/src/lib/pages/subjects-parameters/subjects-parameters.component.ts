import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-subjects-parameters',
  templateUrl: './subjects-parameters.component.html',
  styleUrls: []
})
export class SubjectsParametersComponent implements OnInit {

  constructor(private router:Router) { }

  ngOnInit() {
    this.router.navigateByUrl('/admin/subjects-parameters/lession-locations')
  }

}
