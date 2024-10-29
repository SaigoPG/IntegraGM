using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modelplayer : MonoBehaviour
{
  public Vector3 Position { get; set; }

  public Modelplayer(Vector3 initialPosition)
  {
            Position = initialPosition;
  }
}
