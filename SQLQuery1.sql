USE QLCSKH;
GO

-- 1. Xóa dữ liệu các bảng cũ (Nếu có)
-- Phải xóa theo thứ tự: Bảng con xóa trước, bảng cha xóa sau
DELETE FROM HoaDon_ChiTiet;
DELETE FROM PhanCongChamSoc;
DELETE FROM HoaDon;
DELETE FROM SanPham;
DELETE FROM NhanVien;
DELETE FROM KhachHang;

-- 2. Reset identity (Đưa bộ đếm ID tự tăng về lại số 0)
IF EXISTS (SELECT * FROM sys.identity_columns WHERE OBJECT_NAME(OBJECT_ID) = 'HoaDon_ChiTiet' AND last_value IS NOT NULL)
	DBCC CHECKIDENT('HoaDon_ChiTiet', RESEED, 0);

IF EXISTS (SELECT * FROM sys.identity_columns WHERE OBJECT_NAME(OBJECT_ID) = 'PhanCongChamSoc' AND last_value IS NOT NULL)
	DBCC CHECKIDENT('PhanCongChamSoc', RESEED, 0);

IF EXISTS (SELECT * FROM sys.identity_columns WHERE OBJECT_NAME(OBJECT_ID) = 'HoaDon' AND last_value IS NOT NULL)
	DBCC CHECKIDENT('HoaDon', RESEED, 0);

IF EXISTS (SELECT * FROM sys.identity_columns WHERE OBJECT_NAME(OBJECT_ID) = 'SanPham' AND last_value IS NOT NULL)
	DBCC CHECKIDENT('SanPham', RESEED, 0);

IF EXISTS (SELECT * FROM sys.identity_columns WHERE OBJECT_NAME(OBJECT_ID) = 'NhanVien' AND last_value IS NOT NULL)
	DBCC CHECKIDENT('NhanVien', RESEED, 0);
	
IF EXISTS (SELECT * FROM sys.identity_columns WHERE OBJECT_NAME(OBJECT_ID) = 'KhachHang' AND last_value IS NOT NULL)
	DBCC CHECKIDENT('KhachHang', RESEED, 0);

-- 3. Chèn dữ liệu mẫu vào các bảng
INSERT INTO NhanVien (HoVaTen, DienThoai, Email, TrangThai, TenDangNhap, MatKhau, QuyenHan) VALUES
(N'Trần Chốt Đơn', '0901000001', 'chotdon@congty.com', N'Đang làm việc', 'chotdon', '123456', 1),
(N'Nguyễn Kẻ Hủy Diệt KPI', '0901000002', 'huydietkpi@congty.com', N'Đang làm việc', 'huydiet', '123456', 0),
(N'Lê Thánh Sale', '0901000003', 'thanhsale@congty.com', N'Đang làm việc', 'thanhsale', '123456', 0),
(N'Phạm Chúa Tể Data', '0901000004', 'chuatedata@congty.com', N'Đang làm việc', 'chuatedata', '123456', 0),
(N'Đặng Thần Đồng Telesale', '0901000005', 'thandong@congty.com', N'Đang làm việc', 'thandong', '123456', 0),
(N'Vũ Cỗ Máy Bán Hàng', '0901000006', 'comay@congty.com', N'Đang làm việc', 'comay', '123456', 0),
(N'Hoàng Độc Cô Cầu Bại', '0901000007', 'docco@congty.com', N'Đang làm việc', 'docco', '123456', 0),
(N'Bùi Vua Cạp Data', '0901000008', 'vuacap@congty.com', N'Đang làm việc', 'vuacap', '123456', 0),
(N'Đỗ Bàn Tay Vàng', '0901000009', 'bantayvang@congty.com', N'Đang làm việc', 'bantayvang', '123456', 0),
(N'Hồ Ma Tốc Độ Gọi', '0901000010', 'matocdo@congty.com', N'Đang làm việc', 'matocdo', '123456', 0),
(N'Đoàn Sát Thủ Lạnh Lùng', '0901000015', 'satthu@congty.com', N'Đã nghỉ việc', 'satthu', '123456', 0);

INSERT INTO KhachHang (HoVaTen, DienThoai, DiaChi, NgaySinh, NhomKhachHang) VALUES
(N'Khách Đại Gia Ngầm', '0988000001', N'Biệt thự Thảo Điền, Quận 2, TP.HCM', '1980-01-01', N'VIP'),
(N'Khách Seen Không Rep', '0988000002', N'Hẻm cụt, Gò Vấp, TP.HCM', '1995-02-14', N'Khách thường'),
(N'Khách Hỏi Giá Không Mua', '0988000003', N'Khu phố mộng mơ, Đà Lạt', '1998-05-20', N'Tiềm năng'),
(N'Khách Chờ Lương Về', '0988000004', N'KCN Sóng Thần, Bình Dương', '1992-10-10', N'Tiềm năng'),
(N'Khách Bom Hàng Quốc Dân', '0988000005', N'Đảo hoang, không rõ địa chỉ', '2000-04-01', N'Danh sách đen'),
(N'Khách Thích Mặc Cả', '0988000006', N'Chợ Đồng Xuân, Hà Nội', '1975-08-08', N'Khách thường'),
(N'Khách Hỏi Vợ Đã', '0988000007', N'Khu dân cư sợ vợ, Cần Thơ', '1988-11-11', N'Tiềm năng'),
(N'Khách Đòi Freeship', '0988000008', N'Đỉnh núi Fansipan, Lào Cai', '1999-09-09', N'Khách thường'),
(N'Khách Chốt Không Cần Giá', '0988000009', N'Landmark 81, TP.HCM', '1985-12-25', N'VIP'),
(N'Khách Giận Dỗi', '0988000017', N'Bến Ninh Kiều, Cần Thơ', '1997-07-27', N'Danh sách đen');

INSERT INTO SanPham (TenSanPham, DonGia, SoLuong, HinhAnh) VALUES
(N'Liệu trình trị mụn chuẩn Y Khoa', 499000, 5, N'trimun.jpg'),
(N'Liệu trình chăm sóc da chuyên sâu', 899000, 3, N'chamsoc.jpg'),
(N'Combo Phục hồi da mỏng yếu', 1200000, 2, N'phuchoi.png'),
(N'Kem trị nám tàn nhang', 550000, 8, N'trinam.jpg'),
(N'Serum HA cấp ẩm đa tầng', 350000, 6, N'serumha.png');
GO

INSERT INTO HoaDon (NhanVienID, KhachHangID, NgayLap, GhiChuHoaDon) VALUES
(1, 1, '2026-03-01', N'Trạng thái: Hoàn thành | Tổng tiền: 50.000.000đ (Trần Chốt Đơn chốt Khách Đại Gia Ngầm 50 củ)'),
(2, 4, '2026-03-05', N'Trạng thái: Hoàn thành | Tổng tiền: 120.000.000đ (Kẻ Hủy Diệt KPI bán cho Khách Chốt Không Cần Giá)'),
(3, 2, '2026-03-10', N'Trạng thái: Đang giao hàng | Tổng tiền: 150.000đ (Thánh Sale bán cho Khách Đòi Freeship)'),
(1, 5, '2026-03-12', N'Trạng thái: Đã hủy | Tổng tiền: 5.000.000đ (Sát Thủ Lạnh Lùng đụng độ Khách Bom Hàng)'),
(4, 3, '2026-03-15', N'Trạng thái: Hoàn thành | Tổng tiền: 99.000đ (Nữ Hoàng Khuyến Mãi tung deal sốc)'),
(2, 1, '2026-03-18', N'Trạng thái: Hoàn thành | Tổng tiền: 25.000.000đ (Cỗ Máy Bán Hàng chốt khách cũ)'),
(4, 3, '2026-03-19', N'Trạng thái: Chờ thanh toán | Tổng tiền: 8.500.000đ (Vua Cạp Data đang đợi khách chuyển khoản)');
GO

INSERT INTO PhanCongChamSoc (NhanVienID, KhachHangID, NgayChamSoc, HinhThuc, NoiDung, KetQua, NgayHenLai) VALUES
(10, 2, '2026-03-15', N'Gọi điện', N'Gọi giục chốt đơn áo thun', N'Thuê bao quý khách vừa gọi...', NULL), -- Ma Tốc Độ Gọi vs Khách Seen Không Rep
(1, 1, '2026-03-16', N'Gặp trực tiếp', N'Mời sếp đi nhậu ký hợp đồng dự án mới', N'Chốt ngay trên bàn nhậu', NULL), -- Trần Chốt Đơn vs Khách Đại Gia
(11, 17, '2026-03-17', N'Nhắn tin Zalo', N'Chị ơi shop xin lỗi vụ giao nhầm màu hôm nọ nha', N'Đã xem và Block', NULL), -- Bậc Thầy Chăm Sóc vs Khách Giận Dỗi
(5, 7, '2026-03-18', N'Gọi điện', N'Tư vấn chốt máy giặt cửa ngang giá 12 củ', N'Để anh hỏi vợ đã em ơi', '2026-03-25 09:00:00'), -- Thần Đồng Telesale vs Khách Hỏi Vợ Đã (Hẹn 25/03 gọi lại)
(14, 19, '2026-03-19', N'Gọi điện', N'Tư vấn gói vay vốn lúc 2h sáng', N'Tâm sự chuyện đời, chưa chốt', '2026-03-21 23:00:00'), -- Đôi Môi Mật Ngọt vs Khách Gọi Nửa Đêm
(2, 12, '2026-03-20', N'Gọi điện', N'Báo giá gói cước Internet', N'Vẫn chê đắt, cúp máy ngang', NULL), -- Kẻ Hủy Diệt KPI vs Khách Chê Đắt
(8, 13, '2026-03-20', N'Gặp trực tiếp', N'Đưa mã QR cho quét thanh toán', N'Quên mang điện thoại, thề mai chuyển', '2026-03-21 10:00:00'), -- Vua Cạp Data vs Khách Hẹn Mai Chuyển Khoản
(16, 4, '2026-03-20', N'Nhắn tin Zalo', N'Nhắc nhẹ khoản nợ mua trả góp', N'Em ơi ráng đợi mùng 5 lương về nha', '2026-04-05 08:30:00'); -- Kẻ Săn Mồi vs Khách Chờ Lương Về
GO
