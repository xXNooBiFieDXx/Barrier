using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public GameObject[] maBG;
	public 
	// Use this for initialization
	void Start () {
        Physics2D.IgnoreLayerCollision(9, 10); // Make sure enemy do not collide with their own projectile.
        Physics2D.IgnoreLayerCollision(10, 11); // Make sure projectile do not collide with each other.
        Physics2D.IgnoreLayerCollision(10, 10); // Make sure projectile do not collide with each other.
        Physics2D.IgnoreLayerCollision(11, 11); // Make sure projectile do not collide with each other.
        // layer 8 is player
        // layer 9 is enemy
        // layer 10 is projectile
        // layer 11 is reflected projectile.
    }
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < 4; ++i) {
            maBG[i].transform.Translate((10 * Vector3.left) * Time.deltaTime);
		}					
		// Loop the BG
		for(int i = 0; i < 4; ++i) {
			if(maBG[i].transform.position.x < -15) {
				int nLast = (i+3)%4;
				float x = maBG[nLast].transform.position.x+9.6f;
                maBG[i].transform.position = x * Vector3.right;
			}				
		}
	}
}
