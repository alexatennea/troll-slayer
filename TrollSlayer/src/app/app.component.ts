import { Component, ViewEncapsulation } from '@angular/core';
import { GameService } from './game.service';
import { GameData, AttackData, ReturnedGameData } from './models';

@Component({
  selector: 'app-root',
  encapsulation: ViewEncapsulation.None,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Troll Slayer';
  gameData: GameData | null = null;
  selectedTargetType: string = 'troll';
  selectedTargetIndex: number = 0;
  selectedWeaponIndex: number = 0;
  message: string = '';
  creaturesAlive: number = 0;

  constructor(private gameService: GameService) {}

  startGame() {
    this.gameService.start().subscribe((data: GameData) => {
      this.message = '';
      this.selectedTargetType = 'troll';
      this.selectedTargetIndex = 0;
      this.selectedWeaponIndex = 0;
      this.gameData = data;
      this.creaturesAlive = this.checkCreaturesAlive(data);
    });
  }

  attack() {
    if (!this.gameData) {
      return;
    }

    const attackData: AttackData = {
      gameId: this.gameData.gameId,
      targetType: this.selectedTargetType,
      targetIndex: this.selectedTargetIndex,
      weaponIndex: this.selectedWeaponIndex
    };

    this.gameService.attack(attackData).subscribe((data: ReturnedGameData) => {
      this.message = data.message;
      this.gameData = data.gameData;
      this.creaturesAlive = this.checkCreaturesAlive(data.gameData);
    });

  }

  checkCreaturesAlive(data: GameData): number {
    let count: number = 0;

    data.trolls.forEach(troll => {
      if (troll.health > 0) {
        count++;
      }
    });

    if (data.dragon) {
      if (data.dragon.health > 0) {
        count++;
      }
    }

    return count;
  }
}
