using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraspableObject : MonoBehaviour
{
    [SerializeField] private string _id;
    public string id
    {
        get { return _id; }
        private set { _id = value; }
    }
}
