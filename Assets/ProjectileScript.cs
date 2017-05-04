using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
    public int mProjectileType; 
    // 1: white, 2: red, 3: green, 4: blue, 5: yellow

    public bool mIsEnemy;

    // Use this for initialization
    void Start() {
        mIsEnemy = true; // All projectile is fired by enemy. player have to reflect the projectile to hit the enemy
        // Projectile type need to be set, by default all is white.
        mProjectileType = 1;
        gameObject.layer = 10;
    }

    // Update is called once per frame
    void Update() {
        if(transform.position.x < -10
        || transform.position.x >  10
        || transform.position.y <  -10
        || transform.position.y >  10 ) {
            Destroy(gameObject);
        }
    }

    public void Reflect() {
        if (mIsEnemy) { 
            mIsEnemy = false;
            SetProjectileType(5); // Set to yellow
        }
        gameObject.layer = 11;        
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
}
