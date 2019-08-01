using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour
{
    [SerializeField] GameObject canvas_debat;
    [SerializeField] GameObject canvas_vainqueur;
    [SerializeField] GameObject canvas_vehicule;
    [SerializeField] GameObject helpBg;
    [SerializeField] Button back;

    private string debat;
    private string vainqueur;
    private string button;
    private int sens;

    // Start is called before the first frame update
    void Start()
    {
        sens = 0;
        if (Partie.Langue == "FR")
        {
            debat = "Vous pouvez débattre des usages proposés pour ce véhicule autonome. Pour cela, vous pouvez cliquer sur les jetons\n" +
                "présents sur votre espace personnel. Il apparaîtra devant vous sur la table numérique. Vous pouvez ensuite\n" +
                "l'attribuer à un autre joueur en le glissant sur sa carte, tout en présentant votre argument en même temps.";

            vainqueur = "Le concepteur du véhicule autonome choisit maintenant l'usage qu'il a trouvé le plus convaincant.\n" +
                "Il faut pour cela cliquer sur la carte du personnage associé à cet usage, puis de cliquer sur le bouton \"valider\"\n" +
                "qui s'affiche. Vous pouvez changer votre sélection avant de cliquer sur le bouton.";

            button = "Retour";
        }
        else
        {
            debat = "You can discuss the proposed uses for this autonomous vehicle. To do so, you can click on the tokens on\n" +
                "your personal space. It will then appear on the digital table in front of you. You can then assign it to another\n" +
                "player by making it slide onto their card, while you present your argument";

            vainqueur = "The autonomous vehicle's designer now chooses the use he found the most convicing.\n" +
                "To do so they need to click on the character's card that is associated to this use,\n" +
                "and click on the \"Confirm\" button that will appear.\n" +
                "You can change your selection before clickin the button.";

            button = "Back";
        }
        back.onClick.AddListener(() => OnBackClicked());
        Debug.Log("start done");
    }

    private void OnBackClicked()
    {
        helpBg.SetActive(false);
        canvas_vehicule.SetActive(true);
    }

    private void OnEnable()
    {
        if (canvas_debat.activeSelf)
            helpBg.transform.GetChild(0).GetComponent<Text>().text = debat;
        else
            helpBg.transform.GetChild(0).GetComponent<Text>().text = vainqueur;
        helpBg.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = button;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse Down");
        Rotate();
    }

    private void Rotate()
    {
        sens = (sens+1) % 4;
        Debug.Log("sens = " + sens);
        GameObject text = helpBg.transform.GetChild(0).gameObject;
        text.transform.Rotate(Vector3.back, 90);
        back.transform.Rotate(Vector3.back, 90);
        Vector2 vertical = new Vector2(1080, 1920);
        Vector2 horizontal = new Vector2(1920, 1080);

        switch (sens)
        {
            case 0:
                back.GetComponent<RectTransform>().localPosition = new Vector3((float)7.6, (float)4.15);
                text.GetComponent<RectTransform>().sizeDelta = horizontal;
                text.GetComponent<BoxCollider2D>().size = horizontal;
                break;
            case 1:
                back.GetComponent<RectTransform>().localPosition = new Vector3((float)8.2, (float)-3.3);
                text.GetComponent<RectTransform>().sizeDelta = vertical;
                text.GetComponent<BoxCollider2D>().size = vertical;
                break;
            case 2:
                back.GetComponent<RectTransform>().localPosition = new Vector3((float)-7.6, (float)-4.15);
                text.GetComponent<RectTransform>().sizeDelta = horizontal;
                text.GetComponent<BoxCollider2D>().size = horizontal;
                break;
            case 3:
                back.GetComponent<RectTransform>().localPosition = new Vector3((float)-8.2, (float)3.3);
                text.GetComponent<RectTransform>().sizeDelta = vertical;
                text.GetComponent<BoxCollider2D>().size = vertical;
                break;
        }
    }
}
