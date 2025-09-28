# Web_B1  
# BÀI TẬP VỀ NHÀ 01:
TẠO SOLUTION GỒM CÁC PROJECT SAU:
1. DLL đa năng, keyword: c# window library -> Class Library (.NET Framework) bắt buộc sử dụng .NET Framework 2.0: giải bài toán bất kỳ, độc lạ càng tốt, phải có dấu ấn cá nhân trong kết quả, biên dịch ra DLL. DLL độc lập vì nó ko nhập, ko xuất, nó nhận input truyền vào thuộc tính của nó, và trả về dữ liệu thông qua thuộc tính khác, hoặc thông qua giá trị trả về của hàm. Nó độc lập thì sẽ sử dụng được trên app dạng console (giao diện dòng lệnh - đen sì), cũng sử dụng được trên app desktop (dạng cửa sổ), và cũng sử dụng được trên web form (web chạy qua iis).  
2. Console app, bắt buộc sử dụng .NET Framework 2.0, sử dụng được DLL trên: nhập được input, gọi DLL, hiển thị kết quả, phải có dấu án cá nhân. keyword: c# window Console => Console App (.NET Framework), biên dịch ra EXE  
3. Windows Form Application, bắt buộc sử dụng .NET Framework 2.0**, sử dụng được DLL đa năng trên, kéo các control vào để có thể lấy đc input, gọi DLL truyền input để lấy đc kq, hiển thị kq ra window form, phải có dấu án cá nhân; keyword: c# window Desktop => Windows Form Application (.NET Framework), biên dịch ra EXE  
4. Web đơn giản, bắt buộc sử dụng .NET Framework 2.0, sử dụng web server là IIS, dùng file hosts để tự tạo domain, gắn domain này vào iis, file index.html có sử dụng html css js để xây dựng giao diện nhập được các input cho bài toán, dùng mã js để tiền xử lý dữ liệu, js để gửi lên backend. backend là api.aspx, trong code của api.aspx.cs thì lấy được các input mà js gửi lên, rồi sử dụng được DLL đa năng trên. kết quả gửi lại json cho client, js phía client sẽ nhận được json này hậu xử lý để thay đổi giao diện theo dữ liệu nhận dược, phải có dấu án cá nhân. keyword: c# window web => ASP.NET Web Application (.NET Framework) + tham khảo link chatgpt thầy gửi. project web này biên dịch ra DLL, phải kết hợp với IIS mới chạy được.  
#    =========== BÀI LÀM ==============
    1. Ứng dụng tính chỉ số BMI (Body Mass Index) và tư vấn nhanh sức khỏe, giúp người dùng nhập chiều cao (cm) và cân nặng (kg) để nhận kết quả BMI, phân loại thể trạng theo chuẩn WHO và khuyến nghị cơ bản.
    2. Hệ thống được triển khai theo mô hình 4 project trên .NET Framework 2.0: MyHealthLib (DLL lõi tính toán), HealthConsoleApp (Console dùng thử), HealthWinFormApp (giao diện desktop), HealthWebApp (ASP.NET WebForms + API trả JSON).
    3. Tính năng chính: kiểm tra dữ liệu đầu vào, tính BMI, phân loại (Thiếu cân/Bình thường/Thừa cân/Béo phì), sinh báo cáo tiếng Việt; bản Web cho phép gọi API từ trang index để hiển thị tức thì.
    4. Mục tiêu: cung cấp một giải pháp nhỏ gọn, dễ triển khai trong môi trường cũ (CLR 2.0), có cấu trúc tách lớp rõ ràng để tái sử dụng và mở rộng sau này.
# Tạo Solution và 4 Project  
    1. MyHealthLib (Class Library)  
    2. HealthConsoleApp (Console App)  
    3. HealthWinFormApp (Windows Forms App)  
    4. HealthWebApp (ASP.NET Web Application)  
# Project 1 — MyHealthLib (Class Library, .NET Framework 2.0)
Mục tiêu: thư viện lõi tính BMI dùng chung.  
File: HealthChecker.cs (class chính), Class1.cs (không bắt buộc).  
HealthChecker.cs: Input HeightCm, WeightKg; Output BMI, Category, Report, LastError; hàm Process() → validate > 0 → tính BMI → phân loại → sinh Report → trả 0 nếu OK, <0 nếu lỗi.  
Build: tạo MyHealthLib.dll tại MyHealthLib\bin\Debug.  
# Hình ảnh kiểm thử  
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/a71ac9df-92b8-4a6e-9c6a-5edb5ab89acf" />  

# Project 2 — HealthConsoleApp (Console, .NET Framework 2.0)  
Tham chiếu: Add Reference → Projects → MyHealthLib (Copy Local = True).  
Program.cs: nhập chiều cao/cân nặng → gọi MyHealthLib.HealthChecker.Process() → in BMI/Category/Report hoặc LastError.  
Test: ví dụ 170/65 → BMI ≈ 22.5, “Bình thường”.  
# Hình ảnh kiểm thử  
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/35b4db97-d06a-4627-9883-0d6168b37aba" />  

# Project 3 — HealthWinFormApp (Windows Forms, .NET Framework 2.0)  
Tham chiếu: MyHealthLib.  
Form1.cs: 2 TextBox (cm, kg) + 1 Button “Tính BMI” + vùng kết quả (TextBox multiline/Label).  
Button Click: parse input → HealthChecker.Process() → hiển thị kết quả; lỗi → MessageBox LastError.  
# Hình ảnh kiểm thử  
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/64a70afd-3900-48ff-b0aa-ea6119582da2" />  

# Project 4 — HealthWebApp (ASP.NET Web Forms, .NET Framework 2.0)  
Tham chiếu: MyHealthLib (hoặc copy MyHealthLib.dll vào HealthWebApp\bin).  
File gốc project:  
index.html: 2 ô (cm, kg), nút “Tính BMI”, JS POST form-urlencoded tới API, nhận JSON, hiển thị.  
api.ashx: WebHandler trỏ Class="HealthWebApp.Api".  
Api.cs: public Api : IHttpHandler; đọc h, w; gọi HealthChecker; trả JSON; set Content-Type application/json; try/catch.  
Web.config: chỉ <compilation debug="true" /> và <customErrors mode="Off" /> (không targetFramework).  
IIS Express: Properties → Web → Project URL (HTTP) → Create Virtual Directory → Start Action = index.html → F5.  
Test API: /api.ashx?h=170&w=65 → trả JSON.  
# Hình ảnh kiểm thử  
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/479e0c13-7356-4184-99d0-be3c38ad3616" />  
