using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CenaText : MonoBehaviour
{
    private bool entrou = false;
    [SerializeField] private TMP_Text[] text;
    [SerializeField] private GameObject chuva;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!entrou && collision.tag == "Player")
        {
            text[0].gameObject.SetActive(true);
            text[1].gameObject.SetActive(true);
            entrou = true;
            Debug.Log("Ativou o texto");
            if (chuva != null && chuva.activeInHierarchy)
            {
                chuva.SetActive(!chuva.activeInHierarchy);
            }
        }
    }
}
