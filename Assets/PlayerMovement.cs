using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Cài đặt di chuyển")]
    public float speed = 5f;
    public float jumpForce = 7f;

    [Header("Cài đặt nhảy")]
    public int maxJumps = 2; // Số lần nhảy tối đa (2 = Double Jump)
    private int jumpCount;   // Biến đếm số lần đã nhảy

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpCount = 0; // Ban đầu chưa nhảy lần nào
    }

    void Update()
    {
        // 1. Xử lý di chuyển Trái/Phải
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // 2. Quay mặt nhân vật
        if (moveInput > 0) transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (moveInput < 0) transform.rotation = Quaternion.Euler(0, 180, 0);

        // 3. Xử lý Nhảy (SỬA LẠI PHẦN NÀY)
        if (Input.GetButtonDown("Jump"))
        {
            // Kiểm tra: Nếu số lần đã nhảy < số lần tối đa thì cho nhảy tiếp
            if (jumpCount < maxJumps)
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        // Mẹo nhỏ: Reset vận tốc Y về 0 trước khi nhảy
        // Giúp cú nhảy thứ 2 luôn cao bằng cú nhảy đầu (không bị lực rơi kéo xuống)
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

        // Thêm lực nhảy
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

        // Tăng biến đếm lên 1
        jumpCount++;
    }

    // 4. Hàm phát hiện chạm đất để hồi lại lượt nhảy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra va chạm: Nếu điểm va chạm hướng lên trên (tức là đất dưới chân)
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                jumpCount = 0; // Reset số lần nhảy về 0
                break; // Tìm thấy đất rồi thì dừng kiểm tra
            }
        }
    }
}