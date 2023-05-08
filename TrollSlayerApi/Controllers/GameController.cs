using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrollSlayerApi.Models;

namespace TrollSlayerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private static readonly Dictionary<int, GameState> GameStates = new();

        [HttpGet("start")]
        public ActionResult<GameData> Start()
        {
            var gameId = new Random().Next();
            var gameState = new GameState();
            GameStates[gameId] = gameState;

            if (gameState.GameData.Trolls.Count > 4)
            {
                gameState.GameData.Player.Health = 40;
            }

            return Ok(gameState.GameData);
        }

        [HttpPost("attack")]
        public ActionResult<GameData> Attack(AttackData attackData)
        {
            var gameState = GameStates
                .Where(gs => gs.Value.GameData.GameId == attackData.GameId)
                .Select(gs => gs.Value.GameData)
                .FirstOrDefault();

            if (gameState == null)
            {
                return NotFound("Game not found.");
            }

            if (attackData.WeaponIndex < 0 || attackData.WeaponIndex >= gameState.Player.Weapons.Count)
            {
                return BadRequest("Invalid attack index.");
            }

            Character target = null;

            if (attackData.TargetType == "troll")
            {
                target = gameState.Trolls[attackData.TargetIndex];

                if (target.Health <= 0)
                {
                    return Ok(new { message = "That target is already dead.", playerDamage = 0, targetDamage = 0, gameData = gameState });
                }
            }
            else if (attackData.TargetType == "dragon")
            {
                target = (Character)gameState.Dragon;
            }

            // If no target is found, the game is over
            if (target == null)
            {
                return BadRequest("The game is over. No valid targets.");
            }

            // Perform the attack
            Weapon weapon = gameState.Player.Weapons[attackData.WeaponIndex];
            int damage = weapon.GetDamage();
            target.Health -= damage;

            // Perform a counterattack (by the target)
            int counterDamage = 0;

            if (target.Health > 0)
            {
                Weapon counterWeapon = target.Weapons[new Random().Next(target.Weapons.Count)];
                counterDamage = counterWeapon.GetDamage();
                gameState.Player.Health -= counterDamage;
            }

            if (gameState.Player.Health > 10 && gameState.Trolls.Count(t => t.Health > 0) == 0 && gameState.Dragon == null)
            {
                gameState.Dragon = GameState.CreateDragon();
                gameState.Player.Weapons.Add(new Weapon("Super Smash!", "2d20+8"));
            }

            // Check if the player or target is dead
            if (gameState.Player.Health <= 0 && gameState.Dragon != null)
            {
                return Ok(new { message = "You have been defeated by the Dragon.", playerDamage = damage, targetDamage = counterDamage, gameData = gameState });
            }
            else if (gameState.Player.Health <= 0)
            {
                return Ok(new { message = "You have been defeated.", playerDamage = damage, targetDamage = counterDamage, gameData = gameState });
            }

            if (gameState.Trolls.Count(t => t.Health > 0) == 0 && gameState.Dragon == null) 
            {
                return Ok(new { message = $"You have defeated all the creatures! And avoided the dragon...", playerDamage = damage, targetDamage = counterDamage, gameData = gameState });
            }
            else if (gameState.Trolls.Count(t => t.Health > 0) == 0&& gameState.Dragon != null && gameState.Dragon.Health <= 0) 
            {
                return Ok(new { message = $"You have defeated all the creatures! And the Dragon!!", playerDamage = damage, targetDamage = counterDamage, gameData = gameState });
            }
            else if (target.Health <= 0 && gameState.Dragon != null)
            {
                return Ok(new { message = $"{target.GetType().Name} has been defeated but then, a dragon appears!", playerDamage = damage, targetDamage = counterDamage, gameData = gameState });
            }
            else if (target.Health <= 0)
            {
                return Ok(new { message = $"{target.GetType().Name} has been defeated.", playerDamage = damage, targetDamage = counterDamage, gameData = gameState });
            }

            return Ok(new { message = "Attack successful.", playerDamage = damage, targetDamage = counterDamage, gameData = gameState });
        }
    }
}