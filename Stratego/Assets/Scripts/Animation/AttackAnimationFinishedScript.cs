using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationFinishedScript : MonoBehaviour
{
   void AttackAnimation()
    {
        AE.AttackAnimationFinished?.Invoke(gameObject);
    }
}
