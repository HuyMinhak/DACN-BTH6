namespace QuanLyCSKH.Forms
{
    partial class HoaDon_ChiTiet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HoaDon_ChiTiet));
            cboNhanVien = new ComboBox();
            btnNhap = new Button();
            btnXuat = new Button();
            btnThoat = new Button();
            groupBox1 = new GroupBox();
            dgvHoaDon_ChiTiet = new DataGridView();
            ID = new DataGridViewTextBoxColumn();
            TenSanPham = new DataGridViewTextBoxColumn();
            DonGiaBan = new DataGridViewTextBoxColumn();
            ThanhTien = new DataGridViewTextBoxColumn();
            groupBox2 = new GroupBox();
            btnXacNhanBan = new Button();
            cboSanPham = new ComboBox();
            numDonGiaBan = new NumericUpDown();
            textBox1 = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            cboKhachHang = new ComboBox();
            label1 = new Label();
            btnInHoaDon = new Button();
            btnLuuHoaDon = new Button();
            btnXoa = new Button();
            label6 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHoaDon_ChiTiet).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDonGiaBan).BeginInit();
            SuspendLayout();
            // 
            // cboNhanVien
            // 
            cboNhanVien.FormattingEnabled = true;
            cboNhanVien.Items.AddRange(new object[] { "Khách vãng lai", "Khách hàng mới", "Khách hàng thân thiết", "Khách VIP", "Khách lâu chưa quay lại" });
            cboNhanVien.Location = new Point(173, 41);
            cboNhanVien.Name = "cboNhanVien";
            cboNhanVien.Size = new Size(199, 28);
            cboNhanVien.TabIndex = 26;
            // 
            // btnNhap
            // 
            btnNhap.Location = new Point(491, 412);
            btnNhap.Name = "btnNhap";
            btnNhap.Size = new Size(94, 29);
            btnNhap.TabIndex = 24;
            btnNhap.Text = "Nhập";
            btnNhap.UseVisualStyleBackColor = true;
            btnNhap.Click += btnNhap_Click;
            // 
            // btnXuat
            // 
            btnXuat.Location = new Point(591, 412);
            btnXuat.Name = "btnXuat";
            btnXuat.Size = new Size(94, 29);
            btnXuat.TabIndex = 23;
            btnXuat.Text = "Xuất";
            btnXuat.UseVisualStyleBackColor = true;
            btnXuat.Click += btnXuat_Click;
            // 
            // btnThoat
            // 
            btnThoat.ForeColor = Color.Red;
            btnThoat.Location = new Point(691, 412);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(94, 29);
            btnThoat.TabIndex = 21;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.Controls.Add(dgvHoaDon_ChiTiet);
            groupBox1.Location = new Point(0, 208);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(800, 203);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Danh sách chi tiết hóa đơn";
            // 
            // dgvHoaDon_ChiTiet
            // 
            dgvHoaDon_ChiTiet.AllowUserToAddRows = false;
            dgvHoaDon_ChiTiet.AllowUserToDeleteRows = false;
            dgvHoaDon_ChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHoaDon_ChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHoaDon_ChiTiet.Columns.AddRange(new DataGridViewColumn[] { ID, TenSanPham, DonGiaBan, ThanhTien });
            dgvHoaDon_ChiTiet.Dock = DockStyle.Fill;
            dgvHoaDon_ChiTiet.Location = new Point(3, 23);
            dgvHoaDon_ChiTiet.Margin = new Padding(2, 1, 2, 1);
            dgvHoaDon_ChiTiet.MultiSelect = false;
            dgvHoaDon_ChiTiet.Name = "dgvHoaDon_ChiTiet";
            dgvHoaDon_ChiTiet.ReadOnly = true;
            dgvHoaDon_ChiTiet.RowHeadersWidth = 82;
            dgvHoaDon_ChiTiet.Size = new Size(794, 177);
            dgvHoaDon_ChiTiet.TabIndex = 2;
            // 
            // ID
            // 
            ID.DataPropertyName = "ID";
            ID.HeaderText = "ID";
            ID.MinimumWidth = 10;
            ID.Name = "ID";
            ID.ReadOnly = true;
            // 
            // TenSanPham
            // 
            TenSanPham.DataPropertyName = "TenSanPham";
            TenSanPham.HeaderText = "Tên Sản Phẩm";
            TenSanPham.MinimumWidth = 6;
            TenSanPham.Name = "TenSanPham";
            TenSanPham.ReadOnly = true;
            // 
            // DonGiaBan
            // 
            DonGiaBan.DataPropertyName = "DonGiaBan";
            DonGiaBan.HeaderText = "Đơn Giá Bán";
            DonGiaBan.MinimumWidth = 6;
            DonGiaBan.Name = "DonGiaBan";
            DonGiaBan.ReadOnly = true;
            // 
            // ThanhTien
            // 
            ThanhTien.DataPropertyName = "ThanhTien";
            ThanhTien.HeaderText = "Thành Tiền";
            ThanhTien.MinimumWidth = 6;
            ThanhTien.Name = "ThanhTien";
            ThanhTien.ReadOnly = true;
            // 
            // groupBox2
            // 
            groupBox2.AutoSize = true;
            groupBox2.Controls.Add(btnXacNhanBan);
            groupBox2.Controls.Add(cboSanPham);
            groupBox2.Controls.Add(numDonGiaBan);
            groupBox2.Controls.Add(textBox1);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(cboKhachHang);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(btnInHoaDon);
            groupBox2.Controls.Add(btnLuuHoaDon);
            groupBox2.Controls.Add(groupBox1);
            groupBox2.Controls.Add(cboNhanVien);
            groupBox2.Controls.Add(btnNhap);
            groupBox2.Controls.Add(btnXuat);
            groupBox2.Controls.Add(btnThoat);
            groupBox2.Controls.Add(btnXoa);
            groupBox2.Controls.Add(label6);
            groupBox2.Location = new Point(3, 1);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(806, 467);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Thông tin chi tiết hóa đơn";
            // 
            // btnXacNhanBan
            // 
            btnXacNhanBan.ForeColor = Color.FromArgb(0, 0, 192);
            btnXacNhanBan.Location = new Point(561, 171);
            btnXacNhanBan.Name = "btnXacNhanBan";
            btnXacNhanBan.Size = new Size(94, 29);
            btnXacNhanBan.TabIndex = 37;
            btnXacNhanBan.Text = "Xác nhận bán";
            btnXacNhanBan.UseVisualStyleBackColor = true;
            btnXacNhanBan.Click += btnXacNhanBan_Click;
            // 
            // cboSanPham
            // 
            cboSanPham.DropDownWidth = 350;
            cboSanPham.FormattingEnabled = true;
            cboSanPham.Items.AddRange(new object[] { "Liệu trình trị mụn chuẩn Y khoa", "Chăm sóc da mặt chuyên sâu", "Phục hồi da mỏng yếu, nổi mao mạch", "Tắm trắng phi thuyền hoàng gia", "Triệt lông nách vĩnh viễn (Trọn gói)", "Triệt lông toàn thân VIP", "Nâng cơ trẻ hóa da công nghệ Hifu", "Massage body đá nóng đả thông kinh lạc", "Phun mày tán bột tự nhiên", "Khử thâm môi Collagen" });
            cboSanPham.Location = new Point(18, 174);
            cboSanPham.Name = "cboSanPham";
            cboSanPham.Size = new Size(224, 28);
            cboSanPham.TabIndex = 36;
            // 
            // numDonGiaBan
            // 
            numDonGiaBan.Location = new Point(333, 173);
            numDonGiaBan.Name = "numDonGiaBan";
            numDonGiaBan.Size = new Size(203, 27);
            numDonGiaBan.TabIndex = 35;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(173, 89);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(594, 27);
            textBox1.TabIndex = 34;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(333, 138);
            label4.Name = "label4";
            label4.Size = new Size(85, 20);
            label4.TabIndex = 33;
            label4.Text = "Đơn giá (*):";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 89);
            label3.Name = "label3";
            label3.Size = new Size(120, 20);
            label3.TabIndex = 32;
            label3.Text = "Ghi chú hóa đơn:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 138);
            label2.Name = "label2";
            label2.Size = new Size(98, 20);
            label2.TabIndex = 31;
            label2.Text = "Sản phẩm (*):";
            // 
            // cboKhachHang
            // 
            cboKhachHang.FormattingEnabled = true;
            cboKhachHang.Items.AddRange(new object[] { "Khách vãng lai", "Khách hàng mới", "Khách hàng thân thiết", "Khách VIP", "Khách lâu chưa quay lại" });
            cboKhachHang.Location = new Point(551, 44);
            cboKhachHang.Name = "cboKhachHang";
            cboKhachHang.Size = new Size(216, 28);
            cboKhachHang.TabIndex = 30;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(427, 49);
            label1.Name = "label1";
            label1.Size = new Size(109, 20);
            label1.TabIndex = 29;
            label1.Text = "Khách hàng (*):";
            // 
            // btnInHoaDon
            // 
            btnInHoaDon.Location = new Point(245, 412);
            btnInHoaDon.Name = "btnInHoaDon";
            btnInHoaDon.Size = new Size(213, 29);
            btnInHoaDon.TabIndex = 28;
            btnInHoaDon.Text = "In hóa đơn";
            btnInHoaDon.UseVisualStyleBackColor = true;
            btnInHoaDon.Click += btnInHoaDon_Click;
            // 
            // btnLuuHoaDon
            // 
            btnLuuHoaDon.ForeColor = Color.Blue;
            btnLuuHoaDon.Location = new Point(9, 412);
            btnLuuHoaDon.Name = "btnLuuHoaDon";
            btnLuuHoaDon.Size = new Size(213, 29);
            btnLuuHoaDon.TabIndex = 27;
            btnLuuHoaDon.Text = "Lưu hóa đơn";
            btnLuuHoaDon.UseVisualStyleBackColor = false;
            btnLuuHoaDon.Click += btnLuuHoaDon_Click;
            // 
            // btnXoa
            // 
            btnXoa.ForeColor = Color.Red;
            btnXoa.Location = new Point(673, 173);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 29);
            btnXoa.TabIndex = 19;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(18, 44);
            label6.Name = "label6";
            label6.Size = new Size(123, 20);
            label6.TabIndex = 4;
            label6.Text = "Nhân viên lập (*):";
            // 
            // HoaDon_ChiTiet
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "HoaDon_ChiTiet";
            Text = "Hóa đơn chi tiết";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHoaDon_ChiTiet).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDonGiaBan).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cboNhanVien;
        private Button btnNhap;
        private Button btnXuat;
        private Button btnThoat;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button btnXoa;
        private Label label6;
        private Button btnInHoaDon;
        private Button btnLuuHoaDon;
        private ComboBox cboKhachHang;
        private Label label1;
        private ComboBox cboSanPham;
        private NumericUpDown numDonGiaBan;
        private TextBox textBox1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Button btnXacNhanBan;
        private DataGridView dgvHoaDon_ChiTiet;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn TenSanPham;
        private DataGridViewTextBoxColumn DonGiaBan;
        private DataGridViewTextBoxColumn ThanhTien;
    }
}