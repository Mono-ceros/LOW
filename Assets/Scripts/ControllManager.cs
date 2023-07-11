using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllManager : MonoBehaviour
{
    public GameObject spellBook;
    public InputActionProperty openBook;
    public InputActionProperty castMagic;
    public InputActionProperty indexChange;
    float leftGripValue;
    float rightGripValue;
    bool isFire;
    
    public Transform cameraTr;
    public Transform rightControllerTr;
    public ParticleSystem[] magicEffects;
    public GameObject[] magicPrefabs;
    int index;
   
    private void Awake()
    {
        
     
    }
    void Start()
    {
        index = 0;
        spellBook.SetActive(false);
        isFire = false; 


    }

    // Update is called once per frame
    void Update()
    {
        OpenBook();
        CastMagic();
        IndexChange();

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
            magicEffects[index].Play();
            isFire = true;
        }
        else if (rightGripValue <= 0.9f && rightGripValue > 0)
        {
            magicEffects[index].Stop();
            StartCoroutine(FireMagic());
            isFire = false;
        }
        else
            magicEffects[index].Stop();
    }
   
    IEnumerator FireMagic()
    {
        while (isFire)
        {
            Vector3 newPosition = rightControllerTr.position + cameraTr.forward * 0.2f;
            // ���� ���� (���ư��� �� ����)
            GameObject magic = Instantiate(magicPrefabs[index], newPosition, Camera.main.transform.rotation);
            
            yield return new WaitForSeconds(0.5f);
        }
    }
   void IndexChange()
    {
        float xbutton = indexChange.action.ReadValue<float>();
        if (xbutton>0.9f)
        {
            index++;
            if(index == 2)
                index = 0;
        }
    }
}
