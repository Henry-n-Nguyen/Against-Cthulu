# Against Cthulhu : Facing the Darkness

**MoveStopMove** là một dự án game 2D thuộc thể loại **Platformer** được xây dựng bằng Unity Engine.

Trò chơi xoay quanh hành trình trở về nhân vật Alex. Một chiến binh đang trên
đường quay về quê hương để giải cứu quê hương trước sự tấn công của quái vật
đến từ lũ quái vật biến dị dưới sự điều khiển của một chúa quỷ tự xưng là Cthulhu,
với những ma thuật đầy lạ lẫm với anh ấy. Người chơi sẽ hóa thân vào nhật vật để
tìm đường quay về, trên đường đi có rất nhiều quái vật. người chơi phải hết sức tập
trung để có thể qua các màn, thu thập tài nguyên, nâng cấp bản thân bằng các trang
bị, phép thuật thu thập được và tiêu diệt Chúa quỷ để giải cứu quê hương.

---

## 🎮 Tổng quan trò chơi
Người chơi phải điều khiển nhân vật chạy liên tục, né tránh các đòn tấn công, tiêu diệt kẻ địch và thu thập vật phẩm để sinh tồn lâu nhất có thể.

* **Cơ chế tăng trưởng (Scaling System):** Nhân vật sử dụng tài nguyên để nâng cấp chỉ số của bản thân.
* **Vùng an toàn/Giới hạn:** Bản đồ có biên giới, buộc người chơi phải tương tác với nhau trong không gian hẹp dần.
* **Hệ thống vật phẩm (Shops):** Thu thập tiền vàng để nâng cấp điểm số và các Buff (Khiên bảo vệ, Nam châm hút tiền).
* **Hệ thống skills (Magics):** Thu thập tiền vàng để nâng cấp điểm số và các Buff (Khiên bảo vệ, Nam châm hút tiền).
* **High Score:** Lưu trữ và hiển thị điểm số cao nhất và tài nguyên đang có một cách cục bộ bằng `PlayerPrefs`.

---

## 🛠 Kỹ thuật & Tư duy lập trình (Tech Stack)
Dự án này tập trung vào việc tối ưu hóa hiệu suất và cấu trúc mã nguồn sạch (Clean Code), phù hợp với tiêu chuẩn phát triển game chuyên nghiệp:

1.  **Object Pooling:** Sử dụng để quản lý các chướng ngại vật và hiệu ứng hạt (particles), giúp giảm thiểu việc `Instantiate` và `Destroy` liên tục, tránh gây giật lag do Garbage Collector.
2.  **State Machine:** Quản lý các trạng thái của nhân vật (Idle, Run, Jump, Slide, Death) một cách logic, dễ dàng mở rộng thêm các hành động mới.
3.  **ScriptableObjects:** Dùng để lưu trữ dữ liệu về chỉ số nhân vật và cấu hình vật phẩm, giúp tách biệt dữ liệu khỏi logic code.
4.  **Singleton Pattern:** Áp dụng cho Game Manager và UI Manager để quản lý vòng đời game một cách tập trung.
5.  **Observer Pattenrn:** Áp dụng các sự kiện được đăng kí và quan sát giúp quản lý luồng game.

---
## Một số hình ảnh và video demo
<img width="621" height="350" alt="CS0" src="https://github.com/user-attachments/assets/013b363d-45d6-4208-a94f-62591f750ddd" />
<img width="621" height="344" alt="FinalBoss" src="https://github.com/user-attachments/assets/0491a9ee-ab5f-41e2-9c21-9d97232a995d" />
<img width="619" height="363" alt="Hall" src="https://github.com/user-attachments/assets/21642097-b426-452e-94a8-08939dfda03c" />
<img width="613" height="351" alt="InnerShop" src="https://github.com/user-attachments/assets/b01d4e48-e883-4b26-ab90-f4e87d8ce987" />
<img width="616" height="348" alt="Upgrade" src="https://github.com/user-attachments/assets/2a8d1523-2ebe-4525-8517-75d3a88734a1" />
<img width="616" height="348" alt="Upgrade" src="https://github.com/user-attachments/assets/574d46bb-42d1-4cd7-8c13-dfb79a433f5b" />
<img width="622" height="349" alt="BossFight" src="https://github.com/user-attachments/assets/7a1567c3-5422-4b51-a813-a9cf49e0815c" />

Video demo: https://youtu.be/UvmAbEIhCkg?si=-6VoGWf3i8ZAKg8m

 
