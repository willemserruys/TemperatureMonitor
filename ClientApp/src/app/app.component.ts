import { Component, OnInit } from '@angular/core';
import { TemperatureReadingClient, TemperatureReading } from 'src/app/Core/Services/api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public users: TemperatureReading[];

  constructor(private userService: TemperatureReadingClient) {

  }
  title = 'ClientApp';

  ngOnInit() {
    this.userService.getAll().subscribe(x => {
      this.users = x;
    });
  }
}
