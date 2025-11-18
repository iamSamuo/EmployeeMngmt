import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Employee } from 'src/models/employee';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  private baseURL = `${environment.apiUrl}/employee`;

  constructor(private http: HttpClient) {}

  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(`${this.baseURL}`);
  }

  getEmployeeById(id: number): Observable<Employee> {
    return this.http.get<Employee>(`${this.baseURL}/${id}`);
  }

  createEmployee(employeeData: any) {
    return this.http.post(`${this.baseURL}/create`, employeeData);
  }

  updateEmployee(id: number, employeeData: any) {
    return this.http.put(`${this.baseURL}/update/${id}`, employeeData);
  }

  deleteEmployee(id: number) {
    return this.http.delete(`${this.baseURL}/delete/${id}`);
  }
}
