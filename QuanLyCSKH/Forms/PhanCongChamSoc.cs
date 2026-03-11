using ClosedXML.Excel;
using QuanLyCSKH.Data;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyCSKH.Forms
{
    // CLASS FORM PHẢI ĐẶT LÊN ĐẦU ĐỂ KHÔNG BỊ LỖI MÀN HÌNH DESIGN
    public partial class PhanCongChamSoc : Form
    {
        QLCSKHbContext context = new QLCSKHbContext();

        int idPhanCongHienTai = 0; // Lưu ID của dòng đang chọn
        bool isThemMoi = false;    // Cờ đánh dấu đang ở chế độ Thêm hay Sửa

        public PhanCongChamSoc()
        {
            InitializeComponent();
            this.Load += PhanCongChamSoc_Load;
        }

        private void PhanCongChamSoc_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            LoadDataGidView();
            BatTatChucNang(false); // Khóa các ô nhập liệu lúc mới lên form
            SetButtonState(true);   // Bật Thêm/Sửa/Xóa, Tắt Lưu/Hủy
        }

        public void LoadComboBoxes()
        {
            // Load Nhân viên
            cboNhanVien.DataSource = context.NhanVien.ToList();
            cboNhanVien.ValueMember = "ID";
            cboNhanVien.DisplayMember = "HoVaTen";

            // Load Khách hàng
            cboKhachHang.DataSource = context.KhachHang.ToList();
            cboKhachHang.ValueMember = "ID";
            cboKhachHang.DisplayMember = "HoVaTen";

            // Load Hình thức
            cboHinhThuc.Items.Clear();
            cboHinhThuc.Items.AddRange(new string[] { "Gọi điện thoại", "Nhắn tin Zalo/SMS", "Gặp trực tiếp", "Gửi Email" });
            if (cboHinhThuc.Items.Count > 0) cboHinhThuc.SelectedIndex = 0;

            // Load Kết quả
            cboKetQua.Items.Clear();
            cboKetQua.Items.AddRange(new string[] { "Chưa liên lạc được", "Đang suy nghĩ", "Chốt lịch hẹn thành công", "Từ chối", "Sai số/Thuê bao" });
            if (cboKetQua.Items.Count > 0) cboKetQua.SelectedIndex = 0;
        }

        public void LoadDataGidView()
        {
            dgvPhanCong.AutoGenerateColumns = false; // Tắt tự động tạo cột để map đúng với class

            var danhSach = context.PhanCongChamSoc
                .Select(p => new DanhSachPhanCong
                {
                    ID = p.ID,
                    TenNhanVien = p.NhanVien.HoVaTen,
                    TenKhachHang = p.KhachHang.HoVaTen,
                    NgayChamSoc = p.NgayChamSoc,
                    HinhThuc = p.HinhThuc,
                    NoiDung = p.NoiDung,
                    KetQua = p.KetQua
                }).ToList();

            // Sử dụng BindingList tương tự form HoaDon_ChiTiet
            BindingList<DanhSachPhanCong> bindingList = new BindingList<DanhSachPhanCong>(danhSach);
            dgvPhanCong.DataSource = bindingList;
        }

        public void BatTatChucNang(bool state)
        {
            cboNhanVien.Enabled = state;
            cboKhachHang.Enabled = state;
            cboHinhThuc.Enabled = state;
            cboKetQua.Enabled = state;
            dtpNgayChamSoc.Enabled = state;
            txtNoiDung.Enabled = state;
        }

        public void SetButtonState(bool normalState)
        {
            btnThem.Enabled = normalState;
            btnSua.Enabled = normalState;
            btnXoa.Enabled = normalState;

            btnLuu.Enabled = !normalState;
            btnHuy.Enabled = !normalState;
        }

        public void ClearInputs()
        {
            txtNoiDung.Clear();
            dtpNgayChamSoc.Value = DateTime.Now;
            if (cboHinhThuc.Items.Count > 0) cboHinhThuc.SelectedIndex = 0;
            if (cboKetQua.Items.Count > 0) cboKetQua.SelectedIndex = 0;
        }

        private void dgvPhanCong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && btnThem.Enabled == true)
            {
                DataGridViewRow row = dgvPhanCong.Rows[e.RowIndex];
                idPhanCongHienTai = Convert.ToInt32(row.Cells["ID"].Value);

                var pc = context.PhanCongChamSoc.Find(idPhanCongHienTai);
                if (pc != null)
                {
                    cboNhanVien.SelectedValue = pc.NhanVienID;
                    cboKhachHang.SelectedValue = pc.KhachHangID;
                    dtpNgayChamSoc.Value = pc.NgayChamSoc;
                    cboHinhThuc.Text = pc.HinhThuc;
                    cboKetQua.Text = pc.KetQua;
                    txtNoiDung.Text = pc.NoiDung;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isThemMoi = true;
            ClearInputs();
            BatTatChucNang(true);
            SetButtonState(false);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (idPhanCongHienTai == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            isThemMoi = false;
            BatTatChucNang(true);
            SetButtonState(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (idPhanCongHienTai == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var pc = context.PhanCongChamSoc.Find(idPhanCongHienTai);
                if (pc != null)
                {
                    context.PhanCongChamSoc.Remove(pc);
                    context.SaveChanges();
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadDataGidView();
                    ClearInputs();
                    idPhanCongHienTai = 0;
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNoiDung.Text))
            {
                MessageBox.Show("Vui lòng nhập nội dung chăm sóc!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNoiDung.Focus();
                return;
            }

            if (isThemMoi)
            {
                var pc = new QuanLyCSKH.Data.PhanCongChamSoc
                {
                    NhanVienID = Convert.ToInt32(cboNhanVien.SelectedValue),
                    KhachHangID = Convert.ToInt32(cboKhachHang.SelectedValue),
                    NgayChamSoc = dtpNgayChamSoc.Value,
                    HinhThuc = cboHinhThuc.Text,
                    KetQua = cboKetQua.Text,
                    NoiDung = txtNoiDung.Text
                };
                context.PhanCongChamSoc.Add(pc);
            }
            else
            {
                var pc = context.PhanCongChamSoc.Find(idPhanCongHienTai);
                if (pc != null)
                {
                    pc.NhanVienID = Convert.ToInt32(cboNhanVien.SelectedValue);
                    pc.KhachHangID = Convert.ToInt32(cboKhachHang.SelectedValue);
                    pc.NgayChamSoc = dtpNgayChamSoc.Value;
                    pc.HinhThuc = cboHinhThuc.Text;
                    pc.KetQua = cboKetQua.Text;
                    pc.NoiDung = txtNoiDung.Text;
                    context.PhanCongChamSoc.Update(pc);
                }
            }

            context.SaveChanges();
            MessageBox.Show("Đã lưu thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadDataGidView();
            BatTatChucNang(false);
            SetButtonState(true);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            SetButtonState(true);
            ClearInputs();
            idPhanCongHienTai = 0;
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Xuất dữ liệu ra tập tin Excel";
            saveFileDialog.Filter = "Tập tin Excel|*.xlsx";
            saveFileDialog.FileName = "DanhSachPhanCong_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Sử dụng DataTable theo mẫu
                    DataTable table = new DataTable();
                    table.Columns.AddRange(new DataColumn[7]
                    {
                        new DataColumn("Mã PC", typeof(int)),
                        new DataColumn("Tên Nhân Viên", typeof(string)),
                        new DataColumn("Tên Khách Hàng", typeof(string)),
                        new DataColumn("Ngày Chăm Sóc", typeof(string)),
                        new DataColumn("Hình Thức", typeof(string)),
                        new DataColumn("Nội Dung", typeof(string)),
                        new DataColumn("Kết Quả", typeof(string))
                    });

                    var data = context.PhanCongChamSoc.Select(p => new {
                        p.ID,
                        TenNV = p.NhanVien.HoVaTen,
                        TenKH = p.KhachHang.HoVaTen,
                        p.NgayChamSoc,
                        p.HinhThuc,
                        p.NoiDung,
                        p.KetQua
                    }).ToList();

                    foreach (var p in data)
                    {
                        table.Rows.Add(p.ID, p.TenNV, p.TenKH, p.NgayChamSoc.ToString("dd/MM/yyyy"), p.HinhThuc, p.NoiDung, p.KetQua);
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet = wb.Worksheets.Add(table, "PhanCong");
                        sheet.Columns().AdjustToContents(); // Căn chỉnh kích thước cột
                        wb.SaveAs(saveFileDialog.FileName);
                    }
                    MessageBox.Show("Xuất Excel thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Nhập dữ liệu từ tập tin Excel";
            openFileDialog.Filter = "Tập tin Excel|*.xls;*.xlsx";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable table = new DataTable();
                    using (XLWorkbook workbook = new XLWorkbook(openFileDialog.FileName))
                    {
                        IXLWorksheet worksheet = workbook.Worksheet(1);
                        bool firstRow = true;
                        string readRange = "1:1";

                        foreach (IXLRow row in worksheet.RowsUsed())
                        {
                            if (firstRow)
                            {
                                readRange = string.Format("{0}:{1}", 1, row.LastCellUsed().Address.ColumnNumber);
                                foreach (IXLCell cell in row.Cells(readRange))
                                    table.Columns.Add(cell.Value.ToString());
                                firstRow = false;
                            }
                            else
                            {
                                table.Rows.Add();
                                int cellIndex = 0;
                                foreach (IXLCell cell in row.Cells(readRange))
                                {
                                    table.Rows[table.Rows.Count - 1][cellIndex] = cell.Value.ToString();
                                    cellIndex++;
                                }
                            }
                        }

                        if (table.Rows.Count > 0)
                        {
                            int count = 0;
                            foreach (DataRow r in table.Rows)
                            {
                                // Tương thích với cột từ DataTable
                                string tenNV = r[1].ToString().Trim(); // Cột B
                                string tenKH = r[2].ToString().Trim(); // Cột C

                                var nv = context.NhanVien.FirstOrDefault(x => x.HoVaTen == tenNV);
                                var kh = context.KhachHang.FirstOrDefault(x => x.HoVaTen == tenKH);

                                if (nv != null && kh != null)
                                {
                                    DateTime parsedDate;
                                    bool isDateValid = DateTime.TryParse(r[3].ToString(), out parsedDate);

                                    QuanLyCSKH.Data.PhanCongChamSoc pc = new QuanLyCSKH.Data.PhanCongChamSoc();
                                    pc.NhanVienID = nv.ID;
                                    pc.KhachHangID = kh.ID;
                                    pc.NgayChamSoc = isDateValid ? parsedDate : DateTime.Now;
                                    pc.HinhThuc = r[4].ToString().Trim();
                                    pc.NoiDung = r[5].ToString().Trim();
                                    pc.KetQua = r[6].ToString().Trim();

                                    context.PhanCongChamSoc.Add(pc);
                                    count++;
                                }
                            }
                            context.SaveChanges();
                            MessageBox.Show($"Đã nhập thành công {count} dòng.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataGidView(); // Gọi lại load grid
                        }
                        if (firstRow) MessageBox.Show("Tập tin Excel rỗng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    // DỜI CLASS NÀY XUỐNG ĐÂY ĐỂ TRÁNH LỖI DESIGNER VÀ SỬ DỤNG CHO DATAGRIDVIEW
    public class DanhSachPhanCong
    {
        public int ID { get; set; }
        public string TenNhanVien { get; set; }
        public string TenKhachHang { get; set; }
        public DateTime NgayChamSoc { get; set; }
        public string HinhThuc { get; set; }
        public string NoiDung { get; set; }
        public string KetQua { get; set; }
    }
}