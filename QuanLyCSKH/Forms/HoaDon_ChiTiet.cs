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
    public partial class HoaDon_ChiTiet : Form
    {
        QLCSKHbContext context = new QLCSKHbContext();
        int id;
        BindingList<DanhSachHoaDon_ChiTiet> hoaDonChiTiet = new BindingList<DanhSachHoaDon_ChiTiet>();

        public HoaDon_ChiTiet(int maHoaDon = 0)
        {
            InitializeComponent();
            id = maHoaDon;
            this.Load += HoaDon_ChiTiet_Load;
        }

        public void LayNhanVienVaoComboBox()
        {
            cboNhanVien.DataSource = context.NhanVien.ToList();
            cboNhanVien.ValueMember = "ID";
            cboNhanVien.DisplayMember = "HoVaTen";
        }

        public void LayKhachHangVaoComboBox()
        {
            cboKhachHang.DataSource = context.KhachHang.ToList();
            cboKhachHang.ValueMember = "ID";
            cboKhachHang.DisplayMember = "HoVaTen";
        }

        public void LaySanPhamVaoComboBox()
        {
            cboSanPham.DataSource = context.SanPham.ToList();
            cboSanPham.ValueMember = "ID";
            cboSanPham.DisplayMember = "TenSanPham";
        }

        public void BatTatChucNang()
        {
            if (id == 0 && dgvHoaDon_ChiTiet.Rows.Count == 0)
            {
                // Thay vì gán rỗng (Text = ""), ta chọn mặc định người đầu tiên trong danh sách (SelectedIndex = 0)
                // Hoặc nếu muốn để trống hoàn toàn thì dùng: cboKhachHang.SelectedIndex = -1;

                if (cboKhachHang.Items.Count > 0) cboKhachHang.SelectedIndex = 0;
                if (cboNhanVien.Items.Count > 0) cboNhanVien.SelectedIndex = 0;
                if (cboSanPham.Items.Count > 0) cboSanPham.SelectedIndex = 0;

                textBox1.Text = "";
                if (numDonGiaBan != null) numDonGiaBan.Value = 0;
            }

            btnLuuHoaDon.Enabled = dgvHoaDon_ChiTiet.Rows.Count > 0;
            btnXoa.Enabled = dgvHoaDon_ChiTiet.Rows.Count > 0;
        }

        private void HoaDon_ChiTiet_Load(object sender, EventArgs e)
        {
            LayNhanVienVaoComboBox();
            LayKhachHangVaoComboBox();
            LaySanPhamVaoComboBox();

            dgvHoaDon_ChiTiet.AutoGenerateColumns = false;

            if (id != 0)
            {
                var hoaDon = context.HoaDon.Where(r => r.ID == id).SingleOrDefault();
                if (hoaDon != null)
                {
                    cboNhanVien.SelectedValue = hoaDon.NhanVienID;
                    cboKhachHang.SelectedValue = hoaDon.KhachHangID;
                    textBox1.Text = hoaDon.GhiChuHoaDon; // Đã đổi thành textBox1

                    var ct = context.HoaDon_ChiTiet.Where(r => r.HoaDonID == id).Select(r => new DanhSachHoaDon_ChiTiet
                    {
                        ID = r.ID,
                        HoaDonID = r.HoaDonID,
                        SanPhamID = r.SanPhamID,
                        TenSanPham = r.SanPham.TenSanPham,
                        SoLuongBan = (short)r.SoLuongBan,
                        DonGiaBan = (int)r.DonGiaBan,
                        ThanhTien = Convert.ToInt32(r.DonGiaBan)
                    }).ToList();

                    hoaDonChiTiet = new BindingList<DanhSachHoaDon_ChiTiet>(ct);
                }
            }
            dgvHoaDon_ChiTiet.DataSource = hoaDonChiTiet;
            BatTatChucNang();
        }

        private void btnXacNhanBan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboSanPham.Text))
                MessageBox.Show("Vui lòng chọn sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (numDonGiaBan.Value <= 0)
                MessageBox.Show("Đơn giá bán phải lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                int maSanPham = Convert.ToInt32(cboSanPham.SelectedValue);
                var chiTiet = hoaDonChiTiet.FirstOrDefault(x => x.SanPhamID == maSanPham);

                if (chiTiet != null)
                {
                    chiTiet.DonGiaBan = Convert.ToInt32(numDonGiaBan.Value);
                    chiTiet.ThanhTien = Convert.ToInt32(numDonGiaBan.Value);
                    dgvHoaDon_ChiTiet.Refresh();
                }
                else
                {
                    DanhSachHoaDon_ChiTiet ct = new DanhSachHoaDon_ChiTiet
                    {
                        ID = 0,
                        HoaDonID = id,
                        SanPhamID = maSanPham,
                        TenSanPham = cboSanPham.Text,
                        SoLuongBan = 1,
                        DonGiaBan = Convert.ToInt32(numDonGiaBan.Value),
                        ThanhTien = Convert.ToInt32(numDonGiaBan.Value)
                    };
                    hoaDonChiTiet.Add(ct);
                }
                BatTatChucNang();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon_ChiTiet.CurrentRow != null)
            {
                int maSanPham = Convert.ToInt32(dgvHoaDon_ChiTiet.CurrentRow.Cells["SanPhamID"].Value);
                var chiTiet = hoaDonChiTiet.FirstOrDefault(x => x.SanPhamID == maSanPham);
                if (chiTiet != null)
                {
                    hoaDonChiTiet.Remove(chiTiet);
                }
                BatTatChucNang();
            }
        }

        private void btnLuuHoaDon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboNhanVien.Text))
                MessageBox.Show("Vui lòng chọn nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(cboKhachHang.Text))
                MessageBox.Show("Vui lòng chọn khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (id != 0)
                {
                    QuanLyCSKH.Data.HoaDon hd = context.HoaDon.Find(id);
                    if (hd != null)
                    {
                        hd.NhanVienID = Convert.ToInt32(cboNhanVien.SelectedValue);
                        hd.KhachHangID = Convert.ToInt32(cboKhachHang.SelectedValue);
                        hd.GhiChuHoaDon = textBox1.Text; // Đã đổi thành textBox1
                        context.HoaDon.Update(hd);

                        var old = context.HoaDon_ChiTiet.Where(r => r.HoaDonID == id).ToList();
                        context.HoaDon_ChiTiet.RemoveRange(old);

                        foreach (var item in hoaDonChiTiet)
                        {
                            QuanLyCSKH.Data.HoaDon_ChiTiet ct = new QuanLyCSKH.Data.HoaDon_ChiTiet
                            {
                                HoaDonID = id,
                                SanPhamID = item.SanPhamID,
                                SoLuongBan = item.SoLuongBan,
                                DonGiaBan = item.DonGiaBan
                            };
                            context.HoaDon_ChiTiet.Add(ct);
                        }
                        context.SaveChanges();
                    }
                }
                else
                {
                    QuanLyCSKH.Data.HoaDon hd = new QuanLyCSKH.Data.HoaDon
                    {
                        NhanVienID = Convert.ToInt32(cboNhanVien.SelectedValue),
                        KhachHangID = Convert.ToInt32(cboKhachHang.SelectedValue),
                        NgayLap = DateTime.Now,
                        GhiChuHoaDon = textBox1.Text // Đã đổi thành textBox1
                    };
                    context.HoaDon.Add(hd);
                    context.SaveChanges();

                    foreach (var item in hoaDonChiTiet)
                    {
                        QuanLyCSKH.Data.HoaDon_ChiTiet ct = new QuanLyCSKH.Data.HoaDon_ChiTiet
                        {
                            HoaDonID = hd.ID,
                            SanPhamID = item.SanPhamID,
                            SoLuongBan = item.SoLuongBan,
                            DonGiaBan = item.DonGiaBan
                        };
                        context.HoaDon_ChiTiet.Add(ct);
                    }
                    context.SaveChanges();
                    id = hd.ID;
                }
                MessageBox.Show("Đã lưu thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cboSanPham_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedValue != null)
            {
                int maSanPham = Convert.ToInt32(cboSanPham.SelectedValue);
                var sanPham = context.SanPham.Find(maSanPham);
                if (sanPham != null)
                {
                    numDonGiaBan.Value = sanPham.DonGia;
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
                            foreach (DataRow r in table.Rows)
                            {
                                QuanLyCSKH.Data.HoaDon_ChiTiet ct = new QuanLyCSKH.Data.HoaDon_ChiTiet();
                                ct.HoaDonID = Convert.ToInt32(r["HoaDonID"]);
                                ct.SanPhamID = Convert.ToInt32(r["SanPhamID"]);
                                ct.SoLuongBan = Convert.ToInt32(r["SoLuongBan"]);
                                ct.DonGiaBan = Convert.ToInt32(r["DonGiaBan"]);
                                context.HoaDon_ChiTiet.Add(ct);
                            }
                            context.SaveChanges();
                            MessageBox.Show("Đã nhập thành công " + table.Rows.Count + " dòng.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            HoaDon_ChiTiet_Load(sender, e);
                        }
                        if (firstRow) MessageBox.Show("Tập tin Excel rỗng.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Xuất dữ liệu ra tập tin Excel";
            saveFileDialog.Filter = "Tập tin Excel|*.xlsx";
            saveFileDialog.FileName = "HoaDonChiTiet_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable table = new DataTable();
                    table.Columns.AddRange(new DataColumn[5]
                    {
                        new DataColumn("ID", typeof(int)),
                        new DataColumn("HoaDonID", typeof(int)),
                        new DataColumn("SanPhamID", typeof(int)),
                        new DataColumn("SoLuongBan", typeof(int)),
                        new DataColumn("DonGiaBan", typeof(int))
                    });

                    var chiTiet = context.HoaDon_ChiTiet.ToList();
                    foreach (var p in chiTiet)
                    {
                        table.Rows.Add(p.ID, p.HoaDonID, p.SanPhamID, p.SoLuongBan, p.DonGiaBan);
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet = wb.Worksheets.Add(table, "HoaDonChiTiet");
                        sheet.Columns().AdjustToContents();
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

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng in đang được nâng cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    // DỜI CLASS NÀY XUỐNG ĐÂY ĐỂ TRÁNH LỖI DESIGNER
    public class DanhSachHoaDon_ChiTiet
    {
        public int ID { get; set; }
        public int HoaDonID { get; set; }
        public int SanPhamID { get; set; }
        public string TenSanPham { get; set; }
        public short SoLuongBan { get; set; }
        public int DonGiaBan { get; set; }
        public int ThanhTien { get; set; }
    }
}