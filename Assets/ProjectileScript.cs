using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
    public int mProjectileType;
    public Vector3 mVelocity;
    public float mSpeed;
    // 1: white, 2: red, 3: green, 4: blue, 5: yellow

    public bool mIsEnemy;

    // Use this for initialization
    void Start() {
        mIsEnemy = true; // All projectile is fired by enemy. player have to reflect the projectile to hit the enemy
        // Projectile type need to be set, by default all is white.
        gameObject.layer = 10;
        mSpeed = 10.0f;
        mVelocity = mSpeed * Vector3.left; // Projectile speed is defaulted to 10
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.x < -10
        || transform.position.x > 10) {
            Destroy(gameObject);
        }

        /*if(transform.position.y > 5
        || transform.position.x < -5 ) {
            mVelocity.y *= -1;
        }*/

        float radius = GetComponent<CircleCollider2D>().radius;
        float radiusSq = radius * radius;
        Vector3 newPos = transform.position + mVelocity * Time.deltaTime;

        bool bCollide = false;
        // Doing collision checks with enemy and player...
        if (mProjectileType == 5) { // Reflected projectile collide with enemy...
            GameObject[] aEnemy = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in aEnemy) {
               if(CheckCollision(newPos, enemy, radius)) {
                    bCollide = true;
                    enemy.GetComponent<BasicEnemyScript>().Hit(gameObject);
                }
            }

        } else { // Normal projectile... collide with shield.
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (CheckCollision(newPos, player, radius)) {
                bCollide = true;
                player.GetComponent<ShieldScript>().Hit(gameObject);
            }
        }
        if (!bCollide) {
            transform.position = newPos;
        }


    }

    private bool CheckCollision(Vector3 pos, GameObject obj, float radius) {
        float eRadius = obj.GetComponent<CircleCollider2D>().radius;
        float combinedRadius = eRadius + radius;
        float radiusSq = combinedRadius * combinedRadius;
        // Check if they collide with each other...
        Vector3 ePos = obj.transform.position;
        float x = ePos.x - pos.x;
        float y = ePos.y - pos.y;
        if ((x * x + y * y) < radiusSq) {
            return true;
        } else {
            return false;
        }
    }

    public void Reflect(GameObject obj) {
        if (mIsEnemy) { 
            mIsEnemy = false;
            SetProjectileType(5); // Set to yellow
        }
        gameObject.layer = 11;

        // Set the new position and reflected Reflect velocity
        // First get the point of contact?
        Vector3 pos = transform.position; // My pos
        Vector3 endPos = transform.position + mVelocity * Time.deltaTime;
        Vector3 bPos = obj.transform.position; // Colliding object pos
        
        float totalRadius = obj.GetComponent<CircleCollider2D>().radius + GetComponent<CircleCollider2D>().radius;

        // Game logic
        // Left velocity always be <--- to that the chance of reflected back at the enemy is higher
        // Dun have to be super accurate
        Vector3 reflected = pos - bPos;
        reflected.Normalize();
        mVelocity = mSpeed * reflected;
    }

    public void SetProjectileType(int type) {
        mProjectileType = type;

        switch (mProjectileType) {
            case 1:
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case 4:
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case 5:
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
        }
    }

    public void FireProjectile(int type, Vector3 vel) {
        SetProjectileType(type);
        mVelocity = vel;
    }
}
