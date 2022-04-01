using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class HeadRigLookTarget : MonoBehaviour
{
    // Start is called before the first frame update
    
    private Rig rig;
    private float targetWeight;

    private void Awake()
    {
        rig = GetComponent<Rig>();
    }

    // Update is called once per frame
    private void Update()
    {
        rig.weight = Mathf.Lerp(rig.weight,targetWeight,Time.deltaTime * 10f);

        if(Input.GetKeyDown(KeyCode.T))
        {
            targetWeight = 1f;
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
            targetWeight = 0f;
        }
    }
}
