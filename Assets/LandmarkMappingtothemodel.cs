using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mediapipe;

    public class LandmarkMappingtothemodel : MonoBehaviour
    {
        [SerializeField]  GameObject LeftArm, RightArm, Leftforearm, Rightforearm, Rightarm, Lefthand, Righthand, LeftThigh, RightFebula, LeftFebula, Rightfoot, Leftfoot;
    private void Start()
    {
       
    }
}
public class SDest : AnnotationController
{
    public override void Clear()
    {

    }
}