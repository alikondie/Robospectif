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
    private Vector2 vertical = new Vector2(1080, 1920);
    private Vector2 horizontal = new Vector2(1920, 1080);

    private void OnBackClicked()
    {
        helpBg.SetActive(false);
        if (canvas_debat.activeSelf)
            canvas_vehicule.SetActive(true);
    }

    private void OnEnable()
    {
        sens = 0;
        GameObject text = helpBg.transform.GetChild(0).gameObject;
        text.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        back.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        back.GetComponent<RectTransform>().localPosition = new Vector3((float)7.6, (float)4.15);
        if (Partie.Langue == "FR")
        {
            if (Partie.Type == "expert")
                debat = "Les investisseurs décident ou non de financer un projet. Pour être validé, un projet doit recevoir 3 jetons verts.\n" +
                    "L'investisseur prive commence. Lorsqu'il a fini d'attribuer ses jetons, il décide de laisser la main à l'investisseur public.\n" +
                    "L'investisseur public peut à son tour financer des projets grâce à ses jetons verts, mais peut également poser un véto sur un projet\n" +
                    "déjà financé en lui attribuant un jeton rouge.";
            else
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
            if (Partie.Type == "expert")
                debat = "Investors decide wether they want to fund a project or not. To be valid, a project must receive 3 green tokens.\n" +
                    "The private investor starts first. When they're done assigning their tokens, they hand over to the public investor.\n" +
                    "The public investor can then fund projects by giving green tokens, but they also can deny a project that's already valid\n" +
                    "by assigning it a red token.";
            else
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
        if (canvas_debat.activeSelf)
            helpBg.transform.GetChild(0).GetComponent<Text>().text = debat;
        else
            helpBg.transform.GetChild(0).GetComponent<Text>().text = vainqueur;
        helpBg.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = button;
    }

    private void OnMouseDown()
    {
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
               // text.GetComponent<RectTransform>().sizeDelta = horizontal;
                break;
            case 1:
                back.GetComponent<RectTransform>().localPosition = new Vector3((float)8.2, (float)-3.3);
               // text.GetComponent<RectTransform>().sizeDelta = vertical;
                break;
            case 2:
                back.GetComponent<RectTransform>().localPosition = new Vector3((float)-7.6, (float)-4.15);
             //   text.GetComponent<RectTransform>().sizeDelta = horizontal;
                break;
            case 3:
                back.GetComponent<RectTransform>().localPosition = new Vector3((float)-8.2, (float)3.3);
             //   text.GetComponent<RectTransform>().sizeDelta = vertical;
                break;
        }
    }
}
