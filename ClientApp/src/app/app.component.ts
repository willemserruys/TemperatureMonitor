import { Component, OnInit } from '@angular/core';
import { UserClient, User } from 'src/app/Core/Services/api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public users: User[];

  constructor(private userService: UserClient) {

  }
  title = 'ClientApp';

  ngOnInit() {
    this.userService.getAll().subscribe(x => {
      this.users = x;
    });
  }
}
