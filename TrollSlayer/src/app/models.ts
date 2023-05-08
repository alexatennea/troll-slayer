export interface ReturnedGameData {
    message: string;
    playerDamage: number;
    targetDamage: number;
    gameData: GameData;
}

export interface GameData {
    gameId: number;
    player: Player;
    trolls: Troll[];
    dragon: Dragon;
}

export interface Player {
    health: number;
    weapons: Weapon[];
}

export interface Troll {
    health: number;
    weapons: Weapon[];
}

export interface Dragon {
    health: number;
    weapons: Weapon[];
}

export interface Weapon {
    name: string;
    diceRoll: string;
}

export interface AttackData {
    gameId: number;
    targetType: string;
    targetIndex: number;
    weaponIndex: number;
}