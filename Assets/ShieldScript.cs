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

    public void Hit(GameObject obj) {
        // Check if it is a bullet, if it is turn it to friendly
        if (obj.gameObject.tag == "projectile") {
            // If colliding with a projectile, try to turn it friendly if the shield type is the same
            // Get projectile type first
            ProjectileScript projectile = obj.GetComponent<ProjectileScript>();
            if (projectile.mProjectileType == mShieldType) {

                // Todo: only reflect if hitting shield portion, i.e hit from front.
                projectile.Reflect(gameObject);
            }
            else {
                --mHP;
                Destroy(obj);
            }
        }
    }
}
