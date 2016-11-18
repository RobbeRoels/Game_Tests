using UnityEngine;
using System.Collections;

public class ShootBehavior {
	public float delay;
	public int clipSize;
	public int projectiles;
	public int damagePerProjectile; //For example shotguns shoot multiple projectiles
	public float reloadTime;
	public float range;
	public bool auto;
	public bool fire;

	public int currentClip;

	public ShootBehavior(float delay, int clipSize,int projectiles, int damagePerProjectile, float reloadTime,float range, bool auto, bool fire){
		this.delay = delay;
		this.clipSize = clipSize;
		currentClip = clipSize;
		this.damagePerProjectile = damagePerProjectile;
		this.reloadTime = reloadTime;
		this.range = range;
		this.auto = auto;
		this.fire = fire;
	}

}
