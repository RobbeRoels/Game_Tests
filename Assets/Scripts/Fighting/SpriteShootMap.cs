using UnityEngine;
using System.Collections;

public class SpriteShootMap {

	//TODO: Change this as item implementation.

	public ShootBehavior[] behavior = new ShootBehavior[10];

	public SpriteShootMap(){
//ShootBehavior params: delay, clipSize, projectiles, damagePerProjectile, reloadTime, range, auto, fire
		behavior [0] = new ShootBehavior (0    , 0 , 0,  0 , 0   , 0  , false, false); //No weapon
		/*behavior [1] = new ShootBehavior (0    , 0 , 0, 100, 0   , 5  , false, false); //Knife
		behavior [2] = new ShootBehavior (0.2f , 12, 1, 10 , 1.5f, 100, false, true);  //Pistol
		behavior [3] = new ShootBehavior (0.7f , 7 , 6, 9  , 3.0f, 75 , false, true);  //Lvl 1 Shotgun
		behavior [4] = new ShootBehavior (0.1f , 25, 1, 12 , 2.0f, 100, true , true);  //SMG
		behavior [5] = new ShootBehavior (0.15f, 30, 1, 25 , 2.5f, 100, true , true);  //Lvl 1 Assault Rifle
		behavior [6] = new ShootBehavior (0.4f , 15, 1, 55 , 6.0f, 200, false, true);  //Lvl 1 Rifle
		behavior [7] = new ShootBehavior (0.6f , 14, 8, 12 , 2.4f, 95 , false, true);  //Lvl 2 Shotgun
		behavior [8] = new ShootBehavior (0.15f, 45, 1, 32 , 2.0f, 120, true , true);  //Lvl 2 Assault Rifle
		behavior [9] = new ShootBehavior (0.4f , 20, 1, 95 , 4.5f, 250, false, true);  //Lvl 2 Rifle*/
	}
}
