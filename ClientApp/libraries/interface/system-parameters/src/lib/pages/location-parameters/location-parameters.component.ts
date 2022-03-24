import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-location-parameters',
  templateUrl: './location-parameters.component.html',
  styleUrls: []
})
export class LocationParametersComponent implements OnInit {

  constructor(private router:Router) { }

  ngOnInit() {
    this.router.navigateByUrl('/admin/location-parameters/provinces')
  }

}
