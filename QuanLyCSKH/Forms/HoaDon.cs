using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using QuanLyCSKH.Data; 
using ClosedXML.Excel;

namespace QuanLyCSKH.Forms
{
    public partial class HoaDon : Form
    {
        QLCSKHbContext context = new QLCSKHbContext();
        int idHoaDon;

        public HoaDon()
        {
            InitializeComponent();
        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dgvHoaDon.AutoGenerateColumns = false;

            var danhSachHoaDon = context.HoaDon.Select(r => new
            {
                ID = r.ID,
                NhanVienID = r.NhanVienID,
                KhachHangID = r.KhachHangID,
                HoVaTenNhanVien = r.NhanVien.HoVaTen,
                HoVaTenKhachHang = r.KhachHang.HoVaTen,
                NgayLap = r.NgayLap,


                TongTienHoaDon = r.HoaDon_ChiTiet.Sum(ct => ct.SoLuongBan * ct.DonGiaBan),

                XemChiTiet = "Xem chi tiết"
            }).ToList();

            dgvHoaDon.DataSource = danhSachHoaDon;
        }


        


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow == null) return;

            idHoaDon = Convert.ToInt32(dgvHoaDon.CurrentRow.Cells["ID"].Value);

            using (HoaDon_ChiTiet f = new HoaDon_ChiTiet(idHoaDon))
            {
                f.ShowDialog();
            }
            LoadData();
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow == null) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                idHoaDon = Convert.ToInt32(dgvHoaDon.CurrentRow.Cells["ID"].Value);
                var hd = context.HoaDon.Find(idHoaDon);

                if (hd != null)
                {
                    context.HoaDon.Remove(hd);
                    context.SaveChanges();
                    MessageBox.Show("Đã xóa hóa đơn thành công!");
                    LoadData();
                }
            }
        }

        // 4. Thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 5. Click vào chữ "Xem chi tiết" trên lưới
        private void dgvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có click đúng cột "XemChiTiet" (hay "ChiTiet") không
            if (dgvHoaDon.Columns[e.ColumnIndex].Name == "ChiTiet" && e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dgvHoaDon.Rows[e.RowIndex].Cells["ID"].Value);
                using (HoaDon_ChiTiet f = new HoaDon_ChiTiet(id))
                {
                    f.ShowDialog();
                }
                LoadData();
            }
        }

        // 6. Tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.ToLower().Trim();

            var ketQua = context.HoaDon.Select(r => new
            {
                ID = r.ID,
                NhanVienID = r.NhanVienID,
                KhachHangID = r.KhachHangID,
                HoVaTenNhanVien = r.NhanVien.HoVaTen,
                HoVaTenKhachHang = r.KhachHang.HoVaTen,
                NgayLap = r.NgayLap,
                TongTienHoaDon = r.HoaDon_ChiTiet.Sum(ct => ct.SoLuongBan * ct.DonGiaBan),
                XemChiTiet = "Xem chi tiết"
            }).Where(x => x.HoVaTenNhanVien.ToLower().Contains(keyword)
                       || x.HoVaTenKhachHang.ToLower().Contains(keyword)
                       || x.ID.ToString() == keyword).ToList();

            dgvHoaDon.DataSource = ketQua;
        }

        // 7. Xuất Excel
        private void btnXuat_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Xuất dữ liệu ra tập tin Excel";
            saveFileDialog.Filter = "Tập tin Excel|*.xlsx";
            saveFileDialog.FileName = "HoaDon_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable table = new DataTable();
                    table.Columns.AddRange(new DataColumn[]
                    {
                        new DataColumn("ID", typeof(int)),
                        new DataColumn("NhanVienID", typeof(int)),
                        new DataColumn("KhachHangID", typeof(int)),
                        new DataColumn("NgayLap", typeof(DateTime)),
                        new DataColumn("GhiChuHoaDon", typeof(string)) // Giả sử model có cột này
                    });

                    var danhSachHD = context.HoaDon.ToList();
                    foreach (var p in danhSachHD)
                    {
                        // Kiểm tra xem model của bạn có cột GhiChuHoaDon không, nếu không hãy bỏ dòng p.GhiChuHoaDon đi
                        table.Rows.Add(p.ID, p.NhanVienID, p.KhachHangID, p.NgayLap, p.GhiChuHoaDon);
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet = wb.Worksheets.Add(table, "HoaDon");
                        sheet.Columns().AdjustToContents();
                        wb.SaveAs(saveFileDialog.FileName);
                    }

                    MessageBox.Show("Xuất Excel thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất file: " + ex.Message);
                }
            }
        }

        // 8. Nhập Excel
        private void btnNhap_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Nhập dữ liệu từ tập tin Excel";
            openFileDialog.Filter = "Tập tin Excel|*.xls;*.xlsx";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (XLWorkbook workbook = new XLWorkbook(openFileDialog.FileName))
                    {
                        IXLWorksheet worksheet = workbook.Worksheet(1);
                        var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Bỏ qua dòng tiêu đề

                        int count = 0;
                        foreach (var row in rows)
                        {
                            var hd = new Data.HoaDon
                            {
                                NhanVienID = row.Cell(2).GetValue<int>(), // Cột 2 là NhanVienID
                                KhachHangID = row.Cell(3).GetValue<int>(), // Cột 3 là KhachHangID
                                NgayLap = row.Cell(4).GetValue<DateTime>(), // Cột 4 là NgayLap
                                GhiChuHoaDon = row.Cell(5).GetValue<string>() // Cột 5 là Ghi Chu
                            };
                            context.HoaDon.Add(hd);
                            count++;
                        }

                        context.SaveChanges();
                        LoadData();
                        MessageBox.Show($"Đã nhập thành công {count} dòng.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi nhập file: " + ex.Message);
                }
            }
        }

        private void btnLapHoaDon_Click_1(object sender, EventArgs e)
        {
            using (HoaDon_ChiTiet f = new HoaDon_ChiTiet())
            {
                f.ShowDialog();
            }
            LoadData();
        }
    }
}