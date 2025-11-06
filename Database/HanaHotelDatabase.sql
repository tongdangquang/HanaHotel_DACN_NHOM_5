USE master;
GO

DROP DATABASE IF EXISTS HanaHotel;
GO

CREATE DATABASE HanaHotel;
GO

USE HanaHotel;
GO

CREATE TABLE LoaiPhong (
    MaLoaiPhong INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TenLoaiPhong NVARCHAR(255) NULL,
    DeleteFlag CHAR(1) NOT NULL,
    CreateID INT NOT NULL,
    CreateDateTime DATETIME NOT NULL,
    UpdateID INT NOT NULL,
    UpdateDateTime DATETIME NOT NULL
);

CREATE TABLE KhuyenMai (
    MaKM INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TenKM NVARCHAR(255) NULL,
    GiaKM MONEY NULL,
    NgayBatDau DATETIME NULL,
    NgayKetThuc DATETIME NULL,
    DeleteFlag CHAR(1) NOT NULL,
    CreateID INT NOT NULL,
    CreateDateTime DATETIME NOT NULL,
    UpdateID INT NOT NULL,
    UpdateDateTime DATETIME NOT NULL
);

CREATE TABLE KhachSan (
    MaKhachSan INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TenKhachSan NVARCHAR(255) NULL,
    SoLuongNhanVien INT NULL,
    SoLuongPhong INT NULL,
    TrangThai NVARCHAR(255) NULL,
    DiaChi NVARCHAR(255) NULL,
    DiaChiTinhThanhPho NVARCHAR(255) NULL,
    MaNguoiDungQuanLy INT NULL,
    DeleteFlag CHAR(1) NOT NULL,
    CreateID INT NOT NULL,
    CreateDateTime DATETIME NOT NULL,
    UpdateID INT NOT NULL,
    UpdateDateTime DATETIME NOT NULL
);

CREATE TABLE Phong (
    MaPhong INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TenPhong NVARCHAR(100) NULL,
    TinhTrang NVARCHAR(50) NULL,
    MoTa NVARCHAR(1000) NULL,
    KichThuoc DECIMAL(10, 1) NULL,
    DonGia MONEY NULL,
    MaLoaiPhong INT NULL,
    MaKhachSan INT NULL,
    DeleteFlag CHAR(1) NOT NULL,
    CreateID INT NOT NULL,
    CreateDateTime DATETIME NOT NULL,
    UpdateID INT NOT NULL,
    UpdateDateTime DATETIME NOT NULL
);

CREATE TABLE NguoiDung (
    MaNguoiDung INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    HoTen NVARCHAR(255) NULL,
    CCCD VARCHAR(20) NULL,
    NgaySinh DATETIME NULL,
    GioiTinh CHAR(10) NULL,
    DiaChi NVARCHAR(255) NULL,
    DienThoai VARCHAR(20) NULL,
    Email NVARCHAR(255) NULL,
    TenDangNhap VARCHAR(100) NULL,
    MatKhau NVARCHAR(255) NULL,
    ChucVu NVARCHAR(100) NULL,
    Quyen NVARCHAR(100) NULL,
    MaNguoiDungQuanLy INT NULL,
    DeleteFlag CHAR(1) NOT NULL,
    CreateID INT NOT NULL,
    CreateDateTime DATETIME NOT NULL,
    UpdateID INT NOT NULL,
    UpdateDateTime DATETIME NOT NULL
);

CREATE TABLE DichVu (
    MaDichVu INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TenDichVu NVARCHAR(255) NULL,
    MoTa NVARCHAR(4000) NULL,
    DonGia MONEY NULL,
    TrangThai NVARCHAR(255) NULL,
    DeleteFlag CHAR(1) NOT NULL,
    CreateID INT NOT NULL,
    CreateDateTime DATETIME NOT NULL,
    UpdateID INT NOT NULL,
    UpdateDateTime DATETIME NOT NULL
);

CREATE TABLE DatPhong (
    MaDatPhong INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    NgayDat DATETIME NULL,
    NgayNhan DATETIME NULL,
    NgayTra DATETIME NULL,
    SoNguoiLon INT NULL,
    SoTreEm INT NULL,
    SoLuongPhong INT NULL,
    TrangThai NVARCHAR(255) NULL,
    HoTenKH NVARCHAR(255) NULL,
    Email NVARCHAR(255) NULL,
    DienThoai VARCHAR(20) NULL,
    QuocGia NVARCHAR(255) NULL,
    YeuCauThem NVARCHAR(4000) NULL,
    MaPhong INT NULL,
    MaNguoiDung INT NULL,
    MaDichVu INT NULL,
    DeleteFlag CHAR(1) NOT NULL,
    CreateID INT NOT NULL,
    CreateDateTime DATETIME NOT NULL,
    UpdateID INT NOT NULL,
    UpdateDateTime DATETIME NOT NULL
);

CREATE TABLE DanhGia (
    MaDanhGia INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    SoSao FLOAT NULL,
    NoiDung NVARCHAR(4000) NULL,
    MaDatPhong INT NULL,
    MaNguoiDung INT NULL,
    MaDichVu INT NULL,
    DeleteFlag CHAR(1) NOT NULL,
    CreateID INT NOT NULL,
    CreateDateTime DATETIME NOT NULL,
    UpdateID INT NOT NULL,
    UpdateDateTime DATETIME NOT NULL
);

CREATE TABLE HinhAnh (
    MaAnh INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TenAnh NVARCHAR(255) NULL,
    DuongDan NVARCHAR(255) NULL,
    MaPhong INT NULL,
    MaDanhGia INT NULL,
    DeleteFlag CHAR(1) NOT NULL,
    CreateID INT NOT NULL,
    CreateDateTime DATETIME NOT NULL,
    UpdateID INT NOT NULL,
    UpdateDateTime DATETIME NOT NULL
);

CREATE TABLE ChiTietKhuyenMai (
    MaKM INT NOT NULL,
    MaPhong INT NOT NULL,
    PhanTramKM FLOAT NULL,
    DeleteFlag CHAR(1) NOT NULL,
    CreateID INT NOT NULL,
    CreateDateTime DATETIME NOT NULL,
    UpdateID INT NOT NULL,
    UpdateDateTime DATETIME NOT NULL,
    PRIMARY KEY (MaKM, MaPhong)
);

CREATE TABLE PhuongThucThanhToan (
    MaPhuongThuc INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TenPhuongThuc NVARCHAR(255) NULL,
    DeleteFlag CHAR(1) NOT NULL,
    CreateID INT NOT NULL,
    CreateDateTime DATETIME NOT NULL,
    UpdateID INT NOT NULL,
    UpdateDateTime DATETIME NOT NULL
);

CREATE TABLE ChiTietThanhToan (
    MaDatPhong INT NOT NULL,
    MaPhuongThuc INT NOT NULL,
    ThoiGianThanhToan DATETIME NULL,
    DeleteFlag CHAR(1) NOT NULL,
    CreateID INT NOT NULL,
    CreateDateTime DATETIME NOT NULL,
    UpdateID INT NOT NULL,
    UpdateDateTime DATETIME NOT NULL,
    PRIMARY KEY (MaDatPhong, MaPhuongThuc)
);

CREATE TABLE [Option] (
    TypeName VARCHAR(255) NOT NULL,
    TypeKey VARCHAR(255) NOT NULL,
    TypeValue NVARCHAR(255) NULL,
    DeleteFlag CHAR(1) NOT NULL,
    CreateID INT NOT NULL,
    CreateDateTime DATETIME NOT NULL,
    UpdateID INT NOT NULL,
    UpdateDateTime DATETIME NOT NULL,
    PRIMARY KEY (TypeName, TypeKey)
);


-- 1. NguoiDung
INSERT INTO NguoiDung (HoTen, CCCD, NgaySinh, GioiTinh, DiaChi, DienThoai, Email, TenDangNhap, MatKhau, ChucVu, Quyen,
                       MaNguoiDungQuanLy, DeleteFlag, CreateID, CreateDateTime, UpdateID, UpdateDateTime)
VALUES
(N'Nguyễn Văn Anh', '123456789', '1990-06-01', '0', N'Hà Nội', '0901111111', 'admin1@gmail.com', 'admin1', '123456', N'Quản lý', N'1', NULL, 0, 1, GETDATE(), 1, GETDATE()),
(N'Trần Thị Bình', '987654321', '1991-06-01', '1', N'Hồ Chí Minh', '0902222222', 'ketoan1@gmail.com', 'ketoan1', '123456', N'Kế toán', N'2', 1, 0, 1, GETDATE(), 1, GETDATE()),
(N'Lê Văn Chuyền', '555666777', '1992-06-01', '0', N'Đà Nẵng', '0903333333', 'letan1@gmail.com', 'letan1', '123456', N'Lê tân', N'3', 1, 0, 1, GETDATE(), 1, GETDATE()),
(N'Phạm Thị Dung', '888999000', '1993-06-01', '1', N'Cần Thơ', '0904444444', 'user1@gmail.com', 'user1', '123456', N'Khách hàng', N'4', 1, 0, 1, GETDATE(), 1, GETDATE()),
(N'Hoàng Văn Dương', '111222333', '1994-06-01', '0', N'Hải Phòng', '0905555555', 'user2@gmail.com', 'user2', '123456', N'Khách hàng', N'4', 1, 0, 1, GETDATE(), 1, GETDATE());

-- 2. LoaiPhong
INSERT INTO LoaiPhong (TenLoaiPhong, DeleteFlag, CreateID, CreateDateTime, UpdateID, UpdateDateTime)
VALUES
(N'Tiêu chuẩn', 0, 1, GETDATE(), 1, GETDATE()),
(N'Cao cấp', 0, 1, GETDATE(), 1, GETDATE());

-- 3. KhachSan
INSERT INTO KhachSan (TenKhachSan, SoLuongNhanVien, SoLuongPhong, TrangThai, DiaChi, DiaChiTinhThanhPho,
                      MaNguoiDungQuanLy, DeleteFlag, CreateID, CreateDateTime, UpdateID, UpdateDateTime)
VALUES
(N'Hana Hotel Hà Nội', 30, 100, N'0', N'123 Trần Phú, Hà Nội', N'Hà Nội', 1, 0, 1, GETDATE(), 1, GETDATE()),
(N'Hana Hotel Sài Gòn', 40, 150, N'1', N'456 Lê Lợi, Quận 1', N'Hồ Chí Minh', 1, 0, 1, GETDATE(), 1, GETDATE()),
(N'Hana Hotel Đà Nẵng', 25, 80, N'1', N'789 Võ Nguyên Giáp', N'Đà Nẵng', 1, 0, 1, GETDATE(), 1, GETDATE()),
(N'Hana Hotel Cần Thơ', 20, 60, N'2', N'12 Nguyễn Trãi', N'Cần Thơ', 1, 0, 1, GETDATE(), 1, GETDATE()),
(N'Hana Hotel Hải Phòng', 28, 90, N'1', N'88 Lạch Tray', N'Hải Phòng', 1, 0, 1, GETDATE(), 1, GETDATE());

-- 4. Phong
INSERT INTO Phong (TenPhong, TinhTrang, MoTa, KichThuoc, DonGia, MaLoaiPhong, MaKhachSan,
                   DeleteFlag, CreateID, CreateDateTime, UpdateID, UpdateDateTime)
VALUES
(N'P101', '0', N'Phòng hướng phố', 25, 800000, 1, 1, 0, 1, GETDATE(), 1, GETDATE()),
(N'P102', '1', N'Phòng hướng vườn', 30, 1000000, 2, 1, 0, 1, GETDATE(), 1, GETDATE()),
(N'P201', '0', N'Phòng ban công rộng', 35, 1200000, 1, 2, 0, 1, GETDATE(), 1, GETDATE()),
(N'P202', '2', N'Phòng tầng cao', 40, 1500000, 2, 3, 0, 1, GETDATE(), 1, GETDATE()),
(N'P301', '0', N'Phòng VIP', 50, 2000000, 1, 2, 0, 1, GETDATE(), 1, GETDATE());

-- 5. DichVu
INSERT INTO DichVu (TenDichVu, MoTa, DonGia, TrangThai, DeleteFlag, CreateID, CreateDateTime, UpdateID, UpdateDateTime)
VALUES
(N'Dọn phòng', N'Dịch vụ dọn phòng hàng ngày', 100000, '1', 0, 1, GETDATE(), 1, GETDATE()),
(N'Giặt ủi', N'Dịch vụ giặt ủi nhanh 24h', 150000, '0', 0, 1, GETDATE(), 1, GETDATE()),
(N'Spa', N'Dịch vụ massage và spa', 300000, '1', 0, 1, GETDATE(), 1, GETDATE()),
(N'Đưa đón sân bay', N'Dịch vụ đưa đón bằng xe riêng', 500000, '0', 0, 1, GETDATE(), 1, GETDATE()),
(N'Ăn sáng buffet', N'Buffet sáng phong phú', 200000, '1', 0, 1, GETDATE(), 1, GETDATE());

-- 6. KhuyenMai
INSERT INTO KhuyenMai (TenKM, GiaKM, NgayBatDau, NgayKetThuc, DeleteFlag, CreateID, CreateDateTime, UpdateID, UpdateDateTime)
VALUES
(N'Giảm giá mùa hè', 100000, '2025-06-01', '2025-08-31', 0, 1, GETDATE(), 1, GETDATE()),
(N'Giảm 10% cuối tuần', 0, '2025-05-01', '2025-12-31', 0, 1, GETDATE(), 1, GETDATE()),
(N'Mừng khai trương', 200000, '2025-01-01', '2025-03-31', 0, 1, GETDATE(), 1, GETDATE()),
(N'Khuyến mãi lễ 30/4', 300000, '2025-04-25', '2025-05-05', 0, 1, GETDATE(), 1, GETDATE()),
(N'Tặng buffet sáng', 0, '2025-09-01', '2025-09-30', 0, 1, GETDATE(), 1, GETDATE());

-- 7. DatPhong
INSERT INTO DatPhong (NgayDat, NgayNhan, NgayTra, SoNguoiLon, SoTreEm, SoLuongPhong, TrangThai,
                      HoTenKH, Email, DienThoai, QuocGia, YeuCauThem,
                      MaPhong, MaNguoiDung, MaDichVu, DeleteFlag, CreateID, CreateDateTime, UpdateID, UpdateDateTime)
VALUES
(GETDATE(), '2025-11-01', '2025-11-03', 2, 1, 1, '1', N'Lê Quốc', 'lequoc@gmail.com', '0909999991', N'Việt Nam', N'Không hút thuốc', 1, 1, 1, 0, 1, GETDATE(), 1, GETDATE()),
(GETDATE(), '2025-11-05', '2025-11-08', 1, 0, 1, '2', N'Trần Bình', 'binhtran@gmail.com', '0909999992', N'Việt Nam', N'Phòng yên tĩnh', 2, 2, 2, 0, 1, GETDATE(), 1, GETDATE()),
(GETDATE(), '2025-12-01', '2025-12-04', 2, 2, 1, '3', N'Ngô Hoa', 'hoango@gmail.com', '0909999993', N'Việt Nam', N'Cần nôi em bé', 3, 3, 3, 0, 1, GETDATE(), 1, GETDATE()),
(GETDATE(), '2025-12-15', '2025-12-18', 3, 1, 2, '4', N'Phạm Dũng', 'dungpham@gmail.com', '0909999994', N'Việt Nam', N'Đặt 2 phòng liền nhau', 4, 4, 4, 0, 1, GETDATE(), 1, GETDATE()),
(GETDATE(), '2025-12-25', '2025-12-28', 2, 0, 1, '5', N'Hoàng Nam', 'namhoang@gmail.com', '0909999995', N'Việt Nam', N'Gần thang máy', 5, 5, 5, 0, 1, GETDATE(), 1, GETDATE());

-- 8. DanhGia
INSERT INTO DanhGia (SoSao, NoiDung, MaDatPhong, MaNguoiDung, MaDichVu,
                     DeleteFlag, CreateID, CreateDateTime, UpdateID, UpdateDateTime)
VALUES
(5, N'Rất hài lòng', 1, 1, 1, 0, 1, GETDATE(), 1, GETDATE()),
(4, N'Tốt, nhưng nên cải thiện wifi', 2, 2, 2, 0, 1, GETDATE(), 1, GETDATE()),
(3.5, N'Tạm ổn', 3, 3, 3, 0, 1, GETDATE(), 1, GETDATE()),
(4.5, N'Dịch vụ chuyên nghiệp', 4, 4, 4, 0, 1, GETDATE(), 1, GETDATE()),
(5, N'Spa rất tuyệt vời', 5, 5, 5, 0, 1, GETDATE(), 1, GETDATE());

-- 9. HinhAnh
INSERT INTO HinhAnh (TenAnh, DuongDan, MaPhong, MaDanhGia,
                     DeleteFlag, CreateID, CreateDateTime, UpdateID, UpdateDateTime)
VALUES
(N'Phong101_1.jpg', N'/images/phong101_1.jpg', 1, 1, 0, 1, GETDATE(), 1, GETDATE()),
(N'Phong102_1.jpg', N'/images/phong102_1.jpg', 2, 2, 0, 1, GETDATE(), 1, GETDATE()),
(N'Phong201_1.jpg', N'/images/phong201_1.jpg', 3, 3, 0, 1, GETDATE(), 1, GETDATE()),
(N'Phong202_1.jpg', N'/images/phong202_1.jpg', 4, 4, 0, 1, GETDATE(), 1, GETDATE()),
(N'Phong301_1.jpg', N'/images/phong301_1.jpg', 5, 5, 0, 1, GETDATE(), 1, GETDATE());

-- 10. PhuongThucThanhToan
INSERT INTO PhuongThucThanhToan (TenPhuongThuc, DeleteFlag, CreateID, CreateDateTime, UpdateID, UpdateDateTime)
VALUES
(N'Tiền mặt', 0, 1, GETDATE(), 1, GETDATE()),
(N'Thẻ tín dụng', 0, 1, GETDATE(), 1, GETDATE()),
(N'Chuyển khoản', 0, 1, GETDATE(), 1, GETDATE()),
(N'Ví Momo', 0, 1, GETDATE(), 1, GETDATE()),
(N'ZaloPay', 0, 1, GETDATE(), 1, GETDATE());

-- 11. ChiTietKhuyenMai
INSERT INTO ChiTietKhuyenMai (MaKM, MaPhong, PhanTramKM, DeleteFlag, CreateID, CreateDateTime, UpdateID, UpdateDateTime)
VALUES
(1, 1, 10, 0, 1, GETDATE(), 1, GETDATE()),
(1, 2, 15, 0, 1, GETDATE(), 1, GETDATE()),
(2, 3, 5, 0, 1, GETDATE(), 1, GETDATE()),
(3, 4, 20, 0, 1, GETDATE(), 1, GETDATE()),
(4, 5, 25, 0, 1, GETDATE(), 1, GETDATE());

-- 12. ChiTietThanhToan
INSERT INTO ChiTietThanhToan (MaDatPhong, MaPhuongThuc, ThoiGianThanhToan, DeleteFlag, CreateID, CreateDateTime, UpdateID, UpdateDateTime)
VALUES
(1, 1, GETDATE(), 0, 1, GETDATE(), 1, GETDATE()),
(2, 2, GETDATE(), 0, 1, GETDATE(), 1, GETDATE()),
(3, 3, GETDATE(), 0, 1, GETDATE(), 1, GETDATE()),
(4, 4, GETDATE(), 0, 1, GETDATE(), 1, GETDATE()),
(5, 5, GETDATE(), 0, 1, GETDATE(), 1, GETDATE());

-- 13. Option
INSERT INTO [Option] (TypeName, TypeKey, TypeValue, DeleteFlag, CreateID, CreateDateTime, UpdateID, UpdateDateTime)
VALUES
('NguoiDung_GioiTinh', '0', N'Nam', 0, 1, GETDATE(), 1, GETDATE()),
('NguoiDung_GioiTinh', '1', N'Nữ', 0, 1, GETDATE(), 1, GETDATE()),
('NguoiDung_Quyen', '1', N'Admin', 0, 1, GETDATE(), 1, GETDATE()),
('NguoiDung_Quyen', '2', N'Kế toán', 0, 1, GETDATE(), 1, GETDATE()),
('NguoiDung_Quyen', '3', N'Lê tân', 0, 1, GETDATE(), 1, GETDATE()),
('NguoiDung_Quyen', '4', N'Khách hàng', 0, 1, GETDATE(), 1, GETDATE()),
('KhachSan_TrangThai', '0', N'Không hoạt động', 0, 1, GETDATE(), 1, GETDATE()),
('KhachSan_TrangThai', '1', N'Hoạt động', 0, 1, GETDATE(), 1, GETDATE()),
('KhachSan_TrangThai', '2', N'Bảo trì', 0, 1, GETDATE(), 1, GETDATE()),
('Phong_TinhTrang', '0', N'Trống', 0, 1, GETDATE(), 1, GETDATE()),
('Phong_TinhTrang', '1', N'Đã được đặt', 0, 1, GETDATE(), 1, GETDATE()),
('Phong_TinhTrang', '2', N'Đang bảo trì', 0, 1, GETDATE(), 1, GETDATE()),
('DichVu_TrangThai', '0', N'Không hoạt động', 0, 1, GETDATE(), 1, GETDATE()),
('DichVu_TrangThai', '1', N'Hoạt động', 0, 1, GETDATE(), 1, GETDATE()),
('DatPhong_TrangThai', '1', N'Đang xử lý', 0, 1, GETDATE(), 1, GETDATE()),
('DatPhong_TrangThai', '2', N'Đã xác nhận', 0, 1, GETDATE(), 1, GETDATE()),
('DatPhong_TrangThai', '3', N'Đã thanh toán', 0, 1, GETDATE(), 1, GETDATE()),
('DatPhong_TrangThai', '4', N'Đã hủy', 0, 1, GETDATE(), 1, GETDATE()),
('DatPhong_TrangThai', '5', N'Kết thúc', 0, 1, GETDATE(), 1, GETDATE());

SELECT * FROM DatPhong