using QuanLyCSKH.Data;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using BC = BCrypt.Net.BCrypt;

namespace QuanLyCSKH.Forms
{
    public partial class Main : Form
    {
        // Khởi tạo ngữ cảnh Database
        QLCSKHbContext context = new QLCSKHbContext();

        // ĐỔI TÊN BIẾN (Thêm dấu _ để không trùng với tên Class Form)
        // Lưu ý: Tên Class vẫn giữ nguyên là SanPham, KhachHang...
        QuanLyCSKH.Forms.SanPham _sanPham = null;
        QuanLyCSKH.Forms.KhachHang _khachHang = null;
        QuanLyCSKH.Forms.NhanVien _nhanVien = null;
        QuanLyCSKH.Forms.HoaDon _hoaDon = null;
        QuanLyCSKH.Forms.PhanCongChamSoc _phanCongChamSoc = null;
        QuanLyCSKH.Forms.DangNhap _dangNhap = null;

        string hoVaTenNhanVien = "";

        public Main()
        {
            InitializeComponent();
            this.Load += Main_Load;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ChuaDangNhap();
            ThucHienDangNhap();
        }

        private void ThucHienDangNhap()
        {
        LamLai:
            if (_dangNhap == null || _dangNhap.IsDisposed)
                _dangNhap = new QuanLyCSKH.Forms.DangNhap();

            if (_dangNhap.ShowDialog() == DialogResult.OK)
            {
                // Truy cập vào TextBox (Nhớ chỉnh Modifiers của TextBox sang Public ở bên Form DangNhap)
                string tenDangNhap = _dangNhap.txtTenDangNhap.Text;
                string matKhau = _dangNhap.txtMatKhau.Text;

                if (string.IsNullOrWhiteSpace(tenDangNhap))
                {
                    MessageBox.Show("Tên đăng nhập không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _dangNhap.txtTenDangNhap.Focus();
                    goto LamLai;
                }
                else if (string.IsNullOrWhiteSpace(matKhau))
                {
                    MessageBox.Show("Mật khẩu không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _dangNhap.txtMatKhau.Focus();
                    goto LamLai;
                }
                else
                {
                    var nv = context.NhanVien.Where(r => r.TenDangNhap == tenDangNhap).SingleOrDefault();

                    if (nv == null)
                    {
                        MessageBox.Show("Tên đăng nhập không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _dangNhap.txtTenDangNhap.Focus();
                        goto LamLai;
                    }
                    else
                    {
                        // Kiểm tra mật khẩu (Dùng BC.Verify nếu có mã hóa, hoặc so sánh trực tiếp nếu chưa mã hóa)
                        if (matKhau == nv.MatKhau)
                        {
                            hoVaTenNhanVien = nv.HoVaTen;

                            if (nv.QuyenHan == true)
                                QuyenQuanLy();
                            else
                                QuyenNhanVien();
                        }
                        else
                        {
                            MessageBox.Show("Mật khẩu không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            _dangNhap.txtMatKhau.Focus();
                            goto LamLai;
                        }
                    }
                }
            }
        }

        public void ChuaDangNhap()
        {
            mnuDangNhap.Enabled = true;
            mnuDangXuat.Enabled = false;
            mnuDoiMatKhau.Enabled = false;
            mnuPhanCongChamSoc.Enabled = false;
            mnuSanPham.Enabled = false;
            mnuKhachHang.Enabled = false;
            mnuNhanVien.Enabled = false;
            mnuHoaDon.Enabled = false;
            mnuThongKeSanPham.Enabled = false;
            mnuThongKeDoanhThu.Enabled = false;
            lblTrangThai.Text = "Chưa đăng nhập.";
        }

        public void QuyenQuanLy()
        {
            mnuDangNhap.Enabled = false;
            mnuDangXuat.Enabled = true;
            mnuDoiMatKhau.Enabled = true;
            mnuPhanCongChamSoc.Enabled = true;
            mnuSanPham.Enabled = true;
            mnuKhachHang.Enabled = true;
            mnuNhanVien.Enabled = true;
            mnuHoaDon.Enabled = true;
            mnuThongKeSanPham.Enabled = true;
            mnuThongKeDoanhThu.Enabled = true;
            lblTrangThai.Text = "Quản lý: " + hoVaTenNhanVien;
        }

        public void QuyenNhanVien()
        {
            mnuDangNhap.Enabled = false;
            mnuSanPham.Enabled = false;
            mnuNhanVien.Enabled = false;
            mnuDangXuat.Enabled = true;
            mnuDoiMatKhau.Enabled = true;
            mnuPhanCongChamSoc.Enabled = true;
            mnuKhachHang.Enabled = true;
            mnuHoaDon.Enabled = true;
            mnuThongKeSanPham.Enabled = true;
            mnuThongKeDoanhThu.Enabled = true;
            lblTrangThai.Text = "Nhân viên: " + hoVaTenNhanVien;
        }

        private void mnuDangNhap_Click(object sender, EventArgs e) => ThucHienDangNhap();

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            foreach (Form child in this.MdiChildren) child.Close();
            ChuaDangNhap();
        }

        private void mnuThoat_Click(object sender, EventArgs e) => this.Close();

        private void mnuPhanCongChamSoc_Click(object sender, EventArgs e)
        {
            if (_phanCongChamSoc == null || _phanCongChamSoc.IsDisposed)
            {
                _phanCongChamSoc = new QuanLyCSKH.Forms.PhanCongChamSoc();
                _phanCongChamSoc.MdiParent = this;
                _phanCongChamSoc.Show();
            }
            else _phanCongChamSoc.Activate();
        }

        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            if (_nhanVien == null || _nhanVien.IsDisposed)
            {
                _nhanVien = new QuanLyCSKH.Forms.NhanVien();
                _nhanVien.MdiParent = this;
                _nhanVien.Show();
            }
            else _nhanVien.Activate();
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            if (_khachHang == null || _khachHang.IsDisposed)
            {
                _khachHang = new QuanLyCSKH.Forms.KhachHang();
                _khachHang.MdiParent = this;
                _khachHang.Show();
            }
            else _khachHang.Activate();
        }

        private void mnuSanPham_Click(object sender, EventArgs e)
        {
            if (_sanPham == null || _sanPham.IsDisposed)
            {
                _sanPham = new QuanLyCSKH.Forms.SanPham();
                _sanPham.MdiParent = this;
                _sanPham.Show();
            }
            else _sanPham.Activate();
        }

        private void mnuHoaDon_Click(object sender, EventArgs e)
        {
            if (_hoaDon == null || _hoaDon.IsDisposed)
            {
                _hoaDon = new QuanLyCSKH.Forms.HoaDon();
                _hoaDon.MdiParent = this;
                _hoaDon.Show();
            }
            else _hoaDon.Activate();
        }

        private void lblLienKet_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("explorer.exe", "https://fit.agu.edu.vn"));
        }

        // Các sự kiện menu trống
        private void hệThốngToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void mnuDoiMatKhau_Click(object sender, EventArgs e) { }
        private void mnuThongKeSanPham_Click(object sender, EventArgs e) { }
        private void mnuThongKeDoanhThu_Click(object sender, EventArgs e) { }
        private void mnuHuongDanSuDung_Click(object sender, EventArgs e) { }
        private void mnuThongTinPhanMem_Click(object sender, EventArgs e) { }
        private void lblTrangThai_Click(object sender, EventArgs e) { }
    }
}