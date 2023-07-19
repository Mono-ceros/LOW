using DigitalRuby.ThunderAndLightning;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllManager : MonoBehaviour
{
    public GameObject spellBook;
    public InputActionProperty openBook;
    public InputActionProperty castMagic;
    public InputActionProperty indexChange;
    public LightningSpellScript spell;
    float leftGripValue;
    float rightGripValue;
    bool isFire;
    bool isButton;

    public Transform cameraTr;
    public Transform rightControllerTr;
    public GameObject sheild;
    //public ParticleSystem[] magicEffects;
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
        isButton = false;

    }

    // Update is called once per frame
    void Update()
    {
        OpenBook();
        //CastMagic();
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
    /*void CastMagic()
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
            //StartCoroutine(FireMagic());
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
            // 마법 생성 (날아가게 할 것임)
            GameObject magic = Instantiate(magicPrefabs[index], newPosition, Camera.main.transform.rotation);

            yield return new WaitForSeconds(0.5f);
        }
    }*/
    void IndexChange()
    {

        float xbutton = indexChange.action.ReadValue<float>();
        if (xbutton > 0.9f && !isButton)
        {
            StartCoroutine(XButton());
            index++;
            if (index >= 3)
                index = 0;
            Debug.Log("버튼X");
        }
        
    }
  IEnumerator XButton()
    {
        isButton = true;
        yield return new WaitForSeconds(0.5f);
        isButton = false;

    }
    public void MotionMagic(string rec)
    {
        if (rec == "O")
        {
            Vector3 shieldTransform = Camera.main.transform.position + Camera.main.transform.forward * 0.4f;
            GameObject shelid = Instantiate(sheild, shieldTransform, Camera.main.transform.rotation);
            Destroy(shelid, 3f);
        }
        else if (rec == "Slash")
        {
            if (index == 0)
            {
                Vector3 newPosition = rightControllerTr.position + cameraTr.forward * 0.1f;
                // 마법 생성 
                GameObject magic = Instantiate(magicPrefabs[index], newPosition, Camera.main.transform.rotation);
                Destroy(magic, 1.5f);
            }
            else if (index == 1)
            {
                Vector3 playerDirection = Camera.main.transform.forward;
                playerDirection.y = 0f;
                spell.Direction = playerDirection;
                spell.SpellStart.transform.position = rightControllerTr.position + cameraTr.forward * 0.1f;
                StartCoroutine(LightningSpell());
            }
            else if (index == 2) 
            {
                GameObject magic = Instantiate(magicPrefabs[index-1],Camera.main.transform.up* 3f, Quaternion.identity);
            }
        }
        else
            Debug.Log("안맞음");
    }
    IEnumerator LightningSpell()
    {
        spell.CastSpell();
        yield return new WaitForSeconds(0.6f);
        spell.StopSpell();
    }
}
