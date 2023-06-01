using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    //Attach this script directly on the cannon/source spawner
    [SerializeField] private List<Projectile> projectiles = new List<Projectile>();
    private void OnEnable()
    {
        Projectile.AddSelf += AddProjectile;
    }
    private void OnDisable()
    {
        Projectile.AddSelf -= AddProjectile;
    }

    private void AddProjectile(Projectile value)
    {

        value.timer = 0;
        projectiles.Add(value); // add the projectile to the list of projectiles to be used by the "cannon"
        value.EnableProjectile();
    }

    private void FixedUpdate()
    {
        // pooling has has the advantage of memory management and garbage collection. Spawning and Destroying uses more fragmented resources of 
        // memory allocation and management; thus pooling reserves one chuck of memory and one only. Performance boost.
       
        for(int x = 0; x < projectiles.Count; x++)
        {
            
            projectiles[x].timer += Time.deltaTime;
            projectiles[x].transform.localPosition += projectiles[x].direction * projectiles[x].speed * Time.deltaTime;

            if (projectiles[x].timer >= projectiles[x].lifeSpan)
            {
                projectiles[x].DisableProjectile();
                projectiles.RemoveAt(x); // remove the projectile from the list so it can be readded and recycled. No duplicate projectiles within list.

            }
        }
    }
}
