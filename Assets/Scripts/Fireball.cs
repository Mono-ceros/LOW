using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    float damage = 20f;
    float speed = 1000f;
    float destroyDistance = 20f;

    Rigidbody rb;
    Transform tr;
    ParticleSystem fireEffect;
    Vector3 startPosition;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        fireEffect = GetComponentInChildren<ParticleSystem>();
        startPosition = transform.position;
    }

    private void OnEnable()
    {
        Vector3 playerDirection = Camera.main.transform.forward;

        // Y �� ȸ���� ������� �ʵ��� ����
        playerDirection.y = 0f;

        // ����ȭ�Ͽ� ���� ���ͷ� ��ȯ
        playerDirection.Normalize();

       
        rb.AddForce(playerDirection * speed);

        fireEffect.Play();

    }
    private void OnDisable()
    {

        tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
        rb.Sleep();
        fireEffect.Stop();
    }
    void Update()
    {
        float distance = Vector3.Distance(startPosition, transform.position);
        if (distance >= destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
