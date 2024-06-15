using UnityEngine;
using System.Collections;
using DG.Tweening;

// [ToDo] : 캐릭터 변경 
// 
public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField]
    GameObject characterObj;

    GameObject curCharacterObj;

    void Start()
    {
        if (!transform.GetChild(0))
            curCharacterObj = Instantiate(characterObj, this.transform);
        else
            curCharacterObj = transform.GetChild(0).gameObject;

    }

}
