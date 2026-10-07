using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    [SerializeField] private Text textUI;
    public static int moedas;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moedas = 0;
    }

    // Update is called once per frame
    void Update()
    {
        textUI.text =""+ moedas;
    }
}
