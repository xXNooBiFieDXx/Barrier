using UnityEngine;
using System.Collections;

public class BasicEnemyScript : MonoBehaviour {
    public int mState;
    public float mDuration;
    public float mSpeed;
    public float mXPt;
    public GameObject mProjectile;
    // Use this for initialization
    void Start() {
        mState = 0;
        mSpeed = 5.0f;
        mXPt = Random.Range(0.0f, 5.0f);
        gameObject.layer = 9;
    }

    // Update is called once per frame
    void Update() {
        float dt = Time.deltaTime;
        dt = 1.0f / 60.0f;

        if(transform.position.x > mXPt) {
            transform.Translate(mSpeed * Vector3.left * dt);
        }        

        switch (mState) {
            case 0: // Idle
                // Choose to either go up or down or shoot.
                mDuration -= dt;
                if (mDuration < 0) {
                    mState = Random.Range(0, 2);
                    if (mState == 0) {
                        mDuration = Random.Range(0.5f, 1.0f);
                    }
                }
                break;
            case 1:
                // Shoot
                // Todo: instatiate a bullet and fire
                mDuration = Random.Range(0.5f, 2.5f);
                mState = 0; // Reset to idle.
                Shoot(Random.Range(1, 3));
                break;
        }
    }


    public void Hit(GameObject obj) {
        // Check if it is a bullet, if it is turn it to friendly
        if (obj.gameObject.tag == "projectile") {
            // If colliding with a projectile, try to turn it friendly if the shield type is the same
            // Get projectile type first
            ProjectileScript projectile = obj.GetComponent<ProjectileScript>();
            if (projectile.mProjectileType == 5) {
                Destroy(obj);
                Destroy(gameObject);
            }
        }
    }

    private void Shoot(int type = 1) {
        GameObject bullet = Instantiate(mProjectile, transform.position + (0.5f * Vector3.left), transform.rotation);
        bullet.GetComponent<ProjectileScript>().FireProjectile(type, 10.0f * Vector3.left);
    }
}