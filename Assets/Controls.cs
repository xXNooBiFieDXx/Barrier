using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Controls : MonoBehaviour {
    public DeviceType mType;
    public Vector3 mPos;
    public Text mDebug;
    // Use this for initialization
    void Start() {
        mType = SystemInfo.deviceType;
        mDebug.text = mDebug.text + "\nDevice Type" + mType;
        mPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(mType == DeviceType.Desktop){
            mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);           
        } else if(mType == DeviceType.Handheld) {
            // Todo
        }



        transform.position = new Vector3(mPos.x, mPos.y, transform.position.z);
    }
}
