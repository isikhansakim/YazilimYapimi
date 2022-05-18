# YazilimYapimi

Programın çalışması için program dosyası indirilip içerisine şu kodlar ile sql tabloları oluşturulmalı;
CREATE TABLE Tbl_Kullanici (Kullanici_id int primary key identity (1,1) ,KullaniciAdi varchar(50), Sifre varchar(50), Adi varchar(50), Soyadi varchar(50)); 
CREATE TABLE Tbl_Ogrenci (Kullanici_id int, Ogr_id int primary key identity (1,1), KayitTarihi Datetime, Ogr_Sinif int ); 
CREATE TABLE Tbl_SinavSorumlusu (Kullanici_id int, Sorumlu_id int primary key identity (1,1), KayitTarihi Datetime ); 
CREATE TABLE Tbl_Sorular(Soru_id int primary key identity (1,1), Soru_UniteNo int, Soru_KonuNo int, Soru_No int, Soru_Sinifi int, Soru_Dersi varchar(50), Soru_Unitesi varchar(50), Soru_Konusu varchar(50), Soru_Cevap varchar(1), Soru_Metni varchar(5000), Soru_Resmi varchar(100), Soru_SIKA varchar(1000), Soru_AResim varchar(100), Soru_SIKB varchar(1000), Soru_BResim varchar(100), Soru_SIKC varchar(1000), Soru_CResim varchar(100), Soru_SIKD varchar(1000), Soru_DResim varchar(100));
CREATE TABLE Tbl_DogruSorular (Soru_id int  , Ogr_id int , BilinmeSayisi tinyint , BilindigiGun Datetime, Silinecek tinyint)
CREATE TABLE Tbl_SinavSorular (Sinav_Soru_id int primary key identity(1,1),sorular_soru_id int, doğruMu tinyint default '0')

PROJEYİ YAPANLAR:1- MERT POSTACI  ÖĞRENCİ NUMARASI:202803061 SINIFI: 2.SINIF 2.ÖĞRETİM
                 2- EZER ARDA     ÖĞRENCİ NUMARASI:202803026 SINIFI: 2.SINIF 2.ÖĞRETİM
                 3- IŞIKHAN SAKIM ÖĞRENCİ NUMARASI:202803039 SINIFI: 2.SINIF 2.ÖĞRETİM
