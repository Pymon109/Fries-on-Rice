using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    Rect m_rectBoundary;
    public void SetBoundaryHeight() 
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        m_rectBoundary = new Rect(transform.position, boxCollider.size);
    }

    void ProcessFindTarget()
    {
        int nLayer = 1 << LayerMask.NameToLayer("Fry");
        Collider2D[] colliders = Physics2D.OverlapBoxAll(m_rectBoundary.position, m_rectBoundary.size, 0f, nLayer);
        if (colliders.Length > 0)
        {
            FryMovement fry = colliders[0].GetComponent<FryMovement>();
            if(!fry.IsFixed())
                fry.SetState(FryMovement.FRY_STATE.FALL);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(m_rectBoundary.position, m_rectBoundary.size);
    }

    private void Awake()
    {
        //SetBoundaryHeight();
    }

    private void Update()
    {
        ProcessFindTarget();
    }
}
