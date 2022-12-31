using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public abstract class MonoBehaviourSlowUpdate : MonoBehaviour
{
    protected virtual int TicksToUpdate => 15;
    private int counter;

    private void Update()
    {
        if(counter == 0)
        {
            SlowUpdate();
        }
        counter++;
        if(counter == this.TicksToUpdate)
        {
            counter = 0;
        }
    }

    protected abstract void SlowUpdate();
}