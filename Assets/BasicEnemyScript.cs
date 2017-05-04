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
                mState = Random.Range(1, 4);
                if (mState > 1) { // Movement state
                    mDuration = Random.Range(1.0f, 2.5f);
                }
                break;
            case 1:
                // Shoot
                // Todo: instatiate a bullet and fire
                mState = 0; // Reset to idle.

                Shoot(Random.Range(1, 5));
                break;
            case 2:
                // Move up
                transform.Translate(mSpeed * Vector3.up * dt);
                mDuration -= dt;
                if (mDuration <= 0) {
                    mState = 0; // Reset to idle
                }
                if(transform.position.y > 5) {
                    mState = 3;
                    mDuration = Random.Range(0.2f, 1.5f);
                }

                break;
            case 3:
                // move down
                transform.Translate(mSpeed * Vector3.down * dt);
                mDuration -= dt;
                if (mDuration <= 0) {
                    mState = 0; // Reset to idle
                }

                if(transform.position.y < -5) {
                    mState = 2;
                    mDuration = Random.Range(1.0f, 2.5f);
                }
                break;

        }
    }



    private void OnCollisionEnter2D(Collision2D collision) {
        // Check if it is a bullet, if it is turn it to friendly
        if (collision.gameObject.tag == "projectile") {
            // If colliding with a projectile, try to turn it friendly if the shield type is the same
            // Get projectile type first
            GameObject obj = collision.gameObject;
            ProjectileScript projectile = obj.GetComponent<ProjectileScript>();
            if (!projectile.mIsEnemy) {
                // Destroy both the projectile and the enemy
                Destroy(gameObject);
                Destroy(obj);

                // Todo: add explosion?
            } else {
            }
        }
    }

    private void Shoot(int type = 1) {
        GameObject bullet = Instantiate(mProjectile, transform.position + (2.0f * Vector3.left), transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 500);
        bullet.GetComponent<ProjectileScript>().SetProjectileType(type);
    }
}