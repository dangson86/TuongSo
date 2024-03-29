import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.scss']
})
export class ClientListComponent implements OnInit {
  currentClients: any[] = []

  constructor(private router: Router) {

  }

  ngOnInit(): void {
    let currentClientsString = localStorage.getItem("clients")
    let temp: any[] = currentClientsString == null ? [] : JSON.parse(currentClientsString);
    temp.sort((a, b) => {
      return a.FullName.localeCompare(b.FullName);
    });
    this.currentClients = temp;
  }

  goBack() {
    this.router.navigate([""]);
  }

  selectClient(client, index: number) {
    this.router.navigate([""], {
      queryParams: client
    });
  }

  deleteClient(client, index: number) {
    this.currentClients.splice(index, 1);
    localStorage.setItem("clients", JSON.stringify(this.currentClients));
  }
}
