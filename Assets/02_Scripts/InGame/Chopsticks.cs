using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopsticks : MonoBehaviour
{
    FryMovement m_fryMove;
    public FryMovement fryMove { set { m_fryMove = value; } }

    public enum E_CHOPSTICK_STATE
    {
        SPAWN = 0,
        PINGPONG,
        DROP
    }
    E_CHOPSTICK_STATE m_state = E_CHOPSTICK_STATE.SPAWN;

    void UpdatState()
    {
        switch(m_state)
        {
            case E_CHOPSTICK_STATE.SPAWN:

                break;
            case E_CHOPSTICK_STATE.PINGPONG:
                if(m_fryMove)
                    transform.position = m_fryMove.transform.position;
                break;
            case E_CHOPSTICK_STATE.DROP:

                break;
        }
    }

    public void SetState(E_CHOPSTICK_STATE command)
    {
        switch (command)
        {
            case E_CHOPSTICK_STATE.SPAWN:

                break;
            case E_CHOPSTICK_STATE.PINGPONG:

                break;
            case E_CHOPSTICK_STATE.DROP:

                break;
        }
        m_state = command;
    }    


    void Update()
    {
        UpdatState();
    }
}
