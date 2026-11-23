using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    [SerializeField] private Transform hedef;
    [SerializeField] private float yumusaklik = 5f;
    [SerializeField] private float pixelsPerUnit = 64f; // haritanın PPU değeri

    // Kameranın gidebileceği sınırlar (haritanın kenarları)
    [SerializeField] private Vector2 minSinir = new Vector2(-13f, -10f);
    [SerializeField] private Vector2 maxSinir = new Vector2(13f, 9f);

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (hedef == null) return;

        Vector3 hedefPozisyon = new Vector3(hedef.position.x, hedef.position.y, transform.position.z);
        Vector3 yeniPozisyon = Vector3.Lerp(transform.position, hedefPozisyon, yumusaklik * Time.deltaTime);

        // Kamera haritanın dışını göstermesin
        float yukseklik = cam.orthographicSize;
        float genislik = yukseklik * cam.aspect;
        yeniPozisyon.x = Sinirla(yeniPozisyon.x, minSinir.x + genislik, maxSinir.x - genislik);
        yeniPozisyon.y = Sinirla(yeniPozisyon.y, minSinir.y + yukseklik, maxSinir.y - yukseklik);

        // Kamerayı piksel ızgarasına oturt: yoksa yürürken zemin titrer
        yeniPozisyon.x = Mathf.Round(yeniPozisyon.x * pixelsPerUnit) / pixelsPerUnit;
        yeniPozisyon.y = Mathf.Round(yeniPozisyon.y * pixelsPerUnit) / pixelsPerUnit;

        transform.position = yeniPozisyon;
    }

    // Harita ekrandan küçükse kamerayı ortada tut
    private float Sinirla(float deger, float min, float max)
    {
        if (min > max) return (min + max) / 2f;
        return Mathf.Clamp(deger, min, max);
    }
}
