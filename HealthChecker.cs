using System;
using System.Text;

namespace MyHealthLib
{
    // DLL độc lập: nhập qua thuộc tính, gọi Process(), lấy kết quả qua thuộc tính/Report
    public class HealthChecker
    {
        private double _heightCm;
        private double _weightKg;

        private double _bmi;
        private string _category;
        private string _advice;
        private double _idealMinKg;
        private double _idealMaxKg;
        private string _report;
        private string _signature = "Made by Dinhdat2k4";
        private string _lastError;

        public double HeightCm
        {
            get { return _heightCm; }
            set { _heightCm = value; }
        }

        public double WeightKg
        {
            get { return _weightKg; }
            set { _weightKg = value; }
        }

        public double BMI { get { return _bmi; } }
        public string Category { get { return _category; } }
        public string Advice { get { return _advice; } }
        public double IdealMinKg { get { return _idealMinKg; } }
        public double IdealMaxKg { get { return _idealMaxKg; } }
        public string Report { get { return _report; } }
        public string Signature
        {
            get { return _signature; }
            set { _signature = value; }
        }
        public string LastError { get { return _lastError; } }

        // Trả: 1=gầy, 2=bình thường, 3=thừa cân, 4=béo phì; <0 lỗi
        public int Process()
        {
            _lastError = null;
            if (_heightCm <= 0 || _weightKg <= 0)
            {
                _lastError = "Chiều cao/cân nặng phải > 0.";
                return -1;
            }
            if (_heightCm < 50 || _heightCm > 300)
            {
                _lastError = "Chiều cao (cm) không hợp lệ (50..300).";
                return -2;
            }
            if (_weightKg < 2 || _weightKg > 500)
            {
                _lastError = "Cân nặng (kg) không hợp lệ (2..500).";
                return -3;
            }

            double h = _heightCm / 100.0;
            double bmi = _weightKg / (h * h);
            _bmi = Math.Round(bmi, 1);

            // Phân loại WHO
            if (_bmi < 18.5) _category = "Gầy";
            else if (_bmi < 25.0) _category = "Bình thường";
            else if (_bmi < 30.0) _category = "Thừa cân";
            else _category = "Béo phì";

            // Cân nặng lý tưởng theo BMI 18.5..24.9
            _idealMinKg = Math.Round(18.5 * h * h, 1);
            _idealMaxKg = Math.Round(24.9 * h * h, 1);

            // Gợi ý
            if (_category == "Gầy")
                _advice = "Ăn đủ bữa, tăng đạm tốt, ngủ đủ, tập sức mạnh vừa phải.";
            else if (_category == "Bình thường")
                _advice = "Duy trì thói quen tốt: ăn đủ nhóm chất, vận động 150 phút/tuần.";
            else if (_category == "Thừa cân")
                _advice = "Giảm tinh bột đơn, đồ ngọt; tăng đi bộ/tập sức bền.";
            else
                _advice = "Tham vấn bác sĩ/HLV; kiểm soát khẩu phần, tăng vận động từ từ.";

            // Report có “dấu ấn” cá nhân
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== HealthCheck Report ===");
            sb.AppendLine("Chiều cao: " + _heightCm + " cm");
            sb.AppendLine("Cân nặng: " + _weightKg + " kg");
            sb.AppendLine("BMI: " + _bmi);
            sb.AppendLine("Phân loại: " + _category);
            sb.AppendLine("Cân nặng lý tưởng: " + _idealMinKg + " kg - " + _idealMaxKg + " kg");
            sb.AppendLine("Gợi ý: " + _advice);
            sb.AppendLine("Signature: " + _signature);
            _report = sb.ToString();

            if (_category == "Gầy") return 1;
            if (_category == "Bình thường") return 2;
            if (_category == "Thừa cân") return 3;
            return 4;
        }
    }
}