using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    private float _baseSpeed = 10f;
    private float _gravidade = 9.8f;
    [SerializeField]
    private GameObject playerCamera;
    [SerializeField]
    public GameObject pause;
    private float cameraRotation;
    private CharacterController characterController;
    public GameObject flashLight;
    private bool flashLightOn = true;
    private int ItensAbleToBeCollected = 5;
    public GameObject[] Coletaveis;
    private float messageTime = 10.0f;
    private float initMassage;
    private UnityEngine.AI.NavMeshAgent runner;
    private string[] historinhas = {
        "",
        "A emboscada foi feita utilizando blindados T-80 roubados, eles são equipados com holofotes, não deve ser difícil encontra-los na floresta a frente.",
        "Info: As tripulações dos tanques desapareceram, devo buscar um lugar seguro, no mapa havia uma vila entre as montanhas, procurarei por la.",
        "Info: Nenhum sinal de vida por aqui, há um bunker dos anos 70 aqui perto, capaz de ainda existir algo por la, lembro que devo entrar mais afundo entre as montanhas.",
        "Info: Lendo as notas da missão, devo entregar esses pacotes há uma base militar do outro lado das montanhas, mas não quero voltar pelo caminho que vim, mas acho que não terei escolha."
    };
    private int CollectableIndex = 0;
    public Text textoUI;
    private AudioSource SomLanterna;
    void Start()
    {

        SomLanterna = GetComponent<AudioSource>();
        runner = GameObject.FindGameObjectWithTag("Runner").GetComponent<UnityEngine.AI.NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Main Camera");
        cameraRotation = 0.0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void HideMouse()
    {
        Cursor.visible = false;
    }

    private void ShowMouse()
    {
        Cursor.visible = true;
    }

    void LateUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(playerCamera.transform.position, transform.forward*10.0f, Color.magenta);
        if(Physics.Raycast(playerCamera.transform.position, transform.forward, out hit, 100.0f))
        {
            Debug.Log(hit.collider.name);
        }
    }

    private bool itsCloseToCollectable()
    {
        return Vector3.Distance(transform.position, Coletaveis[CollectableIndex].transform.position) <= 3;
    }

    private void Collect()
    {
        runner.speed *= 2.0f;
        CollectableIndex++;
        if(CollectableIndex >= ItensAbleToBeCollected)
        {
            GameData.Won = true;
            ShowMouse();
            SceneManager.LoadScene("EndGame");
        }
        textoUI.text = historinhas[CollectableIndex];
        initMassage = Time.time;
    }
    void Update()
    {
        // Handling Pause
        if(GameData.Paused)
        {
            if(Input.GetButtonDown("Cancel"))
            {
                HideMouse();
                pause.SetActive(false);
                GameData.Paused = false;
                Cursor.visible = false;
            }
            return;
        }
        if(Input.GetButtonDown("Cancel"))
        {
            GameData.Paused = true;
            pause.SetActive(true);
            ShowMouse();
            textoUI.text = "";
            return;
        }

        if(Input.GetButtonDown("Jump"))
        {
            SomLanterna.Play();
            flashLightOn = !flashLightOn;
            flashLight.SetActive(flashLightOn);
            
        }

        // Handling Movement
        bool groundedPlayer = characterController.isGrounded;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = 0;
        if(!groundedPlayer){
            y = -_gravidade;
        }
        Vector3 direction = transform.right * x + transform.up * y+ transform.forward * z;
        characterController.Move(direction * _baseSpeed * Time.deltaTime);

        // Handling Camera Movement
        float mouse_dX = Input.GetAxis("Mouse X");
        float mouse_dY = -Input.GetAxis("Mouse Y");
        cameraRotation += mouse_dY;
        Mathf.Clamp(cameraRotation, -75.0f, 75.0f);
        transform.Rotate(Vector3.up, mouse_dX);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0.0f, 0.0f);

        // Handling Collecting Itens
        if(itsCloseToCollectable())
        {
            Collect();
        }
        if(Time.time - initMassage > messageTime)
        {
            textoUI.text = "";
        }
    }
}