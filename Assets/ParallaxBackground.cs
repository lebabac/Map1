using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;           // Kéo Main Camera vào đây
    public float parallaxEffect;     // Chỉnh độ trượt (0 đến 1)

    void Start()
    {
        startpos = transform.position.x;
        // Lấy chiều dài của tấm ảnh để biết khi nào cần lặp lại
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Tính toán khoảng cách background đã di chuyển so với Camera
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        // Di chuyển background
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        // Logic lặp lại vô tận (Reset vị trí khi đi quá giới hạn)
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}