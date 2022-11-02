using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Sphere
{
    public Vector3 SC;

    public Vector3 ka,kd,ks;
    public float SR;
    public float ALPHA;
}
public class LineSphereIntersection : MonoBehaviour
{
    new public Renderer renderer;
    public Texture2D background;
    public Vector3 ka;
    public Vector3 kd;
    public Vector3 ks;
    public Vector3 Ia;
    public Vector3 Id;
    public Vector3 Is;
    public Vector3 PoI;
    public Vector3 n;
    public Vector3 LIGHT;
    public Vector3 CAMERA;
    public float ALPHA;
    public float SR;
    public Vector3 SC;

    public Vector3 contact;

    private Camera mainCam;
    public Vector2 CameraResolution;

    float frusttumHeight, frustumWidth;
    float pixelHeight, pixelWidth;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        Vector3 i = Illumination(PoI);
        
        // Create sphere
        GameObject sph = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sph.transform.position = SC;
        sph.transform.localScale = new Vector3(SR*2f, SR*2f, SR*2f);
        Renderer rend = sph.GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_Color", new Color(kd.x, kd.y, kd.z));
        rend.material.SetColor("_SpecColor", new Color(ks.x, ks.y, ks.z));
        mainCam.transform.position = CAMERA;
        mainCam.transform.LookAt(SC);

        // Create pointLight
        GameObject pointLight = new GameObject("ThePointLight");
        Light lightComp = pointLight.AddComponent<Light>();
        lightComp.type = LightType.Point;
        lightComp.color = new Color(Id.x, Id.y, Id.z);
        lightComp.intensity = 20;


        frusttumHeight = 2.0f * mainCam.nearClipPlane * Mathf.Tan(mainCam.fieldOfView * Mathf.Deg2Rad);
        frustumWidth = frusttumHeight * mainCam.aspect;
        pixelHeight = frusttumHeight / CameraResolution.y;
        pixelWidth = frustumWidth / CameraResolution.x;

        var texture = new Texture2D(Mathf.RoundToInt(CameraResolution.x), Mathf.RoundToInt(CameraResolution.y), TextureFormat.ARGB32, false);

        for (int x = 0; x < (int)frustumWidth; x++)
        {
            for (int y = 0; y < (int)frusttumHeight; y++)
            {
                //Color bg = background.GetPixelBilinear(x, y);
                //texture.SetPixel(x, y, bg);
            }
        }
        texture.Apply();
        
        for (int x = 0; x < CameraResolution.x; x++)
        {
            for (int y = 0; y < CameraResolution.y; y++)
            {
                Color color = GetPixel(new Vector3(x, y, 0f));
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        renderer.material.mainTexture = texture;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug Lines
        Vector3 l = LIGHT - PoI;
        Vector3 lp = n * Vector3.Dot(n.normalized, l);
        Vector3 lo = l - lp;
        Vector3 r = lp - lo;
        Vector3 v = CAMERA-PoI;

        Debug.DrawLine(mainCam.transform.position, contact, Color.white);
        Debug.DrawLine(PoI, l + PoI, Color.red);
        Debug.DrawLine(PoI, r + PoI, Color.magenta);
        Debug.DrawLine(PoI, n + PoI, Color.green);
        Debug.DrawLine(PoI, v + PoI, Color.blue);

        Debug.DrawLine(CAMERA, Cast(Vector2.zero), Color.yellow);

        // Debug.DrawLine(CAMERA, Cast(), Color.yellow);
    }

    Vector3 Cast(Vector3 coords)
    {
        float frusttumHeight = 2.0f * mainCam.nearClipPlane * Mathf.Tan(mainCam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustumWidth = frusttumHeight * mainCam.aspect;
        float pixelWidth = frustumWidth / CameraResolution.x;
        float pixelHeight = frusttumHeight / CameraResolution.y;
        Vector3 center = FindTopLeftFrusrtumNear();
        center += +(pixelWidth /2f) * mainCam.transform.right;
        center -= (pixelHeight /2f) * mainCam.transform.up;
        center += +(pixelWidth ) * mainCam.transform.right * coords.x;
        center -= (pixelHeight ) * mainCam.transform.up * coords.y;

        return center;
    }
    
    Color GetPixel(Vector3 coords)
    {

        Vector3 center = Cast(coords);

        Vector3 u = (center - mainCam.transform.position).normalized;
        Vector3 oc = mainCam.transform.position - center;

        float delta = (Vector3.Dot(u.normalized, oc)*Vector3.Dot(u.normalized, oc) - (oc.magnitude*oc.magnitude - SR*SR));
        if(delta < 0)
            return Color.black;
        
        float d_mas = (-2 * (Vector3.Dot(u.normalized, oc)) 
        + Mathf.Sqrt(2 * (Vector3.Dot(u.normalized, oc) * Vector3.Dot(u.normalized, oc)) 
        - 4 * (u.magnitude * u.magnitude) * ((oc.magnitude * oc.magnitude) - (SR * SR))))
        / (2 * (u.magnitude * u.magnitude));
        
        float d_menos = (-2 * (Vector3.Dot(u.normalized, oc)) 
        - Mathf.Sqrt(2 * (Vector3.Dot(u.normalized, oc) * Vector3.Dot(u.normalized, oc)) 
        - 4 * (u.magnitude * u.magnitude) * ((oc.magnitude * oc.magnitude) - (SR * SR))))
        / (2 * (u.magnitude * u.magnitude));

        float d = (delta == 0 ? d_mas : Mathf.Min(d_mas, d_menos));
        Vector3 color = Illumination(center + d * u);

        return new Color(color.x, color.y, color.z);
    }

    Vector3 Illumination(Vector3 PoI)
    {
        Vector3 A = new Vector3(ka.x * Ia.x, ka.y * Ia.y, ka.z * Ia.z);
        Vector3 D = new Vector3(ka.x * Id.x, kd.y * Id.y, kd.z * Id.z);
        Vector3 S = new Vector3(ks.x * Is.x, ks.y * Is.y, ks.z * Is.z);

        Vector3 l = LIGHT - PoI;
        Vector3 v = CAMERA - PoI;
        float dotNuLu = Vector3.Dot(n.normalized, l.normalized);
        float dotNuL = Vector3.Dot(n.normalized, l);

        Vector3 lp = n * dotNuL;
        Vector3 lo = l - lp;
        Vector3 r = lp-lo;
        D *= dotNuLu;
        S *= Mathf.Pow(Vector3.Dot(v.normalized,r.normalized),ALPHA);
        return A + D + S;
    }

    Vector3 FindTopLeftFrusrtumNear()
    {
        //localizar camara
        Vector3 o = mainCam.transform.position;
        //mover hacia adelante
        Vector3 p = o + mainCam.transform.forward * mainCam.nearClipPlane;
        //obtener dimenciones del frustum
        float frusttumHeight = 2.0f * mainCam.nearClipPlane * Mathf.Tan(mainCam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustumWidth = frusttumHeight * mainCam.aspect;
        //mover hacia arriba, media altura
        p += mainCam.transform.up * frusttumHeight / 2.0f;
        //mover a la izquierda, medio ancho
        p -= mainCam.transform.right * frustumWidth / 2.0f;
        return p;

    }
}