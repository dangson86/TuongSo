import { Injectable } from '@angular/core';
import Swal from 'sweetalert2'

@Injectable({
  providedIn: 'root'
})
export class UtilityService {

  constructor() { }

  alert(message: string) {
    try {
      Swal.fire(message);
    } catch (error) {
      alert(message);
    }
  }
}
