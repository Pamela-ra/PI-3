using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueControl : MonoBehaviour
{
    private Dialogue di;

    private void Start()
    {
        di = FindObjectOfType<Dialogue>();
    }

    [Header("Components")]
    public GameObject Estágio;
    public GameObject AEstudantil;
    public GameObject Requerimentos;
    public GameObject SecVirtual;
    public GameObject dialogueObj;
    public GameObject IniciandoEstagio;
    public GameObject DuranteEstagio;
    public GameObject FinalizandoEstagio;
    public Image profile;
    public Text speechText;
    public Text actorNameText;

    [Header("Settings")]
    public float typingSpeed;
    private string[] sentences;
    private int index;




    public void Speech(Sprite p, string[] txt, string actorName)
    {
        SecVirtual.SetActive(false);
        dialogueObj.SetActive(true);
        profile.sprite = p;
        sentences = txt;
        actorNameText.text = actorName;
        StartCoroutine(TypeSentence());
    }
    IEnumerator TypeSentence()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
     }

    public void NextSentence()
    {
        if (speechText.text == sentences[index])
        {

            //Ainda há textos
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else //lido quando os textos acabam
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                di._colidindo = true;
            }
        }
        return;
    }
    public void AbrirEstágio()
    {
        dialogueObj.SetActive(false);
        Estágio.SetActive(true);
    }

    public void FecharEstágio()
    {
        Estágio.SetActive(false);
        dialogueObj.SetActive(true);
    }

    public void AbrirIE()
    {
        Estágio.SetActive(false);
        IniciandoEstagio.SetActive(true);
    }
    public void FecharIE()
    {
        Estágio.SetActive(true);
        IniciandoEstagio.SetActive(false);
    }

    public void AbrirDE()
    {
        Estágio.SetActive(false);
        DuranteEstagio.SetActive(true);
    }
    public void FecharDE()
    {
        Estágio.SetActive(true);
        DuranteEstagio.SetActive(false);
    }
    public void AbrirFE()
    {
        Estágio.SetActive(false);
        FinalizandoEstagio.SetActive(true);
    }
    public void FecharFE()
    {
        Estágio.SetActive(true);
        FinalizandoEstagio.SetActive(false);
    }
    public void AbrirAssEstudantil()
    {
        dialogueObj.SetActive(false);
        AEstudantil.SetActive(true);
    }

    public void FecharAssEstudantil()
    {
        AEstudantil.SetActive(false);
        dialogueObj.SetActive(true);
    }

    public void AbrirRequerimentos()
    {
        dialogueObj.SetActive(false);
        Requerimentos.SetActive(true);
    }

    public void FecharRequerimentos()
    {
        Requerimentos.SetActive(false);
        dialogueObj.SetActive(true);
    }

    public void AbrirChatBot()
    {
        Speech(di.profile, di.speechTxt, di.actorName); ;
        SecVirtual.SetActive(false);
        dialogueObj.SetActive(true);
        di._colidindo = false;
    }

    public void FecharChatBot()
    {
        dialogueObj.SetActive(false);
        di._colidindo = true;
        speechText.text = "";
    }

    public void IVSEdital()
    {
        Application.OpenURL("https://www.ifsc.edu.br/editais-ivs"); 
    }

    public void IVSResultados() 
    {
        Application.OpenURL("https://www.ifsc.edu.br/resultados-ivs");

    }

    public void AMEdital() 
    {
        Application.OpenURL("https://www.ifsc.edu.br/editais-auxilio-moradia");
    }
    public void AMResultados()
    {
        Application.OpenURL("https://www.ifsc.edu.br/resultados-moradia");
    }

    public void APEdital()
    {
        Application.OpenURL("https://www.ifsc.edu.br/editais-paevs");
    }
    public void APResultados()
    {
        Application.OpenURL("https://www.ifsc.edu.br/resultados-paevs");
    }

    public void RequerimentosForm()
    {
        Application.OpenURL("https://limesurvey.ifsc.edu.br/index.php/273639");
    }

    
    public void RequerimentosResultados()
    {
        Application.OpenURL("https://docs.google.com/spreadsheets/d/1fQGq17wx6L2lBOPtCQC4j2ai3ECRfmULo9__xD8_em0/edit#gid=1454380375");
    }
    public void OM() 
    {
        Application.OpenURL("https://www.ifsc.edu.br/documents/35981/0/cursos+check+list.pdf/5206fbad-c52f-44b7-b686-996596cb24f1");
    }
    public void HorasComplementares()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLScfIpVoirslzQATh-m06kxJigi5cO0v_TtQowibhlh_UMCAFQ/viewform");

    }
    public void VagasE()
    {
        Application.OpenURL("https://www.ifsc.edu.br/web/campus-itajai/vagas-de-estagio-e-emprego");
    }

    public void TCE()
    {
        Application.OpenURL("https://www.ifsc.edu.br/documents/d/campus-itajai/tce_in24_20_retificado-docx");
    }

    public void FAF()
    {
        Application.OpenURL("https://www.ifsc.edu.br/documents/35981/1747296/Ficha+de+avalia%C3%A7%C3%A3o+de+frequ%C3%AAncia/95cd1bb6-c4a3-4e39-9070-fdb1882fb1a5");
    }
    public void FAE()
    {
        Application.OpenURL("https://www.ifsc.edu.br/documents/35981/1747296/Ficha+de+avalia%C3%A7%C3%A3o+de+est%C3%A1gio+-+modelo.doc/04ce936b-ff65-4e73-91f1-872068b2badd");
    }
    public void RPE()
    {
        Application.OpenURL("https://www.ifsc.edu.br/documents/35981/1747296/relatorio_parcial_estagio.doc/473583fe-daa9-44a3-94b2-6249474d3c4b");
    }

    public void RPE2()
    {
        Application.OpenURL("https://www.ifsc.edu.br/documents/35981/1747296/Relat%C3%B3rio+parcial+de+est%C3%A1gio_/9b7ee4a8-368a-416c-b14b-6a98884c16f1");
    }
    public void TAE()
    {
        Application.OpenURL("https://www.ifsc.edu.br/documents/35981/1747296/Termo+Aditivo+de+Est%C3%A1gio+2022/5d83974a-dd6a-49da-b00e-aac0dac28563");
    }
    public void TRE()
    {
        Application.OpenURL("https://www.ifsc.edu.br/documents/35981/1747296/Termo+de+Rescis%C3%A3o+de+est%C3%A1gio.docx/9747cac4-d208-4b7f-922e-947390a2feac");
    }
    public void RFET()
    {
        Application.OpenURL("https://www.ifsc.edu.br/documents/35981/1747296/Relat%C3%B3rio+Final+Est%C3%A1gio/3c02ec66-b6d3-4dc7-b913-fe0c42315a07");
    }
    public void ARE()
    {
        Application.OpenURL("https://www.ifsc.edu.br/documents/35981/1747296/ATESTADO+DE+REALIZA%C3%87%C3%83O+DE+EST%C3%81GIO.odt/ea049a8c-f75d-4edd-8c0b-6ccd085395fe");
    }
}
