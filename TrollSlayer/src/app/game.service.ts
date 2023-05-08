import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GameData, AttackData, ReturnedGameData } from './models';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private apiUrl = 'http://localhost:5081/api/game';

  constructor(private http: HttpClient) { }

  start(): Observable<GameData> {
    return this.http.get<GameData>(`${this.apiUrl}/start`);
  }

  attack(attackData: AttackData): Observable<ReturnedGameData> {
    return this.http.post<ReturnedGameData>(`${this.apiUrl}/attack`, attackData);  
  }
}