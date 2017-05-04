using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class ShieldScript : MonoBehaviour {

    int mShieldType;
    // 1: white, 2: red, 3: green, 4: blue, 5: yellow
    int mHP;
    public Text mDebug;

    // Use this for initialization
    void Start() {
        mShieldType = 1;
        mHP = 5;
    }

    // Update is called once per frame
    void Update() {
        mDebug.text = "HP:" + mHP;

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Check if it is a bullet, if it is turn it to friendly
        if(collision.gameObject.tag == "projectile") {
            // If colliding with a projectile, try to turn it friendly if the shield type is the same
            // Get projectile type first
            GameObject obj = collision.gameObject;
            ProjectileScript projectile = obj.GetComponent<ProjectileScript>();
            if(projectile.mProjectileType == mShieldType) {
                projectile.Reflect();
            } else {
                --mHP;
                Destroy(obj);
            }
        }
    }
}
