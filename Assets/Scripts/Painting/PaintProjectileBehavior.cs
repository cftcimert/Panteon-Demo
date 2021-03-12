using UnityEngine;

public class PaintProjectileBehavior : MonoBehaviour
{
    public Color paintColor = Color.white;

    public float shotForce;
    public float paintDiameter = 1.5f;

    [HideInInspector] public Vector3 target;
    [HideInInspector] public Vector3 startPoint;

    GameManager gameManager;
    MenuController menuController;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        menuController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MenuController>();
    }

    private void OnEnable()
    {
        transform.position = startPoint;
    }

    private void Update()
    {
        GoToTarget();
    }

    void OnTriggerEnter(Collider other)
    {
        // Adds paintballs to the pool
        PaintballPool.instance.AddPaintballToPool(gameObject);

        // creates particle effects.
        ParticleSystem cloudParticle = Instantiate(PaintProjectileManager.GetInstance().cloudParticlePrefab);
        ParticleSystem burstParticle = Instantiate(PaintProjectileManager.GetInstance().burstParticlePrefab);

        MyShaderBehavior script = null;
        cloudParticle.transform.position = transform.position;
        burstParticle.transform.position = transform.position;

        var cloudSettings = cloudParticle.main;
        var burstSettings = burstParticle.main;
        cloudSettings.startColor = paintColor;
        burstSettings.startColor = paintColor;

        cloudParticle.Play();
        burstParticle.Play();

        PaintProjectileManager manager = PaintProjectileManager.GetInstance();

        for (int i = 0; i < 14; ++i)
        {
            if (Physics.Raycast(transform.position, manager.GetSphereRay(i), out RaycastHit hit, paintDiameter))
            {
                if (hit.collider is MeshCollider)
                {
                    script = hit.collider.gameObject.GetComponent<MyShaderBehavior>();
                    if (null != script)
                    {
                        script.PaintOnColored(hit.textureCoord2, manager.GetRandomProjectileSplash(), paintColor);
                        script.PaintOnColored(hit.textureCoord2, manager.GetRandomProjectileSplash(), paintColor);
                    }
                }
            }
        }

        if (script != null)
        {
            float value = TextureColorFillCalculator.CalculateFill(GetObjectTexture().GetPixels(), paintColor, 0) * 100;
            menuController.SetSliderValue(value);
        }
    }

    Texture2D GetObjectTexture()
    {
        Texture mainTexture = FindObjectOfType<MyShaderBehavior>().m_texture;
        Texture2D texture2D = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGBA32, false);

        RenderTexture renderTexture = new RenderTexture(mainTexture.width, mainTexture.height, 32);
        Graphics.Blit(mainTexture, renderTexture);

        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        return texture2D;
    }

    void GoToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * shotForce);
    }
}
