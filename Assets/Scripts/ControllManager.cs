using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllManager : MonoBehaviour
{
    public GameObject spellBook;
    public InputActionProperty openBook;
    public InputActionProperty castMagic;
    float leftGripValue;
    float rightGripValue;
    bool isFire;
    public GameObject spherePrefab;
    public Transform cameraTr;
    public Transform rightControllerTr;
    public ParticleSystem magicEffects;
    void Start()
    {
       
        spellBook.SetActive(false);
        isFire = false; 


    }

    // Update is called once per frame
    void Update()
    {
        OpenBook();
        CastMagic();



    }
    void OpenBook()
    {
        leftGripValue = openBook.action.ReadValue<float>();
        if (leftGripValue > 0.9f)
            spellBook.SetActive(true);
        else
            spellBook.SetActive(false);
    }
    void CastMagic()
    {
        rightGripValue = castMagic.action.ReadValue<float>();
        if (rightGripValue > 0.9f)
        {
            magicEffects.Play();
            isFire = true;
        }
        else if (rightGripValue <= 0.9f && rightGripValue > 0)
        {
            magicEffects.Stop();
            StartCoroutine(GenerateSphere());
            isFire = false;
        }
        else
            magicEffects.Stop();


    }
   
    IEnumerator GenerateSphere()
    {
        while (isFire)
        {
            Vector3 newPosition = rightControllerTr.position + cameraTr.forward * 0.2f;
            // 마법 생성 (날아가게 할 것임)
            GameObject cube = Instantiate(spherePrefab, newPosition, Quaternion.identity);
            //  Right Controller의 자식으로 설정
            cube.transform.SetParent(rightControllerTr);
            yield return new WaitForSeconds(0.5f);
        }
    }
   
}
