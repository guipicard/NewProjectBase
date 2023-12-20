using UnityEngine;

public class CheatManager : MonoBehaviour
{
    private const int m_CrystalTypesNum = 4;


    private static CheatManager m_UniqueInstance;
    [SerializeField] private GameObject Player;

    private bool godMode;
    private string[] m_AiTags;

    private string[] m_CrystalTags;

    private Rect m_WinRect = new(20, 20, 500, 500);
    private bool manuelControl;
    private bool maxDamage;
    private bool showCheatsState;

    private bool showCrystalState;
    private bool showWindow;

    public static CheatManager Instance
    {
        get
        {
            if (m_UniqueInstance == null)
            {
                var go = new GameObject("CheatManager");
                m_UniqueInstance = go.AddComponent<CheatManager>();
            }

            return m_UniqueInstance;
        }
    }

    private void Awake()
    {
        if (m_UniqueInstance != null && m_UniqueInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        m_UniqueInstance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        godMode = false;
        maxDamage = false;
        manuelControl = false;
        showWindow = false;
        showCrystalState = false;
        showCheatsState = false;
        m_AiTags = new string[m_CrystalTypesNum] { "Green_Ai", "Red_Ai", "Yellow_Ai", "Blue_Ai" };

        m_CrystalTags = new string[m_CrystalTypesNum]
            { "Green_Crystal_Obj", "Red_Crystal_Obj", "Yellow_Crystal_Obj", "Blue_Crystal_Obj" };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3)) ToggleShowWindow();
    }

    private void OnGUI()
    {
        if (showWindow) m_WinRect = GUI.Window(0, m_WinRect, WindowFunction, "GameInfo");
    }

    private void WindowFunction(int windowID)
    {
    }


    private void ToggleShowWindow()
    {
        showWindow = !showWindow;
    }

    private void ToggleshowCrystalState()
    {
        showCrystalState = !showCrystalState;
    }

    private void ToggleshowCheatsState()
    {
        showCheatsState = !showCheatsState;
    }
}